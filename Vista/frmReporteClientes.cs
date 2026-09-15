using Microsoft.Reporting.WinForms;
using Modelo.Entidades;
using System;
using System.Data;
using System.Windows.Forms;

namespace Vista
{
    public partial class frmReporteClientes : Form
    {

        private DataTable datosClientes;
        private void frmReporteClientes_Load(object sender, EventArgs e)
        {

            this.reportViewerClientes.RefreshReport();
        }

        public frmReporteClientes(DateTime fechaInicio, DateTime fechaFin)
        {
            InitializeComponent();

            ReportesClientes reporte = new ReportesClientes();

            datosClientes = reporte.ObtenerClientesPorFecha(fechaInicio, fechaFin);
        }

        private void reportViewerClientes_Load(object sender, EventArgs e)
        {
            reportViewerClientes.LocalReport.DataSources.Clear();

            ReportDataSource fuente =
                new ReportDataSource("MueblesKeydaDataSet", datosClientes);

            reportViewerClientes.LocalReport.DataSources.Add(fuente);

            reportViewerClientes.RefreshReport();
        }
    }
}
