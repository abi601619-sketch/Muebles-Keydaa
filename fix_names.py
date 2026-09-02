import os

def fix_compras(path):
    with open(path, 'r', encoding='cp1252') as f:
        content = f.read()
        
    content = content.replace('Modelo.Entidades.DbCompras', 'Modelo.Entidades.ComprasDb')
    content = content.replace('MostrarRegistros();', 'MostrarCompras();')
    
    with open(path, 'w', encoding='cp1252') as f:
        f.write(content)

def fix_ventas(path):
    with open(path, 'r', encoding='cp1252') as f:
        content = f.read()
        
    content = content.replace('Modelo.Entidades.Ventas venta = new Modelo.Entidades.Ventas();', 'Modelo.Entidades.DbVentas venta = new Modelo.Entidades.DbVentas();')
    
    with open(path, 'w', encoding='cp1252') as f:
        f.write(content)

fix_compras(r'.\Vista\Compras\frmCompras.cs')
fix_ventas(r'.\Vista\Ventas\frmVentas.cs')
