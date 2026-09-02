using Modelo.Conexión_DB;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Modelo.Entidades
{
    public class DbFactura
    {
        private int IdFactura;
        private DateTime FechaEmision;
        private DateTime FechaVencimiento;
        private int IdVenta;
        private string Observaciones;

        public DbFactura(int idFactura, DateTime fechaEmisión, DateTime fechaVencimiento, int venta, string observaciones)
        {
            IdFactura = idFactura;
            FechaEmision = fechaEmisión;
            FechaVencimiento = fechaVencimiento;
            IdVenta = venta;
            Observaciones = observaciones;
        }

        public int IdFactura1 { get => IdFactura; set => IdFactura = value; }
        public DateTime FechaEmisión1 { get => FechaEmision; set => FechaEmision = value; }
        public DateTime FechaVencimiento1 { get => FechaVencimiento; set => FechaVencimiento = value; }
        public int Venta1 { get => IdVenta; set => IdVenta = value; }
        public string Observaciones1 { get => Observaciones; set => Observaciones = value; }


        public static DataTable CargarRegistrosFacturas()
        {
            SqlConnection conectar = Conexion.Conectar();

            string comando = "SELECT * FROM VerFacturas;";
            SqlDataAdapter adapter = new SqlDataAdapter(comando, conectar);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            return dt;
        }



        public void InsertarFactura()
        {
            string sql = @"INSERT INTO Factura(FechaEmision,FechaVencimiento,  IdVenta, Observaciones)
                       VALUES(@FechaEmision,@FechaVencimiento, @IdVenta,@Observaciones)";

            using (SqlConnection cn = Conexion.Conectar())
            {
                using (SqlCommand cmd = new SqlCommand(sql, cn))
                {
                    cmd.Parameters.AddWithValue("@FechaEmision", FechaEmision);
                    cmd.Parameters.AddWithValue("@FechaVencimiento", FechaVencimiento);
                    cmd.Parameters.AddWithValue("@IdVenta", IdVenta);
                    cmd.Parameters.AddWithValue("@Observaciones", Observaciones);

                    cmd.ExecuteNonQuery();
                }
            }
        }


        public static DataTable BuscarVentaParaFactura(int idVenta)
        {
            SqlConnection conectar = Conexion.Conectar();
            string comando = @"SELECT * FROM VerVentasParaFactura WHERE [#] = @IdVenta;";

            SqlDataAdapter adapter = new SqlDataAdapter(comando, conectar);

            adapter.SelectCommand.Parameters.AddWithValue("@IdVenta", idVenta);

            DataTable dt = new DataTable();
            adapter.Fill(dt);

            return dt;
        }

        //Metodo que manda la informacion de la venta para factura
        public static DataTable CargarDetalleVentaParaFactura(int idVenta)
        {
            SqlConnection conectar = Conexion.Conectar();

            string comando = @"SELECT * FROM VerDetalleVenta WHERE IdVenta = @IdVenta;";

            SqlDataAdapter adapter = new SqlDataAdapter(comando, conectar);

            adapter.SelectCommand.Parameters.AddWithValue("@IdVenta", idVenta);

            DataTable dt = new DataTable();
            adapter.Fill(dt);

            return dt;
        }

        //Carga los datos al formulario de editar la factura
        public static DataTable CargarFacturaPorId(int idFactura)
        {
            SqlConnection conectar = Conexion.Conectar();

            string comando = @"SELECT * FROM VerFacturaEditar WHERE IdFactura = @IdFactura";

            SqlDataAdapter adapter = new SqlDataAdapter(comando, conectar);

            adapter.SelectCommand.Parameters.AddWithValue("@IdFactura", idFactura);

            DataTable dt = new DataTable();
            adapter.Fill(dt);

            return dt;

        }


        public static void ActualizarFactura(int idFactura, DateTime fechaVencimiento, decimal? descuento, string observaciones)
        {
            string sql = @"UPDATE Factura  SET FechaVencimiento = @FechaVencimiento, Descuento = @Descuento,Observaciones = @Observaciones WHERE IdFactura = @IdFactura";

            using (SqlConnection cn = Conexion.Conectar())
            using (SqlCommand cmd = new SqlCommand(sql, cn))
            {
                cmd.Parameters.AddWithValue("@FechaVencimiento", fechaVencimiento);

                cmd.Parameters.AddWithValue("@Descuento", (object)descuento ?? DBNull.Value);

                cmd.Parameters.AddWithValue("@Observaciones", string.IsNullOrWhiteSpace(observaciones)
                        ? (object)DBNull.Value
                        : observaciones);

                cmd.Parameters.AddWithValue("@IdFactura", idFactura);

                cmd.ExecuteNonQuery();
            }
        }

    }

}
