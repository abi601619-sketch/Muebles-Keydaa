import os

def add_delete(path):
    with open(path, 'r', encoding='cp1252') as f:
        content = f.read()
        
    method = '''
        public bool EliminarMaterial()
        {
            SqlConnection conectar = Conexion.Conectar();
            string comando = "DELETE FROM Material WHERE IdMaterial = @IdMaterial;";
            SqlCommand cmd = new SqlCommand(comando, conectar);
            cmd.Parameters.AddWithValue("@IdMaterial", idMaterial1);
            
            try {
                return cmd.ExecuteNonQuery() > 0;
            } catch (Exception) {
                return false;
            } finally {
                conectar.Close();
            }
        }
'''
    content = content.replace('        public bool ActualizarMaterial()', method + '        public bool ActualizarMaterial()')
    
    with open(path, 'w', encoding='cp1252') as f:
        f.write(content)

add_delete(r'.\Modelo\Entidades\Material.cs')
