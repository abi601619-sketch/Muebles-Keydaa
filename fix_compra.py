import os
import re

def fix_detalle_compra(path):
    with open(path, 'r', encoding='cp1252') as f:
        content = f.read()

    old_query = '''        SELECT *
        FROM VerDetalleCompra
        WHERE IdCompra = @IdCompra;'''
        
    new_query = '''        SELECT d.IdDetalleCompraMaterial, d.IdCompra, m.NombreDelMaterial as Material, d.Cantidad, d.PrecioUnitario
        FROM DetalleCompraMaterial d
        INNER JOIN Material m ON d.IdMaterial = m.IdMaterial
        WHERE d.IdCompra = @IdCompra;'''

    content = content.replace(old_query, new_query)

    with open(path, 'w', encoding='cp1252') as f:
        f.write(content)

fix_detalle_compra(r'.\Modelo\Entidades\DetalleCompraMaterial.cs')
