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

        public static DataTable CargarDetallesPorCompra(int idCompra)
        {
            string comandoSQL = @"SELECT d.IdDetalleCompraMaterial,d.IdCompra,d.IdMaterial,m.NombreDelMaterial AS Material,d.Cantidad, d.PrecioUnitario
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
                        case 53:
                            MessageBox.Show("No se puede conectar al servidor SQL.", "ERR-SQL-001", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;

                        case 4060:
                            MessageBox.Show("No se puede acceder a la base de datos.", "ERR-SQL-002", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;

                        case -2:
                            MessageBox.Show("Tiempo de espera agotado.", "ERR-SQL-003", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;

                        case 208:
                            MessageBox.Show("Tabla, vista o procedimiento no encontrado.", "ERR-SQL-004", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;

                        case 547:
                            MessageBox.Show("Violación de FK/CHECK.", "ERR-SQL-007", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;

                        default:
                            MessageBox.Show("Error inesperado de SQL.", "ERR-SQL-999", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                    }

                    return new DataTable();
                }
                catch (System.Exception)
                {
                    MessageBox.Show("Error inesperado de SQL.", "ERR-SQL-999", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return new DataTable();
                }
            }
        }

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

                    string updateSQL = @"UPDATE Material SET Stock = Stock + @Cantidad WHERE IdMaterial = @IdMaterial;";

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
                            MessageBox.Show("No se puede conectar al servidor SQL.", "ERR-SQL-001", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;

                        case 4060:
                            MessageBox.Show("No se puede acceder a la base de datos.", "ERR-SQL-002", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;

                        case -2:
                            MessageBox.Show("Tiempo de espera agotado.", "ERR-SQL-003", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;

                        case 208:
                            MessageBox.Show("Tabla, vista o procedimiento no encontrado.", "ERR-SQL-004", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;

                        case 2627:
                            MessageBox.Show("Registro duplicado por clave primaria/UNIQUE.", "ERR-SQL-005", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;

                        case 2601:
                            MessageBox.Show("Registro duplicado por índice UNIQUE.", "ERR-SQL-006", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;

                        case 547:
                            MessageBox.Show("Violación de FK/CHECK.", "ERR-SQL-007", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;

                        case 515:
                            MessageBox.Show("Campo NOT NULL sin valor.", "ERR-SQL-008", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;

                        case 245:
                            MessageBox.Show("Conversión o formato de datos incorrecto.", "ERR-SQL-009", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;

                        case 8115:
                            MessageBox.Show("Desbordamiento numérico.", "ERR-SQL-010", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;

                        case 8152:
                            MessageBox.Show("Datos demasiado largos para la columna.", "ERR-SQL-011", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;

                        default:
                            MessageBox.Show("Error inesperado de SQL.", "ERR-SQL-999", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                    }

                    return false;
                }
                catch (System.Exception)
                {
                    try
                    {
                        transaccion.Rollback();
                    }
                    catch
                    {
                    }

                    MessageBox.Show("Error inesperado de SQL.", "ERR-SQL-999", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    return false;
                }
            }
        }

        public bool ActualizarDetalleCompra(int idMaterialAnterior, int cantidadAnterior)
        {
            using (SqlConnection conexion = Conexion.Conectar())
            {
                SqlTransaction transaccion = conexion.BeginTransaction();

                try
                {
                    string devolverStock = @"UPDATE Material
                                             SET Stock = Stock - @CantidadAnterior
                                             WHERE IdMaterial = @IdMaterialAnterior;";

                    using (SqlCommand cmd = new SqlCommand(devolverStock, conexion, transaccion))
                    {
                        cmd.Parameters.AddWithValue("@CantidadAnterior", cantidadAnterior);
                        cmd.Parameters.AddWithValue("@IdMaterialAnterior", idMaterialAnterior);

                        cmd.ExecuteNonQuery();
                    }

                    string agregarStock = @"UPDATE Material
                                            SET Stock = Stock + @CantidadNueva
                                            WHERE IdMaterial = @IdMaterialNuevo;";

                    using (SqlCommand cmd = new SqlCommand(agregarStock, conexion, transaccion))
                    {
                        cmd.Parameters.AddWithValue("@CantidadNueva", Cantidad1);
                        cmd.Parameters.AddWithValue("@IdMaterialNuevo", IdMaterial1);

                        cmd.ExecuteNonQuery();
                    }

                    string comandoSQL = @"UPDATE DetalleCompraMaterial
                                          SET IdMaterial = @IdMaterial,
                                              Cantidad = @Cantidad,
                                              PrecioUnitario = @PrecioUnitario
                                          WHERE IdDetalleCompraMaterial = @IdDetalleCompraMaterial;";

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
                            MessageBox.Show("No se puede conectar al servidor SQL.", "ERR-SQL-001", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;

                        case 4060:
                            MessageBox.Show("No se puede acceder a la base de datos.", "ERR-SQL-002", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;

                        case -2:
                            MessageBox.Show("Tiempo de espera agotado.", "ERR-SQL-003", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;

                        case 208:
                            MessageBox.Show("Tabla, vista o procedimiento no encontrado.", "ERR-SQL-004", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;

                        case 2627:
                            MessageBox.Show("Registro duplicado por clave primaria/UNIQUE.", "ERR-SQL-005", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;

                        case 2601:
                            MessageBox.Show("Registro duplicado por índice UNIQUE.", "ERR-SQL-006", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;

                        case 547:
                            MessageBox.Show("Violación de FK/CHECK.", "ERR-SQL-007", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;

                        case 515:
                            MessageBox.Show("Campo NOT NULL sin valor.", "ERR-SQL-008", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;

                        case 245:
                            MessageBox.Show("Conversión o formato de datos incorrecto.", "ERR-SQL-009", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;

                        case 8115:
                            MessageBox.Show("Desbordamiento numérico.", "ERR-SQL-010", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;

                        case 8152:
                            MessageBox.Show("Datos demasiado largos para la columna.", "ERR-SQL-011", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;

                        default:
                            MessageBox.Show("Error inesperado de SQL.", "ERR-SQL-999", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                    }

                    return false;
                }
                catch (System.Exception)
                {
                    try
                    {
                        transaccion.Rollback();
                    }
                    catch
                    {
                    }

                    MessageBox.Show("Error inesperado de SQL.", "ERR-SQL-999", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    return false;
                }
            }
        }
    }
}