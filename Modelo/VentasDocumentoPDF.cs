using Modelo.Entidades;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using System;
using System.Data;
using System.IO;

namespace Modelo.PDF
{
    public class VentasDocumentoPDF : IDocument
    {
        private readonly DataTable ventas;
        private readonly DataTable estadisticas;
        private readonly DateTime fechaInicio;
        private readonly DateTime fechaFin;


        // =========================================================
        // CONSTRUCTOR
        // =========================================================

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


        // =========================================================
        // METADATA
        // =========================================================

        public DocumentMetadata GetMetadata()
        {
            return DocumentMetadata.Default;
        }


        public DocumentSettings GetSettings()
        {
            return DocumentSettings.Default;
        }


        // =========================================================
        // COMPOSE
        // =========================================================

        public void Compose(IDocumentContainer container)
        {
            container.Page(page =>
            {
                page.Size(PageSizes.A4);

                page.Margin(32);

                page.Header()
                    .Element(ConstruirEncabezado);

                page.Content()
                    .Element(ConstruirContenido);

                page.Footer()
                    .Element(ConstruirPie);
            });
        }


        private void ConstruirEncabezado(IContainer contenedorPrincipal)
        {
            string rutaLogo = ConfiguracionEmpresa.Logo;

            if (string.IsNullOrWhiteSpace(rutaLogo) ||
                !File.Exists(rutaLogo))
            {
                throw new Exception(
                    "No se encontró el logo de la empresa.\n\n" +
                    "Configure nuevamente el logo de la empresa."
                );
            }

            byte[] logo = File.ReadAllBytes(rutaLogo);

            contenedorPrincipal
                .Column(contenedor =>
                {
                    // =================================================
                    // ENCABEZADO PRINCIPAL
                    // =================================================

                    contenedor.Item()
                        .Background("#6B3517")
                        .Padding(15)
                        .Row(fila =>
                        {
                            // -----------------------------------------
                            // EMPRESA
                            // -----------------------------------------

                            fila.RelativeItem()
                                .Column(columna =>
                                {
                                    columna.Item()
                                        .Height(55)
                                        .Width(180)
                                        .AlignLeft()
                                        .Image(logo)
                                        .FitArea();

                                    columna.Item()
                                        .PaddingTop(5)
                                        .Text(ConfiguracionEmpresa.Nombre)
                                        .FontSize(10)
                                        .Bold()
                                        .FontColor("#F4DDC5");

                                    columna.Item()
                                        .Text(
                                            "Tel: " +
                                            ConfiguracionEmpresa.Telefono
                                        )
                                        .FontSize(7)
                                        .FontColor("#F4DDC5");

                                    columna.Item()
                                        .Text(
                                            ConfiguracionEmpresa.Correo
                                        )
                                        .FontSize(7)
                                        .FontColor("#F4DDC5");

                                    columna.Item()
                                        .Text(
                                            ConfiguracionEmpresa.Direccion
                                        )
                                        .FontSize(7)
                                        .FontColor("#F4DDC5");
                                });

                            // -----------------------------------------
                            // TÍTULO DEL REPORTE
                            // -----------------------------------------

                            fila.RelativeItem()
                                .AlignMiddle()
                                .Column(columna =>
                                {
                                    columna.Item()
                                        .AlignRight()
                                        .Text("REPORTE DE VENTAS")
                                        .FontSize(19)
                                        .Bold()
                                        .FontColor(Colors.White);

                                    columna.Item()
                                        .PaddingTop(5)
                                        .AlignRight()
                                        .Text("Resumen de ventas")
                                        .FontSize(9)
                                        .FontColor(Colors.White);
                                });
                        });

                    // =================================================
                    // RANGO DE FECHAS
                    // =================================================

                    contenedor.Item()
                        .PaddingTop(10)
                        .Height(32)
                        .Background("#F1E2D1")
                        .AlignCenter()
                        .AlignMiddle()
                        .DefaultTextStyle(x => x
                            .FontSize(9)
                            .FontColor("#6B3517"))
                        .Text(texto =>
                        {
                            texto.Span("Del: ")
                                .Bold();

                            texto.Span(
                                fechaInicio.ToString("dd/MM/yyyy")
                            );

                            texto.Span("    Al: ")
                                .Bold();

                            texto.Span(
                                fechaFin.ToString("dd/MM/yyyy")
                            );
                        });
                });
        }

        // =========================================================
        // CONTENIDO
        // =========================================================

        private void ConstruirContenido(IContainer container)
        {
            container
                .PaddingTop(18)
                .Column(column =>
                {
                    column.Item()
                        .Element(ConstruirEstadisticas);


                    column.Item()
                        .PaddingTop(20)
                        .Element(ConstruirDetalle);


                    column.Item()
                        .PaddingTop(12)
                        .AlignRight()
                        .Element(ConstruirTotalGeneral);
                });
        }


        // =========================================================
        // ESTADÍSTICAS
        // =========================================================

        private void ConstruirEstadisticas(IContainer container)
        {
            int facturas = 0;
            decimal totalVentas = 0;
            decimal ventaMasAlta = 0;


            if (estadisticas != null &&
                estadisticas.Rows.Count > 0)
            {
                DataRow fila =
                    estadisticas.Rows[0];

                facturas =
                    Convert.ToInt32(
                        fila["FacturasEmitidas"]);

                totalVentas =
                    Convert.ToDecimal(
                        fila["TotalVentas"]);

                ventaMasAlta =
                    Convert.ToDecimal(
                        fila["VentaMasAlta"]);
            }


            container.Row(row =>
            {
                row.RelativeItem()
                    .Element(c =>
                        CrearTarjeta(
                            c,
                            "FACTURAS EMITIDAS",
                            facturas.ToString()));


                row.ConstantItem(8);


                row.RelativeItem()
                    .Element(c =>
                        CrearTarjeta(
                            c,
                            "TOTAL DE VENTAS",
                            $"${totalVentas:N2}"));


                row.ConstantItem(8);


                row.RelativeItem()
                    .Element(c =>
                        CrearTarjeta(
                            c,
                            "VENTA MÁS ALTA",
                            $"${ventaMasAlta:N2}"));
            });
        }


