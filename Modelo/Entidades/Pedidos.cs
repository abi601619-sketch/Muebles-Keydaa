using Modelo.Conexión_DB;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;


namespace Modelo.Entidades
{
    public class DbPedidos
    {
        private int idPedido;
        private DateTime FechaDePedido;
        private DateTime FechaDeEntrega;
        private string Estado;
        private int idCotizacion;

        public DbPedidos(int idPedido, DateTime fechaDePedido, DateTime fechaDeEntrega, string estado, int idCotizacion)
        {
            this.idPedido = idPedido;
            FechaDePedido = fechaDePedido;
            FechaDeEntrega = fechaDeEntrega;
            Estado = estado;
            this.idCotizacion = idCotizacion;
        }

        public int IdPedido { get => idPedido; set => idPedido = value; }
        public DateTime FechaDePedido1 { get => FechaDePedido; set => FechaDePedido = value; }
        public DateTime FechaDeEntrega1 { get => FechaDeEntrega; set => FechaDeEntrega = value; }
        public string Estado1 { get => Estado; set => Estado = value; }
        public int IdCotizacion { get => idCotizacion; set => idCotizacion = value; }

        public static DataTable CargarRegistroPedidos()
        {
            SqlConnection conectar = Conexion.Conectar();

            string comando = "SELECT * FROM VerPedido;";
            SqlDataAdapter adapter = new SqlDataAdapter(comando, conectar);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            return dt;
        }



        public static DataTable CargarPedidosRecientes()
        {
            SqlConnection conectar = Conexion.Conectar();

            string comando = "SELECT * FROM Pedido\r\nWHERE FechaDePedido >= '2026-07-15';";
            SqlDataAdapter adapter = new SqlDataAdapter(comando, conectar);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            return dt;
        }

        public static bool ConvertirCotizacionAPedido(int idCotizacion, DateTime fechaEntrega)
        {
            using (SqlConnection conexion = Conexion.Conectar())
            {
                SqlTransaction transaccion = conexion.BeginTransaction();

                try
                {
                    // 1. Crear el Pedido
                    string queryPedido = @"
                INSERT INTO Pedido 
                (
                    FechaDePedido, 
                    FechaDeEntrega, 
                    Estado, 
                    IdCotizacion
                ) 
                VALUES 
                (
                    @FechaPedido, 
                    @FechaEntrega, 
                    'En proceso', 
                    @IdCotizacion
                );

                SELECT SCOPE_IDENTITY();";

                    SqlCommand cmdPedido = new SqlCommand(
                        queryPedido,
                        conexion,
                        transaccion
                    );

                    cmdPedido.Parameters.AddWithValue(
                        "@FechaPedido",
                        DateTime.Now
                    );

                    cmdPedido.Parameters.AddWithValue(
                        "@FechaEntrega",
                        fechaEntrega
                    );

                    cmdPedido.Parameters.AddWithValue(
                        "@IdCotizacion",
                        idCotizacion
                    );

                    int nuevoIdPedido = Convert.ToInt32(
                        cmdPedido.ExecuteScalar()
                    );


                    // 2. Insertar los detalles del pedido
                    //    a partir de los productos de la cotización
                    string queryDetalle = @"
                INSERT INTO DetallePedido 
                (
                    IdPedido, 
                    Mueble, 
                    Cantidad, 
                    Medidas
                )
                SELECT 
                    @IdPedido, 
                    DescripcionMueble, 
                    Cantidad, 
                    CAST(Largo AS VARCHAR) + 'x' + 
                    CAST(Ancho AS VARCHAR) + 'x' + 
                    CAST(Alto AS VARCHAR)
                FROM Productos_Cotizacion 
                WHERE IdCotizacion = @IdCotizacion;";

                    SqlCommand cmdDetalle = new SqlCommand(
                        queryDetalle,
                        conexion,
                        transaccion
                    );

                    cmdDetalle.Parameters.AddWithValue(
                        "@IdPedido",
                        nuevoIdPedido
                    );

                    cmdDetalle.Parameters.AddWithValue(
                        "@IdCotizacion",
                        idCotizacion
                    );

                    cmdDetalle.ExecuteNonQuery();


                    // 3. Crear automáticamente la Producción
                    //    porque el pedido inicia "En proceso"
                    string queryProduccion = @"
                INSERT INTO Produccion 
                (
                    IdPedido, 
                    Progreso
                )
                VALUES 
                (
                    @IdPedido, 
                    0
                );";

                    SqlCommand cmdProduccion = new SqlCommand(
                        queryProduccion,
                        conexion,
                        transaccion
                    );

                    cmdProduccion.Parameters.AddWithValue(
                        "@IdPedido",
                        nuevoIdPedido
                    );

                    cmdProduccion.ExecuteNonQuery();


                    // 4. Confirmar toda la operación
                    transaccion.Commit();

                    return true;
                }
                catch (Exception ex)
                {
                    try
                    {
                        transaccion.Rollback();
                    }
                    catch
                    {
                    }

                    MessageBox.Show(
                        "Error al convertir cotización a pedido: " +
                        ex.Message,
                        "Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error
                    );

                    return false;
                }
            }
        }


        public static bool ActualizarPedidoEstado(int idPedido, string nuevoEstado)
        {
            using (System.Data.SqlClient.SqlConnection conexion = Conexion.Conectar())
            {
                string query = "UPDATE Pedido SET Estado = @Estado WHERE IdPedido = @IdPedido AND (@Estado = 'Cancelado' OR EXISTS (SELECT 1 FROM DetallePedido WHERE IdPedido = @IdPedido))";
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand(query, conexion);
                cmd.Parameters.AddWithValue("@Estado", nuevoEstado);

                cmd.Parameters.AddWithValue("@IdPedido", idPedido);
                try
                {
                    return cmd.ExecuteNonQuery() > 0;
                }
                catch
                {
                    return false;
                }
            }
        }

        public static bool ActualizarPedidoFecha(int idPedido, DateTime fechaEntrega)
        {
            using (System.Data.SqlClient.SqlConnection conexion = Conexion.Conectar())
            {
                string query = "UPDATE Pedido SET FechaDeEntrega = @FechaEntrega WHERE IdPedido = @IdPedido";
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand(query, conexion);

                cmd.Parameters.AddWithValue("@FechaEntrega", fechaEntrega);
                cmd.Parameters.AddWithValue("@IdPedido", idPedido);
                try
                {
                    return cmd.ExecuteNonQuery() > 0;
                }
                catch
                {
                    return false;
                }
            }
        }

        public static DataTable BuscarPedido(string termino)
        {
            SqlConnection con = Conexion.Conectar();

            string comando = @"SELECT * FROM VerPedido WHERE CAST(IdPedido AS VARCHAR) LIKE @buscar OR Cliente LIKE @buscar;";

            SqlDataAdapter ad = new SqlDataAdapter(comando, con);

            ad.SelectCommand.Parameters.AddWithValue("@buscar", "%" + termino + "%");

            DataTable dt = new DataTable();

            ad.Fill(dt);

            return dt;
        }
    }
}

