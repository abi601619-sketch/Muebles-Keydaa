import os
import glob
import re

def add_dock(content, name, dock_val):
    if f'this.{name}.Dock =' not in content:
        target = f'this.{name}.Size ='
        if target in content:
            replacement = f'this.{name}.Dock = {dock_val};\n            {target}'
            content = content.replace(target, replacement)
    return content

def add_anchor(content, name, anchor_val):
    if f'this.{name}.Anchor =' not in content:
        target = f'this.{name}.Size ='
        if target in content:
            replacement = f'this.{name}.Anchor = {anchor_val};\n            {target}'
            content = content.replace(target, replacement)
    return content

def is_control_in_file(content, name):
    return f'this.{name}.Name = "{name}"' in content or f'this.{name} = new ' in content

def fix_all():
    files = glob.glob('Vista/**/*.Designer.cs', recursive=True)
    for path in files:
        if 'frmLogin' in path or 'frmDashboard' in path or 'frmInicio' in path:
            continue
            
        with open(path, 'r', encoding='cp1252') as f:
            content = f.read()

        original = content
        
        # 1. Dock pnlHeader
        if is_control_in_file(content, 'pnlHeader'):
            content = add_dock(content, 'pnlHeader', 'System.Windows.Forms.DockStyle.Fill')

        # 2. Main content panels (Grid containers) -> Top, Bottom, Left, Right
        for pnl in ['pnlContenedorTabla', 'pnlTablaContenido']:
            if is_control_in_file(content, pnl):
                content = add_anchor(content, pnl, '((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)))')
        
        # 3. Sidebar panels -> Top, Bottom, Right
        for pnl in ['pnlPedidaDeDatos']:
            if is_control_in_file(content, pnl):
                content = add_anchor(content, pnl, '((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Right)))')

        # 4. DataGridViews -> Top, Bottom, Left, Right
        grids = re.findall(r'this\.(dgv[a-zA-Z0-9_]+) = new System\.Windows\.Forms\.DataGridView\(\)', content)
        for dgv in grids:
            content = add_anchor(content, dgv, '((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)))')

        # 5. Buttons below grids -> Bottom, Left
        for btn in ['btnEditar', 'btnLimpiar', 'btnGuardarCambios', 'btnCancelar']:
            if is_control_in_file(content, btn):
                # Only anchor bottom left if they are below the grid. But some might be in the sidebar!
                # If they are in the sidebar (pnlPedidaDeDatos), we shouldn't change their anchor, or maybe BottomRight.
                # Actually, let's just anchor btnEditar and btnLimpiar to Bottom, Left.
                pass
                
        if content != original:
            with open(path, 'w', encoding='cp1252') as f:
                f.write(content)
            print(f'Fixed {path}')

fix_all()
