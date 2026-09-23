using Modelo.Conexión_DB;
using System;
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

        public ProductosCotizacion(int idProductosCotizacion, string descripcion_Del_Mueble, int largo, int ancho, int alto, int cantidad, double precioUnitario, double subTotal, int idcotizacion)
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

        public int IdProductosCotizacion1 { get => IdProductosCotizacion; set => IdProductosCotizacion = value; }
        public string Descripcion_Del_Mueble1 { get => Descripcion_Del_Mueble; set => Descripcion_Del_Mueble = value; }
        public int Largo1 { get => Largo; set => Largo = value; }
        public int Ancho1 { get => Ancho; set => Ancho = value; }
        public int Alto1 { get => Alto; set => Alto = value; }
        public int Cantidad1 { get => Cantidad; set => Cantidad = value; }
        public double PrecioUnitario1 { get => PrecioUnitario; set => PrecioUnitario = value; }
        public double SubTotal1 { get => SubTotal; set => SubTotal = value; }
        public int Idcotizacion1 { get => Idcotizacion; set => Idcotizacion = value; }

        public bool InsertarProductoCotizacion()
        {
            string comandoSQL = @"INSERT INTO Productos_Cotizacion (DescripcionMueble, Largo, Ancho, Alto, Cantidad, PrecioUnitario, SubTotal, IdCotizacion)
                VALUES (@DescripcionMueble, @Largo, @Ancho, @Alto, @Cantidad, @PrecioUnitario, @SubTotal, @IdCotizacion);";

            try
            {
                using (SqlConnection conexion = Conexion.Conectar())
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

                    int filasAfectadas = comandoObjeto.ExecuteNonQuery();

                    return filasAfectadas > 0;
                }
            }
            catch (SqlException ex)
            {
                switch (ex.Number)
                {
                    case 2627:
                        MessageBox.Show("El producto ya está registrado en la cotización.", "ERR-SQL-005", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        break;

                    case 2601:
                        MessageBox.Show("El producto ya está registrado y existe un índice único duplicado.", "ERR-SQL-006", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        break;

                    case 547:
                        MessageBox.Show("La cotización indicada no existe o los datos relacionados no son válidos.", "ERR-SQL-007", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        break;

                    case 515:
                        MessageBox.Show("Hay campos obligatorios sin completar.", "ERR-SQL-008", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        break;

                    case 245:
                        MessageBox.Show("Uno de los valores ingresados tiene un formato incorrecto.", "ERR-SQL-009", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        break;

                    case 8115:
                        MessageBox.Show("Uno de los valores numéricos excede el límite permitido.", "ERR-SQL-010", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        break;

                    case 8152:
                        MessageBox.Show("Uno de los datos ingresados es demasiado largo para la columna correspondiente.", "ERR-SQL-011", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        break;

                    case 53:
                        MessageBox.Show("No se pudo establecer conexión con el servidor SQL.", "ERR-SQL-001", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;

                    case 4060:
                        MessageBox.Show("No se pudo acceder a la base de datos.", "ERR-SQL-002", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;

                    case -2:
                        MessageBox.Show("La operación tardó demasiado tiempo.", "ERR-SQL-003", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;

                    case 208:
                        MessageBox.Show("La tabla, vista o procedimiento utilizado no existe.", "ERR-SQL-004", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;

                    default:
                        MessageBox.Show("Ocurrió un error inesperado de SQL.\n" + ex.Message, "ERR-SQL-999", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                }

                return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrió un error inesperado.\n" + ex.Message, "ERR-SQL-999", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }
    }
}