using Modelo.Conexión_DB;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Modelo.Entidades
{
    public class Categorias
    {
        private int IdCategoria;
        private string Nombre_Categoria;
        private string Descripcion;
        private string Estado;

        public Categorias()
        {
        }

        public Categorias(int idCategoria, string nombre_Categoria, string descripcion, string estado)
        {
            IdCategoria = idCategoria;
            Nombre_Categoria = nombre_Categoria;
            Descripcion = descripcion;
            Estado = estado;
        }

        public int IdCategoria1 { get => IdCategoria; set => IdCategoria = value; }
        public string Nombre_Categoria1 { get => Nombre_Categoria; set => Nombre_Categoria = value; }
        public string Descripción1 { get => Descripcion; set => Descripcion = value; }
        public string Estado1 { get => Estado; set => Estado = value; }

        public static DataTable CargarCategorias()
        {
            try
            {
                using (SqlConnection conectar = Conexion.Conectar())
                {
                    string comando = @"SELECT IdCategoria, Nombre_Categoria, Descripcion, Estado 
                                       FROM Categoria;";

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
                        MessageBox.Show("No se pudo conectar con el servidor SQL.", "ERR-SQL-001", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;

                    case 4060:
                        MessageBox.Show("No se pudo acceder a la base de datos.", "ERR-SQL-002", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;

                    case -2:
                        MessageBox.Show("Tiempo de espera agotado.", "ERR-SQL-003", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;

                    case 208:
                        MessageBox.Show("La tabla Categoria no fue encontrada.", "ERR-SQL-004", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;

                    default:
                        MessageBox.Show("Ocurrió un error inesperado de SQL al cargar las categorías.", "ERR-SQL-999", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                }

                return new DataTable();
            }
            catch (Exception)
            {
                MessageBox.Show("Ocurrió un error inesperado al cargar las categorías.", "ERR-SQL-999", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return new DataTable();
            }
        }

        public bool InsertarCategoria()
        {
            string comandoSQL = @"INSERT INTO Categoria 
                                  (Nombre_Categoria, Descripcion, Estado) 
                                  VALUES 
                                  (@Nombre_Categoria, @Descripcion, @Estado);";

            try
            {
                using (SqlConnection conexion = Conexion.Conectar())
                using (SqlCommand comandoObjeto = new SqlCommand(comandoSQL, conexion))
                {
                    comandoObjeto.Parameters.AddWithValue("@Nombre_Categoria", Nombre_Categoria);
                    comandoObjeto.Parameters.AddWithValue("@Descripcion", Descripcion);
                    comandoObjeto.Parameters.AddWithValue("@Estado", Estado);

                    int filaAfectada = comandoObjeto.ExecuteNonQuery();

                    return filaAfectada > 0;
                }
            }
            catch (SqlException ex)
            {
                switch (ex.Number)
                {
                    case 53:
                        MessageBox.Show("No se pudo conectar con el servidor SQL.", "ERR-SQL-001", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;

                    case 4060:
                        MessageBox.Show("No se pudo acceder a la base de datos.", "ERR-SQL-002", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;

                    case -2:
                        MessageBox.Show("Tiempo de espera agotado.", "ERR-SQL-003", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;

                    case 208:
                        MessageBox.Show("La tabla Categoria no fue encontrada.", "ERR-SQL-004", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;

                    case 2627:
                        MessageBox.Show("La categoría ya existe por una clave primaria o restricción UNIQUE.", "ERR-SQL-005", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        break;

                    case 2601:
                        MessageBox.Show("La categoría ya existe por un índice UNIQUE.", "ERR-SQL-006", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        break;

                    case 547:
                        MessageBox.Show("No se puede realizar la operación porque existe una restricción relacionada.", "ERR-SQL-007", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        break;

                    case 515:
                        MessageBox.Show("Uno de los campos obligatorios no tiene valor.", "ERR-SQL-008", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        break;

                    case 245:
                        MessageBox.Show("Uno de los datos tiene un formato incorrecto.", "ERR-SQL-009", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        break;

                    case 8115:
                        MessageBox.Show("Uno de los valores ingresados supera el límite permitido.", "ERR-SQL-010", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;

                    case 8152:
                        MessageBox.Show("Uno de los datos es demasiado largo para la columna correspondiente.", "ERR-SQL-011", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        break;

                    default:
                        MessageBox.Show("Ocurrió un error inesperado de SQL al registrar la categoría.", "ERR-SQL-999", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                }

                return false;
            }
            catch (Exception)
            {
                MessageBox.Show("Ocurrió un error inesperado al registrar la categoría.", "ERR-SQL-999", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        public bool ActualizarCategoria()
        {
            string comando = @"UPDATE Categoria 
                               SET Nombre_Categoria = @Nombre,
                                   Descripcion = @Descripcion,
                                   Estado = @Estado 
                               WHERE IdCategoria = @IdCategoria;";

            try
            {
                using (SqlConnection conectar = Conexion.Conectar())
                using (SqlCommand cmd = new SqlCommand(comando, conectar))
                {
                    cmd.Parameters.AddWithValue("@Nombre", Nombre_Categoria1);
                    cmd.Parameters.AddWithValue("@Descripcion", Descripción1);
                    cmd.Parameters.AddWithValue("@Estado", Estado1);
                    cmd.Parameters.AddWithValue("@IdCategoria", IdCategoria1);

                    int filasAfectadas = cmd.ExecuteNonQuery();

                    return filasAfectadas > 0;
                }
            }
            catch (SqlException ex)
            {
                switch (ex.Number)
                {
                    case 53:
                        MessageBox.Show("No se pudo conectar con el servidor SQL.", "ERR-SQL-001", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;

                    case 4060:
                        MessageBox.Show("No se pudo acceder a la base de datos.", "ERR-SQL-002", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;

                    case -2:
                        MessageBox.Show("Tiempo de espera agotado.", "ERR-SQL-003", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;

                    case 208:
                        MessageBox.Show("La tabla Categoria no fue encontrada.", "ERR-SQL-004", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;

                    case 2627:
                        MessageBox.Show("Ya existe otra categoría con los mismos datos por una clave primaria o restricción UNIQUE.", "ERR-SQL-005", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        break;

                    case 2601:
                        MessageBox.Show("Ya existe otra categoría con los mismos datos por un índice UNIQUE.", "ERR-SQL-006", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        break;

                    case 547:
                        MessageBox.Show("No se puede actualizar la categoría porque existe una restricción relacionada.", "ERR-SQL-007", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        break;

                    case 515:
                        MessageBox.Show("Uno de los campos obligatorios no tiene valor.", "ERR-SQL-008", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        break;

                    case 245:
                        MessageBox.Show("Uno de los datos tiene un formato incorrecto.", "ERR-SQL-009", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        break;

                    case 8115:
                        MessageBox.Show("Uno de los valores ingresados supera el límite permitido.", "ERR-SQL-010", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;

                    case 8152:
                        MessageBox.Show("Uno de los datos es demasiado largo para la columna correspondiente.", "ERR-SQL-011", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        break;

                    default:
                        MessageBox.Show("Ocurrió un error inesperado de SQL al actualizar la categoría.", "ERR-SQL-999", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                }

                return false;
            }
            catch (Exception)
            {
                MessageBox.Show("Ocurrió un error inesperado al actualizar la categoría.", "ERR-SQL-999", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        public static DataTable Buscar(string termino)
        {
            try
            {
                using (SqlConnection con = Conexion.Conectar())
                {
                    string comando = @"SELECT IdCategoria, Nombre_Categoria, Descripcion, Estado 
                                       FROM Categoria 
                                       WHERE CAST(IdCategoria AS VARCHAR) LIKE @buscar 
                                       OR Nombre_Categoria LIKE @buscar;";

                    using (SqlDataAdapter ad = new SqlDataAdapter(comando, con))
                    {
                        ad.SelectCommand.Parameters.AddWithValue("@buscar", "%" + (termino ?? "") + "%");

                        DataTable dt = new DataTable();
                        ad.Fill(dt);

                        return dt;
                    }
                }
            }
            catch (SqlException ex)
            {
                switch (ex.Number)
                {
                    case 53:
                        MessageBox.Show("No se pudo conectar con el servidor SQL.", "ERR-SQL-001", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;

                    case 4060:
                        MessageBox.Show("No se pudo acceder a la base de datos.", "ERR-SQL-002", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;

                    case -2:
                        MessageBox.Show("Tiempo de espera agotado.", "ERR-SQL-003", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;

                    case 208:
                        MessageBox.Show("La tabla Categoria no fue encontrada.", "ERR-SQL-004", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;

                    case 245:
                        MessageBox.Show("Uno de los datos tiene un formato incorrecto.", "ERR-SQL-009", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        break;

                    default:
                        MessageBox.Show("Ocurrió un error inesperado de SQL al buscar categorías.", "ERR-SQL-999", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                }

                return new DataTable();
            }
            catch (Exception)
            {
                MessageBox.Show("Ocurrió un error inesperado al buscar categorías.", "ERR-SQL-999", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return new DataTable();
            }
        }

        public static int ContarCategoriasTotales()
        {
            try
            {
                using (SqlConnection conexion = Conexion.Conectar())
                {
                    string query = "SELECT COUNT(*) FROM Categoria;";

                    using (SqlCommand comando = new SqlCommand(query, conexion))
                    {
                        return Convert.ToInt32(comando.ExecuteScalar());
                    }
                }
            }
            catch (SqlException ex)
            {
                switch (ex.Number)
                {
                    case 53:
                        MessageBox.Show("No se pudo conectar con el servidor SQL.", "ERR-SQL-001", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;

                    case 4060:
                        MessageBox.Show("No se pudo acceder a la base de datos.", "ERR-SQL-002", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;

                    case -2:
                        MessageBox.Show("Tiempo de espera agotado.", "ERR-SQL-003", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;

                    case 208:
                        MessageBox.Show("La tabla Categoria no fue encontrada.", "ERR-SQL-004", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;

                    default:
                        MessageBox.Show("Ocurrió un error inesperado de SQL al obtener el total de categorías.", "ERR-SQL-999", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                }

                return 0;
            }
            catch (Exception)
            {
                MessageBox.Show("Ocurrió un error inesperado al obtener el total de categorías.", "ERR-SQL-999", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return 0;
            }
        }

        public static int ContarCategoriasActivas()
        {
            try
            {
                using (SqlConnection conexion = Conexion.Conectar())
                {
                    string query = @"SELECT COUNT(*) 
                                     FROM Categoria 
                                     WHERE Estado = 'Activa';";

                    using (SqlCommand comando = new SqlCommand(query, conexion))
                    {
                        return Convert.ToInt32(comando.ExecuteScalar());
                    }
                }
            }
            catch (SqlException ex)
            {
                switch (ex.Number)
                {
                    case 53:
                        MessageBox.Show("No se pudo conectar con el servidor SQL.", "ERR-SQL-001", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;

                    case 4060:
                        MessageBox.Show("No se pudo acceder a la base de datos.", "ERR-SQL-002", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;

                    case -2:
                        MessageBox.Show("Tiempo de espera agotado.", "ERR-SQL-003", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;

                    case 208:
                        MessageBox.Show("La tabla Categoria no fue encontrada.", "ERR-SQL-004", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;

                    case 245:
                        MessageBox.Show("Conversión o formato de datos incorrecto en la consulta.", "ERR-SQL-009", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;

                    default:
                        MessageBox.Show("Ocurrió un error inesperado de SQL al obtener las categorías activas.", "ERR-SQL-999", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                }

                return 0;
            }
            catch (Exception)
            {
                MessageBox.Show("Ocurrió un error inesperado al obtener las categorías activas.", "ERR-SQL-999", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return 0;
            }
        }

        public static int ContarCategoriasInactivas()
        {
            try
            {
                using (SqlConnection conexion = Conexion.Conectar())
                {
                    string query = @"SELECT COUNT(*) 
                                     FROM Categoria 
                                     WHERE Estado = 'Inactiva';";

                    using (SqlCommand comando = new SqlCommand(query, conexion))
                    {
                        return Convert.ToInt32(comando.ExecuteScalar());
                    }
                }
            }
            catch (SqlException ex)
            {
                switch (ex.Number)
                {
                    case 53:
                        MessageBox.Show("No se pudo conectar con el servidor SQL.", "ERR-SQL-001", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;

                    case 4060:
                        MessageBox.Show("No se pudo acceder a la base de datos.", "ERR-SQL-002", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;

                    case -2:
                        MessageBox.Show("Tiempo de espera agotado.", "ERR-SQL-003", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;

                    case 208:
                        MessageBox.Show("La tabla Categoria no fue encontrada.", "ERR-SQL-004", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;

                    case 245:
                        MessageBox.Show("Conversión o formato de datos incorrecto en la consulta.", "ERR-SQL-009", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;

                    default:
                        MessageBox.Show("Ocurrió un error inesperado de SQL al obtener las categorías inactivas.", "ERR-SQL-999", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                }

                return 0;
            }
            catch (Exception)
            {
                MessageBox.Show("Ocurrió un error inesperado al obtener las categorías inactivas.", "ERR-SQL-999", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return 0;
            }
        }
    }
}