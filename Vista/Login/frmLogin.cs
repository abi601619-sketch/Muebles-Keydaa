using Modelo.Conexión_DB;
using Modelo.Entidades;
using System;
using System.Data.SqlClient;
using System.Windows.Forms;
using Vista.Dashboard;
using Vista.DashboardSecretario;
using Vista.Responsive;

namespace Vista.Login
{
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
            ResponsiveHelper.Apply(this);
        }




        private void btnIngresar_Click(object sender, EventArgs e)
        {
        }

        private void btnIngresar_Click_1(object sender, EventArgs e)
        {
            IniciarSesion();

        }

        private void btnCerrarClientes_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void IniciarSesion()
        {
            string usuario = txtUsuario.Text.Trim();
            string contraseña = txtContraseña.Text;

            if (string.IsNullOrWhiteSpace(usuario))
            {
                MessageBox.Show("Ingrese su nombre de usuario.", "Campo requerido", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                txtUsuario.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(contraseña))
            {
                MessageBox.Show("Ingrese su contraseña.", "Campo requerido", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                txtContraseña.Focus();
                return;
            }

            using (SqlConnection conexion = Conexion.Conectar())
            {
                string consulta = @"SELECT IdUsuario, Nombre, Usuario, Contraseña, Rol, Estado FROM Usuario
                WHERE Usuario = @Usuario";

                using (SqlCommand comando = new SqlCommand(consulta, conexion))
                {
                    comando.Parameters.AddWithValue("@Usuario", usuario);

                    try
                    {


                        using (SqlDataReader lector = comando.ExecuteReader())
                        {
                            if (!lector.Read())
                            {
                                MessageBox.Show("El usuario o la contraseña son incorrectos.", "Inicio de sesión", MessageBoxButtons.OK, MessageBoxIcon.Error);

                                return;
                            }

                            // Obtener información del usuario
                            int idUsuario = Convert.ToInt32(lector["IdUsuario"]);
                            string nombre = lector["Nombre"].ToString();
                            string usuarioBD = lector["Usuario"].ToString();
                            string hashGuardado = lector["Contraseña"].ToString();
                            string rol = lector["Rol"].ToString();
                            bool estado = Convert.ToBoolean(lector["Estado"]);

                            // Con esa informacion , ahora si se puede comprobar si el usuario está activo o no , principalmente con el ESTADO
                            if (!estado)
                            {
                                MessageBox.Show("Este usuario se encuentra desactivado.", "Acceso denegado", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                                return;
                            }

                            // Verificar contraseña con BCrypt
                            bool contraseñaCorrecta = BCrypt.Net.BCrypt.Verify(contraseña, hashGuardado);
                            // Si algo sale mal y en caso la contraseña sea incorrecta
                            if (!contraseñaCorrecta)
                            {
                                MessageBox.Show("El usuario o la contraseña son incorrectos.", "Inicio de sesión", MessageBoxButtons.OK, MessageBoxIcon.Error);

                                txtContraseña.Clear();
                                txtContraseña.Focus();

                                return;
                            }

                            DbUsuarios usuarios = new DbUsuarios(idUsuario, nombre, usuarioBD, hashGuardado, rol, estado);

                            // Abrir Dashboard según el rol
                            if (rol == "Administrador")
                            {
                                frmDashboard dashboard = new frmDashboard();

                                dashboard.Show();
                            }
                            else if (rol == "Secretario")
                            {
                                frmDashboardSecretariocs dashboard = new frmDashboardSecretariocs();

                                dashboard.Show();
                            }

                            // Ocultar Login
                            this.Hide();

                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Ocurrió un error al iniciar sesión:\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
    }

}

