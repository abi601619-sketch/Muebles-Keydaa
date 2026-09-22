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

        public DbPedidos(
            int idPedido,
            DateTime fechaDePedido,
            DateTime fechaDeEntrega,
            string estado,
            int idCotizacion)
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
            try
            {
                using (SqlConnection conectar = Conexion.Conectar())
                {
                    string comando = "SELECT * FROM VerPedido;";

                    SqlDataAdapter adapter =
                        new SqlDataAdapter(comando, conectar);

                    DataTable dt = new DataTable();

                    adapter.Fill(dt);

                    return dt;
                }
            }
            catch (SqlException ex)
            {
                switch (ex.Number)
                {
                    case 208:
                        MessageBox.Show(
                            "La vista VerPedido no existe en la base de datos.",
                            "Error 208",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                        break;

                    case 53:
                        MessageBox.Show(
                            "No se pudo establecer conexión con el servidor.",
                            "Error 53",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                        break;

                    case 4060:
                        MessageBox.Show(
                            "No se pudo acceder a la base de datos.",
                            "Error 4060",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                        break;

                    case -2:
                        MessageBox.Show(
                            "La consulta tardó demasiado tiempo.",
                            "Error -2",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                        break;

                    default:
                        MessageBox.Show(
                            "Ocurrió un error al cargar los pedidos.\n" + ex.Message,
                            "Error " + ex.Number,
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                        break;
                }

                return new DataTable();
            }
        }


        public static DataTable CargarPedidosRecientes()
        {
            try
            {
                using (SqlConnection conectar = Conexion.Conectar())
                {
                    string comando = @"SELECT *
                                       FROM Pedido
                                       WHERE FechaDePedido >= '2026-07-15';";

                    SqlDataAdapter adapter =
                        new SqlDataAdapter(comando, conectar);

                    DataTable dt = new DataTable();

                    adapter.Fill(dt);

                    return dt;
                }
            }
            catch (SqlException ex)
            {
                switch (ex.Number)
                {
                    case 208:
                        MessageBox.Show(
                            "La tabla Pedido no existe en la base de datos.",
                            "Error 208",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                        break;

                    case 53:
                        MessageBox.Show(
                            "No se pudo establecer conexión con el servidor.",
                            "Error 53",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                        break;

                    case 4060:
                        MessageBox.Show(
                            "No se pudo acceder a la base de datos.",
                            "Error 4060",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                        break;

                    case -2:
                        MessageBox.Show(
                            "La consulta tardó demasiado tiempo.",
                            "Error -2",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                        break;

                    default:
                        MessageBox.Show(
                            "Ocurrió un error al cargar los pedidos recientes.\n" + ex.Message,
                            "Error " + ex.Number,
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                        break;
                }

                return new DataTable();
            }
        }


        public static bool ConvertirCotizacionAPedido(
            int idCotizacion,
            DateTime fechaEntrega)
        {
            using (SqlConnection conexion = Conexion.Conectar())
            {
                SqlTransaction transaccion =
                    conexion.BeginTransaction();

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

                    using (SqlCommand cmdPedido =
                        new SqlCommand(
                            queryPedido,
                            conexion,
                            transaccion))
                    {
                        cmdPedido.Parameters.AddWithValue(
                            "@FechaPedido",
                            DateTime.Now);

                        cmdPedido.Parameters.AddWithValue(
                            "@FechaEntrega",
                            fechaEntrega);

                        cmdPedido.Parameters.AddWithValue(
                            "@IdCotizacion",
                            idCotizacion);

                        int nuevoIdPedido =
                            Convert.ToInt32(
                                cmdPedido.ExecuteScalar());


                        // 2. Insertar los detalles del pedido
                        // a partir de los productos de la cotización
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

                        using (SqlCommand cmdDetalle =
                            new SqlCommand(
                                queryDetalle,
                                conexion,
                                transaccion))
                        {
                            cmdDetalle.Parameters.AddWithValue(
                                "@IdPedido",
                                nuevoIdPedido);

                            cmdDetalle.Parameters.AddWithValue(
                                "@IdCotizacion",
                                idCotizacion);

                            cmdDetalle.ExecuteNonQuery();
                        }


                        // 3. Crear automáticamente la Producción
                        // porque el pedido inicia "En proceso"
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

                        using (SqlCommand cmdProduccion =
                            new SqlCommand(
                                queryProduccion,
                                conexion,
                                transaccion))
                        {
                            cmdProduccion.Parameters.AddWithValue(
                                "@IdPedido",
                                nuevoIdPedido);

                            cmdProduccion.ExecuteNonQuery();
                        }
                    }


                    // 4. Confirmar toda la operación
                    transaccion.Commit();

                    return true;
                }
                catch (SqlException ex)
                {
                    try
                    {
                        transaccion.Rollback();
                    }
                    catch
                    {
                    }

                    switch (ex.Number)
                    {
                        case 547:
                            MessageBox.Show(
                                "No se puede convertir la cotización.\nVerifique los datos relacionados.",
                                "Error 547",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
                            break;

                        case 2627:
                        case 2601:
                            MessageBox.Show(
                                "El pedido o alguno de sus datos ya existe.\nVerifique la cotización.",
                                "Registro Duplicado",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
                            break;

                        case 515:
                            MessageBox.Show(
                                "Faltan datos obligatorios para crear el pedido.",
                                "Error 515",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
                            break;

                        case 245:
                            MessageBox.Show(
                                "Uno de los datos de la cotización tiene un formato incorrecto.",
                                "Error 245",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
                            break;

                        case 8115:
                            MessageBox.Show(
                                "Uno de los valores ingresados es demasiado grande.",
                                "Error 8115",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
                            break;

                        case 53:
                            MessageBox.Show(
                                "No se pudo establecer conexión con el servidor.",
                                "Error 53",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
                            break;

                        case 4060:
                            MessageBox.Show(
                                "No se pudo acceder a la base de datos.",
                                "Error 4060",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
                            break;

                        case -2:
                            MessageBox.Show(
                                "La conversión tardó demasiado tiempo.",
                                "Error -2",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
                            break;

                        case 208:
                            MessageBox.Show(
                                "Una de las tablas utilizadas no existe en la base de datos.",
                                "Error 208",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
                            break;

                        default:
                            MessageBox.Show(
                                "Ocurrió un error al convertir la cotización.\n" + ex.Message,
                                "Error " + ex.Number,
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
                            break;
                    }

                    return false;
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
                        "Ocurrió un error inesperado.\n" + ex.Message,
                        "Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);

                    return false;
                }
            }
        }


        public static bool ActualizarPedidoEstado(int idPedido, string nuevoEstado)
        {
            using (SqlConnection conexion = Conexion.Conectar())
            {
                string query = @" UPDATE Pedido SET Estado = @Estado
                    WHERE IdPedido = @IdPedido AND(@Estado = 'Cancelado'OR EXISTS(SELECT 1 FROM DetallePedido WHERE IdPedido = @IdPedido));";

                using (SqlCommand cmd = new SqlCommand(query, conexion))
                {
                    cmd.Parameters.AddWithValue(
                        "@Estado",
                        nuevoEstado);

                    cmd.Parameters.AddWithValue(
                        "@IdPedido",
                        idPedido);

                    try
                    {
                        int filasAfectadas =
                            cmd.ExecuteNonQuery();

                        if (filasAfectadas > 0)
                        {
                            return true;
                        }

                        MessageBox.Show(
                            "No se pudo actualizar el estado del pedido.\nVerifique que el pedido exista y tenga detalles.",
                            "Pedido no actualizado",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning);

                        return false;
                    }
                    catch (SqlException ex)
                    {
                        switch (ex.Number)
                        {
                            case 547:
                                MessageBox.Show(
                                    "No se puede actualizar el estado del pedido.\nVerifique los datos relacionados.",
                                    "Error 547",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Warning);
                                break;

                            case 515:
                                MessageBox.Show(
                                    "El estado del pedido es obligatorio.",
                                    "Error 515",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Warning);
                                break;

                            case 53:
                                MessageBox.Show(
                                    "No se pudo establecer conexión con el servidor.",
                                    "Error 53",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Error);
                                break;

                            case 4060:
                                MessageBox.Show(
                                    "No se pudo acceder a la base de datos.",
                                    "Error 4060",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Error);
                                break;

                            case -2:
                                MessageBox.Show(
                                    "La actualización tardó demasiado tiempo.",
                                    "Error -2",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Error);
                                break;

                            case 208:
                                MessageBox.Show(
                                    "La tabla Pedido o DetallePedido no existe.",
                                    "Error 208",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Error);
                                break;

                            default:
                                MessageBox.Show(
                                    "Ocurrió un error al actualizar el estado.\n" + ex.Message,
                                    "Error " + ex.Number,
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Error);
                                break;
                        }

                        return false;
                    }
                }
            }
        }


        public static bool ActualizarPedidoFecha(int idPedido, DateTime fechaEntrega)
        {
            using (SqlConnection conexion = Conexion.Conectar())
            {
                string query = @" UPDATE Pedido
                    SET FechaDeEntrega = @FechaEntrega WHERE IdPedido = @IdPedido;";

                using (SqlCommand cmd = new SqlCommand(query, conexion))
                {
                    cmd.Parameters.AddWithValue(
                        "@FechaEntrega",
                        fechaEntrega);

                    cmd.Parameters.AddWithValue(
                        "@IdPedido",
                        idPedido);

                    try
                    {
                        int filasAfectadas =
                            cmd.ExecuteNonQuery();

                        if (filasAfectadas > 0)
                        {
                            return true;
                        }

                        MessageBox.Show(
                            "No se encontró el pedido que desea actualizar.",
                            "Pedido no encontrado",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning);

                        return false;
                    }
                    catch (SqlException ex)
                    {
                        switch (ex.Number)
                        {
                            case 547:
                                MessageBox.Show(
                                    "No se puede actualizar la fecha del pedido.\nVerifique los datos relacionados.",
                                    "Error 547",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Warning);
                                break;

                            case 515:
                                MessageBox.Show(
                                    "La fecha de entrega es obligatoria.",
                                    "Error 515",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Warning);
                                break;

                            case 53:
                                MessageBox.Show(
                                    "No se pudo establecer conexión con el servidor.",
                                    "Error 53",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Error);
                                break;

                            case 4060:
                                MessageBox.Show(
                                    "No se pudo acceder a la base de datos.",
                                    "Error 4060",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Error);
                                break;

                            case -2:
                                MessageBox.Show(
                                    "La actualización tardó demasiado tiempo.",
                                    "Error -2",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Error);
                                break;

                            case 208:
                                MessageBox.Show(
                                    "La tabla Pedido no existe en la base de datos.",
                                    "Error 208",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Error);
                                break;

                            default:
                                MessageBox.Show(
                                    "Ocurrió un error al actualizar la fecha.\n" + ex.Message,
                                    "Error " + ex.Number,
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Error);
                                break;
                        }

                        return false;
                    }
                }
            }
        }


        public static DataTable BuscarPedido(string termino)
        {
            try
            {
                using (SqlConnection con =
                    Conexion.Conectar())
                {
                    string comando = @"
                        SELECT *
                        FROM VerPedido
                        WHERE CAST(IdPedido AS VARCHAR) LIKE @buscar
                        OR Cliente LIKE @buscar;";

                    SqlDataAdapter ad =
                        new SqlDataAdapter(comando, con);

                    ad.SelectCommand.Parameters.AddWithValue(
                        "@buscar",
                        "%" + (termino ?? "") + "%");

                    DataTable dt = new DataTable();

                    ad.Fill(dt);

                    return dt;
                }
            }
            catch (SqlException ex)
            {
                switch (ex.Number)
                {
                    case 208:
                        MessageBox.Show(
                            "La vista VerPedido no existe en la base de datos.",
                            "Error 208",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                        break;

                    case 53:
                        MessageBox.Show(
                            "No se pudo establecer conexión con el servidor.",
                            "Error 53",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                        break;

                    case 4060:
                        MessageBox.Show(
                            "No se pudo acceder a la base de datos.",
                            "Error 4060",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                        break;

                    case -2:
                        MessageBox.Show(
                            "La búsqueda tardó demasiado tiempo.",
                            "Error -2",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                        break;

                    default:
                        MessageBox.Show(
                            "Ocurrió un error al buscar los pedidos.\n" + ex.Message,
                            "Error " + ex.Number,
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                        break;
                }

                return new DataTable();
            }
        }
    }
}
