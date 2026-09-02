using Modelo.Conexión_DB;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Modelo.Entidades
{
    public class DbProveedor
    {
        private int IdProveedor;
        private string Nombre_Proveedor;
        private string Telefono;
        private string Correo;
        private string Ubicacion;

        private bool Estado;

        public DbProveedor(int idProveedor, string nombre_Proveedor, string telefono, string correo, string ubicacion)
        {
            IdProveedor = idProveedor;
            Nombre_Proveedor = nombre_Proveedor;
            Telefono = telefono;
            Correo = correo;
            Ubicacion = ubicacion;
        }
        public DbProveedor()
        {
        }

        public int IdProveedor1 { get => IdProveedor; set => IdProveedor = value; }
        public string Nombre_Proveedor1 { get => Nombre_Proveedor; set => Nombre_Proveedor = value; }
        public string Telefono1 { get => Telefono; set => Telefono = value; }
        public string Correo1 { get => Correo; set => Correo = value; }
        public string Ubicacion1 { get => Ubicacion; set => Ubicacion = value; }
        public bool Estado1 { get => Estado; set => Estado = value; }

        public static DataTable CargarProveedor()
        {
            SqlConnection conectar = Conexion.Conectar();

            string comando = "SELECT * FROM VerProveedores;";
            SqlDataAdapter adapter = new SqlDataAdapter(comando, conectar);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            return dt;
        }

        public bool InsertarProveedor()
        {
            string comandoSQL = "INSERT INTO Proveedor(Nombre_Proveedor,Telefono,Correo,Ubicacion,Estado)" +
                "VALUES (@Nombre_Proveedor,@Telefono,@Correo,@Ubicacion, @Estado);";

            using (SqlConnection conexion = Conexion.Conectar())
            {
                using (SqlCommand comandoObjeto = new SqlCommand(comandoSQL, conexion))
                {
                    comandoObjeto.Parameters.AddWithValue("@Nombre_Proveedor", Nombre_Proveedor);
                    comandoObjeto.Parameters.AddWithValue("@Telefono", Telefono);
                    comandoObjeto.Parameters.AddWithValue("@Correo", Correo);
                    comandoObjeto.Parameters.AddWithValue("@Ubicacion", Ubicacion);
                    comandoObjeto.Parameters.AddWithValue("@Estado", Estado);

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

                                MessageBox.Show(
                                    "El proveedor ya existe en la base de datos. Por favor verifique los datos.",
                                    "Registro Duplicado",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Warning);

                                break;

                            default:

                                MessageBox.Show(
                                    "Ocurrió un error inesperado en la base de datos " + ex.Message,
                                    "Error " + ex.Number,
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Warning);

                                break;
                        }

                        return false;
                    }
                }
            }
        }

        public bool ActualizarProveedor()
        {
            string comandoSQL = "UPDATE Proveedor SET Nombre_Proveedor = @Nombre, Telefono = @Telefono, Correo = @Correo, Ubicacion = @Ubicacion WHERE IdProveedor = @IdProveedor;";
            using (SqlConnection conexion = Conexion.Conectar())
            {
                using (SqlCommand cmd = new SqlCommand(comandoSQL, conexion))
                {
                    cmd.Parameters.AddWithValue("@IdProveedor", IdProveedor1);
                    cmd.Parameters.AddWithValue("@Nombre", Nombre_Proveedor1);
                    cmd.Parameters.AddWithValue("@Telefono", Telefono1);
                    cmd.Parameters.AddWithValue("@Correo", Correo1);
                    cmd.Parameters.AddWithValue("@Ubicacion", Ubicacion1);
                    try
                    {
                        return cmd.ExecuteNonQuery() > 0;
                    }
                    catch (Exception)
                    {
                        return false;
                    }
                }
            }
        }
        public bool DesactivarProveedor(int idProveedor)
        {
            using (SqlConnection conexion = Conexion.Conectar())
            {


                // Primero verificamos el estado actual
                string consultaEstado = @"SELECT Estado FROM Proveedor WHERE IdProveedor = @IdProveedor";

                using (SqlCommand cmdEstado = new SqlCommand(consultaEstado, conexion))
                {
                    cmdEstado.Parameters.AddWithValue("@IdProveedor", idProveedor);

                    object resultado = cmdEstado.ExecuteScalar();

                    if (resultado == null)
                        return false;

                    bool estadoActual = Convert.ToBoolean(resultado);

                    // Si ya está en 0, entonces si realmente el estado está inactivo
                    if (!estadoActual)
                        return false;
                }

                // Si estaba activo, lo desactivamos
                string consultaDesactivar = @"UPDATE Proveedor SET Estado = 0 WHERE IdProveedor = @IdProveedor";

                using (SqlCommand cmd = new SqlCommand(consultaDesactivar, conexion))
                {
                    cmd.Parameters.AddWithValue("@IdProveedor", idProveedor);

                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }

        public static DataTable BuscarProveedor(string termino)
        {
            SqlConnection con = Conexion.Conectar();

            string comando = @"SELECT *  FROM Proveedor WHERE CAST(IdProveedor AS VARCHAR) LIKE @buscar OR Nombre_Proveedor LIKE @buscar;";

            SqlDataAdapter ad = new SqlDataAdapter(comando, con);

            ad.SelectCommand.Parameters.AddWithValue("@buscar", "%" + termino + "%");

            DataTable dt = new DataTable();

            ad.Fill(dt);

            return dt;
        }
    }
}
