using Modelo.Conexión_DB;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Modelo.Entidades
{
    public class MaterialUtilizado
    {
        private int IdMaterialUtilizado;
        private int CantidadUtilizada;

        public MaterialUtilizado(int idMaterialUtilizado, int cantidadUtilizada)
        {
            IdMaterialUtilizado = idMaterialUtilizado;
            CantidadUtilizada = cantidadUtilizada;
        }

        public int IdMaterialUtilizado1 { get => IdMaterialUtilizado; set => IdMaterialUtilizado = value; }
        public int CantidadUtilizada1 { get => CantidadUtilizada; set => CantidadUtilizada = value; }

        public static DataTable CargarMaterialUtilizado()
        {
            SqlConnection conectar = Conexion.Conectar();
            string comando = "SELECT * FROM VerMaterialesUtilizados;";
            SqlDataAdapter adapter = new SqlDataAdapter(comando, conectar);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            return dt;
        }
        public static bool InsertarMaterialUtilizado(int idMaterial, int idProduccion, int cantidad)
        {
            using (System.Data.SqlClient.SqlConnection conexion = Conexion.Conectar())
            {
                System.Data.SqlClient.SqlTransaction transaccion = conexion.BeginTransaction();
                try
                {
                    string insertSql = "INSERT INTO MaterialUtilizado (IdMaterial, IdProduccion, Cantidad_Utilizada) VALUES (@IdMaterial, @IdProduccion, @Cantidad);";
                    System.Data.SqlClient.SqlCommand cmdInsert = new System.Data.SqlClient.SqlCommand(insertSql, conexion, transaccion);
                    cmdInsert.Parameters.AddWithValue("@IdMaterial", idMaterial);
                    cmdInsert.Parameters.AddWithValue("@IdProduccion", idProduccion);
                    cmdInsert.Parameters.AddWithValue("@Cantidad", cantidad);
                    cmdInsert.ExecuteNonQuery();

                    string updateSql = "UPDATE Material SET Stock = Stock - @Cantidad WHERE IdMaterial = @IdMaterial;";
                    System.Data.SqlClient.SqlCommand cmdUpdate = new System.Data.SqlClient.SqlCommand(updateSql, conexion, transaccion);
                    cmdUpdate.Parameters.AddWithValue("@IdMaterial", idMaterial);
                    cmdUpdate.Parameters.AddWithValue("@Cantidad", cantidad);
                    cmdUpdate.ExecuteNonQuery();

                    transaccion.Commit();
                    return true;
                }
                catch (Exception)
                {
                    transaccion.Rollback();
                    return false;
                }
            }
        }
    }
}
