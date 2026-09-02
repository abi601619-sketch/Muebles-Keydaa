using Modelo.Conexión_DB;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;


namespace Modelo.Entidades
{
    public class DbCliente
    {
        private int IdCliente;
        private int TipoCliente;
        private string Identificador1;
        private string Identificador2;
        private string Documento;
        private string Telefono;
        private string Correo;
        private string Direccion;
        private string Estado;


        public DbCliente()
        {

        }

        public DbCliente(int idCliente, int tipoCliente, string identificador1, string identificador2, string documento, string telefono, string correo, string direccion, string estado)
        {
            IdCliente = idCliente;
            TipoCliente = tipoCliente;
            Identificador1 = identificador1;
            Identificador2 = identificador2;
            Documento = documento;
            Telefono = telefono;
            Correo = correo;
            Direccion = direccion;
            Estado1 = estado;
        }

        public int IdCliente1 { get => IdCliente; set => IdCliente = value; }
        public int TipoCliente1 { get => TipoCliente; set => TipoCliente = value; }
        public string Identificador11 { get => Identificador1; set => Identificador1 = value; }
        public string Identificador21 { get => Identificador2; set => Identificador2 = value; }
        public string Documento1 { get => Documento; set => Documento = value; }
        public string Telefono1 { get => Telefono; set => Telefono = value; }
        public string Correo1 { get => Correo; set => Correo = value; }
        public string Direccion1 { get => Direccion; set => Direccion = value; }
        public string Estado1 { get => Estado; set => Estado = value; }

