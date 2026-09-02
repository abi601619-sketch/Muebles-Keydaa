import os

def fix_query(path):
    with open(path, 'r', encoding='cp1252') as f:
        content = f.read()
        
    old_query = "SELECT v.IdVenta, CAST(v.IdVenta AS VARCHAR) + ' - ' + c.Nombre_Cliente AS Display FROM Venta v INNER JOIN Clientes c ON v.IdCliente = c.IdCliente LEFT JOIN Factura f ON v.IdVenta = f.IdVenta WHERE f.IdFactura IS NULL;"
    new_query = "SELECT v.IdVenta, CAST(v.IdVenta AS VARCHAR) + ' - ' + c.Identificador1 + ' ' + ISNULL(c.Identificador2, '') AS Display FROM Venta v INNER JOIN Cliente c ON v.IdCliente = c.IdCliente LEFT JOIN Factura f ON v.IdVenta = f.IdVenta WHERE f.IdFactura IS NULL;"
    
    content = content.replace(old_query, new_query)
    
    with open(path, 'w', encoding='cp1252') as f:
        f.write(content)

fix_query(r'.\Modelo\Entidades\Factura.cs')
