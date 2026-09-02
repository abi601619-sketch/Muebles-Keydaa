import os
import re

def fix_compras_cs(path):
    with open(path, 'r', encoding='cp1252') as f:
        content = f.read()
        
    # Remove the duplicate idCompraSeleccionada and dgvComprasRegistradas_CellDoubleClick that I just added at the end
    content = re.sub(r'        private int idCompraSeleccionada = 0;\s*private void dgvComprasRegistradas_CellDoubleClick.*?        private void btnEliminar_Click', '        private void btnEliminar_Click', content, flags=re.DOTALL)
    
    with open(path, 'w', encoding='cp1252') as f:
        f.write(content)

fix_compras_cs(r'.\Vista\Compras\frmCompras.cs')
