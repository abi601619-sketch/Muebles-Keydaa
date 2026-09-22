using Modelo.Conexión_DB;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Modelo.Entidades
{
    public class DetalleVenta

    {
        private int IdDetalleVenta;
        private int IdVenta;
        private string ProductoVendido;
        private int Cantidad;
        private decimal PrecioUnitario;

        public int IdDetalleVenta1 { get => IdDetalleVenta; set => IdDetalleVenta = value; }
        public int IdVenta1 { get => IdVenta; set => IdVenta = value; }
        public string ProductoVendido1 { get => ProductoVendido; set => ProductoVendido = value; }
        public int Cantidad1 { get => Cantidad; set => Cantidad = value; }
        public decimal PrecioUnitario1 { get => PrecioUnitario; set => PrecioUnitario = value; }

        public DetalleVenta(int idVenta, string productoVendido, int cantidad, decimal precioUnitario)
        {
            IdVenta1 = idVenta;
            ProductoVendido1 = productoVendido;
            Cantidad1 = cantidad;
            PrecioUnitario1 = precioUnitario;
        }

        public DetalleVenta()
        {

        }

        public static DataTable CargarDetalleVenta(int idVenta)
        {
            try
            {
                using (SqlConnection conectar = Conexion.Conectar())
                {
                    string comando = @"
                SELECT *
                FROM VerDetalleVenta
                WHERE IdVenta = @IdVenta;";

                    using (SqlDataAdapter adapter =
                        new SqlDataAdapter(comando, conectar))
                    {
                        adapter.SelectCommand.Parameters.AddWithValue(
                            "@IdVenta",
                            idVenta);

                        DataTable dt = new DataTable();

                        adapter.Fill(dt);

                        return dt;
                    }
                }
            }
            catch (SqlException ex)
            {
                switch (ex.Number)
                {
                    case 208:
                        MessageBox.Show(
                            "No se encontró la vista VerDetalleVenta.",
                            "Error 208",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                        break;

                    case 53:
                        MessageBox.Show(
                            "No se pudo conectar con el servidor SQL.",
                            "Error 53",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                        break;

                    case 4060:
                        MessageBox.Show(
                            "No se pudo acceder a la base de datos.",
                            "Error 4060",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                        break;

                    case -2:
                        MessageBox.Show(
                            "La operación tardó demasiado tiempo.",
                            "Error -2",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                        break;

                    default:
                        MessageBox.Show(
                            "Ocurrió un error al cargar el detalle de venta.",
                            "Error " + ex.Number,
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                        break;
                }

                return new DataTable();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Ocurrió un error inesperado al cargar el detalle:\n" + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                return new DataTable();
            }
        }

    }
}
