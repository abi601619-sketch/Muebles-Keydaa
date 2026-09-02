import os

def add_bom(path):
    with open(path, 'r', encoding='utf-8') as f:
        content = f.read()
        
    if content.startswith('\ufeff'):
        content = content[1:]
        
    with open(path, 'w', encoding='utf-8-sig') as f:
        f.write(content)

add_bom(r'.\Vista\Facturación\frmFacturacion.Designer.cs')
add_bom(r'.\Modelo\Entidades\Cotizacion.cs')
