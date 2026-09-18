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

        // =========================================================
        // REGISTRAR USUARIO
        // =========================================================
        private void RegistrarUsuario()
        {
            try
            {
                string nombre = txtUsuario.Text.Trim();
                string usuarioNombre = txtUsuario.Text.Trim();
                string correo = txtCorreo.Text.Trim();
                string contraseña = txtContrasena.Text;

                // Todo usuario nuevo inicia activo
                bool estado = true;

                // -------------------------------------------------
                // VALIDAR NOMBRE
                // -------------------------------------------------
                if (string.IsNullOrWhiteSpace(nombre))
                {
                    MessageBox.Show(
                        "Debe ingresar el nombre del usuario.",
                        "Advertencia",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );

                    txtUsuario.Focus();
                    return;
                }

                // -------------------------------------------------
                // VALIDAR NOMBRE DE USUARIO
                // -------------------------------------------------
                if (string.IsNullOrWhiteSpace(usuarioNombre))
                {
                    MessageBox.Show(
                        "Debe ingresar un nombre de usuario.",
                        "Advertencia",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );

                    txtUsuario.Focus();
                    return;
                }

                // -------------------------------------------------
                // VALIDAR CORREO
                // -------------------------------------------------
                if (string.IsNullOrWhiteSpace(correo))
                {
                    MessageBox.Show(
                        "Debe ingresar un correo electrónico.",
                        "Advertencia",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );

                    txtCorreo.Focus();
                    return;
                }

                // Validación sencilla del correo
                if (!correo.Contains("@") || !correo.Contains("."))
                {
                    MessageBox.Show(
                        "Ingrese un correo electrónico válido.",
                        "Correo inválido",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );

                    txtCorreo.Focus();
                    return;
                }

                // -------------------------------------------------
                // VALIDAR CONTRASEÑA
                // -------------------------------------------------
                if (string.IsNullOrWhiteSpace(contraseña))
                {
                    MessageBox.Show(
                        "Debe ingresar una contraseña.",
                        "Advertencia",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );

                    txtContrasena.Focus();
                    return;
                }

                // -------------------------------------------------
                // VALIDAR ROL
                // -------------------------------------------------
                if (cmbRol.SelectedIndex == -1)
                {
                    MessageBox.Show(
                        "Se debe seleccionar un rol.",
                        "Advertencia",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );

                    cmbRol.Focus();
                    return;
                }

                string rol = cmbRol.Text.Trim();

                if (rol != "Administrador" &&
                    rol != "Secretario")
                {
                    MessageBox.Show(
                        "Se debe seleccionar un rol válido.",
                        "Advertencia",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );

                    cmbRol.Focus();
                    return;
                }

                // -------------------------------------------------
                // CREAR OBJETO USUARIO
                // -------------------------------------------------
                DbUsuarios nuevoUsuario = new DbUsuarios();

                nuevoUsuario.Nombre1 = nombre;
                nuevoUsuario.Usuario1 = usuarioNombre;
                nuevoUsuario.Correo = correo;
                nuevoUsuario.Contraseña1 = contraseña;
                nuevoUsuario.Rol1 = rol;
                nuevoUsuario.Estado1 = estado;

                // -------------------------------------------------
                // INSERTAR EN LA BASE DE DATOS
                // -------------------------------------------------
                nuevoUsuario.InsertarUsuario();

                MessageBox.Show(
                    "Usuario registrado correctamente.",
                    "Información",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );

                // -------------------------------------------------
                // LIMPIAR FORMULARIO
                // -------------------------------------------------
                LimpiarFormulario();

                // -------------------------------------------------
                // ACTUALIZAR TABLA
                // -------------------------------------------------
                MostrarUsuarios();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Ocurrió un error al registrar el usuario:\n\n" +
                    ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }


        // =========================================================
        // BOTÓN GUARDAR
        // =========================================================
        private void btnGuardarUsuario_Click(object sender, EventArgs e)
        {
            RegistrarUsuario();
        }


        // =========================================================
        // DESACTIVAR USUARIO
        // =========================================================
        private void btnDesactivarUsuario_Click(object sender, EventArgs e)
        {
            try
            {
                // Verificar que haya una fila seleccionada
                if (dgvUsuariosRegistrados.CurrentRow == null)
                {
                    MessageBox.Show(
                        "Debe seleccionar un usuario.",
                        "Advertencia",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );

                    return;
                }

                // Obtener el IdUsuario
                int idUsuario = Convert.ToInt32(
                    dgvUsuariosRegistrados
                    .CurrentRow
                    .Cells["IdUsuario"]
                    .Value
                );

                // Obtener el estado actual
                string estado = dgvUsuariosRegistrados
                    .CurrentRow
                    .Cells["Estado"]
                    .Value?
                    .ToString();

                // Si ya está inactivo
                if (estado == "Inactivo")
                {
                    MessageBox.Show(
                        "El usuario seleccionado ya está inactivo.",
                        "Información",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );

                    return;
                }

                // -------------------------------------------------
                // CONFIRMAR DESACTIVACIÓN
                // -------------------------------------------------
                DialogResult respuesta = MessageBox.Show(
                    "¿Está seguro de desactivar este usuario?",
                    "Confirmar desactivación",
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
                    LimpiarFormulario();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Ocurrió un error al desactivar el usuario:\n\n" +
                    ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }


        // =========================================================
        // MOSTRAR USUARIOS
        // =========================================================
        private void MostrarUsuarios()
        {
            try
            {
                dgvUsuariosRegistrados.DataSource = null;

                dgvUsuariosRegistrados.DataSource =
                    DbUsuarios.CargarUsuarios();

                // El estado se controla desde la base de datos
                // Los nuevos usuarios siempre se registran activos
                chkEstado.Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Ocurrió un error al cargar los usuarios:\n\n" +
                    ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }


        // =========================================================
        // CARGAR FORMULARIO
        // =========================================================
        private void frmUsuarios_Load(object sender, EventArgs e)
        {
            MostrarUsuarios();

            // Como los nuevos usuarios siempre comienzan activos,
            // no necesitamos mostrar este CheckBox.
            chkEstado.Visible = false;
        }


        // =========================================================
        // DOBLE CLIC EN UN USUARIO
        // =========================================================
        private void dgvUsuariosRegistrados_CellDoubleClick(
            object sender,
            DataGridViewCellEventArgs e)
        {
            try
            {
                // Evitar seleccionar encabezados
                if (e.RowIndex < 0)
                {
                    return;
                }

                DataGridViewRow fila =
                    dgvUsuariosRegistrados.Rows[e.RowIndex];

                // -------------------------------------------------
                // CARGAR DATOS DEL USUARIO
                // -------------------------------------------------

                txtUsuario.Text =
                    fila.Cells["Nombre"].Value?
                    .ToString();

                txtUsuario.Text =
                    fila.Cells["Usuario"].Value?
                    .ToString();

                txtCorreo.Text =
                    fila.Cells["Correo"].Value?
                    .ToString();

                cmbRol.Text =
                    fila.Cells["Rol"].Value?
                    .ToString();

                // -------------------------------------------------
                // CARGAR ESTADO
                // -------------------------------------------------

                string estado =
                    fila.Cells["Estado"].Value?
                    .ToString();

                if (estado == "Activo")
                {
                    chkEstado.Checked = true;
                }
                else
                {
                    chkEstado.Checked = false;
                }

                // -------------------------------------------------
                // NO MOSTRAR LA CONTRASEÑA
                // -------------------------------------------------

                txtContrasena.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Ocurrió un error al seleccionar el usuario:\n\n" +
                    ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }


        // =========================================================
        // BOTÓN NUEVO USUARIO
        // =========================================================
        private void btnNuevoUsuario_Click(object sender, EventArgs e)
        {
            LimpiarFormulario();

            txtUsuario.Focus();
        }


        // =========================================================
        // LIMPIAR FORMULARIO
        // =========================================================
        private void LimpiarFormulario()
        {
            txtUsuario.Clear();

            txtCorreo.Clear();

            txtContrasena.Clear();

            cmbRol.SelectedIndex = -1;

            chkEstado.Checked = false;
        }
    }
}