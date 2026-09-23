using Modelo.Entidades;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using System;
using System.Collections.Generic;
using System.IO;

namespace Modelo
{
    public class CotizacionDocumentoPDF : IDocument
    {
        private readonly int idCotizacion;
        private readonly DateTime fecha;
        private readonly string cliente;
        private readonly string telefono;
        private readonly string correo;
        private readonly string direccion;
        private readonly string condicionesPago;
        private readonly string condicionesEntrega;
        private readonly string estado;
        private readonly decimal subtotal;
        private readonly decimal iva;
        private readonly decimal total;
        private readonly List<ProductoPDF> productos;

        public CotizacionDocumentoPDF(
            int idCotizacion,
            DateTime fecha,
            string cliente,
            string telefono,
            string correo,
            string direccion,
            string condicionesPago,
            string condicionesEntrega,
            string estado,
            decimal subtotal,
            decimal iva,
            decimal total,
            List<ProductoPDF> productos)
        {
            this.idCotizacion = idCotizacion;
            this.fecha = fecha;
            this.cliente = cliente;
            this.telefono = telefono;
            this.correo = correo;
            this.direccion = direccion;
            this.condicionesPago = condicionesPago;
            this.condicionesEntrega = condicionesEntrega;
            this.estado = estado;
            this.subtotal = subtotal;
            this.iva = iva;
            this.total = total;
            this.productos = productos;
        }

        public DocumentMetadata GetMetadata()
        {
            return new DocumentMetadata
            {
                Title = $"Cotización #{idCotizacion}",
                Author = ConfiguracionEmpresa.Nombre,
                Subject = "Cotización de muebles"
            };
        }

        public DocumentSettings GetSettings()
        {
            return DocumentSettings.Default;
        }

        public void Compose(IDocumentContainer container)
        {
            container.Page(page =>
            {
                page.Size(PageSizes.A4);
                page.Margin(35);

                page.Header().Element(ConstruirEncabezado);
                page.Content().PaddingVertical(15).Element(ConstruirContenido);

                page.Footer().AlignCenter().Text(text =>
                {
                    text.Span(ConfiguracionEmpresa.Nombre + " | Cotización ");
                    text.Span($"#{idCotizacion}");
                });
            });
        }

        private void ConstruirEncabezado(IContainer container)
        {
            string rutaLogo = ConfiguracionEmpresa.Logo;

            if (string.IsNullOrWhiteSpace(rutaLogo) || !File.Exists(rutaLogo))
            {
                throw new FileNotFoundException($"Error PDF 001: No se encontró el logo de la empresa.\n\nConfigure nuevamente el logo de la empresa.", rutaLogo);
            }

            byte[] logo;

            try
            {
                logo = File.ReadAllBytes(rutaLogo);
            }
            catch (UnauthorizedAccessException ex)
            {
                throw new UnauthorizedAccessException("Error PDF 002: No tiene permisos para acceder al logo de la empresa.", ex);
            }
            catch (IOException ex)
            {
                throw new IOException("Error PDF 003: No se pudo leer el archivo del logo.", ex);
            }
            container.Background("#4A2C1A").Padding(15).Row(row =>
            {
                row.RelativeItem().Column(column =>
                {
                    column.Item().Height(55).Width(180).AlignLeft().Image(logo).FitArea();

                    column.Item().PaddingTop(5).Text(ConfiguracionEmpresa.Nombre).Bold().FontSize(11).FontColor("#F4DDC5");

                    column.Item().Text("Tel: " + ConfiguracionEmpresa.Telefono).FontSize(7).FontColor("#F4DDC5");

                    column.Item().Text(ConfiguracionEmpresa.Correo).FontSize(7).FontColor("#F4DDC5");

                    column.Item().Text(ConfiguracionEmpresa.Direccion).FontSize(7).FontColor("#F4DDC5");
                });

                row.ConstantItem(180).AlignRight().Column(column =>
                {
                    column.Item().AlignRight().Text("COTIZACIÓN").Bold().FontSize(18).FontColor("#FFFFFF");

                    column.Item().PaddingTop(5).AlignRight().Text($"No. {idCotizacion}").FontSize(11).FontColor("#FFFFFF");

                    column.Item().AlignRight().Text($"Fecha: {fecha:dd/MM/yyyy}").FontSize(10).FontColor("#F4DDC5");

                    column.Item().AlignRight().Text($"Estado: {estado}").FontSize(10).FontColor("#F4DDC5");
                });
            });
        }

        private void ConstruirContenido(IContainer container)
        {
            container.Column(column =>
            {
                column.Item().Element(ConstruirDatosCliente);

                column.Item().PaddingTop(15).Text("DETALLE DE LA COTIZACIÓN").Bold().FontSize(12);

                column.Item().PaddingTop(8).Element(ConstruirTablaProductos);

                column.Item().PaddingTop(15).AlignRight().Element(ConstruirTotales);

                column.Item().PaddingTop(20).Element(ConstruirCondiciones);
            });
        }

        private void ConstruirDatosCliente(IContainer container)
        {
            container.Border(1).Padding(10).Column(column =>
                {
                    column.Item().Text("DATOS DEL CLIENTE").Bold().FontSize(11);
                    column.Item().PaddingTop(5).Text($"Cliente: {cliente}");
                    column.Item().Text($"Teléfono: {telefono}");
                    column.Item().Text($"Correo: {correo}");
                    column.Item().Text($"Dirección: {direccion}");
                });
        }

