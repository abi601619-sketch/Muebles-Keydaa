using Modelo.Conexión_DB;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Modelo.Entidades
{
    public class DbCotizacion
    {
        private int IdCotizacion;
        private DateTime Fecha;
        private int IdCliente;
        private string CondicionPago;
        private string CondicionEntrega;
        private decimal Total;
        private string Estado;

        public DbCotizacion(int idCotizacion, DateTime fecha, int idCliente, string condicionesPago, string condicionesEntrega, decimal total, string estado)
        {
            IdCotizacion = idCotizacion;
            Fecha = fecha;
            IdCliente = idCliente;
            CondicionPago = condicionesPago;
            CondicionEntrega = condicionesEntrega;
            Total = total;
            Estado = estado;
        }
        public DbCotizacion()
        {

        }

        public int IdCotizacion1 { get => IdCotizacion; set => IdCotizacion = value; }
        public DateTime Fecha1 { get => Fecha; set => Fecha = value; }
        public int IdCliente1 { get => IdCliente; set => IdCliente = value; }
        public string CondicionesPago1 { get => CondicionPago; set => CondicionPago = value; }
        public string CondicionesEntrega1 { get => CondicionEntrega; set => CondicionEntrega = value; }
        public decimal Total1 { get => Total; set => Total = value; }
        public string Estado1 { get => Estado; set => Estado = value; }

        public static DataTable CargarCotizacion()
        {
            SqlConnection conectar = Conexion.Conectar();

            string comando = "SELECT * FROM VerCotizaciones;";
            SqlDataAdapter adapter = new SqlDataAdapter(comando, conectar);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            return dt;
        }

        public int InsertarCotizacion()
        {
            string comandoSQL = @"INSERT INTO Cotizacion(Fecha,IdCliente,CondicionPago,CondicionEntrega,Total,Estado,IdUsuario) VALUES(@Fecha,@IdCliente,@CondicionPago,@CondicionEntrega,@Total,@Estado, 1); SELECT CAST(SCOPE_IDENTITY() AS INT);";

            using (SqlConnection conexion = Conexion.Conectar())
            {
                using (SqlCommand comandoObjeto =
                    new SqlCommand(comandoSQL, conexion))
                {
                    comandoObjeto.Parameters.AddWithValue("@Fecha", Fecha);

                    comandoObjeto.Parameters.AddWithValue("@IdCliente", IdCliente);

                    comandoObjeto.Parameters.AddWithValue("@CondicionPago", CondicionPago);

                    comandoObjeto.Parameters.AddWithValue("@CondicionEntrega", CondicionEntrega);

                    SqlParameter parametroTotal = comandoObjeto.Parameters.Add("@Total", SqlDbType.Decimal);
                    parametroTotal.Precision = 10;
                    parametroTotal.Scale = 2;
                    parametroTotal.Value = Total;

                    comandoObjeto.Parameters.AddWithValue("@Estado", Estado);

                    try
                    {
                        int idCotizacion = Convert.ToInt32(comandoObjeto.ExecuteScalar());

                        return idCotizacion;
                    }
                    catch (SqlException ex)
                    {
                        MessageBox.Show("Ocurri? un error al guardar la cotizaci?n.\n\n" + ex.Message, "Error " + ex.Number, MessageBoxButtons.OK, MessageBoxIcon.Error);

                        return 0;
                    }
                }
            }
        }


        public bool EliminarCotizacion()
        {
            using (SqlConnection conexion = Conexion.Conectar())
            {
                SqlTransaction transaccion = conexion.BeginTransaction();
                try
                {
                    SqlCommand cmdDetalle = new SqlCommand("DELETE FROM Productos_Cotizacion WHERE IdCotizacion = @IdCotizacion", conexion, transaccion);
                    cmdDetalle.Parameters.AddWithValue("@IdCotizacion", IdCotizacion1);
                    cmdDetalle.ExecuteNonQuery();

                    SqlCommand cmdCot = new SqlCommand("DELETE FROM Cotizacion WHERE IdCotizacion = @IdCotizacion", conexion, transaccion);
                    cmdCot.Parameters.AddWithValue("@IdCotizacion", IdCotizacion1);
                    int filasAfectadas = cmdCot.ExecuteNonQuery();

                    transaccion.Commit();
                    return filasAfectadas > 0;
                }
                catch (Exception)
                {
                    transaccion.Rollback();
                    return false;
                }
            }
        }

        public bool ActualizarEstado(string nuevoEstado)
        {
            string comandoSQL = "UPDATE Cotizacion SET Estado = @Estado WHERE IdCotizacion = @IdCotizacion;";
            using (SqlConnection conexion = Conexion.Conectar())
            {
                using (SqlCommand comandoObjeto = new SqlCommand(comandoSQL, conexion))
                {
                    comandoObjeto.Parameters.AddWithValue("@Estado", nuevoEstado);
                    comandoObjeto.Parameters.AddWithValue("@IdCotizacion", IdCotizacion1);
                    try
                    {
                        return comandoObjeto.ExecuteNonQuery() > 0;
                    }
                    catch (Exception)
                    {
                        return false;
                    }
                }
            }
        }

        public static DataTable BuscarCotizacion(string buscar)
        {
            SqlConnection conectar = Conexion.Conectar();

            string comando = @"SELECT * FROM VerCotizaciones WHERE Cliente LIKE '%' + @Buscar + '%' ORDER BY IdCotizacion";

            SqlDataAdapter adapter = new SqlDataAdapter(comando, conectar);

            adapter.SelectCommand.Parameters.AddWithValue("@Buscar", buscar);

            DataTable dt = new DataTable();

            adapter.Fill(dt);

            return dt;
        }
    }
}
