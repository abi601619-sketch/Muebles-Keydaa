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

        public MaterialUtilizado(int idMaterialUtilizado, int cantidadUtilizada)
        {
            IdMaterialUtilizado = idMaterialUtilizado;
            CantidadUtilizada = cantidadUtilizada;
        }

        public int IdMaterialUtilizado1 { get => IdMaterialUtilizado; set => IdMaterialUtilizado = value; }
        public int CantidadUtilizada1 { get => CantidadUtilizada; set => CantidadUtilizada = value; }


        public static DataTable CargarMaterialUtilizado()
        {
            try
            {
                using (SqlConnection conectar = Conexion.Conectar())
                {
                    string comando = "SELECT * FROM VerMaterialesUtilizados;";

                    SqlDataAdapter adapter =
                        new SqlDataAdapter(comando, conectar);

                    DataTable dt = new DataTable();

                    adapter.Fill(dt);

                    return dt;
                }
            }
            catch (SqlException ex)
            {
                switch (ex.Number)
                {
                    case 208:
                        MessageBox.Show(
                            "La vista VerMaterialesUtilizados no existe en la base de datos.",
                            "Error 208",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                        break;

                    case 53:
                        MessageBox.Show(
                            "No se pudo establecer conexión con el servidor.",
                            "Error 53",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                        break;

                    case 4060:
                        MessageBox.Show(
                            "No se pudo acceder a la base de datos.",
                            "Error 4060",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                        break;

                    case -2:
                        MessageBox.Show(
                            "La consulta tardó demasiado tiempo.",
                            "Error -2",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                        break;

                    default:
                        MessageBox.Show(
                            "Ocurrió un error al cargar los materiales utilizados.\n" + ex.Message,
                            "Error " + ex.Number,
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                        break;
                }

                return new DataTable();
            }
        }


        public static bool InsertarMaterialUtilizado(
            int idMaterial,
            int idProduccion,
            int cantidad)
        {
            try
            {
                var materiales = new DataTable();

                materiales.Columns.Add(
                    "IdMaterial",
                    typeof(int));

                materiales.Columns.Add(
                    "Cantidad_Utilizada",
                    typeof(int));

                materiales.Rows.Add(
                    idMaterial,
                    cantidad);

                GuardarConsumo(
                    idProduccion,
                    materiales);

                return true;
            }
            catch (SqlException ex)
            {
                switch (ex.Number)
                {
                    case 50001:
                        MessageBox.Show(
                            "Stock insuficiente o material inexistente.\nNo se guardó el consumo.",
                            "Error 50001",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning);
                        break;

                    case 547:
                        MessageBox.Show(
                            "El material o la producción indicada no existe.",
                            "Error 547",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning);
                        break;

                    case 2627:
                    case 2601:
                        MessageBox.Show(
                            "El material utilizado ya se encuentra registrado.",
                            "Registro Duplicado",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning);
                        break;

                    case 515:
                        MessageBox.Show(
                            "Debe completar todos los datos obligatorios.",
                            "Error 515",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning);
                        break;

                    case 245:
                        MessageBox.Show(
                            "Uno de los datos ingresados tiene un formato incorrecto.",
                            "Error 245",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning);
                        break;

                    case 53:
                        MessageBox.Show(
                            "No se pudo establecer conexión con el servidor.",
                            "Error 53",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                        break;

                    case 4060:
                        MessageBox.Show(
                            "No se pudo acceder a la base de datos.",
                            "Error 4060",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                        break;

                    case -2:
                        MessageBox.Show(
                            "La operación tardó demasiado tiempo.",
                            "Error -2",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                        break;

                    case 208:
                        MessageBox.Show(
                            "La tabla o procedimiento utilizado no existe.",
                            "Error 208",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                        break;

                    default:
                        MessageBox.Show(
                            "Ocurrió un error al registrar el material utilizado.\n" + ex.Message,
                            "Error " + ex.Number,
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                        break;
                }

                return false;
            }
            catch (InvalidOperationException ex)
            {
                MessageBox.Show(
                    ex.Message,
                    "Cantidad no válida",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Ocurrió un error inesperado.\n" + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                return false;
            }
        }


        // Todo el lote se guarda o se revierte; el stock se valida dentro de SQL.
        public static void GuardarConsumo(
            int idProduccion,
            DataTable materiales)
        {
            using (SqlConnection conexion = Conexion.Conectar())
            using (SqlTransaction transaccion =
                conexion.BeginTransaction())
            {
                try
                {
                    foreach (DataRow fila in materiales.Rows)
                    {
                        int cantidad =
                            Convert.ToInt32(
                                fila["Cantidad_Utilizada"]);

                        if (cantidad <= 0)
                        {
                            throw new InvalidOperationException(
                                "La cantidad utilizada debe ser mayor que cero.");
                        }

                        using (var cmd = new SqlCommand(@"
                            UPDATE Material
                            SET Stock = Stock - @Cantidad
                            WHERE IdMaterial = @IdMaterial
                            AND Stock >= @Cantidad;

                            IF @@ROWCOUNT = 0
                                THROW 50001,
                                'Stock insuficiente o material inexistente. No se guardó el consumo.',
                                1;

                            INSERT INTO MaterialUtilizado
                            (
                                IdMaterial,
                                IdProduccion,
                                Cantidad_Utilizada
                            )
                            VALUES
                            (
                                @IdMaterial,
                                @IdProduccion,
                                @Cantidad
                            );",
                            conexion,
                            transaccion))
                        {
                            cmd.Parameters.AddWithValue(
                                "@IdMaterial",
                                fila["IdMaterial"]);

                            cmd.Parameters.AddWithValue(
                                "@IdProduccion",
                                idProduccion);

                            cmd.Parameters.AddWithValue(
                                "@Cantidad",
                                cantidad);

                            cmd.ExecuteNonQuery();
                        }
                    }

                    transaccion.Commit();
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
                        case 50001:
                            MessageBox.Show(
                                "Stock insuficiente o material inexistente.\nNo se guardó el consumo.",
                                "Error 50001",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
                            break;

                        case 547:
                            MessageBox.Show(
                                "El material o la producción indicada no existe.",
                                "Error 547",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
                            break;

                        case 2627:
                        case 2601:
                            MessageBox.Show(
                                "El material utilizado ya se encuentra registrado.",
                                "Registro Duplicado",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
                            break;

                        case 515:
                            MessageBox.Show(
                                "Debe completar todos los datos obligatorios.",
                                "Error 515",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
                            break;

                        case 245:
                            MessageBox.Show(
                                "Uno de los datos ingresados tiene un formato incorrecto.",
                                "Error 245",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
                            break;

                        case 8115:
                            MessageBox.Show(
                                "La cantidad ingresada es demasiado grande.",
                                "Error 8115",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
                            break;

                        case 53:
                            MessageBox.Show(
                                "No se pudo establecer conexión con el servidor.",
                                "Error 53",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
                            break;

                        case 4060:
                            MessageBox.Show(
                                "No se pudo acceder a la base de datos.",
                                "Error 4060",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
                            break;

                        case -2:
                            MessageBox.Show(
                                "La operación tardó demasiado tiempo.",
                                "Error -2",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
                            break;

                        case 208:
                            MessageBox.Show(
                                "La tabla utilizada no existe en la base de datos.",
                                "Error 208",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
                            break;

                        default:
                            MessageBox.Show(
                                "Ocurrió un error al guardar el consumo.\n" + ex.Message,
                                "Error " + ex.Number,
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
                            break;
                    }

                    throw;
                }
                catch (InvalidOperationException ex)
                {
                    try
                    {
                        transaccion.Rollback();
                    }
                    catch
                    {
                    }

                    MessageBox.Show(
                        ex.Message,
                        "Cantidad no válida",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);

                    throw;
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

                    MessageBox.Show(
                        "Ocurrió un error inesperado.\n" + ex.Message,
                        "Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);

                    throw;
                }
            }
        }
    }
}