import os

def fix_inventario(path):
    with open(path, 'r', encoding='cp1252') as f:
        content = f.read()
        
    old_bloquear = '''        private void BloquearCampos()
        {
            txtMaterial.ReadOnly = true;
            cbUnidadMedida.Enabled = true;
            cbCategorias.Enabled = true;
            txtCantidad.ReadOnly=true;'''
            
    new_bloquear = '''        private void BloquearCampos()
        {
            txtMaterial.ReadOnly = true;
            cbUnidadMedida.Enabled = false;
            cbCategorias.Enabled = false;
            txtCantidad.ReadOnly = true;'''
            
    old_habilitar = '''        private void HabilitarCampos()
        {
            txtMaterial.ReadOnly = false;
            cbUnidadMedida.Enabled = false;
            cbCategorias.Enabled = false;
            txtCantidad.ReadOnly=false;'''
            
    new_habilitar = '''        private void HabilitarCampos()
        {
            txtMaterial.ReadOnly = false;
            cbUnidadMedida.Enabled = true;
            cbCategorias.Enabled = true;
            txtCantidad.ReadOnly = false;'''
            
    content = content.replace(old_bloquear, new_bloquear)
    content = content.replace(old_habilitar, new_habilitar)
    
    with open(path, 'w', encoding='cp1252') as f:
        f.write(content)

fix_inventario(r'.\Vista\Inventario\frmInventario.cs')