        public static DataTable CargarCorporativos()
        {
            SqlConnection conectar = Conexion.Conectar();
            string comando = "SELECT IdCliente,Identificador1 AS Nombre_De_Empresa,Identificador2 AS Nombre_Del_Encargado,Documento AS NIT,Telefono,Correo,Direccion,Estado FROM Cliente \r\nWHERE IdTipoCliente =1; ";
            SqlDataAdapter adapter = new SqlDataAdapter(comando, conectar);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            return dt;
        }
        public static DataTable CargarIndividuales()
        {
            SqlConnection conectar = Conexion.Conectar();
            string comando = "SELECT IdCliente,Identificador1 AS Nombre,Identificador2 AS Apellidos,Documento AS DUI,Telefono,Correo,Direccion,Estado  FROM Cliente \r\nWHERE IdTipoCliente =2;";
            SqlDataAdapter adapter = new SqlDataAdapter(comando, conectar);
            DataTable DT = new DataTable();
            adapter.Fill(DT);
            return DT;
        }
        public static DataTable CargarTodosLosClientes()
        {
            SqlConnection conectar = Conexion.Conectar();
            string comando = "SELECT IdCliente, CASE WHEN IdTipoCliente = 1 THEN Identificador1 + ' - ' + Identificador2 WHEN IdTipoCliente = 2 THEN Identificador1 + ' ' + Identificador2 END AS NombreCliente FROM Cliente WHERE IdTipoCliente IN (1, 2)";
            SqlDataAdapter adapter = new SqlDataAdapter(comando, conectar);
            DataTable DT = new DataTable();
            adapter.Fill(DT);
            return DT;
        }
        public bool InsertarClienteIndividual()
        {
            string comandoSQL = "INSERT INTO Cliente(IdTipoCliente,Identificador1,Identificador2,Documento,Telefono,Correo,Direccion,Estado)" +
                "VALUES (@IdTipoCliente,@Identificador1,@Identificador2,@Documento,@Telefono,@Correo,@Direccion,@Estado);";

            using (SqlConnection conexion = Conexion.Conectar())
            {
                using (SqlCommand comandoObjeto = new SqlCommand(comandoSQL, conexion))
                {
                    // Agregan los parámetros
                    comandoObjeto.Parameters.AddWithValue("@IdTipoCliente", TipoCliente);
                    comandoObjeto.Parameters.AddWithValue("@Identificador1", Identificador1);
                    comandoObjeto.Parameters.AddWithValue("@Identificador2", Identificador2);
                    comandoObjeto.Parameters.AddWithValue("@Documento", Documento);
                    comandoObjeto.Parameters.AddWithValue("@Telefono", Telefono);
                    comandoObjeto.Parameters.AddWithValue("@Correo", Correo);
                    comandoObjeto.Parameters.AddWithValue("@Direccion", Direccion);
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

                                MessageBox.Show("El identificador del cliente ya existe en la base de datos. Por favor verifique los datos.", "Registro Duplicado", MessageBoxButtons.OK, MessageBoxIcon.Warning);

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

        public bool InsertarClienteCorporativo()
        {
            string comandoSQL = "INSERT INTO Cliente(IdTipoCliente,Identificador1,Identificador2,Documento,Telefono,Correo,Direccion,Estado)" +
                "VALUES (@IdTipoCliente,@Identificador1,@Identificador2,@Documento,@Telefono,@Correo,@Direccion,@Estado);";

            using (SqlConnection conexion = Conexion.Conectar())
            {
                using (SqlCommand comandoObjeto = new SqlCommand(comandoSQL, conexion))
                {
                    // Agregan los parámetros
                    comandoObjeto.Parameters.AddWithValue("@IdTipoCliente", TipoCliente);
                    comandoObjeto.Parameters.AddWithValue("@Identificador1", Identificador1);
                    comandoObjeto.Parameters.AddWithValue("@Identificador2", Identificador2);
                    comandoObjeto.Parameters.AddWithValue("@Documento", Documento);
                    comandoObjeto.Parameters.AddWithValue("@Telefono", Telefono);
                    comandoObjeto.Parameters.AddWithValue("@Correo", Correo);
                    comandoObjeto.Parameters.AddWithValue("@Direccion", Direccion);
                    comandoObjeto.Parameters.AddWithValue("@Estado", Estado);

                    try
                    {
                        // Se ejecuta una sola vez
                        // y se guarda la cantidad de filas afectadas
                        int filaAfectada = comandoObjeto.ExecuteNonQuery();

                        // Si se afectó más de 0 filas retorna true
                        // y sino false
                        return filaAfectada > 0;
                    }
                    catch (SqlException ex)
                    {
                        switch (ex.Number)
                        {
                            case 2627:
                            case 2601:

                                MessageBox.Show("El identificador del cliente ya existe en la base de datos. Por favor verifique los datos.", "Registro Duplicado", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                                break;

                            default:

                                MessageBox.Show("Ocurrió un error inesperado en la base de datos " + ex.Message, "Error " + ex.Number, MessageBoxButtons.OK, MessageBoxIcon.Warning);

                                break;
                        }

                        return false;
                    }
                }
            }
        }

        public static DataTable CargarClientesParaSeleccionar()
        {
            SqlConnection conectar = Conexion.Conectar();

            string comando = "SELECT IdCliente, CASE WHEN IdTipoCliente = 2 THEN Identificador1 + ' ' + Identificador2 ELSE Identificador1 END AS Cliente, Telefono, Correo, Direccion, Estado FROM Cliente;";

            SqlDataAdapter adapter = new SqlDataAdapter(comando, conectar);

            DataTable dt = new DataTable();

            adapter.Fill(dt);

            return dt;
        }

        public bool ActualizarCliente()
        {
            string comandoSQL = "UPDATE Cliente SET " + "IdTipoCliente = @IdTipoCliente, " + "Identificador1 = @Identificador1, " + "Identificador2 = @Identificador2, " + "Documento = @Documento, " +
                "Telefono = @Telefono, " + "Correo = @Correo, " + "Direccion = @Direccion, " + "Estado = @Estado " + "WHERE IdCliente = @IdCliente;";

            using (SqlConnection conexion = Conexion.Conectar())
            {
                using (SqlCommand comandoObjeto = new SqlCommand(comandoSQL, conexion))
                {
                    comandoObjeto.Parameters.AddWithValue("@IdCliente", IdCliente);
                    comandoObjeto.Parameters.AddWithValue("@IdTipoCliente", TipoCliente);
                    comandoObjeto.Parameters.AddWithValue("@Identificador1", Identificador1);
                    comandoObjeto.Parameters.AddWithValue("@Identificador2", Identificador2);
                    comandoObjeto.Parameters.AddWithValue("@Documento", Documento);
                    comandoObjeto.Parameters.AddWithValue("@Telefono", Telefono);
                    comandoObjeto.Parameters.AddWithValue("@Correo", Correo);
                    comandoObjeto.Parameters.AddWithValue("@Direccion", Direccion);
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

                                MessageBox.Show("El identificador del cliente ya existe en la base de datos. Por favor verifique los datos.", "Registro Duplicado", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                                break;

                            default:

                                MessageBox.Show("Ocurrió un error inesperado en la base de datos " + ex.Message, "Error " + ex.Number, MessageBoxButtons.OK, MessageBoxIcon.Warning);

                                break;
                        }

                        return false;
                    }
                }
            }
        }
        public static int ContarClientesTotales()
        {
            int total = 0;
            using (SqlConnection conexion = Conexion.Conectar())
            {
                string comandoSQL = "SELECT COUNT(*) FROM Cliente;";
                using (SqlCommand comandoObjeto = new SqlCommand(comandoSQL, conexion))
                {
                    total = Convert.ToInt32(comandoObjeto.ExecuteScalar());
                }
            }
            return total;
        }

        public static int ContarClientesActivos()
        {
            int total = 0;
            using (SqlConnection conexion = Conexion.Conectar())
            {
                string comandoSQL = "SELECT COUNT(*) FROM Cliente WHERE Estado = 'Activo';";
                using (SqlCommand comandoObjeto = new SqlCommand(comandoSQL, conexion))
                {
                    total = Convert.ToInt32(comandoObjeto.ExecuteScalar());
                }
            }
            return total;
        }

        public static int ContarClientesInactivos()
        {
            int total = 0;
            using (SqlConnection conexion = Conexion.Conectar())
            {
                string comandoSQL = "SELECT COUNT(*) FROM Cliente WHERE Estado = 'Inactivo';";
                using (SqlCommand comandoObjeto = new SqlCommand(comandoSQL, conexion))
                {
                    total = Convert.ToInt32(comandoObjeto.ExecuteScalar());
                }
            }
            return total;
        }


        public static DataTable BuscarCliente(string texto, int tipoCliente)
        {
            SqlConnection conectar = Conexion.Conectar();

            string comando = @"
        SELECT *
        FROM VerClientes
        WHERE IdCliente IN
        (
            SELECT IdCliente
            FROM Cliente
            WHERE IdTipoCliente = @TipoCliente
        )
        AND (
            Cliente LIKE @Texto
            OR Telefono LIKE @Texto
            OR Correo LIKE @Texto
            OR Direccion LIKE @Texto
        );";

            SqlDataAdapter adapter = new SqlDataAdapter(comando, conectar);

            adapter.SelectCommand.Parameters.AddWithValue("@Texto", "%" + texto + "%");
            adapter.SelectCommand.Parameters.AddWithValue("@TipoCliente", tipoCliente);

            DataTable dt = new DataTable();
            adapter.Fill(dt);

            conectar.Close();

            return dt;
        }
    }
}
