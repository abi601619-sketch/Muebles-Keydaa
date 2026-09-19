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
                //OBTENER LOS DATOS GENERALES DE LA FACTURA
                //Manda los datos generales de la factura segun el ID de la factura

                DataTable factura = ObtenerFactura(idFactura);
                //Comprueba que la factura si exista
                if (factura.Rows.Count == 0)
                {
                    MessageBox.Show("No se encontró la factura.", "Factura", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    return;
                }
                //Aqui pide la ultima fila, es decir el ID de la factura
                DataRow datos = factura.Rows[0];

                //Aqui se obtiene el id de la venta para poder guardar los productos de esa venta
                int idVenta = Convert.ToInt32(datos["IdVenta"]);

                //Aqui ya se obtienen los productos que se vendieron en esa venta
                DataTable detalle = ObtenerDetalleVenta(idVenta);

                //Configura la licencia del QuestPDF
                QuestPDF.Settings.License = LicenseType.Community;

                //Crea el documento PDF
                Document.Create(documento =>
                {
                    documento.Page(page =>
                    {
                        //Establece el tamaño de la pagina, en este caso es Carta
                        page.Size(PageSizes.Letter);

                        //MARGEN
                        page.Margin(40);

                        //Luego configuramos el encabezado de esta pagina, es  decir todo lo que este dentro de este bloque será el encabezado visible
                        //en el documento PDF
                        page.Header()
                            .Column(columna =>
                            {
                                // LOGO DE LA EMPRESA
                                string rutaLogo = ConfiguracionEmpresa.Logo;

                                if (!string.IsNullOrWhiteSpace(rutaLogo) &&
                                    File.Exists(rutaLogo))
                                {
                                    byte[] logo = File.ReadAllBytes(rutaLogo);

                                    columna.Item()
                                        .Height(70)
                                        .Width(180)
                                        .AlignLeft()
                                        .Image(logo)
                                        .FitArea();
                                }
                                else
                                {
                                    columna.Item()
                                        .Text(ConfiguracionEmpresa.Nombre)
                                        .FontSize(20)
                                        .Bold();
                                }

                                columna.Item()
                                    .PaddingTop(5)
                                    .Text(ConfiguracionEmpresa.Nombre)
                                    .FontSize(12)
                                    .Bold();

                                columna.Item()
                                    .Text(
                                        "Teléfono: " +
                                        ConfiguracionEmpresa.Telefono
                                    )
                                    .FontSize(8);

                                columna.Item()
                                    .Text(
                                        "Correo: " +
                                        ConfiguracionEmpresa.Correo
                                    )
                                    .FontSize(8);

                                columna.Item()
                                    .Text(
                                        "Dirección: " +
                                        ConfiguracionEmpresa.Direccion
                                    )
                                    .FontSize(8);

                                columna.Item()
                                    .PaddingTop(5)
                                    .Text("FACTURA")
                                    .FontSize(24)
                                    .Bold();

                                //obtiene el numero de la factura y le da un tamaño adecuado
                                columna.Item()
                                    .Text(
                                        "N.º " +
                                        datos["IdFactura"].ToString()
                                    )
                                    .FontSize(12);

                                //Linea horinzontal como decorativo visual
                                columna.Item().LineHorizontal(1);
                            });

                        // LUEGO SE CONFIGURA EL CUERPO DE LA FACTURA, YA QUE YA SE TIENE EL ENCABEZADO
                        page.Content()
                            .Column(columna =>
                            {
                                //Coloca un espacio entre los elementos
                                columna.Spacing(10);

                                // INFORMACIÓN DE LA FACTURA

                                columna.Item().Text("Información de la factura").FontSize(14).Bold();

                                columna.Item()

                                //Row permite que los elementos se coloquen de forma horizontal
                                    .Row(fila =>
                                    {
                                        //Divide el espacio de forma proporcional
                                        fila.RelativeItem()
                                            .Column(col =>
                                            {
                                                col.Item().Text(
                                                    "Fecha de emisión: " +
                                                    Convert.ToDateTime(
                                                        datos["FechaEmision"]
                                                    ).ToString("dd/MM/yyyy")
                                                );

                                                col.Item().Text(
                                                    "Fecha de vencimiento: " +
                                                    Convert.ToDateTime(
                                                        datos["FechaVencimiento"]
                                                    ).ToString("dd/MM/yyyy")
                                                );
                                            });

                                        fila.RelativeItem()
                                        //Obtiene el ID de la venta y lo muestra
                                            .Column(col =>
                                            {
                                                col.Item().Text(
                                                    "N.º de venta: " +
                                                    datos["IdVenta"].ToString()
                                                );
                                            });
                                    });
                                //Datos del clientes
                                columna.Item().Text("Datos del cliente").FontSize(14).Bold();

                                columna.Item()
                                    .Column(col =>
                                    {
                                        //Obtiene todos los datos del cliente y configuramos como se vera el texto en el PDF
                                        col.Item().Text($"Cliente: {datos["Cliente"]}");
                                        col.Item().Text($"Documento: {datos["Documento"]}");
                                        col.Item().Text($"Teléfono: {datos["Telefono"]}");
                                        col.Item().Text($"Correo: {datos["Correo"]}");
                                    });

                                //DETALLE DE LOS PRODUCTOS DE LA VENTA, PARA MOSTRARLOS EN LA FACTURA
                                columna.Item().Text("Detalle de la venta").FontSize(14).Bold();

                                columna.Item()
                                    .Table(tabla =>
                                    {
                                        //Crea una tabla con sus columnas dandoles un tamaño, con los números de RELATIVE
                                        //Segun el numero es el espacio que cada encabezado tendra
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
                                            encabezado.Cell().Element(EstiloCeldaEncabezado).Text("Producto");

                                            encabezado.Cell().Element(EstiloCeldaEncabezado).AlignCenter().Text("Cantidad");

                                            encabezado.Cell().Element(EstiloCeldaEncabezado).AlignRight().Text("Precio");

                                            encabezado.Cell().Element(EstiloCeldaEncabezado).AlignRight().Text("Subtotal");
                                        });

                                        // Productos
                                        //Con el bucle se recorren todos los productos, Y todos se convierten en string
                                        foreach (DataRow filaDetalle in detalle.Rows)
                                        {
                                            tabla.Cell().Element(EstiloCelda).Text(filaDetalle["ProductoVendido"].ToString());

                                            tabla.Cell().Element(EstiloCelda).AlignCenter().Text(filaDetalle["Cantidad"].ToString());

                                            tabla.Cell().Element(EstiloCelda)
                                                .AlignRight()
                                                .Text(
                                                    "$ " +
                                                    Convert.ToDecimal(
                                                        filaDetalle["PrecioUnitario"]
                                                    ).ToString("0.00")
                                                );

                                            tabla.Cell().Element(EstiloCelda)
                                                .AlignRight()
                                                .Text(
                                                    "$ " +
                                                    Convert.ToDecimal(
                                                        filaDetalle["SubTotal"]
                                                    ).ToString("0.00")
                                                );
                                        }
                                    });

                                //Se convierten los totales
                                columna.Item().AlignRight().Column(totales =>
                                {
                                    decimal subtotal = Convert.ToDecimal(datos["SubTotal"]);

                                    decimal descuento = Convert.ToDecimal(datos["Descuento"]);

                                    decimal iva = Convert.ToDecimal(datos["IVA"]);

                                    decimal total = Convert.ToDecimal(datos["Total"]);
                                    //Se muestran los totales como elemntos de la factura
                                    totales.Item().Text(
                                        "Subtotal: $ " +
                                        subtotal.ToString("0.00")
                                    );

                                    totales.Item().Text(
                                        "Descuento: $ " +
                                        descuento.ToString("0.00")
                                    );

                                    totales.Item().Text(
                                        "IVA (13%): $ " +
                                        iva.ToString("0.00")
                                    );
                                    //El total se muestra en negrita
                                    totales.Item()
                                        .Text(
                                            "TOTAL: $ " +
                                            total.ToString("0.00")
                                        )
                                        .FontSize(14)
                                        .Bold();
                                });


                                string observaciones = datos["Observaciones"] == DBNull.Value ? "" : datos["Observaciones"].ToString();

                                //SI EXITE LA FACTURA SE VA A MOSTRAR, SINO EXISTE NO SE VA A MOSTRAR
                                if (!string.IsNullOrWhiteSpace(observaciones))
                                {
                                    columna.Item().Text("Observaciones").FontSize(14).Bold();

                                    columna.Item().Text(observaciones);
                                }
                            });

                        //FOOTER DE LA PAGINA
                        page.Footer().AlignCenter().Text(texto =>
                        {
                            texto.Span(
                                "Factura generada por el sistema de " +
                                ConfiguracionEmpresa.Nombre
                            );
                        });
                    });

                }).GeneratePdf(rutaArchivo);

                MessageBox.Show("PDF generado correctamente.", "PDF", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al generar el PDF:\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //METODO PARA OBTENER LA FACTURA
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

        //METODO PARA OBTENER DETALLE DE VENTA
        private static DataTable ObtenerDetalleVenta(int idVenta)
        {
            DataTable dt = new DataTable();

            using (SqlConnection conexion = Conexion.Conectar())
            {
                string sql = @"SELECT * FROM VerDetalleVenta WHERE IdVenta = @IdVenta";

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
            //Se asigna el diseño de darlle una linea bajo cada Titulo con un espacio entre el texto y la linea separadora
            return container.BorderBottom(1).Padding(5);
        }

        //ESTILO DE LAS CELDAS
        private static IContainer EstiloCelda(IContainer container)
        {
            //Se asigna el diseño de darlle una linea bajo cada Celda con un espacio entre el texto y la linea separadora
            return container.BorderBottom(1).Padding(5);
        }
    }
}