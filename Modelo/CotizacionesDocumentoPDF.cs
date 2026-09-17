using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using System;
using System.Data;
using System.IO;

public class CotizacionesDocumentoPDF : IDocument
{
    private DataTable cotizaciones;
    private DateTime fechaInicio;
    private DateTime fechaFin;

    private int cotizacionesRegistradas;
    private int cotizacionesAprobadas;
    private int cotizacionesRechazadas;


    public CotizacionesDocumentoPDF(
        DataTable cotizaciones,
        DateTime fechaInicio,
        DateTime fechaFin,
        int cotizacionesRegistradas,
        int cotizacionesAprobadas,
        int cotizacionesRechazadas)
    {
        this.cotizaciones = cotizaciones;
        this.fechaInicio = fechaInicio;
        this.fechaFin = fechaFin;

        this.cotizacionesRegistradas = cotizacionesRegistradas;
        this.cotizacionesAprobadas = cotizacionesAprobadas;
        this.cotizacionesRechazadas = cotizacionesRechazadas;
    }


    // ==========================================
    // METADATOS
    // ==========================================

    public DocumentMetadata GetMetadata()
    {
        return new DocumentMetadata
        {
            Title = "Reporte de Cotizaciones",
            Author = "Muebles Keyda",
            Subject = "Reporte de cotizaciones"
        };
    }


    // ==========================================
    // CONFIGURACIÓN
    // ==========================================

    public DocumentSettings GetSettings()
    {
        return new DocumentSettings();
    }


    // ==========================================
    // DOCUMENTO
    // ==========================================

    public void Compose(IDocumentContainer documento)
    {
        documento.Page(pagina =>
        {
            pagina.Size(PageSizes.A4.Landscape());

            pagina.Margin(25);

            pagina.DefaultTextStyle(estilo =>
            {
                estilo.FontFamily("Lato");
                estilo.FontSize(9);
                return estilo;
            });

            pagina.Header()
                .Element(ConstruirEncabezado);

            pagina.Content()
                .Element(ConstruirContenido);

            pagina.Footer()
                .AlignCenter()
                .Text("Muebles Keyda | Reporte de Cotizaciones")
                .FontSize(8)
                .FontColor("#777777");
        });
    }


