using Modelo.Conexión_DB;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Modelo.Entidades
{
    public class DbProducción
    {
        private int IdProduccion;
        private int Pedido;
        private string Cliente;
        private string Mueble;
        private DateTime FechaEntrega;
        private string Estado;
        private int Progreso;


        public DbProducción()
        {
        }

        public DbProducción(int idProduccion, int pedido, string cliente, string mueble, DateTime fechaEntrega, string estado, int progreso)
        {
            IdProduccion1 = idProduccion;
            Pedido1 = pedido;
            Cliente1 = cliente;
            Mueble1 = mueble;
            FechaEntrega1 = fechaEntrega;
            Estado1 = estado;
            Progreso1 = progreso;
        }

        public int IdProduccion1 { get => IdProduccion; set => IdProduccion = value; }
        public int Pedido1 { get => Pedido; set => Pedido = value; }
        public string Cliente1 { get => Cliente; set => Cliente = value; }
        public string Mueble1 { get => Mueble; set => Mueble = value; }
        public DateTime FechaEntrega1 { get => FechaEntrega; set => FechaEntrega = value; }
        public string Estado1 { get => Estado; set => Estado = value; }
        public int Progreso1 { get => Progreso; set => Progreso = value; }

        public static DataTable CargarProducción()
        {
            using (SqlConnection conectar = Conexion.Conectar())
            {
                string comando = "SELECT * FROM VerProduccion;";

                SqlDataAdapter adapter =
                    new SqlDataAdapter(comando, conectar);

                DataTable dt = new DataTable();

                adapter.Fill(dt);

                return dt;
            }
        }

        public bool ObtenerProduccion()
        {
            string comandoSQL = @"SELECT IdProduccion, IdPedido, Cliente,Producto, [Fecha de Entrega], Progreso, Estado FROM VerProduccion WHERE IdProduccion = @IdProduccion;";

            using (SqlConnection conexion = Conexion.Conectar())
            {
                using (SqlCommand comandoObjeto =
                    new SqlCommand(comandoSQL, conexion))
                {
                    comandoObjeto.Parameters.AddWithValue(
                        "@IdProduccion",
                        IdProduccion1);

                    try
                    {
                        using (SqlDataReader reader =
                            comandoObjeto.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                IdProduccion1 = Convert.ToInt32(reader["IdProduccion"]);

                                Pedido1 = Convert.ToInt32(reader["IdPedido"]);

                                Cliente1 = reader["Cliente"].ToString();

                                Mueble1 = reader["Producto"].ToString();

                                FechaEntrega1 = Convert.ToDateTime(reader["Fecha de Entrega"]);

                                Progreso1 = Convert.ToInt32(reader["Progreso"]);

                                Estado1 = reader["Estado"].ToString();

                                return true;
                            }
                        }
                    }
                    catch (SqlException ex)
                    {
                        MessageBox.Show(
                            "Error al consultar la producción:\n\n"
                            + ex.Message,
                            "Error " + ex.Number,
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);

                        return false;
                    }
                }
            }

            return false;
        }


        public bool ActualizarProduccion()
        {
            string comandoSQL = @"
        UPDATE Produccion
        SET Progreso = @Progreso
        WHERE IdProduccion = @IdProduccion;

        IF @Progreso = 100
        BEGIN
            UPDATE Pedido
            SET Estado = 'Finalizado'
            WHERE IdPedido = (
                SELECT IdPedido
                FROM Produccion
                WHERE IdProduccion = @IdProduccion
            );
        END";

            using (SqlConnection conexion = Conexion.Conectar())
            {
                SqlTransaction transaccion = null;

                try
                {
                    transaccion = conexion.BeginTransaction();

                    using (SqlCommand comando = new SqlCommand(
                        comandoSQL,
                        conexion,
                        transaccion))
                    {
                        comando.Parameters.Add("@IdProduccion", SqlDbType.Int).Value =
                            IdProduccion;

                        comando.Parameters.Add("@Progreso", SqlDbType.Int).Value =
                            Progreso;

                        int filasAfectadas = comando.ExecuteNonQuery();

                        transaccion.Commit();

                        if (Progreso == 100)
                        {
                            MessageBox.Show(
                                "La producción ha finalizado correctamente.\n\n" +
                                "El pedido ha sido marcado automáticamente como FINALIZADO.",
                                "Producción finalizada",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information
                            );
                        }
                        else
                        {
                            MessageBox.Show(
                                "La producción se actualizó correctamente.\n\n" +
                                "ID Producción: " + IdProduccion +
                                "\nProgreso: " + Progreso + "%",
                                "Producción actualizada",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information
                            );
                        }

                        return filasAfectadas > 0;
                    }
                }
                catch (SqlException ex)
                {
                    if (transaccion != null)
                    {
                        try
                        {
                            transaccion.Rollback();
                        }
                        catch
                        {
                        }
                    }

                    MessageBox.Show(
                        "ERROR SQL:\n\n" + ex.Message,
                        "Error " + ex.Number,
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error
                    );

                    return false;
                }
                catch (Exception ex)
                {
                    if (transaccion != null)
                    {
                        try
                        {
                            transaccion.Rollback();
                        }
                        catch
                        {
                        }
                    }

                    MessageBox.Show(
                        "ERROR:\n\n" + ex.Message,
                        "Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error
                    );

                    return false;
                }
            }
        }



        // CALCULAR ESTADÍSTICAS

        // Totales
        public static int ContarProduccionesTotales()
        {
            try
            {
                using (SqlConnection conexion = Conexion.Conectar())
                {
                    string query = "SELECT COUNT(*) FROM Produccion";

                    using (SqlCommand comando = new SqlCommand(query, conexion))
                    {
                        return Convert.ToInt32(comando.ExecuteScalar());
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Error al contar las producciones totales: " + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );

                return 0;
            }
        }

        // Pendientes
        public static int ContarProduccionesPendientes()
        {
            try
            {
                using (SqlConnection conexion = Conexion.Conectar())
                {
                    string query = @"SELECT COUNT(*) 
                             FROM Produccion 
                             WHERE Progreso = 0";

                    using (SqlCommand comando = new SqlCommand(query, conexion))
                    {
                        return Convert.ToInt32(comando.ExecuteScalar());
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Error al contar las producciones pendientes: " + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );

                return 0;
            }
        }

        // En producción
        public static int ContarProduccionesEnProceso()
        {
            try
            {
                using (SqlConnection conexion = Conexion.Conectar())
                {
                    string query = @"SELECT COUNT(*) 
                             FROM Produccion 
                             WHERE Progreso BETWEEN 1 AND 99";

                    using (SqlCommand comando = new SqlCommand(query, conexion))
                    {
                        return Convert.ToInt32(comando.ExecuteScalar());
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Error al contar las producciones en proceso: " + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );

                return 0;
            }
        }

        // Finalizados
        public static int ContarProduccionesFinalizadas()
        {
            try
            {
                using (SqlConnection conexion = Conexion.Conectar())
                {
                    string query = @"SELECT COUNT(*) 
                             FROM Produccion 
                             WHERE Progreso = 100";

                    using (SqlCommand comando = new SqlCommand(query, conexion))
                    {
                        return Convert.ToInt32(comando.ExecuteScalar());
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Error al contar las producciones finalizadas: " + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );

                return 0;
            }
        }


    }
}

