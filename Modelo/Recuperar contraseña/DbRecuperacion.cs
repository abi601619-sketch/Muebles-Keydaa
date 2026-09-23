using Modelo.Conexión_DB;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Modelo.Entidades
{
    public class DbRecuperacion
    {
        private int IdRecuperacion;
        private int IdUsuario;
        private string Codigo;
        private DateTime FechaGeneracion;
        private DateTime FechaExpiracion;
        private bool Usado;

        public DbRecuperacion() { }

        public int IdRecuperacion1 { get => IdRecuperacion; set => IdRecuperacion = value; }
        public int IdUsuario1 { get => IdUsuario; set => IdUsuario = value; }
        public string Codigo1 { get => Codigo; set => Codigo = value; }
        public DateTime FechaGeneracion1 { get => FechaGeneracion; set => FechaGeneracion = value; }
        public DateTime FechaExpiracion1 { get => FechaExpiracion; set => FechaExpiracion = value; }
        public bool Usado1 { get => Usado; set => Usado = value; }

        public static DataTable BuscarUsuarioPorCorreo(string correo)
        {
            DataTable tabla = new DataTable();

            try
            {
                using (SqlConnection conectar = Conexion.Conectar())
                using (SqlCommand comando = new SqlCommand("sp_Usuario_BuscarPorCorreo", conectar))
                {
                    comando.CommandType = CommandType.StoredProcedure;
                    comando.Parameters.AddWithValue("@Correo", correo);

                    using (SqlDataAdapter adapter = new SqlDataAdapter(comando))
                    {
                        adapter.Fill(tabla);
                    }
                }
            }
            catch (SqlException ex)
            {
                MostrarErrorSQL(ex);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error inesperado: " + ex.Message, "Error inesperado 999", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return tabla;
        }

        public static bool GuardarCodigo(int idUsuario, string codigo)
        {
            try
            {
                using (SqlConnection conectar = Conexion.Conectar())
                using (SqlCommand comando = new SqlCommand("sp_Recuperacion_Crear", conectar))
                {
                    comando.CommandType = CommandType.StoredProcedure;
                    comando.Parameters.AddWithValue("@IdUsuario", idUsuario);
                    comando.Parameters.AddWithValue("@Codigo", codigo);
                    comando.ExecuteNonQuery();
                }

                return true;
            }
            catch (SqlException ex)
            {
                MostrarErrorSQL(ex);
                return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error inesperado: " + ex.Message, "Error inesperado 999", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        public static DataTable VerificarCodigo(int idUsuario, string codigo)
        {
            DataTable tabla = new DataTable();

            try
            {
                using (SqlConnection conectar = Conexion.Conectar())
                using (SqlCommand comando = new SqlCommand("sp_Recuperacion_Verificar", conectar))
                {
                    comando.CommandType = CommandType.StoredProcedure;
                    comando.Parameters.AddWithValue("@IdUsuario", idUsuario);
                    comando.Parameters.AddWithValue("@Codigo", codigo);

                    using (SqlDataAdapter adapter = new SqlDataAdapter(comando))
                    {
                        adapter.Fill(tabla);
                    }
                }
            }
            catch (SqlException ex)
            {
                MostrarErrorSQL(ex);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error inesperado: " + ex.Message, "Error inesperado 999", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return tabla;
        }

        public static bool CambiarContraseña(int idUsuario, string nuevaContraseña)
        {
            try
            {
                string contraseñaHash = BCrypt.Net.BCrypt.HashPassword(nuevaContraseña);

                using (SqlConnection conectar = Conexion.Conectar())
                using (SqlCommand comando = new SqlCommand("sp_Usuario_CambiarContraseña", conectar))
                {
                    comando.CommandType = CommandType.StoredProcedure;
                    comando.Parameters.AddWithValue("@IdUsuario", idUsuario);
                    comando.Parameters.AddWithValue("@NuevaContraseña", contraseñaHash);

                    comando.ExecuteNonQuery();
                }

                return true;
            }
            catch (SqlException ex)
            {
                MostrarErrorSQL(ex);
                return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error inesperado: " + ex.Message, "Error inesperado 999", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        public static bool MarcarCodigoUsado(int idRecuperacion)
        {
            try
            {
                using (SqlConnection conectar = Conexion.Conectar())
                using (SqlCommand comando = new SqlCommand("sp_Recuperacion_MarcarUsado", conectar))
                {
                    comando.CommandType = CommandType.StoredProcedure;
                    comando.Parameters.AddWithValue("@IdRecuperacion", idRecuperacion);

                    comando.ExecuteNonQuery();
                }

                return true;
            }
            catch (SqlException ex)
            {
                MostrarErrorSQL(ex);
                return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error inesperado: " + ex.Message, "Error inesperado 999", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        private static void MostrarErrorSQL(SqlException ex)
        {
            switch (ex.Number)
            {
                case 53:
                    MessageBox.Show("No se pudo conectar con el servidor SQL.", "Error SQL 53", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;

                case 4060:
                    MessageBox.Show("No se pudo acceder a la base de datos.", "Error SQL 4060", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;

                case -2:
                    MessageBox.Show("La operación tardó demasiado.", "Error SQL -2", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    break;

                case 208:
                    MessageBox.Show("El procedimiento almacenado o una tabla utilizada no existe.", "Error SQL 208", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;

                case 2812:
                    MessageBox.Show("El procedimiento almacenado no existe.", "Error SQL 2812", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;

                case 201:
                    MessageBox.Show("Faltan parámetros requeridos para ejecutar el procedimiento.", "Error SQL 201", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    break;

                case 8144:
                    MessageBox.Show("El procedimiento recibió parámetros no válidos.", "Error SQL 8144", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    break;

                case 245:
                    MessageBox.Show("Se encontró un valor con un formato incorrecto.", "Error SQL 245", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    break;

                case 547:
                    MessageBox.Show("La operación no puede realizarse debido a registros relacionados.", "Error SQL 547", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    break;

                case 2601:
                    MessageBox.Show("El registro que intenta guardar ya existe.", "Error SQL 2601", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    break;

                case 2627:
                    MessageBox.Show("El registro que intenta guardar ya existe.", "Error SQL 2627", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    break;

                case 515:
                    MessageBox.Show("Falta un dato obligatorio.", "Error SQL 515", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    break;

                case 8152:
                    MessageBox.Show("Uno de los datos ingresados es demasiado largo.", "Error SQL 8152", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    break;

                case 8114:
                    MessageBox.Show("No se pudo convertir uno de los valores enviados.", "Error SQL 8114", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    break;

                case 2628:
                    MessageBox.Show("Uno de los datos ingresados excede el tamaño permitido.", "Error SQL 2628", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    break;

                case 1205:
                    MessageBox.Show("La operación fue bloqueada por otra transacción. Intente nuevamente.", "Error SQL 1205", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    break;

                case 18456:
                    MessageBox.Show("No se pudo iniciar sesión en SQL Server.", "Error SQL 18456", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;

                default:
                    MessageBox.Show("Error SQL: " + ex.Message, "Error SQL " + ex.Number, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
            }
        }
    }
}