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

        public DbCliente() { }

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

        public static DataTable CargarCorporativos(int registrosSaltar, int registrosPorPagina)
        {
            DataTable dt = new DataTable();

            try
            {
                using (SqlConnection conectar = Conexion.Conectar())
                {
                    string comando = @"SELECT IdCliente, Identificador1 AS Nombre_De_Empresa, Identificador2 AS Nombre_Del_Encargado, Documento AS NIT, Telefono, Correo, Direccion, Estado 
                                       FROM Cliente 
                                       WHERE IdTipoCliente = 1 
                                       ORDER BY IdCliente 
                                       OFFSET @RegistrosSaltar ROWS FETCH NEXT @RegistrosPorPagina ROWS ONLY;";

                    using (SqlCommand cmd = new SqlCommand(comando, conectar))
                    {
                        cmd.Parameters.AddWithValue("@RegistrosSaltar", registrosSaltar);
                        cmd.Parameters.AddWithValue("@RegistrosPorPagina", registrosPorPagina);

                        using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                            adapter.Fill(dt);
                    }
                }
            }
            catch (SqlException ex)
            {
                switch (ex.Number)
                {
                    case 53:
                        MessageBox.Show("No se puede conectar al servidor SQL.", "ERR-SQL-001", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;

                    case 4060:
                        MessageBox.Show("No se puede acceder a la base de datos.", "ERR-SQL-002", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;

                    case -2:
                        MessageBox.Show("Tiempo de espera agotado.", "ERR-SQL-003", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;

                    case 208:
                        MessageBox.Show("Tabla, vista o procedimiento no encontrado.", "ERR-SQL-004", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;

                    default:
                        MessageBox.Show("Error inesperado de SQL.", "ERR-SQL-999", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Error inesperado de SQL.", "ERR-SQL-999", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return dt;
        }

        public static int ObtenerTotalCorporativos()
        {
            int total = 0;

            try
            {
                using (SqlConnection conectar = Conexion.Conectar())
                {
                    string comando = "SELECT COUNT(*) FROM Cliente WHERE IdTipoCliente = 1;";

                    using (SqlCommand cmd = new SqlCommand(comando, conectar))
                        total = Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
            catch (SqlException ex)
            {
                switch (ex.Number)
                {
                    case 53:
                        MessageBox.Show("No se puede conectar al servidor SQL.", "ERR-SQL-001", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;

                    case 4060:
                        MessageBox.Show("No se puede acceder a la base de datos.", "ERR-SQL-002", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;

                    case -2:
                        MessageBox.Show("Tiempo de espera agotado.", "ERR-SQL-003", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;

                    case 208:
                        MessageBox.Show("Tabla, vista o procedimiento no encontrado.", "ERR-SQL-004", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;

                    default:
                        MessageBox.Show("Error inesperado de SQL.", "ERR-SQL-999", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Error inesperado de SQL.", "ERR-SQL-999", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return total;
        }

        public static DataTable CargarIndividuales(int registrosSaltar, int registrosPorPagina)
        {
            DataTable dt = new DataTable();

            try
            {
                using (SqlConnection conectar = Conexion.Conectar())
                {
                    string comando = @"SELECT IdCliente, Identificador1 AS Nombre, Identificador2 AS Apellidos, Documento AS DUI, Telefono, Correo, Direccion, Estado 
                                       FROM Cliente 
                                       WHERE IdTipoCliente = 2 
                                       ORDER BY IdCliente 
                                       OFFSET @RegistrosSaltar ROWS FETCH NEXT @RegistrosPorPagina ROWS ONLY;";

                    using (SqlCommand cmd = new SqlCommand(comando, conectar))
                    {
                        cmd.Parameters.AddWithValue("@RegistrosSaltar", registrosSaltar);
                        cmd.Parameters.AddWithValue("@RegistrosPorPagina", registrosPorPagina);

                        using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                            adapter.Fill(dt);
                    }
                }
            }
            catch (SqlException ex)
            {
                switch (ex.Number)
                {
                    case 53:
                        MessageBox.Show("No se puede conectar al servidor SQL.", "ERR-SQL-001", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;

                    case 4060:
                        MessageBox.Show("No se puede acceder a la base de datos.", "ERR-SQL-002", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;

                    case -2:
                        MessageBox.Show("Tiempo de espera agotado.", "ERR-SQL-003", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;

                    case 208:
                        MessageBox.Show("Tabla, vista o procedimiento no encontrado.", "ERR-SQL-004", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;

                    default:
                        MessageBox.Show("Error inesperado de SQL.", "ERR-SQL-999", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Error inesperado de SQL.", "ERR-SQL-999", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return dt;
        }

        public static int ObtenerTotalIndividuales()
        {
            int total = 0;

            try
            {
                using (SqlConnection conectar = Conexion.Conectar())
                {
                    string comando = "SELECT COUNT(*) FROM Cliente WHERE IdTipoCliente = 2;";

                    using (SqlCommand cmd = new SqlCommand(comando, conectar))
                        total = Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
            catch (SqlException ex)
            {
                switch (ex.Number)
                {
                    case 53:
                        MessageBox.Show("No se puede conectar al servidor SQL.", "ERR-SQL-001", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;

                    case 4060:
                        MessageBox.Show("No se puede acceder a la base de datos.", "ERR-SQL-002", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;

                    case -2:
                        MessageBox.Show("Tiempo de espera agotado.", "ERR-SQL-003", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;

                    case 208:
                        MessageBox.Show("Tabla, vista o procedimiento no encontrado.", "ERR-SQL-004", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;

                    default:
                        MessageBox.Show("Error inesperado de SQL.", "ERR-SQL-999", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Error inesperado de SQL.", "ERR-SQL-999", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return total;
        }

        public bool InsertarClienteIndividual()
        {
            string comandoSQL = @"INSERT INTO Cliente
                                  (IdTipoCliente, Identificador1, Identificador2, Documento, Telefono, Correo, Direccion, Estado) 
                                  VALUES
                                  (@IdTipoCliente, @Identificador1, @Identificador2, @Documento, @Telefono, @Correo, @Direccion, @Estado);";

            try
            {
                using (SqlConnection conexion = Conexion.Conectar())
                using (SqlCommand comandoObjeto = new SqlCommand(comandoSQL, conexion))
                {
                    comandoObjeto.Parameters.AddWithValue("@IdTipoCliente", TipoCliente);
                    comandoObjeto.Parameters.AddWithValue("@Identificador1", Identificador1);
                    comandoObjeto.Parameters.AddWithValue("@Identificador2", Identificador2);
                    comandoObjeto.Parameters.AddWithValue("@Documento", Documento);
                    comandoObjeto.Parameters.AddWithValue("@Telefono", Telefono);
                    comandoObjeto.Parameters.AddWithValue("@Correo", Correo);
                    comandoObjeto.Parameters.AddWithValue("@Direccion", Direccion);
                    comandoObjeto.Parameters.AddWithValue("@Estado", Estado);

                    int filaAfectada = comandoObjeto.ExecuteNonQuery();

                    return filaAfectada > 0;
                }
            }
            catch (SqlException ex)
            {
                switch (ex.Number)
                {
                    case 2627:
                        MessageBox.Show("Registro duplicado por clave primaria/UNIQUE.", "ERR-SQL-005", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        break;

                    case 2601:
                        MessageBox.Show("Registro duplicado por índice UNIQUE.", "ERR-SQL-006", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        break;

                    case 547:
                        MessageBox.Show("Violación de FK/CHECK.", "ERR-SQL-007", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        break;

                    case 515:
                        MessageBox.Show("Campo NOT NULL sin valor.", "ERR-SQL-008", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        break;

                    case 245:
                        MessageBox.Show("Conversión o formato de datos incorrecto.", "ERR-SQL-009", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        break;

                    case 8115:
                        MessageBox.Show("Desbordamiento numérico.", "ERR-SQL-010", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;

                    case 8152:
                        MessageBox.Show("Datos demasiado largos para la columna.", "ERR-SQL-011", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;

                    case 53:
                        MessageBox.Show("No se puede conectar al servidor SQL.", "ERR-SQL-001", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;

                    case 4060:
                        MessageBox.Show("No se puede acceder a la base de datos.", "ERR-SQL-002", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;

                    case -2:
                        MessageBox.Show("Tiempo de espera agotado.", "ERR-SQL-003", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;

                    case 208:
                        MessageBox.Show("Tabla, vista o procedimiento no encontrado.", "ERR-SQL-004", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;

                    default:
                        MessageBox.Show("Error inesperado de SQL.", "ERR-SQL-999", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                }

                return false;
            }
            catch (Exception)
            {
                MessageBox.Show("Error inesperado de SQL.", "ERR-SQL-999", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        public bool InsertarClienteCorporativo()
        {
            string comandoSQL = @"INSERT INTO Cliente
                                  (IdTipoCliente, Identificador1, Identificador2, Documento, Telefono, Correo, Direccion, Estado)
                                  VALUES
                                  (@IdTipoCliente, @Identificador1, @Identificador2, @Documento, @Telefono, @Correo, @Direccion, @Estado);";

            using (SqlConnection conexion = Conexion.Conectar())
            using (SqlCommand comandoObjeto = new SqlCommand(comandoSQL, conexion))
            {
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
                            MessageBox.Show("Registro duplicado por clave primaria/UNIQUE.", "ERR-SQL-005", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            break;

                        case 2601:
                            MessageBox.Show("Registro duplicado por índice UNIQUE.", "ERR-SQL-006", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            break;

                        case 547:
                            MessageBox.Show("Violación de FK/CHECK.", "ERR-SQL-007", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            break;

                        case 515:
                            MessageBox.Show("Campo NOT NULL sin valor.", "ERR-SQL-008", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            break;

                        case 245:
                            MessageBox.Show("Conversión o formato de datos incorrecto.", "ERR-SQL-009", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            break;

                        case 8115:
                            MessageBox.Show("Desbordamiento numérico.", "ERR-SQL-010", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;

                        case 8152:
                            MessageBox.Show("Datos demasiado largos para la columna.", "ERR-SQL-011", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;

                        case 53:
                            MessageBox.Show("No se puede conectar al servidor SQL.", "ERR-SQL-001", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;

                        case 4060:
                            MessageBox.Show("No se puede acceder a la base de datos.", "ERR-SQL-002", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;

                        case -2:
                            MessageBox.Show("Tiempo de espera agotado.", "ERR-SQL-003", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;

                        case 208:
                            MessageBox.Show("Tabla, vista o procedimiento no encontrado.", "ERR-SQL-004", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;

                        default:
                            MessageBox.Show("Error inesperado de SQL.", "ERR-SQL-999", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                    }

                    return false;
                }
                catch (Exception)
                {
                    MessageBox.Show("Error inesperado de SQL.", "ERR-SQL-999", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
        }

        public static DataTable CargarClientesParaSeleccionar()
        {
            try
            {
                using (SqlConnection conectar = Conexion.Conectar())
                {
                    string comando = "SELECT * FROM SeleccionClientes";

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
                    case 53:
                        MessageBox.Show("No se puede conectar al servidor SQL.", "ERR-SQL-001", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;

                    case 4060:
                        MessageBox.Show("No se puede acceder a la base de datos.", "ERR-SQL-002", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;

                    case -2:
                        MessageBox.Show("Tiempo de espera agotado.", "ERR-SQL-003", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;

                    case 208:
                        MessageBox.Show("Tabla, vista o procedimiento no encontrado.", "ERR-SQL-004", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;

                    default:
                        MessageBox.Show("Error inesperado de SQL.", "ERR-SQL-999", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                }

                return new DataTable();
            }
            catch (Exception)
            {
                MessageBox.Show("Error inesperado de SQL.", "ERR-SQL-999", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return new DataTable();
            }
        }

        public bool ActualizarCliente()
        {
            string comandoSQL = @"UPDATE Cliente 
                                  SET IdTipoCliente = @IdTipoCliente,
                                      Identificador1 = @Identificador1,
                                      Identificador2 = @Identificador2,
                                      Documento = @Documento, 
                                      Telefono = @Telefono,
                                      Correo = @Correo,
                                      Direccion = @Direccion,
                                      Estado = @Estado 
                                  WHERE IdCliente = @IdCliente;";

            try
            {
                using (SqlConnection conexion = Conexion.Conectar())
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

                    int filaAfectada = comandoObjeto.ExecuteNonQuery();

                    return filaAfectada > 0;
                }
            }
            catch (SqlException ex)
            {
                switch (ex.Number)
                {
                    case 2627:
                        MessageBox.Show("Registro duplicado por clave primaria/UNIQUE.", "ERR-SQL-005", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        break;

                    case 2601:
                        MessageBox.Show("Registro duplicado por índice UNIQUE.", "ERR-SQL-006", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        break;

                    case 547:
                        MessageBox.Show("Violación de FK/CHECK.", "ERR-SQL-007", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        break;

                    case 515:
                        MessageBox.Show("Campo NOT NULL sin valor.", "ERR-SQL-008", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        break;

                    case 245:
                        MessageBox.Show("Conversión o formato de datos incorrecto.", "ERR-SQL-009", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        break;

                    case 8115:
                        MessageBox.Show("Desbordamiento numérico.", "ERR-SQL-010", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;

                    case 8152:
                        MessageBox.Show("Datos demasiado largos para la columna.", "ERR-SQL-011", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;

                    case 53:
                        MessageBox.Show("No se puede conectar al servidor SQL.", "ERR-SQL-001", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;

                    case 4060:
                        MessageBox.Show("No se puede acceder a la base de datos.", "ERR-SQL-002", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;

                    case -2:
                        MessageBox.Show("Tiempo de espera agotado.", "ERR-SQL-003", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;

                    case 208:
                        MessageBox.Show("Tabla, vista o procedimiento no encontrado.", "ERR-SQL-004", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;

                    default:
                        MessageBox.Show("Error inesperado de SQL.", "ERR-SQL-999", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                }

                return false;
            }
            catch (Exception)
            {
                MessageBox.Show("Error inesperado de SQL.", "ERR-SQL-999", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        public static int ContarClientesTotales()
        {
            int total = 0;

            try
            {
                using (SqlConnection conexion = Conexion.Conectar())
                {
                    string comandoSQL = "SELECT COUNT(*) FROM Cliente;";

                    using (SqlCommand comandoObjeto = new SqlCommand(comandoSQL, conexion))
                        total = Convert.ToInt32(comandoObjeto.ExecuteScalar());
                }
            }
            catch (SqlException ex)
            {
                switch (ex.Number)
                {
                    case 53:
                        MessageBox.Show("No se puede conectar al servidor SQL.", "ERR-SQL-001", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;

                    case 4060:
                        MessageBox.Show("No se puede acceder a la base de datos.", "ERR-SQL-002", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;

                    case -2:
                        MessageBox.Show("Tiempo de espera agotado.", "ERR-SQL-003", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;

                    case 208:
                        MessageBox.Show("Tabla, vista o procedimiento no encontrado.", "ERR-SQL-004", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;

                    default:
                        MessageBox.Show("Error inesperado de SQL.", "ERR-SQL-999", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Error inesperado de SQL.", "ERR-SQL-999", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return total;
        }

        public static int ContarClientesActivos()
        {
            int total = 0;

            try
            {
                using (SqlConnection conexion = Conexion.Conectar())
                {
                    string comandoSQL = "SELECT COUNT(*) FROM Cliente WHERE Estado = 'Activo';";

                    using (SqlCommand comandoObjeto = new SqlCommand(comandoSQL, conexion))
                        total = Convert.ToInt32(comandoObjeto.ExecuteScalar());
                }
            }
            catch (SqlException ex)
            {
                switch (ex.Number)
                {
                    case 53:
                        MessageBox.Show("No se puede conectar al servidor SQL.", "ERR-SQL-001", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;

                    case 4060:
                        MessageBox.Show("No se puede acceder a la base de datos.", "ERR-SQL-002", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;

                    case -2:
                        MessageBox.Show("Tiempo de espera agotado.", "ERR-SQL-003", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;

                    case 208:
                        MessageBox.Show("Tabla, vista o procedimiento no encontrado.", "ERR-SQL-004", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;

                    case 245:
                        MessageBox.Show("Conversión o formato de datos incorrecto.", "ERR-SQL-009", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;

                    default:
                        MessageBox.Show("Error inesperado de SQL.", "ERR-SQL-999", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Error inesperado de SQL.", "ERR-SQL-999", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return total;
        }

        public static int ContarClientesInactivos()
        {
            int total = 0;

            try
            {
                using (SqlConnection conexion = Conexion.Conectar())
                {
                    string comandoSQL = "SELECT COUNT(*) FROM Cliente WHERE Estado = 'Inactivo';";

                    using (SqlCommand comandoObjeto = new SqlCommand(comandoSQL, conexion))
                        total = Convert.ToInt32(comandoObjeto.ExecuteScalar());
                }
            }
            catch (SqlException ex)
            {
                switch (ex.Number)
                {
                    case 53:
                        MessageBox.Show("No se puede conectar al servidor SQL.", "ERR-SQL-001", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;

                    case 4060:
                        MessageBox.Show("No se puede acceder a la base de datos.", "ERR-SQL-002", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;

                    case -2:
                        MessageBox.Show("Tiempo de espera agotado.", "ERR-SQL-003", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;

                    case 208:
                        MessageBox.Show("Tabla, vista o procedimiento no encontrado.", "ERR-SQL-004", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;

                    case 245:
                        MessageBox.Show("Conversión o formato de datos incorrecto.", "ERR-SQL-009", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;

                    default:
                        MessageBox.Show("Error inesperado de SQL.", "ERR-SQL-999", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Error inesperado de SQL.", "ERR-SQL-999", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return total;
        }

        public static DataTable BuscarClienteIndividual(string texto)
        {
            DataTable dt = new DataTable();

            try
            {
                using (SqlConnection conexion = Conexion.Conectar())
                {
                    string consulta = @"SELECT * FROM BuscarClientesIndividuales 
                                        WHERE Nombre LIKE '%' + @Texto + '%' 
                                        OR Apellidos LIKE '%' + @Texto + '%' 
                                        OR DUI LIKE '%' + @Texto + '%' 
                                        OR Telefono LIKE '%' + @Texto + '%' 
                                        OR Correo LIKE '%' + @Texto + '%' 
                                        OR Direccion LIKE '%' + @Texto + '%';";

                    using (SqlDataAdapter adapter = new SqlDataAdapter(consulta, conexion))
                    {
                        adapter.SelectCommand.Parameters.AddWithValue("@Texto", texto ?? "");
                        adapter.Fill(dt);
                    }
                }
            }
            catch (SqlException ex)
            {
                switch (ex.Number)
                {
                    case 53:
                        MessageBox.Show("No se puede conectar al servidor SQL.", "ERR-SQL-001", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;

                    case 4060:
                        MessageBox.Show("No se puede acceder a la base de datos.", "ERR-SQL-002", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;

                    case -2:
                        MessageBox.Show("Tiempo de espera agotado.", "ERR-SQL-003", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;

                    case 208:
                        MessageBox.Show("Tabla, vista o procedimiento no encontrado.", "ERR-SQL-004", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;

                    default:
                        MessageBox.Show("Error inesperado de SQL.", "ERR-SQL-999", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Error inesperado de SQL.", "ERR-SQL-999", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return dt;
        }

        public static DataTable BuscarClienteCorporativo(string texto)
        {
            DataTable dt = new DataTable();

            try
            {
                using (SqlConnection conexion = Conexion.Conectar())
                {
                    string consulta = @"SELECT * FROM BuscarClientesCorporativos 
                                        WHERE [Empresa] LIKE '%' + @Texto + '%' 
                                        OR Encargado LIKE '%' + @Texto + '%' 
                                        OR NIT LIKE '%' + @Texto + '%' 
                                        OR Telefono LIKE '%' + @Texto + '%' 
                                        OR Correo LIKE '%' + @Texto + '%' 
                                        OR Direccion LIKE '%' + @Texto + '%';";

                    using (SqlDataAdapter adapter = new SqlDataAdapter(consulta, conexion))
                    {
                        adapter.SelectCommand.Parameters.AddWithValue("@Texto", texto ?? "");
                        adapter.Fill(dt);
                    }
                }
            }
            catch (SqlException ex)
            {
                switch (ex.Number)
                {
                    case 53:
                        MessageBox.Show("No se puede conectar al servidor SQL.", "ERR-SQL-001", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;

                    case 4060:
                        MessageBox.Show("No se puede acceder a la base de datos.", "ERR-SQL-002", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;

                    case -2:
                        MessageBox.Show("Tiempo de espera agotado.", "ERR-SQL-003", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;

                    case 208:
                        MessageBox.Show("Tabla, vista o procedimiento no encontrado.", "ERR-SQL-004", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;

                    default:
                        MessageBox.Show("Error inesperado de SQL.", "ERR-SQL-999", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Error inesperado de SQL.", "ERR-SQL-999", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return dt;
        }

        public static DataTable BuscarClientesSeleecion(string texto)
        {
            DataTable dt = new DataTable();

            try
            {
                using (SqlConnection conexion = Conexion.Conectar())
                {
                    string consulta = @"SELECT * FROM SeleccionClientes 
                                        WHERE Cliente LIKE '%' + @Texto + '%' 
                                        OR Telefono LIKE '%' + @Texto + '%' 
                                        OR Correo LIKE '%' + @Texto + '%' 
                                        OR Direccion LIKE '%' + @Texto + '%';";

                    using (SqlDataAdapter adapter = new SqlDataAdapter(consulta, conexion))
                    {
                        adapter.SelectCommand.Parameters.AddWithValue("@Texto", texto ?? "");
                        adapter.Fill(dt);
                    }
                }
            }
            catch (SqlException ex)
            {
                switch (ex.Number)
                {
                    case 53:
                        MessageBox.Show("No se puede conectar al servidor SQL.", "ERR-SQL-001", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;

                    case 4060:
                        MessageBox.Show("No se puede acceder a la base de datos.", "ERR-SQL-002", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;

                    case -2:
                        MessageBox.Show("Tiempo de espera agotado.", "ERR-SQL-003", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;

                    case 208:
                        MessageBox.Show("Tabla, vista o procedimiento no encontrado.", "ERR-SQL-004", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;

                    default:
                        MessageBox.Show("Error inesperado de SQL.", "ERR-SQL-999", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Error inesperado de SQL.", "ERR-SQL-999", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return dt;
        }
    }
}