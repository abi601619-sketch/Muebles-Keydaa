using Modelo.Entidades;
using System;
using System.Windows.Forms;

namespace Vista.Configuracion_Inicial
{
    public partial class ConfiguracionParte3 : Form
    {
        public ConfiguracionParte3()
        {
            InitializeComponent();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            frmConfiguracionparte2 frm = new frmConfiguracionparte2();
            frm.ShowDialog();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            // Verifica que se haya escrito el nombre.
            if (string.IsNullOrWhiteSpace(txtNombreAdministrador.Text))
            {
                MessageBox.Show("Ingrese el nombre completo del administrador.", "Campo obligatorio",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);

                txtNombreAdministrador.Focus();
                return;
            }

            // Verifica que se haya escrito el usuario.
            if (string.IsNullOrWhiteSpace(txtUsuario.Text))
            {
                MessageBox.Show("Ingrese un nombre de usuario.", "Campo obligatorio",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);

                txtUsuario.Focus();
                return;
            }

            // Verifica que se haya escrito una contraseña.
            if (string.IsNullOrWhiteSpace(txtContrasena.Text))
            {
                MessageBox.Show("Ingrese una contraseña.", "Campo obligatorio",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);

                txtContrasena.Focus();
                return;
            }

            // Verifica que se haya confirmado la contraseña.
            if (string.IsNullOrWhiteSpace(txtConfirmarContrasena.Text))
            {
                MessageBox.Show("Confirme la contraseña.", "Campo obligatorio",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);

                txtConfirmarContrasena.Focus();
                return;
            }

            // Verifica que ambas contraseñas sean iguales.
            if (txtContrasena.Text != txtConfirmarContrasena.Text)
            {
                MessageBox.Show("Las contraseñas no coinciden.", "Contraseña incorrecta",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);

                txtConfirmarContrasena.Focus();
                return;
            }

            // Verifica que el usuario haya aceptado crear la cuenta.
            if (!chkAceptarCondiciones.Checked)
            {
                MessageBox.Show("Debe aceptar la creación de la cuenta principal para continuar.", "Confirmación requerida",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);

                chkAceptarCondiciones.Focus();
                return;
            }

            // Crea un objeto de la clase DbUsuario.
            DbUsuarios usuario = new DbUsuarios();

            // Intenta crear el administrador inicial.
            bool creado = usuario.CrearAdministradorInicial(
                txtNombreAdministrador.Text.Trim(),
                txtUsuario.Text.Trim(),
                txtContrasena.Text
            );

            // Si el administrador se creó correctamente...
            if (creado)
            {
                MessageBox.Show("La cuenta de administrador se creó correctamente.\n\nAhora puede iniciar sesión.", "Configuración completada",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Oculta este formulario.
                this.Hide();

                ConfiguracionUltimaParte frm = new ConfiguracionUltimaParte();
                frm.ShowDialog();

            }
        }

        private void ConfiguracionParte3_Load(object sender, EventArgs e)
        {
            // Oculta la contraseña mostrando puntos.
            txtContrasena.UseSystemPasswordChar = true;

            // Oculta la confirmación de contraseña mostrando puntos.
            txtConfirmarContrasena.UseSystemPasswordChar = true;
        }
    }
}
