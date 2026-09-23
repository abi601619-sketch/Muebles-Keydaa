using Modelo.Conexión_DB;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Modelo.Entidades
{
    public class DbPedidos
    {
        private int idPedido;
        private DateTime FechaDePedido;
        private DateTime FechaDeEntrega;
        private string Estado;
        private int idCotizacion;

        public DbPedidos(int idPedido, DateTime fechaDePedido, DateTime fechaDeEntrega, string estado, int idCotizacion)
        {
            this.idPedido = idPedido;
            FechaDePedido = fechaDePedido;
            FechaDeEntrega = fechaDeEntrega;
            Estado = estado;
            this.idCotizacion = idCotizacion;
        }

        public int IdPedido { get => idPedido; set => idPedido = value; }
        public DateTime FechaDePedido1 { get => FechaDePedido; set => FechaDePedido = value; }
        public DateTime FechaDeEntrega1 { get => FechaDeEntrega; set => FechaDeEntrega = value; }
        public string Estado1 { get => Estado; set => Estado = value; }
        public int IdCotizacion { get => idCotizacion; set => idCotizacion = value; }

        public static DataTable CargarRegistroPedidos()
        {
            try
            {
                using (SqlConnection conexion = Conexion.Conectar())
                {
                    string comandoSQL = "SELECT * FROM VerPedido;";

                    using (SqlDataAdapter adapter = new SqlDataAdapter(comandoSQL, conexion))
                    {
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        return dt;
                    }
                }
            }
            catch (SqlException ex)
            {
                switch (ex.Number)
                {
                    case 53:
                        MessageBox.Show("No se pudo establecer conexión con el servidor.", "ERR-SQL-001", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;

                    case 4060:
                        MessageBox.Show("No se pudo acceder a la base de datos.", "ERR-SQL-002", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;

                    case -2:
                        MessageBox.Show("La consulta tardó demasiado tiempo.", "ERR-SQL-003", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;

                    case 208:
                        MessageBox.Show("La vista VerPedido no existe en la base de datos.", "ERR-SQL-004", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;

                    default:
                        MessageBox.Show("Ocurrió un error al cargar los pedidos.\n" + ex.Message, "ERR-SQL-999", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                }

                return new DataTable();
            }
        }

        public static DataTable CargarPedidosRecientes()
        {
            try
            {
                using (SqlConnection conexion = Conexion.Conectar())
                {
                    string comandoSQL = "SELECT * FROM Pedido WHERE FechaDePedido >= '2026-07-15';";

                    using (SqlDataAdapter adapter = new SqlDataAdapter(comandoSQL, conexion))
                    {
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        return dt;
                    }
                }
            }
            catch (SqlException ex)
            {
                switch (ex.Number)
                {
                    case 53:
                        MessageBox.Show("No se pudo establecer conexión con el servidor.", "ERR-SQL-001", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;

                    case 4060:
                        MessageBox.Show("No se pudo acceder a la base de datos.", "ERR-SQL-002", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;

                    case -2:
                        MessageBox.Show("La consulta tardó demasiado tiempo.", "ERR-SQL-003", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;

                    case 208:
                        MessageBox.Show("La tabla Pedido no existe en la base de datos.", "ERR-SQL-004", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;

                    default:
                        MessageBox.Show("Ocurrió un error al cargar los pedidos recientes.\n" + ex.Message, "ERR-SQL-999", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                }

                return new DataTable();
            }
        }

        public static bool ConvertirCotizacionAPedido(int idCotizacion, DateTime fechaEntrega)
        {
            using (SqlConnection conexion = Conexion.Conectar())
            using (SqlTransaction transaccion = conexion.BeginTransaction())
            {
                try
                {
                    string queryPedido = "INSERT INTO Pedido (FechaDePedido, FechaDeEntrega, Estado, IdCotizacion) VALUES (@FechaPedido, @FechaEntrega, 'En proceso', @IdCotizacion); SELECT SCOPE_IDENTITY();";

                    int nuevoIdPedido;

                    using (SqlCommand cmdPedido = new SqlCommand(queryPedido, conexion, transaccion))
                    {
                        cmdPedido.Parameters.AddWithValue("@FechaPedido", DateTime.Now);
                        cmdPedido.Parameters.AddWithValue("@FechaEntrega", fechaEntrega);
                        cmdPedido.Parameters.AddWithValue("@IdCotizacion", idCotizacion);

                        nuevoIdPedido = Convert.ToInt32(cmdPedido.ExecuteScalar());
                    }

                    string queryDetalle = "INSERT INTO DetallePedido (IdPedido, Mueble, Cantidad, Medidas) SELECT @IdPedido, DescripcionMueble, Cantidad, CAST(Largo AS VARCHAR) + 'x' + CAST(Ancho AS VARCHAR) + 'x' + CAST(Alto AS VARCHAR) FROM Productos_Cotizacion WHERE IdCotizacion = @IdCotizacion;";

                    using (SqlCommand cmdDetalle = new SqlCommand(queryDetalle, conexion, transaccion))
                    {
                        cmdDetalle.Parameters.AddWithValue("@IdPedido", nuevoIdPedido);
                        cmdDetalle.Parameters.AddWithValue("@IdCotizacion", idCotizacion);
                        cmdDetalle.ExecuteNonQuery();
                    }

                    string queryProduccion = "INSERT INTO Produccion (IdPedido, Progreso) VALUES (@IdPedido, 0);";

                    using (SqlCommand cmdProduccion = new SqlCommand(queryProduccion, conexion, transaccion))
                    {
                        cmdProduccion.Parameters.AddWithValue("@IdPedido", nuevoIdPedido);
                        cmdProduccion.ExecuteNonQuery();
                    }

                    transaccion.Commit();
                    return true;
                }
                catch (SqlException ex)
                {
                    try
                    {
                        transaccion.Rollback();
                    }
                    catch
                    {
                    }

                    switch (ex.Number)
                    {
                        case 53:
                            MessageBox.Show("No se pudo establecer conexión con el servidor.", "ERR-SQL-001", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;

                        case 4060:
                            MessageBox.Show("No se pudo acceder a la base de datos.", "ERR-SQL-002", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;

                        case -2:
                            MessageBox.Show("La conversión tardó demasiado tiempo.", "ERR-SQL-003", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;

                        case 208:
                            MessageBox.Show("Una de las tablas utilizadas no existe en la base de datos.", "ERR-SQL-004", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;

                        case 2627:
                            MessageBox.Show("El pedido o alguno de sus datos ya existe. Verifique la cotización.", "ERR-SQL-005", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            break;

                        case 2601:
                            MessageBox.Show("El pedido o alguno de sus datos ya existe. Verifique la cotización.", "ERR-SQL-006", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            break;

                        case 547:
                            MessageBox.Show("No se puede convertir la cotización. Verifique los datos relacionados.", "ERR-SQL-007", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            break;

                        case 515:
                            MessageBox.Show("Faltan datos obligatorios para crear el pedido.", "ERR-SQL-008", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            break;

                        case 245:
                            MessageBox.Show("Uno de los datos de la cotización tiene un formato incorrecto.", "ERR-SQL-009", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            break;

                        case 8115:
                            MessageBox.Show("Uno de los valores ingresados es demasiado grande.", "ERR-SQL-010", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            break;

                        case 8152:
                            MessageBox.Show("Uno de los datos es demasiado largo para la columna correspondiente.", "ERR-SQL-011", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            break;

                        default:
                            MessageBox.Show("Ocurrió un error al convertir la cotización.\n" + ex.Message, "ERR-SQL-999", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                    }

                    return false;
                }
                catch (Exception ex)
                {
                    try
                    {
                        transaccion.Rollback();
                    }
                    catch
                    {
                    }

                    MessageBox.Show("Ocurrió un error inesperado.\n" + ex.Message, "ERR-SQL-999", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
        }

        public static bool ActualizarPedidoEstado(int idPedido, string nuevoEstado)
        {
            using (SqlConnection conexion = Conexion.Conectar())
            {
                string query = "UPDATE Pedido SET Estado = @Estado WHERE IdPedido = @IdPedido AND (@Estado = 'Cancelado' OR EXISTS (SELECT 1 FROM DetallePedido WHERE IdPedido = @IdPedido));";

                using (SqlCommand cmd = new SqlCommand(query, conexion))
                {
                    cmd.Parameters.AddWithValue("@Estado", nuevoEstado);
                    cmd.Parameters.AddWithValue("@IdPedido", idPedido);

                    try
                    {
                        int filasAfectadas = cmd.ExecuteNonQuery();

                        if (filasAfectadas > 0)
                        {
                            return true;
                        }

                        MessageBox.Show("No se pudo actualizar el estado del pedido. Verifique que el pedido exista y tenga detalles.", "Pedido no actualizado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return false;
                    }
                    catch (SqlException ex)
                    {
                        switch (ex.Number)
                        {
                            case 53:
                                MessageBox.Show("No se pudo establecer conexión con el servidor.", "ERR-SQL-001", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                break;

                            case 4060:
                                MessageBox.Show("No se pudo acceder a la base de datos.", "ERR-SQL-002", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                break;

                            case -2:
                                MessageBox.Show("La actualización tardó demasiado tiempo.", "ERR-SQL-003", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                break;

                            case 208:
                                MessageBox.Show("La tabla Pedido o DetallePedido no existe.", "ERR-SQL-004", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                break;

                            case 547:
                                MessageBox.Show("No se puede actualizar el estado del pedido. Verifique los datos relacionados.", "ERR-SQL-007", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                break;

                            case 515:
                                MessageBox.Show("El estado del pedido es obligatorio.", "ERR-SQL-008", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                break;

                            default:
                                MessageBox.Show("Ocurrió un error al actualizar el estado.\n" + ex.Message, "ERR-SQL-999", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                break;
                        }

                        return false;
                    }
                }
            }
        }

        public static bool ActualizarPedidoFecha(int idPedido, DateTime fechaEntrega)
        {
            using (SqlConnection conexion = Conexion.Conectar())
            {
                string query = "UPDATE Pedido SET FechaDeEntrega = @FechaEntrega WHERE IdPedido = @IdPedido;";

                using (SqlCommand cmd = new SqlCommand(query, conexion))
                {
                    cmd.Parameters.AddWithValue("@FechaEntrega", fechaEntrega);
                    cmd.Parameters.AddWithValue("@IdPedido", idPedido);

                    try
                    {
                        int filasAfectadas = cmd.ExecuteNonQuery();

                        if (filasAfectadas > 0)
                        {
                            return true;
                        }

                        MessageBox.Show("No se encontró el pedido que desea actualizar.", "Pedido no encontrado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return false;
                    }
                    catch (SqlException ex)
                    {
                        switch (ex.Number)
                        {
                            case 53:
                                MessageBox.Show("No se pudo establecer conexión con el servidor.", "ERR-SQL-001", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                break;

                            case 4060:
                                MessageBox.Show("No se pudo acceder a la base de datos.", "ERR-SQL-002", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                break;

                            case -2:
                                MessageBox.Show("La actualización tardó demasiado tiempo.", "ERR-SQL-003", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                break;

                            case 208:
                                MessageBox.Show("La tabla Pedido no existe en la base de datos.", "ERR-SQL-004", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                break;

                            case 547:
                                MessageBox.Show("No se puede actualizar la fecha del pedido. Verifique los datos relacionados.", "ERR-SQL-007", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                break;

                            case 515:
                                MessageBox.Show("La fecha de entrega es obligatoria.", "ERR-SQL-008", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                break;

                            default:
                                MessageBox.Show("Ocurrió un error al actualizar la fecha.\n" + ex.Message, "ERR-SQL-999", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                break;
                        }

                        return false;
                    }
                }
            }
        }

        public static DataTable BuscarPedido(string termino)
        {
            try
            {
                using (SqlConnection conexion = Conexion.Conectar())
                {
                    string comandoSQL = "SELECT * FROM VerPedido WHERE CAST(IdPedido AS VARCHAR) LIKE @buscar OR Cliente LIKE @buscar;";

                    using (SqlDataAdapter adapter = new SqlDataAdapter(comandoSQL, conexion))
                    {
                        adapter.SelectCommand.Parameters.AddWithValue("@buscar", "%" + (termino ?? "") + "%");

                        DataTable dt = new DataTable();
                        adapter.Fill(dt);

                        return dt;
                    }
                }
            }
            catch (SqlException ex)
            {
                switch (ex.Number)
                {
                    case 53:
                        MessageBox.Show("No se pudo establecer conexión con el servidor.", "ERR-SQL-001", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;

                    case 4060:
                        MessageBox.Show("No se pudo acceder a la base de datos.", "ERR-SQL-002", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;

                    case -2:
                        MessageBox.Show("La búsqueda tardó demasiado tiempo.", "ERR-SQL-003", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;

                    case 208:
                        MessageBox.Show("La vista VerPedido no existe en la base de datos.", "ERR-SQL-004", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;

                    default:
                        MessageBox.Show("Ocurrió un error al buscar los pedidos.\n" + ex.Message, "ERR-SQL-999", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                }

                return new DataTable();
            }
        }
    }
}