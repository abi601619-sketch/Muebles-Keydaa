using Modelo.Conexión_DB;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Modelo.Entidades
{
    public class ReportesVentas
    {
        // =========================================================
        // PROPIEDADES DEL REPORTE
        // =========================================================

        private int IdVenta;
        private int? N_Factura;
        private string Nombre_De_Cliente;
        private DateTime FechaVenta;
        private double Subtotal;
        private double TotalAPagar;


        public ReportesVentas()
        {
        }


        public ReportesVentas(
            int idVenta,
            int? n_Factura,
            string nombre_De_Cliente,
            DateTime fechaVenta,
            double subtotal,
            double totalAPagar)
        {
            IdVenta = idVenta;
            N_Factura = n_Factura;
            Nombre_De_Cliente = nombre_De_Cliente;
            FechaVenta = fechaVenta;
            Subtotal = subtotal;
            TotalAPagar = totalAPagar;
        }


        public int IdVenta1
        {
            get => IdVenta;
            set => IdVenta = value;
        }


        public int? N_Factura1
        {
            get => N_Factura;
            set => N_Factura = value;
        }


        public string Nombre_De_Cliente1
        {
            get => Nombre_De_Cliente;
            set => Nombre_De_Cliente = value;
        }


        public DateTime FechaVenta1
        {
            get => FechaVenta;
            set => FechaVenta = value;
        }


        public double Subtotal1
        {
            get => Subtotal;
            set => Subtotal = value;
        }


        public double TotalAPagar1
        {
            get => TotalAPagar;
            set => TotalAPagar = value;
        }


        // =========================================================
        // CARGAR TODAS LAS VENTAS
        // =========================================================

        public static DataTable CargarReporteVentas()
        {
            DataTable dt = new DataTable();

            try
            {
                using (SqlConnection conectar = Conexion.Conectar())
                {
                    string comando = @"
                        SELECT *
                        FROM VerReporteVentas;";

                    using (SqlDataAdapter adapter =
                        new SqlDataAdapter(comando, conectar))
                    {
                        adapter.Fill(dt);
                    }
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show(
                    "Error SQL " + ex.Number + ": " + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Error inesperado: " + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }

            return dt;
        }


        // =========================================================
        // OBTENER VENTAS POR RANGO DE FECHAS
        // =========================================================

        public static DataTable ObtenerVentasPorFecha(
            DateTime fechaInicio,
            DateTime fechaFin)
        {
            DataTable tabla = new DataTable();

            try
            {
                string consulta = @"
                    SELECT *
                    FROM VerReporteVentas
                    WHERE FechaVenta >= @FechaInicio
                      AND FechaVenta < @FechaFin;";

                using (SqlConnection conexion = Conexion.Conectar())
                {
                    using (SqlDataAdapter adaptador =
                        new SqlDataAdapter(consulta, conexion))
                    {
                        adaptador.SelectCommand.Parameters.Add(
                            "@FechaInicio",
                            SqlDbType.DateTime
                        ).Value = fechaInicio.Date;

                        adaptador.SelectCommand.Parameters.Add(
                            "@FechaFin",
                            SqlDbType.DateTime
                        ).Value = fechaFin.Date.AddDays(1);

                        adaptador.Fill(tabla);
                    }
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show(
                    "Error SQL " + ex.Number + ": " + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Error inesperado: " + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }

            return tabla;
        }


        // =========================================================
        // OBTENER ESTADÍSTICAS DEL REPORTE
        // =========================================================

        public static DataTable ObtenerEstadisticasVentas(
            DateTime fechaInicio,
            DateTime fechaFin)
        {
            DataTable tabla = new DataTable();

            try
            {
                string consulta = @"
                    SELECT
                        COUNT(*) AS FacturasEmitidas,

                        ISNULL(SUM(TotalAPagar), 0)
                            AS TotalVentas,

                        ISNULL(MAX(TotalAPagar), 0)
                            AS VentaMasAlta

                    FROM VerReporteVentas

                    WHERE FechaVenta >= @FechaInicio
                      AND FechaVenta < @FechaFin;";

                using (SqlConnection conexion = Conexion.Conectar())
                {
                    using (SqlDataAdapter adaptador =
                        new SqlDataAdapter(consulta, conexion))
                    {
                        adaptador.SelectCommand.Parameters.Add(
                            "@FechaInicio",
                            SqlDbType.DateTime
                        ).Value = fechaInicio.Date;

                        adaptador.SelectCommand.Parameters.Add(
                            "@FechaFin",
                            SqlDbType.DateTime
                        ).Value = fechaFin.Date.AddDays(1);

                        adaptador.Fill(tabla);
                    }
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show(
                    "Error SQL " + ex.Number + ": " + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Error inesperado: " + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }

            return tabla;
        }

        public static int ContarFacturasEmitidas()
        {
            int total = 0;

            try
            {
                using (SqlConnection conexion = Conexion.Conectar())
                {
                    string comandoSQL =
                        "SELECT COUNT(*) FROM Factura;";

                    using (SqlCommand comandoObjeto =
                        new SqlCommand(comandoSQL, conexion))
                    {
                        total = Convert.ToInt32(
                            comandoObjeto.ExecuteScalar());
                    }
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show(
                    "Error SQL " + ex.Number + ": " + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Error inesperado: " + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }

            return total;
        }

        public static int ContarVentasTotales()
        {
            int total = 0;

            try
            {
                using (SqlConnection conexion = Conexion.Conectar())
                {
                    string comandoSQL =
                        "SELECT COUNT(*) FROM Venta;";

                    using (SqlCommand comandoObjeto =
                        new SqlCommand(comandoSQL, conexion))
                    {
                        total = Convert.ToInt32(
                            comandoObjeto.ExecuteScalar());
                    }
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show(
                    "Error SQL " + ex.Number + ": " + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Error inesperado: " + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }

            return total;
        }
    }
}