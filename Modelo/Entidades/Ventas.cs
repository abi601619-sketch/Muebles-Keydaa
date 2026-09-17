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
        private DateTime FechaVenta;
        private int Cliente;
        private int MetodoPago;
        private decimal SubTotal;




        public DbVentas()
        {

        }

        public DbVentas(int idVenta, DateTime fechaVenta, int cliente, int metodoPago, decimal subTotal)
        {
            IdVenta = idVenta;
            FechaVenta = fechaVenta;
            Cliente = cliente;
            MetodoPago = metodoPago;
            SubTotal = subTotal;
        }

        public int IdVenta1 { get => IdVenta; set => IdVenta = value; }
        public DateTime FechaVenta1 { get => FechaVenta; set => FechaVenta = value; }
        public int Cliente1 { get => Cliente; set => Cliente = value; }
        public int MetodoPago1 { get => MetodoPago; set => MetodoPago = value; }
        public decimal SubTotal1 { get => SubTotal; set => SubTotal = value; }

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


        public int InsertarVenta()
        {
            try
            {
                string comandoSQL = @"INSERT INTO Venta 
                    (FechaVenta, IdCliente, IdMetodoPago, SubTotal)
                    VALUES
                    (@FechaVenta, @IdCliente, @IdMetodoPago, @SubTotal);

                    SELECT CAST(SCOPE_IDENTITY() AS INT);";

                using (SqlConnection conexion = Conexion.Conectar())
                {
                    using (SqlCommand comandoObjeto =
                        new SqlCommand(comandoSQL, conexion))
                    {
                        comandoObjeto.Parameters.AddWithValue("@FechaVenta", FechaVenta);

                        comandoObjeto.Parameters.AddWithValue("@IdCliente", Cliente);

                        comandoObjeto.Parameters.AddWithValue("@IdMetodoPago", MetodoPago);

                        SqlParameter parametro =
                            comandoObjeto.Parameters.Add("@SubTotal", SqlDbType.Decimal);

                        parametro.Precision = 10;
                        parametro.Scale = 2;
                        parametro.Value = SubTotal1;

                        int idVenta =
                            Convert.ToInt32(comandoObjeto.ExecuteScalar());

                        return idVenta;
                    }
                }
            }
            catch (SqlException ex)
            {
                switch (ex.Number)
                {
                    case 2627:
                    case 2601:
                        MessageBox.Show(
                            "Error 2627/2601: La venta ya está registrada.",
                            "Venta duplicada",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning
                        );
                        break;

                    case 547:
                        MessageBox.Show(
                            "Error 547: El cliente o método de pago no existe.",
                            "Error de relación",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error
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

                    case 245:
                        MessageBox.Show(
                            "Error 245: Uno de los datos tiene un formato incorrecto.",
                            "Error de conversión",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning
                        );
                        break;

                    case 8115:
                        MessageBox.Show(
                            "Error 8115: El subtotal supera el límite permitido.",
                            "Error en subtotal",
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
                            "Error 208: La tabla Venta no existe.",
                            "Error de base de datos",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error
                        );
                        break;

                    default:
                        MessageBox.Show(
                            "Error SQL " + ex.Number + ": " + ex.Message,
                            "Error al guardar venta",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error
                        );
                        break;
                }

                return 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Error inesperado: " + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );

                return 0;
            }
        }

        public bool ActualizarVenta()
        {
            try
            {
                string comandoSQL = @"UPDATE Venta 
                    SET FechaVenta = @FechaVenta,
                        IdMetodoPago = @IdMetodoPago,
                        SubTotal = @SubTotal
                    WHERE IdVenta = @IdVenta;";

                using (SqlConnection conexion = Conexion.Conectar())
                {
                    using (SqlCommand comandoObjeto =
                        new SqlCommand(comandoSQL, conexion))
                    {
                        comandoObjeto.Parameters.AddWithValue(
                            "@FechaVenta",
                            FechaVenta1
                        );

                        comandoObjeto.Parameters.AddWithValue(
                            "@IdMetodoPago",
                            MetodoPago1
                        );

                        SqlParameter parametro =
                            comandoObjeto.Parameters.Add(
                                "@SubTotal",
                                SqlDbType.Decimal
                            );

                        parametro.Precision = 10;
                        parametro.Scale = 2;
                        parametro.Value = SubTotal1;

                        comandoObjeto.Parameters.AddWithValue(
                            "@IdVenta",
                            IdVenta1
                        );

                        int filasAfectadas =
                            comandoObjeto.ExecuteNonQuery();

                        if (filasAfectadas > 0)
                        {
                            return true;
                        }

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
                switch (ex.Number)
                {
                    case 547:
                        MessageBox.Show(
                            "Error 547: El método de pago no existe.",
                            "Error de relación",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error
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

                    case 245:
                        MessageBox.Show(
                            "Error 245: Uno de los datos tiene un formato incorrecto.",
                            "Error de conversión",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning
                        );
                        break;

                    case 8115:
                        MessageBox.Show(
                            "Error 8115: El subtotal supera el límite permitido.",
                            "Error en subtotal",
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
                            "Error 208: La tabla Venta no existe.",
                            "Error de base de datos",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error
                        );
                        break;

                    default:
                        MessageBox.Show(
                            "Error SQL " + ex.Number + ": " + ex.Message,
                            "Error al actualizar venta",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error
                        );
                        break;
                }

                return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Error inesperado: " + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );

                return false;
            }
        }

        public DataTable ObtenerDetalleVenta(int idDetalleVenta)
        {
            DataTable dt = new DataTable();

            try
            {
                using (SqlConnection cn = Conexion.Conectar())
                {
                    string query = @"SELECT 
                        IdDetalleVenta,
                        IdVenta,
                        ProductoVendido,
                        Cantidad,
                        PrecioUnitario
                        FROM DetalleVenta
                        WHERE IdDetalleVenta = @IdDetalleVenta;";

                    using (SqlCommand cmd = new SqlCommand(query, cn))
                    {
                        cmd.Parameters.AddWithValue(
                            "@IdDetalleVenta",
                            idDetalleVenta
                        );

                        SqlDataAdapter da = new SqlDataAdapter(cmd);

                        da.Fill(dt);
                    }
                }
            }
            catch (SqlException ex)
            {
                switch (ex.Number)
                {
                    case 208:
                        MessageBox.Show(
                            "Error 208: La tabla DetalleVenta no existe.",
                            "Error de base de datos",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error
                        );
                        break;

                    case 245:
                        MessageBox.Show(
                            "Error 245: El identificador del detalle no es válido.",
                            "Error de conversión",
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

                    default:
                        MessageBox.Show(
                            "Error SQL " + ex.Number + ": " + ex.Message,
                            "Error al cargar detalle",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error
                        );
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Error inesperado: " + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }

            return dt;
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
                        cmd.Parameters.AddWithValue("@IdVenta", IdVenta);

                        cmd.ExecuteNonQuery();
                    }

                    // Eliminar detalle de venta.
                    string cmdDetalle =
                        "DELETE FROM DetalleVenta WHERE IdVenta = @IdVenta;";

                    using (SqlCommand cmd =
                        new SqlCommand(cmdDetalle, conexion, transaccion))
                    {
                        cmd.Parameters.AddWithValue("@IdVenta", IdVenta);

                        cmd.ExecuteNonQuery();
                    }

                    // Eliminar venta.
                    string cmdVenta =
                        "DELETE FROM Venta WHERE IdVenta = @IdVenta;";

                    using (SqlCommand cmd =
                        new SqlCommand(cmdVenta, conexion, transaccion))
                    {
                        cmd.Parameters.AddWithValue("@IdVenta", IdVenta);

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
