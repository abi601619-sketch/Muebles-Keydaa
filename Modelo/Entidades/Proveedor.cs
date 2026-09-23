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

        public DbProveedor() { }

        public int IdProveedor1 { get => IdProveedor; set => IdProveedor = value; }
        public string Nombre_Proveedor1 { get => Nombre_Proveedor; set => Nombre_Proveedor = value; }
        public string Telefono1 { get => Telefono; set => Telefono = value; }
        public string Correo1 { get => Correo; set => Correo = value; }
        public string Ubicacion1 { get => Ubicacion; set => Ubicacion = value; }
        public bool Estado1 { get => Estado; set => Estado = value; }


        public static DataTable CargarProveedor()
        {
            try
            {
                using (SqlConnection conectar = Conexion.Conectar())
                {
                    string comando = "SELECT * FROM VerProveedores;";

                    SqlDataAdapter adapter = new SqlDataAdapter(comando, conectar);
                    DataTable dt = new DataTable();

                    adapter.Fill(dt);
                    return dt;
                }
            }
            catch (SqlException ex)
            {
                switch (ex.Number)
                {
                    case 53:
                        MessageBox.Show("No se pudo establecer conexión con el servidor.", "ERR-SQL-001", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;

                    case 4060:
                        MessageBox.Show("No se pudo acceder a la base de datos.", "ERR-SQL-002", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;

                    case -2:
                        MessageBox.Show("La consulta tardó demasiado tiempo.", "ERR-SQL-003", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        break;

                    case 208:
                        MessageBox.Show("La vista VerProveedores no existe en la base de datos.", "ERR-SQL-004", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;

                    default:
                        MessageBox.Show("Ocurrió un error al cargar los proveedores.\n" + ex.Message, "ERR-SQL-999", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                }

                return new DataTable();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrió un error inesperado.\n" + ex.Message, "ERR-SQL-999", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return new DataTable();
            }
        }


        public bool InsertarProveedor()
        {
            string comandoSQL = @"INSERT INTO Proveedor
                (Nombre_Proveedor, Telefono, Correo, Ubicacion, Estado)
                VALUES (@Nombre, @Telefono, @Correo, @Ubicacion, @Estado);";

            try
            {
                using (SqlConnection conexion = Conexion.Conectar())
                using (SqlCommand comando = new SqlCommand(comandoSQL, conexion))
                {
                    comando.Parameters.AddWithValue("@Nombre", Nombre_Proveedor);
                    comando.Parameters.AddWithValue("@Telefono", Telefono);
                    comando.Parameters.AddWithValue("@Correo", Correo);
                    comando.Parameters.AddWithValue("@Ubicacion", Ubicacion);
                    comando.Parameters.AddWithValue("@Estado", Estado);

                    return comando.ExecuteNonQuery() > 0;
                }
            }
            catch (SqlException ex)
            {
                switch (ex.Number)
                {
                    case 2627:
                        MessageBox.Show("El proveedor ya existe por una clave primaria o restricción UNIQUE.", "ERR-SQL-005", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        break;

                    case 2601:
                        MessageBox.Show("El proveedor ya existe por un índice UNIQUE.", "ERR-SQL-006", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        break;

                    case 547:
                        MessageBox.Show("No se puede registrar el proveedor debido a una restricción de la base de datos.", "ERR-SQL-007", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        break;

                    case 515:
                        MessageBox.Show("Hay campos obligatorios sin completar.", "ERR-SQL-008", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        break;

                    case 245:
                        MessageBox.Show("Uno de los datos ingresados tiene un formato incorrecto.", "ERR-SQL-009", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        break;

                    case 8115:
                        MessageBox.Show("Uno de los valores numéricos excede el límite permitido.", "ERR-SQL-010", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        break;

                    case 8152:
                        MessageBox.Show("Uno de los datos es demasiado largo para la columna correspondiente.", "ERR-SQL-011", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        break;

                    case 53:
                        MessageBox.Show("No se pudo establecer conexión con el servidor.", "ERR-SQL-001", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;

                    case 4060:
                        MessageBox.Show("No se pudo acceder a la base de datos.", "ERR-SQL-002", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;

                    case -2:
                        MessageBox.Show("La operación tardó demasiado tiempo.", "ERR-SQL-003", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        break;

                    case 208:
                        MessageBox.Show("La tabla Proveedor no existe en la base de datos.", "ERR-SQL-004", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;

                    default:
                        MessageBox.Show("Ocurrió un error al registrar el proveedor.\n" + ex.Message, "ERR-SQL-999", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                }

                return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrió un error inesperado.\n" + ex.Message, "ERR-SQL-999", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }


        public bool ActualizarProveedor()
        {
            string comandoSQL = @"UPDATE Proveedor SET Nombre_Proveedor = @Nombre, Telefono = @Telefono,
                Correo = @Correo, Ubicacion = @Ubicacion WHERE IdProveedor = @IdProveedor;";

            try
            {
                using (SqlConnection conexion = Conexion.Conectar())
                using (SqlCommand comando = new SqlCommand(comandoSQL, conexion))
                {
                    comando.Parameters.AddWithValue("@IdProveedor", IdProveedor1);
                    comando.Parameters.AddWithValue("@Nombre", Nombre_Proveedor1);
                    comando.Parameters.AddWithValue("@Telefono", Telefono1);
                    comando.Parameters.AddWithValue("@Correo", Correo1);
                    comando.Parameters.AddWithValue("@Ubicacion", Ubicacion1);

                    if (comando.ExecuteNonQuery() > 0)
                        return true;

                    MessageBox.Show("No se encontró el proveedor que desea actualizar.", "Proveedor no encontrado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
            }
            catch (SqlException ex)
            {
                switch (ex.Number)
                {
                    case 2627:
                        MessageBox.Show("Los datos del proveedor ya existen por una clave primaria o restricción UNIQUE.", "ERR-SQL-005", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        break;

                    case 2601:
                        MessageBox.Show("Los datos del proveedor ya existen por un índice UNIQUE.", "ERR-SQL-006", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        break;

                    case 547:
                        MessageBox.Show("No se puede actualizar el proveedor debido a una restricción.", "ERR-SQL-007", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        break;

                    case 515:
                        MessageBox.Show("Hay campos obligatorios sin completar.", "ERR-SQL-008", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        break;

                    case 245:
                        MessageBox.Show("Uno de los datos ingresados tiene un formato incorrecto.", "ERR-SQL-009", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        break;

                    case 8115:
                        MessageBox.Show("Uno de los valores numéricos excede el límite permitido.", "ERR-SQL-010", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        break;

                    case 8152:
                        MessageBox.Show("Uno de los datos es demasiado largo para la columna correspondiente.", "ERR-SQL-011", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        break;

                    case 53:
                        MessageBox.Show("No se pudo establecer conexión con el servidor.", "ERR-SQL-001", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;

                    case 4060:
                        MessageBox.Show("No se pudo acceder a la base de datos.", "ERR-SQL-002", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;

                    case -2:
                        MessageBox.Show("La operación tardó demasiado tiempo.", "ERR-SQL-003", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        break;

                    case 208:
                        MessageBox.Show("La tabla Proveedor no existe en la base de datos.", "ERR-SQL-004", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;

                    default:
                        MessageBox.Show("Ocurrió un error al actualizar el proveedor.\n" + ex.Message, "ERR-SQL-999", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                }

                return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrió un error inesperado.\n" + ex.Message, "ERR-SQL-999", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }


        public bool DesactivarProveedor(int idProveedor)
        {
            try
            {
                using (SqlConnection conexion = Conexion.Conectar())
                {
                    string consultaEstado = "SELECT Estado FROM Proveedor WHERE IdProveedor = @IdProveedor;";

                    using (SqlCommand cmdEstado = new SqlCommand(consultaEstado, conexion))
                    {
                        cmdEstado.Parameters.AddWithValue("@IdProveedor", idProveedor);

                        object resultado = cmdEstado.ExecuteScalar();

                        if (resultado == null)
                        {
                            MessageBox.Show("No se encontró el proveedor indicado.", "Proveedor no encontrado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return false;
                        }

                        if (!Convert.ToBoolean(resultado))
                        {
                            MessageBox.Show("El proveedor ya se encuentra inactivo.", "Proveedor inactivo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return false;
                        }
                    }

                    string consultaDesactivar = "UPDATE Proveedor SET Estado = 0 WHERE IdProveedor = @IdProveedor;";

                    using (SqlCommand comando = new SqlCommand(consultaDesactivar, conexion))
                    {
                        comando.Parameters.AddWithValue("@IdProveedor", idProveedor);
                        return comando.ExecuteNonQuery() > 0;
                    }
                }
            }
            catch (SqlException ex)
            {
                switch (ex.Number)
                {
                    case 547:
                        MessageBox.Show("No se puede desactivar el proveedor debido a una restricción.", "ERR-SQL-007", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        break;

                    case 515:
                        MessageBox.Show("El estado del proveedor no puede quedar vacío.", "ERR-SQL-008", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        break;

                    case 245:
                        MessageBox.Show("El ID del proveedor no tiene un formato válido.", "ERR-SQL-009", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        break;

                    case 53:
                        MessageBox.Show("No se pudo establecer conexión con el servidor.", "ERR-SQL-001", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;

                    case 4060:
                        MessageBox.Show("No se pudo acceder a la base de datos.", "ERR-SQL-002", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;

                    case -2:
                        MessageBox.Show("La operación tardó demasiado tiempo.", "ERR-SQL-003", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        break;

                    case 208:
                        MessageBox.Show("La tabla Proveedor no existe en la base de datos.", "ERR-SQL-004", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;

                    default:
                        MessageBox.Show("Ocurrió un error al desactivar el proveedor.\n" + ex.Message, "ERR-SQL-999", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                }

                return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrió un error inesperado.\n" + ex.Message, "ERR-SQL-999", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }


        public static DataTable BuscarProveedor(string termino)
        {
            try
            {
                using (SqlConnection conexion = Conexion.Conectar())
                {
                    string comando = @"SELECT * FROM VerProveedores
                        WHERE CAST(IdProveedor AS VARCHAR) LIKE @Buscar OR Proveedor LIKE @Buscar;";

                    SqlDataAdapter adapter = new SqlDataAdapter(comando, conexion);
                    adapter.SelectCommand.Parameters.AddWithValue("@Buscar", "%" + (termino ?? "") + "%");

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
                        MessageBox.Show("La vista VerProveedores no existe en la base de datos.", "ERR-SQL-004", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;

                    case 53:
                        MessageBox.Show("No se pudo establecer conexión con el servidor.", "ERR-SQL-001", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;

                    case 4060:
                        MessageBox.Show("No se pudo acceder a la base de datos.", "ERR-SQL-002", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;

                    case -2:
                        MessageBox.Show("La búsqueda tardó demasiado tiempo.", "ERR-SQL-003", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        break;

                    case 245:
                        MessageBox.Show("El valor de búsqueda no tiene un formato válido.", "ERR-SQL-009", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        break;

                    default:
                        MessageBox.Show("Ocurrió un error al buscar los proveedores.\n" + ex.Message, "ERR-SQL-999", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                }

                return new DataTable();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrió un error inesperado.\n" + ex.Message, "ERR-SQL-999", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return new DataTable();
            }
        }
    }
}