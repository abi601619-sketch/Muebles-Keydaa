using Modelo.Conexión_DB;
using Modelo.Entidades;
using System;
using System.Data.SqlClient;
using System.Windows.Forms;
using Vista.Responsive;

namespace Vista.Usuarios
{
    public partial class frmUsuarios : Form
    {
        public frmUsuarios()
        {
            InitializeComponent();
            ResponsiveHelper.Apply(this);
        }

        private void RegistrarUsuario()
        {
            string nombre = txtNombre.Text;
            string usuario = txtNombreUsuario.Text;
            string contraseña = txtContraseña.Text;
            bool estado = chkEstado.Checked;

            if (cmbRol.SelectedIndex == -1)
            {
                MessageBox.Show("Se debe de seleccionar un rol");
                return;
            }

            string rol = cmbRol.Text.Trim();
            if (rol != "Administrador" && rol != "Secretario")
            {
                MessageBox.Show("Se debe de seleccionar un rol válido");
                return;
            }


            if (string.IsNullOrWhiteSpace(nombre) || string.IsNullOrWhiteSpace(usuario) || string.IsNullOrWhiteSpace(contraseña))
            {
                MessageBox.Show("Es necesario rellenar los campos para continuar");
                return;
            }
            // Generar el hash con BCrypt
            string contraseñaHash = BCrypt.Net.BCrypt.HashPassword(contraseña);
            using (SqlConnection conexion = Conexion.Conectar())
            {
                string consulta = @"INSERT INTO Usuario (Nombre, Usuario, Contraseña, Rol, Estado) VALUES (@Nombre, @Usuario, @Contraseña, @Rol, @Estado)";
                using (SqlCommand comando = new SqlCommand(consulta, conexion))
                {
                    comando.Parameters.AddWithValue("@Nombre", nombre);
                    comando.Parameters.AddWithValue("@Usuario", usuario);
                    comando.Parameters.AddWithValue("@Contraseña", contraseñaHash);
                    comando.Parameters.AddWithValue("@Rol", rol);
                    comando.Parameters.AddWithValue("@Estado", chkEstado.Checked);

                    try
                    {

                        comando.ExecuteNonQuery();

                        MessageBox.Show("Usuario creado correctamente.", "Éxito");

                        txtNombre.Text = null;
                        txtNombreUsuario.Text = null;
                        txtContraseña.Text = null;
                    }
                    catch (SqlException ex)
                    {
                        if (ex.Number == 2627)
                        {
                            MessageBox.Show("El nombre de usuario ya está en uso.", "Error");
                        }
                        else
                        {
                            MessageBox.Show("Error al crear el usuario: " + ex.Message);
                        }
                    }

                    MostrarUsuarios();
                }
            }
        }



        private void btnGuardarUsuario_Click(object sender, EventArgs e)
        {
            RegistrarUsuario();
        }

        private void btnDesactivarUsuario_Click(object sender, EventArgs e)
        {
            if (dgvUsuariosRegistrados.SelectedRows.Count == 0)
            {
                MessageBox.Show("Seleccione un usario", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            int idUsuario = Convert.ToInt32(dgvUsuariosRegistrados.SelectedRows[0].Cells["IdUsuario"].Value);
            DialogResult resultado = MessageBox.Show("Este usuario se eliminará, ¿Estás seguro?", "Confirmar Eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (resultado == DialogResult.Yes)
            {
                EliminarUsuario(idUsuario);
            }
        }

        private void EliminarUsuario(int idUsuario)
        {
            using (SqlConnection conexion = Conexion.Conectar())
            {
                string consulta = "DELETE FROM Usuario WHERE IdUsuario = @IdUsuario";
                using (SqlCommand comando = new SqlCommand(consulta, conexion))
                {
                    comando.Parameters.AddWithValue("@IdUsuario", idUsuario);
                    try
                    {
                        conexion.Open();
                        comando.ExecuteNonQuery();
                        MessageBox.Show("Usuario desactivado correctamente");
                        MostrarUsuarios();
                    }
                    catch (SqlException ex)
                    {
                        MessageBox.Show("No fue posible eliminar el usuario: " + ex.Message);
                    }
                }
            }
        }

        private void MostrarUsuarios()
        {
            dgvUsuariosRegistrados.DataSource = null;
            dgvUsuariosRegistrados.DataSource = DbUsuarios.CargarUsuarios();
        }

        private void frmUsuarios_Load(object sender, EventArgs e)
        {
            MostrarUsuarios();
        }
    }
}
