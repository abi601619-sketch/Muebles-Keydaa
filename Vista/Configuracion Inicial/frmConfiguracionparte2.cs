using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Vista.Configuracion_Inicial
{
    public partial class frmConfiguracionparte2 : Form
    {
        public frmConfiguracionparte2()
        {
            InitializeComponent();
        }

        private string rutaLogo = "";

        private void btnAtras_Click(object sender, EventArgs e)
        {
            ConfiguracionInicial frm = new ConfiguracionInicial();
            frm.ShowDialog();
        }



        private void btnSeleccionarLogo_Click(object sender, EventArgs e)
        {
            OpenFileDialog abrirImagen = new OpenFileDialog();

            // Solo permite imágenes JPG, JPEG y PNG
            abrirImagen.Filter =
                "Imágenes (*.jpg;*.jpeg;*.png)|*.jpg;*.jpeg;*.png";

            abrirImagen.FilterIndex = 1;

            // No permite seleccionar archivos que no sean imágenes
            abrirImagen.CheckFileExists = true;
            abrirImagen.CheckPathExists = true;

            if (abrirImagen.ShowDialog() == DialogResult.OK)
            {
                rutaLogo = abrirImagen.FileName;

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
                catch (Exception)
                {
                    MessageBox.Show(
                        "ERR-IMG-001: El archivo seleccionado no es una imagen válida.",
                        "Error de imagen",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error
                    );

                    rutaLogo = "";
                }
            }
        }


        private void btnSiguiente_Click(object sender, EventArgs e)
        {
            try
            {
                // Validar nombre
                if (string.IsNullOrWhiteSpace(txtNombreEmpresa.Text))
                {
                    MessageBox.Show(
                        "Ingrese el nombre de la empresa.",
                        "Configuración",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);

                    txtNombreEmpresa.Focus();
                    return;
                }

                // Validar teléfono
                if (string.IsNullOrWhiteSpace(txtTelefono.Text))
                {
                    MessageBox.Show(
                        "Ingrese el teléfono.",
                        "Configuración",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);

                    txtTelefono.Focus();
                    return;
                }

                // Validar correo
                if (string.IsNullOrWhiteSpace(txtCorreo.Text))
                {
                    MessageBox.Show(
                        "Ingrese el correo electrónico.",
                        "Configuración",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);

                    txtCorreo.Focus();
                    return;
                }

                // Validar dirección
                if (string.IsNullOrWhiteSpace(txtDireccion.Text))
                {
                    MessageBox.Show(
                        "Ingrese la dirección.",
                        "Configuración",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);

                    txtDireccion.Focus();
                    return;
                }

                // Validar logo
                if (string.IsNullOrWhiteSpace(rutaLogo))
                {
                    MessageBox.Show(
                        "Seleccione el logo de la empresa.",
                        "Configuración",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);

                    return;
                }

                // ==========================================
                // GUARDAR DATOS DE LA EMPRESA
                // ==========================================

                global::Modelo.Properties.Settings.Default.NombreEmpresa =
                    txtNombreEmpresa.Text;

                global::Modelo.Properties.Settings.Default.TelefonoEmpresa =
                    txtTelefono.Text;

                global::Modelo.Properties.Settings.Default.CorreoEmpresa =
                    txtCorreo.Text;

                global::Modelo.Properties.Settings.Default.DireccionEmpresa =
                    txtDireccion.Text;

                // ==========================================
                // GUARDAR RUTA DEL LOGO
                // ==========================================

                global::Modelo.Properties.Settings.Default.LogoEmpresa =
                    rutaLogo;

                // ==========================================
                // GUARDAR PERMANENTEMENTE
                // ==========================================

                global::Vista.Properties.Settings.Default.Save();

                MessageBox.Show(
                    "La configuración se guardó correctamente.",
                    "Configuración",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                // Abrir siguiente formulario
                ConfiguracionParte3 frm =
                    new ConfiguracionParte3();

                frm.Show();

                this.Hide();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Ocurrió un error al guardar la configuración:\n\n"
                    + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }

        }

        private void frmConfiguracionparte2_Load(object sender, EventArgs e)
        {
            // Cargar datos guardados
            txtNombreEmpresa.Text =
                Modelo.Properties.Settings.Default.NombreEmpresa;

            txtTelefono.Text =
                Modelo.Properties.Settings.Default.TelefonoEmpresa;

            txtCorreo.Text =
                Modelo.Properties.Settings.Default.CorreoEmpresa;

            txtDireccion.Text =
                Modelo.Properties.Settings.Default.DireccionEmpresa;

            // Cargar ruta del logo
            rutaLogo =
                Modelo.Properties.Settings.Default.LogoEmpresa;

            // Mostrar logo si existe
            if (!string.IsNullOrWhiteSpace(rutaLogo) &&
                File.Exists(rutaLogo))
            {
                using (Image imagenOriginal = Image.FromFile(rutaLogo))
                {
                    picLogo.Image = new Bitmap(imagenOriginal);
                }

                picLogo.SizeMode = PictureBoxSizeMode.Zoom;
            }
        }
    }



}


