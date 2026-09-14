using Modelo.Entidades;
using System;
using System.Windows.Forms;
using Vista.Responsive;



namespace Vista.Reportes
{
    public partial class frmReportes : Form
    {
        public frmReportes()
        {
            InitializeComponent();
            ResponsiveHelper.Apply(this);
            btnVentas.Cursor = Cursors.Default;
        }

        private void btnClientes_Click(object sender, EventArgs e)
        {

            pnlReporteDeClientes.Visible = true;
            pnlReportesVentas.Visible = false;
            pnlBarraCambio.Visible = true;
            pnlBarraCambioVentas.Visible = false;


        }

        private void btnVentas_Click(object sender, EventArgs e)
        {
            pnlReporteDeClientes.Visible = false;
            pnlReportesVentas.Visible = true;
            pnlBarraCambioVentas.Visible = true;
            pnlBarraCambio.Visible = false;
        }

        public void CargarReporteClientes()
        {
            dgvReporteClientes.DataSource = null;
            dgvReporteClientes.DataSource = ReportesClientes.CargarReporteClientes();
        }

        public void CargarReporteVentas()
        {
            dgvReporteVentas.DataSource = null;
            dgvReporteVentas.DataSource = ReportesVentas.CargarReporteVentas();
        }

        private void frmReportes_Load(object sender, EventArgs e)
        {
            CargarReporteClientes();
            CargarReporteVentas();
            ActualizarEstadisticasClientes();
            ActualizarEstadisticasVentas();

            dtFechaFin.MaxDate = DateTime.Today;
            dtFechaInicio.MaxDate = DateTime.Now;

            dgvReporteClientes.Columns["TipoCliente"].HeaderText = "Tipo de Cliente";

            dgvReporteVentas.Columns["IdVenta"].HeaderText = "N° de Venta";

            dgvReporteVentas.Columns["N° FACTURA"].Visible = false;

            dgvReporteVentas.Columns["FechaVenta"].HeaderText = "Fecha de venta";

        }

        private void tableLayoutPanel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void lblClientesCorporativos_Click(object sender, EventArgs e)
        {

        }

        private void pbClientesFrecuentes_Click(object sender, EventArgs e)
        {

        }

        private void ActualizarEstadisticasClientes()
        {
            lblContadorTotal.Text = ReportesClientes.ContarClientesTotales().ToString();
            lblContadorCorporativos.Text = ReportesClientes.ContarClientesCorporativos().ToString();
            lblContadorIndividual.Text = ReportesClientes.ContarClientesIndividuales().ToString();
        }

        private void ActualizarEstadisticasVentas()
        {
            lblContadorVentasTotales.Text = ReportesVentas.ContarVentasTotales().ToString();
            lblMostrarFacturasEmitidas.Text = ReportesVentas.ContarFacturasEmitidas().ToString();
        }

    }
}

