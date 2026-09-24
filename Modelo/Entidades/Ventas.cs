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
        public static bool VentaTieneFactura(int idVenta)
        {
            try
            {
                using (SqlConnection conectar = Conexion.Conectar())
                {
                    string consulta = @" SELECT COUNT(*) FROM Factura
                WHERE IdVenta = @IdVenta";

                    using (SqlCommand comando = new SqlCommand(consulta, conectar))
                    {
                        comando.Parameters.AddWithValue("@IdVenta", idVenta);

                        int cantidad = Convert.ToInt32(comando.ExecuteScalar());

                        return cantidad > 0;
                    }
                }
            }
            catch (SqlException ex)
            {
                switch (ex.Number)
                {
                    case 53:
                        MessageBox.Show("No se pudo establecer conexión con el servidor de base de datos.", "ERR-SQL-001: Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;

                    case 4060:
                        MessageBox.Show("No se pudo acceder a la base de datos especificada.", "ERR-SQL-002: Acceso a la base de datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;

                    case -2:
                        MessageBox.Show("La operación excedió el tiempo de espera permitido.", "ERR-SQL-003: Tiempo de espera agotado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        break;

                    case 208:
                        MessageBox.Show("No se encontró la tabla o el objeto requerido en la base de datos.", "ERR-SQL-004: Objeto inexistente", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;

                    case 2627:
                        MessageBox.Show("Se intentó registrar un valor duplicado que debe ser único.", "ERR-SQL-005: Clave duplicada", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        break;

                    case 2601:
                        MessageBox.Show("Se intentó registrar un valor duplicado en un índice único.", "ERR-SQL-006: Índice UNIQUE duplicado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        break;

                    case 547:
                        MessageBox.Show("La operación no puede realizarse porque viola una restricción de integridad de la base de datos.", "ERR-SQL-007: Restricción de integridad", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        break;

                    case 515:
                        MessageBox.Show("No se puede completar la operación porque falta un dato obligatorio.", "ERR-SQL-008: Campo obligatorio", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        break;

                    case 245:
                        MessageBox.Show("Se produjo un error al convertir un dato al tipo requerido.", "ERR-SQL-009: Error de conversión", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;

                    case 8115:
                        MessageBox.Show("El valor ingresado excede el rango permitido para el tipo de dato.", "ERR-SQL-010: Desbordamiento numérico", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;

                    case 8152:
                        MessageBox.Show("Uno de los datos ingresados supera la longitud permitida.", "ERR-SQL-011: Longitud de datos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        break;

                    default:
                        MessageBox.Show("Se produjo un error inesperado al consultar la base de datos. Código SQL: " + ex.Number, "ERR-SQL-999: Otro error SQL", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                }

                return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Se produjo un error inesperado al verificar si la venta tiene una factura registrada. " + ex.Message, "ERR-C#-001: Error inesperado", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }
    }
}
