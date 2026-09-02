using Modelo.Conexión_DB;
using System.Data;
using System.Data.SqlClient;

namespace Modelo.Entidades
{
    public class DbUsuarios
    {
        private int IdUsuario;
        private string Nombre;
        private string Usuario;
        private string Contraseña;
        private string Rol;
        private bool Estado;

        public DbUsuarios(int idUsuario, string nombre, string usuario, string contraseña, string rol, bool estado)
        {
            IdUsuario = idUsuario;
            Nombre = nombre;
            Usuario = usuario;
            Contraseña = contraseña;
            Rol = rol;
            Estado = estado;
        }

        public DbUsuarios()
        {


        }

        public int IdUsuario1 { get => IdUsuario; set => IdUsuario = value; }
        public string Nombre1 { get => Nombre; set => Nombre = value; }
        public string Usuario1 { get => Usuario; set => Usuario = value; }
        public string Contraseña1 { get => Contraseña; set => Contraseña = value; }
        public string Rol1 { get => Rol; set => Rol = value; }
        public bool Estado1 { get => Estado; set => Estado = value; }

        public static DataTable CargarUsuarios()
        {
            SqlConnection conectar = Conexion.Conectar();
            string comando = "SELECT * FROM VerUsuarios;";
            SqlDataAdapter adapter = new SqlDataAdapter(comando, conectar);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            return dt;
        }
    }
}
