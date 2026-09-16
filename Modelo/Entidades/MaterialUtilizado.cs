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
            var materiales = new DataTable();
            materiales.Columns.Add("IdMaterial", typeof(int));
            materiales.Columns.Add("Cantidad_Utilizada", typeof(int));
            materiales.Rows.Add(idMaterial, cantidad);
            GuardarConsumo(idProduccion, materiales);
            return true;
        }

        // Todo el lote se guarda o se revierte; el stock se valida dentro de SQL.
        public static void GuardarConsumo(int idProduccion, DataTable materiales)
        {
            using (SqlConnection conexion = Conexion.Conectar())
            using (SqlTransaction transaccion = conexion.BeginTransaction())
            {
                try
                {
                    foreach (DataRow fila in materiales.Rows)
                    {
                        int cantidad = Convert.ToInt32(fila["Cantidad_Utilizada"]);
                        if (cantidad <= 0)
                            throw new InvalidOperationException("La cantidad utilizada debe ser mayor que cero.");
                        using (var cmd = new SqlCommand(@"
                            UPDATE Material SET Stock = Stock - @Cantidad
                            WHERE IdMaterial = @IdMaterial AND Stock >= @Cantidad;
                            IF @@ROWCOUNT = 0
                                THROW 50001, 'Stock insuficiente o material inexistente. No se guardó el consumo.', 1;
                            INSERT INTO MaterialUtilizado (IdMaterial, IdProduccion, Cantidad_Utilizada)
                            VALUES (@IdMaterial, @IdProduccion, @Cantidad);", conexion, transaccion))
                        {
                            cmd.Parameters.AddWithValue("@IdMaterial", fila["IdMaterial"]);
                            cmd.Parameters.AddWithValue("@IdProduccion", idProduccion);
                            cmd.Parameters.AddWithValue("@Cantidad", cantidad);
                            cmd.ExecuteNonQuery();
                        }
                    }
                    transaccion.Commit();
                }
                catch
                {
                    transaccion.Rollback();
                    throw;
                }
            }
        }
    }
}
