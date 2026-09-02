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
    public class MetodoPago
    {
        private int IdMetodoPago;
        private string MetodoDePago;

        public MetodoPago(int idMetodoPago, string metodoDePago)
        {
            IdMetodoPago1=idMetodoPago;
            MetodoDePago1=metodoDePago;
        }

        public MetodoPago()
        {

        }

        public int IdMetodoPago1 { get => IdMetodoPago; set => IdMetodoPago=value; }
        public string MetodoDePago1 { get => MetodoDePago; set => MetodoDePago=value; }

        public static DataTable CargarMetodosDePago()
        {
            SqlConnection conectar = Conexion.Conectar();

            string comando = "SELECT * FROM MetodoPago";
            SqlDataAdapter adapter = new SqlDataAdapter(comando, conectar);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            return dt;
        }
    }
}
