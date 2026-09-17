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

        public DbUsuarios()
        {

        }

        public int IdUsuario1 { get => IdUsuario; set => IdUsuario = value; }
        public string Nombre1 { get => Nombre; set => Nombre = value; }
        public string Usuario1 { get => Usuario; set => Usuario = value; }
        public string Contraseña1 { get => Contraseña; set => Contraseña = value; }
        public string Rol1 { get => Rol; set => Rol = value; }
        public bool Estado1 { get => Estado; set => Estado = value; }

        public static DataTable CargarUsuarios()
        {
            try
            {
                SqlConnection conectar = Conexion.Conectar();

                string comando = "SELECT * FROM VerUsuarios;";

                SqlDataAdapter adapter = new SqlDataAdapter(comando, conectar);

                DataTable dt = new DataTable();

                adapter.Fill(dt);

                return dt;
            }
            catch (SqlException ex)
            {
                switch (ex.Number)
                {
                    case 208:
                        MessageBox.Show(
                            "Error 208: La vista VerUsuarios no existe.",
                            "Error de base de datos",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error
                        );
                        break;

                    case 53:
                        MessageBox.Show(
                            "Error 53: No se pudo conectar con el servidor SQL.",
                            "Error de conexión",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error
                        );
                        break;

                    case 4060:
                        MessageBox.Show(
                            "Error 4060: No se pudo acceder a la base de datos.",
                            "Error de base de datos",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error
                        );
                        break;

                    case -2:
                        MessageBox.Show(
                            "Error -2: La operación tardó demasiado.",
                            "Tiempo de espera agotado",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning
                        );
                        break;

                    default:
                        MessageBox.Show(
                            "Error SQL " + ex.Number + ": " + ex.Message,
                            "Error de base de datos",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error
                        );
                        break;
                }

                return new DataTable();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Error inesperado: " + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );

                return new DataTable();
            }
        }

        public void InsertarUsuario()
        {
            try
            {
                string contraseñaHash = BCrypt.Net.BCrypt.HashPassword(Contraseña);

                string comandoSQL = @"INSERT INTO Usuario
                    (Nombre, Usuario, Contraseña, Rol, Estado)
                    VALUES
                    (@Nombre, @Usuario, @Contraseña, @Rol, @Estado);";

                using (SqlConnection conexion = Conexion.Conectar())
                {
                    using (SqlCommand comandoObjeto = new SqlCommand(comandoSQL, conexion))
                    {
                        comandoObjeto.Parameters.AddWithValue("@Nombre", Nombre);
                        comandoObjeto.Parameters.AddWithValue("@Usuario", Usuario);
                        comandoObjeto.Parameters.AddWithValue("@Contraseña", contraseñaHash);
                        comandoObjeto.Parameters.AddWithValue("@Rol", Rol);
                        comandoObjeto.Parameters.AddWithValue("@Estado", Estado);

                        comandoObjeto.ExecuteNonQuery();
                    }
                }
            }
            catch (SqlException ex)
            {
                switch (ex.Number)
                {
                    case 2627:
                    case 2601:
                        MessageBox.Show(
                            "Error 2627/2601: El nombre de usuario ya está en uso.",
                            "Usuario duplicado",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning
                        );
                        break;

                    case 547:
                        MessageBox.Show(
                            "Error 547: El rol seleccionado no existe.",
                            "Error de relación",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error
                        );
                        break;

                    case 515:
                        MessageBox.Show(
                            "Error 515: Hay campos obligatorios sin completar.",
                            "Datos incompletos",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning
                        );
                        break;

                    case 53:
                        MessageBox.Show(
                            "Error 53: No se pudo conectar con el servidor SQL.",
                            "Error de conexión",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error
                        );
                        break;

                    case 4060:
                        MessageBox.Show(
                            "Error 4060: No se pudo acceder a la base de datos.",
                            "Error de base de datos",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error
                        );
                        break;

                    case -2:
                        MessageBox.Show(
                            "Error -2: La operación tardó demasiado.",
                            "Tiempo de espera agotado",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning
                        );
                        break;

                    case 208:
                        MessageBox.Show(
                            "Error 208: La tabla Usuario no existe.",
                            "Error de base de datos",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error
                        );
                        break;

                    default:
                        MessageBox.Show(
                            "Error SQL " + ex.Number + ": " + ex.Message,
                            "Error al guardar usuario",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error
                        );
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Error inesperado: " + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        public void ActualizarUsuario(int idUsuario)
        {
            try
            {
                string comandoSQL = @"UPDATE Usuario 
                    SET Nombre = @Nombre,
                        Usuario = @Usuario,
                        Rol = @Rol,
                        Estado = @Estado
                    WHERE IdUsuario = @IdUsuario;";

                using (SqlConnection conexion = Conexion.Conectar())
                {
                    using (SqlCommand comandoObjeto = new SqlCommand(comandoSQL, conexion))
                    {
                        comandoObjeto.Parameters.AddWithValue("@IdUsuario", idUsuario);
                        comandoObjeto.Parameters.AddWithValue("@Nombre", Nombre);
                        comandoObjeto.Parameters.AddWithValue("@Usuario", Usuario);
                        comandoObjeto.Parameters.AddWithValue("@Rol", Rol);
                        comandoObjeto.Parameters.AddWithValue("@Estado", Estado);

                        int filasAfectadas = comandoObjeto.ExecuteNonQuery();

                        if (filasAfectadas == 0)
                        {
                            MessageBox.Show(
                                "No se encontró el usuario indicado.",
                                "Usuario no encontrado",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning
                            );
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                switch (ex.Number)
                {
                    case 2627:
                    case 2601:
                        MessageBox.Show(
                            "Error 2627/2601: El nombre de usuario ya está en uso.",
                            "Usuario duplicado",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning
                        );
                        break;

                    case 547:
                        MessageBox.Show(
                            "Error 547: El rol seleccionado no existe.",
                            "Error de relación",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error
                        );
                        break;

                    case 515:
                        MessageBox.Show(
                            "Error 515: Hay campos obligatorios sin completar.",
                            "Datos incompletos",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning
                        );
                        break;

                    case 53:
                        MessageBox.Show(
                            "Error 53: No se pudo conectar con el servidor SQL.",
                            "Error de conexión",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error
                        );
                        break;

                    case 4060:
                        MessageBox.Show(
                            "Error 4060: No se pudo acceder a la base de datos.",
                            "Error de base de datos",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error
                        );
                        break;

                    case -2:
                        MessageBox.Show(
                            "Error -2: La operación tardó demasiado.",
                            "Tiempo de espera agotado",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning
                        );
                        break;

                    case 208:
                        MessageBox.Show(
                            "Error 208: La tabla Usuario no existe.",
                            "Error de base de datos",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error
                        );
                        break;

                    default:
                        MessageBox.Show(
                            "Error SQL " + ex.Number + ": " + ex.Message,
                            "Error al actualizar usuario",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error
                        );
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Error inesperado: " + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        public void DesactivarUsuario(int idUsuario)
        {
            try
            {
                // Le asigna el valor de 0 para desactivarlo,
                // equivaliendo a un false.
                string comandoSQL = @"UPDATE Usuario 
                    SET Estado = 0 
                    WHERE IdUsuario = @IdUsuario;";

                using (SqlConnection conexion = Conexion.Conectar())
                {
                    using (SqlCommand comandoObjeto = new SqlCommand(comandoSQL, conexion))
                    {
                        comandoObjeto.Parameters.AddWithValue("@IdUsuario", idUsuario);

                        // Ejecuta la consulta.
                        int filasAfectadas = comandoObjeto.ExecuteNonQuery();

                        if (filasAfectadas == 0)
                        {
                            MessageBox.Show(
                                "No se encontró el usuario indicado.",
                                "Usuario no encontrado",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning
                            );
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                switch (ex.Number)
                {
                    case 547:
                        MessageBox.Show(
                            "Error 547: No se puede desactivar el usuario.",
                            "Error de relación",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error
                        );
                        break;

                    case 53:
                        MessageBox.Show(
                            "Error 53: No se pudo conectar con el servidor SQL.",
                            "Error de conexión",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error
                        );
                        break;

                    case 4060:
                        MessageBox.Show(
                            "Error 4060: No se pudo acceder a la base de datos.",
                            "Error de base de datos",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error
                        );
                        break;

                    case -2:
                        MessageBox.Show(
                            "Error -2: La operación tardó demasiado.",
                            "Tiempo de espera agotado",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning
                        );
                        break;

                    case 208:
                        MessageBox.Show(
                            "Error 208: La tabla Usuario no existe.",
                            "Error de base de datos",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error
                        );
                        break;

                    default:
                        MessageBox.Show(
                            "Error SQL " + ex.Number + ": " + ex.Message,
                            "Error al desactivar usuario",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error
                        );
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Error inesperado: " + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }
    }
}
