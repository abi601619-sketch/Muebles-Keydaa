using Modelo.Conexión_DB;
using System.Data;
using System.Data.SqlClient;

namespace Modelo.Entidades
{
    public class UnidadMedida
    {
        private int IdUnidadMedida;
        private string UnidadDeMedida;

        public UnidadMedida(int idUnidadMedida, string unidadDeMedida)
        {
            IdUnidadMedida1 = idUnidadMedida;
            UnidadDeMedida1 = unidadDeMedida;
        }
        public UnidadMedida()
        {

        }

        public int IdUnidadMedida1 { get => IdUnidadMedida; set => IdUnidadMedida = value; }
        public string UnidadDeMedida1 { get => UnidadDeMedida; set => UnidadDeMedida = value; }

        public static DataTable CargarUnidadesDeMedida()
        {
            SqlConnection conectar = Conexion.Conectar();

            string comando = "SELECT * FROM UnidadMedida;";
            SqlDataAdapter adapter = new SqlDataAdapter(comando, conectar);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            return dt;
        }
    }
}
