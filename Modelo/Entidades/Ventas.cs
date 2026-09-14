using Modelo.Conexión_DB;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Modelo.Entidades
{
    public class DbVentas
    {
        private int IdVenta;
        private DateTime FechaVenta;
        private int Cliente;
        private int MetodoPago;
        private decimal SubTotal;




        public DbVentas()
        {

        }

        public DbVentas(int idVenta, DateTime fechaVenta, int cliente, int metodoPago, decimal subTotal)
        {
            IdVenta = idVenta;
            FechaVenta = fechaVenta;
            Cliente = cliente;
            MetodoPago = metodoPago;
            SubTotal = subTotal;
        }

        public int IdVenta1 { get => IdVenta; set => IdVenta = value; }
        public DateTime FechaVenta1 { get => FechaVenta; set => FechaVenta = value; }
        public int Cliente1 { get => Cliente; set => Cliente = value; }
        public int MetodoPago1 { get => MetodoPago; set => MetodoPago = value; }
        public decimal SubTotal1 { get => SubTotal; set => SubTotal = value; }

        public static DataTable CargarVentas()
        {
            SqlConnection conectar = Conexion.Conectar();
            string comando = "SELECT *FROM VerVentas;";
            SqlDataAdapter adapter = new SqlDataAdapter(comando, conectar);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            return dt;
        }


        public int InsertarVenta()
        {
            string comandoSQL = @"INSERT INTO Venta (FechaVenta, IdCliente, IdMetodoPago, SubTotal)
        VALUES(@FechaVenta, @IdCliente, @IdMetodoPago, @SubTotal );
        SELECT CAST(SCOPE_IDENTITY() AS INT);";

            using (SqlConnection conexion = Conexion.Conectar())
            {
                using (SqlCommand comandoObjeto =
                    new SqlCommand(comandoSQL, conexion))
                {
                    comandoObjeto.Parameters.AddWithValue("@FechaVenta", FechaVenta);

                    comandoObjeto.Parameters.AddWithValue("@IdCliente", Cliente);

                    comandoObjeto.Parameters.AddWithValue("@IdMetodoPago", MetodoPago);

                    SqlParameter parametro = comandoObjeto.Parameters.Add("@SubTotal", SqlDbType.Decimal);

                    parametro.Precision = 10;
                    parametro.Scale = 2;
                    parametro.Value = SubTotal1;

                    try
                    {
                        int idVenta =
                            Convert.ToInt32(comandoObjeto.ExecuteScalar());

                        return idVenta;
                    }
                    catch (SqlException ex)
                    {
                        MessageBox.Show(
                            "Ocurrió un error al guardar la venta.\n\n"
                            + ex.Message,
                            "Error " + ex.Number,
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error
                        );

                        return 0;
                    }
                }
            }
        }

        public bool ActualizarVenta()
        {
            string comandoSQL = @"UPDATE Venta 
                          SET FechaVenta = @FechaVenta,
                              IdMetodoPago = @IdMetodoPago,
                              SubTotal = @SubTotal
                          WHERE IdVenta = @IdVenta";

            using (SqlConnection conexion = Conexion.Conectar())
            {
                using (SqlCommand comandoObjeto = new SqlCommand(comandoSQL, conexion))
                {
                    comandoObjeto.Parameters.AddWithValue("@FechaVenta", FechaVenta1);
                    comandoObjeto.Parameters.AddWithValue("@IdMetodoPago", MetodoPago1);
                    comandoObjeto.Parameters.AddWithValue("@SubTotal", SubTotal1);
                    comandoObjeto.Parameters.AddWithValue("@IdVenta", IdVenta1);

                    try
                    {
                        return comandoObjeto.ExecuteNonQuery() > 0;
                    }
                    catch (SqlException)
                    {
                        return false;
                    }
                }
            }
        }

        public DataTable ObtenerDetalleVenta(int idDetalleVenta)
        {
            DataTable dt = new DataTable();

            try
            {
                using (SqlConnection cn = Conexion.Conectar())
                {
                    string query = @"SELECT IdDetalleVenta, IdVenta, ProductoVendido, Cantidad, PrecioUnitario FROM DetalleVenta
    WHERE IdDetalleVenta = @IdDetalleVenta";

                    SqlCommand cmd = new SqlCommand(query, cn);

                    cmd.Parameters.AddWithValue("@IdDetalleVenta", idDetalleVenta);



                    SqlDataAdapter da = new SqlDataAdapter(cmd);

                    da.Fill(dt);

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Error al cargar el detalle de la venta: " + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }

            return dt;
        }

        public bool EliminarVenta()
        {
            using (SqlConnection conexion = Conexion.Conectar())
            {
                SqlTransaction transaccion = conexion.BeginTransaction();
                try
                {
                    // Eliminar factura si existe
                    string cmdFactura = "DELETE FROM Factura WHERE IdVenta = @IdVenta;";
                    using (SqlCommand cmd = new SqlCommand(cmdFactura, conexion, transaccion))
                    {
                        cmd.Parameters.AddWithValue("@IdVenta", IdVenta);
                        cmd.ExecuteNonQuery();
                    }

                    // Eliminar detalle de venta
                    string cmdDetalle = "DELETE FROM DetalleVenta WHERE IdVenta = @IdVenta;";
                    using (SqlCommand cmd = new SqlCommand(cmdDetalle, conexion, transaccion))
                    {
                        cmd.Parameters.AddWithValue("@IdVenta", IdVenta);
                        cmd.ExecuteNonQuery();
                    }

                    // Eliminar venta
                    string cmdVenta = "DELETE FROM Venta WHERE IdVenta = @IdVenta;";
                    using (SqlCommand cmd = new SqlCommand(cmdVenta, conexion, transaccion))
                    {
                        cmd.Parameters.AddWithValue("@IdVenta", IdVenta);
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
                catch (Exception)
                {
                    transaccion.Rollback();
                    return false;
                }
            }
        }

        public static DataTable BuscarVenta(string termino)
        {
            SqlConnection con = Conexion.Conectar();

            string comando = @"SELECT * FROM VerVentas WHERE CAST(IdVenta AS VARCHAR) LIKE @buscar  OR Cliente LIKE @buscar;";

            SqlDataAdapter ad = new SqlDataAdapter(comando, con);

            ad.SelectCommand.Parameters.AddWithValue("@buscar", "%" + termino + "%");

            DataTable dt = new DataTable();

            ad.Fill(dt);

            return dt;
        }
    }
}
