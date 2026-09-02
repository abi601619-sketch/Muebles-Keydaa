import os
import re

def fix_compras_des(path):
    with open(path, 'r', encoding='cp1252') as f:
        content = f.read()
        
    content = content.replace('this.dgvComprasRegistradas.TabIndex = 2;\n            this.dgvComprasRegistradas.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvComprasRegistradas_CellDoubleClick);', 'this.dgvComprasRegistradas.TabIndex = 2;')
    
    with open(path, 'w', encoding='cp1252') as f:
        f.write(content)

fix_compras_des(r'.\Vista\Compras\frmCompras.Designer.cs')
