using Modelo.Conexión_DB;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Modelo.Entidades
{
    public class Material
    {
        private int idMaterial;
        private string NombreDelMaterial;
        private int UnidadDeMedida;
        private int Stock;
        private string Categoria;

        public int idMaterial1 { get => idMaterial; set => idMaterial = value; }
        public string NombreDelMaterial1 { get => NombreDelMaterial; set => NombreDelMaterial = value; }
        public int UnidadDeMedida1 { get => UnidadDeMedida; set => UnidadDeMedida = value; }
        public int Stock1 { get => Stock; set => Stock = value; }
        public string Categoria1 { get => Categoria; set => Categoria = value; }

        public Material()
        {
        }

        public Material(int idMaterial, string nombreDelMaterial, int unidadDeMedida, int stock, string categoria)
        {
            idMaterial1 = idMaterial;
            NombreDelMaterial1 = nombreDelMaterial;
            UnidadDeMedida1 = unidadDeMedida;
            Stock1 = stock;
            Categoria1 = categoria;
        }

        public static DataTable CargarMateriales()
        {
            try
            {
                using (SqlConnection conectar = Conexion.Conectar())
                {
                    string comando = "SELECT * FROM VerMaterial;";

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
                MostrarErrorSql(ex, "cargar los materiales");
                return new DataTable();
            }
        }

        public bool InsertarMateriales()
        {
            string comandoSQL = @"INSERT INTO Material (NombreDelMaterial, IdUnidadDeMedida, Stock, Categoria)
                                  VALUES (@NombreDelMaterial, @IdUnidadDeMedida, @Stock, (SELECT TOP 1 IdCategoria  FROM Categoria WHERE Nombre_Categoria = @Categoria));";

            try
            {
                using (SqlConnection conexion = Conexion.Conectar())
                using (SqlCommand comandoObjeto = new SqlCommand(comandoSQL, conexion))
                {
                    comandoObjeto.Parameters.AddWithValue("@NombreDelMaterial", NombreDelMaterial1);
                    comandoObjeto.Parameters.AddWithValue("@IdUnidadDeMedida", UnidadDeMedida1);
                    comandoObjeto.Parameters.AddWithValue("@Stock", Stock1);
                    comandoObjeto.Parameters.AddWithValue("@Categoria", Categoria1);

                    return comandoObjeto.ExecuteNonQuery() > 0;
                }
            }
            catch (SqlException ex)
            {
                switch (ex.Number)
                {
                    case 2627:
                        MessageBox.Show("El material ya existe en la base de datos. Por favor use otro material.", "ERR-SQL-005", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        break;

                    case 2601:
                        MessageBox.Show("El material ya existe en la base de datos. Por favor use otro material.", "ERR-SQL-006", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        break;

                    case 547:
                        MessageBox.Show("La categoría o unidad de medida seleccionada no es válida.", "ERR-SQL-007", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        break;

                    case 515:
                        MessageBox.Show("Debe completar todos los campos obligatorios.", "ERR-SQL-008", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        break;

                    case 245:
                        MessageBox.Show("Uno de los datos ingresados tiene un formato incorrecto.", "ERR-SQL-009", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        break;

                    case 8115:
                        MessageBox.Show("El valor ingresado para el stock es demasiado grande.", "ERR-SQL-010", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        break;

                    case 8152:
                        MessageBox.Show("Uno de los datos ingresados es demasiado largo para la columna correspondiente.", "ERR-SQL-011", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        break;

                    case 53:
                        MessageBox.Show("No se pudo establecer conexión con el servidor SQL.", "ERR-SQL-001", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;

                    case 4060:
                        MessageBox.Show("No se pudo acceder a la base de datos.", "ERR-SQL-002", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;

                    case -2:
                        MessageBox.Show("La operación tardó demasiado tiempo.", "ERR-SQL-003", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;

                    case 208:
                        MessageBox.Show("La tabla, vista o procedimiento utilizado no existe.", "ERR-SQL-004", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;

                    default:
                        MessageBox.Show("Ocurrió un error inesperado en la base de datos.\n" + ex.Message, "ERR-SQL-999", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                }

                return false;
            }
        }

        public bool ActualizarMaterial()
        {
            string comandoSQL = @"UPDATE Material  SET NombreDelMaterial = @NombreDelMaterial,  IdUnidadDeMedida = @IdUnidadDeMedida,  Stock = @Stock,  Categoria = (SELECT TOP 1 IdCategoria
                                 FROM Categoria WHERE Nombre_Categoria = @Categoria) WHERE IdMaterial = @IdMaterial;";

            try
            {
                using (SqlConnection conexion = Conexion.Conectar())
                using (SqlCommand comandoObjeto = new SqlCommand(comandoSQL, conexion))
                {
                    comandoObjeto.Parameters.AddWithValue("@IdMaterial", idMaterial1);
                    comandoObjeto.Parameters.AddWithValue("@NombreDelMaterial", NombreDelMaterial1);
                    comandoObjeto.Parameters.AddWithValue("@IdUnidadDeMedida", UnidadDeMedida1);
                    comandoObjeto.Parameters.AddWithValue("@Stock", Stock1);
                    comandoObjeto.Parameters.AddWithValue("@Categoria", Categoria1);

                    int filasAfectadas = comandoObjeto.ExecuteNonQuery();

                    if (filasAfectadas > 0)
                        return true;

                    MessageBox.Show("No se encontró el material que desea actualizar.", "Material no encontrado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
            }
            catch (SqlException ex)
            {
                switch (ex.Number)
                {
                    case 2627:
                        MessageBox.Show("El material ya existe en la base de datos. Por favor verifique los datos.", "ERR-SQL-005", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        break;

                    case 2601:
                        MessageBox.Show("El material ya existe en la base de datos. Por favor verifique los datos.", "ERR-SQL-006", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        break;

                    case 547:
                        MessageBox.Show("La categoría o unidad de medida seleccionada no es válida.", "ERR-SQL-007", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        break;

                    case 515:
                        MessageBox.Show("Debe completar todos los campos obligatorios.", "ERR-SQL-008", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        break;

                    case 245:
                        MessageBox.Show("Uno de los datos ingresados tiene un formato incorrecto.", "ERR-SQL-009", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        break;

                    case 8115:
                        MessageBox.Show("El valor ingresado para el stock es demasiado grande.", "ERR-SQL-010", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        break;

                    case 8152:
                        MessageBox.Show("Uno de los datos ingresados es demasiado largo para la columna correspondiente.", "ERR-SQL-011", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        break;

                    case 53:
                        MessageBox.Show("No se pudo establecer conexión con el servidor SQL.", "ERR-SQL-001", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;

                    case 4060:
                        MessageBox.Show("No se pudo acceder a la base de datos.", "ERR-SQL-002", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;

                    case -2:
                        MessageBox.Show("La operación tardó demasiado tiempo.", "ERR-SQL-003", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;

                    case 208:
                        MessageBox.Show("La tabla, vista o procedimiento utilizado no existe.", "ERR-SQL-004", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;

                    default:
                        MessageBox.Show("Ocurrió un error inesperado en la base de datos.\n" + ex.Message, "ERR-SQL-999", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                }

                return false;
            }
        }

        public static DataTable BuscarMaterial(string texto)
        {
            try
            {
                using (SqlConnection conectar = Conexion.Conectar())
                {
                    string comando = @"SELECT * FROM VerMaterial  WHERE CAST(IdMaterial AS VARCHAR) LIKE @Texto OR Material LIKE @Texto
                                       OR Categoria LIKE @Texto ORDER BY IdMaterial;";

                    using (SqlDataAdapter adapter = new SqlDataAdapter(comando, conectar))
                    {
                        adapter.SelectCommand.Parameters.AddWithValue("@Texto", "%" + (texto ?? "") + "%");

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
                        MessageBox.Show("No se pudo establecer conexión con el servidor SQL.", "ERR-SQL-001", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;

                    case 4060:
                        MessageBox.Show("No se pudo acceder a la base de datos.", "ERR-SQL-002", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;

                    case -2:
                        MessageBox.Show("La búsqueda tardó demasiado tiempo.", "ERR-SQL-003", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;

                    case 208:
                        MessageBox.Show("La vista VerMaterial no existe en la base de datos.", "ERR-SQL-004", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;

                    default:
                        MessageBox.Show("Ocurrió un error al buscar los materiales.\n" + ex.Message, "ERR-SQL-999", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                }

                return new DataTable();
            }
        }

        public static int ContarMaterialesTotales()
        {
            try
            {
                using (SqlConnection conexion = Conexion.Conectar())
                using (SqlCommand comando = new SqlCommand("SELECT COUNT(*) FROM VerMaterial", conexion))
                {
                    return Convert.ToInt32(comando.ExecuteScalar());
                }
            }
            catch (SqlException ex)
            {
                MostrarErrorSql(ex, "contar los materiales totales");
                return 0;
            }
        }

        public static int ContarMaterialesAgotandose()
        {
            try
            {
                using (SqlConnection conexion = Conexion.Conectar())
                using (SqlCommand comando = new SqlCommand("SELECT COUNT(*) FROM VerMaterial WHERE Stock BETWEEN 1 AND 15", conexion))
                {
                    return Convert.ToInt32(comando.ExecuteScalar());
                }
            }
            catch (SqlException ex)
            {
                MostrarErrorSql(ex, "contar los materiales que se están agotando");
                return 0;
            }
        }

        public static int ContarMaterialesDisponibles()
        {
            try
            {
                using (SqlConnection conexion = Conexion.Conectar())
                using (SqlCommand comando = new SqlCommand("SELECT COUNT(*) FROM VerMaterial WHERE Stock > 15", conexion))
                {
                    return Convert.ToInt32(comando.ExecuteScalar());
                }
            }
            catch (SqlException ex)
            {
                MostrarErrorSql(ex, "contar los materiales disponibles");
                return 0;
            }
        }

        public static int ContarMaterialesAgotados()
        {
            try
            {
                using (SqlConnection conexion = Conexion.Conectar())
                using (SqlCommand comando = new SqlCommand("SELECT COUNT(*) FROM VerMaterial WHERE Stock = 0", conexion))
                {
                    return Convert.ToInt32(comando.ExecuteScalar());
                }
            }
            catch (SqlException ex)
            {
                MostrarErrorSql(ex, "contar los materiales agotados");
                return 0;
            }
        }

        private static void MostrarErrorSql(SqlException ex, string operacion)
        {
            switch (ex.Number)
            {
                case 53:
                    MessageBox.Show("No se pudo establecer conexión con el servidor SQL.", "ERR-SQL-001", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;

                case 4060:
                    MessageBox.Show("No se pudo acceder a la base de datos.", "ERR-SQL-002", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;

                case -2:
                    MessageBox.Show("La operación tardó demasiado tiempo.", "ERR-SQL-003", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;

                case 208:
                    MessageBox.Show("La tabla, vista o procedimiento utilizado no existe.", "ERR-SQL-004", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;

                case 2627:
                    MessageBox.Show("El registro ya existe por una clave primaria o restricción UNIQUE.", "ERR-SQL-005", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    break;

                case 2601:
                    MessageBox.Show("El registro ya existe por un índice UNIQUE.", "ERR-SQL-006", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    break;

                case 547:
                    MessageBox.Show("Se produjo una violación de una clave foránea o restricción CHECK.", "ERR-SQL-007", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    break;

                case 515:
                    MessageBox.Show("Falta un valor obligatorio en un campo NOT NULL.", "ERR-SQL-008", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    break;

                case 245:
                    MessageBox.Show("Uno de los datos ingresados tiene un formato o conversión incorrecta.", "ERR-SQL-009", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    break;

                case 8115:
                    MessageBox.Show("El valor ingresado excede el límite numérico permitido.", "ERR-SQL-010", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    break;

                case 8152:
                    MessageBox.Show("Uno de los datos ingresados es demasiado largo para la columna.", "ERR-SQL-011", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    break;

                default:
                    MessageBox.Show("Ocurrió un error inesperado de SQL al " + operacion + ".\n" + ex.Message, "ERR-SQL-999", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
            }
        }
    }
}