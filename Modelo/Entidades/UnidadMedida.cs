using Modelo.Conexión_DB;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

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
            try
            {
                using (SqlConnection conectar = Conexion.Conectar())
                {
                    string comando = "SELECT * FROM UnidadMedida;";

                    using (SqlDataAdapter adapter = new SqlDataAdapter(comando, conectar))
                    {
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        return dt;
                    }
                }
            }
            catch (SqlException ex)
            {
                switch (ex.Number)
                {
                    case 208:
                        MessageBox.Show("La tabla UnidadMedida no existe.", "Error 208", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;

                    case 53:
                        MessageBox.Show("No se pudo conectar con el servidor SQL.", "Error 53", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;

                    case 4060:
                        MessageBox.Show("No se pudo acceder a la base de datos.", "Error 4060", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;

                    case -2:
                        MessageBox.Show("La operación tardó demasiado.", "Error -2", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        break;

                    default:
                        MessageBox.Show("Ocurrió un error al cargar las unidades de medida.\n" + ex.Message, "Error " + ex.Number, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                }

                return new DataTable();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrió un error inesperado.\n" + ex.Message, "Error inesperado", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return new DataTable();
            }
        }
    }
}