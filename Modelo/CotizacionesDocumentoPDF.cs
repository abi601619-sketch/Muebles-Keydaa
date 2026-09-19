using Modelo.Entidades;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using System;
using System.Data;
using System.IO;

namespace Modelo
{
    public class CotizacionesDocumentoPDF : IDocument
    {
        private DataTable cotizaciones;
        private DateTime fechaInicio;
        private DateTime fechaFin;

        private int cotizacionesRegistradas;
        private int cotizacionesAprobadas;
        private int cotizacionesRechazadas;


        // =========================================================
        // CONSTRUCTOR
        // =========================================================

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


        // =========================================================
        // CONFIGURACIÓN DEL DOCUMENTO
        // =========================================================

        public DocumentMetadata GetMetadata()
        {
            return new DocumentMetadata
            {
                Title = "Reporte de Cotizaciones",
                Author = ConfiguracionEmpresa.Nombre,
                Subject = "Reporte de cotizaciones"
            };
        }


        public DocumentSettings GetSettings()
        {
            return new DocumentSettings
            {
                CompressDocument = true
            };
        }


        // =========================================================
        // CREACIÓN DEL PDF
        // =========================================================

        public void Compose(IDocumentContainer container)
        {
            container.Page(pagina =>
            {
                pagina.Size(PageSizes.A4.Landscape());

                pagina.Margin(25);

                pagina.DefaultTextStyle(estilo =>
                {
                    estilo.FontSize(9);

                    return estilo;
                });


                // ENCABEZADO
                pagina.Header()
                    .Element(ConstruirEncabezado);


                // CONTENIDO
                pagina.Content()
                    .Element(ConstruirContenido);


                // PIE DE PÁGINA
                pagina.Footer()
                    .AlignCenter()
                    .Text(
                        ConfiguracionEmpresa.Nombre +
                        " | Reporte de Cotizaciones"
                    )
                    .FontSize(8)
                    .FontColor("#777777");
            });
        }


        // =========================================================
        // ENCABEZADO
        // =========================================================

        private void ConstruirEncabezado(
            IContainer contenedorPrincipal)
        {
            string rutaLogo = ConfiguracionEmpresa.Logo;


            // -----------------------------------------------------
            // COMPROBAR QUE EXISTA EL LOGO
            // -----------------------------------------------------

            if (string.IsNullOrWhiteSpace(rutaLogo) ||
                !File.Exists(rutaLogo))
            {
                throw new Exception(
                    "No se encontró el logo de la empresa.\n\n" +
                    "Configure nuevamente el logo de la empresa."
                );
            }


            byte[] logo = File.ReadAllBytes(rutaLogo);


            // -----------------------------------------------------
            // DISEÑO DEL ENCABEZADO
            // -----------------------------------------------------

            contenedorPrincipal
                .Background("#4A2C1A")
                .Padding(15)
                .Row(fila =>
                {

                    // =================================================
                    // INFORMACIÓN DE LA EMPRESA
                    // =================================================

                    fila.RelativeItem()
                        .Column(columna =>
                        {

                            // LOGO
                            columna.Item()
                                .Height(55)
                                .Width(180)
                                .AlignLeft()
                                .Image(logo)
                                .FitArea();


                            // NOMBRE
                            columna.Item()
                                .PaddingTop(5)
                                .Text(
                                    ConfiguracionEmpresa.Nombre
                                )
                                .FontSize(10)
                                .Bold()
                                .FontColor("#F4DDC5");


                            // TELÉFONO
                            columna.Item()
                                .Text(
                                    "Tel: " +
                                    ConfiguracionEmpresa.Telefono
                                )
                                .FontSize(7)
                                .FontColor("#F4DDC5");


                            // CORREO
                            columna.Item()
                                .Text(
                                    ConfiguracionEmpresa.Correo
                                )
                                .FontSize(7)
                                .FontColor("#F4DDC5");


                            // DIRECCIÓN
                            columna.Item()
                                .Text(
                                    ConfiguracionEmpresa.Direccion
                                )
                                .FontSize(7)
                                .FontColor("#F4DDC5");
                        });


                    // =================================================
                    // TÍTULO DEL REPORTE
                    // =================================================

                    fila.RelativeItem()
                        .AlignRight()
                        .Column(columna =>
                        {

                            columna.Item()
                                .AlignRight()
                                .Text(
                                    "REPORTE DE COTIZACIONES"
                                )
                                .FontSize(20)
                                .Bold()
                                .FontColor("#FFFFFF");


                            columna.Item()
                                .PaddingTop(5)
                                .AlignRight()
                                .Text(
                                    "Registro de cotizaciones"
                                )
                                .FontSize(10)
                                .FontColor("#F4DDC5");


                            columna.Item()
                                .PaddingTop(8)
                                .AlignRight()
                                .Text(
                                    "Período: " +
                                    fechaInicio.ToString("dd/MM/yyyy") +
                                    " - " +
                                    fechaFin.ToString("dd/MM/yyyy")
                                )
                                .FontSize(8)
                                .FontColor("#FFFFFF");
                        });
                });
        }


        // =========================================================
        // CONTENIDO DEL REPORTE
        // =========================================================

