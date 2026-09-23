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

        public DbVentas()
        {
        }

        public int IdVenta1
        {
            get => IdVenta;
            set => IdVenta = value;
        }

        public static DataTable CargarVentas()
        {
            try
            {
                using (SqlConnection conexion = Conexion.Conectar())
                {
                    string consulta = "SELECT * FROM VerVentas;";

                    using (SqlDataAdapter adaptador = new SqlDataAdapter(consulta, conexion))
                    {
                        DataTable tabla = new DataTable();
                        adaptador.Fill(tabla);
                        return tabla;
                    }
                }
            }
            catch (SqlException ex)
            {
                switch (ex.Number)
                {
                    case 208:
                        MessageBox.Show("Error 208: La vista VerVentas no existe.", "Error 208", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;

                    case 53:
                        MessageBox.Show("Error 53: No se pudo conectar con el servidor SQL.", "Error 53", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;

                    case 4060:
                        MessageBox.Show("Error 4060: No se pudo acceder a la base de datos.", "Error 4060", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;

                    case -2:
                        MessageBox.Show("Error -2: La operación tardó demasiado.", "Error -2", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        break;

                    default:
                        MessageBox.Show("Error SQL " + ex.Number + ": " + ex.Message, "Error " + ex.Number, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                }

                return new DataTable();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error inesperado: " + ex.Message, "Error inesperado", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return new DataTable();
            }
        }

        public static DataTable CargarDetalleVenta(int idVenta)
        {
            DataTable tabla = new DataTable();

            try
            {
                using (SqlConnection conexion = Conexion.Conectar())
                {
                    string consulta = @"
                        SELECT IdDetalleVenta, IdVenta, ProductoVendido,
                               Cantidad, PrecioUnitario,
                               (Cantidad * PrecioUnitario) AS SubTotal
                        FROM DetalleVenta
                        WHERE IdVenta = @IdVenta;";

                    using (SqlCommand comando = new SqlCommand(consulta, conexion))
                    {
                        comando.Parameters.AddWithValue("@IdVenta", idVenta);

                        using (SqlDataAdapter adaptador = new SqlDataAdapter(comando))
                        {
                            adaptador.Fill(tabla);
                        }
                    }
                }

                return tabla;
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Error SQL " + ex.Number + ": " + ex.Message, "Error " + ex.Number, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return new DataTable();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error inesperado: " + ex.Message, "Error inesperado", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return new DataTable();
            }
        }

        public bool EliminarVenta()
        {
            using (SqlConnection conexion = Conexion.Conectar())
            {
                SqlTransaction transaccion = null;

                try
                {
                    transaccion = conexion.BeginTransaction();

                    string eliminarFactura = "DELETE FROM Factura WHERE IdVenta = @IdVenta;";

                    using (SqlCommand comando = new SqlCommand(eliminarFactura, conexion, transaccion))
                    {
                        comando.Parameters.AddWithValue("@IdVenta", IdVenta1);
                        comando.ExecuteNonQuery();
                    }

                    string eliminarDetalle = "DELETE FROM DetalleVenta WHERE IdVenta = @IdVenta;";

                    using (SqlCommand comando = new SqlCommand(eliminarDetalle, conexion, transaccion))
                    {
                        comando.Parameters.AddWithValue("@IdVenta", IdVenta1);
                        comando.ExecuteNonQuery();
                    }

                    string eliminarVenta = "DELETE FROM Venta WHERE IdVenta = @IdVenta;";

                    using (SqlCommand comando = new SqlCommand(eliminarVenta, conexion, transaccion))
                    {
                        comando.Parameters.AddWithValue("@IdVenta", IdVenta1);

                        int filasAfectadas = comando.ExecuteNonQuery();

                        if (filasAfectadas > 0)
                        {
                            transaccion.Commit();
                            return true;
                        }

                        transaccion.Rollback();

                        MessageBox.Show("No se encontró la venta indicada.", "Venta no encontrada", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                        return false;
                    }
                }
                catch (SqlException ex)
                {
                    try
                    {
                        transaccion?.Rollback();
                    }
                    catch
                    {
                    }

                    switch (ex.Number)
                    {
                        case 547:
                            MessageBox.Show("Error 547: La venta tiene registros relacionados.", "Error 547", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            break;

                        case 515:
                            MessageBox.Show("Error 515: Hay campos obligatorios sin completar.", "Error 515", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            break;

                        case 53:
                            MessageBox.Show("Error 53: No se pudo conectar con el servidor SQL.", "Error 53", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;

                        case 4060:
                            MessageBox.Show("Error 4060: No se pudo acceder a la base de datos.", "Error 4060", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;

                        case -2:
                            MessageBox.Show("Error -2: La operación tardó demasiado.", "Error -2", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            break;

                        case 208:
                            MessageBox.Show("Error 208: Una tabla relacionada no existe.", "Error 208", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;

                        default:
                            MessageBox.Show("Error SQL " + ex.Number + ": " + ex.Message, "Error " + ex.Number, MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                    }

                    return false;
                }
                catch (Exception ex)
                {
                    try
                    {
                        transaccion?.Rollback();
                    }
                    catch
                    {
                    }

                    MessageBox.Show("Error inesperado: " + ex.Message, "Error inesperado", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
        }

        public static DataTable BuscarVenta(string termino)
        {
            try
            {
                using (SqlConnection conexion = Conexion.Conectar())
                {
                    string consulta = @"SELECT * FROM VerVentas
                        WHERE CAST(IdVenta AS VARCHAR) LIKE @buscar OR Cliente LIKE @buscar;";

                    using (SqlDataAdapter adaptador = new SqlDataAdapter(consulta, conexion))
                    {
                        adaptador.SelectCommand.Parameters.AddWithValue("@buscar", "%" + termino + "%");

                        DataTable tabla = new DataTable();
                        adaptador.Fill(tabla);
                        return tabla;
                    }
                }
            }
            catch (SqlException ex)
            {
                switch (ex.Number)
                {
                    case 208:
                        MessageBox.Show("Error 208: La vista VerVentas no existe.", "Error 208", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;

                    case 53:
                        MessageBox.Show("Error 53: No se pudo conectar con el servidor SQL.", "Error 53", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;

                    case 4060:
                        MessageBox.Show("Error 4060: No se pudo acceder a la base de datos.", "Error 4060", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;

                    case -2:
                        MessageBox.Show("Error -2: La operación tardó demasiado.", "Error -2", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        break;

                    case 245:
                        MessageBox.Show("Error 245: No se pudo convertir el identificador.", "Error 245", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        break;

                    default:
                        MessageBox.Show("Error SQL " + ex.Number + ": " + ex.Message, "Error " + ex.Number, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                }

                return new DataTable();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error inesperado: " + ex.Message, "Error inesperado", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return new DataTable();
            }
        }
    }
}