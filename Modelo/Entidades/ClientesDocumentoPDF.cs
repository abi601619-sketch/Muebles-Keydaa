using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using System;
using System.Data;
using System.IO;

namespace Modelo.PDF
{
    public class ClientesDocumentoPDF : IDocument
    {
        private readonly DataTable clientes;
        private readonly DateTime fechaInicio;
        private readonly DateTime fechaFin;
        private readonly int clientesTotales;
        private readonly int clientesCorporativos;
        private readonly int clientesIndividuales;

        public ClientesDocumentoPDF(DataTable clientes, DateTime fechaInicio, DateTime fechaFin, int clientesTotales, int clientesCorporativos, int clientesIndividuales)
        {
            this.clientes = clientes;
            this.fechaInicio = fechaInicio;
            this.fechaFin = fechaFin;
            this.clientesTotales = clientesTotales;
            this.clientesCorporativos = clientesCorporativos;
            this.clientesIndividuales = clientesIndividuales;
        }

        // INFORMACIÓN DEL DOCUMENTO

        public DocumentMetadata GetMetadata()
        {
            return new DocumentMetadata
            {
                Title = "Reporte de Clientes - " + Modelo.Properties.Settings.Default.NombreEmpresa,
                Author = Modelo.Properties.Settings.Default.NombreEmpresa,
                Subject = "Reporte de clientes"
            };
        }

        public DocumentSettings GetSettings()
        {
            return new DocumentSettings();
        }

        // CREAR DOCUMENTO

