import os

def fix_inventario_unidades(path):
    with open(path, 'r', encoding='cp1252') as f:
        content = f.read()

    old_code = '''            string idUnidadStr = fila.Cells["IdUnidadDeMedida"].Value?.ToString();'''
    new_code = '''            string idUnidadStr = "";
            if (dgvMateriales.Columns.Contains("IdUnidadDeMedida")) idUnidadStr = fila.Cells["IdUnidadDeMedida"].Value?.ToString();
            else if (dgvMateriales.Columns.Contains("UnidadDeMedida")) idUnidadStr = fila.Cells["UnidadDeMedida"].Value?.ToString();
            else if (dgvMateriales.Columns.Contains("Unidad_De_Medida")) idUnidadStr = fila.Cells["Unidad_De_Medida"].Value?.ToString();'''

    content = content.replace(old_code, new_code)

    with open(path, 'w', encoding='cp1252') as f:
        f.write(content)

fix_inventario_unidades(r'.\Vista\Inventario\frmInventario.cs')
