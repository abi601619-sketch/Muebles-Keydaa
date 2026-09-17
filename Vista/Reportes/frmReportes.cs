using Modelo.Entidades;
using QuestPDF.Fluent;
using System;
using System.Data;
using System.Diagnostics;
using System.IO;
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
            btnConsultarVentas.Visible = false;
            btnExportarReporteVentas.Visible = false;
        }

        private void btnClientes_Click(object sender, EventArgs e)
        {
            //Ventas
            pnlReportesVentas.Visible = false;
            pnlBarraCambioVentas.Visible = false;
            btnExportarReporteVentas.Visible = false;
            btnConsultarVentas.Visible = false;

            //Clientes
            btnConsultar.Visible = true;
            pnlBarraCambiosClientes.Visible = true;
            pnlReporteDeClientes.Visible = true;
            btnExportarReporteClientes.Visible = true;


            //Cotizaciones
            pnlBarraCambiosCotizaciones.Visible = false;
            pnlReporteCotizaciones.Visible = false;
            btnConsultarCotizaciones.Visible = false;
            btnExportarCotizaciones.Visible = false;


        }

        private void btnVentas_Click(object sender, EventArgs e)
        {
            //Ventas
            pnlReportesVentas.Visible = true;
            pnlBarraCambioVentas.Visible = true;
            btnExportarReporteVentas.Visible = true;
            btnConsultarVentas.Visible = true;

            //Clientes
            btnConsultar.Visible = false;
            pnlBarraCambiosClientes.Visible = false;
            pnlReporteDeClientes.Visible = false;
            btnExportarReporteClientes.Visible = false;


            //Cotizaciones
            pnlBarraCambiosCotizaciones.Visible = false;
            pnlReporteCotizaciones.Visible = false;
            btnConsultarCotizaciones.Visible = false;
            btnExportarCotizaciones.Visible = false;
        }
        private void btnCotizaciones_Click(object sender, EventArgs e)
        {
            //Ventas
            pnlReportesVentas.Visible = false;
            pnlBarraCambioVentas.Visible = false;
            btnExportarReporteVentas.Visible = false;
            btnConsultarVentas.Visible = false;

            //Clientes
            btnConsultar.Visible = false;
            pnlBarraCambiosClientes.Visible = false;
            pnlReporteDeClientes.Visible = false;
            btnExportarReporteClientes.Visible = false;


            //Cotizaciones
            pnlBarraCambiosCotizaciones.Visible = true;
            pnlReporteCotizaciones.Visible = true;
            btnConsultarCotizaciones.Visible = true;
            btnExportarCotizaciones.Visible = true;
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

            dtpFechaFin.MaxDate = DateTime.Today;
            dtpFechaInicio.MaxDate = DateTime.Now;

            //Ventas
            pnlReportesVentas.Visible = false;
            pnlBarraCambioVentas.Visible = false;
            btnExportarReporteVentas.Visible = false;
            btnConsultarVentas.Visible = false;

            //Clientes
            btnConsultar.Visible = true;
            pnlBarraCambiosClientes.Visible = true;
            pnlReporteDeClientes.Visible = true;
            btnExportarReporteClientes.Visible = true;

            //Cotizaciones
            pnlBarraCambiosCotizaciones.Visible = false;
            pnlReporteCotizaciones.Visible = false;
            btnConsultarCotizaciones.Visible = false;
            btnExportarCotizaciones.Visible = false;


            dgvReporteVentas.Columns["IdVenta"].HeaderText = "N° de Venta";
            //dgvReporteVentas.Columns["N° FACTURA"].Visible = false;
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

        private void btnConsultar_Click(object sender, EventArgs e)
        {
            try
            {
                DateTime fechaInicio = dtpFechaInicio.Value.Date;
                DateTime fechaFin = dtpFechaFin.Value.Date;

                if (fechaInicio > fechaFin)
                {
                    MessageBox.Show(
                        "La fecha de inicio no puede ser mayor que la fecha final.",
                        "Período inválido",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );

                    return;
                }
                ReportesClientes reporte = new ReportesClientes();
                DataTable clientes = reporte.ObtenerClientesPorFecha(fechaInicio, fechaFin);


                if (clientes == null || clientes.Rows.Count == 0)
                {
                    MessageBox.Show(
                        "No existen clientes registrados durante el período seleccionado.",
                        "Sin resultados",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );

                    return;
                }

                dgvReporteClientes.DataSource = null;
                dgvReporteClientes.DataSource = clientes;
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Ocurrió un error al consultar el reporte de clientes:\n\n" +
                    ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        private void btnExportarReporteClientes_Click(object sender, EventArgs e)
        {
            try
            {
                DateTime fechaInicio = dtpFechaInicio.Value.Date;
                DateTime fechaFin = dtpFechaFin.Value.Date;

                // ==========================================
                // VALIDAR PERÍODO
                // ==========================================

                if (fechaInicio > fechaFin)
                {
                    MessageBox.Show(
                        "La fecha de inicio no puede ser mayor que la fecha final.",
                        "Período inválido",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );

                    return;
                }

                // ==========================================
                // OBTENER CLIENTES DEL PERÍODO
                // ==========================================

                ReportesClientes reporte = new ReportesClientes();

                DataTable clientes =
                    reporte.ObtenerClientesPorFecha(
                        fechaInicio,
                        fechaFin
                    );

                if (clientes == null || clientes.Rows.Count == 0)
                {
                    MessageBox.Show(
                        "No existen clientes registrados durante el período seleccionado.",
                        "Sin resultados",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );

                    return;
                }

                // ==========================================
                // OBTENER ESTADÍSTICAS
                // ==========================================

                DataTable estadisticas =
                    ReportesClientes.ObtenerEstadisticasClientes(
                        fechaInicio,
                        fechaFin
                    );

                int clientesTotales = 0;
                int clientesCorporativos = 0;
                int clientesIndividuales = 0;

                if (estadisticas != null &&
                    estadisticas.Rows.Count > 0)
                {
                    clientesTotales =
                        Convert.ToInt32(
                            estadisticas.Rows[0]["ClientesTotales"]
                        );

                    clientesCorporativos =
                        Convert.ToInt32(
                            estadisticas.Rows[0]["ClientesCorporativos"]
                        );

                    clientesIndividuales =
                        Convert.ToInt32(
                            estadisticas.Rows[0]["ClientesIndividuales"]
                        );
                }

                // ==========================================
                // CREAR CARPETA DE REPORTES
                // ==========================================

                string carpetaReportes =
                    Path.Combine(
                        Application.StartupPath,
                        "Reportes"
                    );

                if (!Directory.Exists(carpetaReportes))
                {
                    Directory.CreateDirectory(carpetaReportes);
                }

                // ==========================================
                // NOMBRE DEL ARCHIVO
                // ==========================================

                string nombreArchivo =
                    $"Reporte_Clientes_{fechaInicio:dd-MM-yyyy}_{fechaFin:dd-MM-yyyy}.pdf";

                string rutaArchivo =
                    Path.Combine(
                        carpetaReportes,
                        nombreArchivo
                    );

                // ==========================================
                // CREAR DOCUMENTO PDF
                // ==========================================

                ClientesDocumentoPDF documento =
                    new ClientesDocumentoPDF(
                        clientes,
                        fechaInicio,
                        fechaFin,
                        clientesTotales,
                        clientesCorporativos,
                        clientesIndividuales
                    );

                documento.GeneratePdf(rutaArchivo);

                // ==========================================
                // MENSAJE
                // ==========================================

                MessageBox.Show(
                    "El reporte de clientes se generó correctamente.\n\n" +
                    $"Período: {fechaInicio:dd/MM/yyyy} - {fechaFin:dd/MM/yyyy}\n\n" +
                    $"Clientes totales: {clientesTotales}\n" +
                    $"Clientes corporativos: {clientesCorporativos}\n" +
                    $"Clientes individuales: {clientesIndividuales}\n\n" +
                    $"Guardado en:\n{rutaArchivo}",
                    "Reporte generado",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );

                // ==========================================
                // ABRIR PDF
                // ==========================================

                Process.Start(new ProcessStartInfo
                {
                    FileName = rutaArchivo,
                    UseShellExecute = true
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Ocurrió un error al generar el reporte de clientes:\n\n" +
                    ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        private void btnConsultarVentas_Click(object sender, EventArgs e)
        {
            try
            {
                DateTime fechaInicio = dtpFechaInicio.Value.Date;
                DateTime fechaFin = dtpFechaFin.Value.Date;

                if (fechaInicio > fechaFin)
                {
                    MessageBox.Show(
                        "La fecha de inicio no puede ser mayor que la fecha de fin.",
                        "Rango de fechas",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );

                    return;
                }

                dgvReporteVentas.DataSource =
                    ReportesVentas.ObtenerVentasPorFecha(
                        fechaInicio,
                        fechaFin
                    );
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Ocurrió un error al consultar el reporte de ventas:\n\n" +
                    ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        private void btnExportarReporteVentas_Click(object sender, EventArgs e)
        {
            try
            {
                DateTime fechaInicio = dtpFechaInicio.Value.Date;
                DateTime fechaFin = dtpFechaFin.Value.Date;

                // ==========================================
                // VALIDAR PERÍODO
                // ==========================================

                if (fechaInicio > fechaFin)
                {
                    MessageBox.Show(
                        "La fecha de inicio no puede ser mayor que la fecha final.",
                        "Período inválido",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );

                    return;
                }

                // ==========================================
                // OBTENER VENTAS DEL PERÍODO
                // ==========================================

                DataTable ventas = ReportesVentas.ObtenerVentasPorFecha(
                    fechaInicio,
                    fechaFin
                );

                if (ventas == null || ventas.Rows.Count == 0)
                {
                    MessageBox.Show(
                        "No existen ventas registradas durante el período seleccionado.",
                        "Sin resultados",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );

                    return;
                }

                // ==========================================
                // OBTENER ESTADÍSTICAS
                // ==========================================

                DataTable estadisticas =
                    ReportesVentas.ObtenerEstadisticasVentas(
                        fechaInicio,
                        fechaFin
                    );

                int facturasEmitidas = 0;
                double totalVentas = 0;
                double ventaMasAlta = 0;

                if (estadisticas != null && estadisticas.Rows.Count > 0)
                {
                    facturasEmitidas = Convert.ToInt32(
                        estadisticas.Rows[0]["FacturasEmitidas"]
                    );

                    totalVentas = Convert.ToDouble(
                        estadisticas.Rows[0]["TotalVentas"]
                    );

                    ventaMasAlta = Convert.ToDouble(
                        estadisticas.Rows[0]["VentaMasAlta"]
                    );
                }

                // ==========================================
                // CREAR CARPETA DE REPORTES
                // ==========================================

                string carpetaReportes = Path.Combine(
                    Application.StartupPath,
                    "Reportes"
                );

                if (!Directory.Exists(carpetaReportes))
                {
                    Directory.CreateDirectory(carpetaReportes);
                }

                // ==========================================
                // NOMBRE DEL ARCHIVO
                // ==========================================

                string nombreArchivo =
                    $"Reporte_Ventas_{fechaInicio:dd-MM-yyyy}_{fechaFin:dd-MM-yyyy}.pdf";

                string rutaArchivo = Path.Combine(
                    carpetaReportes,
                    nombreArchivo
                );

                // ==========================================
                // CREAR DOCUMENTO
                // ==========================================

                VentasDocumentoPDF documento = new VentasDocumentoPDF(
                    ventas,
                    fechaInicio,
                    fechaFin,
                    facturasEmitidas,
                    totalVentas,
                    ventaMasAlta
                );

                documento.GeneratePdf(rutaArchivo);

                // ==========================================
                // MENSAJE
                // ==========================================

                MessageBox.Show(
                    "El reporte de ventas se generó correctamente.\n\n" +
                    $"Período: {fechaInicio:dd/MM/yyyy} - {fechaFin:dd/MM/yyyy}\n\n" +
                    $"Facturas emitidas: {facturasEmitidas}\n" +
                    $"Total de ventas: ${totalVentas:N2}\n" +
                    $"Venta más alta: ${ventaMasAlta:N2}\n\n" +
                    $"Guardado en:\n{rutaArchivo}",
                    "Reporte generado",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );

                // ==========================================
                // ABRIR PDF
                // ==========================================

                Process.Start(new ProcessStartInfo
                {
                    FileName = rutaArchivo,
                    UseShellExecute = true
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Ocurrió un error al generar el reporte:\n\n" +
                    ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }


    }
}

