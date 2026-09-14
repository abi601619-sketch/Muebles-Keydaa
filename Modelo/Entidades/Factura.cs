using Modelo.Conexión_DB;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Modelo.Entidades
{
    public class DbFactura
    {
        private int IdFactura;
        private DateTime FechaEmision;
        private DateTime FechaVencimiento;
        private int IdVenta;
        private string Observaciones;

        public DbFactura(int idFactura, DateTime fechaEmisión, DateTime fechaVencimiento, int venta, string observaciones)
        {
            IdFactura = idFactura;
            FechaEmision = fechaEmisión;
            FechaVencimiento = fechaVencimiento;
            IdVenta = venta;
            Observaciones = observaciones;
        }

        public DbFactura()
        {

        }

        public int IdFactura1 { get => IdFactura; set => IdFactura = value; }
        public DateTime FechaEmisión1 { get => FechaEmision; set => FechaEmision = value; }
        public DateTime FechaVencimiento1 { get => FechaVencimiento; set => FechaVencimiento = value; }
        public int Venta1 { get => IdVenta; set => IdVenta = value; }
        public string Observaciones1 { get => Observaciones; set => Observaciones = value; }


        public static DataTable CargarRegistrosFacturas()
        {
            DataTable dt = new DataTable();

            try
            {
                using (SqlConnection conectar = Conexion.Conectar())
                {
                    string comando = "SELECT * FROM VerFacturas;";

                    using (SqlDataAdapter adapter = new SqlDataAdapter(comando, conectar))
                    {
                        adapter.Fill(dt);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar los registros de facturas: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return dt;
        }


        public void InsertarFactura()
        {
            string sql = @"INSERT INTO Factura(FechaEmision, FechaVencimiento, IdVenta, Observaciones)
                   VALUES (@FechaEmision, @FechaVencimiento, @IdVenta, @Observaciones)";

            try
            {
                using (SqlConnection cn = Conexion.Conectar())
                using (SqlCommand cmd = new SqlCommand(sql, cn))
                {
                    cmd.Parameters.AddWithValue("@FechaEmision", FechaEmision);
                    cmd.Parameters.AddWithValue("@FechaVencimiento", FechaVencimiento);
                    cmd.Parameters.AddWithValue("@IdVenta", IdVenta);
                    cmd.Parameters.AddWithValue("@Observaciones", Observaciones);

                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al insertar la factura: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        public static DataTable BuscarVentaParaFactura(int idVenta)
        {
            DataTable dt = new DataTable();

            try
            {
                using (SqlConnection conectar = Conexion.Conectar())
                {
                    string comando = @"SELECT * FROM VerVentasParaFactura WHERE [#] = @IdVenta;";

                    using (SqlDataAdapter adapter = new SqlDataAdapter(comando, conectar))
                    {
                        adapter.SelectCommand.Parameters.AddWithValue("@IdVenta", idVenta);

                        adapter.Fill(dt);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al buscar la venta para factura: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return dt;
        }


        // Método que manda la información de la venta para factura
        public static DataTable CargarDetalleVentaParaFactura(int idVenta)
        {
            DataTable dt = new DataTable();

            try
            {
                using (SqlConnection conectar = Conexion.Conectar())
                {
                    string comando = @"SELECT * FROM VerDetalleVenta WHERE IdVenta = @IdVenta;";

                    using (SqlDataAdapter adapter = new SqlDataAdapter(comando, conectar))
                    {
                        adapter.SelectCommand.Parameters.AddWithValue("@IdVenta", idVenta);

                        adapter.Fill(dt);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar el detalle de la venta: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return dt;
        }


        // Carga los datos al formulario de editar la factura
        public static DataTable CargarFacturaPorId(int idFactura)
        {
            DataTable dt = new DataTable();

            try
            {
                using (SqlConnection conectar = Conexion.Conectar())
                {
                    string comando = @"SELECT * FROM VerFacturaEditar WHERE IdFactura = @IdFactura";

                    using (SqlDataAdapter adapter = new SqlDataAdapter(comando, conectar))
                    {
                        adapter.SelectCommand.Parameters.AddWithValue("@IdFactura", idFactura);

                        adapter.Fill(dt);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar la factura: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return dt;
        }


        public static void ActualizarFactura(
            int idFactura,
            DateTime fechaVencimiento,
            decimal? descuento,
            string observaciones)
        {
            string sql = @"UPDATE Factura SET FechaVencimiento = @FechaVencimiento, Descuento = @Descuento, Observaciones = @Observaciones  WHERE IdFactura = @IdFactura";

            try
            {
                using (SqlConnection cn = Conexion.Conectar())
                using (SqlCommand cmd = new SqlCommand(sql, cn))
                {
                    cmd.Parameters.AddWithValue("@FechaVencimiento", fechaVencimiento);

                    cmd.Parameters.AddWithValue("@Descuento", (object)descuento ?? DBNull.Value);

                    cmd.Parameters.AddWithValue("@Observaciones", string.IsNullOrWhiteSpace(observaciones) ? (object)DBNull.Value : observaciones);

                    cmd.Parameters.AddWithValue("@IdFactura", idFactura);

                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Factura actualizada correctamente.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al actualizar la factura: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public static DataTable BuscarFacturas(string texto)
        {
            DataTable dt = new DataTable();

            using (SqlConnection conexion = Conexion.Conectar())
            {
                try
                {
                    string consulta = @"SELECT * FROM VerFacturas WHERE CAST(IdFactura AS VARCHAR) LIKE '%' + @Texto + '%'
                    OR Cliente LIKE '%' + @Texto + '%' OR [Método de Pago] LIKE '%' + @Texto + '%' ORDER BY IdFactura DESC";

                    using (SqlCommand comando = new SqlCommand(consulta, conexion))
                    {
                        comando.Parameters.AddWithValue("@Texto", texto);

                        using (SqlDataAdapter adaptador = new SqlDataAdapter(comando))
                        {
                            adaptador.Fill(dt);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            return dt;
        }

    }

}
