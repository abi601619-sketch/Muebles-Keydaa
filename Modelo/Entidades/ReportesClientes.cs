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

      


       
    }
}
