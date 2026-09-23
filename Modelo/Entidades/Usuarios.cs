using Modelo.Conexión_DB;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Modelo.Entidades
{
    public class DbUsuarios
    {
        private int IdUsuario;
        private string Nombre;
        private string Usuario;
        private string Contraseña;
        private string Rol;
        private string Correo;
        private bool Estado;

        public DbUsuarios(int idUsuario, string nombre, string usuario, string contraseña, string rol, bool estado)
        {
            IdUsuario = idUsuario;
            Nombre = nombre;
            Usuario = usuario;
            Contraseña = contraseña;
            Rol = rol;
            Estado = estado;
        }

        public DbUsuarios() { }

        public int IdUsuario1 { get => IdUsuario; set => IdUsuario = value; }
        public string Nombre1 { get => Nombre; set => Nombre = value; }
        public string Usuario1 { get => Usuario; set => Usuario = value; }
        public string Contraseña1 { get => Contraseña; set => Contraseña = value; }
        public string Rol1 { get => Rol; set => Rol = value; }
        public string Correo1 { get => Correo; set => Correo = value; }
        public bool Estado1 { get => Estado; set => Estado = value; }

        public static DataTable CargarUsuarios()
        {
            DataTable dt = new DataTable();

            try
            {
                using (SqlConnection conexion = Conexion.Conectar())
                {
                    string comando = "SELECT * FROM VerUsuarios;";

                    using (SqlDataAdapter adapter = new SqlDataAdapter(comando, conexion))
                    {
                        adapter.Fill(dt);
                    }
                }
            }
            catch (SqlException ex)
            {
                switch (ex.Number)
                {
                    case 208:
                        MessageBox.Show("La vista VerUsuarios no existe.", "Error 208", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;

                    case 53:
                        MessageBox.Show("No se pudo conectar con el servidor SQL.", "Error 53", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;

                    case 4060:
                        MessageBox.Show("No se pudo acceder a la base de datos.", "Error 4060", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;

                    case -2:
                        MessageBox.Show("La operación tardó demasiado.", "Error -2", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        break;

                    default:
                        MessageBox.Show("Error SQL: " + ex.Message, "Error " + ex.Number, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error inesperado: " + ex.Message, "Error inesperado", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return dt;
        }

        public void InsertarUsuario()
        {
            try
            {
                string contraseñaHash = BCrypt.Net.BCrypt.HashPassword(Contraseña);

                string comandoSQL = @"INSERT INTO Usuario (Nombre, Usuario, Correo, Contraseña, Rol, Estado)
                    VALUES (@Nombre, @Usuario, @Correo, @Contraseña, @Rol, @Estado);";

                using (SqlConnection conexion = Conexion.Conectar())
                using (SqlCommand comando = new SqlCommand(comandoSQL, conexion))
                {
                    comando.Parameters.AddWithValue("@Nombre", Nombre);
                    comando.Parameters.AddWithValue("@Usuario", Usuario);
                    comando.Parameters.AddWithValue("@Correo", Correo);
                    comando.Parameters.AddWithValue("@Contraseña", contraseñaHash);
                    comando.Parameters.AddWithValue("@Rol", Rol);
                    comando.Parameters.AddWithValue("@Estado", Estado);

                    comando.ExecuteNonQuery();
                }
            }
            catch (SqlException ex)
            {
                switch (ex.Number)
                {
                    case 2627:
                    case 2601:
                        MessageBox.Show("El nombre de usuario o correo electrónico ya está registrado.", "Error " + ex.Number, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        break;

                    case 547:
                        MessageBox.Show("El rol seleccionado no existe.", "Error 547", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;

                    case 515:
                        MessageBox.Show("Hay campos obligatorios sin completar.", "Error 515", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        break;

                    case 53:
                        MessageBox.Show("No se pudo conectar con el servidor SQL.", "Error 53", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;

                    case 4060:
                        MessageBox.Show("No se pudo acceder a la base de datos.", "Error 4060", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;

                    case -2:
                        MessageBox.Show("La operación tardó demasiado.", "Error -2", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        break;

                    case 208:
                        MessageBox.Show("La tabla Usuario no existe.", "Error 208", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;

                    case 8152:
                        MessageBox.Show("Uno de los datos ingresados es demasiado largo.", "Error 8152", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        break;

                    default:
                        MessageBox.Show("Error SQL: " + ex.Message, "Error " + ex.Number, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error inesperado: " + ex.Message, "Error inesperado", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void DesactivarUsuario(int idUsuario)
        {
            try
            {
                string comandoSQL = @"UPDATE Usuario SET Estado = 0 WHERE IdUsuario = @IdUsuario;";

                using (SqlConnection conexion = Conexion.Conectar())
                using (SqlCommand comando = new SqlCommand(comandoSQL, conexion))
                {
                    comando.Parameters.AddWithValue("@IdUsuario", idUsuario);

                    int filasAfectadas = comando.ExecuteNonQuery();

                    if (filasAfectadas == 0)
                    {
                        MessageBox.Show("No se encontró el usuario indicado.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            catch (SqlException ex)
            {
                switch (ex.Number)
                {
                    case 547:
                        MessageBox.Show("No se puede desactivar el usuario.", "Error 547", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;

                    case 53:
                        MessageBox.Show("No se pudo conectar con el servidor SQL.", "Error 53", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;

                    case 4060:
                        MessageBox.Show("No se pudo acceder a la base de datos.", "Error 4060", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;

                    case -2:
                        MessageBox.Show("La operación tardó demasiado.", "Error -2", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        break;

                    case 208:
                        MessageBox.Show("La tabla Usuario no existe.", "Error 208", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;

                    default:
                        MessageBox.Show("Error SQL: " + ex.Message, "Error " + ex.Number, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error inesperado: " + ex.Message, "Error inesperado", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public static bool ExistenUsuarios()
        {
            try
            {
                string comandoSQL = "SELECT COUNT(*) FROM Usuario;";

                using (SqlConnection conexion = Conexion.Conectar())
                using (SqlCommand comando = new SqlCommand(comandoSQL, conexion))
                {
                    int cantidadUsuarios = Convert.ToInt32(comando.ExecuteScalar());
                    return cantidadUsuarios > 0;
                }
            }
            catch (SqlException ex)
            {
                switch (ex.Number)
                {
                    case 53:
                        MessageBox.Show("No se pudo conectar con el servidor SQL.", "Error 53", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;

                    case 4060:
                        MessageBox.Show("No se pudo acceder a la base de datos.", "Error 4060", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;

                    case -2:
                        MessageBox.Show("La operación tardó demasiado.", "Error -2", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        break;

                    case 208:
                        MessageBox.Show("La tabla Usuario no existe.", "Error 208", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;

                    default:
                        MessageBox.Show("Error SQL: " + ex.Message, "Error " + ex.Number, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                }

                return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error inesperado: " + ex.Message, "Error inesperado", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        public bool CrearAdministradorInicial(string nombre, string usuario, string contraseña)
        {
            try
            {
                string contraseñaHash = BCrypt.Net.BCrypt.HashPassword(contraseña);

                string comandoSQL = @"INSERT INTO Usuario
                    (Nombre, Usuario, Contraseña, Rol, Estado)
                    VALUES (@Nombre, @Usuario, @Contraseña, 'Administrador', 1);";

                using (SqlConnection conexion = Conexion.Conectar())
                using (SqlCommand comando = new SqlCommand(comandoSQL, conexion))
                {
                    comando.Parameters.AddWithValue("@Nombre", nombre);
                    comando.Parameters.AddWithValue("@Usuario", usuario);
                    comando.Parameters.AddWithValue("@Contraseña", contraseñaHash);

                    return comando.ExecuteNonQuery() > 0;
                }
            }
            catch (SqlException ex)
            {
                switch (ex.Number)
                {
                    case 53:
                        MessageBox.Show("No se pudo conectar con el servidor SQL.", "Error 53", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;

                    case 4060:
                        MessageBox.Show("No se pudo acceder a la base de datos.", "Error 4060", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;

                    case -2:
                        MessageBox.Show("La operación tardó demasiado.", "Error -2", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        break;

                    case 208:
                        MessageBox.Show("La tabla Usuario no existe.", "Error 208", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;

                    case 2627:
                    case 2601:
                        MessageBox.Show("El nombre de usuario ya existe.", "Error " + ex.Number, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        break;

                    case 515:
                        MessageBox.Show("Falta un dato obligatorio.", "Error 515", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        break;

                    case 8152:
                        MessageBox.Show("Uno de los datos ingresados es demasiado largo.", "Error 8152", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        break;

                    default:
                        MessageBox.Show("Error SQL: " + ex.Message, "Error " + ex.Number, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                }

                return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error inesperado: " + ex.Message, "Error inesperado", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }
    }
}