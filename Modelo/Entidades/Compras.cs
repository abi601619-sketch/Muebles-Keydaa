using Modelo.Conexión_DB;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Modelo.Entidades
{
    public class ComprasDb
    {
        private int IdCompra;
        private DateTime FechaCompra;
        private decimal TotalCompra;
        private int IdProveedor;

        public ComprasDb() { }

        public ComprasDb(int idCompra, DateTime fechaCompra, decimal totalCompra, int idProveedor)
        {
            IdCompra1 = idCompra;
            FechaCompra1 = fechaCompra;
            TotalCompra1 = totalCompra;
            IdProveedor1 = idProveedor;
        }

        public int IdCompra1 { get => IdCompra; set => IdCompra = value; }
        public DateTime FechaCompra1 { get => FechaCompra; set => FechaCompra = value; }
        public decimal TotalCompra1 { get => TotalCompra; set => TotalCompra = value; }
        public int IdProveedor1 { get => IdProveedor; set => IdProveedor = value; }

        public static DataTable CargarComprasRegistradas()
        {
            SqlConnection conectar = Conexion.Conectar();

            string comando = "SELECT * FROM VerCompras;";
            SqlDataAdapter adapter = new SqlDataAdapter(comando, conectar);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            return dt;
        }

        public int InsertarCompra()
        {
            string comandoSQL = @"INSERT INTO Compras (FechaCompra,TotalCompra,IdProveedor)VALUES
            (@FechaCompra,@TotalCompra,@IdProveedor); SELECT CAST(SCOPE_IDENTITY() AS INT);"; //SELECT CAST(SCOPE_IDENTITY() AS INT); SIRVE PARA OBTENER EL ID QUE SLQ RECIENTEMENTE GUARDO

            using (SqlConnection conexion = Conexion.Conectar())
            {
                using (SqlCommand comandoObjeto = new SqlCommand(comandoSQL, conexion))
                {
                    comandoObjeto.Parameters.AddWithValue("@FechaCompra", FechaCompra);

                    comandoObjeto.Parameters.AddWithValue("@IdProveedor", IdProveedor);

                    SqlParameter parametro = comandoObjeto.Parameters.Add("@TotalCompra", SqlDbType.Decimal);
                    parametro.Precision = 10;
                    parametro.Scale = 2;
                    parametro.Value = TotalCompra;

                    try
                    {
                        int idCompra =
                            Convert.ToInt32(comandoObjeto.ExecuteScalar());

                        return idCompra;
                    }
                    catch (SqlException ex)
                    {
                        MessageBox.Show("Ocurrió un error al guardar la compra.\n\n" + ex.Message, "Error " + ex.Number, MessageBoxButtons.OK, MessageBoxIcon.Error);

                        return 0;
                    }
                }

            }
        }

        public bool EliminarCompra()
        {
            using (SqlConnection conexion = Conexion.Conectar())
            {
                SqlTransaction transaccion = conexion.BeginTransaction();
                try
                {
                    string cmdDetalle = "DELETE FROM DetalleCompraMaterial WHERE IdCompra = @IdCompra;";
                    using (SqlCommand cmd = new SqlCommand(cmdDetalle, conexion, transaccion))
                    {
                        cmd.Parameters.AddWithValue("@IdCompra", IdCompra1);
                        cmd.ExecuteNonQuery();
                    }

                    string cmdCompra = "DELETE FROM Compras WHERE IdCompra = @IdCompra;";
                    using (SqlCommand cmd = new SqlCommand(cmdCompra, conexion, transaccion))
                    {
                        cmd.Parameters.AddWithValue("@IdCompra", IdCompra1);
                        int filas = cmd.ExecuteNonQuery();

                        if (filas > 0)
                        {
                            transaccion.Commit();
                            return true;
                        }
                        else
                        {
                            transaccion.Rollback();
                            return false;
                        }
                    }
                }
                catch (Exception)
                {
                    transaccion.Rollback();
                    return false;
                }
            }
        }

        //Metodo para buscar una compra ya registrada
        public static DataTable Buscar(string termino)
        {
            SqlConnection con = Conexion.Conectar();
            string comando = @"SELECT *
                   FROM VerCompras
                   WHERE CAST(IdCompra AS VARCHAR) LIKE @buscar
                      OR Proveedor LIKE @buscar;";

            SqlDataAdapter ad = new SqlDataAdapter(comando, con);

            ad.SelectCommand.Parameters.AddWithValue("@buscar", "%" + termino + "%");

            DataTable dt = new DataTable();

            ad.Fill(dt);

            return dt;
        }


    }
}
