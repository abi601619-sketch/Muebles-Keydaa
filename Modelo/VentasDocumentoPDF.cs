using Modelo.Entidades;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using System;
using System.Data;
using System.IO;
using System.Windows.Forms;

namespace Modelo.PDF
{
    public class VentasDocumentoPDF : IDocument
    {
        private readonly DataTable ventas;
        private readonly DataTable estadisticas;
        private readonly DateTime fechaInicio;
        private readonly DateTime fechaFin;

        public VentasDocumentoPDF(
            DataTable ventas,
            DataTable estadisticas,
            DateTime fechaInicio,
            DateTime fechaFin,
            string rutaLogo)
        {
            this.ventas = ventas;
            this.estadisticas = estadisticas;
            this.fechaInicio = fechaInicio;
            this.fechaFin = fechaFin;
        }

        public DocumentMetadata GetMetadata()
        {
            return DocumentMetadata.Default;
        }

        public DocumentSettings GetSettings()
        {
            return DocumentSettings.Default;
        }

        public void Compose(IDocumentContainer container)
        {
            try
            {
                container.Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.Margin(32);
                    page.Header().Element(ConstruirEncabezado);
                    page.Content().Element(ConstruirContenido);
                    page.Footer().Element(ConstruirPie);
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se pudo construir el documento PDF.\n" + ex.Message, "ERR-PDF-001", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ConstruirEncabezado(IContainer contenedorPrincipal)
        {
            try
            {
                string rutaLogo = ConfiguracionEmpresa.Logo;

                if (string.IsNullOrWhiteSpace(rutaLogo) || !File.Exists(rutaLogo))
                {
                    MessageBox.Show("No se encontró el logo de la empresa.", "ERR-PDF-002", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                byte[] logo = File.ReadAllBytes(rutaLogo);

                contenedorPrincipal.Column(contenedor =>
                {
                    contenedor.Item().Background("#6B3517").Padding(15).Row(fila =>
                    {
                        fila.RelativeItem().Column(columna =>
                        {
                            columna.Item().Height(55).Width(180).AlignLeft().Image(logo).FitArea();
                            columna.Item().PaddingTop(5).Text(ConfiguracionEmpresa.Nombre).FontSize(10).Bold().FontColor("#F4DDC5");
                            columna.Item().Text("Tel: " + ConfiguracionEmpresa.Telefono).FontSize(7).FontColor("#F4DDC5");
                            columna.Item().Text(ConfiguracionEmpresa.Correo).FontSize(7).FontColor("#F4DDC5");
                            columna.Item().Text(ConfiguracionEmpresa.Direccion).FontSize(7).FontColor("#F4DDC5");
                        });

                        fila.RelativeItem().AlignMiddle().Column(columna =>
                        {
                            columna.Item().AlignRight().Text("REPORTE DE VENTAS").FontSize(19).Bold().FontColor(Colors.White);
                            columna.Item().PaddingTop(5).AlignRight().Text("Resumen de ventas").FontSize(9).FontColor(Colors.White);
                        });
                    });

                    contenedor.Item().PaddingTop(10).Height(32).Background("#F1E2D1").AlignCenter().AlignMiddle().DefaultTextStyle(x => x.FontSize(9).FontColor("#6B3517")).Text(texto =>
                    {
                        texto.Span("Del: ").Bold();
                        texto.Span(fechaInicio.ToString("dd/MM/yyyy"));
                        texto.Span("    Al: ").Bold();
                        texto.Span(fechaFin.ToString("dd/MM/yyyy"));
                    });
                });
            }
            catch (FileNotFoundException)
            {
                MessageBox.Show("No se encontró el archivo del logo.", "ERR-PDF-003", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (UnauthorizedAccessException)
            {
                MessageBox.Show("No se tiene permiso para acceder al logo de la empresa.", "ERR-PDF-004", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrió un error al construir el encabezado.\n" + ex.Message, "ERR-PDF-005", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ConstruirContenido(IContainer container)
        {
            try
            {
                container.PaddingTop(18).Column(column =>
                {
                    column.Item().Element(ConstruirEstadisticas);
                    column.Item().PaddingTop(20).Element(ConstruirDetalle);
                    column.Item().PaddingTop(12).AlignRight().Element(ConstruirTotalGeneral);
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrió un error al construir el contenido del reporte.\n" + ex.Message, "ERR-PDF-006", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ConstruirEstadisticas(IContainer container)
        {
            try
            {
                int facturas = 0;
                decimal totalVentas = 0;
                decimal ventaMasAlta = 0;

                if (estadisticas != null && estadisticas.Rows.Count > 0)
                {
                    DataRow fila = estadisticas.Rows[0];

                    facturas = Convert.ToInt32(fila["FacturasEmitidas"]);
                    totalVentas = Convert.ToDecimal(fila["TotalVentas"]);
                    ventaMasAlta = Convert.ToDecimal(fila["VentaMasAlta"]);
                }

                container.Row(row =>
                {
                    row.RelativeItem().Element(c => CrearTarjeta(c, "FACTURAS EMITIDAS", facturas.ToString()));
                    row.ConstantItem(8);
                    row.RelativeItem().Element(c => CrearTarjeta(c, "TOTAL DE VENTAS", $"${totalVentas:N2}"));
                    row.ConstantItem(8);
                    row.RelativeItem().Element(c => CrearTarjeta(c, "VENTA MÁS ALTA", $"${ventaMasAlta:N2}"));
                });
            }
            catch (FormatException)
            {
                MessageBox.Show("Los datos de las estadísticas tienen un formato incorrecto.", "ERR-PDF-007", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrió un error al construir las estadísticas.\n" + ex.Message, "ERR-PDF-008", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CrearTarjeta(IContainer container, string titulo, string valor)
        {
            container.Height(62).Background("#F1E2D1").Border(1).BorderColor("#D9BFA5").Padding(10).Column(column =>
            {
                column.Item().Text(titulo).FontSize(7).Bold().FontColor("#6B3517");
                column.Item().PaddingTop(5).Text(valor).FontSize(15).Bold().FontColor("#6B3517");
            });
        }

        private void ConstruirDetalle(IContainer container)
        {
            try
            {
                container.Column(column =>
                {
                    column.Item().Text("DETALLE DE VENTAS").FontSize(10).Bold().FontColor("#6B3517");
                    column.Item().PaddingTop(7).Element(ConstruirTabla);
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrió un error al construir el detalle de ventas.\n" + ex.Message, "ERR-PDF-009", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ConstruirTabla(IContainer container)
        {
            try
            {
                if (ventas == null)
                {
                    MessageBox.Show("No existen datos de ventas para generar el reporte.", "ERR-PDF-010", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                container.Table(table =>
                {
                    table.ColumnsDefinition(columns =>
                    {
                        columns.ConstantColumn(45);
                        columns.RelativeColumn(3);
                        columns.ConstantColumn(75);
                        columns.ConstantColumn(85);
                        columns.ConstantColumn(85);
                    });

                    table.Header(header =>
                    {
                        CeldaEncabezado(header.Cell(), "FACT.");
                        CeldaEncabezado(header.Cell(), "CLIENTE");
                        CeldaEncabezado(header.Cell(), "FECHA");
                        CeldaEncabezado(header.Cell(), "SUBTOTAL");
                        CeldaEncabezado(header.Cell(), "TOTAL");
                    });

                    foreach (DataRow fila in ventas.Rows)
                    {
                        string factura = fila["N° FACTURA"] == DBNull.Value ? "-" : fila["N° FACTURA"].ToString();
                        string cliente = fila["Nombre De Cliente"].ToString();
                        string fecha = Convert.ToDateTime(fila["FechaVenta"]).ToString("dd/MM/yyyy");
                        decimal subtotal = Convert.ToDecimal(fila["SubTotal"]);
                        decimal total = Convert.ToDecimal(fila["TotalAPagar"]);

                        CeldaDato(table.Cell(), factura);
                        CeldaDato(table.Cell(), cliente);
                        CeldaDato(table.Cell(), fecha);
                        CeldaDatoDerecha(table.Cell(), $"${subtotal:N2}");
                        CeldaDatoDerecha(table.Cell(), $"${total:N2}");
                    }
                });
            }
            catch (FormatException)
            {
                MessageBox.Show("Uno de los datos de las ventas tiene un formato incorrecto.", "ERR-PDF-011", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (InvalidCastException)
            {
                MessageBox.Show("No se pudo convertir uno de los datos de las ventas.", "ERR-PDF-012", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrió un error al construir la tabla de ventas.\n" + ex.Message, "ERR-PDF-013", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CeldaEncabezado(IContainer container, string texto)
        {
            container.Background("#6B3517").PaddingVertical(5).PaddingHorizontal(5).Text(texto).FontSize(7).Bold().FontColor(Colors.White);
        }

        private void CeldaDato(IContainer container, string texto)
        {
            container.BorderBottom(1).BorderColor("#E2D1C1").PaddingVertical(4).PaddingHorizontal(5).Text(texto).FontSize(7);
        }

        private void CeldaDatoDerecha(IContainer container, string texto)
        {
            container.BorderBottom(1).BorderColor("#E2D1C1").PaddingVertical(4).PaddingHorizontal(5).AlignRight().Text(texto).FontSize(7);
        }

        private void ConstruirTotalGeneral(IContainer container)
        {
            try
            {
                decimal totalGeneral = 0;

                if (ventas != null)
                {
                    foreach (DataRow fila in ventas.Rows)
                    {
                        totalGeneral += Convert.ToDecimal(fila["TotalAPagar"]);
                    }
                }

                container.Background("#F1E2D1").PaddingVertical(10).PaddingHorizontal(14).Row(row =>
                {
                    row.AutoItem().Text("TOTAL GENERAL:").FontSize(9).Bold().FontColor("#6B3517");
                    row.ConstantItem(15);
                    row.AutoItem().Text($"${totalGeneral:N2}").FontSize(12).Bold().FontColor("#6B3517");
                });
            }
            catch (FormatException)
            {
                MessageBox.Show("El total de ventas contiene un formato incorrecto.", "ERR-PDF-014", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrió un error al calcular el total general.\n" + ex.Message, "ERR-PDF-015", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ConstruirPie(IContainer container)
        {
            try
            {
                container.AlignCenter().Text($"Muebles Keyda | Reporte de Ventas | Generado: {DateTime.Now:dd/MM/yyyy HH:mm}").FontSize(7).FontColor("#6B3517");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrió un error al construir el pie del reporte.\n" + ex.Message, "ERR-PDF-016", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void GenerarPDF(string ruta)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(ruta))
                {
                    MessageBox.Show("La ruta del archivo PDF no puede estar vacía.", "ERR-PDF-017", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                Document.Create(Compose).GeneratePdf(ruta);
            }
            catch (UnauthorizedAccessException)
            {
                MessageBox.Show("No tiene permisos para guardar el archivo PDF en la ubicación seleccionada.", "ERR-PDF-018", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (DirectoryNotFoundException)
            {
                MessageBox.Show("No se encontró la carpeta donde se desea guardar el PDF.", "ERR-PDF-019", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (IOException)
            {
                MessageBox.Show("No se pudo crear o guardar el archivo PDF.", "ERR-PDF-020", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrió un error al generar el PDF.\n" + ex.Message, "ERR-PDF-021", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}