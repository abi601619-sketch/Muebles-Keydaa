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

        public int IdUnidadMedida1
        {
            get => IdUnidadMedida;
            set => IdUnidadMedida = value;
        }

        public string UnidadDeMedida1
        {
            get => UnidadDeMedida;
            set => UnidadDeMedida = value;
        }

        public static DataTable CargarUnidadesDeMedida()
        {
            try
            {
                SqlConnection conectar = Conexion.Conectar();

                string comando = "SELECT * FROM UnidadMedida;";

                SqlDataAdapter adapter = new SqlDataAdapter(comando, conectar);

                DataTable dt = new DataTable();

                adapter.Fill(dt);

                return dt;
            }
            catch (SqlException ex)
            {
                switch (ex.Number)
                {
                    case 208:
                        MessageBox.Show(
                            "Error 208: La tabla UnidadMedida no existe.",
                            "Error de base de datos",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error
                        );
                        break;

                    case 53:
                        MessageBox.Show(
                            "Error 53: No se pudo conectar con el servidor SQL.",
                            "Error de conexión",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error
                        );
                        break;

                    case 4060:
                        MessageBox.Show(
                            "Error 4060: No se pudo acceder a la base de datos.",
                            "Error de base de datos",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error
                        );
                        break;

                    case -2:
                        MessageBox.Show(
                            "Error -2: La operación tardó demasiado.",
                            "Tiempo de espera agotado",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning
                        );
                        break;

                    default:
                        MessageBox.Show(
                            "Error SQL " + ex.Number + ": " + ex.Message,
                            "Error de base de datos",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error
                        );
                        break;
                }

                return new DataTable();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Error inesperado: " + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );

                return new DataTable();
            }
        }
    }
}