        private void ConstruirTablaProductos(IContainer container)
        {
            if (productos == null || productos.Count == 0)
            {
                throw new InvalidOperationException("Error PDF 004: No existen productos para generar la cotización.");
            }

            container.Table(table =>
            {
                table.ColumnsDefinition(columns =>
                {
                    columns.RelativeColumn(3);
                    columns.ConstantColumn(45);
                    columns.ConstantColumn(45);
                    columns.ConstantColumn(45);
                    columns.ConstantColumn(45);
                    columns.ConstantColumn(70);
                    columns.ConstantColumn(70);
                });

                table.Header(header =>
                {
                    header.Cell().Element(EncabezadoTabla).Text("Producto");
                    header.Cell().Element(EncabezadoTabla).Text("Largo");
                    header.Cell().Element(EncabezadoTabla).Text("Ancho");
                    header.Cell().Element(EncabezadoTabla).Text("Alto");
                    header.Cell().Element(EncabezadoTabla).Text("Cant.");
                    header.Cell().Element(EncabezadoTabla).Text("P. Unit.");
                    header.Cell().Element(EncabezadoTabla).Text("Subtotal");
                });

                foreach (ProductoPDF producto in productos)
                {
                    table.Cell().Element(CeldaTabla).Text(producto.Descripcion);
                    table.Cell().Element(CeldaTabla).Text($"{producto.Largo}");
                    table.Cell().Element(CeldaTabla).Text($"{producto.Ancho}");
                    table.Cell().Element(CeldaTabla).Text($"{producto.Alto}");
                    table.Cell().Element(CeldaTabla).Text($"{producto.Cantidad}");
                    table.Cell().Element(CeldaTabla).Text($"${producto.PrecioUnitario:N2}");
                    table.Cell().Element(CeldaTabla).Text($"${producto.SubTotal:N2}");
                }
            });
        }

        private IContainer EncabezadoTabla(IContainer container)
        {
            return container.Border(1).Padding(5).Background(Colors.Grey.Lighten2);
        }

        private IContainer CeldaTabla(IContainer container)
        {
            return container.Border(1).Padding(5);
        }

        private void ConstruirTotales(IContainer container)
        {
            container.Width(220).Column(column =>
                {
                    column.Item().Row(row =>
                    {
                        row.RelativeItem().Text("Subtotal:");
                        row.ConstantItem(90).AlignRight().Text($"${subtotal:N2}");
                    });

                    column.Item().PaddingTop(5).Row(row =>
                    {
                        row.RelativeItem().Text("IVA (13%):");
                        row.ConstantItem(90).AlignRight().Text($"${iva:N2}");
                    });

                    column.Item().PaddingTop(5).BorderTop(1).PaddingTop(5).Row(row =>
                    {
                        row.RelativeItem().Text("TOTAL").Bold();
                        row.ConstantItem(90).AlignRight().Text($"${total:N2}").Bold();
                    });
                });
        }

        private void ConstruirCondiciones(IContainer container)
        {
            container.Border(1).Padding(10).Column(column =>
                {
                    column.Item().Text("CONDICIONES").Bold().FontSize(11);

                    column.Item().PaddingTop(5).Text($"Forma de pago: {condicionesPago}");

                    column.Item().Text($"Condiciones de entrega: {condicionesEntrega}");
                });
        }

        public static void Generar(string rutaPDF, int idCotizacion, DateTime fecha, string cliente, string telefono, string correo, string direccion,
            string condicionesPago, string condicionesEntrega, string estado, decimal subtotal, decimal iva, decimal total, List<ProductoPDF> productos)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(rutaPDF))
                {
                    throw new ArgumentException(
                        "Error PDF 005: La ruta del archivo PDF no puede estar vacía."
                    );
                }

                string directorio = Path.GetDirectoryName(rutaPDF);

                if (!string.IsNullOrWhiteSpace(directorio) &&
                    !Directory.Exists(directorio))
                {
                    Directory.CreateDirectory(directorio);
                }

                CotizacionDocumentoPDF documento = new CotizacionDocumentoPDF(idCotizacion, fecha, cliente, telefono, correo, direccion, condicionesPago, condicionesEntrega,
                    estado, subtotal, iva, total, productos);

                documento.GeneratePdf(rutaPDF);
            }
            catch (UnauthorizedAccessException ex)
            {
                throw new UnauthorizedAccessException("Error PDF 006: No tiene permisos para guardar el archivo PDF.", ex);
            }
            catch (DirectoryNotFoundException ex)
            {
                throw new DirectoryNotFoundException("Error PDF 007: No se encontró la carpeta donde se guardará el PDF.", ex);
            }
            catch (IOException ex)
            {
                throw new IOException("Error PDF 008: Ocurrió un problema al crear o guardar el archivo PDF.", ex);
            }
            catch (QuestPDF.Drawing.Exceptions.DocumentDrawingException ex)
            {
                throw new Exception("Error PDF 009: QuestPDF no pudo generar correctamente el documento.", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Error PDF 999: Ocurrió un error inesperado al generar el PDF.", ex);
            }
        }
    }

    public class ProductoPDF
    {
        public string Descripcion { get; set; }
        public int Largo { get; set; }
        public int Ancho { get; set; }
        public int Alto { get; set; }
        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
        public decimal SubTotal { get; set; }
    }
}