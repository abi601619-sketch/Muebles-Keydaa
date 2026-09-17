
using Modelo.Conexión_DB;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

public class ReportesCotizaciones
{
    // ==========================================
    // CARGAR TODAS LAS COTIZACIONES
    // ==========================================

    public static DataTable CargarReporteCotizaciones()
    {
        DataTable tabla = new DataTable();

        try
        {
            using (SqlConnection conexion = Conexion.Conectar())
            {
                string consulta = @"
                    SELECT *
                    FROM VerCotizaciones2;";

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
                "Ocurrió un error al cargar el reporte de cotizaciones:\n\n" +
                ex.Message,
                "Error",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error
            );
        }

        return tabla;
    }





    // ==========================================
    // OBTENER COTIZACIONES POR FECHA
    // ==========================================

    public static DataTable ObtenerCotizacionesPorFecha(
        DateTime fechaInicio,
        DateTime fechaFin)
    {
        DataTable tabla = new DataTable();

        try
        {
            string consulta = @"
                SELECT *
                FROM VerCotizaciones
                WHERE Fecha >= @FechaInicio
                  AND Fecha < @FechaFin;";

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
                "Ocurrió un error al obtener las cotizaciones por fecha:\n\n" +
                ex.Message,
                "Error",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error
            );
        }

        return tabla;
    }


    // ==========================================
    // OBTENER ESTADÍSTICAS
    // ==========================================

    public static DataTable ObtenerEstadisticasCotizaciones(
        DateTime fechaInicio,
        DateTime fechaFin)
    {
        DataTable tabla = new DataTable();

        try
        {
            string consulta = @"
                SELECT

                    COUNT(*) AS CotizacionesRegistradas,

                    ISNULL(
                        SUM(
                            CASE
                                WHEN Estado = 'Aprobada'
                                THEN 1
                                ELSE 0
                            END
                        ), 0
                    ) AS CotizacionesAprobadas,

                    ISNULL(
                        SUM(
                            CASE
                                WHEN Estado = 'Rechazada'
                                THEN 1
                                ELSE 0
                            END
                        ), 0
                    ) AS CotizacionesRechazadas

                FROM VerCotizaciones2

                WHERE Fecha >= @FechaInicio
                  AND Fecha < @FechaFin;";

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
                "Ocurrió un error al obtener las estadísticas de cotizaciones:\n\n" +
                ex.Message,
                "Error",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error
            );
        }

        return tabla;
    }


}

