using Modelo.Entidades;
using System;
using System.Data;
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
            ConfigurarTooltips();
        }

        // VARIABLES PARA LA PAGINACIÓN
        private DataTable dtUsuarios;
        private int paginaActual = 1;
        private int registrosPorPagina = 20;
        private int totalPaginas = 0;



        //CONFIGURAR TOOLTIPS
        private void ConfigurarTooltips()
        {
            // Crear ToolTip
            ToolTip toolTip1 = new ToolTip();

            // Propiedades del ToolTip
            toolTip1.AutoPopDelay = 5000;
            toolTip1.InitialDelay = 500;
            toolTip1.ReshowDelay = 200;
            toolTip1.ShowAlways = true;

            // Datos del usuario
            toolTip1.SetToolTip(txtUsuario, "Ingrese el nombre del usuario.");

            toolTip1.SetToolTip(txtCorreo, "Ingrese el correo electrónico del usuario.");

            toolTip1.SetToolTip(txtContrasena, "Ingrese la contraseña del usuario.");

            toolTip1.SetToolTip(cmbRol, "Seleccione el rol que tendrá el usuario.");

            toolTip1.SetToolTip(chkEstado, "Indica si el usuario se encuentra activo.");

            // Botones
            toolTip1.SetToolTip(btnNuevoUsuario, "Limpia el formulario para registrar un nuevo usuario.");

            toolTip1.SetToolTip(btnGuardarUsuario, "Guarda el nuevo usuario en el sistema.");

            toolTip1.SetToolTip(btnDesactivarUsuario, "Desactiva el usuario seleccionado.");

            // Tabla de usuarios
            toolTip1.SetToolTip(dgvUsuariosRegistrados, "Muestra los usuarios registrados. Haz doble clic en un usuario para consultar sus datos.");
        }

        // REGISTRAR USUARIO
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

                // VALIDAR NOMBRE
                if (string.IsNullOrWhiteSpace(nombre))
                {
                    MessageBox.Show("Debe ingresar el nombre del usuario.", "Advertencia",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    txtUsuario.Focus();
                    return;
                }

                // VALIDAR NOMBRE DE USUARIO
                if (string.IsNullOrWhiteSpace(usuarioNombre))
                {
                    MessageBox.Show("Debe ingresar un nombre de usuario.", "Advertencia",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    txtUsuario.Focus();
                    return;
                }

                // VALIDAR CORREO
                if (string.IsNullOrWhiteSpace(correo))
                {
                    MessageBox.Show("Debe ingresar un correo electrónico.", "Advertencia",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    txtCorreo.Focus();
                    return;
                }

                // Validación sencilla del correo
                if (!correo.Contains("@") || !correo.Contains("."))
                {
                    MessageBox.Show("Ingrese un correo electrónico válido.", "Correo inválido",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    txtCorreo.Focus();
                    return;
                }

                // VALIDAR CONTRASEÑA
                if (string.IsNullOrWhiteSpace(contraseña))
                {
                    MessageBox.Show("Debe ingresar una contraseña.", "Advertencia",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    txtContrasena.Focus();
                    return;
                }

                // VALIDAR ROL
                if (cmbRol.SelectedIndex == -1)
                {
                    MessageBox.Show("Se debe seleccionar un rol.", "Advertencia",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    cmbRol.Focus();
                    return;
                }

                string rol = cmbRol.Text.Trim();

                if (rol != "Administrador" &&
                    rol != "Secretario")
                {
                    MessageBox.Show("Se debe seleccionar un rol válido.", "Advertencia",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    cmbRol.Focus();
                    return;
                }

                // CREAR OBJETO USUARIO
                DbUsuarios nuevoUsuario = new DbUsuarios();

                nuevoUsuario.Nombre1 = nombre;
                nuevoUsuario.Usuario1 = usuarioNombre;
                nuevoUsuario.Correo = correo;
                nuevoUsuario.Contraseña1 = contraseña;
                nuevoUsuario.Rol1 = rol;
                nuevoUsuario.Estado1 = estado;

                // INSERTAR EN LA BASE DE DATOS
                nuevoUsuario.InsertarUsuario();

                MessageBox.Show("Usuario registrado correctamente.", "Información",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                // LIMPIAR FORMULARIO
                LimpiarFormulario();

                // ACTUALIZAR TABLA
                MostrarUsuarios();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrió un error al registrar el usuario:\n\n" + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        // BOTÓN GUARDAR
        private void btnGuardarUsuario_Click(object sender, EventArgs e)
        {
            RegistrarUsuario();
        }


        // DESACTIVAR USUARIO
        private void btnDesactivarUsuario_Click(object sender, EventArgs e)
        {
            try
            {
                // Verificar que haya una fila seleccionada
                if (dgvUsuariosRegistrados.CurrentRow == null)
                {
                    MessageBox.Show("Debe seleccionar un usuario.", "Advertencia",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    return;
                }

                // Obtener el IdUsuario
                int idUsuario = Convert.ToInt32(dgvUsuariosRegistrados.CurrentRow.Cells["IdUsuario"].Value);

                // Obtener el estado actual
                string estado = dgvUsuariosRegistrados.CurrentRow.Cells["Estado"].Value?.ToString();

                // Si ya está inactivo
                if (estado == "Inactivo")
                {
                    MessageBox.Show("El usuario seleccionado ya está inactivo.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    return;
                }

                // CONFIRMAR DESACTIVACIÓN
                DialogResult respuesta = MessageBox.Show("¿Está seguro de desactivar este usuario?", "Confirmar desactivación",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question
                );

                if (respuesta == DialogResult.Yes)
                {
                    DbUsuarios usuario = new DbUsuarios();

                    usuario.DesactivarUsuario(idUsuario);

                    MessageBox.Show("Usuario desactivado correctamente.", "Información",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);

                    MostrarUsuarios();
                    LimpiarFormulario();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrió un error al desactivar el usuario:\n\n" + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        // MOSTRAR USUARIOS
        private void MostrarUsuarios()
        {
            try
            {
                // Cargar todos los usuarios
                dtUsuarios = DbUsuarios.CargarUsuarios();

                // Volver a la primera página
                paginaActual = 1;

                // Calcular el total de páginas
                CalcularPaginasUsuarios();

                // Mostrar la primera página
                MostrarPaginaUsuarios();

                // El estado se controla desde la base de datos
                // Los nuevos usuarios siempre se registran activos
                chkEstado.Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrió un error al cargar los usuarios:\n\n" + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CalcularPaginasUsuarios()
        {
            if (dtUsuarios == null || dtUsuarios.Rows.Count == 0)
            {
                totalPaginas = 1;
                paginaActual = 1;
                return;
            }

            totalPaginas = (int)Math.Ceiling(
                (double)dtUsuarios.Rows.Count / registrosPorPagina
            );

            if (totalPaginas == 0)
                totalPaginas = 1;

            if (paginaActual > totalPaginas)
                paginaActual = totalPaginas;
        }

        private void MostrarPaginaUsuarios()
        {
            if (dtUsuarios == null)
                return;

            DataTable dtPagina = dtUsuarios.Clone();

            int inicio = (paginaActual - 1) * registrosPorPagina;

            int fin = Math.Min(
                inicio + registrosPorPagina,
                dtUsuarios.Rows.Count
            );

            for (int i = inicio; i < fin; i++)
            {
                dtPagina.ImportRow(dtUsuarios.Rows[i]);
            }

            // Mostrar únicamente los registros de la página actual
            dgvUsuariosRegistrados.DataSource = null;
            dgvUsuariosRegistrados.DataSource = dtPagina;

            // Mostrar página actual
            lblPagina.Text =
                $"Página {paginaActual} de {totalPaginas}";

            // Activar o desactivar botones
            btnAnterior.Enabled = paginaActual > 1;
            btnSiguiente.Enabled = paginaActual < totalPaginas;
        }

        private void btnAnterior_Click(object sender, EventArgs e)
        {
            if (paginaActual > 1)
            {
                paginaActual--;
                MostrarPaginaUsuarios();
            }
        }

        private void btnSiguiente_Click(object sender, EventArgs e)
        {
            if (paginaActual < totalPaginas)
            {
                paginaActual++;
                MostrarPaginaUsuarios();
            }
        }

        // CARGAR FORMULARIO
        private void frmUsuarios_Load(object sender, EventArgs e)
        {
            MostrarUsuarios();

            // Como los nuevos usuarios siempre comienzan activos,
            // no necesitamos mostrar este CheckBox.
            chkEstado.Visible = false;
        }


        // DOBLE CLIC EN UN USUARIO
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

                DataGridViewRow fila = dgvUsuariosRegistrados.Rows[e.RowIndex];

                // CARGAR DATOS DEL USUARIO

                txtUsuario.Text = fila.Cells["Nombre"].Value?.ToString();

                txtUsuario.Text = fila.Cells["Usuario"].Value?.ToString();

                txtCorreo.Text = fila.Cells["Correo"].Value?.ToString();

                cmbRol.Text = fila.Cells["Rol"].Value?.ToString();

                // CARGAR ESTADO

                string estado = fila.Cells["Estado"].Value?.ToString();

                if (estado == "Activo")
                {
                    chkEstado.Checked = true;
                }
                else
                {
                    chkEstado.Checked = false;
                }

                // NO MOSTRAR LA CONTRASEÑA

                txtContrasena.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrió un error al seleccionar el usuario:\n\n" + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // BOTÓN NUEVO USUARIO
        private void btnNuevoUsuario_Click(object sender, EventArgs e)
        {
            LimpiarFormulario();

            txtUsuario.Focus();
        }


        // LIMPIAR FORMULARIO
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