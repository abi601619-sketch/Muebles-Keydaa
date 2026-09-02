import os

def fix_material_unidades(path):
    with open(path, 'r', encoding='cp1252') as f:
        content = f.read()
        
    content = content.replace('if (nombreUnidad == "Piezas") return 3;', 'if (nombreUnidad == "Piezas" || nombreUnidad == "Pliegos") return 3;')
    
    with open(path, 'w', encoding='cp1252') as f:
        f.write(content)

fix_material_unidades(r'.\Modelo\Entidades\Material.cs')
