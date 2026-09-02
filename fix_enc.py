import os

def fix_enc(path):
    with open(path, 'r', encoding='cp1252') as f:
        content = f.read()
        
    content = content.replace('Ubicaci\xc3\xb3n1', 'Ubicaci\xf3n1')
    content = content.replace('Ubicaci\xef\xbf\xbdn1', 'Ubicaci\xf3n1')
    
    with open(path, 'w', encoding='cp1252') as f:
        f.write(content)

fix_enc(r'.\Modelo\Entidades\Proveedor.cs')

def fix_inv(path):
    with open(path, 'r', encoding='cp1252') as f:
        content = f.read()
        
    content = content.replace('\xc3\xbf', '\xf3')
    content = content.replace('\xef\xbf\xbd', 'u')
    
    with open(path, 'w', encoding='cp1252') as f:
        f.write(content)

fix_inv(r'.\Vista\Inventario\frmInventario.cs')
