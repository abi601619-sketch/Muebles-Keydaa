using Modelo.Conexión_DB;
using Modelo.Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Windows.Forms;

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
        try
        {
            using (SqlConnection conectar = Conexion.Conectar())
            {
                string comando = "SELECT * FROM VerCompras;";
                SqlDataAdapter adapter = new SqlDataAdapter(comando, conectar);
                DataTable dt = new DataTable();

                adapter.Fill(dt);

                return dt;
            }
        }
        catch (SqlException ex)
        {
            switch (ex.Number)
            {
                case 53:
                    MessageBox.Show("No se puede conectar al servidor SQL.", "ERR-SQL-001", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;

                case 4060:
                    MessageBox.Show("No se puede acceder a la base de datos.", "ERR-SQL-002", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;

                case -2:
                    MessageBox.Show("Tiempo de espera agotado.", "ERR-SQL-003", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;

                case 208:
                    MessageBox.Show("Tabla, vista o procedimiento no encontrado.", "ERR-SQL-004", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;

                default:
                    MessageBox.Show("Error inesperado de SQL.", "ERR-SQL-999", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
            }

            return new DataTable();
        }
        catch (Exception)
        {
            MessageBox.Show("Error inesperado de SQL.", "ERR-SQL-999", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return new DataTable();
        }
    }

    public static DataTable ObtenerCompraPorId(int idCompra)
    {
        string comandoSQL = @"SELECT IdCompra, FechaCompra, TotalCompra, IdProveedor
                              FROM Compras
                              WHERE IdCompra = @IdCompra;";

        try
        {
            using (SqlConnection conexion = Conexion.Conectar())
            using (SqlCommand comando = new SqlCommand(comandoSQL, conexion))
            {
                comando.Parameters.AddWithValue("@IdCompra", idCompra);

                SqlDataAdapter adapter = new SqlDataAdapter(comando);
                DataTable dt = new DataTable();

                adapter.Fill(dt);

                return dt;
            }
        }
        catch (SqlException ex)
        {
            switch (ex.Number)
            {
                case 53:
                    MessageBox.Show("No se puede conectar al servidor SQL.", "ERR-SQL-001", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;

                case 4060:
                    MessageBox.Show("No se puede acceder a la base de datos.", "ERR-SQL-002", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;

                case -2:
                    MessageBox.Show("Tiempo de espera agotado.", "ERR-SQL-003", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;

                case 208:
                    MessageBox.Show("Tabla, vista o procedimiento no encontrado.", "ERR-SQL-004", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;

                default:
                    MessageBox.Show("Error inesperado de SQL.", "ERR-SQL-999", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
            }

            return new DataTable();
        }
        catch (Exception)
        {
            MessageBox.Show("Error inesperado de SQL.", "ERR-SQL-999", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return new DataTable();
        }
    }

    public static int GuardarCompleta(int idCompra, DateTime fecha, int idProveedor, IList<DetalleCompraMaterial> detalles)
    {
        if (detalles == null || detalles.Count == 0)
        {
            MessageBox.Show("Agrega al menos un material.", "ERR-VAL-001", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return 0;
        }

        try
        {
            using (SqlConnection conexion = Conexion.Conectar())
            using (SqlTransaction transaccion = conexion.BeginTransaction(IsolationLevel.Serializable))
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = conexion;
                cmd.Transaction = transaccion;

                cmd.Parameters.AddWithValue("@IdCompra", idCompra);
                cmd.Parameters.AddWithValue("@Fecha", fecha);
                cmd.Parameters.AddWithValue("@Proveedor", idProveedor);

                StringBuilder sql = new StringBuilder(
                    @"DECLARE @Detalles TABLE
                      (
                          Id int,
                          Material int,
                          Cantidad int,
                          Precio decimal(10,2)
                      );");

                for (int i = 0; i < detalles.Count; i++)
                {
                    DetalleCompraMaterial d = detalles[i];

                    if (d.Cantidad1 <= 0 || d.PrecioUnitario1 < 0)
                    {
                        MessageBox.Show("Revisa la cantidad y el precio de los materiales.", "ERR-VAL-002", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                        transaccion.Rollback();

                        return 0;
                    }

                    sql.AppendFormat(
                        "INSERT INTO @Detalles VALUES (@d{0}, @m{0}, @c{0}, @p{0});",
                        i);

                    cmd.Parameters.AddWithValue("@d" + i, d.IdDetalleCompraMaterial1);
                    cmd.Parameters.AddWithValue("@m" + i, d.IdMaterial1);
                    cmd.Parameters.AddWithValue("@c" + i, d.Cantidad1);

                    SqlParameter precio = cmd.Parameters.Add("@p" + i, SqlDbType.Decimal);
                    precio.Precision = 10;
                    precio.Scale = 2;
                    precio.Value = d.PrecioUnitario1;
                }

                sql.Append(@"
                    IF @IdCompra = 0
                    BEGIN
                        INSERT INTO Compras
                        (
                            FechaCompra,
                            TotalCompra,
                            IdProveedor
                        )
                        VALUES
                        (
                            @Fecha,
                            0,
                            @Proveedor
                        );

                        SET @IdCompra = CONVERT(int, SCOPE_IDENTITY());
                    END
                    ELSE IF NOT EXISTS
                    (
                        SELECT 1
                        FROM Compras WITH (UPDLOCK, HOLDLOCK)
                        WHERE IdCompra = @IdCompra
                    )
                        THROW 50002, 'La compra ya no existe. Vuelva a cargar la lista.', 1;

                    IF EXISTS
                    (
                        SELECT 1
                        FROM @Detalles d
                        WHERE d.Id <> 0
                        AND NOT EXISTS
                        (
                            SELECT 1
                            FROM DetalleCompraMaterial o
                            WHERE o.IdCompra = @IdCompra
                            AND o.IdDetalleCompraMaterial = d.Id
                        )
                    )
                        THROW 50003, 'Los detalles cambiaron. Vuelva a cargar la compra.', 1;

                    DECLARE @Cambios TABLE
                    (
                        Material int PRIMARY KEY,
                        Cantidad int
                    );

                    INSERT INTO @Cambios
                    SELECT Material, SUM(Cantidad)
                    FROM
                    (
                        SELECT Material, Cantidad
                        FROM @Detalles

                        UNION ALL

                        SELECT IdMaterial, -Cantidad
                        FROM DetalleCompraMaterial WITH (UPDLOCK, HOLDLOCK)
                        WHERE IdCompra = @IdCompra
                    ) movimientos
                    GROUP BY Material;

                    IF EXISTS
                    (
                        SELECT 1
                        FROM @Cambios c
                        LEFT JOIN Material m WITH (UPDLOCK, HOLDLOCK)
                            ON m.IdMaterial = c.Material
                        WHERE m.IdMaterial IS NULL
                           OR m.Stock + c.Cantidad < 0
                    )
                        THROW 50004, 'No hay stock suficiente para revertir la compra: parte del material ya fue utilizado.', 1;

                    UPDATE m
                    SET Stock = Stock + c.Cantidad
                    FROM Material m
                    JOIN @Cambios c
                        ON m.IdMaterial = c.Material;

                    DELETE FROM DetalleCompraMaterial
                    WHERE IdCompra = @IdCompra
                    AND IdDetalleCompraMaterial NOT IN
                    (
                        SELECT Id
                        FROM @Detalles
                    );

                    UPDATE o
                    SET IdMaterial = d.Material,
                        Cantidad = d.Cantidad,
                        PrecioUnitario = d.Precio
                    FROM DetalleCompraMaterial o
                    JOIN @Detalles d
                        ON o.IdDetalleCompraMaterial = d.Id
                    WHERE o.IdCompra = @IdCompra;

                    INSERT INTO DetalleCompraMaterial
                    (
                        IdCompra,
                        IdMaterial,
                        Cantidad,
                        PrecioUnitario
                    )
                    SELECT @IdCompra, Material, Cantidad, Precio
                    FROM @Detalles
                    WHERE Id = 0;

                    UPDATE Compras
                    SET FechaCompra = @Fecha,
                        IdProveedor = @Proveedor,
                        TotalCompra =
                        (
                            SELECT SUM(Cantidad * Precio)
                            FROM @Detalles
                        )
                    WHERE IdCompra = @IdCompra;

                    SELECT @IdCompra;");

                cmd.CommandText = sql.ToString();

                try
                {
                    int resultado = Convert.ToInt32(cmd.ExecuteScalar());

                    transaccion.Commit();

                    return resultado;
                }
                catch (SqlException ex)
                {
                    try
                    {
                        transaccion.Rollback();
                    }
                    catch
                    {
                    }

                    switch (ex.Number)
                    {
                        case 50002:
                            MessageBox.Show("La compra ya no existe. Vuelve a cargar la lista.", "ERR-SQL-999", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;

                        case 50003:
                            MessageBox.Show("Los detalles cambiaron. Vuelve a cargar la compra.", "ERR-SQL-999", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;

                        case 50004:
                            MessageBox.Show("No hay stock suficiente para revertir la compra.", "ERR-SQL-999", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;

                        case 53:
                            MessageBox.Show("No se puede conectar al servidor SQL.", "ERR-SQL-001", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;

                        case 4060:
                            MessageBox.Show("No se puede acceder a la base de datos.", "ERR-SQL-002", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;

                        case -2:
                            MessageBox.Show("Tiempo de espera agotado.", "ERR-SQL-003", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;

                        case 208:
                            MessageBox.Show("Tabla, vista o procedimiento no encontrado.", "ERR-SQL-004", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;

                        case 2627:
                            MessageBox.Show("Registro duplicado por clave primaria/UNIQUE.", "ERR-SQL-005", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;

                        case 2601:
                            MessageBox.Show("Registro duplicado por índice UNIQUE.", "ERR-SQL-006", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;

                        case 547:
                            MessageBox.Show("Violación de FK/CHECK.", "ERR-SQL-007", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;

                        case 515:
                            MessageBox.Show("Campo NOT NULL sin valor.", "ERR-SQL-008", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;

                        case 245:
                            MessageBox.Show("Conversión o formato de datos incorrecto.", "ERR-SQL-009", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;

                        case 8115:
                            MessageBox.Show("Desbordamiento numérico.", "ERR-SQL-010", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;

                        case 8152:
                            MessageBox.Show("Datos demasiado largos para la columna.", "ERR-SQL-011", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;

                        default:
                            MessageBox.Show("Error inesperado de SQL.", "ERR-SQL-999", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                    }

                    return 0;
                }
                catch (Exception)
                {
                    try
                    {
                        transaccion.Rollback();
                    }
                    catch
                    {
                    }

                    MessageBox.Show("Error inesperado de SQL.", "ERR-SQL-999", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    return 0;
                }
            }
        }
        catch (SqlException ex)
        {
            switch (ex.Number)
            {
                case 53:
                    MessageBox.Show("No se puede conectar al servidor SQL.", "ERR-SQL-001", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;

                case 4060:
                    MessageBox.Show("No se puede acceder a la base de datos.", "ERR-SQL-002", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;

                case -2:
                    MessageBox.Show("Tiempo de espera agotado.", "ERR-SQL-003", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;

                case 208:
                    MessageBox.Show("Tabla, vista o procedimiento no encontrado.", "ERR-SQL-004", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;

                case 2627:
                    MessageBox.Show("Registro duplicado por clave primaria/UNIQUE.", "ERR-SQL-005", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;

                case 2601:
                    MessageBox.Show("Registro duplicado por índice UNIQUE.", "ERR-SQL-006", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;

                case 547:
                    MessageBox.Show("Violación de FK/CHECK.", "ERR-SQL-007", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;

                case 515:
                    MessageBox.Show("Campo NOT NULL sin valor.", "ERR-SQL-008", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;

                case 245:
                    MessageBox.Show("Conversión o formato de datos incorrecto.", "ERR-SQL-009", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;

                case 8115:
                    MessageBox.Show("Desbordamiento numérico.", "ERR-SQL-010", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;

                case 8152:
                    MessageBox.Show("Datos demasiado largos para la columna.", "ERR-SQL-011", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;

                default:
                    MessageBox.Show("Error inesperado de SQL.", "ERR-SQL-999", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
            }

            return 0;
        }
        catch (Exception)
        {
            MessageBox.Show("Error inesperado de SQL.", "ERR-SQL-999", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return 0;
        }
    }

    public bool EliminarCompra()
    {
        try
        {
            using (SqlConnection conexion = Conexion.Conectar())
            using (SqlTransaction transaccion = conexion.BeginTransaction(IsolationLevel.Serializable))
            {
                try
                {
                    string actualizarStock = @"
                        IF NOT EXISTS
                        (
                            SELECT 1
                            FROM Compras WITH (UPDLOCK, HOLDLOCK)
                            WHERE IdCompra = @IdCompra
                        )
                            THROW 50001, 'La compra ya no existe.', 1;

                        IF EXISTS
                        (
                            SELECT 1
                            FROM Material m WITH (UPDLOCK, HOLDLOCK)
                            JOIN
                            (
                                SELECT IdMaterial, SUM(Cantidad) AS Cantidad
                                FROM DetalleCompraMaterial WITH (UPDLOCK, HOLDLOCK)
                                WHERE IdCompra = @IdCompra
                                GROUP BY IdMaterial
                            ) d
                                ON m.IdMaterial = d.IdMaterial
                            WHERE m.Stock < d.Cantidad
                        )
                            THROW 50002, 'No hay stock suficiente para revertir la compra: parte del material ya fue utilizado.', 1;

                        UPDATE m
                        SET m.Stock = m.Stock - d.Cantidad
                        FROM Material m
                        INNER JOIN
                        (
                            SELECT IdMaterial, SUM(Cantidad) AS Cantidad
                            FROM DetalleCompraMaterial WITH (UPDLOCK, HOLDLOCK)
                            WHERE IdCompra = @IdCompra
                            GROUP BY IdMaterial
                        ) d
                            ON m.IdMaterial = d.IdMaterial;";

                    using (SqlCommand cmd = new SqlCommand(actualizarStock, conexion, transaccion))
                    {
                        cmd.Parameters.AddWithValue("@IdCompra", IdCompra1);
                        cmd.ExecuteNonQuery();
                    }

                    string cmdDetalle = @"DELETE FROM DetalleCompraMaterial WHERE IdCompra = @IdCompra;";

                    using (SqlCommand cmd = new SqlCommand(cmdDetalle, conexion, transaccion))
                    {
                        cmd.Parameters.AddWithValue("@IdCompra", IdCompra1);
                        cmd.ExecuteNonQuery();
                    }

                    string cmdCompra = @"DELETE FROM Compras WHERE IdCompra = @IdCompra;";

                    using (SqlCommand cmd = new SqlCommand(cmdCompra, conexion, transaccion))
                    {
                        cmd.Parameters.AddWithValue("@IdCompra", IdCompra1);

                        int filas = cmd.ExecuteNonQuery();

                        if (filas > 0)
                        {
                            transaccion.Commit();
                            return true;
                        }

                        transaccion.Rollback();

                        return false;
                    }
                }
                catch (SqlException ex)
                {
                    try
                    {
                        transaccion.Rollback();
                    }
                    catch
                    {
                    }

                    switch (ex.Number)
                    {
                        case 50001:
                            MessageBox.Show("La compra ya no existe.", "ERR-SQL-999", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;

                        case 50002:
                            MessageBox.Show("No hay stock suficiente para revertir la compra.", "ERR-SQL-999", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;

                        case 53:
                            MessageBox.Show("No se puede conectar al servidor SQL.", "ERR-SQL-001", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;

                        case 4060:
                            MessageBox.Show("No se puede acceder a la base de datos.", "ERR-SQL-002", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;

                        case -2:
                            MessageBox.Show("Tiempo de espera agotado.", "ERR-SQL-003", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;

                        case 208:
                            MessageBox.Show("Tabla, vista o procedimiento no encontrado.", "ERR-SQL-004", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;

                        case 2627:
                            MessageBox.Show("Registro duplicado por clave primaria/UNIQUE.", "ERR-SQL-005", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;

                        case 2601:
                            MessageBox.Show("Registro duplicado por índice UNIQUE.", "ERR-SQL-006", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;

                        case 547:
                            MessageBox.Show("Violación de FK/CHECK.", "ERR-SQL-007", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;

                        case 515:
                            MessageBox.Show("Campo NOT NULL sin valor.", "ERR-SQL-008", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;

                        case 245:
                            MessageBox.Show("Conversión o formato de datos incorrecto.", "ERR-SQL-009", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;

                        case 8115:
                            MessageBox.Show("Desbordamiento numérico.", "ERR-SQL-010", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;

                        case 8152:
                            MessageBox.Show("Datos demasiado largos para la columna.", "ERR-SQL-011", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;

                        default:
                            MessageBox.Show("Error inesperado de SQL.", "ERR-SQL-999", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                    }

                    return false;
                }
                catch (Exception)
                {
                    try
                    {
                        transaccion.Rollback();
                    }
                    catch
                    {
                    }

                    MessageBox.Show("Error inesperado de SQL.", "ERR-SQL-999", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    return false;
                }
            }
        }
        catch (SqlException ex)
        {
            switch (ex.Number)
            {
                case 53:
                    MessageBox.Show("No se puede conectar al servidor SQL.", "ERR-SQL-001", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;

                case 4060:
                    MessageBox.Show("No se puede acceder a la base de datos.", "ERR-SQL-002", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;

                case -2:
                    MessageBox.Show("Tiempo de espera agotado.", "ERR-SQL-003", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;

                case 208:
                    MessageBox.Show("Tabla, vista o procedimiento no encontrado.", "ERR-SQL-004", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;

                case 2627:
                    MessageBox.Show("Registro duplicado por clave primaria/UNIQUE.", "ERR-SQL-005", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;

                case 2601:
                    MessageBox.Show("Registro duplicado por índice UNIQUE.", "ERR-SQL-006", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;

                case 547:
                    MessageBox.Show("Violación de FK/CHECK.", "ERR-SQL-007", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;

                case 515:
                    MessageBox.Show("Campo NOT NULL sin valor.", "ERR-SQL-008", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;

                case 245:
                    MessageBox.Show("Conversión o formato de datos incorrecto.", "ERR-SQL-009", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;

                case 8115:
                    MessageBox.Show("Desbordamiento numérico.", "ERR-SQL-010", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;

                case 8152:
                    MessageBox.Show("Datos demasiado largos para la columna.", "ERR-SQL-011", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;

                default:
                    MessageBox.Show("Error inesperado de SQL.", "ERR-SQL-999", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
            }

            return false;
        }
        catch (Exception)
        {
            MessageBox.Show("Error inesperado de SQL.", "ERR-SQL-999", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return false;
        }
    }

    public static DataTable Buscar(string termino)
    {
        string comando = @"SELECT *
                           FROM VerCompras
                           WHERE CAST(IdCompra AS VARCHAR) LIKE @buscar
                              OR Proveedor LIKE @buscar;";

        try
        {
            using (SqlConnection con = Conexion.Conectar())
            using (SqlDataAdapter ad = new SqlDataAdapter(comando, con))
            {
                ad.SelectCommand.Parameters.AddWithValue("@buscar", "%" + (termino ?? "") + "%");

                DataTable dt = new DataTable();

                ad.Fill(dt);

                return dt;
            }
        }
        catch (SqlException ex)
        {
            switch (ex.Number)
            {
                case 53:
                    MessageBox.Show("No se puede conectar al servidor SQL.", "ERR-SQL-001", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;

                case 4060:
                    MessageBox.Show("No se puede acceder a la base de datos.", "ERR-SQL-002", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;

                case -2:
                    MessageBox.Show("Tiempo de espera agotado.", "ERR-SQL-003", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;

                case 208:
                    MessageBox.Show("Tabla, vista o procedimiento no encontrado.", "ERR-SQL-004", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;

                default:
                    MessageBox.Show("Error inesperado de SQL.", "ERR-SQL-999", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
            }

            return new DataTable();
        }
        catch (Exception)
        {
            MessageBox.Show("Error inesperado de SQL.", "ERR-SQL-999", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return new DataTable();
        }
    }
}