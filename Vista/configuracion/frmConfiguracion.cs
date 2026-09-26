using System;
using System.Drawing;
using System.IO;
using System.Net.Mail;
using System.Windows.Forms;
using Vista.Responsive;

namespace Vista.configuracion
{
    public partial class frmConfiguracion : Form
    {
        private string rutaLogo = "";
        public frmConfiguracion()
        {
            InitializeComponent();
            ResponsiveHelper.Apply(this);

        }

        private void CargarInformacionEmpresa()
        {
            txtNombreEmpresa.Text = Modelo.Properties.Settings.Default.NombreEmpresa;

            txtTelefonoEmpresa.Text = Modelo.Properties.Settings.Default.TelefonoEmpresa;

            txtCorreoEmpresa.Text =
                Modelo.Properties.Settings.Default.CorreoEmpresa;

            txtDireccionEmpresa.Text = Modelo.Properties.Settings.Default.DireccionEmpresa;

            rutaLogo = Modelo.Properties.Settings.Default.LogoEmpresa;

            // Mostrar logo
            if (!string.IsNullOrWhiteSpace(rutaLogo) && File.Exists(rutaLogo))
            {
                try
                {
                    if (picLogo.Image != null)
                    {
                        picLogo.Image.Dispose();
                        picLogo.Image = null;
                    }

                    using (Image imagenOriginal = Image.FromFile(rutaLogo))
                    {
                        picLogo.Image = new Bitmap(imagenOriginal);
                    }

                    picLogo.SizeMode = PictureBoxSizeMode.Zoom;
                }
                catch
                {
                    picLogo.Image = null;
                }
            }
        }

