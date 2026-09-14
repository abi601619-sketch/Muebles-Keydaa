using Modelo.Conexión_DB;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Modelo.Entidades
{
    public class ReportesVentas
    {
        private int IdVenta;
        private int N_Factura;
        private string Nombre_De_Cliente;
        private DateTime FechaVenta;
        private string ProductosVendidos;
        private string MetodoPago;
        private double Subtotal;
        private double TotalAPagar;

        public ReportesVentas(int idVenta, int n_Factura, string nombre_De_Cliente, DateTime fechaVenta, string productosVendidos, string metodoPago, double subtotal, double totalAPagar)
        {
            IdVenta = idVenta;
            N_Factura = n_Factura;
            Nombre_De_Cliente = nombre_De_Cliente;
            FechaVenta = fechaVenta;
            ProductosVendidos = productosVendidos;
            MetodoPago = metodoPago;
            Subtotal = subtotal;
            TotalAPagar = totalAPagar;
        }

        public int IdVenta1 { get => IdVenta; set => IdVenta = value; }
        public int N_Factura1 { get => N_Factura; set => N_Factura = value; }
        public string Nombre_De_Cliente1 { get => Nombre_De_Cliente; set => Nombre_De_Cliente = value; }
        public DateTime FechaVenta1 { get => FechaVenta; set => FechaVenta = value; }
        public string ProductosVendidos1 { get => ProductosVendidos; set => ProductosVendidos = value; }
        public string MetodoPago1 { get => MetodoPago; set => MetodoPago = value; }
        public double Subtotal1 { get => Subtotal; set => Subtotal = value; }
        public double TotalAPagar1 { get => TotalAPagar; set => TotalAPagar = value; }

        public static DataTable CargarReporteVentas()
        {
            SqlConnection conectar = Conexion.Conectar();

            string comando = " SELECT* FROM ReporteDetalleVentas;";
            SqlDataAdapter adapter = new SqlDataAdapter(comando, conectar);

            DataTable dt = new DataTable();
            adapter.Fill(dt);

            return dt;
        }

        public static int ContarVentasTotales()
        {
            int total = 0;

            using (SqlConnection conexion = Conexion.Conectar())
            {
                string comandoSQL = "SELECT COUNT(*) FROM Venta;";

                using (SqlCommand comandoObjeto = new SqlCommand(comandoSQL, conexion))
                {
                    total = Convert.ToInt32(comandoObjeto.ExecuteScalar());
                }
            }

            return total;
        }

        public static int ContarFacturasEmitidas()
        {
            int total = 0;

            using (SqlConnection conexion = Conexion.Conectar())
            {
                string comandoSQL = "SELECT COUNT(*) FROM Factura;";

                using (SqlCommand comandoObjeto = new SqlCommand(comandoSQL, conexion))
                {
                    total = Convert.ToInt32(comandoObjeto.ExecuteScalar());
                }
            }

            return total;
        }
    }
}
