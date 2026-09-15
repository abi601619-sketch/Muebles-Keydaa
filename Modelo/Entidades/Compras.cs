using Modelo.Conexión_DB;
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Modelo.Entidades
{
    public class ComprasDb
    {
        private int IdCompra;
        private DateTime FechaCompra;
        private decimal TotalCompra;
        private int IdProveedor;

        public ComprasDb() { }

        public ComprasDb(int idCompra, DateTime fechaCompra, decimal totalCompra, int idProveedor)
        {
            IdCompra1 = idCompra;
            FechaCompra1 = fechaCompra;
            TotalCompra1 = totalCompra;
            IdProveedor1 = idProveedor;
        }

        public int IdCompra1 { get => IdCompra; set => IdCompra = value; }
        public DateTime FechaCompra1 { get => FechaCompra; set => FechaCompra = value; }
        public decimal TotalCompra1 { get => TotalCompra; set => TotalCompra = value; }
        public int IdProveedor1 { get => IdProveedor; set => IdProveedor = value; }

        public static DataTable CargarComprasRegistradas()
        {
            SqlConnection conectar = Conexion.Conectar();

            string comando = "SELECT * FROM VerCompras;";
            SqlDataAdapter adapter = new SqlDataAdapter(comando, conectar);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            return dt;
        }

        public int InsertarCompra()
        {
            string comandoSQL = @"INSERT INTO Compras (FechaCompra,TotalCompra,IdProveedor)VALUES
            (@FechaCompra,@TotalCompra,@IdProveedor); SELECT CAST(SCOPE_IDENTITY() AS INT);"; //SELECT CAST(SCOPE_IDENTITY() AS INT); SIRVE PARA OBTENER EL ID QUE SLQ RECIENTEMENTE GUARDO

            using (SqlConnection conexion = Conexion.Conectar())
            {
                using (SqlCommand comandoObjeto = new SqlCommand(comandoSQL, conexion))
                {
                    comandoObjeto.Parameters.AddWithValue("@FechaCompra", FechaCompra);

                    comandoObjeto.Parameters.AddWithValue("@IdProveedor", IdProveedor);

                    SqlParameter parametro = comandoObjeto.Parameters.Add("@TotalCompra", SqlDbType.Decimal);
                    parametro.Precision = 10;
                    parametro.Scale = 2;
                    parametro.Value = TotalCompra;

                    try
                    {
                        int idCompra =
                            Convert.ToInt32(comandoObjeto.ExecuteScalar());

                        return idCompra;
                    }
                    catch (SqlException ex)
                    {
                        MessageBox.Show("Ocurrió un error al guardar la compra.\n\n" + ex.Message, "Error " + ex.Number, MessageBoxButtons.OK, MessageBoxIcon.Error);

                        return 0;
                    }
                }

            }
        }

        public static bool ActualizarCompra(int idCompra, DateTime fechaCompra, decimal totalCompra, int idProveedor)
        {
            string comandoSQL = @"UPDATE Compras SET FechaCompra = @FechaCompra,TotalCompra = @TotalCompra,IdProveedor = @IdProveedor WHERE IdCompra = @IdCompra;";

            using (SqlConnection conexion = Conexion.Conectar())
            {
                try
                {
                    using (SqlCommand comando = new SqlCommand(comandoSQL, conexion))
                    {
                        comando.Parameters.AddWithValue("@IdCompra", idCompra);
                        comando.Parameters.AddWithValue("@FechaCompra", fechaCompra);
                        comando.Parameters.AddWithValue("@TotalCompra", totalCompra);
                        comando.Parameters.AddWithValue("@IdProveedor", idProveedor);

                        int filasAfectadas = comando.ExecuteNonQuery();

                        return filasAfectadas > 0;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al actualizar la compra: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    return false;
                }
            }
        }

        public static DataTable ObtenerCompraPorId(int idCompra)
        {
            string comandoSQL = @"SELECT IdCompra, FechaCompra, TotalCompra, IdProveedor FROM Compras WHERE IdCompra = @IdCompra;";

            using (SqlConnection conexion = Conexion.Conectar())
            {
                try
                {
                    using (SqlCommand comando = new SqlCommand(comandoSQL, conexion))
                    {
                        comando.Parameters.AddWithValue("@IdCompra", idCompra);

                        SqlDataAdapter adapter = new SqlDataAdapter(comando);
                        DataTable dt = new DataTable();

                        adapter.Fill(dt);

                        return dt;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al obtener la compra: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    return null;
                }
            }
        }

        public static int GuardarCompleta(int idCompra, DateTime fecha, int idProveedor,
            IList<DetalleCompraMaterial> detalles)
        {
            if (detalles.Count == 0)
                throw new InvalidOperationException("Agrega al menos un material.");
            using (SqlConnection conexion = Conexion.Conectar())
            using (SqlTransaction transaccion = conexion.BeginTransaction(IsolationLevel.Serializable))
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = conexion;
                cmd.Transaction = transaccion;
                cmd.Parameters.AddWithValue("@IdCompra", idCompra);
                cmd.Parameters.AddWithValue("@Fecha", fecha);
                cmd.Parameters.AddWithValue("@Proveedor", idProveedor);
                var sql = new StringBuilder(@"
                    DECLARE @Detalles TABLE (Id int, Material int, Cantidad int, Precio decimal(10,2));");
                for (int i = 0; i < detalles.Count; i++)
                {
                    var d = detalles[i];
                    if (d.Cantidad1 <= 0 || d.PrecioUnitario1 < 0)
                        throw new InvalidOperationException("Revisa la cantidad y el precio de los materiales.");
                    sql.AppendFormat("INSERT INTO @Detalles VALUES (@d{0}, @m{0}, @c{0}, @p{0});", i);
                    cmd.Parameters.AddWithValue("@d" + i, d.IdDetalleCompraMaterial1);
                    cmd.Parameters.AddWithValue("@m" + i, d.IdMaterial1);
                    cmd.Parameters.AddWithValue("@c" + i, d.Cantidad1);
                    var precio = cmd.Parameters.Add("@p" + i, SqlDbType.Decimal);
                    precio.Precision = 10;
                    precio.Scale = 2;
                    precio.Value = d.PrecioUnitario1;
                }
                sql.Append(@"
                    IF @IdCompra = 0
                    BEGIN
                        INSERT INTO Compras (FechaCompra, TotalCompra, IdProveedor)
                        VALUES (@Fecha, 0, @Proveedor);
                        SET @IdCompra = CONVERT(int, SCOPE_IDENTITY());
                    END
                    ELSE IF NOT EXISTS (SELECT 1 FROM Compras WITH (UPDLOCK, HOLDLOCK) WHERE IdCompra = @IdCompra)
                        THROW 50002, 'La compra ya no existe. Vuelva a cargar la lista.', 1;
                    IF EXISTS (SELECT 1 FROM @Detalles d WHERE d.Id <> 0 AND NOT EXISTS
                        (SELECT 1 FROM DetalleCompraMaterial o WHERE o.IdCompra = @IdCompra AND o.IdDetalleCompraMaterial = d.Id))
                        THROW 50003, 'Los detalles cambiaron. Vuelva a cargar la compra.', 1;
                    DECLARE @Cambios TABLE (Material int PRIMARY KEY, Cantidad int);
                    INSERT INTO @Cambios
                    SELECT Material, SUM(Cantidad) FROM (
                        SELECT Material, Cantidad FROM @Detalles
                        UNION ALL
                        SELECT IdMaterial, -Cantidad FROM DetalleCompraMaterial WITH (UPDLOCK, HOLDLOCK)
                        WHERE IdCompra = @IdCompra
                    ) movimientos GROUP BY Material;
                    IF EXISTS (SELECT 1 FROM @Cambios c LEFT JOIN Material m WITH (UPDLOCK, HOLDLOCK)
                        ON m.IdMaterial = c.Material WHERE m.IdMaterial IS NULL OR m.Stock + c.Cantidad < 0)
                        THROW 50004, 'No hay stock suficiente para revertir la compra: parte del material ya fue utilizado.', 1;
                    UPDATE m SET Stock = Stock + c.Cantidad FROM Material m
                        JOIN @Cambios c ON m.IdMaterial = c.Material;
                    DELETE FROM DetalleCompraMaterial WHERE IdCompra = @IdCompra
                        AND IdDetalleCompraMaterial NOT IN (SELECT Id FROM @Detalles);
                    UPDATE o SET IdMaterial = d.Material, Cantidad = d.Cantidad, PrecioUnitario = d.Precio
                        FROM DetalleCompraMaterial o JOIN @Detalles d ON o.IdDetalleCompraMaterial = d.Id
                        WHERE o.IdCompra = @IdCompra;
                    INSERT INTO DetalleCompraMaterial (IdCompra, IdMaterial, Cantidad, PrecioUnitario)
                        SELECT @IdCompra, Material, Cantidad, Precio FROM @Detalles WHERE Id = 0;
                    UPDATE Compras SET FechaCompra = @Fecha, IdProveedor = @Proveedor,
                        TotalCompra = (SELECT SUM(Cantidad * Precio) FROM @Detalles) WHERE IdCompra = @IdCompra;
                    SELECT @IdCompra;");
                cmd.CommandText = sql.ToString();
                try
                {
                    int resultado = Convert.ToInt32(cmd.ExecuteScalar());
                    transaccion.Commit();
                    return resultado;
                }
                catch
                {
                    transaccion.Rollback();
                    throw;
                }
            }
        }

        public bool EliminarCompra()
        {
            using (SqlConnection conexion = Conexion.Conectar())
            {
                SqlTransaction transaccion =
                    conexion.BeginTransaction(IsolationLevel.Serializable);

                try
                {
                    //DEVUELVE EL STOCK

                    string actualizarStock = @"
                IF NOT EXISTS (SELECT 1 FROM Compras WITH (UPDLOCK, HOLDLOCK) WHERE IdCompra = @IdCompra)
                    THROW 50001, 'La compra ya no existe.', 1;
                IF EXISTS (SELECT 1 FROM Material m WITH (UPDLOCK, HOLDLOCK)
                    JOIN (SELECT IdMaterial, SUM(Cantidad) AS Cantidad
                        FROM DetalleCompraMaterial WITH (UPDLOCK, HOLDLOCK)
                        WHERE IdCompra = @IdCompra GROUP BY IdMaterial) d ON m.IdMaterial = d.IdMaterial
                    WHERE m.Stock < d.Cantidad)
                    THROW 50002, 'No hay stock suficiente para revertir la compra: parte del material ya fue utilizado.', 1;
                UPDATE m
                SET m.Stock = m.Stock - d.Cantidad
                FROM Material m
                INNER JOIN (SELECT IdMaterial, SUM(Cantidad) AS Cantidad
                    FROM DetalleCompraMaterial WITH (UPDLOCK, HOLDLOCK)
                    WHERE IdCompra = @IdCompra GROUP BY IdMaterial) d
                    ON m.IdMaterial = d.IdMaterial;";

                    using (SqlCommand cmd = new SqlCommand(actualizarStock, conexion, transaccion))
                    {
                        cmd.Parameters.AddWithValue("@IdCompra", IdCompra1);

                        cmd.ExecuteNonQuery();
                    }


                    // ELIMINAR LOS DETALLES DE COMPRAS


                    string cmdDetalle = @"DELETE FROM DetalleCompraMaterial WHERE IdCompra = @IdCompra;";

                    using (SqlCommand cmd = new SqlCommand(cmdDetalle, conexion, transaccion))
                    {
                        cmd.Parameters.AddWithValue("@IdCompra", IdCompra1);

                        cmd.ExecuteNonQuery();
                    }

                    // ELIMINA LA COMPRA DEL MATERIAL

                    string cmdCompra = @"DELETE FROM Compras  WHERE IdCompra = @IdCompra;";

                    using (SqlCommand cmd = new SqlCommand(cmdCompra, conexion, transaccion))
                    {
                        cmd.Parameters.AddWithValue("@IdCompra", IdCompra1);

                        int filas = cmd.ExecuteNonQuery();

                        if (filas > 0)
                        {
                            transaccion.Commit();
                            return true;
                        }
                        else
                        {
                            transaccion.Rollback();
                            return false;
                        }
                    }
                }
                catch (Exception ex)
                {
                    transaccion.Rollback();

                    MessageBox.Show("Ocurrió un error al eliminar la compra.\n\n" + ex.Message, "Error", MessageBoxButtons.OK,
                        MessageBoxIcon.Error);

                    return false;
                }
            }
        }

        //Metodo para buscar una compra ya registrada
        public static DataTable Buscar(string termino)
        {
            SqlConnection con = Conexion.Conectar();
            string comando = @"SELECT * FROM VerCompras WHERE CAST(IdCompra AS VARCHAR) LIKE @buscar OR Proveedor LIKE @buscar;";

            SqlDataAdapter ad = new SqlDataAdapter(comando, con);

            ad.SelectCommand.Parameters.AddWithValue("@buscar", "%" + termino + "%");

            DataTable dt = new DataTable();

            ad.Fill(dt);

            return dt;
        }


    }
}
