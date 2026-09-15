using Modelo.Conexión_DB;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Modelo.Entidades
{
    public class GeneradorFactura
    {
        public static void Generar(int idFactura, string rutaArchivo)
        {
            try
            {
                // 1. OBTENER LOS DATOS GENERALES DE LA FACTURA

                DataTable factura = ObtenerFactura(idFactura);

                if (factura.Rows.Count == 0)
                {
                    MessageBox.Show(
                        "No se encontró la factura.",
                        "Factura",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);

                    return;
                }

                DataRow datos = factura.Rows[0];

                // =========================================================
                // 2. OBTENER EL DETALLE DE LA VENTA
                // =========================================================

                int idVenta = Convert.ToInt32(datos["IdVenta"]);

                DataTable detalle = ObtenerDetalleVenta(idVenta);

                // =========================================================
                // 3. CONFIGURAR QUESTPDF
                // =========================================================

                QuestPDF.Settings.License = LicenseType.Community;

                // =========================================================
                // 4. CREAR EL DOCUMENTO PDF
                // =========================================================

                Document.Create(documento =>
                {
                    documento.Page(page =>
                    {
                        page.Size(PageSizes.Letter);
                        page.Margin(40);

                        // =================================================
                        // ENCABEZADO
                        // =================================================

                        page.Header()
                            .Column(columna =>
                            {
                                columna.Item().Text("FACTURA").FontSize(24).Bold();

                                columna.Item().Text($"N.º {datos["IdFactura"]}").FontSize(12);

                                columna.Item().LineHorizontal(1);
                            });

                        // CONTENIDO
                        page.Content()
                            .Column(columna =>
                            {
                                columna.Spacing(10);

                                // INFORMACIÓN DE LA FACTURA

                                columna.Item()
                                    .Text("Información de la factura")
                                    .FontSize(14)
                                    .Bold();

                                columna.Item()
                                    .Row(fila =>
                                    {
                                        fila.RelativeItem()
                                            .Column(col =>
                                            {
                                                col.Item().Text(
                                                    $"Fecha de emisión: {Convert.ToDateTime(datos["FechaEmision"]):dd/MM/yyyy}");

                                                col.Item().Text(
                                                    $"Fecha de vencimiento: {Convert.ToDateTime(datos["FechaVencimiento"]):dd/MM/yyyy}");
                                            });

                                        fila.RelativeItem()
                                            .Column(col =>
                                            {
                                                col.Item().Text(
                                                    $"N.º de venta: {datos["IdVenta"]}");
                                            });
                                    });

                                // -----------------------------------------
                                // DATOS DEL CLIENTE
                                // -----------------------------------------

                                columna.Item()
                                    .Text("Datos del cliente")
                                    .FontSize(14)
                                    .Bold();

                                columna.Item()
                                    .Column(col =>
                                    {
                                        col.Item().Text($"Cliente: {datos["Cliente"]}");
                                        col.Item().Text($"Documento: {datos["Documento"]}");
                                        col.Item().Text($"Teléfono: {datos["Telefono"]}");
                                        col.Item().Text($"Correo: {datos["Correo"]}");
                                    });

                                // -----------------------------------------
                                // DETALLE DE PRODUCTOS
                                // -----------------------------------------

                                columna.Item()
                                    .Text("Detalle de la venta")
                                    .FontSize(14)
                                    .Bold();

                                columna.Item()
                                    .Table(tabla =>
                                    {
                                        tabla.ColumnsDefinition(columnas =>
                                        {
                                            columnas.RelativeColumn(4);
                                            columnas.RelativeColumn(1);
                                            columnas.RelativeColumn(2);
                                            columnas.RelativeColumn(2);
                                        });

                                        // Encabezados
                                        tabla.Header(encabezado =>
                                        {
                                            encabezado.Cell()
                                                .Element(EstiloCeldaEncabezado)
                                                .Text("Producto");

                                            encabezado.Cell()
                                                .Element(EstiloCeldaEncabezado)
                                                .AlignCenter()
                                                .Text("Cantidad");

                                            encabezado.Cell()
                                                .Element(EstiloCeldaEncabezado)
                                                .AlignRight()
                                                .Text("Precio");

                                            encabezado.Cell()
                                                .Element(EstiloCeldaEncabezado)
                                                .AlignRight()
                                                .Text("Subtotal");
                                        });

                                        // Productos
                                        foreach (DataRow filaDetalle in detalle.Rows)
                                        {
                                            tabla.Cell()
                                                .Element(EstiloCelda)
                                                .Text(filaDetalle["ProductoVendido"].ToString());

                                            tabla.Cell()
                                                .Element(EstiloCelda)
                                                .AlignCenter()
                                                .Text(filaDetalle["Cantidad"].ToString());

                                            tabla.Cell()
                                                .Element(EstiloCelda)
                                                .AlignRight()
                                                .Text(
                                                    Convert.ToDecimal(
                                                        filaDetalle["PrecioUnitario"])
                                                    .ToString("$ 0.00"));

                                            tabla.Cell()
                                                .Element(EstiloCelda)
                                                .AlignRight()
                                                .Text(
                                                    Convert.ToDecimal(
                                                        filaDetalle["SubTotal"])
                                                    .ToString("$ 0.00"));
                                        }
                                    });

                                // -----------------------------------------
                                // TOTALES
                                // -----------------------------------------

                                columna.Item()
                                    .AlignRight()
                                    .Column(totales =>
                                    {
                                        decimal subtotal =
                                            Convert.ToDecimal(datos["SubTotal"]);

                                        decimal descuento =
                                            Convert.ToDecimal(datos["Descuento"]);

                                        decimal iva =
                                            Convert.ToDecimal(datos["IVA"]);

                                        decimal total =
                                            Convert.ToDecimal(datos["Total"]);

                                        totales.Item()
                                            .Text($"Subtotal: $ {subtotal:0.00}");

                                        totales.Item()
                                            .Text($"Descuento: $ {descuento:0.00}");

                                        totales.Item()
                                            .Text($"IVA (13%): $ {iva:0.00}");

                                        totales.Item()
                                            .Text($"TOTAL: $ {total:0.00}")
                                            .FontSize(14)
                                            .Bold();
                                    });

                                // -----------------------------------------
                                // OBSERVACIONES
                                // -----------------------------------------

                                string observaciones =
                                    datos["Observaciones"] == DBNull.Value
                                        ? ""
                                        : datos["Observaciones"].ToString();

                                if (!string.IsNullOrWhiteSpace(observaciones))
                                {
                                    columna.Item()
                                        .Text("Observaciones")
                                        .FontSize(14)
                                        .Bold();

                                    columna.Item()
                                        .Text(observaciones);
                                }
                            });

                        // =================================================
                        // PIE DE PÁGINA
                        // =================================================

                        page.Footer()
                            .AlignCenter()
                            .Text(texto =>
                            {
                                texto.Span("Factura generada por el sistema");
                            });
                    });

                }).GeneratePdf(rutaArchivo);

                MessageBox.Show(
                    "PDF generado correctamente.",
                    "PDF",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Error al generar el PDF:\n" + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        // =============================================================
        // OBTENER FACTURA
        // =============================================================

        private static DataTable ObtenerFactura(int idFactura)
        {
            DataTable dt = new DataTable();

            using (SqlConnection conexion = Conexion.Conectar())
            {
                string sql = @"
                SELECT *
                FROM VerFacturaEditar
                WHERE IdFactura = @IdFactura";

                using (SqlCommand comando = new SqlCommand(sql, conexion))
                {
                    comando.Parameters.AddWithValue("@IdFactura", idFactura);

                    using (SqlDataAdapter adapter =
                        new SqlDataAdapter(comando))
                    {
                        adapter.Fill(dt);
                    }
                }
            }

            return dt;
        }

        // =============================================================
        // OBTENER DETALLE DE VENTA
        // =============================================================

        private static DataTable ObtenerDetalleVenta(int idVenta)
        {
            DataTable dt = new DataTable();

            using (SqlConnection conexion = Conexion.Conectar())
            {
                string sql = @"
                SELECT *
                FROM VerDetalleVenta
                WHERE IdVenta = @IdVenta";

                using (SqlCommand comando = new SqlCommand(sql, conexion))
                {
                    comando.Parameters.AddWithValue("@IdVenta", idVenta);

                    using (SqlDataAdapter adapter =
                        new SqlDataAdapter(comando))
                    {
                        adapter.Fill(dt);
                    }
                }
            }

            return dt;
        }

        // =============================================================
        // ESTILO DEL ENCABEZADO DE LA TABLA
        // =============================================================

        private static IContainer EstiloCeldaEncabezado(IContainer container)
        {
            return container
                .BorderBottom(1)
                .Padding(5);
        }

        // =============================================================
        // ESTILO DE LAS CELDAS
        // =============================================================

        private static IContainer EstiloCelda(IContainer container)
        {
            return container
                .BorderBottom(1)
                .Padding(5);
        }
    }
}
