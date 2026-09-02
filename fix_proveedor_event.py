import os

def fix_proveedores(path):
    with open(path, 'r', encoding='cp1252') as f:
        content = f.read()
        
    search = 'this.dgvProveedor.RowTemplate.Height = 24;'
    insert = '\n            this.dgvProveedor.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvProveedor_CellDoubleClick);'
    
    if search in content and 'dgvProveedor_CellDoubleClick' not in content:
        content = content.replace(search, search + insert)
        with open(path, 'w', encoding='cp1252') as f:
            f.write(content)
            print("Fixed Proveedores")

fix_proveedores(r'.\Vista\Proveedores\frmProveedores.Designer.cs')
