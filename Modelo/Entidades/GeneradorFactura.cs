using Modelo.Conexión_DB;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Windows.Forms;

namespace Modelo.Entidades
{
    public class GeneradorFactura
    {
        public static void Generar(int idFactura, string rutaArchivo)
        {
            try
            {
                // VALIDAR LA RUTA DEL ARCHIVO

                if (string.IsNullOrWhiteSpace(rutaArchivo))
                {
                    MessageBox.Show("La ruta del archivo PDF está vacía.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    return;
                }

                string carpeta = Path.GetDirectoryName(rutaArchivo);

                if (!Directory.Exists(carpeta))
                {
                    Directory.CreateDirectory(carpeta);
                }

                // OBTENER LOS DATOS GENERALES DE LA FACTURA

                DataTable factura = ObtenerFactura(idFactura);

                if (factura.Rows.Count == 0)
                {
                    MessageBox.Show("No se encontró la factura.", "Factura", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    return;
                }

                DataRow datos = factura.Rows[0];

                // OBTENER EL ID DE LA VENTA

                int idVenta = Convert.ToInt32(datos["IdVenta"]);

                // OBTENER DETALLE DE LA VENTA

                DataTable detalle = ObtenerDetalleVenta(idVenta);

                // CONFIGURAR QUESTPDF

                QuestPDF.Settings.License = LicenseType.Community;

                // CREAR DOCUMENTO PDF

                Document.Create(documento =>
                {
                    documento.Page(page =>
                    {
                        // CONFIGURACIÓN DE LA PÁGINA

                        page.Size(PageSizes.Letter);
                        page.Margin(40);

                        // ENCABEZADO

                        page.Header().Column(columna =>
                        {
                            string rutaLogo = ConfiguracionEmpresa.Logo;

                            if (!string.IsNullOrWhiteSpace(rutaLogo) && File.Exists(rutaLogo))
                            {
                                byte[] logo = File.ReadAllBytes(rutaLogo);

                                columna.Item().Height(70).Width(180).AlignLeft().Image(logo).FitArea();
                            }
                            else
                            {
                                columna.Item().Text(ConfiguracionEmpresa.Nombre).FontSize(20).Bold();
                            }

                            columna.Item().PaddingTop(5).Text(ConfiguracionEmpresa.Nombre).FontSize(12).Bold();

                            columna.Item().Text("Teléfono: " + ConfiguracionEmpresa.Telefono).FontSize(8);

                            columna.Item().Text("Correo: " + ConfiguracionEmpresa.Correo).FontSize(8);

                            columna.Item().Text("Dirección: " + ConfiguracionEmpresa.Direccion).FontSize(8);

                            columna.Item().PaddingTop(5).Text("FACTURA").FontSize(24).Bold();

                            columna.Item().Text("N.º " + datos["IdFactura"].ToString()).FontSize(12);

                            columna.Item().LineHorizontal(1);
                        });

                        // CONTENIDO

                        page.Content().Column(columna =>
                        {
                            columna.Spacing(10);

                            // INFORMACIÓN DE LA FACTURA

                            columna.Item().Text("Información de la factura").FontSize(14).Bold();

                            columna.Item().Row(fila =>
                            {
                                fila.RelativeItem().Column(col =>
                                {
                                    col.Item().Text("Fecha de emisión: " + Convert.ToDateTime(datos["FechaEmision"]).ToString("dd/MM/yyyy"));

                                    if (datos["FechaVencimiento"] != DBNull.Value)
                                    {
                                        col.Item().Text("Fecha de vencimiento: " + Convert.ToDateTime(datos["FechaVencimiento"]).ToString("dd/MM/yyyy"));
                                    }
                                });

                                fila.RelativeItem().Column(col =>
                                {
                                    col.Item().Text("N.º de venta: " + datos["IdVenta"].ToString());
                                });
                            });

                            // DATOS DEL CLIENTE

                            columna.Item().Text("Datos del cliente").FontSize(14).Bold();

                            columna.Item().Column(col =>
                            {
                                col.Item().Text($"Cliente: {datos["Cliente"]}");
                                col.Item().Text($"Documento: {datos["Documento"]}");
                                col.Item().Text($"Teléfono: {datos["Telefono"]}");
                                col.Item().Text($"Correo: {datos["Correo"]}");
                            });

                            // DETALLE DE LA VENTA

                            columna.Item().Text("Detalle de la venta").FontSize(14).Bold();

                            columna.Item().Table(tabla =>
                            {
                                tabla.ColumnsDefinition(columnas =>
                                {
                                    columnas.RelativeColumn(4);
                                    columnas.RelativeColumn(1);
                                    columnas.RelativeColumn(2);
                                    columnas.RelativeColumn(2);
                                });

                                // ENCABEZADOS DE LA TABLA

                                tabla.Header(encabezado =>
                                {
                                    encabezado.Cell().Element(EstiloCeldaEncabezado).Text("Producto");

                                    encabezado.Cell().Element(EstiloCeldaEncabezado).AlignCenter().Text("Cantidad");

                                    encabezado.Cell().Element(EstiloCeldaEncabezado).AlignRight().Text("Precio");

                                    encabezado.Cell().Element(EstiloCeldaEncabezado).AlignRight().Text("Subtotal");
                                });

                                // PRODUCTOS

                                foreach (DataRow filaDetalle in detalle.Rows)
                                {
                                    tabla.Cell().Element(EstiloCelda).Text(filaDetalle["ProductoVendido"].ToString());

                                    tabla.Cell().Element(EstiloCelda).AlignCenter().Text(filaDetalle["Cantidad"].ToString());

                                    tabla.Cell().Element(EstiloCelda).AlignRight().Text("$ " + Convert.ToDecimal(filaDetalle["PrecioUnitario"]).ToString("0.00"));

                                    tabla.Cell().Element(EstiloCelda).AlignRight().Text("$ " + Convert.ToDecimal(filaDetalle["SubTotal"]).ToString("0.00"));
                                }
                            });

                            // TOTALES

                            columna.Item().AlignRight().Column(totales =>
                            {
                                decimal subtotal = Convert.ToDecimal(datos["SubTotal"]);
                                decimal descuento = Convert.ToDecimal(datos["Descuento"]);
                                decimal iva = Convert.ToDecimal(datos["IVA"]);
                                decimal total = Convert.ToDecimal(datos["Total"]);

                                totales.Item().Text("Subtotal: $ " + subtotal.ToString("0.00"));

                                totales.Item().Text("Descuento: $ " + descuento.ToString("0.00"));

                                totales.Item().Text("IVA (13%): $ " + iva.ToString("0.00"));

                                totales.Item().Text("TOTAL: $ " + total.ToString("0.00")).FontSize(14).Bold();
                            });

                            // OBSERVACIONES

                            string observaciones = datos["Observaciones"] == DBNull.Value ? "" : datos["Observaciones"].ToString();

                            if (!string.IsNullOrWhiteSpace(observaciones))
                            {
                                columna.Item().Text("Observaciones").FontSize(14).Bold();

                                columna.Item().Text(observaciones);
                            }
                        });

                        // PIE DE PÁGINA

                        page.Footer().AlignCenter().Text(texto =>
                        {
                            texto.Span("Factura generada por el sistema de " + ConfiguracionEmpresa.Nombre);
                        });
                    });
                }).GeneratePdf(rutaArchivo);

                // COMPROBAR QUE EL PDF SE CREÓ

                if (File.Exists(rutaArchivo))
                {
                    MessageBox.Show("PDF generado correctamente.\n\n" + "Ubicación:\n" + rutaArchivo, "PDF",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("El PDF no se encontró después de generarlo.\n\n" + "Ruta esperada:\n" + rutaArchivo, "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al generar el PDF:\n\n" + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // OBTENER LOS DATOS DE LA FACTURA

        private static DataTable ObtenerFactura(int idFactura)
        {
            DataTable dt = new DataTable();

            using (SqlConnection conexion = Conexion.Conectar())
            {
                string sql = @"SELECT * FROM VerFacturaEditar WHERE IdFactura = @IdFactura";

                using (SqlCommand comando = new SqlCommand(sql, conexion))
                {
                    comando.Parameters.AddWithValue("@IdFactura", idFactura);

                    using (SqlDataAdapter adapter = new SqlDataAdapter(comando))
                    {
                        adapter.Fill(dt);
                    }
                }
            }

            return dt;
        }

        // OBTENER EL DETALLE DE LA VENTA

        private static DataTable ObtenerDetalleVenta(int idVenta)
        {
            DataTable dt = new DataTable();

            using (SqlConnection conexion = Conexion.Conectar())
            {
                string sql = @"SELECT *FROM VerDetalleVenta WHERE IdVenta = @IdVenta";

                using (SqlCommand comando = new SqlCommand(sql, conexion))
                {
                    comando.Parameters.AddWithValue("@IdVenta", idVenta);

                    using (SqlDataAdapter adapter = new SqlDataAdapter(comando))
                    {
                        adapter.Fill(dt);
                    }
                }
            }

            return dt;
        }

        // ESTILO DEL ENCABEZADO DE LA TABLA

        private static IContainer EstiloCeldaEncabezado(IContainer container)
        {
            return container.BorderBottom(1).Padding(5);
        }

        // ESTILO DE LAS CELDAS

        private static IContainer EstiloCelda(IContainer container)
        {
            return container.BorderBottom(1).Padding(5);
        }
    }
}