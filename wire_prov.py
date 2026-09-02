import os

def wire_prov(path_cs, path_designer):
    with open(path_cs, 'r', encoding='cp1252') as f:
        content = f.read()
        
    methods = '''
        private int idProveedorSeleccionado = 0;
        
        private void dgvProveedor_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow fila = dgvProveedor.Rows[e.RowIndex];
                idProveedorSeleccionado = Convert.ToInt32(fila.Cells["IdProveedor"].Value);
                txtNombreProveedor.Text = fila.Cells["Nombre_Proveedor"].Value.ToString();
                txtTelefono.Text = fila.Cells["Telefono"].Value.ToString();
                txtCorreo.Text = fila.Cells["Correo"].Value.ToString();
                txtUbicacion.Text = fila.Cells["Ubicacion"].Value.ToString();
                
                btnEditar.Visible = true;
                btnGuardar.Visible = false;
            }
        }
        
        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (idProveedorSeleccionado == 0)
            {
                MessageBox.Show("Seleccione un proveedor para editar.");
                return;
            }
            
            DbProveedor proveedor = new DbProveedor();
            proveedor.IdProveedor1 = idProveedorSeleccionado;
            proveedor.Nombre_Proveedor1 = txtNombreProveedor.Text.Trim();
            proveedor.Telefono1 = txtTelefono.Text.Trim();
            proveedor.Correo1 = txtCorreo.Text.Trim();
            proveedor.Ubicaci\xf3n1 = txtUbicacion.Text.Trim();
            
            if (proveedor.ActualizarProveedor())
            {
                MessageBox.Show("Proveedor actualizado correctamente.");
                MostrarProveedor();
                Limpiar();
            }
            else
            {
                MessageBox.Show("Error al actualizar el proveedor.");
            }
        }
        
        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (idProveedorSeleccionado == 0)
            {
                MessageBox.Show("Seleccione un proveedor para eliminar.");
                return;
            }
            
            DialogResult res = MessageBox.Show("¿Está seguro de eliminar este proveedor?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (res == DialogResult.Yes)
            {
                DbProveedor proveedor = new DbProveedor();
                proveedor.IdProveedor1 = idProveedorSeleccionado;
                
                if (proveedor.EliminarProveedor())
                {
                    MessageBox.Show("Proveedor eliminado correctamente.");
                    MostrarProveedor();
                    Limpiar();
                }
                else
                {
                    MessageBox.Show("No se puede eliminar el proveedor porque tiene registros asociados.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        
        private void Limpiar()
        {
            txtNombreProveedor.Clear();
            txtTelefono.Clear();
            txtCorreo.Clear();
            txtUbicacion.Clear();
            idProveedorSeleccionado = 0;
            btnEditar.Visible = false;
            btnGuardar.Visible = true;
        }
'''
    content = content.replace('    }\n}\n', methods + '    }\n}\n')
    with open(path_cs, 'w', encoding='cp1252') as f:
        f.write(content)
        
    with open(path_designer, 'r', encoding='cp1252') as f:
        content = f.read()
        
    content = content.replace('this.btnEliminar.UseVisualStyleBackColor = false;', 'this.btnEliminar.UseVisualStyleBackColor = false;\n            this.btnEliminar.Click += new System.EventHandler(this.btnEliminar_Click);')
    content = content.replace('this.btnEditar.UseVisualStyleBackColor = false;', 'this.btnEditar.UseVisualStyleBackColor = false;\n            this.btnEditar.Click += new System.EventHandler(this.btnEditar_Click);')
    content = content.replace('this.dgvProveedor.TabIndex = 2;', 'this.dgvProveedor.TabIndex = 2;\n            this.dgvProveedor.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvProveedor_CellDoubleClick);')
    
    with open(path_designer, 'w', encoding='cp1252') as f:
        f.write(content)

wire_prov(r'.\Vista\Proveedores\frmProveedores.cs', r'.\Vista\Proveedores\frmProveedores.Designer.cs')
