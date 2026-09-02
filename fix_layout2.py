import os
import glob

def add_dock_fill(path, name):
    with open(path, 'r', encoding='utf-8') as f:
        content = f.read()
    
    if f'this.{name}.Dock =' not in content:
        target = f'this.{name}.Size ='
        replacement = f'this.{name}.Dock = System.Windows.Forms.DockStyle.Fill;\n            {target}'
        content = content.replace(target, replacement)
        with open(path, 'w', encoding='utf-8') as f:
            f.write(content)

def add_anchor(path, name, anchor_val="((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)))"):
    with open(path, 'r', encoding='utf-8') as f:
        content = f.read()
    
    if f'this.{name}.Anchor =' not in content:
        target = f'this.{name}.Size ='
        replacement = f'this.{name}.Anchor = {anchor_val};\n            {target}'
        content = content.replace(target, replacement)
        with open(path, 'w', encoding='utf-8') as f:
            f.write(content)

for path in glob.glob('Vista/**/frmProduccion.Designer.cs', recursive=True) + glob.glob('Vista/**/frmProduccionSecretario.Designer.cs', recursive=True):
    add_dock_fill(path, 'pnlHeader')
    add_anchor(path, 'pnlContenedorTabla')
    add_anchor(path, 'dgvProduccion')
    add_anchor(path, 'btnEditar', "((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)))")
