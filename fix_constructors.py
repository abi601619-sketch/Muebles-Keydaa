import os

def fix_compras_cs(path):
    with open(path, 'r', encoding='cp1252') as f:
        content = f.read()
        
    content = content.replace('public ComprasDb(int idCompra, DateTime fechaCompra, decimal totalCompra, int idProveedor)', 'public ComprasDb() { }\n\n        public ComprasDb(int idCompra, DateTime fechaCompra, decimal totalCompra, int idProveedor)')
    
    with open(path, 'w', encoding='cp1252') as f:
        f.write(content)

fix_compras_cs(r'.\Modelo\Entidades\Compras.cs')

def fix_frm_ventas(path):
    with open(path, 'r', encoding='cp1252') as f:
        content = f.read()
        
    content = content.replace('venta.IdVenta = id;', 'venta.IdVenta1 = id;')
    
    with open(path, 'w', encoding='cp1252') as f:
        f.write(content)

fix_frm_ventas(r'.\Vista\Ventas\frmVentas.cs')
