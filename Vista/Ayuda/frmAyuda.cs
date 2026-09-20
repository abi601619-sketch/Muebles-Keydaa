using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using Vista.Responsive;

namespace Vista.Ayuda
{
    public partial class frmAyuda : Form
    {
        public frmAyuda()
        {
            InitializeComponent();
            ResponsiveHelper.Apply(this);
            ConfigurarTooltips();
            // Configuración del formulario
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = true;
        }

        private void ConfigurarTooltips()
        {
            // Crear ToolTip
            ToolTip toolTip1 = new ToolTip();

            // Propiedades del ToolTip
            toolTip1.AutoPopDelay = 5000;
            toolTip1.InitialDelay = 500;
            toolTip1.ReshowDelay = 200;
            toolTip1.ShowAlways = true;

            // Manuales
            toolTip1.SetToolTip(btnDescargarManualUsuario,
                "Descarga el manual de usuario del sistema.");

            toolTip1.SetToolTip(btnDescargarManualTecnico,
                "Descarga el manual técnico del sistema.");

            // Tutoriales
            toolTip1.SetToolTip(btnVerTuTorialRecuperar, "Muestra el tutorial para recuperar una contraseña.");

            toolTip1.SetToolTip(btnVerTutorialVenta, "Muestra el tutorial para registrar una venta.");

            toolTip1.SetToolTip(btnTutorialFactura, "Muestra el tutorial para generar una factura.");

            toolTip1.SetToolTip(btnVerTutorialCotizacion, "Muestra el tutorial para realizar una cotización.");
        }

        private void btnDescargarManualUsuario_Click(object sender, EventArgs e)
        {
            try
            {
                AbrirManual("ManualUsuario.pdf");
            }
            catch (FileNotFoundException)
            {
                MostrarError(1);
            }
            catch (Exception)
            {
                MostrarError(3);
            }
        }

        private void btnDescargarManualTecnico_Click(object sender, EventArgs e)
        {
            try
            {
                AbrirManual("ManualTecnico.pdf");
            }
            catch (FileNotFoundException)
            {
                MostrarError(2);
            }
            catch (Exception)
            {
                MostrarError(4);
            }
        }

        // ABRIR MANUAL
        private void AbrirManual(string nombreArchivo)
        {
            string ruta = Path.Combine(
                Application.StartupPath, "Recursos", "Manuales", nombreArchivo);

            if (!File.Exists(ruta))
            {
                throw new FileNotFoundException();
            }

            Process.Start(new ProcessStartInfo
            {
                FileName = ruta,
                UseShellExecute = true
            });
        }

        private void btnVerTuTorialRecuperar_Click(object sender, EventArgs e)
        {
            try
            {
                AbrirTutorial("https://www.youtube.com/");
            }
            catch (Exception)
            {
                MostrarError(6);
            }
        }

        private void btnVerTutorialVenta_Click(object sender, EventArgs e)
        {
            try
            {
                AbrirTutorial("https://www.youtube.com/");
            }
            catch (Exception)
            {
                MostrarError(6);
            }
        }

        private void btnTutorialFactura_Click(object sender, EventArgs e)
        {
            try
            {
                AbrirTutorial("https://www.youtube.com/");
            }
            catch (Exception)
            {
                MostrarError(6);
            }
        }

        private void btnVerTutorialCotizacion_Click(object sender, EventArgs e)
        {
            try
            {
                AbrirTutorial("https://www.youtube.com/");
            }
            catch (Exception)
            {
                MostrarError(6);
            }
        }
        // ABRIR TUTORIAL
        private void AbrirTutorial(string url)
        {
            if (string.IsNullOrWhiteSpace(url))
            {
                MostrarError(5);
                return;
            }

            Process.Start(new ProcessStartInfo
            {
                FileName = url,
                UseShellExecute = true
            });
        }

        // MOSTRAR ERRORES
        private void MostrarError(int error)
        {
            string mensaje = "";

            switch (error)
            {
                case 1:
                    mensaje = "No se encontró el Manual de Usuario.";
                    break;

                case 2:
                    mensaje = "No se encontró el Manual Técnico.";
                    break;

                case 3:
                    mensaje = "No se pudo abrir el Manual de Usuario.";
                    break;

                case 4:
                    mensaje = "No se pudo abrir el Manual Técnico.";
                    break;

                case 5:
                    mensaje = "El enlace del tutorial no está configurado.";
                    break;

                case 6:
                    mensaje = "No se pudo abrir el tutorial.";
                    break;

                default:
                    mensaje = "Ha ocurrido un error inesperado.";
                    break;
            }

            MessageBox.Show(
                mensaje,
                "Centro de Ayuda",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error
            );
        }

    }
}



