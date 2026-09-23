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
            IdCliente = idCliente; TipoCliente = tipoCliente; Identificador1 = identificador1; Identificador2 = identificador2; Documento = documento; Telefono = telefono; Correo = correo; Direccion = direccion; Estado1 = estado;
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
                    string comando = @"SELECT IdCliente,Identificador1 AS Nombre_De_Empresa, Identificador2 AS Nombre_Del_Encargado, Documento AS NIT, Telefono, Correo, Direccion, Estado FROM Cliente WHERE IdTipoCliente = 1 ORDER BY IdCliente OFFSET @RegistrosSaltar ROWS FETCH NEXT @RegistrosPorPagina ROWS ONLY;";

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
                        MessageBox.Show("No se pudo establecer conexión con el servidor de base de datos.", "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                    case 4060:
                        MessageBox.Show("No se pudo acceder a la base de datos.", "Error de base de datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                    case -2:
                        MessageBox.Show("La operación tardó demasiado tiempo. Intente nuevamente.", "Tiempo de espera agotado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        break;
                    default:
                        MessageBox.Show("Ocurrió un error al cargar los clientes corporativos.\n\nCódigo: " + ex.Number + "\nDetalle: " + ex.Message, "Error de base de datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrió un error inesperado al cargar los clientes corporativos.\n\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return dt;
        }

        //OBTENER LOS TOTALES DE CORPORATIVOS , PARA SABER EL TOTAL DE REGISTROS DE ESA TABLA
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
                        MessageBox.Show("No se pudo establecer conexión con el servidor de base de datos.", "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                    case 4060:
                        MessageBox.Show("No se pudo acceder a la base de datos.", "Error de base de datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                    case -2:
                        MessageBox.Show("La operación tardó demasiado tiempo. Intente nuevamente.", "Tiempo de espera agotado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        break;
                    default:
                        MessageBox.Show("Ocurrió un error al obtener el total de clientes corporativos.\n\nCódigo: " + ex.Number + "\nDetalle: " + ex.Message, "Error de base de datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrió un error inesperado al obtener el total de clientes corporativos.\n\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                    string comando = @"SELECT IdCliente, Identificador1 AS Nombre, Identificador2 AS Apellidos, Documento AS DUI, Telefono, Correo, Direccion, Estado FROM Cliente WHERE IdTipoCliente = 2 ORDER BY IdCliente OFFSET @RegistrosSaltar ROWS FETCH NEXT @RegistrosPorPagina ROWS ONLY;";

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
                        MessageBox.Show("No se pudo establecer conexión con el servidor de base de datos.", "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                    case 4060:
                        MessageBox.Show("No se pudo acceder a la base de datos.", "Error de base de datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                    case -2:
                        MessageBox.Show("La operación tardó demasiado tiempo. Intente nuevamente.", "Tiempo de espera agotado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        break;
                    default:
                        MessageBox.Show("Ocurrió un error al cargar los clientes individuales.\n\nCódigo: " + ex.Number + "\nDetalle: " + ex.Message, "Error de base de datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrió un error inesperado al cargar los clientes individuales.\n\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return dt;
        }

        //OBTENER LOS TOTALES DE INDIVIDUALES, PARA SABER EL TOTAL DE REGISTROS DE ESA TABLA
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
                        MessageBox.Show("No se pudo establecer conexión con el servidor de base de datos.", "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                    case 4060:
                        MessageBox.Show("No se pudo acceder a la base de datos.", "Error de base de datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                    case -2:
                        MessageBox.Show("La operación tardó demasiado tiempo. Intente nuevamente.", "Tiempo de espera agotado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        break;
                    default:
                        MessageBox.Show("Ocurrió un error al obtener el total de clientes individuales.\n\nCódigo: " + ex.Number + "\nDetalle: " + ex.Message, "Error de base de datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrió un error inesperado al obtener el total de clientes individuales.\n\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return total;
        }

        public bool InsertarClienteIndividual()
        {
            string comandoSQL = @"INSERT INTO Cliente(IdTipoCliente, Identificador1, Identificador2,Documento, Telefono, Correo, Direccion, Estado) VALUES(@IdTipoCliente, @Identificador1, @Identificador2, @Documento, @Telefono, @Correo, @Direccion, @Estado);";

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
                    case 2601:
                        MessageBox.Show("El identificador o documento del cliente ya existe en la base de datos.", "Registro duplicado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        break;
                    case 547:
                        MessageBox.Show("No se puede registrar el cliente porque el tipo de cliente seleccionado no existe.", "Error de relación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        break;
                    case 515:
                        MessageBox.Show("No se puede registrar el cliente porque uno de los campos obligatorios está vacío.", "Datos incompletos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        break;
                    case 245:
                        MessageBox.Show("Uno de los datos proporcionados tiene un formato incorrecto.", "Error de formato", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        break;
                    case 53:
                        MessageBox.Show("No se pudo establecer conexión con el servidor de base de datos.", "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                    case 4060:
                        MessageBox.Show("No se pudo acceder a la base de datos.", "Error de base de datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                    case -2:
                        MessageBox.Show("La operación tardó demasiado tiempo. Intente nuevamente.", "Tiempo de espera agotado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        break;
                    default:
                        MessageBox.Show("Ocurrió un error inesperado en la base de datos.\n\nCódigo: " + ex.Number + "\nDetalle: " + ex.Message, "Error de base de datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                }

                return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrió un error inesperado al registrar el cliente.\n\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        public bool InsertarClienteCorporativo()
        {
            string comandoSQL = "INSERT INTO Cliente(IdTipoCliente,Identificador1,Identificador2,Documento,Telefono,Correo,Direccion,Estado)" + "VALUES (@IdTipoCliente,@Identificador1,@Identificador2,@Documento,@Telefono,@Correo,@Direccion,@Estado);";

            using (SqlConnection conexion = Conexion.Conectar())
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
                            MessageBox.Show("El identificador o documento del cliente ya existe.", "Registro duplicado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            break;
                        case 547:
                            MessageBox.Show("No se puede actualizar el cliente porque el tipo de cliente seleccionado no existe.", "Error de relación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            break;
                        case 515:
                            MessageBox.Show("No se puede actualizar el cliente porque uno de los campos obligatorios está vacío.", "Datos incompletos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            break;
                        case 245:
                            MessageBox.Show("Uno de los datos proporcionados tiene un formato incorrecto.", "Error de formato", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            break;
                        case 53:
                            MessageBox.Show("No se pudo establecer conexión con el servidor de base de datos.", "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                        case 4060:
                            MessageBox.Show("No se pudo acceder a la base de datos.", "Error de base de datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                        case -2:
                            MessageBox.Show("La operación tardó demasiado tiempo.", "Tiempo de espera agotado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            break;
                        default:
                            MessageBox.Show("Ocurrió un error inesperado en la base de datos.\n\nCódigo: " + ex.Number + "\nDetalle: " + ex.Message, "Error de base de datos", MessageBoxButtons.OK, MessageBoxIcon.Error);

                            break;
                    }

                    return false;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ocurrió un error inesperado al actualizar el cliente.\n\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
        }

        public static DataTable CargarClientesParaSeleccionar()
        {
            SqlConnection conectar = Conexion.Conectar();
            string comando = "SELECT * FROM SeleccionClientes";
            SqlDataAdapter adapter = new SqlDataAdapter(comando, conectar);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            return dt;
        }

        public bool ActualizarCliente()
        {
            string comandoSQL = @"UPDATE Cliente SET IdTipoCliente = @IdTipoCliente, Identificador1 = @Identificador1, Identificador2 = @Identificador2, Documento = @Documento, Telefono = @Telefono, Correo = @Correo, Direccion = @Direccion, Estado = @Estado WHERE IdCliente = @IdCliente;";

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
                    case 2601:
                        MessageBox.Show("El identificador o documento del cliente ya existe.", "Registro duplicado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        break;
                    case 547:
                        MessageBox.Show("No se puede actualizar el cliente porque el tipo de cliente seleccionado no existe.", "Error de relación", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                        MessageBox.Show("No se puede actualizar el cliente porque uno de los campos obligatorios está vacío.", "Datos incompletos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        break;
                    case 245:
                        MessageBox.Show("Uno de los datos proporcionados tiene un formato incorrecto.", "Error de formato", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        break;
                    case 53:
                        MessageBox.Show("No se pudo establecer conexión con el servidor de base de datos.", "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                    case 4060:
                        MessageBox.Show("No se pudo acceder a la base de datos.", "Error de base de datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                    case -2:
                        MessageBox.Show("La operación tardó demasiado tiempo.", "Tiempo de espera agotado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        break;
                    default:
                        MessageBox.Show("Ocurrió un error inesperado en la base de datos.\n\nCódigo: " + ex.Number + "\nDetalle: " + ex.Message, "Error de base de datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                }

                return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrió un error inesperado al actualizar el cliente.\n\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                        MessageBox.Show("No se pudo establecer conexión con el servidor.", "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                    case 4060:
                        MessageBox.Show("No se pudo acceder a la base de datos.", "Error de base de datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                    case -2:
                        MessageBox.Show("La consulta tardó demasiado tiempo.", "Tiempo de espera agotado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        break;
                    default:
                        MessageBox.Show("Ocurrió un error al contar los clientes.\n\nCódigo: " + ex.Number + "\nDetalle: " + ex.Message, "Error de base de datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrió un error inesperado al contar los clientes.\n\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                        MessageBox.Show("No se pudo establecer conexión con el servidor.", "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                    case 4060:
                        MessageBox.Show("No se pudo acceder a la base de datos.", "Error de base de datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                    case -2:
                        MessageBox.Show("La consulta tardó demasiado tiempo.", "Tiempo de espera agotado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        break;
                    default:
                        MessageBox.Show("Ocurrió un error al contar los clientes activos.\n\nCódigo: " + ex.Number + "\nDetalle: " + ex.Message, "Error de base de datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrió un error inesperado al contar los clientes activos.\n\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                        MessageBox.Show("No se pudo establecer conexión con el servidor.", "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                    case 4060:
                        MessageBox.Show("No se pudo acceder a la base de datos.", "Error de base de datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                    case -2:
                        MessageBox.Show("La consulta tardó demasiado tiempo.", "Tiempo de espera agotado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        break;
                    default:
                        MessageBox.Show("Ocurrió un error al contar los clientes inactivos.\n\nCódigo: " + ex.Number + "\nDetalle: " + ex.Message, "Error de base de datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrió un error inesperado al contar los clientes inactivos.\n\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                    string consulta = @"SELECT * FROM BuscarClientesIndividuales WHERE Nombre LIKE '%' + @Texto + '%' OR Apellidos LIKE '%' + @Texto + '%' OR DUI LIKE '%' + @Texto + '%' OR Telefono LIKE '%' + @Texto + '%' OR Correo LIKE '%' + @Texto + '%' OR Direccion LIKE '%' + @Texto + '%';";

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
                        MessageBox.Show("No se pudo establecer conexión con el servidor.", "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                    case 4060:
                        MessageBox.Show("No se pudo acceder a la base de datos.", "Error de base de datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                    case -2:
                        MessageBox.Show("La búsqueda tardó demasiado tiempo.", "Tiempo de espera agotado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        break;
                    case 208:
                        MessageBox.Show("No se encontró la vista BuscarClientesIndividuales en la base de datos.", "Objeto no encontrado", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                    default:
                        MessageBox.Show("Ocurrió un error al buscar clientes individuales.\n\nCódigo: " + ex.Number + "\nDetalle: " + ex.Message, "Error de base de datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrió un error inesperado al buscar clientes individuales.\n\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                    string consulta = @"SELECT * FROM BuscarClientesCorporativos WHERE [Empresa] LIKE '%' + @Texto + '%' OR Encargado LIKE '%' + @Texto + '%' OR NIT LIKE '%' + @Texto + '%' OR Telefono LIKE '%' + @Texto + '%' OR Correo LIKE '%' + @Texto + '%' OR Direccion LIKE '%' + @Texto + '%';";

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
                        MessageBox.Show("No se pudo establecer conexión con el servidor.", "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                    case 4060:
                        MessageBox.Show("No se pudo acceder a la base de datos.", "Error de base de datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                    case -2:
                        MessageBox.Show("La búsqueda tardó demasiado tiempo.", "Tiempo de espera agotado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        break;
                    case 208:
                        MessageBox.Show("No se encontró la vista BuscarClientesCorporativos en la base de datos.", "Objeto no encontrado", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                    default:
                        MessageBox.Show("Ocurrió un error al buscar clientes corporativos.\n\nCódigo: " + ex.Number + "\nDetalle: " + ex.Message, "Error de base de datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrió un error inesperado al buscar clientes corporativos.\n\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                    string consulta = @"SELECT * FROM SeleccionClientes WHERE Cliente LIKE '%' + @Texto + '%' OR Telefono LIKE '%' + @Texto + '%' OR Correo LIKE '%' + @Texto + '%' OR Direccion LIKE '%' + @Texto + '%';";

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
                        MessageBox.Show("No se pudo establecer conexión con el servidor.", "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                    case 4060:
                        MessageBox.Show("No se pudo acceder a la base de datos.", "Error de base de datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                    case -2:
                        MessageBox.Show("La búsqueda tardó demasiado tiempo.", "Tiempo de espera agotado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        break;
                    case 208:
                        MessageBox.Show("No se encontró la vista SeleccionClientes en la base de datos.", "Objeto no encontrado", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                    default:
                        MessageBox.Show("Ocurrió un error al buscar clientes.\n\nCódigo: " + ex.Number + "\nDetalle: " + ex.Message, "Error de base de datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrió un error inesperado al buscar clientes.\n\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return dt;
        }
    }
}