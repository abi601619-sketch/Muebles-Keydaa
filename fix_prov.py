import os
import re

def fix_proveedor(path):
    with open(path, 'r', encoding='cp1252') as f:
        content = f.read()
        
    # Find the old ActualizarProveedor and remove it
    old_method = r'        public bool ActualizarProveedor\(\)\s*\{\s*string comandoSQL = "UPDATE Proveedor SET Nombre_Proveedor = @Nombre, Telefono = @Telefono, Correo = @Correo WHERE IdProveedor = @IdProveedor";.*?catch \(SqlException\) \{ return false; \}\s*\}\s*\}\s*\}'
    
    content = re.sub(old_method, '', content, flags=re.DOTALL)
    
    with open(path, 'w', encoding='cp1252') as f:
        f.write(content)

fix_proveedor(r'.\Modelo\Entidades\Proveedor.cs')
