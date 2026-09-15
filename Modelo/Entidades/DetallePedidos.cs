using Modelo.Conexión_DB;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo.Entidades
{
    public class DetallePedidos
    {
        private int IdDetallePedido;
        private int Cantidad;
        private double Largo;
        private double Ancho;
        private double Alto;
        private string MuebleARealizar;
        private string Descripcion;

        public DetallePedidos(int idDetallePedido, int cantidad, double largo, double ancho, double alto, string muebleARealizar, string descripcion)
        {
            IdDetallePedido=idDetallePedido;
            Cantidad=cantidad;
            Largo=largo;
            Ancho=ancho;
            Alto=alto;
            MuebleARealizar=muebleARealizar;
            Descripcion=descripcion;
        }

        public int IdDetallePedido1 { get => IdDetallePedido; set => IdDetallePedido=value; }
        public int Cantidad1 { get => Cantidad; set => Cantidad=value; }
        public double Largo1 { get => Largo; set => Largo=value; }
        public double Ancho1 { get => Ancho; set => Ancho=value; }
        public double Alto1 { get => Alto; set => Alto=value; }
        public string MuebleARealizar1 { get => MuebleARealizar; set => MuebleARealizar=value; }
        public string Descripcion1 { get => Descripcion; set => Descripcion=value; }

                public static DataTable CargarDetallesPorPedido(int idPedido)
        {
            SqlConnection conectar = Conexion.Conectar();
            string comando = "SELECT * FROM DetallePedido WHERE IdPedido = @IdPedido;";
            SqlDataAdapter adapter = new SqlDataAdapter(comando, conectar);
            adapter.SelectCommand.Parameters.AddWithValue("@IdPedido", idPedido);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            return dt;
        }

        public static bool EliminarDetalle(int idPedido, int idDetalle, bool confirmarCancelacion)
        {
            using (SqlConnection conexion = Conexion.Conectar())
            using (SqlTransaction transaccion = conexion.BeginTransaction(IsolationLevel.Serializable))
            using (var cmd = new SqlCommand(@"
                IF NOT EXISTS (SELECT 1 FROM Pedido WITH (UPDLOCK, HOLDLOCK) WHERE IdPedido = @Pedido)
                    THROW 50001, 'El pedido ya no existe.', 1;
                DECLARE @Cantidad int = (SELECT COUNT(*) FROM DetallePedido WITH (UPDLOCK, HOLDLOCK) WHERE IdPedido = @Pedido);
                IF @Cantidad = 1 AND @Confirmar = 0
                    THROW 50002, 'Este es ahora el último producto. Vuelva a intentar para confirmar la cancelación.', 1;
                DELETE FROM DetallePedido WHERE IdDetallePedido = @Detalle AND IdPedido = @Pedido;
                IF @@ROWCOUNT = 0
                    THROW 50003, 'El producto ya no existe. Actualice el pedido.', 1;
                IF NOT EXISTS (SELECT 1 FROM DetallePedido WHERE IdPedido = @Pedido)
                BEGIN
                    UPDATE Pedido SET Estado = 'Cancelado' WHERE IdPedido = @Pedido;
                    SELECT CAST(1 AS bit);
                END
                ELSE SELECT CAST(0 AS bit);", conexion, transaccion))
            {
                cmd.Parameters.AddWithValue("@Pedido", idPedido);
                cmd.Parameters.AddWithValue("@Detalle", idDetalle);
                cmd.Parameters.AddWithValue("@Confirmar", confirmarCancelacion);
                try
                {
                    bool cancelado = Convert.ToBoolean(cmd.ExecuteScalar());
                    transaccion.Commit();
                    return cancelado;
                }
                catch
                {
                    transaccion.Rollback();
                    throw;
                }
            }
        }

        public static bool InsertarDetalle(int idPedido, string mueble, int cantidad, string medidas)
        {
            using (SqlConnection conexion = Conexion.Conectar())
            {
                string query = "INSERT INTO DetallePedido (IdPedido, Mueble, Cantidad, Medidas) VALUES (@IdPedido, @Mueble, @Cantidad, @Medidas)";
                SqlCommand cmd = new SqlCommand(query, conexion);
                cmd.Parameters.AddWithValue("@IdPedido", idPedido);
                cmd.Parameters.AddWithValue("@Mueble", mueble);
                cmd.Parameters.AddWithValue("@Cantidad", cantidad);
                cmd.Parameters.AddWithValue("@Medidas", medidas);
                try {
                    return cmd.ExecuteNonQuery() > 0;
                } catch {
                    return false;
                }
            }
        }
        public static DataTable CargarDetallesPedidos()
        {
            SqlConnection conectar = Conexion.Conectar();

            string comando = "SELECT * FROM DetallePedido;";
            SqlDataAdapter adapter = new SqlDataAdapter(comando, conectar);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            return dt;
        }
    }
}