        // =========================================================
        // TARJETA
        // =========================================================

        private void CrearTarjeta(
            IContainer container,
            string titulo,
            string valor)
        {
            container
                .Height(62)
                .Background("#F1E2D1")
                .Border(1)
                .BorderColor("#D9BFA5")
                .Padding(10)
                .Column(column =>
                {
                    column.Item()
                        .Text(titulo)
                        .FontSize(7)
                        .Bold()
                        .FontColor("#6B3517");

                    column.Item()
                        .PaddingTop(5)
                        .Text(valor)
                        .FontSize(15)
                        .Bold()
                        .FontColor("#6B3517");
                });
        }


        // =========================================================
        // DETALLE
        // =========================================================

        private void ConstruirDetalle(IContainer container)
        {
            container.Column(column =>
            {
                column.Item()
                    .Text("DETALLE DE VENTAS")
                    .FontSize(10)
                    .Bold()
                    .FontColor("#6B3517");


                column.Item()
                    .PaddingTop(7)
                    .Element(ConstruirTabla);
            });
        }


        // =========================================================
        // TABLA
        // =========================================================

        private void ConstruirTabla(IContainer container)
        {
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


                // ENCABEZADOS
                table.Header(header =>
                {
                    CeldaEncabezado(
                        header.Cell(),
                        "FACT.");

                    CeldaEncabezado(
                        header.Cell(),
                        "CLIENTE");

                    CeldaEncabezado(
                        header.Cell(),
                        "FECHA");

                    CeldaEncabezado(
                        header.Cell(),
                        "SUBTOTAL");

                    CeldaEncabezado(
                        header.Cell(),
                        "TOTAL");
                });


                // FILAS
                foreach (DataRow fila in ventas.Rows)
                {
                    string factura =
                        fila["N° FACTURA"] == DBNull.Value
                        ? "-"
                        : fila["N° FACTURA"].ToString();


                    string cliente =
                        fila["Nombre De Cliente"].ToString();


                    string fecha =
                        Convert.ToDateTime(
                            fila["FechaVenta"])
                        .ToString("dd/MM/yyyy");


                    decimal subtotal =
                        Convert.ToDecimal(
                            fila["SubTotal"]);


                    decimal total =
                        Convert.ToDecimal(
                            fila["TotalAPagar"]);


                    CeldaDato(
                        table.Cell(),
                        factura);


                    CeldaDato(
                        table.Cell(),
                        cliente);


                    CeldaDato(
                        table.Cell(),
                        fecha);


                    CeldaDatoDerecha(
                        table.Cell(),
                        $"${subtotal:N2}");


                    CeldaDatoDerecha(
                        table.Cell(),
                        $"${total:N2}");
                }
            });
        }


        // =========================================================
        // ENCABEZADO DE TABLA
        // =========================================================

        private void CeldaEncabezado(
            IContainer container,
            string texto)
        {
            container
                .Background("#6B3517")
                .PaddingVertical(5)
                .PaddingHorizontal(5)
                .Text(texto)
                .FontSize(7)
                .Bold()
                .FontColor(Colors.White);
        }


        // =========================================================
        // CELDA
        // =========================================================

        private void CeldaDato(
            IContainer container,
            string texto)
        {
            container
                .BorderBottom(1)
                .BorderColor("#E2D1C1")
                .PaddingVertical(4)
                .PaddingHorizontal(5)
                .Text(texto)
                .FontSize(7);
        }


        // =========================================================
        // CELDA NUMÉRICA
        // =========================================================

        private void CeldaDatoDerecha(
            IContainer container,
            string texto)
        {
            container
                .BorderBottom(1)
                .BorderColor("#E2D1C1")
                .PaddingVertical(4)
                .PaddingHorizontal(5)
                .AlignRight()
                .Text(texto)
                .FontSize(7);
        }


        // =========================================================
        // TOTAL GENERAL
        // =========================================================

        private void ConstruirTotalGeneral(IContainer container)
        {
            decimal totalGeneral = 0;


            foreach (DataRow fila in ventas.Rows)
            {
                totalGeneral +=
                    Convert.ToDecimal(
                        fila["TotalAPagar"]);
            }


            container
                .Background("#F1E2D1")
                .PaddingVertical(10)
                .PaddingHorizontal(14)
                .Row(row =>
                {
                    row.AutoItem()
                        .Text("TOTAL GENERAL:")
                        .FontSize(9)
                        .Bold()
                        .FontColor("#6B3517");


                    row.ConstantItem(15);


                    row.AutoItem()
                        .Text($"${totalGeneral:N2}")
                        .FontSize(12)
                        .Bold()
                        .FontColor("#6B3517");
                });
        }


        // =========================================================
        // PIE
        // =========================================================

        private void ConstruirPie(IContainer container)
        {
            container
                .AlignCenter()
                .Text(
                    $"Muebles Keyda | Reporte de Ventas | Generado: " +
                    $"{DateTime.Now:dd/MM/yyyy HH:mm}"
                )
                .FontSize(7)
                .FontColor("#6B3517");
        }


        // =========================================================
        // GENERAR PDF
        // =========================================================

        public void GenerarPDF(string ruta)
        {
            Document
                .Create(Compose)
                .GeneratePdf(ruta);
        }
    }
}