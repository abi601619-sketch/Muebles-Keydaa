using Modelo.Conexión_DB;
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
            SqlConnection conectar = Conexion.Conectar();
            string comando = "SELECT * FROM VerUsuarios;";
            SqlDataAdapter adapter = new SqlDataAdapter(comando, conectar);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            return dt;
        }

        public void InsertarUsuario()
        {
            string contraseñaHash = BCrypt.Net.BCrypt.HashPassword(Contraseña);

            string comandoSQL = @"INSERT INTO Usuario(Nombre, Usuario, Contraseña, Rol, Estado)
            VALUES(@Nombre, @Usuario, @Contraseña, @Rol, @Estado);";

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

                    try
                    {
                        comandoObjeto.ExecuteNonQuery();
                    }
                    catch (SqlException ex)
                    {
                        if (ex.Number == 2627)
                        {
                            MessageBox.Show("El nombre de usuario ya está en uso.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else
                        {
                            MessageBox.Show("Ocurrió un error al guardar el usuario.\n\n" + ex.Message, "Error " + ex.Number, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }

            }
        }

        public void ActualizarUsuario(int idUsuario)
        {
            string comandoSQL = @"UPDATE Usuario SET Nombre = @Nombre,Usuario = @Usuario, Rol = @Rol,Estado = @Estado WHERE IdUsuario = @IdUsuario;";

            using (SqlConnection conexion = Conexion.Conectar())
            {
                using (SqlCommand comandoObjeto = new SqlCommand(comandoSQL, conexion))
                {
                    comandoObjeto.Parameters.AddWithValue("@IdUsuario", idUsuario);
                    comandoObjeto.Parameters.AddWithValue("@Nombre", Nombre);
                    comandoObjeto.Parameters.AddWithValue("@Usuario", Usuario);
                    comandoObjeto.Parameters.AddWithValue("@Rol", Rol);
                    comandoObjeto.Parameters.AddWithValue("@Estado", Estado);

                    try
                    {
                        comandoObjeto.ExecuteNonQuery();
                    }
                    catch (SqlException ex)
                    {
                        if (ex.Number == 2627)
                        {
                            MessageBox.Show("El nombre de usuario ya está en uso.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else
                        {
                            MessageBox.Show("Ocurrió un error al actualizar el usuario.\n\n" + ex.Message, "Error " + ex.Number, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
        }

        public void DesactivarUsuario(int idUsuario)
        {
            // Le asigna el valor de 0 para desactivarlo , equivaliendo a un false
            string comandoSQL = @"UPDATE Usuario SET Estado = 0 WHERE IdUsuario = @IdUsuario;";

            using (SqlConnection conexion = Conexion.Conectar())
            {
                using (SqlCommand comandoObjeto = new SqlCommand(comandoSQL, conexion))
                {
                    comandoObjeto.Parameters.AddWithValue("@IdUsuario", idUsuario);

                    try
                    {
                        //Ejecuta la consulta
                        comandoObjeto.ExecuteNonQuery();
                    }
                    catch (SqlException ex)
                    {
                        MessageBox.Show("Ocurrió un error al desactivar el usuario.\n\n" + ex.Message, "Error " + ex.Number, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
    }
}
