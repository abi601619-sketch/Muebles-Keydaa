using Modelo.Conexión_DB;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Modelo.Entidades
{
    public class ReportesClientes
    {
        private string Nombre_del_Cliente;
        private string TipoCliente;
        private string Encargado;
        private int Documento;
        private string telefono;
        private string correo;
        private string direccion;


        public static DataTable CargarReporteClientes()
        {
            SqlConnection conectar = Conexion.Conectar();

            string comando = "SELECT *FROM VerReporteClientes;";
            SqlDataAdapter adapter = new SqlDataAdapter(comando, conectar);

            DataTable dt = new DataTable();
            adapter.Fill(dt);

            return dt;
        }

        public static int ContarClientesCorporativos()
        {
            int total = 0;

            using (SqlConnection conexion = Conexion.Conectar())
            {
                string comandoSQL = "SELECT COUNT(*) FROM Cliente WHERE IdTipoCliente = 1;";

                using (SqlCommand comandoObjeto = new SqlCommand(comandoSQL, conexion))
                {
                    total = Convert.ToInt32(comandoObjeto.ExecuteScalar());
                }
            }

            return total;
        }

        public static int ContarClientesIndividuales()
        {
            int total = 0;

            using (SqlConnection conexion = Conexion.Conectar())
            {
                string comandoSQL = "SELECT COUNT(*) FROM Cliente WHERE IdTipoCliente = 2;";

                using (SqlCommand comandoObjeto = new SqlCommand(comandoSQL, conexion))
                {
                    total = Convert.ToInt32(comandoObjeto.ExecuteScalar());
                }
            }

            return total;
        }

        public static int ContarClientesTotales()
        {
            int total = 0;
            using (SqlConnection conexion = Conexion.Conectar())
            {
                string comandoSQL = "SELECT COUNT(*) FROM Cliente;";
                using (SqlCommand comandoObjeto = new SqlCommand(comandoSQL, conexion))
                {
                    total = Convert.ToInt32(comandoObjeto.ExecuteScalar());
                }
            }
            return total;
        }


    }
}
