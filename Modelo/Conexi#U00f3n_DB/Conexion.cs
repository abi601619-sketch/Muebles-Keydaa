using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo.Conexión_DB
{
    public class Conexion
    {
        private static string servidor = "(localdb)\\MSSQLLocalDB";
        private static string baseDeDatos = "MueblesKeyda";

        public static SqlConnection Conectar()
        {
            string cadena = $"Data source={servidor};" +
                $"Initial Catalog={baseDeDatos};" +
                $"Integrated Security=true;";
            SqlConnection conectar = new SqlConnection(cadena);
            conectar.Open();
            return conectar;
        }
    }
}
