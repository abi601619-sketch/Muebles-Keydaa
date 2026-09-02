import os

def rename_prop(path):
    with open(path, 'r', encoding='cp1252') as f:
        content = f.read()
        
    content = content.replace('Ubicaci\xf3n1', 'Ubicacion1')
    content = content.replace('Ubicaci\xc3\xb3n1', 'Ubicacion1')
    content = content.replace('Ubicaci\xef\xbf\xbdn1', 'Ubicacion1')
    content = content.replace('Ubicaci?n1', 'Ubicacion1')
    
    with open(path, 'w', encoding='cp1252') as f:
        f.write(content)

rename_prop(r'.\Modelo\Entidades\Proveedor.cs')
rename_prop(r'.\Vista\Proveedores\frmProveedores.cs')
