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
    public class DetalleVenta  

    {
        private int IdDetalleVenta;
        private int IdVenta;
        private string ProductoVendido;
        private int Cantidad;
        private decimal PrecioUnitario;

        public int IdDetalleVenta1 { get => IdDetalleVenta; set => IdDetalleVenta=value; }
        public int IdVenta1 { get => IdVenta; set => IdVenta=value; }
        public string ProductoVendido1 { get => ProductoVendido; set => ProductoVendido=value; }
        public int Cantidad1 { get => Cantidad; set => Cantidad=value; }
        public decimal PrecioUnitario1 { get => PrecioUnitario; set => PrecioUnitario=value; }

        public DetalleVenta(int idVenta, string productoVendido, int cantidad, decimal precioUnitario)
        {
            IdVenta1=idVenta;
            ProductoVendido1=productoVendido;
            Cantidad1=cantidad;
            PrecioUnitario1=precioUnitario;
        }
        public DetalleVenta()
        {

        }

        public static DataTable CargarDetalleVenta(int idVenta)
        {
            SqlConnection conectar = Conexion.Conectar();

            string comando = @"
                SELECT *
                FROM VerDetalleVenta
                WHERE IdVenta = @IdVenta;";

            SqlDataAdapter adapter =
                new SqlDataAdapter(comando, conectar);

            adapter.SelectCommand.Parameters.AddWithValue(
                "@IdVenta",
                idVenta
            );

            DataTable dt = new DataTable();

            adapter.Fill(dt);

            return dt;
        }

        public bool InsertarDetalleVenta()
        {
            string comandoSQL = "INSERT INTO DetalleVenta(IdVenta, ProductoVendido, Cantidad, PrecioUnitario)" +
                " VALUES (@IdVenta, @ProductoVendido , @Cantidad, @PrecioUnitario);";

            using (SqlConnection conexion = Conexion.Conectar())
            {
                using (SqlCommand comandoObjeto = new SqlCommand(comandoSQL, conexion))
                {
                    // Agregar parámetros
                    comandoObjeto.Parameters.AddWithValue("@IdVenta", IdVenta);
                    comandoObjeto.Parameters.AddWithValue("@ProductoVendido", ProductoVendido);
                    comandoObjeto.Parameters.AddWithValue("@Cantidad", Cantidad);

                    SqlParameter parametroPrecio = comandoObjeto.Parameters.Add("@PrecioUnitario", SqlDbType.Decimal);
                    parametroPrecio.Precision = 10;
                    parametroPrecio.Scale = 2;
                    parametroPrecio.Value = PrecioUnitario;

                    try
                    {
                        int filaAfectada = comandoObjeto.ExecuteNonQuery();

                        return filaAfectada > 0;
                    }
                    catch (SqlException ex)
                    {
                        switch (ex.Number)
                        {
                            case 2627:
                            case 2601:

                                MessageBox.Show(
                                    "El detalle de venta ya existe en la base de datos.",
                                    "Registro Duplicado",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Warning);

                                break;

                            case 547:

                                MessageBox.Show(
                                    "La venta o el producto indicado no existe en la base de datos.",
                                    "Error de relación",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Warning);

                                break;

                            default:

                                MessageBox.Show(
                                    "Ocurrió un error inesperado en la base de datos "
                                    + ex.Message,
                                    "Error " + ex.Number,
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Warning);

                                break;
                        }

                        return false;
                    }
                }
            }
        }
        
        public bool ActualizarDetalleVenta()
        {
            string comandoSQL = @"UPDATE DetalleVenta SET ProductoVendido = @ProductoVendido, Cantidad = @Cantidad, PrecioUnitario = @PrecioUnitario 
                                  WHERE IdDetalleVenta = @IdDetalleVenta";
            using (SqlConnection conexion = Conexion.Conectar())
            {
                using (SqlCommand comandoObjeto = new SqlCommand(comandoSQL, conexion))
                {
                    comandoObjeto.Parameters.AddWithValue("@ProductoVendido", ProductoVendido1);
                    comandoObjeto.Parameters.AddWithValue("@Cantidad", Cantidad1);
                    comandoObjeto.Parameters.AddWithValue("@PrecioUnitario", PrecioUnitario1);
                    comandoObjeto.Parameters.AddWithValue("@IdDetalleVenta", IdDetalleVenta1);
                    
                    try { return comandoObjeto.ExecuteNonQuery() > 0; }
                    catch (SqlException) { return false; }
                }
            }
        }

        public static bool EliminarDetalleVenta(int idDetalleVenta)
        {
            using (SqlConnection conexion = Conexion.Conectar())
            {
                string comandoSQL = "DELETE FROM DetalleVenta WHERE IdDetalleVenta = @IdDetalleVenta";
                using (SqlCommand comandoObjeto = new SqlCommand(comandoSQL, conexion))
                {
                    comandoObjeto.Parameters.AddWithValue("@IdDetalleVenta", idDetalleVenta);
                    try { return comandoObjeto.ExecuteNonQuery() > 0; }
                    catch (SqlException) { return false; }
                }
            }
        }
    }
}
    