        private void ConstruirContenido(IContainer contenedor)
        {
            contenedor
                .PaddingTop(15)
                .Column(columna =>
                {

                    // =================================================
                    // RESUMEN
                    // =================================================

                    columna.Item()
                        .Row(fila =>
                        {

                            // REGISTRADAS
                            fila.RelativeItem()
                                .Padding(5)
                                .Background("#F4DDC5")
                                .Padding(10)
                                .Column(c =>
                                {
                                    c.Item()
                                        .Text("COTIZACIONES REGISTRADAS")
                                        .FontSize(8)
                                        .Bold()
                                        .FontColor("#4A2C1A");

                                    c.Item()
                                        .PaddingTop(5)
                                        .Text(
                                            cotizacionesRegistradas.ToString()
                                        )
                                        .FontSize(18)
                                        .Bold()
                                        .FontColor("#4A2C1A");
                                });


                            // APROBADAS
                            fila.RelativeItem()
                                .Padding(5)
                                .Background("#E8F5E9")
                                .Padding(10)
                                .Column(c =>
                                {
                                    c.Item()
                                        .Text("COTIZACIONES APROBADAS")
                                        .FontSize(8)
                                        .Bold();

                                    c.Item()
                                        .PaddingTop(5)
                                        .Text(
                                            cotizacionesAprobadas.ToString()
                                        )
                                        .FontSize(18)
                                        .Bold();
                                });


                            // RECHAZADAS
                            fila.RelativeItem()
                                .Padding(5)
                                .Background("#FDECEC")
                                .Padding(10)
                                .Column(c =>
                                {
                                    c.Item()
                                        .Text("COTIZACIONES RECHAZADAS")
                                        .FontSize(8)
                                        .Bold();

                                    c.Item()
                                        .PaddingTop(5)
                                        .Text(
                                            cotizacionesRechazadas.ToString()
                                        )
                                        .FontSize(18)
                                        .Bold();
                                });
                        });


                    // =================================================
                    // ESPACIO
                    // =================================================

                    columna.Item()
                        .PaddingTop(15);


                    // =================================================
                    // TABLA DE COTIZACIONES
                    // =================================================

                    columna.Item()
                        .Table(tabla =>
                        {

                            // -------------------------------------------------
                            // DEFINIR COLUMNAS
                            // -------------------------------------------------

                            tabla.ColumnsDefinition(columnas =>
                            {
                                columnas.RelativeColumn();
                                columnas.RelativeColumn(2);
                                columnas.RelativeColumn(2);
                                columnas.RelativeColumn();
                                columnas.RelativeColumn();
                            });


                            // -------------------------------------------------
                            // ENCABEZADOS
                            // -------------------------------------------------

                            tabla.Header(encabezado =>
                            {
                                encabezado.Cell()
                                    .Background("#4A2C1A")
                                    .Padding(7)
                                    .Text("ID")
                                    .Bold()
                                    .FontColor("#FFFFFF");

                                encabezado.Cell()
                                    .Background("#4A2C1A")
                                    .Padding(7)
                                    .Text("CLIENTE")
                                    .Bold()
                                    .FontColor("#FFFFFF");

                                encabezado.Cell()
                                    .Background("#4A2C1A")
                                    .Padding(7)
                                    .Text("CONDICIÓN DE PAGO")
                                    .Bold()
                                    .FontColor("#FFFFFF");

                                encabezado.Cell()
                                    .Background("#4A2C1A")
                                    .Padding(7)
                                    .Text("FECHA")
                                    .Bold()
                                    .FontColor("#FFFFFF");

                                encabezado.Cell()
                                    .Background("#4A2C1A")
                                    .Padding(7)
                                    .Text("ESTADO")
                                    .Bold()
                                    .FontColor("#FFFFFF");
                            });


                            // -------------------------------------------------
                            // DATOS
                            // -------------------------------------------------

                            if (cotizaciones != null &&
                                cotizaciones.Rows.Count > 0)
                            {
                                foreach (DataRow fila in cotizaciones.Rows)
                                {
                                    tabla.Cell()
                                        .Padding(6)
                                        .Text(
                                            ObtenerDato(
                                                fila,
                                                "IdCotizacion"
                                            )
                                        );

                                    tabla.Cell()
                                        .Padding(6)
                                        .Text(
                                            ObtenerDato(
                                                fila,
                                                "Cliente"
                                            )
                                        );

                                    tabla.Cell()
                                        .Padding(6)
                                        .Text(
                                            ObtenerDato(
                                                fila,
                                                "CondicionPago"
                                            )
                                        );

                                    tabla.Cell()
                                        .Padding(6)
                                        .Text(
                                            ObtenerDato(
                                                fila,
                                                "Fecha"
                                            )
                                        );

                                    tabla.Cell()
                                        .Padding(6)
                                        .Text(
                                            ObtenerDato(
                                                fila,
                                                "Estado"
                                            )
                                        );
                                }
                            }
                            else
                            {
                                tabla.Cell()
                                    .ColumnSpan(5)
                                    .Padding(15)
                                    .AlignCenter()
                                    .Text(
                                        "No existen cotizaciones " +
                                        "para el período seleccionado."
                                    )
                                    .Italic();
                            }
                        });
                });
        }


        // =========================================================
        // OBTENER DATOS DEL DATATABLE
        // =========================================================

        private string ObtenerDato(
            DataRow fila,
            string nombreColumna)
        {
            if (fila.Table.Columns.Contains(nombreColumna))
            {
                if (fila[nombreColumna] != DBNull.Value)
                {
                    return fila[nombreColumna].ToString();
                }
            }

            return "-";
        }
    }
}
