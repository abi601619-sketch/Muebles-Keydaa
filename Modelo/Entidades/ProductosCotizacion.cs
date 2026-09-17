using Modelo.Conexión_DB;
using System;
using System.Data;
using System.Data.SqlClient;
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

        public ProductosCotizacion(
            int idProductosCotizacion,
            string descripcion_Del_Mueble,
            int largo,
            int ancho,
            int alto,
            int cantidad,
            double precioUnitario,
            double subTotal,
            int idcotizacion)
        {
            IdProductosCotizacion1 = idProductosCotizacion;
            Descripcion_Del_Mueble1 = descripcion_Del_Mueble;
            Largo1 = largo;
            Ancho1 = ancho;
            Alto1 = alto;
            Cantidad1 = cantidad;
            PrecioUnitario1 = precioUnitario;
            SubTotal1 = subTotal;
            Idcotizacion1 = idcotizacion;
        }

        public ProductosCotizacion()
        {

        }

        public int IdProductosCotizacion1
        {
            get => IdProductosCotizacion;
            set => IdProductosCotizacion = value;
        }

        public string Descripcion_Del_Mueble1
        {
            get => Descripcion_Del_Mueble;
            set => Descripcion_Del_Mueble = value;
        }

        public int Largo1
        {
            get => Largo;
            set => Largo = value;
        }

        public int Ancho1
        {
            get => Ancho;
            set => Ancho = value;
        }

        public int Alto1
        {
            get => Alto;
            set => Alto = value;
        }

        public int Cantidad1
        {
            get => Cantidad;
            set => Cantidad = value;
        }

        public double PrecioUnitario1
        {
            get => PrecioUnitario;
            set => PrecioUnitario = value;
        }

        public double SubTotal1
        {
            get => SubTotal;
            set => SubTotal = value;
        }

        public int Idcotizacion1
        {
            get => Idcotizacion;
            set => Idcotizacion = value;
        }


        public bool InsertarProductoCotizacion()
        {
            string comandoSQL = @"INSERT INTO Productos_Cotizacion 
                                  (DescripcionMueble, Largo, Ancho, Alto, Cantidad, PrecioUnitario, SubTotal, IdCotizacion) 
                                  VALUES 
                                  (@DescripcionMueble, @Largo, @Ancho, @Alto, @Cantidad, @PrecioUnitario, @SubTotal, @IdCotizacion);";

            try
            {
                using (SqlConnection conexion = Conexion.Conectar())
                {
                    using (SqlCommand comandoObjeto = new SqlCommand(comandoSQL, conexion))
                    {
                        comandoObjeto.Parameters.AddWithValue(
                            "@DescripcionMueble",
                            Descripcion_Del_Mueble1);

                        comandoObjeto.Parameters.AddWithValue(
                            "@Largo",
                            Largo1);

                        comandoObjeto.Parameters.AddWithValue(
                            "@Ancho",
                            Ancho1);

                        comandoObjeto.Parameters.AddWithValue(
                            "@Alto",
                            Alto1);

                        comandoObjeto.Parameters.AddWithValue(
                            "@Cantidad",
                            Cantidad1);

                        comandoObjeto.Parameters.AddWithValue(
                            "@PrecioUnitario",
                            PrecioUnitario1);

                        comandoObjeto.Parameters.AddWithValue(
                            "@SubTotal",
                            SubTotal1);

                        comandoObjeto.Parameters.AddWithValue(
                            "@IdCotizacion",
                            Idcotizacion1);

                        int filaAfectada = comandoObjeto.ExecuteNonQuery();

                        return filaAfectada > 0;
                    }
                }
            }
            catch (SqlException ex)
            {
                switch (ex.Number)
                {
                    case 2627:
                    case 2601:
                        MessageBox.Show(
                            "Error 2627/2601: El producto ya está registrado.",
                            "Error",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning);
                        break;

                    case 547:
                        MessageBox.Show(
                            "Error 547: La cotización indicada no existe.",
                            "Error",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning);
                        break;

                    case 515:
                        MessageBox.Show(
                            "Error 515: Hay campos obligatorios sin completar.",
                            "Error",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning);
                        break;

                    case 245:
                        MessageBox.Show(
                            "Error 245: Existe un valor con formato incorrecto.",
                            "Error",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning);
                        break;

                    case 8115:
                        MessageBox.Show(
                            "Error 8115: El valor numérico excede el límite permitido.",
                            "Error",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning);
                        break;

                    case 53:
                        MessageBox.Show(
                            "Error 53: No se pudo conectar con el servidor.",
                            "Error",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                        break;

                    case 4060:
                        MessageBox.Show(
                            "Error 4060: No se pudo acceder a la base de datos.",
                            "Error",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                        break;

                    case -2:
                        MessageBox.Show(
                            "Error -2: La operación tardó demasiado.",
                            "Error",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning);
                        break;

                    case 208:
                        MessageBox.Show(
                            "Error 208: La tabla o vista no existe.",
                            "Error",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                        break;

                    default:
                        MessageBox.Show(
                            "Error SQL " + ex.Number + ": " + ex.Message,
                            "Error",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                        break;
                }

                return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Error inesperado: " + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                return false;
            }
        }


        public static DataTable CargarDetalleCotizacion()
        {
            SqlConnection conectar = null;

            try
            {
                conectar = Conexion.Conectar();

                string comando = @"SELECT * 
                                   FROM Pedido
                                   WHERE FechaDePedido >= '2026-07-01';";

                SqlDataAdapter adapter = new SqlDataAdapter(comando, conectar);

                DataTable dt = new DataTable();

                adapter.Fill(dt);

                return dt;
            }
            catch (SqlException ex)
            {
                switch (ex.Number)
                {
                    case 208:
                        MessageBox.Show(
                            "Error 208: La tabla o vista no existe.",
                            "Error",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                        break;

                    case 53:
                        MessageBox.Show(
                            "Error 53: No se pudo conectar con el servidor.",
                            "Error",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                        break;

                    case 4060:
                        MessageBox.Show(
                            "Error 4060: No se pudo acceder a la base de datos.",
                            "Error",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                        break;

                    case -2:
                        MessageBox.Show(
                            "Error -2: La operación tardó demasiado.",
                            "Error",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning);
                        break;

                    default:
                        MessageBox.Show(
                            "Error SQL " + ex.Number + ": " + ex.Message,
                            "Error",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                        break;
                }

                return new DataTable();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Error inesperado: " + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                return new DataTable();
            }
            finally
            {
                if (conectar != null)
                {
                    conectar.Dispose();
                }
            }
        }


        public static DataTable CargarProductosPorCotizacion(int idCotizacion)
        {
            SqlConnection conectar = null;

            try
            {
                conectar = Conexion.Conectar();

                string comando = @"SELECT 
                                      IdProductosCotizacion,
                                      DescripcionMueble,
                                      Largo,
                                      Ancho,
                                      Alto,
                                      Cantidad,
                                      PrecioUnitario,
                                      SubTotal
                                   FROM Productos_Cotizacion
                                   WHERE IdCotizacion = @IdCotizacion;";

                SqlDataAdapter adapter = new SqlDataAdapter(comando, conectar);

                adapter.SelectCommand.Parameters.AddWithValue(
                    "@IdCotizacion",
                    idCotizacion);

                DataTable dt = new DataTable();

                adapter.Fill(dt);

                return dt;
            }
            catch (SqlException ex)
            {
                switch (ex.Number)
                {
                    case 208:
                        MessageBox.Show(
                            "Error 208: La tabla o vista no existe.",
                            "Error",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                        break;

                    case 53:
                        MessageBox.Show(
                            "Error 53: No se pudo conectar con el servidor.",
                            "Error",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                        break;

                    case 4060:
                        MessageBox.Show(
                            "Error 4060: No se pudo acceder a la base de datos.",
                            "Error",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                        break;

                    case -2:
                        MessageBox.Show(
                            "Error -2: La operación tardó demasiado.",
                            "Error",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning);
                        break;

                    case 245:
                        MessageBox.Show(
                            "Error 245: El ID de cotización no es válido.",
                            "Error",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning);
                        break;

                    default:
                        MessageBox.Show(
                            "Error SQL " + ex.Number + ": " + ex.Message,
                            "Error",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                        break;
                }

                return new DataTable();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Error inesperado: " + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                return new DataTable();
            }
            finally
            {
                if (conectar != null)
                {
                    conectar.Dispose();
                }
            }
        }
    }
}
