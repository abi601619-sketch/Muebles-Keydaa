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

        public int IdProveedor1
        {
            get => IdProveedor;
            set => IdProveedor = value;
        }

        public string Nombre_Proveedor1
        {
            get => Nombre_Proveedor;
            set => Nombre_Proveedor = value;
        }

        public string Telefono1
        {
            get => Telefono;
            set => Telefono = value;
        }

        public string Correo1
        {
            get => Correo;
            set => Correo = value;
        }

        public string Ubicacion1
        {
            get => Ubicacion;
            set => Ubicacion = value;
        }

        public bool Estado1
        {
            get => Estado;
            set => Estado = value;
        }


        public static DataTable CargarProveedor()
        {
            SqlConnection conectar = null;

            try
            {
                conectar = Conexion.Conectar();

                string comando = "SELECT * FROM VerProveedores;";

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
                            "Error 208: La tabla o vista de proveedores no existe.",
                            "Error",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                        break;

                    case 53:
                        MessageBox.Show(
                            "Error 53: No se pudo conectar con el servidor.",
                            "Error",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                        break;

                    case 4060:
                        MessageBox.Show(
                            "Error 4060: No se pudo acceder a la base de datos.",
                            "Error",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                        break;

                    case -2:
                        MessageBox.Show(
                            "Error -2: La operación tardó demasiado.",
                            "Error",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning);
                        break;

                    default:
                        MessageBox.Show(
                            "Error SQL " + ex.Number + ": " + ex.Message,
                            "Error",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
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
                    MessageBoxIcon.Error);

                return new DataTable();
            }
            finally
            {
                if (conectar != null)
                {
                    conectar.Dispose();
                }
            }
        }


        public bool InsertarProveedor()
        {
            string comandoSQL = @"INSERT INTO Proveedor
                                  (Nombre_Proveedor, Telefono, Correo, Ubicacion, Estado)
                                  VALUES
                                  (@Nombre_Proveedor, @Telefono, @Correo, @Ubicacion, @Estado);";

            try
            {
                using (SqlConnection conexion = Conexion.Conectar())
                {
                    using (SqlCommand comandoObjeto = new SqlCommand(comandoSQL, conexion))
                    {
                        comandoObjeto.Parameters.AddWithValue(
                            "@Nombre_Proveedor",
                            Nombre_Proveedor);

                        comandoObjeto.Parameters.AddWithValue(
                            "@Telefono",
                            Telefono);

                        comandoObjeto.Parameters.AddWithValue(
                            "@Correo",
                            Correo);

                        comandoObjeto.Parameters.AddWithValue(
                            "@Ubicacion",
                            Ubicacion);

                        comandoObjeto.Parameters.AddWithValue(
                            "@Estado",
                            Estado);

                        int filaAfectada = comandoObjeto.ExecuteNonQuery();

                        return filaAfectada > 0;
                    }
                }
            }
            catch (SqlException ex)
            {
                switch (ex.Number)
                {
                    case 2627:
                    case 2601:
                        MessageBox.Show(
                            "Error 2627/2601: El proveedor ya existe en la base de datos.",
                            "Registro Duplicado",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning);
                        break;

                    case 515:
                        MessageBox.Show(
                            "Error 515: Hay campos obligatorios sin completar.",
                            "Error",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning);
                        break;

                    case 547:
                        MessageBox.Show(
                            "Error 547: No se puede registrar el proveedor por una restricción.",
                            "Error",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning);
                        break;

                    case 53:
                        MessageBox.Show(
                            "Error 53: No se pudo conectar con el servidor.",
                            "Error",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                        break;

                    case 4060:
                        MessageBox.Show(
                            "Error 4060: No se pudo acceder a la base de datos.",
                            "Error",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                        break;

                    case -2:
                        MessageBox.Show(
                            "Error -2: La operación tardó demasiado.",
                            "Error",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning);
                        break;

                    default:
                        MessageBox.Show(
                            "Error SQL " + ex.Number + ": " + ex.Message,
                            "Error",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                        break;
                }

                return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Error inesperado: " + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                return false;
            }
        }


        public bool ActualizarProveedor()
        {
            string comandoSQL = @"UPDATE Proveedor 
                                  SET Nombre_Proveedor = @Nombre,
                                      Telefono = @Telefono,
                                      Correo = @Correo,
                                      Ubicacion = @Ubicacion
                                  WHERE IdProveedor = @IdProveedor;";

            try
            {
                using (SqlConnection conexion = Conexion.Conectar())
                {
                    using (SqlCommand cmd = new SqlCommand(comandoSQL, conexion))
                    {
                        cmd.Parameters.AddWithValue(
                            "@IdProveedor",
                            IdProveedor1);

                        cmd.Parameters.AddWithValue(
                            "@Nombre",
                            Nombre_Proveedor1);

                        cmd.Parameters.AddWithValue(
                            "@Telefono",
                            Telefono1);

                        cmd.Parameters.AddWithValue(
                            "@Correo",
                            Correo1);

                        cmd.Parameters.AddWithValue(
                            "@Ubicacion",
                            Ubicacion1);

                        int filasAfectadas = cmd.ExecuteNonQuery();

                        if (filasAfectadas > 0)
                        {
                            return true;
                        }

                        MessageBox.Show(
                            "No se encontró el proveedor que desea actualizar.",
                            "Aviso",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning);

                        return false;
                    }
                }
            }
            catch (SqlException ex)
            {
                switch (ex.Number)
                {
                    case 2627:
                    case 2601:
                        MessageBox.Show(
                            "Error 2627/2601: Los datos del proveedor ya existen.",
                            "Registro Duplicado",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning);
                        break;

                    case 515:
                        MessageBox.Show(
                            "Error 515: Hay campos obligatorios sin completar.",
                            "Error",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning);
                        break;

                    case 547:
                        MessageBox.Show(
                            "Error 547: No se puede actualizar por una restricción.",
                            "Error",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning);
                        break;

                    case 53:
                        MessageBox.Show(
                            "Error 53: No se pudo conectar con el servidor.",
                            "Error",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                        break;

                    case 4060:
                        MessageBox.Show(
                            "Error 4060: No se pudo acceder a la base de datos.",
                            "Error",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                        break;

                    case -2:
                        MessageBox.Show(
                            "Error -2: La operación tardó demasiado.",
                            "Error",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning);
                        break;

                    case 208:
                        MessageBox.Show(
                            "Error 208: La tabla de proveedores no existe.",
                            "Error",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                        break;

                    default:
                        MessageBox.Show(
                            "Error SQL " + ex.Number + ": " + ex.Message,
                            "Error",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                        break;
                }

                return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Error inesperado: " + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                return false;
            }
        }


        public bool DesactivarProveedor(int idProveedor)
        {
            try
            {
                using (SqlConnection conexion = Conexion.Conectar())
                {
                    // Primero verificamos el estado actual
                    string consultaEstado = @"SELECT Estado 
                                              FROM Proveedor 
                                              WHERE IdProveedor = @IdProveedor;";

                    using (SqlCommand cmdEstado = new SqlCommand(consultaEstado, conexion))
                    {
                        cmdEstado.Parameters.AddWithValue(
                            "@IdProveedor",
                            idProveedor);

                        // Obtiene un solo valor con ExecuteScalar
                        object resultado = cmdEstado.ExecuteScalar();

                        // Si no existe retorna false
                        if (resultado == null)
                        {
                            MessageBox.Show(
                                "No se encontró el proveedor indicado.",
                                "Aviso",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);

                            return false;
                        }

                        // Convierte el estado en true o false
                        bool estadoActual = Convert.ToBoolean(resultado);

                        // Si ya está en 0, el proveedor está inactivo
                        if (!estadoActual)
                        {
                            MessageBox.Show(
                                "El proveedor ya se encuentra inactivo.",
                                "Aviso",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);

                            return false;
                        }
                    }

                    // Si estaba activo, lo desactivamos
                    string consultaDesactivar = @"UPDATE Proveedor 
                                                  SET Estado = 0 
                                                  WHERE IdProveedor = @IdProveedor;";

                    using (SqlCommand cmd = new SqlCommand(
                        consultaDesactivar,
                        conexion))
                    {
                        cmd.Parameters.AddWithValue(
                            "@IdProveedor",
                            idProveedor);

                        int filasAfectadas = cmd.ExecuteNonQuery();

                        return filasAfectadas > 0;
                    }
                }
            }
            catch (SqlException ex)
            {
                switch (ex.Number)
                {
                    case 547:
                        MessageBox.Show(
                            "Error 547: No se puede desactivar el proveedor por una restricción.",
                            "Error",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning);
                        break;

                    case 515:
                        MessageBox.Show(
                            "Error 515: El estado del proveedor no puede quedar vacío.",
                            "Error",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning);
                        break;

                    case 245:
                        MessageBox.Show(
                            "Error 245: El ID del proveedor no es válido.",
                            "Error",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning);
                        break;

                    case 53:
                        MessageBox.Show(
                            "Error 53: No se pudo conectar con el servidor.",
                            "Error",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                        break;

                    case 4060:
                        MessageBox.Show(
                            "Error 4060: No se pudo acceder a la base de datos.",
                            "Error",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                        break;

                    case -2:
                        MessageBox.Show(
                            "Error -2: La operación tardó demasiado.",
                            "Error",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning);
                        break;

                    case 208:
                        MessageBox.Show(
                            "Error 208: La tabla de proveedores no existe.",
                            "Error",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                        break;

                    default:
                        MessageBox.Show(
                            "Error SQL " + ex.Number + ": " + ex.Message,
                            "Error",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                        break;
                }

                return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Error inesperado: " + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                return false;
            }
        }


        public static DataTable BuscarProveedor(string termino)
        {
            SqlConnection con = null;

            try
            {
                con = Conexion.Conectar();

                string comando = @"SELECT * 
                                   FROM VerProveedores 
                                   WHERE CAST(IdProveedor AS VARCHAR) LIKE @buscar 
                                      OR Proveedor LIKE @buscar;";

                SqlDataAdapter ad = new SqlDataAdapter(comando, con);

                ad.SelectCommand.Parameters.AddWithValue(
                    "@buscar",
                    "%" + termino + "%");

                DataTable dt = new DataTable();

                ad.Fill(dt);

                return dt;
            }
            catch (SqlException ex)
            {
                switch (ex.Number)
                {
                    case 208:
                        MessageBox.Show(
                            "Error 208: La vista VerProveedores no existe.",
                            "Error",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                        break;

                    case 53:
                        MessageBox.Show(
                            "Error 53: No se pudo conectar con el servidor.",
                            "Error",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                        break;

                    case 4060:
                        MessageBox.Show(
                            "Error 4060: No se pudo acceder a la base de datos.",
                            "Error",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                        break;

                    case -2:
                        MessageBox.Show(
                            "Error -2: La búsqueda tardó demasiado.",
                            "Error",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning);
                        break;

                    case 245:
                        MessageBox.Show(
                            "Error 245: El valor de búsqueda no es válido.",
                            "Error",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning);
                        break;

                    default:
                        MessageBox.Show(
                            "Error SQL " + ex.Number + ": " + ex.Message,
                            "Error",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
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
                    MessageBoxIcon.Error);

                return new DataTable();
            }
            finally
            {
                if (con != null)
                {
                    con.Dispose();
                }
            }
        }
    }
}