        public void Compose(IDocumentContainer documento)
        {
            documento.Page(pagina =>
            {
                // CONFIGURACIÓN DE PÁGINA

                pagina.Size(PageSizes.A4.Landscape());
                pagina.Margin(25);
                pagina.DefaultTextStyle(x => x.FontFamily("Lato").FontSize(8));

                // ENCABEZADO

                pagina.Header().Column(header =>
                {
                    // ENCABEZADO PRINCIPAL

                    header.Item().Background("#633719").Padding(18).Row(row =>
                    {
                        // LOGO + INFORMACIÓN DE LA EMPRESA

                        row.RelativeItem().Column(col =>
                        {
                            // OBTENER RUTA DEL LOGO

                            string rutaLogo = Modelo.Properties.Settings.Default.LogoEmpresa;

                            // VALIDAR QUE EXISTA UNA RUTA

                            if (string.IsNullOrWhiteSpace(rutaLogo))
                                throw new Exception("ERR-PDF-001: No se ha configurado el logo de la empresa.");

                            // VALIDAR QUE EXISTA EL ARCHIVO

                            if (!File.Exists(rutaLogo))
                                throw new Exception("ERR-PDF-002: No se encontró el logo configurado en:\n\n" + rutaLogo);

                            // CARGAR LOGO

                            byte[] logo = File.ReadAllBytes(rutaLogo);

                            // MOSTRAR LOGO

                            col.Item().Height(55).Width(180).AlignLeft().Image(logo).FitArea();

                            // NOMBRE DE LA EMPRESA

                            col.Item().PaddingTop(5).Text(Modelo.Properties.Settings.Default.NombreEmpresa).FontSize(11).Bold().FontColor("#FFFFFF");

                            // TELÉFONO

                            col.Item().Text("Tel: " + Modelo.Properties.Settings.Default.TelefonoEmpresa).FontSize(8).FontColor("#F4DDC5");

                            // CORREO

                            col.Item().Text("Correo: " + Modelo.Properties.Settings.Default.CorreoEmpresa).FontSize(8).FontColor("#F4DDC5");

                            // DIRECCIÓN

                            col.Item().Text("Dirección: " + Modelo.Properties.Settings.Default.DireccionEmpresa).FontSize(8).FontColor("#F4DDC5");
                        });

                        // TÍTULO DEL REPORTE

                        row.RelativeItem().AlignRight().Column(col =>
                        {
                            col.Item().Text("REPORTE DE CLIENTES").FontSize(19).Bold().FontColor("#FFFFFF");
                            col.Item().PaddingTop(3).Text("Registro de clientes").FontSize(10).FontColor("#F4DDC5");
                        });
                    });

                    // PERÍODO

                    header.Item().PaddingTop(10).Background("#F1E2D2").Padding(10).AlignCenter().Text($"Del: {fechaInicio:dd/MM/yyyy}     " + $"Al: {fechaFin:dd/MM/yyyy}").Bold().FontSize(10).FontColor("#633719");
                });

                // CONTENIDO

                pagina.Content().PaddingTop(15).Column(contenido =>
                {
                    // TARJETAS DE ESTADÍSTICAS

                    contenido.Item().Row(row =>
                    {
                        CrearTarjeta(row, "CLIENTES TOTALES", clientesTotales.ToString());
                        CrearTarjeta(row, "CLIENTES CORPORATIVOS", clientesCorporativos.ToString());
                        CrearTarjeta(row, "CLIENTES INDIVIDUALES", clientesIndividuales.ToString());
                    });

                    // TÍTULO DE TABLA

                    contenido.Item().PaddingTop(18).Background("#633719").Padding(8).Text("INFORMACIÓN DE CLIENTES").FontSize(11).Bold().FontColor("#FFFFFF");

                    // TABLA

                    contenido.Item().PaddingTop(5).Table(tabla =>
                    {
                        tabla.ColumnsDefinition(columnas =>
                        {
                            // Cliente
                            columnas.RelativeColumn(2.2f);
                            // Tipo
                            columnas.RelativeColumn(1.2f);
                            // Encargado
                            columnas.RelativeColumn(1.5f);
                            // Documento
                            columnas.RelativeColumn(1.2f);
                            // Teléfono
                            columnas.RelativeColumn(1.2f);
                            // Correo
                            columnas.RelativeColumn(2.2f);
                            // Dirección
                            columnas.RelativeColumn(2.3f);
                            // Fecha
                            columnas.ConstantColumn(75);
                        });

                        // ENCABEZADOS DE TABLA

                        tabla.Header(header =>
                        {
                            EncabezadoTabla(header, "CLIENTE");
                            EncabezadoTabla(header, "TIPO");
                            EncabezadoTabla(header, "ENCARGADO");
                            EncabezadoTabla(header, "DOCUMENTO");
                            EncabezadoTabla(header, "TELÉFONO");
                            EncabezadoTabla(header, "CORREO");
                            EncabezadoTabla(header, "DIRECCIÓN");
                            EncabezadoTabla(header, "FECHA REG.");
                        });

                        // FILAS

                        foreach (DataRow fila in clientes.Rows)
                        {
                            CeldaTabla(tabla, fila["Nombre del Cliente"]);
                            CeldaTabla(tabla, fila["Tipo de Cliente"]);
                            CeldaTabla(tabla, fila["Encargado"]);
                            CeldaTabla(tabla, fila["Documento"]);
                            CeldaTabla(tabla, fila["Teléfono"]);
                            CeldaTabla(tabla, fila["Correo"]);
                            CeldaTabla(tabla, fila["Dirección"]);

                            string fechaRegistro = "";

                            if (fila["Fecha de Registro"] != DBNull.Value)
                                fechaRegistro = Convert.ToDateTime(fila["Fecha de Registro"]).ToString("dd/MM/yyyy");

                            CeldaTabla(tabla, fechaRegistro);
                        }
                    });

                    // TOTAL

                    contenido.Item().PaddingTop(12).AlignRight().Background("#F1E2D2").Padding(10).Row(row =>
                    {
                        row.AutoItem().Text("TOTAL CLIENTES:").Bold().FontSize(11).FontColor("#633719");
                        row.AutoItem().PaddingLeft(15).Text(clientesTotales.ToString()).Bold().FontSize(13).FontColor("#633719");
                    });
                });

                // PIE DE PÁGINA

                pagina.Footer().AlignCenter().Text(text =>
                {
                    // NOMBRE DE LA EMPRESA

                    text.Span(Modelo.Properties.Settings.Default.NombreEmpresa + " | Reporte de Clientes").FontFamily("Lato").FontSize(8).FontColor("#633719");

                    // FECHA DE GENERACIÓN

                    text.Span($"  |  Generado: {DateTime.Now:dd/MM/yyyy HH:mm}").FontFamily("Lato").FontSize(8).FontColor("#633719");
                });
            });
        }

        // TARJETA DE ESTADÍSTICA

        private void CrearTarjeta(RowDescriptor row, string titulo, string valor)
        {
            row.RelativeItem().Padding(5).Background("#F1E2D2").Border(1).BorderColor("#D5BFA8").Padding(12).Column(col =>
            {
                col.Item().Text(titulo).FontFamily("Lato").FontSize(8).Bold().FontColor("#633719");
                col.Item().PaddingTop(5).Text(valor).FontFamily("Lato").FontSize(18).Bold().FontColor("#633719");
            });
        }

        // ENCABEZADO DE TABLA

        private void EncabezadoTabla(TableCellDescriptor header, string texto)
        {
            header.Cell().Background("#633719").Padding(6).Text(texto).Bold().FontColor("#FFFFFF").FontSize(7);
        }

        // CELDA DE TABLA

        private void CeldaTabla(TableDescriptor tabla, object valor)
        {
            string texto = "";

            if (valor != null && valor != DBNull.Value)
                texto = valor.ToString();

            tabla.Cell().BorderBottom(1).BorderColor("#E2D2C2").Padding(5).Text(texto).FontFamily("Lato").FontSize(7);
        }
    }
}