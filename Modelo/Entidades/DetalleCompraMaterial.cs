using Modelo.Conexión_DB;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Modelo.Entidades
{
    public class DetalleCompraMaterial
    {
        private int IdDetalleCompraMaterial;
        private int IdCompra;
        private int IdMaterial;
        private int Cantidad;
        private decimal PrecioUnitario;

        public DetalleCompraMaterial() { }

        public DetalleCompraMaterial(int idDetalleCompraMaterial, int idCompra, int idMaterial, int cantidad, decimal precioUnitario)
        {
            IdDetalleCompraMaterial = idDetalleCompraMaterial;
            IdCompra = idCompra;
            IdMaterial = idMaterial;
            Cantidad = cantidad;
            PrecioUnitario = precioUnitario;
        }

        public int IdDetalleCompraMaterial1 { get => IdDetalleCompraMaterial; set => IdDetalleCompraMaterial = value; }
        public int IdCompra1 { get => IdCompra; set => IdCompra = value; }
        public int IdMaterial1 { get => IdMaterial; set => IdMaterial = value; }
        public int Cantidad1 { get => Cantidad; set => Cantidad = value; }
        public decimal PrecioUnitario1 { get => PrecioUnitario; set => PrecioUnitario = value; }

        // CARGAR DETALLES POR COMPRA

        public static DataTable CargarDetallesPorCompra(int idCompra)
        {
            string comandoSQL = @"SELECT d.IdDetalleCompraMaterial, d.IdCompra, d.IdMaterial, m.NombreDelMaterial AS Material, d.Cantidad, d.PrecioUnitario
                                  FROM DetalleCompraMaterial d
                                  INNER JOIN Material m ON d.IdMaterial = m.IdMaterial
                                  WHERE d.IdCompra = @IdCompra;";

            using (SqlConnection conexion = Conexion.Conectar())
            using (SqlCommand comando = new SqlCommand(comandoSQL, conexion))
            {
                comando.Parameters.AddWithValue("@IdCompra", idCompra);
                SqlDataAdapter adapter = new SqlDataAdapter(comando);
                DataTable dt = new DataTable();

                try
                {
                    adapter.Fill(dt);
                    return dt;
                }
                catch (SqlException ex)
                {
                    switch (ex.Number)
                    {
                        case 208:
                            MessageBox.Show("No se encontró una de las tablas utilizadas.", "Error 208", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                        case 547:
                            MessageBox.Show("No se puede cargar el detalle por datos relacionados.", "Error 547", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                        case 53:
                            MessageBox.Show("No se pudo conectar con el servidor SQL.", "Error 53", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                        case 4060:
                            MessageBox.Show("No se pudo acceder a la base de datos.", "Error 4060", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                        case -2:
                            MessageBox.Show("La operación tardó demasiado tiempo.", "Error -2", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                        default:
                            MessageBox.Show("Ocurrió un error al cargar el detalle de compra.", "Error " + ex.Number, MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                    }

                    return new DataTable();
                }
                catch (System.Exception ex)
                {
                    MessageBox.Show("Ocurrió un error inesperado al cargar el detalle.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return new DataTable();
                }
            }
        }

        // INSERTAR DETALLE DE COMPRA

        public bool InsertarDetalleCompra()
        {
            using (SqlConnection conexion = Conexion.Conectar())
            {
                SqlTransaction transaccion = conexion.BeginTransaction();

                try
                {
                    string comandoSQL = @"INSERT INTO DetalleCompraMaterial(IdCompra, IdMaterial, Cantidad, PrecioUnitario)
                                          VALUES (@IdCompra, @IdMaterial, @Cantidad, @PrecioUnitario);";

                    using (SqlCommand comandoObjeto = new SqlCommand(comandoSQL, conexion, transaccion))
                    {
                        comandoObjeto.Parameters.AddWithValue("@IdCompra", IdCompra1);
                        comandoObjeto.Parameters.AddWithValue("@IdMaterial", IdMaterial1);
                        comandoObjeto.Parameters.AddWithValue("@Cantidad", Cantidad1);

                        SqlParameter parametro = comandoObjeto.Parameters.Add("@PrecioUnitario", SqlDbType.Decimal);
                        parametro.Precision = 10;
                        parametro.Scale = 2;
                        parametro.Value = PrecioUnitario1;

                        comandoObjeto.ExecuteNonQuery();
                    }

                    string updateSQL = "UPDATE Material SET Stock = Stock + @Cantidad WHERE IdMaterial = @IdMaterial;";

                    using (SqlCommand cmdUpdate = new SqlCommand(updateSQL, conexion, transaccion))
                    {
                        cmdUpdate.Parameters.AddWithValue("@Cantidad", Cantidad1);
                        cmdUpdate.Parameters.AddWithValue("@IdMaterial", IdMaterial1);
                        cmdUpdate.ExecuteNonQuery();
                    }

                    transaccion.Commit();
                    return true;
                }
                catch (SqlException ex)
                {
                    try { transaccion.Rollback(); } catch { }

                    switch (ex.Number)
                    {
                        case 2627:
                        case 2601:
                            MessageBox.Show("El detalle de compra ya existe.", "Error " + ex.Number, MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                        case 547:
                            MessageBox.Show("La compra o el material seleccionado no existe.", "Error 547", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                        case 515:
                            MessageBox.Show("Faltan datos obligatorios para guardar el detalle.", "Error 515", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                        case 8115:
                            MessageBox.Show("El precio o cantidad excede el límite permitido.", "Error 8115", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                        case 245:
                            MessageBox.Show("Uno de los datos ingresados tiene un formato incorrecto.", "Error 245", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                        case 53:
                            MessageBox.Show("No se pudo conectar con el servidor SQL.", "Error 53", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                        case 4060:
                            MessageBox.Show("No se pudo acceder a la base de datos.", "Error 4060", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                        case -2:
                            MessageBox.Show("La operación tardó demasiado tiempo.", "Error -2", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                        default:
                            MessageBox.Show("Ocurrió un error al guardar el detalle de compra.", "Error " + ex.Number, MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                    }

                    return false;
                }
                catch (System.Exception ex)
                {
                    try { transaccion.Rollback(); } catch { }

                    MessageBox.Show("Ocurrió un error inesperado al guardar el detalle.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
        }

        // ACTUALIZAR DETALLE DE COMPRA

        public bool ActualizarDetalleCompra(int idMaterialAnterior, int cantidadAnterior)
        {
            using (SqlConnection conexion = Conexion.Conectar())
            {
                SqlTransaction transaccion = conexion.BeginTransaction();

                try
                {
                    // Devuelve al stock la cantidad anterior

                    string devolverStock = @"UPDATE Material SET Stock = Stock - @CantidadAnterior WHERE IdMaterial = @IdMaterialAnterior;";

                    using (SqlCommand cmd = new SqlCommand(devolverStock, conexion, transaccion))
                    {
                        cmd.Parameters.AddWithValue("@CantidadAnterior", cantidadAnterior);
                        cmd.Parameters.AddWithValue("@IdMaterialAnterior", idMaterialAnterior);
                        cmd.ExecuteNonQuery();
                    }

                    // Agrega al stock la nueva cantidad

                    string agregarStock = @"UPDATE Material SET Stock = Stock + @CantidadNueva WHERE IdMaterial = @IdMaterialNuevo;";

                    using (SqlCommand cmd = new SqlCommand(agregarStock, conexion, transaccion))
                    {
                        cmd.Parameters.AddWithValue("@CantidadNueva", Cantidad1);
                        cmd.Parameters.AddWithValue("@IdMaterialNuevo", IdMaterial1);
                        cmd.ExecuteNonQuery();
                    }

                    // Actualiza el detalle de la compra

                    string comandoSQL = @"UPDATE DetalleCompraMaterial SET IdMaterial = @IdMaterial, Cantidad = @Cantidad, PrecioUnitario = @PrecioUnitario WHERE IdDetalleCompraMaterial = @IdDetalleCompraMaterial;";

                    using (SqlCommand comando = new SqlCommand(comandoSQL, conexion, transaccion))
                    {
                        comando.Parameters.AddWithValue("@IdDetalleCompraMaterial", IdDetalleCompraMaterial1);
                        comando.Parameters.AddWithValue("@IdMaterial", IdMaterial1);
                        comando.Parameters.AddWithValue("@Cantidad", Cantidad1);

                        SqlParameter parametro = comando.Parameters.Add("@PrecioUnitario", SqlDbType.Decimal);
                        parametro.Precision = 10;
                        parametro.Scale = 2;
                        parametro.Value = PrecioUnitario1;

                        comando.ExecuteNonQuery();
                    }

                    transaccion.Commit();
                    return true;
                }
                catch (SqlException ex)
                {
                    try { transaccion.Rollback(); }
                    catch
                    { }

                    switch (ex.Number)
                    {
                        case 547:
                            MessageBox.Show("El material o detalle seleccionado no existe.", "Error 547", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                        case 515:
                            MessageBox.Show("Faltan datos obligatorios para actualizar el detalle.", "Error 515", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                        case 8115:
                            MessageBox.Show("El precio o cantidad excede el límite permitido.", "Error 8115", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                        case 245:
                            MessageBox.Show("Uno de los datos ingresados tiene un formato incorrecto.", "Error 245", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                        case 53:
                            MessageBox.Show("No se pudo conectar con el servidor SQL.", "Error 53", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                        case 4060:
                            MessageBox.Show("No se pudo acceder a la base de datos.", "Error 4060", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                        case -2:
                            MessageBox.Show("La operación tardó demasiado tiempo.", "Error -2", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                        default:
                            MessageBox.Show("Ocurrió un error al actualizar el detalle.", "Error " + ex.Number, MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                    }

                    return false;
                }
                catch (System.Exception ex)
                {
                    try { transaccion.Rollback(); } catch { }

                    MessageBox.Show("Ocurrió un error inesperado al actualizar el detalle.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
        }
    }
}