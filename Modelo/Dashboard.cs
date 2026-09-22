using Modelo.Conexión_DB;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

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
        public static int ContarPedidosActivos()
        {
            int total = 0;

            try
            {
                using (SqlConnection conexion = Conexion.Conectar())
                {
                    using (SqlCommand comando = new SqlCommand("sp_ContarPedidosActivos", conexion))
                    {
                        comando.CommandType = CommandType.StoredProcedure;

                        total = Convert.ToInt32(comando.ExecuteScalar());
                    }
                }
            }
            catch (SqlException ex)
            {
                switch (ex.Number)
                {
                    case 2:
                        MessageBox.Show(
                            "No se pudo conectar con la base de datos.\nVerifique el servidor.",
                            "Error",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                        break;

                    case 53:
                        MessageBox.Show(
                            "No se encontró el servidor de base de datos.\nVerifique la conexión.",
                            "Error",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                        break;

                    default:
                        MessageBox.Show(
                            "Ocurrió un error al obtener los pedidos activos.\nCódigo: " + ex.Number,
                            "Error",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Ocurrió un error inesperado.\n" + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }

            return total;
        }



        //COTIZACIONES POR ESTADO
        // COTIZACIONES POR ESTADO
        public static DataTable ObtenerCotizacionesPorEstado()
        {
            DataTable datos = new DataTable();

            try
            {
                using (SqlConnection conexion = Conexion.Conectar())
                {
                    using (SqlCommand comando = new SqlCommand(
                        "sp_Dashboard_CotizacionesEstado",
                        conexion))
                    {
                        comando.CommandType = CommandType.StoredProcedure;

                        using (SqlDataAdapter adaptador =
                            new SqlDataAdapter(comando))
                        {
                            adaptador.Fill(datos);
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                switch (ex.Number)
                {
                    case 2:
                        MessageBox.Show(
                            "No se pudo conectar con la base de datos.\nVerifique el servidor.",
                            "Error",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                        break;

                    case 53:
                        MessageBox.Show(
                            "No se encontró el servidor de base de datos.\nVerifique la conexión.",
                            "Error",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                        break;

                    default:
                        MessageBox.Show(
                            "Ocurrió un error al obtener las cotizaciones por estado.\nCódigo: "
                            + ex.Number,
                            "Error",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Ocurrió un error inesperado.\n" + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }

            return datos;
        }

        // PEDIDOS POR ESTADO
        public static DataTable ObtenerPedidosPorEstado()
        {
            DataTable datos = new DataTable();

            try
            {
                using (SqlConnection conexion = Conexion.Conectar())
                {
                    using (SqlCommand comando = new SqlCommand(
                        "sp_Dashboard_PedidosEstado",
                        conexion))
                    {
                        comando.CommandType = CommandType.StoredProcedure;

                        using (SqlDataAdapter adaptador =
                            new SqlDataAdapter(comando))
                        {
                            adaptador.Fill(datos);
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                switch (ex.Number)
                {
                    case 2:
                        MessageBox.Show(
                            "No se pudo conectar con la base de datos.\nVerifique el servidor.",
                            "Error",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                        break;

                    case 53:
                        MessageBox.Show(
                            "No se encontró el servidor de base de datos.\nVerifique la conexión.",
                            "Error",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                        break;

                    case 2812:
                        MessageBox.Show(
                            "No se encontró el procedimiento almacenado de pedidos por estado.\nVerifique que exista en la base de datos.",
                            "Error",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                        break;

                    default:
                        MessageBox.Show(
                            "Ocurrió un error al obtener los pedidos por estado.\nCódigo: "
                            + ex.Number,
                            "Error",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Ocurrió un error inesperado.\n" + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }

            return datos;
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

        //INVENTARIO

        public DataTable ObtenerInventarioEstado()
        {
            DataTable tabla = new DataTable();

            try
            {
                using (SqlConnection conexion = Conexion.Conectar())
                {
                    using (SqlCommand comando =
                           new SqlCommand(
                               "sp_Dashboard_InventarioEstado",
                               conexion))
                    {
                        comando.CommandType =
                            CommandType.StoredProcedure;

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
