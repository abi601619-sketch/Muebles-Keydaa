import os

def fix_ventas_designer(path):
    with open(path, 'r', encoding='cp1252') as f:
        content = f.read()
        
    content = content.replace('this.dgvVentasRegistradas.TabIndex = 2;\n            this.dgvVentasRegistradas.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvVentas_CellDoubleClick);', 'this.dgvVentasRegistradas.TabIndex = 2;')
    
    with open(path, 'w', encoding='cp1252') as f:
        f.write(content)

fix_ventas_designer(r'.\Vista\Ventas\frmVentas.Designer.cs')
