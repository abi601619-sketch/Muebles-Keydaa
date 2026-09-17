using Modelo.Conexión_DB;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Modelo.Entidades
{
    public class Material
    {
        private int idMaterial;
        private string NombreDelMaterial;
        private int UnidadDeMedida;
        private int Stock;
        private string Categoria;

        public int idMaterial1 { get => idMaterial; set => idMaterial = value; }
        public string NombreDelMaterial1 { get => NombreDelMaterial; set => NombreDelMaterial = value; }
        public int UnidadDeMedida1 { get => UnidadDeMedida; set => UnidadDeMedida = value; }
        public int Stock1 { get => Stock; set => Stock = value; }
        public string Categoria1 { get => Categoria; set => Categoria = value; }

        public Material(int idMaterial, string nombreDelMaterial, int unidadDeMedida, int stock, string categoria)
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
            try
            {
                using (SqlConnection conectar = Conexion.Conectar())
                {
                    string comando = "SELECT * FROM VerMaterial;";

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
                    case 208:
                        MessageBox.Show(
                            "La vista VerMaterial no existe en la base de datos.",
                            "Error 208",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                        break;

                    case 53:
                        MessageBox.Show(
                            "No se pudo establecer conexión con el servidor.",
                            "Error 53",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                        break;

                    case 4060:
                        MessageBox.Show(
                            "No se pudo acceder a la base de datos.",
                            "Error 4060",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                        break;

                    case -2:
                        MessageBox.Show(
                            "La operación tardó demasiado tiempo.",
                            "Error -2",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                        break;

                    default:
                        MessageBox.Show(
                            "Ocurrió un error al cargar los materiales.\n" + ex.Message,
                            "Error " + ex.Number,
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                        break;
                }

                return new DataTable();
            }
        }


        public bool InsertarMateriales()
        {
            string comandoSQL = @"INSERT INTO Material
                                (NombreDelMaterial, IdUnidadDeMedida, Stock, Categoria)
                                VALUES
                                (@NombreDelMaterial, @IdUnidadDeMedida, @Stock,
                                (SELECT TOP 1 IdCategoria
                                 FROM Categoria
                                 WHERE Nombre_Categoria = @Categoria));";

            using (SqlConnection conexion = Conexion.Conectar())
            {
                using (SqlCommand comandoObjeto = new SqlCommand(comandoSQL, conexion))
                {
                    comandoObjeto.Parameters.AddWithValue(
                        "@NombreDelMaterial",
                        NombreDelMaterial1);

                    comandoObjeto.Parameters.AddWithValue(
                        "@IdUnidadDeMedida",
                        UnidadDeMedida1);

                    comandoObjeto.Parameters.AddWithValue(
                        "@Stock",
                        Stock1);

                    comandoObjeto.Parameters.AddWithValue(
                        "@Categoria",
                        Categoria1);

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
                                    "El material ya existe en la base de datos.\nPor favor use otro material.",
                                    "Registro Duplicado",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Warning);
                                break;

                            case 547:
                                MessageBox.Show(
                                    "La categoría o unidad de medida seleccionada no es válida.",
                                    "Error 547",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Warning);
                                break;

                            case 515:
                                MessageBox.Show(
                                    "Debe completar todos los campos obligatorios.",
                                    "Error 515",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Warning);
                                break;

                            case 245:
                                MessageBox.Show(
                                    "Uno de los datos ingresados tiene un formato incorrecto.",
                                    "Error 245",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Warning);
                                break;

                            case 8115:
                                MessageBox.Show(
                                    "El valor ingresado para el stock es demasiado grande.",
                                    "Error 8115",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Warning);
                                break;

                            case 53:
                                MessageBox.Show(
                                    "No se pudo establecer conexión con el servidor.",
                                    "Error 53",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Error);
                                break;

                            case 4060:
                                MessageBox.Show(
                                    "No se pudo acceder a la base de datos.",
                                    "Error 4060",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Error);
                                break;

                            case -2:
                                MessageBox.Show(
                                    "La operación tardó demasiado tiempo.",
                                    "Error -2",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Error);
                                break;

                            case 208:
                                MessageBox.Show(
                                    "La tabla o consulta utilizada no existe.",
                                    "Error 208",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Error);
                                break;

                            default:
                                MessageBox.Show(
                                    "Ocurrió un error inesperado en la base de datos.\n" + ex.Message,
                                    "Error " + ex.Number,
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Error);
                                break;
                        }

                        return false;
                    }
                }
            }
        }


        public bool ActualizarMaterial()
        {
            string comandoSQL = @"UPDATE Material
                                SET NombreDelMaterial = @NombreDelMaterial,
                                    IdUnidadDeMedida = @IdUnidadDeMedida,
                                    Stock = @Stock,
                                    Categoria =
                                    (SELECT TOP 1 IdCategoria
                                     FROM Categoria
                                     WHERE Nombre_Categoria = @Categoria)
                                WHERE IdMaterial = @IdMaterial;";

            using (SqlConnection conexion = Conexion.Conectar())
            {
                using (SqlCommand comandoObjeto = new SqlCommand(comandoSQL, conexion))
                {
                    comandoObjeto.Parameters.AddWithValue(
                        "@IdMaterial",
                        idMaterial1);

                    comandoObjeto.Parameters.AddWithValue(
                        "@NombreDelMaterial",
                        NombreDelMaterial1);

                    comandoObjeto.Parameters.AddWithValue(
                        "@IdUnidadDeMedida",
                        UnidadDeMedida1);

                    comandoObjeto.Parameters.AddWithValue(
                        "@Stock",
                        Stock1);

                    comandoObjeto.Parameters.AddWithValue(
                        "@Categoria",
                        Categoria1);

                    try
                    {
                        int filaAfectada = comandoObjeto.ExecuteNonQuery();

                        if (filaAfectada > 0)
                        {
                            return true;
                        }

                        MessageBox.Show(
                            "No se encontró el material que desea actualizar.",
                            "Material no encontrado",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning);

                        return false;
                    }
                    catch (SqlException ex)
                    {
                        switch (ex.Number)
                        {
                            case 2627:
                            case 2601:
                                MessageBox.Show(
                                    "El material ya existe en la base de datos.\nPor favor verifique los datos.",
                                    "Registro Duplicado",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Warning);
                                break;

                            case 547:
                                MessageBox.Show(
                                    "La categoría o unidad de medida seleccionada no es válida.",
                                    "Error 547",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Warning);
                                break;

                            case 515:
                                MessageBox.Show(
                                    "Debe completar todos los campos obligatorios.",
                                    "Error 515",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Warning);
                                break;

                            case 245:
                                MessageBox.Show(
                                    "Uno de los datos ingresados tiene un formato incorrecto.",
                                    "Error 245",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Warning);
                                break;

                            case 8115:
                                MessageBox.Show(
                                    "El valor ingresado para el stock es demasiado grande.",
                                    "Error 8115",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Warning);
                                break;

                            case 53:
                                MessageBox.Show(
                                    "No se pudo establecer conexión con el servidor.",
                                    "Error 53",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Error);
                                break;

                            case 4060:
                                MessageBox.Show(
                                    "No se pudo acceder a la base de datos.",
                                    "Error 4060",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Error);
                                break;

                            case -2:
                                MessageBox.Show(
                                    "La operación tardó demasiado tiempo.",
                                    "Error -2",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Error);
                                break;

                            case 208:
                                MessageBox.Show(
                                    "La tabla o consulta utilizada no existe.",
                                    "Error 208",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Error);
                                break;

                            default:
                                MessageBox.Show(
                                    "Ocurrió un error inesperado en la base de datos.\n" + ex.Message,
                                    "Error " + ex.Number,
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Error);
                                break;
                        }

                        return false;
                    }
                }
            }
        }


        public bool ActualizarStock(int cantidad)
        {
            string comandoSQL = @"UPDATE Material
                                SET Stock = Stock + @Cantidad
                                WHERE IdMaterial = @IdMaterial;";

            using (SqlConnection conexion = Conexion.Conectar())
            {
                using (SqlCommand comandoObjeto =
                    new SqlCommand(comandoSQL, conexion))
                {
                    comandoObjeto.Parameters.AddWithValue(
                        "@IdMaterial",
                        idMaterial1);

                    comandoObjeto.Parameters.AddWithValue(
                        "@Cantidad",
                        cantidad);

                    try
                    {
                        int filasAfectadas =
                            comandoObjeto.ExecuteNonQuery();

                        if (filasAfectadas > 0)
                        {
                            return true;
                        }

                        MessageBox.Show(
                            "No se encontró el material para actualizar el stock.",
                            "Material no encontrado",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning);

                        return false;
                    }
                    catch (SqlException ex)
                    {
                        switch (ex.Number)
                        {
                            case 547:
                                MessageBox.Show(
                                    "No se puede actualizar el stock del material.\nVerifique los datos relacionados.",
                                    "Error 547",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Warning);
                                break;

                            case 245:
                                MessageBox.Show(
                                    "La cantidad ingresada tiene un formato incorrecto.",
                                    "Error 245",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Warning);
                                break;

                            case 8115:
                                MessageBox.Show(
                                    "El nuevo valor del stock es demasiado grande.",
                                    "Error 8115",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Warning);
                                break;

                            case 53:
                                MessageBox.Show(
                                    "No se pudo establecer conexión con el servidor.",
                                    "Error 53",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Error);
                                break;

                            case 4060:
                                MessageBox.Show(
                                    "No se pudo acceder a la base de datos.",
                                    "Error 4060",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Error);
                                break;

                            case -2:
                                MessageBox.Show(
                                    "La operación tardó demasiado tiempo.",
                                    "Error -2",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Error);
                                break;

                            case 208:
                                MessageBox.Show(
                                    "La tabla Material no existe en la base de datos.",
                                    "Error 208",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Error);
                                break;

                            default:
                                MessageBox.Show(
                                    "Ocurrió un error al actualizar el stock.\n" + ex.Message,
                                    "Error " + ex.Number,
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Error);
                                break;
                        }

                        return false;
                    }
                }
            }
        }


        public static DataTable BuscarMaterial(string texto)
        {
            try
            {
                using (SqlConnection conectar = Conexion.Conectar())
                {
                    string comando = @"SELECT *
                                     FROM VerMaterial
                                     WHERE CAST(IdMaterial AS VARCHAR) LIKE @Texto
                                     OR Material LIKE @Texto
                                     OR Categoria LIKE @Texto
                                     ORDER BY IdMaterial;";

                    SqlDataAdapter adapter =
                        new SqlDataAdapter(comando, conectar);

                    adapter.SelectCommand.Parameters.AddWithValue(
                        "@Texto",
                        "%" + (texto ?? "") + "%");

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
                        MessageBox.Show(
                            "La vista VerMaterial no existe en la base de datos.",
                            "Error 208",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                        break;

                    case 53:
                        MessageBox.Show(
                            "No se pudo establecer conexión con el servidor.",
                            "Error 53",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                        break;

                    case 4060:
                        MessageBox.Show(
                            "No se pudo acceder a la base de datos.",
                            "Error 4060",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                        break;

                    case -2:
                        MessageBox.Show(
                            "La búsqueda tardó demasiado tiempo.",
                            "Error -2",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                        break;

                    default:
                        MessageBox.Show(
                            "Ocurrió un error al buscar los materiales.\n" + ex.Message,
                            "Error " + ex.Number,
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                        break;
                }

                return new DataTable();
            }
        }


        // CALCULAR ESTADÍSTICAS DE INVENTARIO

        // Total de materiales
        public static int ContarMaterialesTotales()
        {
            try
            {
                using (SqlConnection conexion = Conexion.Conectar())
                {
                    string query = @"SELECT COUNT(*) FROM VerMaterial";

                    using (SqlCommand comando = new SqlCommand(query, conexion))
                    {
                        return Convert.ToInt32(comando.ExecuteScalar());
                    }
                }
            }
            catch (SqlException ex)
            {
                switch (ex.Number)
                {
                    case 208:
                        MessageBox.Show(
                            "La vista VerMaterial no existe en la base de datos.",
                            "Error 208",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                        break;

                    case 53:
                        MessageBox.Show(
                            "No se pudo establecer conexión con el servidor.",
                            "Error 53",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                        break;

                    case 4060:
                        MessageBox.Show(
                            "No se pudo acceder a la base de datos.",
                            "Error 4060",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                        break;

                    case -2:
                        MessageBox.Show(
                            "La consulta tardó demasiado tiempo.",
                            "Error -2",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                        break;

                    default:
                        MessageBox.Show(
                            "Error al contar los materiales totales.\n" + ex.Message,
                            "Error " + ex.Number,
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                        break;
                }

                return 0;
            }
        }


        // Materiales agotándose
        public static int ContarMaterialesAgotandose()
        {
            try
            {
                using (SqlConnection conexion = Conexion.Conectar())
                {
                    string query = @"SELECT COUNT(*)
                                     FROM VerMaterial
                                     WHERE Stock BETWEEN 1 AND 15";

                    using (SqlCommand comando = new SqlCommand(query, conexion))
                    {
                        return Convert.ToInt32(comando.ExecuteScalar());
                    }
                }
            }
            catch (SqlException ex)
            {
                switch (ex.Number)
                {
                    case 208:
                        MessageBox.Show(
                            "La vista VerMaterial no existe en la base de datos.",
                            "Error 208",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                        break;

                    case 53:
                        MessageBox.Show(
                            "No se pudo establecer conexión con el servidor.",
                            "Error 53",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                        break;

                    case 4060:
                        MessageBox.Show(
                            "No se pudo acceder a la base de datos.",
                            "Error 4060",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                        break;

                    case -2:
                        MessageBox.Show(
                            "La consulta tardó demasiado tiempo.",
                            "Error -2",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                        break;

                    default:
                        MessageBox.Show(
                            "Error al contar los materiales que se están agotando.\n" + ex.Message,
                            "Error " + ex.Number,
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                        break;
                }

                return 0;
            }
        }


        // Materiales disponibles
        public static int ContarMaterialesDisponibles()
        {
            try
            {
                using (SqlConnection conexion = Conexion.Conectar())
                {
                    string query = @"SELECT COUNT(*)
                                     FROM VerMaterial
                                     WHERE Stock > 15";

                    using (SqlCommand comando = new SqlCommand(query, conexion))
                    {
                        return Convert.ToInt32(comando.ExecuteScalar());
                    }
                }
            }
            catch (SqlException ex)
            {
                switch (ex.Number)
                {
                    case 208:
                        MessageBox.Show(
                            "La vista VerMaterial no existe en la base de datos.",
                            "Error 208",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                        break;

                    case 53:
                        MessageBox.Show(
                            "No se pudo establecer conexión con el servidor.",
                            "Error 53",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                        break;

                    case 4060:
                        MessageBox.Show(
                            "No se pudo acceder a la base de datos.",
                            "Error 4060",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                        break;

                    case -2:
                        MessageBox.Show(
                            "La consulta tardó demasiado tiempo.",
                            "Error -2",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                        break;

                    default:
                        MessageBox.Show(
                            "Error al contar los materiales disponibles.\n" + ex.Message,
                            "Error " + ex.Number,
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                        break;
                }

                return 0;
            }
        }


        // Materiales agotados
        public static int ContarMaterialesAgotados()
        {
            try
            {
                using (SqlConnection conexion = Conexion.Conectar())
                {
                    string query = @"SELECT COUNT(*)
                                     FROM VerMaterial
                                     WHERE Stock = 0";

                    using (SqlCommand comando = new SqlCommand(query, conexion))
                    {
                        return Convert.ToInt32(comando.ExecuteScalar());
                    }
                }
            }
            catch (SqlException ex)
            {
                switch (ex.Number)
                {
                    case 208:
                        MessageBox.Show(
                            "La vista VerMaterial no existe en la base de datos.",
                            "Error 208",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                        break;

                    case 53:
                        MessageBox.Show(
                            "No se pudo establecer conexión con el servidor.",
                            "Error 53",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                        break;

                    case 4060:
                        MessageBox.Show(
                            "No se pudo acceder a la base de datos.",
                            "Error 4060",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                        break;

                    case -2:
                        MessageBox.Show(
                            "La consulta tardó demasiado tiempo.",
                            "Error -2",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                        break;

                    default:
                        MessageBox.Show(
                            "Error al contar los materiales agotados.\n" + ex.Message,
                            "Error " + ex.Number,
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                        break;
                }

                return 0;
            }
        }
    }
}
