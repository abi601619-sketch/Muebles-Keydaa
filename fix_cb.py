import os

def fix_cb_items(path):
    with open(path, 'r', encoding='cp1252') as f:
        content = f.read()

    old_items = '''            this.cbUnidadMedida.Items.AddRange(new object[] {
            "Centimetros",
            "Metros",
            "Pliegos"});'''
            
    new_items = '''            this.cbUnidadMedida.Items.AddRange(new object[] {
            "Centimetros",
            "Metros",
            "Pliegos",
            "Piezas",
            "Pulgadas",
            "Litros",
            "Galones",
            "Libras"});'''
            
    content = content.replace(old_items, new_items)

    with open(path, 'w', encoding='cp1252') as f:
        f.write(content)

fix_cb_items(r'.\Vista\Inventario\frmInventario.Designer.cs')
