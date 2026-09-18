using Modelo.Conexión_DB;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Modelo.Entidades
{
    public class DbRecuperacion
    {
        private int IdRecuperacion;
        private int IdUsuario;
        private string Codigo;
        private DateTime FechaGeneracion;
        private DateTime FechaExpiracion;
        private bool Usado;

        public DbRecuperacion()
        {

        }
        public static DataTable BuscarUsuarioPorCorreo(string correo)
        {
            DataTable tabla = new DataTable();

            try
            {
                using (SqlConnection conectar = Conexion.Conectar())
                {
                    using (SqlCommand comando = new SqlCommand("sp_Usuario_BuscarPorCorreo", conectar))
                    {
                        comando.CommandType = CommandType.StoredProcedure;

                        comando.Parameters.AddWithValue("@Correo", correo);

                        SqlDataAdapter adapter = new SqlDataAdapter(comando);

                        adapter.Fill(tabla);
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return tabla;
        }

        public static bool GuardarCodigo(int idUsuario, string codigo)
        {
            try
            {
                using (SqlConnection conectar = Conexion.Conectar())
                {
                    using (SqlCommand comando = new SqlCommand("sp_Recuperacion_Crear", conectar))
                    {
                        comando.CommandType = CommandType.StoredProcedure;

                        comando.Parameters.AddWithValue("@IdUsuario", idUsuario);
                        comando.Parameters.AddWithValue("@Codigo", codigo);

                        comando.ExecuteNonQuery();
                    }
                }

                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static DataTable VerificarCodigo(int idUsuario, string codigo)
        {
            DataTable tabla = new DataTable();

            try
            {
                using (SqlConnection conectar = Conexion.Conectar())
                {
                    using (SqlCommand comando = new SqlCommand(
                        "sp_Recuperacion_Verificar",
                        conectar))
                    {
                        comando.CommandType = CommandType.StoredProcedure;

                        comando.Parameters.AddWithValue("@IdUsuario", idUsuario);
                        comando.Parameters.AddWithValue("@Codigo", codigo);

                        SqlDataAdapter adapter = new SqlDataAdapter(comando);

                        adapter.Fill(tabla);
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return tabla;
        }

        public static bool CambiarContraseña(int idUsuario, string nuevaContraseña)
        {
            try
            {
                // Encriptar la nueva contraseña con BCrypt
                string contraseñaHash = BCrypt.Net.BCrypt.HashPassword(nuevaContraseña);

                using (SqlConnection conectar = Conexion.Conectar())
                using (SqlCommand comando = new SqlCommand("sp_Usuario_CambiarContraseña", conectar))
                {
                    comando.CommandType = CommandType.StoredProcedure;

                    comando.Parameters.AddWithValue("@IdUsuario", idUsuario);
                    comando.Parameters.AddWithValue("@NuevaContraseña", contraseñaHash);

                    comando.ExecuteNonQuery();
                }

                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static bool MarcarCodigoUsado(int idRecuperacion)
        {
            try
            {
                using (SqlConnection conectar = Conexion.Conectar())
                {
                    using (SqlCommand comando = new SqlCommand("sp_Recuperacion_MarcarUsado", conectar))
                    {
                        comando.CommandType = CommandType.StoredProcedure;

                        comando.Parameters.AddWithValue("@IdRecuperacion", idRecuperacion);

                        comando.ExecuteNonQuery();
                    }
                }

                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
