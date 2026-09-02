using Modelo.Conexión_DB;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Modelo.Entidades
{
    public class ProductosCotizacion
    {
        private int IdProductosCotizacion;
        private string Descripcion_Del_Mueble;
        private int Largo;
        private int Ancho;
        private int Alto;
        private int Cantidad;
        private double PrecioUnitario;
        private double SubTotal;
        private int Idcotizacion;

        public ProductosCotizacion(int idProductosCotizacion, string descripcion_Del_Mueble, int largo, int ancho, int alto, int cantidad, double precioUnitario, double subTotal, int idcotizacion)
        {
            IdProductosCotizacion1=idProductosCotizacion;
            Descripcion_Del_Mueble1=descripcion_Del_Mueble;
            Largo1=largo;
            Ancho1=ancho;
            Alto1=alto;
            Cantidad1=cantidad;
            PrecioUnitario1=precioUnitario;
            SubTotal1=subTotal;
            Idcotizacion1=idcotizacion;
        }
        public ProductosCotizacion()
        {

        }

        public int IdProductosCotizacion1 { get => IdProductosCotizacion; set => IdProductosCotizacion=value; }
        public string Descripcion_Del_Mueble1 { get => Descripcion_Del_Mueble; set => Descripcion_Del_Mueble=value; }
        public int Largo1 { get => Largo; set => Largo=value; }
        public int Ancho1 { get => Ancho; set => Ancho=value; }
        public int Alto1 { get => Alto; set => Alto=value; }
        public int Cantidad1 { get => Cantidad; set => Cantidad=value; }
        public double PrecioUnitario1 { get => PrecioUnitario; set => PrecioUnitario=value; }
        public double SubTotal1 { get => SubTotal; set => SubTotal=value; }
        public int Idcotizacion1 { get => Idcotizacion; set => Idcotizacion=value; }

        
        public bool InsertarProductoCotizacion()
        {
            string comandoSQL = @"INSERT INTO Productos_Cotizacion (DescripcionMueble, Largo, Ancho, Alto, Cantidad, PrecioUnitario, SubTotal, IdCotizacion) 
                                  VALUES (@DescripcionMueble, @Largo, @Ancho, @Alto, @Cantidad, @PrecioUnitario, @SubTotal, @IdCotizacion);";

            using (SqlConnection conexion = Conexion.Conectar())
            {
                using (SqlCommand comandoObjeto = new SqlCommand(comandoSQL, conexion))
                {
                    comandoObjeto.Parameters.AddWithValue("@DescripcionMueble", Descripcion_Del_Mueble1);
                    comandoObjeto.Parameters.AddWithValue("@Largo", Largo1);
                    comandoObjeto.Parameters.AddWithValue("@Ancho", Ancho1);
                    comandoObjeto.Parameters.AddWithValue("@Alto", Alto1);
                    comandoObjeto.Parameters.AddWithValue("@Cantidad", Cantidad1);
                    comandoObjeto.Parameters.AddWithValue("@PrecioUnitario", PrecioUnitario1);
                    comandoObjeto.Parameters.AddWithValue("@SubTotal", SubTotal1);
                    comandoObjeto.Parameters.AddWithValue("@IdCotizacion", Idcotizacion1);

                    try
                    {
                        int filaAfectada = comandoObjeto.ExecuteNonQuery();
                        return filaAfectada > 0;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error: " + ex.Message);
                        return false;
                    }
                }
            }
        }

        public static DataTable CargarDetalleCotizacion()
        {
            SqlConnection conectar = Conexion.Conectar();

            string comando = " SELECT * FROM Pedido\r\nWHERE FechaDePedido >= '2026-07-01';";
            SqlDataAdapter adapter = new SqlDataAdapter(comando, conectar);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            return dt;
        }

        public static DataTable CargarProductosPorCotizacion(int idCotizacion)
        {
            SqlConnection conectar = Conexion.Conectar();

            string comando = @"SELECT IdProductosCotizacion,DescripcionMueble,Largo,Ancho,Alto,Cantidad,PrecioUnitario,SubTotal 
                             FROM Productos_Cotizacion WHERE IdCotizacion = @IdCotizacion;";

            SqlDataAdapter adapter = new SqlDataAdapter(comando, conectar);

            adapter.SelectCommand.Parameters.AddWithValue("@IdCotizacion",idCotizacion);

            DataTable dt = new DataTable();

            adapter.Fill(dt);

            return dt;
        }


    }
}
