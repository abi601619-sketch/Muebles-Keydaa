using Modelo.Conexión_DB;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Modelo.Entidades
{
    public class MaterialUtilizado
    {
        private int IdMaterialUtilizado;
        private int CantidadUtilizada;

        public int IdMaterialUtilizado1 { get => IdMaterialUtilizado; set => IdMaterialUtilizado = value; }
        public int CantidadUtilizada1 { get => CantidadUtilizada; set => CantidadUtilizada = value; }

        public MaterialUtilizado(int idMaterialUtilizado, int cantidadUtilizada)
        {
            IdMaterialUtilizado = idMaterialUtilizado;
            CantidadUtilizada = cantidadUtilizada;
        }

        public static DataTable CargarMaterialUtilizado()
        {
            try
            {
                using (SqlConnection conectar = Conexion.Conectar())
                {
                    string comando = "SELECT * FROM VerMaterialesUtilizados;";

                    using (SqlDataAdapter adapter = new SqlDataAdapter(comando, conectar))
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
                        MessageBox.Show("La vista VerMaterialesUtilizados no existe en la base de datos.", "ERR-SQL-004", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;

                    default:
                        MessageBox.Show("Ocurrió un error al cargar los materiales utilizados.\n" + ex.Message, "ERR-SQL-999", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                }

                return new DataTable();
            }
        }

        public static bool InsertarMaterialUtilizado(int idMaterial, int idProduccion, int cantidad)
        {
            try
            {
                DataTable materiales = new DataTable();
                materiales.Columns.Add("IdMaterial", typeof(int));
                materiales.Columns.Add("Cantidad_Utilizada", typeof(int));
                materiales.Rows.Add(idMaterial, cantidad);

                GuardarConsumo(idProduccion, materiales);

                return true;
            }
            catch (SqlException ex)
            {
                switch (ex.Number)
                {
                    case 50001:
                        MessageBox.Show("Stock insuficiente o material inexistente.\nNo se guardó el consumo.", "ERR-SQL-012", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        break;

                    case 53:
                        MessageBox.Show("No se pudo establecer conexión con el servidor.", "ERR-SQL-001", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;

                    case 4060:
                        MessageBox.Show("No se pudo acceder a la base de datos.", "ERR-SQL-002", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;

                    case -2:
                        MessageBox.Show("La operación tardó demasiado tiempo.", "ERR-SQL-003", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;

                    case 208:
                        MessageBox.Show("La tabla o procedimiento utilizado no existe.", "ERR-SQL-004", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;

                    case 2627:
                        MessageBox.Show("El material utilizado ya se encuentra registrado.", "ERR-SQL-005", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        break;

                    case 2601:
                        MessageBox.Show("El material utilizado ya se encuentra registrado.", "ERR-SQL-006", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        break;

                    case 547:
                        MessageBox.Show("El material o la producción indicada no existe.", "ERR-SQL-007", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        break;

                    case 515:
                        MessageBox.Show("Debe completar todos los datos obligatorios.", "ERR-SQL-008", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        break;

                    case 245:
                        MessageBox.Show("Uno de los datos ingresados tiene un formato incorrecto.", "ERR-SQL-009", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        break;

                    case 8115:
                        MessageBox.Show("La cantidad ingresada es demasiado grande.", "ERR-SQL-010", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        break;

                    case 8152:
                        MessageBox.Show("Uno de los datos ingresados supera la longitud permitida.", "ERR-SQL-011", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        break;

                    default:
                        MessageBox.Show("Ocurrió un error al registrar el material utilizado.\n" + ex.Message, "ERR-SQL-999", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                }

                return false;
            }
            catch (InvalidOperationException ex)
            {
                MessageBox.Show(ex.Message, "ERR-VAL-001", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrió un error inesperado.\n" + ex.Message, "ERR-SQL-999", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        public static void GuardarConsumo(int idProduccion, DataTable materiales)
        {
            using (SqlConnection conexion = Conexion.Conectar())
            using (SqlTransaction transaccion = conexion.BeginTransaction())
            {
                try
                {
                    foreach (DataRow fila in materiales.Rows)
                    {
                        int cantidad = Convert.ToInt32(fila["Cantidad_Utilizada"]);

                        if (cantidad <= 0)
                            throw new InvalidOperationException("La cantidad utilizada debe ser mayor que cero.");

                        string sql = @"UPDATE Material SET Stock = Stock - @Cantidad WHERE IdMaterial = @IdMaterial AND Stock >= @Cantidad;
                                       IF @@ROWCOUNT = 0 THROW 50001, 'Stock insuficiente o material inexistente. No se guardó el consumo.', 1;
                                       INSERT INTO MaterialUtilizado (IdMaterial, IdProduccion, Cantidad_Utilizada) VALUES (@IdMaterial, @IdProduccion, @Cantidad);";

                        using (SqlCommand cmd = new SqlCommand(sql, conexion, transaccion))
                        {
                            cmd.Parameters.AddWithValue("@IdMaterial", fila["IdMaterial"]);
                            cmd.Parameters.AddWithValue("@IdProduccion", idProduccion);
                            cmd.Parameters.AddWithValue("@Cantidad", cantidad);
                            cmd.ExecuteNonQuery();
                        }
                    }

                    transaccion.Commit();
                }
                catch (SqlException ex)
                {
                    try { transaccion.Rollback(); } catch { }

                    switch (ex.Number)
                    {
                        case 50001:
                            MessageBox.Show("Stock insuficiente o material inexistente.\nNo se guardó el consumo.", "ERR-SQL-012", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            break;

                        case 53:
                            MessageBox.Show("No se pudo establecer conexión con el servidor.", "ERR-SQL-001", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;

                        case 4060:
                            MessageBox.Show("No se pudo acceder a la base de datos.", "ERR-SQL-002", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;

                        case -2:
                            MessageBox.Show("La operación tardó demasiado tiempo.", "ERR-SQL-003", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;

                        case 208:
                            MessageBox.Show("La tabla utilizada no existe en la base de datos.", "ERR-SQL-004", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;

                        case 2627:
                            MessageBox.Show("El material utilizado ya se encuentra registrado.", "ERR-SQL-005", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            break;

                        case 2601:
                            MessageBox.Show("El material utilizado ya se encuentra registrado.", "ERR-SQL-006", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            break;

                        case 547:
                            MessageBox.Show("El material o la producción indicada no existe.", "ERR-SQL-007", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            break;

                        case 515:
                            MessageBox.Show("Debe completar todos los datos obligatorios.", "ERR-SQL-008", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            break;

                        case 245:
                            MessageBox.Show("Uno de los datos ingresados tiene un formato incorrecto.", "ERR-SQL-009", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            break;

                        case 8115:
                            MessageBox.Show("La cantidad ingresada es demasiado grande.", "ERR-SQL-010", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            break;

                        case 8152:
                            MessageBox.Show("Uno de los datos ingresados supera la longitud permitida.", "ERR-SQL-011", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            break;

                        default:
                            MessageBox.Show("Ocurrió un error al guardar el consumo.\n" + ex.Message, "ERR-SQL-999", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                    }

                    throw;
                }
                catch (InvalidOperationException ex)
                {
                    try { transaccion.Rollback(); } catch { }

                    MessageBox.Show(ex.Message, "ERR-VAL-001", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    throw;
                }
                catch (Exception ex)
                {
                    try { transaccion.Rollback(); } catch { }

                    MessageBox.Show("Ocurrió un error inesperado.\n" + ex.Message, "ERR-SQL-999", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    throw;
                }
            }
        }
    }
}