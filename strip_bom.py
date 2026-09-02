import os

def fix_file(path):
    with open(path, 'r', encoding='utf-8') as f:
        content = f.read()
        
    if content.startswith('\ufeff'):
        content = content[1:]
        
    with open(path, 'w', encoding='utf-8') as f:
        f.write(content)

fix_file(r'.\Vista\Facturación\frmFacturacion.Designer.cs')
fix_file(r'.\Modelo\Entidades\Cotizacion.cs')
