using System.Windows.Forms;

namespace Vista.Ayuda
{
    public partial class frmAyuda : Form
    {
        public frmAyuda()
        {
            InitializeComponent();
            ConfigurarTooltips();
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
            toolTip1.SetToolTip(button6,
                "Muestra el tutorial para recuperar una contraseña.");

            toolTip1.SetToolTip(btnVerTutorialVenta,
                "Muestra el tutorial para registrar una venta.");

            toolTip1.SetToolTip(btnTutorialFactura,
                "Muestra el tutorial para generar una factura.");

            toolTip1.SetToolTip(btnVerTutorialCotizacion,
                "Muestra el tutorial para realizar una cotización.");
        }
    }
}
