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
        WHERE IdProduccion = @IdProduccion;";

            using (SqlConnection conexion = Conexion.Conectar())
            {
                using (SqlCommand comando = new SqlCommand(comandoSQL, conexion))
                {
                    comando.Parameters.Add("@IdProduccion", SqlDbType.Int).Value =
                        IdProduccion;

                    comando.Parameters.Add("@Progreso", SqlDbType.Int).Value =
                        Progreso;

                    try
                    {
                        int filasAfectadas = comando.ExecuteNonQuery();

                        MessageBox.Show(
                            "ID: " + IdProduccion +
                            "\nProgreso enviado: " + Progreso +
                            "\nFilas afectadas: " + filasAfectadas,
                            "Prueba de actualización",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);

                        return filasAfectadas > 0;
                    }
                    catch (SqlException ex)
                    {
                        MessageBox.Show(
                            "ERROR SQL:\n\n" + ex.Message,
                            "Error " + ex.Number,
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);

                        return false;
                    }
                }
            }
        }




    }
}

