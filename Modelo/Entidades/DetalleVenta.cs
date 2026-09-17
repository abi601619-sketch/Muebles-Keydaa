using Modelo.Conexión_DB;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Modelo.Entidades
{
    public class DetalleVenta

    {
        private int IdDetalleVenta;
        private int IdVenta;
        private string ProductoVendido;
        private int Cantidad;
        private decimal PrecioUnitario;

        public int IdDetalleVenta1 { get => IdDetalleVenta; set => IdDetalleVenta = value; }
        public int IdVenta1 { get => IdVenta; set => IdVenta = value; }
        public string ProductoVendido1 { get => ProductoVendido; set => ProductoVendido = value; }
        public int Cantidad1 { get => Cantidad; set => Cantidad = value; }
        public decimal PrecioUnitario1 { get => PrecioUnitario; set => PrecioUnitario = value; }

        public DetalleVenta(int idVenta, string productoVendido, int cantidad, decimal precioUnitario)
        {
            IdVenta1 = idVenta;
            ProductoVendido1 = productoVendido;
            Cantidad1 = cantidad;
            PrecioUnitario1 = precioUnitario;
        }

        public DetalleVenta()
        {

        }

        public static DataTable CargarDetalleVenta(int idVenta)
        {
            try
            {
                SqlConnection conectar = Conexion.Conectar();

                string comando = @"
                SELECT *
                FROM VerDetalleVenta
                WHERE IdVenta = @IdVenta;";

                SqlDataAdapter adapter =
                    new SqlDataAdapter(comando, conectar);

                adapter.SelectCommand.Parameters.AddWithValue(
                    "@IdVenta",
                    idVenta
                );

                DataTable dt = new DataTable();

                adapter.Fill(dt);

                return dt;
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
                        MessageBox.Show("Ocurrió un error al cargar el detalle de venta.",
                            "Error " + ex.Number, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                }

                return new DataTable();
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("Ocurrió un error inesperado al cargar el detalle.",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return new DataTable();
            }
        }

        public bool InsertarDetalleVenta()
        {
            string comandoSQL = "INSERT INTO DetalleVenta(IdVenta, ProductoVendido, Cantidad, PrecioUnitario)" +
                " VALUES (@IdVenta, @ProductoVendido , @Cantidad, @PrecioUnitario);";

            using (SqlConnection conexion = Conexion.Conectar())
            {
                using (SqlCommand comandoObjeto = new SqlCommand(comandoSQL, conexion))
                {
                    // Agregar parámetros
                    comandoObjeto.Parameters.AddWithValue("@IdVenta", IdVenta);
                    comandoObjeto.Parameters.AddWithValue("@ProductoVendido", ProductoVendido);
                    comandoObjeto.Parameters.AddWithValue("@Cantidad", Cantidad);

                    SqlParameter parametroPrecio = comandoObjeto.Parameters.Add("@PrecioUnitario", SqlDbType.Decimal);
                    parametroPrecio.Precision = 10;
                    parametroPrecio.Scale = 2;
                    parametroPrecio.Value = PrecioUnitario;

                    try
                    {
                        int filaAfectada = comandoObjeto.ExecuteNonQuery();

                        return filaAfectada > 0;
                    }
                    catch (SqlException ex)
                    {
                        switch (ex.Number)
                        {
                            case 2627:
                            case 2601:
                                MessageBox.Show("El detalle de venta ya existe en la base de datos.",
                                    "Error " + ex.Number, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                break;

                            case 547:
                                MessageBox.Show("La venta o el producto indicado no existe.",
                                    "Error 547", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                break;

                            case 515:
                                MessageBox.Show("Faltan datos obligatorios para guardar el detalle.",
                                    "Error 515", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                break;

                            case 8115:
                                MessageBox.Show("El precio o cantidad excede el límite permitido.",
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
                                MessageBox.Show("Ocurrió un error al guardar el detalle de venta.",
                                    "Error " + ex.Number, MessageBoxButtons.OK, MessageBoxIcon.Error);
                                break;
                        }

                        return false;
                    }
                    catch (System.Exception ex)
                    {
                        MessageBox.Show("Ocurrió un error inesperado al guardar el detalle.",
                            "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                        return false;
                    }
                }
            }
        }

        public bool ActualizarDetalleVenta()
        {
            string comandoSQL = @"UPDATE DetalleVenta SET ProductoVendido = @ProductoVendido, Cantidad = @Cantidad, PrecioUnitario = @PrecioUnitario 
                                  WHERE IdDetalleVenta = @IdDetalleVenta";

            using (SqlConnection conexion = Conexion.Conectar())
            {
                using (SqlCommand comandoObjeto = new SqlCommand(comandoSQL, conexion))
                {
                    comandoObjeto.Parameters.AddWithValue("@ProductoVendido", ProductoVendido1);
                    comandoObjeto.Parameters.AddWithValue("@Cantidad", Cantidad1);
                    comandoObjeto.Parameters.AddWithValue("@PrecioUnitario", PrecioUnitario1);
                    comandoObjeto.Parameters.AddWithValue("@IdDetalleVenta", IdDetalleVenta1);

                    try
                    {
                        return comandoObjeto.ExecuteNonQuery() > 0;
                    }
                    catch (SqlException ex)
                    {
                        switch (ex.Number)
                        {
                            case 547:
                                MessageBox.Show("No se puede actualizar el detalle por datos relacionados.",
                                    "Error 547", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                break;

                            case 515:
                                MessageBox.Show("Faltan datos obligatorios para actualizar el detalle.",
                                    "Error 515", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                break;

                            case 8115:
                                MessageBox.Show("El precio o cantidad excede el límite permitido.",
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
                                MessageBox.Show("Ocurrió un error al actualizar el detalle de venta.",
                                    "Error " + ex.Number, MessageBoxButtons.OK, MessageBoxIcon.Error);
                                break;
                        }

                        return false;
                    }
                    catch (System.Exception ex)
                    {
                        MessageBox.Show("Ocurrió un error inesperado al actualizar el detalle.",
                            "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                        return false;
                    }
                }
            }
        }

        public static bool EliminarDetalleVenta(int idDetalleVenta)
        {
            using (SqlConnection conexion = Conexion.Conectar())
            {
                string comandoSQL = "DELETE FROM DetalleVenta WHERE IdDetalleVenta = @IdDetalleVenta";

                using (SqlCommand comandoObjeto = new SqlCommand(comandoSQL, conexion))
                {
                    comandoObjeto.Parameters.AddWithValue("@IdDetalleVenta", idDetalleVenta);

                    try
                    {
                        int filasAfectadas = comandoObjeto.ExecuteNonQuery();

                        if (filasAfectadas == 0)
                        {
                            MessageBox.Show("No se encontró el detalle de venta seleccionado.",
                                "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                            return false;
                        }

                        return true;
                    }
                    catch (SqlException ex)
                    {
                        switch (ex.Number)
                        {
                            case 547:
                                MessageBox.Show("No se puede eliminar el detalle porque tiene datos relacionados.",
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
                                MessageBox.Show("Ocurrió un error al eliminar el detalle de venta.",
                                    "Error " + ex.Number, MessageBoxButtons.OK, MessageBoxIcon.Error);
                                break;
                        }

                        return false;
                    }
                    catch (System.Exception ex)
                    {
                        MessageBox.Show("Ocurrió un error inesperado al eliminar el detalle.",
                            "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                        return false;
                    }
                }
            }
        }
    }
}
