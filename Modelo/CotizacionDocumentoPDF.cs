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


        // =========================================================
        // CONSTRUCTOR
        // =========================================================

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


        // =========================================================
        // INFORMACIÓN DEL DOCUMENTO
        // =========================================================

        public DocumentMetadata GetMetadata()
        {
            return new DocumentMetadata
            {
                Title = $"Cotización #{idCotizacion}",

                Author = ConfiguracionEmpresa.Nombre,

                Subject = "Cotización de muebles"
            };
        }


        // =========================================================
        // CONFIGURACIÓN
        // =========================================================

        public DocumentSettings GetSettings()
        {
            return DocumentSettings.Default;
        }


        // =========================================================
        // CREAR DOCUMENTO
        // =========================================================

        public void Compose(IDocumentContainer container)
        {
            container
                .Page(page =>
                {
                    page.Size(PageSizes.A4);

                    page.Margin(35);


                    // ENCABEZADO
                    page.Header()
                        .Element(ConstruirEncabezado);


                    // CONTENIDO
                    page.Content()
                        .PaddingVertical(15)
                        .Element(ConstruirContenido);


                    // PIE DE PÁGINA
                    page.Footer()
                        .AlignCenter()
                        .Text(text =>
                        {
                            text.Span(
                                ConfiguracionEmpresa.Nombre +
                                " | Cotización "
                            );

                            text.Span($"#{idCotizacion}");
                        });
                });
        }


        // =========================================================
        // ENCABEZADO
        // =========================================================

        private void ConstruirEncabezado(IContainer container)
        {
            string rutaLogo =
                ConfiguracionEmpresa.Logo;


            // -----------------------------------------------------
            // COMPROBAR LOGO
            // -----------------------------------------------------

            if (string.IsNullOrWhiteSpace(rutaLogo) ||
                !File.Exists(rutaLogo))
            {
                throw new Exception(
                    "No se encontró el logo de la empresa.\n\n" +
                    "Configure nuevamente el logo de la empresa."
                );
            }


            // -----------------------------------------------------
            // CARGAR LOGO
            // -----------------------------------------------------

            byte[] logo = File.ReadAllBytes(rutaLogo);


            // -----------------------------------------------------
            // DISEÑO DEL ENCABEZADO
            // -----------------------------------------------------

            container
                .Background("#4A2C1A")
                .Padding(15)
                .Row(row =>
                {

                    // =================================================
                    // INFORMACIÓN DE LA EMPRESA
                    // =================================================

                    row.RelativeItem()
                        .Column(column =>
                        {

                            // LOGO
                            column.Item()
                                .Height(55)
                                .Width(180)
                                .AlignLeft()
                                .Image(logo)
                                .FitArea();


                            // NOMBRE DE LA EMPRESA
                            column.Item()
                                .PaddingTop(5)
                                .Text(
                                    ConfiguracionEmpresa.Nombre
                                )
                                .Bold()
                                .FontSize(11)
                                .FontColor("#F4DDC5");


                            // TELÉFONO
                            column.Item()
                                .Text(
                                    "Tel: " +
                                    ConfiguracionEmpresa.Telefono
                                )
                                .FontSize(7)
                                .FontColor("#F4DDC5");


                            // CORREO
                            column.Item()
                                .Text(
                                    ConfiguracionEmpresa.Correo
                                )
                                .FontSize(7)
                                .FontColor("#F4DDC5");


                            // DIRECCIÓN
                            column.Item()
                                .Text(
                                    ConfiguracionEmpresa.Direccion
                                )
                                .FontSize(7)
                                .FontColor("#F4DDC5");
                        });


                    // =================================================
                    // INFORMACIÓN DE LA COTIZACIÓN
                    // =================================================

                    row.ConstantItem(180)
                        .AlignRight()
                        .Column(column =>
                        {

                            column.Item()
                                .AlignRight()
                                .Text("COTIZACIÓN")
                                .Bold()
                                .FontSize(18)
                                .FontColor("#FFFFFF");


                            column.Item()
                                .PaddingTop(5)
                                .AlignRight()
                                .Text($"No. {idCotizacion}")
                                .FontSize(11)
                                .FontColor("#FFFFFF");


                            column.Item()
                                .AlignRight()
                                .Text(
                                    $"Fecha: {fecha:dd/MM/yyyy}"
                                )
                                .FontSize(10)
                                .FontColor("#F4DDC5");


                            column.Item()
                                .AlignRight()
                                .Text(
                                    $"Estado: {estado}"
                                )
                                .FontSize(10)
                                .FontColor("#F4DDC5");
                        });
                });
        }


        // =========================================================
        // CONTENIDO
        // =========================================================

        private void ConstruirContenido(IContainer container)
        {
            container.Column(column =>
            {

                // =================================================
                // DATOS DEL CLIENTE
                // =================================================

                column.Item()
                    .Element(ConstruirDatosCliente);


                // =================================================
                // TÍTULO
                // =================================================

                column.Item()
                    .PaddingTop(15)
                    .Text("DETALLE DE LA COTIZACIÓN")
                    .Bold()
                    .FontSize(12);


                // =================================================
                // TABLA DE PRODUCTOS
                // =================================================

                column.Item()
                    .PaddingTop(8)
                    .Element(ConstruirTablaProductos);


                // =================================================
                // TOTALES
                // =================================================

                column.Item()
                    .PaddingTop(15)
                    .AlignRight()
                    .Element(ConstruirTotales);


                // =================================================
                // CONDICIONES
                // =================================================

                column.Item()
                    .PaddingTop(20)
                    .Element(ConstruirCondiciones);
            });
        }


        // =========================================================
        // DATOS DEL CLIENTE
        // =========================================================

        private void ConstruirDatosCliente(IContainer container)
        {
            container
                .Border(1)
                .Padding(10)
                .Column(column =>
                {
                    column.Item()
                        .Text("DATOS DEL CLIENTE")
                        .Bold()
                        .FontSize(11);


                    column.Item()
                        .PaddingTop(5)
                        .Text($"Cliente: {cliente}");


                    column.Item()
                        .Text($"Teléfono: {telefono}");


                    column.Item()
                        .Text($"Correo: {correo}");


                    column.Item()
                        .Text($"Dirección: {direccion}");
                });
        }


        // =========================================================
        // TABLA DE PRODUCTOS
        // =========================================================

        private void ConstruirTablaProductos(IContainer container)
        {
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


                // -------------------------------------------------
                // ENCABEZADOS
                // -------------------------------------------------

                table.Header(header =>
                {
                    header.Cell()
                        .Element(EncabezadoTabla)
                        .Text("Producto");

                    header.Cell()
                        .Element(EncabezadoTabla)
                        .Text("Largo");

                    header.Cell()
                        .Element(EncabezadoTabla)
                        .Text("Ancho");

                    header.Cell()
                        .Element(EncabezadoTabla)
                        .Text("Alto");

                    header.Cell()
                        .Element(EncabezadoTabla)
                        .Text("Cant.");

                    header.Cell()
                        .Element(EncabezadoTabla)
                        .Text("P. Unit.");

                    header.Cell()
                        .Element(EncabezadoTabla)
                        .Text("Subtotal");
                });


                // -------------------------------------------------
                // PRODUCTOS
                // -------------------------------------------------

                foreach (ProductoPDF producto in productos)
                {
                    table.Cell()
                        .Element(CeldaTabla)
                        .Text(producto.Descripcion);


                    table.Cell()
                        .Element(CeldaTabla)
                        .Text($"{producto.Largo}");


                    table.Cell()
                        .Element(CeldaTabla)
                        .Text($"{producto.Ancho}");


                    table.Cell()
                        .Element(CeldaTabla)
                        .Text($"{producto.Alto}");


                    table.Cell()
                        .Element(CeldaTabla)
                        .Text($"{producto.Cantidad}");


                    table.Cell()
                        .Element(CeldaTabla)
                        .Text(
                            $"${producto.PrecioUnitario:N2}"
                        );


                    table.Cell()
                        .Element(CeldaTabla)
                        .Text(
                            $"${producto.SubTotal:N2}"
                        );
                }
            });
        }


        // =========================================================
        // ESTILO ENCABEZADO TABLA
        // =========================================================

        private IContainer EncabezadoTabla(IContainer container)
        {
            return container
                .Border(1)
                .Padding(5)
                .Background(Colors.Grey.Lighten2);
        }


        // =========================================================
        // ESTILO CELDA
        // =========================================================

        private IContainer CeldaTabla(IContainer container)
        {
            return container
                .Border(1)
                .Padding(5);
        }


        // =========================================================
        // TOTALES
        // =========================================================

        private void ConstruirTotales(IContainer container)
        {
            container
                .Width(220)
                .Column(column =>
                {

                    // SUBTOTAL
                    column.Item()
                        .Row(row =>
                        {
                            row.RelativeItem()
                                .Text("Subtotal:");


                            row.ConstantItem(90)
                                .AlignRight()
                                .Text(
                                    $"${subtotal:N2}"
                                );
                        });


                    // IVA
                    column.Item()
                        .PaddingTop(5)
                        .Row(row =>
                        {
                            row.RelativeItem()
                                .Text("IVA (13%):");


                            row.ConstantItem(90)
                                .AlignRight()
                                .Text(
                                    $"${iva:N2}"
                                );
                        });


                    // TOTAL
                    column.Item()
                        .PaddingTop(5)
                        .BorderTop(1)
                        .PaddingTop(5)
                        .Row(row =>
                        {
                            row.RelativeItem()
                                .Text("TOTAL")
                                .Bold();


                            row.ConstantItem(90)
                                .AlignRight()
                                .Text(
                                    $"${total:N2}"
                                )
                                .Bold();
                        });
                });
        }


        // =========================================================
        // CONDICIONES
        // =========================================================

        private void ConstruirCondiciones(IContainer container)
        {
            container
                .Border(1)
                .Padding(10)
                .Column(column =>
                {
                    column.Item()
                        .Text("CONDICIONES")
                        .Bold()
                        .FontSize(11);


                    column.Item()
                        .PaddingTop(5)
                        .Text(
                            $"Forma de pago: {condicionesPago}"
                        );


                    column.Item()
                        .Text(
                            $"Condiciones de entrega: " +
                            $"{condicionesEntrega}"
                        );
                });
        }


        // =========================================================
        // GENERAR PDF
        // =========================================================

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
            CotizacionDocumentoPDF documento =
                new CotizacionDocumentoPDF(
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
                    productos
                );

            documento.GeneratePdf(rutaPDF);
        }
    }


    // =============================================================
    // PRODUCTO PARA PDF
    // =============================================================

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