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
            SqlConnection conectar = Conexion.Conectar();

            string comando = "SELECT IdCategoria,Nombre_Categoria,Descripcion,Estado FROM Categoria;";
            SqlDataAdapter adapter = new SqlDataAdapter(comando, conectar);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            return dt;
        }


        public bool InsertarCategoria()
        {
            string comandoSQL = "INSERT INTO Categoria(Nombre_Categoria,Descripcion,Estado)" +
                "VALUES (@Nombre_Categoria,@Descripcion,@Estado);";

            // El bloque using asegura que la conexión y el comando
            // se cierren y se destruyan
            // incluso si ocurre un error

            using (SqlConnection conexion = Conexion.Conectar())
            {
                using (SqlCommand comandoObjeto = new SqlCommand(comandoSQL, conexion))
                {
                    // Agregan los parámetros
                    comandoObjeto.Parameters.AddWithValue("@Nombre_Categoria", Nombre_Categoria);
                    comandoObjeto.Parameters.AddWithValue("@Descripcion", Descripcion);
                    comandoObjeto.Parameters.AddWithValue("@Estado", Estado);

                    try
                    {
                        // Se ejecuta una sola vez
                        // y se guarda la cantidad de filas afectadas
                        int filaAfectada = comandoObjeto.ExecuteNonQuery();

                        // Si se afectó más de 0 filas retorna true
                        // y sino false
                        return filaAfectada > 0;
                    }
                    catch (SqlException ex)
                    {
                        switch (ex.Number)
                        {
                            case 2627:
                            case 2601:

                                MessageBox.Show("La categoría ya existe en la base de datos. Por favor use otro nombre.", "Registro Duplicado", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                                break;

                            default:

                                MessageBox.Show("Ocurrió un error inesperado en la base de datos " + ex.Message, "Error " + ex.Number, MessageBoxButtons.OK, MessageBoxIcon.Warning);

                                break;
                        }

                        return false;
                    }


                }
            }
        }

        public void ActualizarCategoria()
        {
            SqlConnection conectar = Conexion.Conectar();

            string comando = "UPDATE Categoria SET Nombre_Categoria = @Nombre, Descripcion = @Descripcion, Estado = @Estado WHERE IdCategoria = @IdCategoria";

            SqlCommand cmd = new SqlCommand(comando, conectar);

            cmd.Parameters.AddWithValue("@Nombre", Nombre_Categoria1);
            cmd.Parameters.AddWithValue("@Descripcion", Descripción1);
            cmd.Parameters.AddWithValue("@Estado", Estado1);
            cmd.Parameters.AddWithValue("@IdCategoria", IdCategoria1);

            cmd.ExecuteNonQuery();
            conectar.Close();
        }

        public static DataTable Buscar(string termino)
        {
            SqlConnection con = Conexion.Conectar();

            string comando = "SELECT IdCategoria, Nombre_Categoria, Descripcion, Estado FROM Categoria WHERE CAST(IdCategoria AS VARCHAR) LIKE @buscar" +
                             " OR Nombre_Categoria LIKE @buscar;";

            SqlDataAdapter ad = new SqlDataAdapter(comando, con);

            ad.SelectCommand.Parameters.AddWithValue("@buscar", "%" + termino + "%");

            DataTable dt = new DataTable();

            ad.Fill(dt);

            return dt;
        }

        // CALCULAR ESTADÍSTICAS DE CATEGORÍAS

        // Total de categorías
        public static int ContarCategoriasTotales()
        {
            try
            {
                using (SqlConnection conexion = Conexion.Conectar())
                {
                    string query = "SELECT COUNT(*) FROM Categoria";

                    using (SqlCommand comando = new SqlCommand(query, conexion))
                    {
                        return Convert.ToInt32(comando.ExecuteScalar());
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al contar las categorías totales: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return 0;
            }
        }

        // Categorías activas
        public static int ContarCategoriasActivas()
        {
            try
            {
                using (SqlConnection conexion = Conexion.Conectar())
                {
                    string query = @"SELECT COUNT(*) FROM Categoria WHERE Estado = 'Activa'";

                    using (SqlCommand comando = new SqlCommand(query, conexion))
                    {
                        return Convert.ToInt32(comando.ExecuteScalar());
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al contar las categorías activas: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return 0;
            }
        }

        // Categorías inactivas
        public static int ContarCategoriasInactivas()
        {
            try
            {
                using (SqlConnection conexion = Conexion.Conectar())
                {
                    string query = @"SELECT COUNT(*) FROM Categoria WHERE Estado = 'Inactiva'";

                    using (SqlCommand comando = new SqlCommand(query, conexion))
                    {
                        return Convert.ToInt32(comando.ExecuteScalar());
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al contar las categorías inactivas: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return 0;
            }
        }
    }
}
