using Modelo.Conexión_DB;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;


namespace Modelo.Entidades
{
    public class ReportesClientes
    {
        public static DataTable CargarReporteClientes()
        {
            DataTable tabla = new DataTable();

            try
            {
                using (SqlConnection conexion = Conexion.Conectar())
                {
                    string consulta = "SELECT * FROM VerReporteClientes2;";

                    using (SqlDataAdapter adaptador =
                        new SqlDataAdapter(consulta, conexion))
                    {
                        adaptador.Fill(tabla);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Ocurrió un error al cargar el reporte de clientes:\n\n" +
                    ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }

            return tabla;
        }


        public static DataTable ObtenerClientes()
        {
            DataTable tabla = new DataTable();

            try
            {
                using (SqlConnection conexion = Conexion.Conectar())
                {
                    string consulta =
                        "SELECT * FROM VerReporteClientes2;";

                    using (SqlDataAdapter adaptador =
                        new SqlDataAdapter(consulta, conexion))
                    {
                        adaptador.Fill(tabla);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Ocurrió un error al obtener los clientes:\n\n" +
                    ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }

            return tabla;
        }
        public static int ContarClientesCorporativos()
        {
            int total = 0;

            try
            {
                using (SqlConnection conexion = Conexion.Conectar())
                {
                    string consulta =
                        "SELECT COUNT(*) FROM Cliente WHERE IdTipoCliente = 1;";

                    using (SqlCommand comando =
                        new SqlCommand(consulta, conexion))
                    {
                        total = Convert.ToInt32(
                            comando.ExecuteScalar()
                        );
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Ocurrió un error al contar los clientes corporativos:\n\n" +
                    ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }

            return total;
        }

        public static int ContarClientesIndividuales()
        {
            int total = 0;

            try
            {
                using (SqlConnection conexion = Conexion.Conectar())
                {
                    string consulta =
                        "SELECT COUNT(*) FROM Cliente WHERE IdTipoCliente = 2;";

                    using (SqlCommand comando =
                        new SqlCommand(consulta, conexion))
                    {
                        total = Convert.ToInt32(
                            comando.ExecuteScalar()
                        );
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Ocurrió un error al contar los clientes individuales:\n\n" +
                    ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }

            return total;
        }

        public static int ContarClientesTotales()
        {
            int total = 0;

            try
            {
                using (SqlConnection conexion = Conexion.Conectar())
                {
                    string consulta =
                        "SELECT COUNT(*) FROM Cliente;";

                    using (SqlCommand comando =
                        new SqlCommand(consulta, conexion))
                    {
                        total = Convert.ToInt32(
                            comando.ExecuteScalar()
                        );
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Ocurrió un error al contar los clientes totales:\n\n" +
                    ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }

            return total;
        }
        public DataTable ObtenerClientesPorFecha(
      DateTime fechaInicio,
      DateTime fechaFin)
        {
            DataTable tabla = new DataTable();

            try
            {
                string consulta = @"
            SELECT *
            FROM VerReporteClientes2
            WHERE [Fecha de Registro] >= @FechaInicio
              AND [Fecha de Registro] < @FechaFin;";

                using (SqlConnection conexion = Conexion.Conectar())
                {
                    using (SqlDataAdapter adaptador =
                        new SqlDataAdapter(consulta, conexion))
                    {
                        adaptador.SelectCommand.Parameters.Add(
                            "@FechaInicio",
                            SqlDbType.Date
                        ).Value = fechaInicio.Date;

                        adaptador.SelectCommand.Parameters.Add(
                            "@FechaFin",
                            SqlDbType.Date
                        ).Value = fechaFin.Date.AddDays(1);

                        adaptador.Fill(tabla);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Ocurrió un error al obtener los clientes por fecha:\n\n" +
                    ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }

            return tabla;
        }

        public static DataTable ObtenerEstadisticasClientes(
      DateTime fechaInicio,
      DateTime fechaFin)
        {
            DataTable tabla = new DataTable();

            try
            {
                string consulta = @"
            SELECT
                COUNT(*) AS ClientesTotales,

                ISNULL(
                    SUM(
                        CASE
                            WHEN [Tipo de Cliente] = 'Empresa'
                            THEN 1
                            ELSE 0
                        END
                    ), 0
                ) AS ClientesCorporativos,

                ISNULL(
                    SUM(
                        CASE
                            WHEN [Tipo de Cliente] = 'Persona Natural'
                            THEN 1
                            ELSE 0
                        END
                    ), 0
                ) AS ClientesIndividuales

            FROM VerReporteClientes2

            WHERE [Fecha de Registro] >= @FechaInicio
              AND [Fecha de Registro] < @FechaFin;";

                using (SqlConnection conexion = Conexion.Conectar())
                {
                    using (SqlDataAdapter adaptador =
                        new SqlDataAdapter(consulta, conexion))
                    {
                        adaptador.SelectCommand.Parameters.Add(
                            "@FechaInicio",
                            SqlDbType.Date
                        ).Value = fechaInicio.Date;

                        adaptador.SelectCommand.Parameters.Add(
                            "@FechaFin",
                            SqlDbType.Date
                        ).Value = fechaFin.Date.AddDays(1);

                        adaptador.Fill(tabla);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Ocurrió un error al obtener las estadísticas de clientes:\n\n" +
                    ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }

            return tabla;
        }


    }
}
