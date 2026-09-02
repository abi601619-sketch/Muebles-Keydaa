using Modelo.Conexión_DB;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Modelo.Entidades
{
    public class Material
    {
        private int idMaterial;
        private string NombreDelMaterial;
        private string UnidadDeMedida;
        private int Stock;
        private string Categoria;

        public int idMaterial1 { get => idMaterial; set => idMaterial = value; }
        public string NombreDelMaterial1 { get => NombreDelMaterial; set => NombreDelMaterial = value; }
        public string UnidadDeMedida1 { get => UnidadDeMedida; set => UnidadDeMedida = value; }
        public int Stock1 { get => Stock; set => Stock = value; }
        public string Categoria1 { get => Categoria; set => Categoria = value; }

        public Material(int idMaterial, string nombreDelMaterial, string unidadDeMedida, int stock, string categoria)
        {
            idMaterial1 = idMaterial;
            NombreDelMaterial1 = nombreDelMaterial;
            UnidadDeMedida1 = unidadDeMedida;
            Stock1 = stock;
            this.Categoria1 = categoria;
        }

        public Material() { }


        public static DataTable CargarMateriales()
        {
            SqlConnection conectar = Conexion.Conectar();
            string comando = "SELECT * FROM VerMaterial;";
            SqlDataAdapter adapter = new SqlDataAdapter(comando, conectar);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            return dt;
        }

        public bool InsertarMateriales()
        {
            string comandoSQL = "INSERT INTO Material(NombreDelMaterial, IdUnidadDeMedida, Stock, Categoria) VALUES (@NombreDelMaterial, @IdUnidad, @Stock, (SELECT TOP 1 IdCategoria FROM Categoria WHERE Nombre_Categoria = @Categoria));";
            //El bloque using asegura que la conexion y el comando
            // se cierren y se destruyan 
            // incluso si ocurre un error
            using (SqlConnection conexion = Conexion.Conectar())
            {
                using (SqlCommand comandoObjeto = new SqlCommand(comandoSQL, conexion))
                {
                    // A gregan los parametros
                    comandoObjeto.Parameters.AddWithValue("@NombreDelMaterial", NombreDelMaterial1);
                    comandoObjeto.Parameters.AddWithValue("@IdUnidad", UnidadDeMedida);
                    comandoObjeto.Parameters.AddWithValue("@Stock", Stock1);
                    comandoObjeto.Parameters.AddWithValue("@Categoria", Categoria);
                    try
                    {
                        // Se ejecuta una sola vez
                        // y se guarda la cant de filas afectadas
                        int filaAfectada = comandoObjeto.ExecuteNonQuery();
                        // si se afecto mas de 0 filas retorna true y sino false 
                        return filaAfectada > 0;
                    }
                    catch (SqlException ex)
                    {
                        switch (ex.Number)
                        {
                            case 2627:
                            case 2601:
                                MessageBox.Show("El modelo ya existe en la base de datos. Por favro use otro modelo.",
                                    "Registro Duplicado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                break;
                            default:
                                MessageBox.Show("Ocurrio un error inesperado en la base de datos" + ex.Message,
                                  "Error" + ex.Number, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                break;

                        }
                        return false;
                    }
                }
            }

        }



        public bool ActualizarMaterial()
        {
            string comandoSQL = "UPDATE Material SET NombreDelMaterial = @NombreDelMaterial, IdUnidadDeMedida = @IdUnidad, Stock = @Stock, Categoria = (SELECT TOP 1 IdCategoria FROM Categoria WHERE Nombre_Categoria = @Categoria) WHERE IdMaterial = @IdMaterial;";

            using (SqlConnection conexion = Conexion.Conectar())
            {
                using (SqlCommand comandoObjeto = new SqlCommand(comandoSQL, conexion))
                {
                    comandoObjeto.Parameters.AddWithValue("@IdMaterial", idMaterial1);
                    comandoObjeto.Parameters.AddWithValue("@NombreDelMaterial", NombreDelMaterial1);
                    comandoObjeto.Parameters.AddWithValue("@IdUnidad", UnidadDeMedida1);
                    comandoObjeto.Parameters.AddWithValue("@Stock", Stock1);
                    comandoObjeto.Parameters.AddWithValue("@Categoria", Categoria);

                    try
                    {
                        int filaAfectada = comandoObjeto.ExecuteNonQuery();

                        return filaAfectada > 0;
                    }
                    catch (SqlException ex)
                    {
                        switch (ex.Number)
                        {
                            case 2627:
                            case 2601:

                                MessageBox.Show("El material ya existe en la base de datos. Por favor verifique los datos.", "Registro Duplicado", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                                break;

                            default:

                                MessageBox.Show("Ocurrió? un error inesperado en la base de datos " + ex.Message, "Error " + ex.Number, MessageBoxButtons.OK, MessageBoxIcon.Warning);

                                break;
                        }

                        return false;
                    }
                }
            }
        }

        public bool ActualizarStock(int cantidad)
        {
            string comandoSQL = @"UPDATE Material SET Stock = Stock + @Cantidad 
                                WHERE IdMaterial = @IdMaterial;";

            using (SqlConnection conexion = Conexion.Conectar())
            {
                using (SqlCommand comandoObjeto =
                    new SqlCommand(comandoSQL, conexion))
                {
                    comandoObjeto.Parameters.AddWithValue("@IdMaterial", idMaterial1);

                    comandoObjeto.Parameters.AddWithValue("@Cantidad", cantidad);

                    try
                    {
                        int filasAfectadas =
                            comandoObjeto.ExecuteNonQuery();

                        return filasAfectadas > 0;
                    }
                    catch (SqlException ex)
                    {
                        MessageBox.Show("Ocurri? un error al actualizar el stock.\n\n" + ex.Message, "Error " + ex.Number, MessageBoxButtons.OK, MessageBoxIcon.Error);

                        return false;
                    }
                }
            }
        }

        public static DataTable BuscarMaterial(string texto)
        {
            SqlConnection conectar = Conexion.Conectar();

            string comando = @"SELECT * FROM VerMaterial WHERE CAST(IdMaterial AS VARCHAR) LIKE @Texto OR Material LIKE @Texto OR Categoria LIKE @Texto ORDER BY IdMaterial;";

            SqlDataAdapter adapter = new SqlDataAdapter(comando, conectar);

            adapter.SelectCommand.Parameters.AddWithValue("@Texto", "%" + texto + "%");

            DataTable dt = new DataTable();

            adapter.Fill(dt);

            return dt;
        }


    }
}

