using Modelo.Conexión_DB;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Modelo.Entidades
{
    public class MetodoPago
    {
        private int IdMetodoPago;
        private string MetodoDePago;

        public MetodoPago(int idMetodoPago, string metodoDePago)
        {
            IdMetodoPago1 = idMetodoPago;
            MetodoDePago1 = metodoDePago;
        }

        public MetodoPago()
        {

        }

        public int IdMetodoPago1 { get => IdMetodoPago; set => IdMetodoPago = value; }
        public string MetodoDePago1 { get => MetodoDePago; set => MetodoDePago = value; }


        public static DataTable CargarMetodosDePago()
        {
            try
            {
                using (SqlConnection conectar = Conexion.Conectar())
                {
                    string comando = "SELECT * FROM MetodoPago";

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
                            "La tabla MetodoPago no existe en la base de datos.",
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
                            "Ocurrió un error al cargar los métodos de pago.\n" + ex.Message,
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