    // ==========================================
    // ENCABEZADO
    // ==========================================
    private void ConstruirEncabezado(IContainer contenedorPrincipal)
    {
        string rutaLogo = Path.Combine(
            @"C:\Documentos\Muebles Keyda en Git\Vista\Resources",
            "Logo de la empresa png.png"
        );

        if (!File.Exists(rutaLogo))
        {
            throw new Exception(
                "NO SE ENCONTRÓ EL LOGO EN:\n\n" +
                rutaLogo
            );
        }

        byte[] logo = File.ReadAllBytes(rutaLogo);

        contenedorPrincipal
            .Background("#4A2C1A")
            .Padding(15)
            .Row(fila =>
            {
                // ==========================================
                // LOGO
                // ==========================================

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
                            .Text("DISEÑO · CONFORT · ELEGANCIA")
                            .FontSize(8)
                            .Bold()
                            .FontColor("#F4DDC5");
                    });


                // ==========================================
                // TÍTULO
                // ==========================================

                fila.RelativeItem()
                    .AlignRight()
                    .Column(columna =>
                    {
                        columna.Item()
                            .AlignRight()
                            .Text("REPORTE DE COTIZACIONES")
                            .FontSize(20)
                            .Bold()
                            .FontColor("#FFFFFF");

                        columna.Item()
                            .PaddingTop(5)
                            .AlignRight()
                            .Text("Registro de cotizaciones")
                            .FontSize(10)
                            .FontColor("#F4DDC5");
                    });
            });
    }


    // ==========================================
    // CONTENIDO
    // ==========================================

    private void ConstruirContenido(IContainer contenedorPrincipal)
    {
        contenedorPrincipal.Column(columnaPrincipal =>
        {
            // ==========================================
            // PERÍODO
            // ==========================================

            columnaPrincipal.Item()
                .PaddingTop(10)
                .Background("#F4DDC5")
                .Padding(8)
                .Text(
                    "PERÍODO: " +
                    fechaInicio.ToString("dd/MM/yyyy") +
                    " - " +
                    fechaFin.ToString("dd/MM/yyyy")
                )
                .Bold()
                .FontSize(9)
                .FontColor("#4A2C1A");


            // ==========================================
            // ESTADÍSTICAS
            // ==========================================

            columnaPrincipal.Item()
                .PaddingTop(10)
                .Row(fila =>
                {
                    fila.RelativeItem()
                        .PaddingRight(5)
                        .Element(elemento =>
                            CrearTarjeta(
                                elemento,
                                "COTIZACIONES APROBADAS",
                                cotizacionesAprobadas.ToString()
                            )
                        );

                    fila.RelativeItem()
                        .PaddingHorizontal(5)
                        .Element(elemento =>
                            CrearTarjeta(
                                elemento,
                                "COTIZACIONES RECHAZADAS",
                                cotizacionesRechazadas.ToString()
                            )
                        );

                    fila.RelativeItem()
                        .PaddingLeft(5)
                        .Element(elemento =>
                            CrearTarjeta(
                                elemento,
                                "COTIZACIONES REGISTRADAS",
                                cotizacionesRegistradas.ToString()
                            )
                        );
                });


            // ==========================================
            // TÍTULO
            // ==========================================

            columnaPrincipal.Item()
                .PaddingTop(15)
                .Text("DETALLE DE COTIZACIONES")
                .FontSize(12)
                .Bold()
                .FontColor("#4A2C1A");


            // ==========================================
            // TABLA
            // ==========================================

            columnaPrincipal.Item()
                .PaddingTop(5)
                .Table(tabla =>
                {
                    tabla.ColumnsDefinition(columnas =>
                    {
                        columnas.ConstantColumn(70);
                        columnas.ConstantColumn(75);
                        columnas.RelativeColumn(2);
                        columnas.RelativeColumn(1.5f);
                        columnas.RelativeColumn(1);
                        columnas.ConstantColumn(85);
                    });


                    // ENCABEZADOS

                    EncabezadoTabla(
                        tabla,
                        "N° COTIZACIÓN"
                    );

                    EncabezadoTabla(
                        tabla,
                        "FECHA"
                    );

                    EncabezadoTabla(
                        tabla,
                        "CLIENTE"
                    );

                    EncabezadoTabla(
                        tabla,
                        "TIPO DE CLIENTE"
                    );

                    EncabezadoTabla(
                        tabla,
                        "ESTADO"
                    );

                    EncabezadoTabla(
                        tabla,
                        "TOTAL"
                    );


                    // ==========================================
                    // FILAS
                    // ==========================================

                    foreach (DataRow fila in cotizaciones.Rows)
                    {
                        CeldaTabla(
                            tabla,
                            fila["IdCotizacion"]
                        );


                        string fecha = "";

                        if (fila["Fecha"] != DBNull.Value)
                        {
                            fecha =
                                Convert.ToDateTime(
                                    fila["Fecha"]
                                ).ToString("dd/MM/yyyy");
                        }

                        CeldaTabla(
                            tabla,
                            fecha
                        );


                        CeldaTabla(
                            tabla,
                            fila["Cliente"]
                        );


                        CeldaTabla(
                            tabla,
                            fila["Tipo de Cliente"]
                        );


                        CeldaTabla(
                            tabla,
                            fila["Estado"]
                        );


                        string total = "";

                        if (fila["Total"] != DBNull.Value)
                        {
                            total =
                                Convert.ToDecimal(
                                    fila["Total"]
                                ).ToString("$#,##0.00");
                        }

                        CeldaTabla(
                            tabla,
                            total
                        );
                    }
                });


            // ==========================================
            // TOTAL DE COTIZACIONES
            // ==========================================

            columnaPrincipal.Item()
                .PaddingTop(10)
                .AlignRight()
                .Background("#F4DDC5")
                .Padding(8)
                .Text(
                    "COTIZACIONES REGISTRADAS: " +
                    cotizacionesRegistradas
                )
                .Bold()
                .FontSize(9)
                .FontColor("#4A2C1A");
        });
    }


    // ==========================================
    // TARJETA
    // ==========================================

    private IContainer CrearTarjeta(
        IContainer contenedor,
        string titulo,
        string valor)
    {
        contenedor
            .Background("#F4DDC5")
            .Border(1)
            .BorderColor("#D4B08A")
            .Padding(10)
            .Column(columna =>
            {
                columna.Item()
                    .AlignCenter()
                    .Text(titulo)
                    .FontSize(8)
                    .Bold()
                    .FontColor("#6B452D");

                columna.Item()
                    .PaddingTop(4)
                    .AlignCenter()
                    .Text(valor)
                    .FontSize(18)
                    .Bold()
                    .FontColor("#4A2C1A");
            });

        return contenedor;
    }


    // ==========================================
    // ENCABEZADO DE TABLA
    // ==========================================

    private void EncabezadoTabla(
        TableDescriptor tabla,
        string texto)
    {
        tabla.Cell()
            .Background("#4A2C1A")
            .Padding(5)
            .AlignCenter()
            .Text(texto)
            .FontSize(7)
            .Bold()
            .FontColor("#FFFFFF");
    }


    // ==========================================
    // CELDA DE TABLA
    // ==========================================

    private void CeldaTabla(
        TableDescriptor tabla,
        object valor)
    {
        string texto =
            valor == DBNull.Value ||
            valor == null
                ? ""
                : valor.ToString();

        tabla.Cell()
            .BorderBottom(1)
            .BorderColor("#DDDDDD")
            .Padding(5)
            .Text(texto)
            .FontSize(8);
    }
}
