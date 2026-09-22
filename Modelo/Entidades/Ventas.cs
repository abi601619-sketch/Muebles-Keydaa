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

        public int IdVenta1 { get => IdVenta; set => IdVenta = value; }

        public DbVentas()
        {

        }

        public static DataTable CargarVentas()
        {
            try
            {
                SqlConnection conectar = Conexion.Conectar();

                string comando = "SELECT * FROM VerVentas;";

                SqlDataAdapter adapter = new SqlDataAdapter(comando, conectar);

                DataTable dt = new DataTable();

                adapter.Fill(dt);

                return dt;
            }
            catch (SqlException ex)
            {
                switch (ex.Number)
                {
                    case 208:
                        MessageBox.Show(
                            "Error 208: La vista VerVentas no existe.",
                            "Error de base de datos",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error
                        );
                        break;

                    case 53:
                        MessageBox.Show(
                            "Error 53: No se pudo conectar con el servidor SQL.",
                            "Error de conexión",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error
                        );
                        break;

                    case 4060:
                        MessageBox.Show(
                            "Error 4060: No se pudo acceder a la base de datos.",
                            "Error de base de datos",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error
                        );
                        break;

                    case -2:
                        MessageBox.Show(
                            "Error -2: La operación tardó demasiado.",
                            "Tiempo de espera agotado",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning
                        );
                        break;

                    default:
                        MessageBox.Show(
                            "Error SQL " + ex.Number + ": " + ex.Message,
                            "Error de base de datos",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error
                        );
                        break;
                }

                return new DataTable();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Error inesperado: " + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );

                return new DataTable();
            }
        }


        public static DataTable CargarDetalleVenta(int idVenta)
        {
            DataTable dt = new DataTable();

            try
            {
                using (SqlConnection conectar = Conexion.Conectar())
                {
                    string comando = @"
                SELECT
                    IdDetalleVenta,
                    IdVenta,
                    ProductoVendido,
                    Cantidad,
                    PrecioUnitario,
                    (Cantidad * PrecioUnitario) AS SubTotal
                FROM DetalleVenta
                WHERE IdVenta = @IdVenta;";

                    using (SqlCommand cmd = new SqlCommand(comando, conectar))
                    {
                        cmd.Parameters.AddWithValue("@IdVenta", idVenta);

                        using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                        {
                            adapter.Fill(dt);
                        }
                    }
                }

                return dt;
            }
            catch (SqlException ex)
            {
                MessageBox.Show(
                    "Error SQL al cargar los detalles de la venta:\n" + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                return new DataTable();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Error al cargar los detalles de la venta:\n" + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

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

                    // Eliminar factura si existe.
                    string cmdFactura =
                        "DELETE FROM Factura WHERE IdVenta = @IdVenta;";

                    using (SqlCommand cmd =
                        new SqlCommand(cmdFactura, conexion, transaccion))
                    {
                        cmd.Parameters.AddWithValue("@IdVenta", IdVenta1);

                        cmd.ExecuteNonQuery();
                    }

                    // Eliminar detalle de venta.
                    string cmdDetalle =
                        "DELETE FROM DetalleVenta WHERE IdVenta = @IdVenta;";

                    using (SqlCommand cmd =
                        new SqlCommand(cmdDetalle, conexion, transaccion))
                    {
                        cmd.Parameters.AddWithValue("@IdVenta", IdVenta1);

                        cmd.ExecuteNonQuery();
                    }

                    // Eliminar venta.
                    string cmdVenta =
                        "DELETE FROM Venta WHERE IdVenta = @IdVenta;";

                    using (SqlCommand cmd =
                        new SqlCommand(cmdVenta, conexion, transaccion))
                    {
                        cmd.Parameters.AddWithValue("@IdVenta", IdVenta1);

                        int filas = cmd.ExecuteNonQuery();

                        if (filas > 0)
                        {
                            transaccion.Commit();
                            return true;
                        }
                        else
                        {
                            transaccion.Rollback();

                            MessageBox.Show(
                                "No se encontró la venta indicada.",
                                "Venta no encontrada",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning
                            );

                            return false;
                        }
                    }
                }
                catch (SqlException ex)
                {
                    try
                    {
                        if (transaccion != null)
                        {
                            transaccion.Rollback();
                        }
                    }
                    catch
                    {
                    }

                    switch (ex.Number)
                    {
                        case 547:
                            MessageBox.Show(
                                "Error 547: La venta tiene registros relacionados.",
                                "Error al eliminar",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning
                            );
                            break;

                        case 515:
                            MessageBox.Show(
                                "Error 515: Hay campos obligatorios sin completar.",
                                "Datos incompletos",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning
                            );
                            break;

                        case 53:
                            MessageBox.Show(
                                "Error 53: No se pudo conectar con el servidor SQL.",
                                "Error de conexión",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error
                            );
                            break;

                        case 4060:
                            MessageBox.Show(
                                "Error 4060: No se pudo acceder a la base de datos.",
                                "Error de base de datos",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error
                            );
                            break;

                        case -2:
                            MessageBox.Show(
                                "Error -2: La operación tardó demasiado.",
                                "Tiempo de espera agotado",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning
                            );
                            break;

                        case 208:
                            MessageBox.Show(
                                "Error 208: Una tabla relacionada no existe.",
                                "Error de base de datos",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error
                            );
                            break;

                        default:
                            MessageBox.Show(
                                "Error SQL " + ex.Number + ": " + ex.Message,
                                "Error al eliminar venta",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error
                            );
                            break;
                    }

                    return false;
                }
                catch (Exception ex)
                {
                    try
                    {
                        if (transaccion != null)
                        {
                            transaccion.Rollback();
                        }
                    }
                    catch
                    {
                    }

                    MessageBox.Show(
                        "Error inesperado: " + ex.Message,
                        "Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error
                    );

                    return false;
                }
            }
        }

        public static DataTable BuscarVenta(string termino)
        {
            try
            {
                SqlConnection con = Conexion.Conectar();

                string comando = @"SELECT * 
                    FROM VerVentas 
                    WHERE CAST(IdVenta AS VARCHAR) LIKE @buscar
                    OR Cliente LIKE @buscar;";

                SqlDataAdapter ad = new SqlDataAdapter(comando, con);

                ad.SelectCommand.Parameters.AddWithValue(
                    "@buscar",
                    "%" + termino + "%"
                );

                DataTable dt = new DataTable();

                ad.Fill(dt);

                return dt;
            }
            catch (SqlException ex)
            {
                switch (ex.Number)
                {
                    case 208:
                        MessageBox.Show(
                            "Error 208: La vista VerVentas no existe.",
                            "Error de base de datos",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error
                        );
                        break;

                    case 53:
                        MessageBox.Show(
                            "Error 53: No se pudo conectar con el servidor SQL.",
                            "Error de conexión",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error
                        );
                        break;

                    case 4060:
                        MessageBox.Show(
                            "Error 4060: No se pudo acceder a la base de datos.",
                            "Error de base de datos",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error
                        );
                        break;

                    case -2:
                        MessageBox.Show(
                            "Error -2: La operación tardó demasiado.",
                            "Tiempo de espera agotado",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning
                        );
                        break;

                    case 245:
                        MessageBox.Show(
                            "Error 245: No se pudo convertir el identificador.",
                            "Error de conversión",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning
                        );
                        break;

                    default:
                        MessageBox.Show(
                            "Error SQL " + ex.Number + ": " + ex.Message,
                            "Error al buscar venta",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error
                        );
                        break;
                }

                return new DataTable();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Error inesperado: " + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );

                return new DataTable();
            }
        }
    }
}
