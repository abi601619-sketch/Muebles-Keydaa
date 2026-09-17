using Modelo.Conexión_DB;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Modelo.Entidades
{
    public class ReportesVentas
    {
        private int IdVenta;
        private int N_Factura;
        private string Nombre_De_Cliente;
        private DateTime FechaVenta;
        private string ProductosVendidos;
        private string MetodoPago;
        private double Subtotal;
        private double TotalAPagar;

        public ReportesVentas(
            int idVenta,
            int n_Factura,
            string nombre_De_Cliente,
            DateTime fechaVenta,
            string productosVendidos,
            string metodoPago,
            double subtotal,
            double totalAPagar)
        {
            IdVenta = idVenta;
            N_Factura = n_Factura;
            Nombre_De_Cliente = nombre_De_Cliente;
            FechaVenta = fechaVenta;
            ProductosVendidos = productosVendidos;
            MetodoPago = metodoPago;
            Subtotal = subtotal;
            TotalAPagar = totalAPagar;
        }

        public ReportesVentas()
        {
        }

        public int IdVenta1
        {
            get => IdVenta;
            set => IdVenta = value;
        }

        public int N_Factura1
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

        public string ProductosVendidos1
        {
            get => ProductosVendidos;
            set => ProductosVendidos = value;
        }

        public string MetodoPago1
        {
            get => MetodoPago;
            set => MetodoPago = value;
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


        public static DataTable CargarReporteVentas()
        {
            DataTable dt = new DataTable();

            try
            {
                using (SqlConnection conectar = Conexion.Conectar())
                {
                    string comando = @"
                    SELECT *
                    FROM ReporteDetalleVentas;";

                    using (SqlDataAdapter adapter =
                           new SqlDataAdapter(comando, conectar))
                    {
                        adapter.Fill(dt);
                    }
                }
            }
            catch (SqlException ex)
            {
                switch (ex.Number)
                {
                    case 208:
                        MessageBox.Show(
                            "Error 208: La vista o tabla del reporte no existe.",
                            "Error",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                        break;

                    case 53:
                        MessageBox.Show(
                            "Error 53: No se pudo conectar con el servidor.",
                            "Error",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                        break;

                    case 4060:
                        MessageBox.Show(
                            "Error 4060: No se pudo acceder a la base de datos.",
                            "Error",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                        break;

                    case -2:
                        MessageBox.Show(
                            "Error -2: La operación tardó demasiado.",
                            "Error",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning);
                        break;

                    default:
                        MessageBox.Show(
                            "Error SQL " + ex.Number + ": " + ex.Message,
                            "Error",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                        break;
                }
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
                switch (ex.Number)
                {
                    case 208:
                        MessageBox.Show(
                            "Error 208: La tabla Venta no existe.",
                            "Error",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                        break;

                    case 53:
                        MessageBox.Show(
                            "Error 53: No se pudo conectar con el servidor.",
                            "Error",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                        break;

                    case 4060:
                        MessageBox.Show(
                            "Error 4060: No se pudo acceder a la base de datos.",
                            "Error",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                        break;

                    case -2:
                        MessageBox.Show(
                            "Error -2: La operación tardó demasiado.",
                            "Error",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning);
                        break;

                    default:
                        MessageBox.Show(
                            "Error SQL " + ex.Number + ": " + ex.Message,
                            "Error",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                        break;
                }
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
                switch (ex.Number)
                {
                    case 208:
                        MessageBox.Show(
                            "Error 208: La tabla Factura no existe.",
                            "Error",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                        break;

                    case 53:
                        MessageBox.Show(
                            "Error 53: No se pudo conectar con el servidor.",
                            "Error",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                        break;

                    case 4060:
                        MessageBox.Show(
                            "Error 4060: No se pudo acceder a la base de datos.",
                            "Error",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                        break;

                    case -2:
                        MessageBox.Show(
                            "Error -2: La operación tardó demasiado.",
                            "Error",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning);
                        break;

                    default:
                        MessageBox.Show(
                            "Error SQL " + ex.Number + ": " + ex.Message,
                            "Error",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                        break;
                }
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
                switch (ex.Number)
                {
                    case 208:
                        MessageBox.Show(
                            "Error 208: La vista VerReporteVentas no existe.",
                            "Error",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                        break;

                    case 53:
                        MessageBox.Show(
                            "Error 53: No se pudo conectar con el servidor.",
                            "Error",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                        break;

                    case 4060:
                        MessageBox.Show(
                            "Error 4060: No se pudo acceder a la base de datos.",
                            "Error",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                        break;

                    case -2:
                        MessageBox.Show(
                            "Error -2: La consulta tardó demasiado.",
                            "Error",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning);
                        break;

                    case 245:
                        MessageBox.Show(
                            "Error 245: Existe un valor con formato incorrecto.",
                            "Error",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning);
                        break;

                    default:
                        MessageBox.Show(
                            "Error SQL " + ex.Number + ": " + ex.Message,
                            "Error",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                        break;
                }
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


        public static DataTable ObtenerEstadisticasVentas(
            DateTime fechaInicio,
            DateTime fechaFin)
        {
            DataTable tabla = new DataTable();

            try
            {
                string consulta = @"
                    SELECT
                        COUNT(DISTINCT [N° FACTURA]) AS FacturasEmitidas,
                        ISNULL(SUM(TotalAPagar), 0) AS TotalVentas,
                        ISNULL(MAX(TotalAPagar), 0) AS VentaMasAlta
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
                switch (ex.Number)
                {
                    case 208:
                        MessageBox.Show(
                            "Error 208: La vista VerReporteVentas no existe.",
                            "Error",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                        break;

                    case 53:
                        MessageBox.Show(
                            "Error 53: No se pudo conectar con el servidor.",
                            "Error",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                        break;

                    case 4060:
                        MessageBox.Show(
                            "Error 4060: No se pudo acceder a la base de datos.",
                            "Error",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                        break;

                    case -2:
                        MessageBox.Show(
                            "Error -2: La consulta tardó demasiado.",
                            "Error",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning);
                        break;

                    case 245:
                        MessageBox.Show(
                            "Error 245: Existe un valor con formato incorrecto.",
                            "Error",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning);
                        break;

                    case 8115:
                        MessageBox.Show(
                            "Error 8115: El valor numérico excede el límite permitido.",
                            "Error",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning);
                        break;

                    default:
                        MessageBox.Show(
                            "Error SQL " + ex.Number + ": " + ex.Message,
                            "Error",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                        break;
                }
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
    }
}
