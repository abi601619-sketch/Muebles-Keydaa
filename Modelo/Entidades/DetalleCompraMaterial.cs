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
    public class DetalleCompraMaterial
    {
        private int IdDetalleCompraMaterial;
        private int IdCompra;
        private int IdMaterial;
        private int Cantidad;
        private decimal PrecioUnitario;

        

        public DetalleCompraMaterial()
        {

        }

        public DetalleCompraMaterial(int idDetalleCompraMaterial, int idCompra, int idMaterial, int cantidad, decimal precioUnitario)
        {
            IdDetalleCompraMaterial=idDetalleCompraMaterial;
            IdCompra=idCompra;
            IdMaterial=idMaterial;
            Cantidad=cantidad;
            PrecioUnitario=precioUnitario;
        }

        public int IdDetalleCompraMaterial1 { get => IdDetalleCompraMaterial; set => IdDetalleCompraMaterial=value; }
        public int IdCompra1 { get => IdCompra; set => IdCompra=value; }
        public int IdMaterial1 { get => IdMaterial; set => IdMaterial=value; }
        public int Cantidad1 { get => Cantidad; set => Cantidad=value; }
        public decimal PrecioUnitario1 { get => PrecioUnitario; set => PrecioUnitario=value; }

        public static DataTable CargarDetallesPorCompra(int idCompra)
        {
            string comandoSQL = @"
        SELECT d.IdDetalleCompraMaterial, d.IdCompra, d.IdMaterial, m.NombreDelMaterial as Material, d.Cantidad, d.PrecioUnitario
        FROM DetalleCompraMaterial d
        INNER JOIN Material m ON d.IdMaterial = m.IdMaterial
        WHERE d.IdCompra = @IdCompra;
    ";

            using (SqlConnection conexion = Conexion.Conectar())
            {
                using (SqlCommand comando =
                    new SqlCommand(comandoSQL, conexion))
                {
                    comando.Parameters.AddWithValue(
                        "@IdCompra",
                        idCompra);

                    SqlDataAdapter adapter =
                        new SqlDataAdapter(comando);

                    DataTable dt = new DataTable();

                    adapter.Fill(dt);

                    return dt;
                }
            }
        }

        public bool InsertarDetalleCompra()
        {
            using (SqlConnection conexion = Conexion.Conectar())
            {
                SqlTransaction transaccion = conexion.BeginTransaction();
                try
                {
                    string comandoSQL = @"INSERT INTO DetalleCompraMaterial(IdCompra,IdMaterial,Cantidad,PrecioUnitario)
                                          VALUES (@IdCompra,@IdMaterial,@Cantidad,@PrecioUnitario);";

                    using (SqlCommand comandoObjeto = new SqlCommand(comandoSQL, conexion, transaccion))
                    {
                        comandoObjeto.Parameters.AddWithValue("@IdCompra", IdCompra1);
                        comandoObjeto.Parameters.AddWithValue("@IdMaterial", IdMaterial1);
                        comandoObjeto.Parameters.AddWithValue("@Cantidad", Cantidad1);

                        SqlParameter parametro = comandoObjeto.Parameters.Add("@PrecioUnitario", SqlDbType.Decimal);
                        parametro.Precision = 10;
                        parametro.Scale = 2;
                        parametro.Value = PrecioUnitario1;

                        comandoObjeto.ExecuteNonQuery();
                    }

                    string updateSQL = "UPDATE Material SET Stock = Stock + @Cantidad WHERE IdMaterial = @IdMaterial;";
                    using (SqlCommand cmdUpdate = new SqlCommand(updateSQL, conexion, transaccion))
                    {
                        cmdUpdate.Parameters.AddWithValue("@Cantidad", Cantidad1);
                        cmdUpdate.Parameters.AddWithValue("@IdMaterial", IdMaterial1);
                        cmdUpdate.ExecuteNonQuery();
                    }

                    transaccion.Commit();
                    return true;
                }
                catch (SqlException ex)
                {
                    transaccion.Rollback();
                    MessageBox.Show("Ocurrió un error al guardar el detalle de compra.\n\n"+ ex.Message, "Error " + ex.Number,MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
        }
    }
}

