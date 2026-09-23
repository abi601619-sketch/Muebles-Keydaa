using Modelo.Conexión_DB;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Modelo.Entidades
{
    public class DbFactura
    {
        private int IdFactura;
        private DateTime FechaEmision;
        private DateTime FechaVencimiento;

        private decimal Descuento;
        private int IdVenta;
        private string Observaciones;

        public DbFactura(int idFactura, DateTime fechaEmisión, DateTime fechaVencimiento, int venta, string observaciones)
        {
            IdFactura = idFactura;
            FechaEmision = fechaEmisión;
            FechaVencimiento = fechaVencimiento;
            IdVenta = venta;
            Observaciones = observaciones;
        }

        public DbFactura()
        {

        }

        public int IdFactura1 { get => IdFactura; set => IdFactura = value; }
        public DateTime FechaEmisión1 { get => FechaEmision; set => FechaEmision = value; }
        public DateTime FechaVencimiento1 { get => FechaVencimiento; set => FechaVencimiento = value; }
        public int Venta1 { get => IdVenta; set => IdVenta = value; }
        public string Observaciones1 { get => Observaciones; set => Observaciones = value; }
        public decimal Descuento1 { get => Descuento; set => Descuento = value; }

        public static DataTable CargarRegistrosFacturas()
        {
            DataTable dt = new DataTable();

            try
            {
                using (SqlConnection conectar = Conexion.Conectar())
                {
                    string comando = "SELECT * FROM VerFacturas;";

                    using (SqlDataAdapter adapter = new SqlDataAdapter(comando, conectar))
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
                        MessageBox.Show("No se encontró la vista VerFacturas.",
                            "Error 208", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;

                    case 53:
                        MessageBox.Show("No se pudo conectar con el servidor SQL.",
                            "Error 53", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;

                    case 4060:
                        MessageBox.Show("No se pudo acceder a la base de datos.",
                            "Error 4060", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;

                    case -2:
                        MessageBox.Show("La operación tardó demasiado tiempo.",
                            "Error -2", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;

                    default:
                        MessageBox.Show("Ocurrió un error al cargar las facturas.",
                            "Error " + ex.Number, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrió un error inesperado al cargar las facturas.",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return dt;
        }

        public int InsertarFactura()
        {
            string sql = @"INSERT INTO Factura (FechaEmision, FechaVencimiento, IdVenta, Descuento, Observaciones)
                   VALUES(@FechaEmision, @FechaVencimiento, @IdVenta, @Descuento, @Observaciones);
                   SELECT CAST(SCOPE_IDENTITY() AS INT);";

            try
            {
                using (SqlConnection cn = Conexion.Conectar())
                using (SqlCommand cmd = new SqlCommand(sql, cn))
                {
                    cmd.Parameters.AddWithValue("@FechaEmision", FechaEmision);
                    cmd.Parameters.AddWithValue("@FechaVencimiento", FechaVencimiento);
                    cmd.Parameters.AddWithValue("@IdVenta", IdVenta);
                    cmd.Parameters.AddWithValue("@Descuento", Descuento);

                    cmd.Parameters.AddWithValue("@Observaciones", string.IsNullOrWhiteSpace(Observaciones) ? (object)DBNull.Value : Observaciones);

                    return Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
            catch (SqlException ex)
            {
                switch (ex.Number)
                {
                    case 2627:
                    case 2601:
                        MessageBox.Show("La factura ya existe en la base de datos.",
                            "Error " + ex.Number, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        break;

                    case 547:
                        MessageBox.Show("La venta indicada no existe en la base de datos.",
                            "Error 547", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        break;

                    case 515:
                        MessageBox.Show("Faltan datos obligatorios para guardar la factura.",
                            "Error 515", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;

                    case 8115:
                        MessageBox.Show("El descuento excede el límite permitido.",
                            "Error 8115", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;

                    case 245:
                        MessageBox.Show("Uno de los datos ingresados tiene un formato incorrecto.",
                            "Error 245", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;

                    case 53:
                        MessageBox.Show("No se pudo conectar con el servidor SQL.",
                            "Error 53", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;

                    case 4060:
                        MessageBox.Show("No se pudo acceder a la base de datos.",
                            "Error 4060", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;

                    case -2:
                        MessageBox.Show("La operación tardó demasiado tiempo.",
                            "Error -2", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;

                    default:
                        MessageBox.Show("Error al insertar la factura:\n\n" + ex.Message, "Error " + ex.Number, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                }

                return 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrió un error inesperado al insertar la factura.",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return 0;
            }
        }


        public static DataTable BuscarVentaParaFactura(int idVenta)
        {
            DataTable dt = new DataTable();

            try
            {
                using (SqlConnection conectar = Conexion.Conectar())
                {
                    string comando = @"SELECT * FROM VerVentasParaFactura WHERE [#] = @IdVenta;";

                    using (SqlDataAdapter adapter = new SqlDataAdapter(comando, conectar))
                    {
                        adapter.SelectCommand.Parameters.AddWithValue("@IdVenta", idVenta);

                        adapter.Fill(dt);
                    }
                }
            }
            catch (SqlException ex)
            {
                switch (ex.Number)
                {
                    case 208:
                        MessageBox.Show("No se encontró la vista VerVentasParaFactura.",
                            "Error 208", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;

                    case 53:
                        MessageBox.Show("No se pudo conectar con el servidor SQL.",
                            "Error 53", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;

                    case 4060:
                        MessageBox.Show("No se pudo acceder a la base de datos.",
                            "Error 4060", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;

                    case -2:
                        MessageBox.Show("La operación tardó demasiado tiempo.",
                            "Error -2", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;

                    default:
                        MessageBox.Show("Ocurrió un error al buscar la venta para factura.",
                            "Error " + ex.Number, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrió un error inesperado al buscar la venta.",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return dt;
        }


        // Método que manda la información de la venta para factura
        public static DataTable CargarDetalleVentaParaFactura(int idVenta)
        {
            DataTable dt = new DataTable();

            try
            {
                using (SqlConnection conectar = Conexion.Conectar())
                {
                    string comando = @"SELECT * FROM VerDetalleVenta WHERE IdVenta = @IdVenta;";

                    using (SqlDataAdapter adapter = new SqlDataAdapter(comando, conectar))
                    {
                        adapter.SelectCommand.Parameters.AddWithValue("@IdVenta", idVenta);

                        adapter.Fill(dt);
                    }
                }
            }
            catch (SqlException ex)
            {
                switch (ex.Number)
                {
                    case 208:
                        MessageBox.Show("No se encontró la vista VerDetalleVenta.",
                            "Error 208", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;

                    case 53:
                        MessageBox.Show("No se pudo conectar con el servidor SQL.",
                            "Error 53", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;

                    case 4060:
                        MessageBox.Show("No se pudo acceder a la base de datos.",
                            "Error 4060", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;

                    case -2:
                        MessageBox.Show("La operación tardó demasiado tiempo.",
                            "Error -2", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;

                    default:
                        MessageBox.Show("Ocurrió un error al cargar el detalle de la venta.",
                            "Error " + ex.Number, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrió un error inesperado al cargar el detalle.",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return dt;
        }


        // Carga los datos al formulario de editar la factura
        public static DataTable CargarFacturaPorId(int idFactura)
        {
            DataTable dt = new DataTable();

            try
            {
                using (SqlConnection conectar = Conexion.Conectar())
                {
                    string comando = @"SELECT * FROM VerFacturaEditar WHERE IdFactura = @IdFactura";

                    using (SqlDataAdapter adapter = new SqlDataAdapter(comando, conectar))
                    {
                        adapter.SelectCommand.Parameters.AddWithValue("@IdFactura", idFactura);

                        adapter.Fill(dt);
                    }
                }
            }
            catch (SqlException ex)
            {
                switch (ex.Number)
                {
                    case 208:
                        MessageBox.Show("No se encontró la vista VerFacturaEditar.",
                            "Error 208", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;

                    case 53:
                        MessageBox.Show("No se pudo conectar con el servidor SQL.",
                            "Error 53", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;

                    case 4060:
                        MessageBox.Show("No se pudo acceder a la base de datos.",
                            "Error 4060", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;

                    case -2:
                        MessageBox.Show("La operación tardó demasiado tiempo.",
                            "Error -2", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;

                    default:
                        MessageBox.Show("Ocurrió un error al cargar la factura.",
                            "Error " + ex.Number, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrió un error inesperado al cargar la factura.",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return dt;
        }


        public static void ActualizarFactura(
            int idFactura,
            DateTime fechaVencimiento,
            decimal? descuento,
            string observaciones)
        {
            string sql = @"UPDATE Factura SET FechaVencimiento = @FechaVencimiento, Descuento = @Descuento, Observaciones = @Observaciones  WHERE IdFactura = @IdFactura";

            try
            {
                using (SqlConnection cn = Conexion.Conectar())
                using (SqlCommand cmd = new SqlCommand(sql, cn))
                {
                    cmd.Parameters.AddWithValue("@FechaVencimiento", fechaVencimiento);

                    cmd.Parameters.AddWithValue("@Descuento", (object)descuento ?? DBNull.Value);

                    cmd.Parameters.AddWithValue("@Observaciones", string.IsNullOrWhiteSpace(observaciones) ? (object)DBNull.Value : observaciones);

                    cmd.Parameters.AddWithValue("@IdFactura", idFactura);

                    int filasAfectadas = cmd.ExecuteNonQuery();

                    if (filasAfectadas > 0)
                    {
                        MessageBox.Show("Factura actualizada correctamente.",
                            "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("No se encontró la factura seleccionada.",
                            "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            catch (SqlException ex)
            {
                switch (ex.Number)
                {
                    case 547:
                        MessageBox.Show("No se puede actualizar la factura por datos relacionados.",
                            "Error 547", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;

                    case 515:
                        MessageBox.Show("Faltan datos obligatorios para actualizar la factura.",
                            "Error 515", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;

                    case 8115:
                        MessageBox.Show("El descuento excede el límite permitido.",
                            "Error 8115", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;

                    case 245:
                        MessageBox.Show("Uno de los datos ingresados tiene un formato incorrecto.",
                            "Error 245", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;

                    case 53:
                        MessageBox.Show("No se pudo conectar con el servidor SQL.",
                            "Error 53", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;

                    case 4060:
                        MessageBox.Show("No se pudo acceder a la base de datos.",
                            "Error 4060", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;

                    case -2:
                        MessageBox.Show("La operación tardó demasiado tiempo.",
                            "Error -2", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;

                    default:
                        MessageBox.Show("Ocurrió un error al actualizar la factura.",
                            "Error " + ex.Number, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrió un error inesperado al actualizar la factura.",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public static DataTable BuscarFacturas(string texto)
        {
            DataTable dt = new DataTable();

            using (SqlConnection conexion = Conexion.Conectar())
            {
                try
                {
                    string consulta = @"SELECT * FROM VerFacturas WHERE CAST(IdFactura AS VARCHAR) LIKE '%' + @Texto + '%'
                    OR Cliente LIKE '%' + @Texto + '%' OR [Método de Pago] LIKE '%' + @Texto + '%' ORDER BY IdFactura DESC";

                    using (SqlCommand comando = new SqlCommand(consulta, conexion))
                    {
                        comando.Parameters.AddWithValue("@Texto", texto ?? "");

                        using (SqlDataAdapter adaptador = new SqlDataAdapter(comando))
                        {
                            adaptador.Fill(dt);
                        }
                    }
                }
                catch (SqlException ex)
                {
                    switch (ex.Number)
                    {
                        case 208:
                            MessageBox.Show("No se encontró la vista VerFacturas.",
                                "Error 208", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;

                        case 53:
                            MessageBox.Show("No se pudo conectar con el servidor SQL.",
                                "Error 53", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;

                        case 4060:
                            MessageBox.Show("No se pudo acceder a la base de datos.",
                                "Error 4060", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;

                        case -2:
                            MessageBox.Show("La operación tardó demasiado tiempo.",
                                "Error -2", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;

                        default:
                            MessageBox.Show("Ocurrió un error al buscar las facturas.",
                                "Error " + ex.Number, MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                    }
                }
                catch (System.Exception ex)
                {
                    MessageBox.Show("Ocurrió un error inesperado al buscar las facturas.",
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            return dt;
        }

    }

}
