using Modelo.Conexión_DB;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Datos
{
    public class DbDashboard
    {
        private readonly string cadenaConexion;

        public DbDashboard(string cadenaConexion)
        {
            this.cadenaConexion = cadenaConexion;
        }


        //INDICADORES

        public DataTable ObtenerIndicadores()
        {
            DataTable tabla = new DataTable();

            try
            {
                using (SqlConnection conexion = new SqlConnection(cadenaConexion))
                {
                    using (SqlCommand comando = new SqlCommand("sp_Dashboard_Indicadores",
                        conexion))
                    {
                        comando.CommandType = CommandType.StoredProcedure;

                        using (SqlDataAdapter adaptador = new SqlDataAdapter(comando))
                        {
                            adaptador.Fill(tabla);
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return tabla;
        }


        //PEDIDOS POR ESTADO

        public DataTable ObtenerPedidosPorEstado(int? mes = null, int? anio = null)
        {
            DataTable tabla = new DataTable();

            try
            {
                using (SqlConnection conexion = new SqlConnection(cadenaConexion))
                {
                    using (SqlCommand comando = new SqlCommand("sp_Dashboard_PedidosEstado",
                        conexion))
                    {
                        comando.CommandType = CommandType.StoredProcedure;

                        comando.Parameters.AddWithValue("@Mes", (object)mes ?? DBNull.Value);

                        comando.Parameters.AddWithValue("@Anio", (object)anio ?? DBNull.Value);

                        using (SqlDataAdapter adaptador = new SqlDataAdapter(comando))
                        {
                            adaptador.Fill(tabla);
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return tabla;
        }


        //COTIZACIONES POR ESTADO

        public DataTable ObtenerCotizacionesPorEstado()
        {
            DataTable tabla = new DataTable();

            try
            {
                using (SqlConnection conexion = Conexion.Conectar())
                {
                    using (SqlCommand comando =
                           new SqlCommand(
                               "sp_Dashboard_CotizacionesEstado",
                               conexion))
                    {
                        comando.CommandType =
                            CommandType.StoredProcedure;

                        comando.Parameters.AddWithValue(
                            "@Mes",
                            DBNull.Value
                        );

                        comando.Parameters.AddWithValue(
                            "@Anio",
                            DateTime.Now.Year
                        );

                        using (SqlDataAdapter adaptador =
                               new SqlDataAdapter(comando))
                        {
                            adaptador.Fill(tabla);
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return tabla;
        }

        //VENTAS MENSUALES

        public DataTable ObtenerVentasPorMes()
        {
            DataTable tabla = new DataTable();

            try
            {
                using (SqlConnection conexion = Conexion.Conectar())
                {
                    using (SqlCommand comando =
                           new SqlCommand(
                               "sp_Dashboard_VentasMensuales",
                               conexion))
                    {
                        comando.CommandType =
                            CommandType.StoredProcedure;

                        comando.Parameters.AddWithValue(
                            "@Anio",
                            DateTime.Now.Year
                        );

                        using (SqlDataAdapter adaptador =
                               new SqlDataAdapter(comando))
                        {
                            adaptador.Fill(tabla);
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return tabla;
        }
    }
}
