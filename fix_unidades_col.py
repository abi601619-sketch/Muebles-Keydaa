import os
import re

def fix_inventario_unidades_col(path):
    with open(path, 'r', encoding='cp1252') as f:
        content = f.read()

    old_code = '''            string idUnidadStr = "";
            if (dgvMateriales.Columns.Contains("IdUnidadDeMedida")) idUnidadStr = fila.Cells["IdUnidadDeMedida"].Value?.ToString();
            else if (dgvMateriales.Columns.Contains("UnidadDeMedida")) idUnidadStr = fila.Cells["UnidadDeMedida"].Value?.ToString();
            else if (dgvMateriales.Columns.Contains("Unidad_De_Medida")) idUnidadStr = fila.Cells["Unidad_De_Medida"].Value?.ToString();'''
            
    new_code = '''            string colUnidad = "";
            foreach (DataGridViewColumn col in dgvMateriales.Columns)
            {
                if (col.Name.Contains("Unidad"))
                {
                    colUnidad = col.Name;
                    break;
                }
            }
            string idUnidadStr = "";
            if (!string.IsNullOrEmpty(colUnidad))
                idUnidadStr = fila.Cells[colUnidad].Value?.ToString();'''

    content = content.replace(old_code, new_code)
    
    # Also add "Piezas", "Pulgadas", "Litros", "Galones", "Libras" to the combobox in the form load or designer.
    # We can just do it in frmInventario_Load.

    with open(path, 'w', encoding='cp1252') as f:
        f.write(content)

fix_inventario_unidades_col(r'.\Vista\Inventario\frmInventario.cs')
