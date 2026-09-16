using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using System;
using System.Collections.Generic;

namespace Modelo.PDF
{
    public class CotizacionDocumentoPDF : IDocument
    {
        // ==============================
        // DATOS DE LA COTIZACIÓN
        // ==============================

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

        // ==============================
        // CONSTRUCTOR
        // ==============================

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

        // ==============================
        // METADATA
        // ==============================

        public DocumentMetadata GetMetadata()
        {
            return DocumentMetadata.Default;
        }

        // ==============================
        // SETTINGS
        // ==============================

        public DocumentSettings GetSettings()
        {
            return DocumentSettings.Default;
        }

        // ==============================
        // DOCUMENTO
        // ==============================

        public void Compose(IDocumentContainer container)
        {
            container.Page(page =>
            {
                page.Size(PageSizes.A4);

                page.Margin(35);

                // ==================================
                // ENCABEZADO
                // ==================================

                page.Header()
                    .Column(column =>
                    {
                        column.Item()
                            .AlignCenter()
                            .Text("MUEBLES KEYDA")
                            .FontSize(25)
                            .Bold();

                        column.Item()
                            .AlignCenter()
                            .PaddingTop(3)
                            .Text("COTIZACIÓN")
                            .FontSize(18)
                            .Bold();

                        column.Item()
                            .PaddingTop(10)
                            .LineHorizontal(1);
                    });

                // ==================================
                // CONTENIDO
                // ==================================

                page.Content()
                    .PaddingTop(20)
                    .Column(column =>
                    {
                        // ==================================
                        // INFORMACIÓN DE COTIZACIÓN
                        // ==================================

                        column.Item()
                            .Row(row =>
                            {
                                row.RelativeItem()
                                    .Text(text =>
                                    {
                                        text.Span("Cotización N.º: ")
                                            .Bold();

                                        text.Span(idCotizacion.ToString());
                                    });

                                row.RelativeItem()
                                    .AlignRight()
                                    .Text(text =>
                                    {
                                        text.Span("Fecha: ")
                                            .Bold();

                                        text.Span(fecha.ToString("dd/MM/yyyy"));
                                    });
                            });

                        // ==================================
                        // DATOS DEL CLIENTE
                        // ==================================

                        column.Item()
                            .PaddingTop(20)
                            .Border(1)
                            .Padding(10)
                            .Column(clienteColumn =>
                            {
                                clienteColumn.Item()
                                    .Text("DATOS DEL CLIENTE")
                                    .FontSize(13)
                                    .Bold();

                                clienteColumn.Item()
                                    .PaddingTop(8)
                                    .Text(text =>
                                    {
                                        text.Span("Cliente: ")
                                            .Bold();

                                        text.Span(cliente);
                                    });

                                clienteColumn.Item()
                                    .PaddingTop(4)
                                    .Text(text =>
                                    {
                                        text.Span("Teléfono: ")
                                            .Bold();

                                        text.Span(telefono);
                                    });

                                clienteColumn.Item()
                                    .PaddingTop(4)
                                    .Text(text =>
                                    {
                                        text.Span("Correo: ")
                                            .Bold();

                                        text.Span(correo);
                                    });

                                clienteColumn.Item()
                                    .PaddingTop(4)
                                    .Text(text =>
                                    {
                                        text.Span("Dirección: ")
                                            .Bold();

                                        text.Span(direccion);
                                    });
                            });

                        // ==================================
                        // DETALLE
                        // ==================================

                        column.Item()
                            .PaddingTop(20)
                            .Text("DETALLE DE LOS PRODUCTOS")
                            .FontSize(13)
                            .Bold();

                        // ==================================
                        // TABLA DE PRODUCTOS
                        // ==================================

                        column.Item()
                            .PaddingTop(8)
                            .Table(table =>
                            {
                                table.ColumnsDefinition(columns =>
                                {
                                    columns.RelativeColumn(3);
                                    columns.RelativeColumn(0.8f);
                                    columns.RelativeColumn(0.8f);
                                    columns.RelativeColumn(0.8f);
                                    columns.RelativeColumn(0.8f);
                                    columns.RelativeColumn(1.3f);
                                    columns.RelativeColumn(1.3f);
                                });

                                // ==============================
                                // CABECERA
                                // ==============================

                                table.Header(header =>
                                {
                                    header.Cell()
                                        .Background(Colors.Grey.Lighten2)
                                        .Padding(5)
                                        .Text("Producto")
                                        .Bold();

                                    header.Cell()
                                        .Background(Colors.Grey.Lighten2)
                                        .Padding(5)
                                        .AlignCenter()
                                        .Text("Largo")
                                        .Bold();

                                    header.Cell()
                                        .Background(Colors.Grey.Lighten2)
                                        .Padding(5)
                                        .AlignCenter()
                                        .Text("Ancho")
                                        .Bold();

                                    header.Cell()
                                        .Background(Colors.Grey.Lighten2)
                                        .Padding(5)
                                        .AlignCenter()
                                        .Text("Alto")
                                        .Bold();

                                    header.Cell()
                                        .Background(Colors.Grey.Lighten2)
                                        .Padding(5)
                                        .AlignCenter()
                                        .Text("Cant.")
                                        .Bold();

                                    header.Cell()
                                        .Background(Colors.Grey.Lighten2)
                                        .Padding(5)
                                        .AlignRight()
                                        .Text("P. Unit.")
                                        .Bold();

                                    header.Cell()
                                        .Background(Colors.Grey.Lighten2)
                                        .Padding(5)
                                        .AlignRight()
                                        .Text("Subtotal")
                                        .Bold();
                                });

                                // ==============================
                                // PRODUCTOS
                                // ==============================

                                foreach (var producto in productos)
                                {
                                    table.Cell()
                                        .BorderBottom(1)
                                        .Padding(5)
                                        .Text(producto.Descripcion);

                                    table.Cell()
                                        .BorderBottom(1)
                                        .Padding(5)
                                        .AlignCenter()
                                        .Text(producto.Largo.ToString());

                                    table.Cell()
                                        .BorderBottom(1)
                                        .Padding(5)
                                        .AlignCenter()
                                        .Text(producto.Ancho.ToString());

                                    table.Cell()
                                        .BorderBottom(1)
                                        .Padding(5)
                                        .AlignCenter()
                                        .Text(producto.Alto.ToString());

                                    table.Cell()
                                        .BorderBottom(1)
                                        .Padding(5)
                                        .AlignCenter()
                                        .Text(producto.Cantidad.ToString());

                                    table.Cell()
                                        .BorderBottom(1)
                                        .Padding(5)
                                        .AlignRight()
                                        .Text($"${producto.PrecioUnitario:0.00}");

                                    table.Cell()
                                        .BorderBottom(1)
                                        .Padding(5)
                                        .AlignRight()
                                        .Text($"${producto.SubTotal:0.00}");
                                }
                            });

                        // ==================================
                        // TOTALES
                        // ==================================

                        column.Item()
                            .PaddingTop(15)
                            .AlignRight()
                            .Column(totales =>
                            {
                                totales.Item()
                                    .Text(text =>
                                    {
                                        text.Span("Subtotal: ")
                                            .Bold();

                                        text.Span($"${subtotal:0.00}");
                                    });

                                totales.Item()
                                    .PaddingTop(5)
                                    .Text(text =>
                                    {
                                        text.Span("IVA (13%): ")
                                            .Bold();

                                        text.Span($"${iva:0.00}");
                                    });

                                totales.Item()
                                    .PaddingTop(5)
                                    .Text(text =>
                                    {
                                        text.Span("TOTAL: ")
                                            .Bold()
                                            .FontSize(14);

                                        text.Span($"${total:0.00}")
                                            .Bold()
                                            .FontSize(14);
                                    });
                            });

                        // ==================================
                        // CONDICIONES
                        // ==================================

                        column.Item()
                            .PaddingTop(25)
                            .Text("CONDICIONES")
                            .FontSize(13)
                            .Bold();

                        column.Item()
                            .PaddingTop(8)
                            .Text(text =>
                            {
                                text.Span("Condiciones de pago: ")
                                    .Bold();

                                text.Span(condicionesPago);
                            });

                        column.Item()
                            .PaddingTop(5)
                            .Text(text =>
                            {
                                text.Span("Condiciones de entrega: ")
                                    .Bold();

                                text.Span(condicionesEntrega);
                            });

                        column.Item()
                            .PaddingTop(5)
                            .Text(text =>
                            {
                                text.Span("Estado: ")
                                    .Bold();

                                text.Span(estado);
                            });
                    });

                // ==================================
                // PIE DE PÁGINA
                // ==================================

                page.Footer()
                    .AlignCenter()
                    .Text(text =>
                    {
                        text.Span("Muebles Keyda | Cotización N.º ");
                        text.Span(idCotizacion.ToString())
                            .Bold();
                    });
            });
        }

        // ==================================
        // GENERAR PDF
        // ==================================

        public static void Generar(
            string rutaPDF,
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
            var documento = new CotizacionDocumentoPDF(
                idCotizacion,
                fecha,
                cliente,
                telefono,
                correo,
                direccion,
                condicionesPago,
                condicionesEntrega,
                estado,
                subtotal,
                iva,
                total,
                productos);

            documento.GeneratePdf(rutaPDF);
        }
    }

    // ==================================
    // CLASE PARA LOS PRODUCTOS DEL PDF
    // ==================================

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