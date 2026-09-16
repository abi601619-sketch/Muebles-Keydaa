using Modelo.Entidades;
using System;
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
            string nombre = txtNombre.Text.Trim();
            string usuarioNombre = txtNombreUsuario.Text.Trim();
            string contraseña = txtContrasena.Text;
            bool estado = true;
            chkEstado.Checked = true;
            chkEstado.Visible = false;

            if (cmbRol.SelectedIndex == -1)
            {
                MessageBox.Show("Se debe seleccionar un rol");
                return;
            }

            string rol = cmbRol.Text.Trim();

            if (rol != "Administrador" && rol != "Secretario")
            {
                MessageBox.Show("Se debe seleccionar un rol válido");
                return;
            }

            if (string.IsNullOrWhiteSpace(nombre) || string.IsNullOrWhiteSpace(usuarioNombre) || string.IsNullOrWhiteSpace(contraseña))
            {
                MessageBox.Show(
                    "Es necesario rellenar los campos para continuar");
                return;
            }

            if (chkEstado.Checked == false)
            {
                MessageBox.Show("Es necesario marcar como activo al usuario inicialmente");
                return;
            }

            DbUsuarios nuevoUsuario = new DbUsuarios();

            nuevoUsuario.Nombre1 = nombre;
            nuevoUsuario.Usuario1 = usuarioNombre;
            nuevoUsuario.Contraseña1 = contraseña;
            nuevoUsuario.Rol1 = rol;
            nuevoUsuario.Estado1 = estado;

            nuevoUsuario.InsertarUsuario();

            txtNombre.Clear();
            txtNombreUsuario.Clear();
            txtContrasena.Clear();

            MostrarUsuarios();
        }



        private void btnGuardarUsuario_Click(object sender, EventArgs e)
        {
            RegistrarUsuario();
            LimpiarFormulario();
        }

        private void btnDesactivarUsuario_Click(object sender, EventArgs e)
        {
            try
            {
                // Verificar que haya una fila seleccionada
                if (dgvUsuariosRegistrados.CurrentRow == null)
                {
                    MessageBox.Show("Debe seleccionar un usuario.");
                    return;
                }

                // Obtener el IdUsuario de la fila actual
                int idUsuario = Convert.ToInt32(
                    dgvUsuariosRegistrados.CurrentRow.Cells["IdUsuario"].Value
                );

                // Preguntar si desea desactivar
                DialogResult respuesta = MessageBox.Show(
                    "¿Está seguro de desactivar este usuario?",
                    "Confirmar",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question
                );

                if (respuesta == DialogResult.Yes)
                {
                    DbUsuarios usuario = new DbUsuarios();

                    usuario.DesactivarUsuario(idUsuario);

                    MessageBox.Show(
                        "Usuario desactivado correctamente.",
                        "Información",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );

                    MostrarUsuarios();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Ocurrió un error al desactivar el usuario:\n\n" + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }


        private void MostrarUsuarios()
        {
            dgvUsuariosRegistrados.DataSource = null;
            dgvUsuariosRegistrados.DataSource = DbUsuarios.CargarUsuarios();
            chkEstado.Visible = false;
        }

        private void frmUsuarios_Load(object sender, EventArgs e)
        {
            MostrarUsuarios();
            chkEstado.Visible = false;
        }

        private void dgvUsuariosRegistrados_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }

            DataGridViewRow fila = dgvUsuariosRegistrados.Rows[e.RowIndex];

            txtNombre.Text = fila.Cells["Nombre"].Value?.ToString();
            txtNombreUsuario.Text = fila.Cells["Usuario"].Value?.ToString();
            cmbRol.Text = fila.Cells["Rol"].Value?.ToString();

            string estado = fila.Cells["Estado"].Value?.ToString();

            if (estado == "Activo")
            {
                chkEstado.Checked = true;
            }
            else
            {
                chkEstado.Checked = false;
            }

        }

        private void btnNuevoUsuario_Click(object sender, EventArgs e)
        {
            LimpiarFormulario();
        }

        private void LimpiarFormulario()
        {
            txtNombre.Clear();
            txtNombreUsuario.Clear();
            txtContrasena.Clear();
            cmbRol.SelectedIndex = -1;
        }
    }
}
