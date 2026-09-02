import os

def wire_delete(path_cs, path_designer):
    with open(path_cs, 'r', encoding='cp1252') as f:
        content = f.read()
        
    method = '''
        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (idMaterialSeleccionado == 0)
            {
                MessageBox.Show("Seleccione un material primero.");
                return;
            }
            DialogResult resultado = MessageBox.Show("¿Está seguro de eliminar este material?", "Confirmar eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (resultado == DialogResult.Yes)
            {
                Material material = new Material();
                material.idMaterial1 = idMaterialSeleccionado;
                if (material.EliminarMaterial())
                {
                    MessageBox.Show("Material eliminado correctamente.");
                    MostrarInventario();
                    
                    idMaterialSeleccionado = 0;
                    txtMaterial.Clear();
                    txtCantidad.Clear();
                    cbUnidadMedida.SelectedIndex = -1;
                    cbCategorias.SelectedIndex = -1;
                }
                else
                {
                    MessageBox.Show("No se puede eliminar el material porque está siendo utilizado en producciones.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
'''
    content = content.replace('        private void btnGuardar_Click(object sender, EventArgs e)', method + '        private void btnGuardar_Click(object sender, EventArgs e)')
    with open(path_cs, 'w', encoding='cp1252') as f:
        f.write(content)
        
    with open(path_designer, 'r', encoding='cp1252') as f:
        content = f.read()
        
    content = content.replace('this.btnEliminar.UseVisualStyleBackColor = false;', 'this.btnEliminar.UseVisualStyleBackColor = false;\n            this.btnEliminar.Click += new System.EventHandler(this.btnEliminar_Click);')
    with open(path_designer, 'w', encoding='cp1252') as f:
        f.write(content)

wire_delete(r'.\Vista\Inventario\frmInventario.cs', r'.\Vista\Inventario\frmInventario.Designer.cs')
