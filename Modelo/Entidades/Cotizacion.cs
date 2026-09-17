using Modelo.Conexión_DB;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Modelo.Entidades
{
    public class DbCotizacion
    {
        private int IdCotizacion;
        private DateTime Fecha;
        private int IdCliente;
        private string CondicionPago;
        private string CondicionEntrega;
        private decimal Total;
        private string Estado;

        public DbCotizacion(int idCotizacion, DateTime fecha, int idCliente, string condicionesPago, string condicionesEntrega, decimal total, string estado)
        {
            IdCotizacion = idCotizacion;
            Fecha = fecha;
            IdCliente = idCliente;
            CondicionPago = condicionesPago;
            CondicionEntrega = condicionesEntrega;
            Total = total;
            Estado = estado;
        }

        public DbCotizacion()
        {

        }

        public int IdCotizacion1 { get => IdCotizacion; set => IdCotizacion = value; }
        public DateTime Fecha1 { get => Fecha; set => Fecha = value; }
        public int IdCliente1 { get => IdCliente; set => IdCliente = value; }
        public string CondicionesPago1 { get => CondicionPago; set => CondicionPago = value; }
        public string CondicionesEntrega1 { get => CondicionEntrega; set => CondicionEntrega = value; }
        public decimal Total1 { get => Total; set => Total = value; }
        public string Estado1 { get => Estado; set => Estado = value; }

        public static DataTable CargarCotizacion()
        {
            try
            {
                SqlConnection conectar = Conexion.Conectar();

                string comando = "SELECT * FROM VerCotizaciones;";
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
                        MessageBox.Show("No se encontró la vista VerCotizaciones.",
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
                        MessageBox.Show("Ocurrió un error al cargar las cotizaciones.",
                            "Error " + ex.Number, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                }

                return new DataTable();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrió un error inesperado al cargar las cotizaciones.",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return new DataTable();
            }
        }

        public int InsertarCotizacion()
        {
            string comandoSQL = @"INSERT INTO Cotizacion(Fecha,IdCliente,CondicionPago,CondicionEntrega,Total,Estado,IdUsuario) VALUES(@Fecha,@IdCliente,@CondicionPago,@CondicionEntrega,@Total,@Estado, 1); SELECT CAST(SCOPE_IDENTITY() AS INT);";

            using (SqlConnection conexion = Conexion.Conectar())
            {
                using (SqlCommand comandoObjeto =
                    new SqlCommand(comandoSQL, conexion))
                {
                    comandoObjeto.Parameters.AddWithValue("@Fecha", Fecha);

                    comandoObjeto.Parameters.AddWithValue("@IdCliente", IdCliente);

                    comandoObjeto.Parameters.AddWithValue("@CondicionPago", CondicionPago);

                    comandoObjeto.Parameters.AddWithValue("@CondicionEntrega", CondicionEntrega);

                    SqlParameter parametroTotal = comandoObjeto.Parameters.Add("@Total", SqlDbType.Decimal);
                    parametroTotal.Precision = 10;
                    parametroTotal.Scale = 2;
                    parametroTotal.Value = Total;

                    comandoObjeto.Parameters.AddWithValue("@Estado", Estado);

                    try
                    {
                        int idCotizacion = Convert.ToInt32(comandoObjeto.ExecuteScalar());

                        return idCotizacion;
                    }
                    catch (SqlException ex)
                    {
                        switch (ex.Number)
                        {
                            case 2627:
                            case 2601:
                                MessageBox.Show("La cotización ya existe.",
                                    "Error " + ex.Number, MessageBoxButtons.OK, MessageBoxIcon.Error);
                                break;

                            case 547:
                                MessageBox.Show("El cliente o usuario seleccionado no existe.",
                                    "Error 547", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                break;

                            case 515:
                                MessageBox.Show("Faltan datos obligatorios para guardar la cotización.",
                                    "Error 515", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                break;

                            case 8115:
                                MessageBox.Show("El total de la cotización excede el límite permitido.",
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
                                MessageBox.Show("Ocurrió un error al guardar la cotización.",
                                    "Error " + ex.Number, MessageBoxButtons.OK, MessageBoxIcon.Error);
                                break;
                        }

                        return 0;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Ocurrió un error inesperado al guardar la cotización.",
                            "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                        return 0;
                    }
                }
            }
        }


        public bool EliminarCotizacion()
        {
            using (SqlConnection conexion = Conexion.Conectar())
            {
                SqlTransaction transaccion = conexion.BeginTransaction();

                try
                {
                    SqlCommand cmdDetalle = new SqlCommand("DELETE FROM Productos_Cotizacion WHERE IdCotizacion = @IdCotizacion", conexion, transaccion);
                    cmdDetalle.Parameters.AddWithValue("@IdCotizacion", IdCotizacion1);
                    cmdDetalle.ExecuteNonQuery();

                    SqlCommand cmdCot = new SqlCommand("DELETE FROM Cotizacion WHERE IdCotizacion = @IdCotizacion", conexion, transaccion);
                    cmdCot.Parameters.AddWithValue("@IdCotizacion", IdCotizacion1);
                    int filasAfectadas = cmdCot.ExecuteNonQuery();

                    transaccion.Commit();
                    return filasAfectadas > 0;
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
                        case 547:
                            MessageBox.Show("No se puede eliminar la cotización porque tiene datos relacionados.",
                                "Error 547", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                            MessageBox.Show("Ocurrió un error al eliminar la cotización.",
                                "Error " + ex.Number, MessageBoxButtons.OK, MessageBoxIcon.Error);
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

                    MessageBox.Show("Ocurrió un error inesperado al eliminar la cotización.",
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    return false;
                }
            }
        }

        public bool ActualizarEstado(string nuevoEstado)
        {
            string comandoSQL = "UPDATE Cotizacion SET Estado = @Estado WHERE IdCotizacion = @IdCotizacion;";

            using (SqlConnection conexion = Conexion.Conectar())
            {
                using (SqlCommand comandoObjeto = new SqlCommand(comandoSQL, conexion))
                {
                    comandoObjeto.Parameters.AddWithValue("@Estado", nuevoEstado);
                    comandoObjeto.Parameters.AddWithValue("@IdCotizacion", IdCotizacion1);

                    try
                    {
                        return comandoObjeto.ExecuteNonQuery() > 0;
                    }
                    catch (SqlException ex)
                    {
                        switch (ex.Number)
                        {
                            case 547:
                                MessageBox.Show("No se puede actualizar el estado de la cotización.",
                                    "Error 547", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                break;

                            case 515:
                                MessageBox.Show("El estado de la cotización es obligatorio.",
                                    "Error 515", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                                MessageBox.Show("Ocurrió un error al actualizar el estado.",
                                    "Error " + ex.Number, MessageBoxButtons.OK, MessageBoxIcon.Error);
                                break;
                        }

                        return false;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Ocurrió un error inesperado al actualizar el estado.",
                            "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                        return false;
                    }
                }
            }
        }

        public static DataTable BuscarCotizacion(string buscar)
        {
            try
            {
                SqlConnection conectar = Conexion.Conectar();

                string comando = @"SELECT * FROM VerCotizaciones WHERE Cliente LIKE '%' + @Buscar + '%' ORDER BY IdCotizacion";

                SqlDataAdapter adapter = new SqlDataAdapter(comando, conectar);

                adapter.SelectCommand.Parameters.AddWithValue("@Buscar", buscar ?? "");

                DataTable dt = new DataTable();

                adapter.Fill(dt);

                return dt;
            }
            catch (SqlException ex)
            {
                switch (ex.Number)
                {
                    case 208:
                        MessageBox.Show("No se encontró la vista VerCotizaciones.",
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
                        MessageBox.Show("Ocurrió un error al buscar la cotización.",
                            "Error " + ex.Number, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                }

                return new DataTable();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrió un error inesperado al buscar la cotización.",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return new DataTable();
            }
        }
    }
}
