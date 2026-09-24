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
                    string comando = @"SELECT * 
                                       FROM VerDetalleVenta 
                                       WHERE IdVenta = @IdVenta;";

                    using (SqlDataAdapter adapter = new SqlDataAdapter(comando, conectar))
                    {
                        adapter.SelectCommand.Parameters.AddWithValue("@IdVenta", idVenta);

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
                    case 53:
                        MessageBox.Show("No se puede conectar al servidor SQL.", "ERR-SQL-001", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;

                    case 4060:
                        MessageBox.Show("No se puede acceder a la base de datos.", "ERR-SQL-002", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;

                    case -2:
                        MessageBox.Show("Tiempo de espera agotado.", "ERR-SQL-003", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;

                    case 208:
                        MessageBox.Show("Tabla, vista o procedimiento no encontrado.", "ERR-SQL-004", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;

                    case 2627:
                        MessageBox.Show("Registro duplicado por clave primaria/UNIQUE.", "ERR-SQL-005", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;

                    case 2601:
                        MessageBox.Show("Registro duplicado por índice UNIQUE.", "ERR-SQL-006", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;

                    case 547:
                        MessageBox.Show("Violación de FK/CHECK.", "ERR-SQL-007", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;

                    case 515:
                        MessageBox.Show("Campo NOT NULL sin valor.", "ERR-SQL-008", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;

                    case 245:
                        MessageBox.Show("Conversión o formato de datos incorrecto.", "ERR-SQL-009", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;

                    case 8115:
                        MessageBox.Show("Desbordamiento numérico.", "ERR-SQL-010", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;

                    case 8152:
                        MessageBox.Show("Datos demasiado largos para la columna.", "ERR-SQL-011", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;

                    default:
                        MessageBox.Show("Error inesperado de SQL.", "ERR-SQL-999", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                }

                return new DataTable();
            }
            catch (Exception)
            {
                MessageBox.Show("Ocurrió un error inesperado.", "ERR-SQL-999", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return new DataTable();
            }
        }
    }
}