using Modelo.Entidades;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using System;
using System.Data;
using System.IO;

public class VentasDocumentoPDF : IDocument
{
    private readonly DataTable ventas;
    private readonly DateTime fechaInicio;
    private readonly DateTime fechaFin;
    private readonly int facturasEmitidas;
    private readonly double totalVentas;
    private readonly double ventaMasAlta;

    public VentasDocumentoPDF(
        DataTable ventas,
        DateTime fechaInicio,
        DateTime fechaFin,
        int facturasEmitidas,
        double totalVentas,
        double ventaMasAlta)
    {
        this.ventas = ventas;
        this.fechaInicio = fechaInicio;
        this.fechaFin = fechaFin;
        this.facturasEmitidas = facturasEmitidas;
        this.totalVentas = totalVentas;
        this.ventaMasAlta = ventaMasAlta;
    }


    // ==========================================================
    // INFORMACIÓN DEL DOCUMENTO
    // ==========================================================

    public DocumentMetadata GetMetadata()
    {
        return new DocumentMetadata
        {
            Title =
                "Reporte de Ventas - " +
                ConfiguracionEmpresa.Nombre,

            Author =
                ConfiguracionEmpresa.Nombre,

            Subject = "Reporte de ventas"
        };
    }


    // ==========================================================
    // CONFIGURACIÓN DEL DOCUMENTO
    // ==========================================================

    public DocumentSettings GetSettings()
    {
        return new DocumentSettings();
    }


    // ==========================================================
    // COMPOSICIÓN DEL PDF
    // ==========================================================

