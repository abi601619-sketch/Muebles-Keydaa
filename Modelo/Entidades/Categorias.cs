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

        //METODO PARA CARGAR LOS REGISTROS DE GATEGORIAS
        public static DataTable CargarCategorias()
        {
            try
            {
                SqlConnection conectar = Conexion.Conectar();

                string comando = @" SELECT IdCategoria, Nombre_Categoria, Descripcion, Estado FROM Categoria;";

                SqlDataAdapter adapter = new SqlDataAdapter(comando, conectar);

                DataTable dt = new DataTable();

                adapter.Fill(dt);

                conectar.Close();

                return dt;
            }
            catch (SqlException ex)
            {
                switch (ex.Number)
                {
                    // Error: no existe la tabla o el objeto especificado
                    case 208:
                        MessageBox.Show("La tabla Categoria no existe en la base de datos.", "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;

                    // Error: no se pudo establecer conexión con SQL Server
                    case 53:
                        MessageBox.Show("No se pudo establecer conexión con el servidor SQL.", "Error de Conexión",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;

                    // Otros errores SQL
                    default:
                        MessageBox.Show("Error al cargar las categorías: " + ex.Message, "Error " + ex.Number,
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                }

                return new DataTable();
            }
        }

        public bool InsertarCategoria()
        {
            string comandoSQL = @"INSERT INTO Categoria (Nombre_Categoria, Descripcion, Estado)VALUES( @Nombre_Categoria, @Descripcion,@Estado );";

            using (SqlConnection conexion = Conexion.Conectar())
            using (SqlCommand comandoObjeto = new SqlCommand(comandoSQL, conexion))
            {
                comandoObjeto.Parameters.AddWithValue("@Nombre_Categoria", Nombre_Categoria);

                comandoObjeto.Parameters.AddWithValue("@Descripcion", Descripcion);

                comandoObjeto.Parameters.AddWithValue("@Estado", Estado);

                try
                {
                    int filaAfectada = comandoObjeto.ExecuteNonQuery();

                    return filaAfectada > 0;
                }
                catch (SqlException ex)
                {
                    switch (ex.Number)
                    {
                        // Error: clave primaria o restricción UNIQUE duplicada
                        case 2627:

                        // Error: índice UNIQUE duplicado
                        case 2601:

                            MessageBox.Show("La categoría ya existe en la base de datos.", "Registro Duplicado",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            break;

                        // Error: se está intentando insertar NULL
                        // en una columna NOT NULL
                        case 515:

                            MessageBox.Show("No se pueden dejar campos obligatorios vacíos.", "Datos Obligatorios",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            break;

                        // Otros errores
                        default:

                            MessageBox.Show("Ocurrió un error al registrar la categoría: " + ex.Message, "Error " + ex.Number,
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                    }

                    return false;
                }
            }
        }

        //METODO PARA ACTUALIZAR CATEGORIA
        public bool ActualizarCategoria()
        {
            try
            {
                SqlConnection conectar = Conexion.Conectar();

                string comando = @" UPDATE Categoria SET Nombre_Categoria = @Nombre,Descripcion = @Descripcion, Estado = @Estado WHERE IdCategoria = @IdCategoria";

                SqlCommand cmd = new SqlCommand(comando, conectar);

                cmd.Parameters.AddWithValue("@Nombre", Nombre_Categoria1);

                cmd.Parameters.AddWithValue("@Descripcion", Descripción1);

                cmd.Parameters.AddWithValue("@Estado", Estado1);

                cmd.Parameters.AddWithValue("@IdCategoria", IdCategoria1);

                int filasAfectadas = cmd.ExecuteNonQuery();

                conectar.Close();

                return filasAfectadas > 0;
            }
            catch (SqlException ex)
            {
                switch (ex.Number)
                {
                    // Error: nombre de categoría duplicado
                    case 2627:
                    case 2601:

                        MessageBox.Show("Ya existe otra categoría con ese nombre.", "Registro Duplicado",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        break;

                    // Error: valor NULL en columna NOT NULL
                    case 515:

                        MessageBox.Show("Uno de los campos obligatorios está vacío.", "Datos Obligatorios",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        break;

                    // Otros errores SQL
                    default:

                        MessageBox.Show("Error al actualizar la categoría: " + ex.Message, "Error " + ex.Number,
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                }

                return false;
            }
        }


        public static DataTable Buscar(string termino)
        {
            try
            {
                SqlConnection con = Conexion.Conectar();

                string comando = @"SELECT IdCategoria, Nombre_Categoria,  Descripcion, Estado
            FROM Categoria WHERE CAST(IdCategoria AS VARCHAR) LIKE @buscar OR Nombre_Categoria LIKE @buscar;";

                SqlDataAdapter ad = new SqlDataAdapter(comando, con);

                ad.SelectCommand.Parameters.AddWithValue("@buscar", "%" + termino + "%");

                DataTable dt = new DataTable();

                ad.Fill(dt);

                con.Close();

                return dt;
            }
            catch (SqlException ex)
            {
                switch (ex.Number)
                {
                    // Error: la tabla Categoria no existe
                    case 208:

                        MessageBox.Show("La tabla Categoria no existe en la base de datos.", "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;

                    // Error: no se pudo establecer conexión
                    case 53:

                        MessageBox.Show("No se pudo conectar con el servidor SQL.", "Error de Conexión",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;

                    // Otros errores SQL
                    default:

                        MessageBox.Show("Error al buscar categorías: " + ex.Message, "Error " + ex.Number,
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                }

                return new DataTable();
            }
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
            catch (SqlException ex)
            {
                switch (ex.Number)
                {
                    // Error: la tabla Categoria no existe
                    case 208:

                        MessageBox.Show("La tabla Categoria no existe.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;

                    // Error: no se pudo conectar con SQL Server
                    case 53:

                        MessageBox.Show("No se pudo establecer conexión con SQL Server.", "Error de Conexión", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;

                    // Otros errores
                    default:

                        MessageBox.Show("Error al contar las categorías: " + ex.Message, "Error " + ex.Number, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                }

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
                        return Convert.ToInt32(
                            comando.ExecuteScalar()
                        );
                    }
                }
            }
            catch (SqlException ex)
            {
                switch (ex.Number)
                {
                    // Error: no existe la tabla Categoria
                    case 208:

                        MessageBox.Show("La tabla Categoria no existe.", "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;

                    // Error: conversión de datos
                    case 245:

                        MessageBox.Show("El tipo de dato del campo Estado no es compatible " + "con la consulta.", "Error de Tipo de Dato",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;

                    // Otros errores
                    default:

                        MessageBox.Show("Error al contar las categorías activas: " + ex.Message, "Error " + ex.Number,
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                }

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
                    string query = @" SELECT COUNT(*)  FROM Categoria  WHERE Estado = 'Inactiva'";

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
                    // Error: no existe la tabla Categoria
                    case 208:

                        MessageBox.Show("La tabla Categoria no existe.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;

                    // Error: conversión de datos
                    case 245:

                        MessageBox.Show("El tipo de dato del campo Estado no es compatible " + "con la consulta.", "Error de Tipo de Dato", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;

                    // Otros errores
                    default:

                        MessageBox.Show("Error al contar las categorías inactivas: " + ex.Message, "Error " + ex.Number, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                }

                return 0;
            }
        }
    }
}
