import os

def fix_dgv_autosize(path):
    with open(path, 'r', encoding='cp1252') as f:
        content = f.read()
        
    content = content.replace('this.dgvClientesIndividuales.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;', 'this.dgvClientesIndividuales.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;')
    content = content.replace('this.dgvClientesCorporativos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;', 'this.dgvClientesCorporativos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;')
    
    with open(path, 'w', encoding='cp1252') as f:
        f.write(content)

fix_dgv_autosize(r'.\Vista\Clientes\frmClientes.Designer.cs')
