using Modelo.Conexión_DB;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

public class ReportesCotizaciones
{
    public static DataTable CargarReporteCotizaciones()
    {
        DataTable tabla = new DataTable();

        try
        {
            using (SqlConnection conexion = Conexion.Conectar())
            {
                string consulta = "SELECT * FROM VerCotizaciones2;";

                using (SqlDataAdapter adaptador = new SqlDataAdapter(consulta, conexion))
                    adaptador.Fill(tabla);
            }
        }
        catch (SqlException ex)
        {
            switch (ex.Number)
            {
                case 208:
                    MessageBox.Show("La vista VerCotizaciones2 no existe.", "ERR-SQL-004", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;

                case 53:
                    MessageBox.Show("No se pudo conectar con el servidor.", "ERR-SQL-001", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;

                case 4060:
                    MessageBox.Show("No se pudo acceder a la base de datos.", "ERR-SQL-002", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;

                case -2:
                    MessageBox.Show("La operación tardó demasiado.", "ERR-SQL-003", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    break;

                default:
                    MessageBox.Show("Ocurrió un error SQL.\n" + ex.Message, "ERR-SQL-999", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show("Ocurrió un error inesperado.\n" + ex.Message, "ERR-SQL-999", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        return tabla;
    }


    public static DataTable ObtenerCotizacionesPorFecha(DateTime fechaInicio, DateTime fechaFin)
    {
        DataTable tabla = new DataTable();

        try
        {
            string consulta = @"SELECT * FROM VerCotizaciones WHERE Fecha >= @FechaInicio AND Fecha < @FechaFin;";

            using (SqlConnection conexion = Conexion.Conectar())
            using (SqlDataAdapter adaptador = new SqlDataAdapter(consulta, conexion))
            {
                adaptador.SelectCommand.Parameters.Add("@FechaInicio", SqlDbType.Date).Value = fechaInicio.Date;
                adaptador.SelectCommand.Parameters.Add("@FechaFin", SqlDbType.Date).Value = fechaFin.Date.AddDays(1);

                adaptador.Fill(tabla);
            }
        }
        catch (SqlException ex)
        {
            switch (ex.Number)
            {
                case 208:
                    MessageBox.Show("La vista VerCotizaciones no existe.", "ERR-SQL-004", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;

                case 53:
                    MessageBox.Show("No se pudo conectar con el servidor.", "ERR-SQL-001", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;

                case 4060:
                    MessageBox.Show("No se pudo acceder a la base de datos.", "ERR-SQL-002", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;

                case -2:
                    MessageBox.Show("La consulta tardó demasiado.", "ERR-SQL-003", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    break;

                case 245:
                    MessageBox.Show("Existe un valor con formato incorrecto.", "ERR-SQL-009", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    break;

                default:
                    MessageBox.Show("Ocurrió un error SQL.\n" + ex.Message, "ERR-SQL-999", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show("Ocurrió un error inesperado.\n" + ex.Message, "ERR-SQL-999", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        return tabla;
    }


    public static DataTable ObtenerEstadisticasCotizaciones(DateTime fechaInicio, DateTime fechaFin)
    {
        DataTable tabla = new DataTable();

        try
        {
            string consulta = @"SELECT COUNT(*) AS CotizacionesRegistradas, ISNULL(SUM(CASE WHEN Estado = 'Aprobada' THEN 1 ELSE 0 END), 0) AS CotizacionesAprobadas,
                                ISNULL(SUM(CASE WHEN Estado = 'Rechazada' THEN 1 ELSE 0 END), 0) AS CotizacionesRechazadas FROM VerCotizaciones2
                                WHERE Fecha >= @FechaInicio AND Fecha < @FechaFin;";

            using (SqlConnection conexion = Conexion.Conectar())
            using (SqlDataAdapter adaptador = new SqlDataAdapter(consulta, conexion))
            {
                adaptador.SelectCommand.Parameters.Add("@FechaInicio", SqlDbType.Date).Value = fechaInicio.Date;
                adaptador.SelectCommand.Parameters.Add("@FechaFin", SqlDbType.Date).Value = fechaFin.Date.AddDays(1);

                adaptador.Fill(tabla);
            }
        }
        catch (SqlException ex)
        {
            switch (ex.Number)
            {
                case 208:
                    MessageBox.Show("La vista VerCotizaciones2 no existe.", "ERR-SQL-004", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;

                case 53:
                    MessageBox.Show("No se pudo conectar con el servidor.", "ERR-SQL-001", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;

                case 4060:
                    MessageBox.Show("No se pudo acceder a la base de datos.", "ERR-SQL-002", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;

                case -2:
                    MessageBox.Show("La consulta tardó demasiado.", "ERR-SQL-003", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    break;

                case 245:
                    MessageBox.Show("Existe un valor con formato incorrecto.", "ERR-SQL-009", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    break;

                default:
                    MessageBox.Show("Ocurrió un error SQL.\n" + ex.Message, "ERR-SQL-999", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show("Ocurrió un error inesperado.\n" + ex.Message, "ERR-SQL-999", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        return tabla;
    }
}