    public void Compose(IDocumentContainer documento)
    {
        documento.Page(pagina =>
        {
            pagina.Size(PageSizes.A4);

            pagina.Margin(30);

            pagina.DefaultTextStyle(x =>
                x.FontFamily("Lato")
                 .FontSize(9)
            );


            // ==================================================
            // ENCABEZADO
            // ==================================================

            pagina.Header()
                .Column(header =>
                {

                    // ==================================================
                    // LOGO Y TÍTULO
                    // ==================================================

                    header.Item()
                        .Background("#633719")
                        .Padding(10)
                        .Row(row =>
                        {

                            // ==================================================
                            // LOGO Y ESLOGAN
                            // ==================================================

                            row.RelativeItem()
                                .Column(col =>
                                {

                                    string rutaLogo =
                                        ConfiguracionEmpresa.Logo;


                                    // Verificamos que exista un logo configurado
                                    if (!string.IsNullOrWhiteSpace(rutaLogo) &&
                                        File.Exists(rutaLogo))
                                    {
                                        byte[] logo =
                                            File.ReadAllBytes(rutaLogo);


                                        col.Item()
                                            .Height(80)
                                            .Width(180)
                                            .AlignLeft()
                                            .Image(logo)
                                            .FitArea();
                                    }
                                    else
                                    {
                                        // Si no existe logo,
                                        // mostramos el nombre de la empresa
                                        col.Item()
                                            .Height(80)
                                            .Width(180)
                                            .AlignLeft()
                                            .AlignMiddle()
                                            .Text(
                                                ConfiguracionEmpresa.Nombre
                                            )
                                            .FontSize(18)
                                            .Bold()
                                            .FontColor("#FFFFFF");
                                    }


                                    // ==================================================
                                    // ESLOGAN
                                    // ==================================================

                                    col.Item()
                                        .PaddingTop(3)
                                        .Text(
                                            "DISEÑO · CONFORT · ELEGANCIA"
                                        )
                                        .FontSize(8)
                                        .Bold()
                                        .FontColor("#F4DDC5");
                                });


                            // ==================================================
                            // INFORMACIÓN DEL REPORTE
                            // ==================================================

                            row.RelativeItem()
                                .AlignRight()
                                .Column(col =>
                                {

                                    col.Item()
                                        .Text("REPORTE DE VENTAS")
                                        .FontSize(19)
                                        .Bold()
                                        .FontColor("#FFFFFF");


                                    col.Item()
                                        .Text("Resumen de ventas")
                                        .FontSize(10)
                                        .FontColor("#F4DDC5");

                                });

                        });


                    // ==================================================
                    // PERÍODO
                    // ==================================================

                    header.Item()
                        .PaddingTop(10)
                        .Background("#F1E2D2")
                        .Padding(10)
                        .AlignCenter()
                        .Text(
                            $"Del: {fechaInicio:dd/MM/yyyy}     " +
                            $"Al: {fechaFin:dd/MM/yyyy}"
                        )
                        .Bold()
                        .FontSize(10)
                        .FontColor("#633719");

                });


            // ==================================================
            // CONTENIDO
            // ==================================================

            pagina.Content()
                .PaddingTop(15)
                .Column(contenido =>
                {

                    // ==================================================
                    // ESTADÍSTICAS
                    // ==================================================

                    contenido.Item()
                        .Row(row =>
                        {

                            CrearTarjeta(
                                row,
                                "FACTURAS EMITIDAS",
                                facturasEmitidas.ToString()
                            );


                            CrearTarjeta(
                                row,
                                "TOTAL DE VENTAS",
                                $"${totalVentas:N2}"
                            );


                            CrearTarjeta(
                                row,
                                "VENTA MÁS ALTA",
                                $"${ventaMasAlta:N2}"
                            );

                        });


                    // ==================================================
                    // TÍTULO DE LA TABLA
                    // ==================================================

                    contenido.Item()
                        .PaddingTop(20)
                        .Text("DETALLE DE VENTAS")
                        .FontSize(12)
                        .Bold()
                        .FontColor("#633719");


                    // ==================================================
                    // TABLA DE VENTAS
                    // ==================================================

                    contenido.Item()
                        .PaddingTop(8)
                        .Table(tabla =>
                        {

                            // ==================================================
                            // COLUMNAS
                            // ==================================================

                            tabla.ColumnsDefinition(columnas =>
                            {
                                columnas.ConstantColumn(45);    // Factura
                                columnas.RelativeColumn(2.5f);  // Cliente
                                columnas.ConstantColumn(65);    // Fecha
                                columnas.RelativeColumn(1.2f);  // Método
                                columnas.ConstantColumn(65);    // Subtotal
                                columnas.ConstantColumn(70);    // Total
                            });


                            // ==================================================
                            // ENCABEZADOS
                            // ==================================================

                            tabla.Header(header =>
                            {

                                EncabezadoTabla(
                                    header,
                                    "FACT."
                                );


                                EncabezadoTabla(
                                    header,
                                    "CLIENTE"
                                );


                                EncabezadoTabla(
                                    header,
                                    "FECHA"
                                );


                                EncabezadoTabla(
                                    header,
                                    "MÉTODO"
                                );


                                EncabezadoTabla(
                                    header,
                                    "SUBTOTAL"
                                );


                                EncabezadoTabla(
                                    header,
                                    "TOTAL"
                                );

                            });


                            // ==================================================
                            // FILAS DE VENTAS
                            // ==================================================

                            foreach (DataRow fila in ventas.Rows)
                            {

                                // ==================================================
                                // FACTURA
                                // ==================================================

                                tabla.Cell()
                                    .BorderBottom(1)
                                    .BorderColor("#E2D2C2")
                                    .Padding(6)
                                    .Text(
                                        fila["N° FACTURA"]?.ToString() ?? ""
                                    )
                                    .FontSize(8);


                                // ==================================================
                                // CLIENTE
                                // ==================================================

                                tabla.Cell()
                                    .BorderBottom(1)
                                    .BorderColor("#E2D2C2")
                                    .Padding(6)
                                    .Text(
                                        fila["Nombre De Cliente"]?.ToString() ?? ""
                                    )
                                    .FontSize(8);


                                // ==================================================
                                // FECHA
                                // ==================================================

                                tabla.Cell()
                                    .BorderBottom(1)
                                    .BorderColor("#E2D2C2")
                                    .Padding(6)
                                    .Text(
                                        Convert.ToDateTime(
                                            fila["FechaVenta"]
                                        ).ToString("dd/MM/yyyy")
                                    )
                                    .FontSize(8);


                                // ==================================================
                                // MÉTODO DE PAGO
                                // ==================================================

                                tabla.Cell()
                                    .BorderBottom(1)
                                    .BorderColor("#E2D2C2")
                                    .Padding(6)
                                    .Text(
                                        fila["MetodoPago"]?.ToString() ?? ""
                                    )
                                    .FontSize(8);


                                // ==================================================
                                // SUBTOTAL
                                // ==================================================

                                tabla.Cell()
                                    .BorderBottom(1)
                                    .BorderColor("#E2D2C2")
                                    .Padding(6)
                                    .AlignRight()
                                    .Text(
                                       "$" + totalVentas.ToString("N2")
                                    )
                                    .FontSize(8);


                                // ==================================================
                                // TOTAL
                                // ==================================================

                                tabla.Cell()
                                    .BorderBottom(1)
                                    .BorderColor("#E2D2C2")
                                    .Padding(6)
                                    .AlignRight()
                                    .Text(
                                       "$" + totalVentas.ToString("N2")
                                    )
                                    .Bold()
                                    .FontSize(8);

                            }

                        });


                    // ==================================================
                    // TOTAL GENERAL
                    // ==================================================

                    contenido.Item()
                        .PaddingTop(15)
                        .AlignRight()
                        .Background("#F1E2D2")
                        .Padding(12)
                        .Row(row =>
                        {

                            row.AutoItem()
                                .Text("TOTAL GENERAL:")
                                .Bold()
                                .FontSize(11)
                                .FontColor("#633719");


                            row.AutoItem()
                                .PaddingLeft(15)
                                .Text(
                                    $"${totalVentas:N2}"
                                )
                                .Bold()
                                .FontSize(13)
                                .FontColor("#633719");

                        });

                });


            // ==================================================
            // PIE DE PÁGINA
            // ==================================================

            pagina.Footer()
                .AlignCenter()
                .Text(text =>
                {

                    text.Span(
                        ConfiguracionEmpresa.Nombre +
                        " | Reporte de Ventas"
                    )
                    .FontFamily("Lato")
                    .FontSize(8)
                    .FontColor("#633719");


                    text.Span(
                        $"  |  Generado: " +
                        $"{DateTime.Now:dd/MM/yyyy HH:mm}"
                    )
                    .FontFamily("Lato")
                    .FontSize(8)
                    .FontColor("#633719");

                });

        });
    }


    // ==========================================================
    // TARJETA DE ESTADÍSTICA
    // ==========================================================

    private void CrearTarjeta(
        RowDescriptor row,
        string titulo,
        string valor)
    {

        row.RelativeItem()
            .Padding(5)
            .Background("#F1E2D2")
            .Border(1)
            .BorderColor("#D5BFA8")
            .Padding(12)
            .Column(col =>
            {

                col.Item()
                    .Text(titulo)
                    .FontFamily("Lato")
                    .FontSize(8)
                    .Bold()
                    .FontColor("#633719");


                col.Item()
                    .PaddingTop(5)
                    .Text(valor)
                    .FontFamily("Lato")
                    .FontSize(18)
                    .Bold()
                    .FontColor("#633719");

            });

    }


    // ==========================================================
    // ENCABEZADO DE TABLA
    // ==========================================================

    private void EncabezadoTabla(
        TableCellDescriptor header,
        string texto)
    {

        header.Cell()
            .Background("#633719")
            .Padding(6)
            .Text(texto)
            .Bold()
            .FontColor("#FFFFFF")
            .FontSize(7);

    }
}