        private void btnGuardarLogo_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog abrirImagen = new OpenFileDialog())
            {
                abrirImagen.Filter = "Imágenes (*.jpg;*.jpeg;*.png)|*.jpg;*.jpeg;*.png";

                abrirImagen.FilterIndex = 1;
                abrirImagen.CheckFileExists = true;
                abrirImagen.CheckPathExists = true;

                if (abrirImagen.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        rutaLogo = abrirImagen.FileName;

                        if (picLogo.Image != null)
                        {
                            picLogo.Image.Dispose();
                            picLogo.Image = null;
                        }

                        using (Image imagenOriginal =
                               Image.FromFile(rutaLogo))
                        {
                            picLogo.Image = new Bitmap(imagenOriginal);
                        }

                        picLogo.SizeMode = PictureBoxSizeMode.Zoom;

                        errorProvider1.SetError(btnGuardarLogo, "");
                    }
                    catch
                    {
                        rutaLogo = "";

                        MessageBox.Show("El archivo seleccionado no es una imagen válida.", "ERR-IMG-001", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void frmConfiguracion_Load(object sender, EventArgs e)
        {
            CargarInformacionEmpresa();
            ConfigurarTooltips();
        }

        private void btnGuardarCambioInformacion_Click(object sender, EventArgs e)
        {
            try
            {

                // Validar nombre
                if (string.IsNullOrWhiteSpace(txtNombreEmpresa.Text))
                {
                    errorProvider1.SetError(txtNombreEmpresa, "Campo obligatorio");

                    txtNombreEmpresa.Focus();
                    return;
                }

                // Validar teléfono
                if (string.IsNullOrWhiteSpace(txtTelefonoEmpresa.Text))
                {
                    errorProvider1.SetError(txtTelefonoEmpresa, "Campo obligatorio");

                    txtTelefonoEmpresa.Focus();
                    return;
                }

                // Validar correo
                if (string.IsNullOrWhiteSpace(txtCorreoEmpresa.Text))
                {
                    errorProvider1.SetError(txtCorreoEmpresa, "Campo obligatorio");

                    txtCorreoEmpresa.Focus();
                    return;
                }

                if (!ValidarCorreo())
                {
                    return;
                }

                // Validar dirección
                if (string.IsNullOrWhiteSpace(txtDireccionEmpresa.Text))
                {
                    errorProvider1.SetError(txtDireccionEmpresa, "Campo obligatorio");

                    txtDireccionEmpresa.Focus();
                    return;
                }

                // Validar logo
                if (string.IsNullOrWhiteSpace(rutaLogo))
                {
                    errorProvider1.SetError(btnGuardarLogo, "Seleccione el logo de la empresa");

                    btnGuardarLogo.Focus();
                    return;
                }

                if (!File.Exists(rutaLogo))
                {
                    errorProvider1.SetError(btnGuardarLogo, "El archivo del logo no existe");

                    return;
                }

                // GUARDAR CAMBIOS
                Modelo.Properties.Settings.Default.NombreEmpresa = txtNombreEmpresa.Text.Trim();

                Modelo.Properties.Settings.Default.TelefonoEmpresa = txtTelefonoEmpresa.Text.Trim();

                Modelo.Properties.Settings.Default.CorreoEmpresa = txtCorreoEmpresa.Text.Trim();

                Modelo.Properties.Settings.Default.DireccionEmpresa = txtDireccionEmpresa.Text.Trim();

                Modelo.Properties.Settings.Default.LogoEmpresa = rutaLogo;

                // Guardar permanentemente
                Modelo.Properties.Settings.Default.Save();

                MessageBox.Show("La información de la empresa se actualizó correctamente.", "Configuración", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrió un error al guardar la información.\n\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }

        private bool ValidarCorreo()
        {
            try
            {
                MailAddress correo = new MailAddress(txtCorreoEmpresa.Text.Trim());

                if (correo.Address != txtCorreoEmpresa.Text.Trim())
                {
                    errorProvider1.SetError(txtCorreoEmpresa, "Ingrese un correo electrónico válido");

                    txtCorreoEmpresa.Focus();

                    return false;
                }

                return true;
            }
            catch
            {
                errorProvider1.SetError(txtCorreoEmpresa, "Ingrese un correo electrónico válido");

                txtCorreoEmpresa.Focus();

                return false;
            }
        }

        private void txtNombreEmpresa_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) &&
              !char.IsWhiteSpace(e.KeyChar) &&
              !char.IsControl(e.KeyChar) &&
              e.KeyChar != '.' &&
              e.KeyChar != '&' &&
              e.KeyChar != '-')
            {
                e.Handled = true;
            }
        }

        private void txtTelefonoEmpresa_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) &&
                !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtTelefonoEmpresa_TextChanged(object sender, EventArgs e)
        {
            string telefono = txtTelefonoEmpresa.Text.Replace("-", "");

            if (telefono.Length > 8)
            {
                telefono = telefono.Substring(0, 8);
            }

            if (telefono.Length > 4)
            {
                telefono = telefono.Insert(4, "-");
            }

            if (txtTelefonoEmpresa.Text != telefono)
            {
                txtTelefonoEmpresa.Text = telefono;
                txtTelefonoEmpresa.SelectionStart = txtTelefonoEmpresa.Text.Length;
            }
        }

        private void txtCorreoEmpresa_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetterOrDigit(e.KeyChar) &&
                  !char.IsControl(e.KeyChar) &&
                  e.KeyChar != '@' &&
                  e.KeyChar != '.' &&
                  e.KeyChar != '_' &&
                  e.KeyChar != '-' &&
                  e.KeyChar != '+')
            {
                e.Handled = true;
            }
        }

        private void txtDireccionEmpresa_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetterOrDigit(e.KeyChar) &&
             !char.IsWhiteSpace(e.KeyChar) &&
             !char.IsControl(e.KeyChar) &&
             e.KeyChar != ',' &&
             e.KeyChar != '.' &&
             e.KeyChar != '#' &&
             e.KeyChar != '-' &&
             e.KeyChar != '/')
            {
                e.Handled = true;
            }
        }

        private void ConfigurarTooltips()
        {
            ToolTip toolTip = new ToolTip();

            toolTip.AutoPopDelay = 5000;
            toolTip.InitialDelay = 500;
            toolTip.ReshowDelay = 200;
            toolTip.ShowAlways = true;

            toolTip.SetToolTip(txtNombreEmpresa, "Ingrese el nombre de la empresa.");
            toolTip.SetToolTip(txtTelefonoEmpresa, "Ingrese el número de teléfono de la empresa.");
            toolTip.SetToolTip(txtCorreoEmpresa, "Ingrese el correo electrónico de la empresa.");
            toolTip.SetToolTip(txtDireccionEmpresa, "Ingrese la dirección de la empresa.");
            toolTip.SetToolTip(btnGuardarLogo, "Seleccione el logo de la empresa.");
            toolTip.SetToolTip(picLogo, "Vista previa del logo de la empresa.");
            toolTip.SetToolTip(btnGuardarCambioInformacion, "Guarda los cambios de la información de la empresa.");
        }
    }
}

