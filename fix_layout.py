import os

def add_dock_fill(path, name):
    with open(path, 'r', encoding='utf-8') as f:
        content = f.read()
    
    # Check if already has Dock
    if f'this.{name}.Dock =' not in content:
        # Find where Size is set and inject Dock right after
        target = f'this.{name}.Size ='
        replacement = f'this.{name}.Dock = System.Windows.Forms.DockStyle.Fill;\n            {target}'
        content = content.replace(target, replacement)
        
        with open(path, 'w', encoding='utf-8') as f:
            f.write(content)

def add_anchor(path, name, anchor_val="((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)))"):
    with open(path, 'r', encoding='utf-8') as f:
        content = f.read()
    
    # Check if already has Anchor
    if f'this.{name}.Anchor =' not in content:
        target = f'this.{name}.Size ='
        replacement = f'this.{name}.Anchor = {anchor_val};\n            {target}'
        content = content.replace(target, replacement)
        
        with open(path, 'w', encoding='utf-8') as f:
            f.write(content)

def fix_form(path):
    add_dock_fill(path, 'pnlHeader')
    add_anchor(path, 'pnlContenedorTabla')
    add_anchor(path, 'dgvProduccion')
    
    # The Edit button is below the DataGridView inside pnlContenedorTabla.
    # It should anchor to Bottom, Left so it stays below the grid.
    add_anchor(path, 'btnEditar', "((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)))")
    add_anchor(path, 'btnGuardar', "((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)))") # for secretarios if any

fix_form(r'Vista\Produccin\frmProduccion.Designer.cs')
fix_form(r'Vista\Produccion Secretario\frmProduccionSecretario.Designer.cs')
