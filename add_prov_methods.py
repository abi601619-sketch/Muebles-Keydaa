import os

def add_methods(path):
    with open(path, 'r', encoding='cp1252') as f:
        content = f.read()
        
    methods = '''
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
                    cmd.Parameters.AddWithValue("@Ubicacion", Ubicaci\xf3n1);
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
        
        public bool EliminarProveedor()
        {
            string comandoSQL = "DELETE FROM Proveedor WHERE IdProveedor = @IdProveedor;";
            using (SqlConnection conexion = Conexion.Conectar())
            {
                using (SqlCommand cmd = new SqlCommand(comandoSQL, conexion))
                {
                    cmd.Parameters.AddWithValue("@IdProveedor", IdProveedor1);
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
'''
    content = content.replace('    }\n}\n', methods + '    }\n}\n')
    
    with open(path, 'w', encoding='cp1252') as f:
        f.write(content)

add_methods(r'.\Modelo\Entidades\Proveedor.cs')
