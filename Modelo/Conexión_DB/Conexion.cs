using System.Data.SqlClient;

namespace Modelo.Conexión_DB
{
    public class Conexion
    {
        private static string servidor = "LOWKIPC\\LOWK";
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
