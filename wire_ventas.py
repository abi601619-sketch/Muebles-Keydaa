import os

def wire_ventas_del(path_cs, path_designer):
    with open(path_cs, 'r', encoding='cp1252') as f:
        content = f.read()
        
    methods = '''
        private int idVentaSeleccionada = 0;
        
        private void dgvVentas_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow fila = dgvVentasRegistradas.Rows[e.RowIndex];
                idVentaSeleccionada = Convert.ToInt32(fila.Cells["IdVenta"].Value);
            }
        }
        
        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (idVentaSeleccionada == 0)
            {
                MessageBox.Show("Seleccione una venta dando doble clic en la tabla.");
                return;
            }
            
            DialogResult res = MessageBox.Show("¿Está seguro de eliminar esta venta? Se eliminarán todos los detalles y facturas asociados de forma permanente.", "Confirmar Eliminación (Cascada)", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (res == DialogResult.Yes)
            {
                Modelo.Entidades.Ventas venta = new Modelo.Entidades.Ventas();
                venta.IdVenta = idVentaSeleccionada;
                
                if (venta.EliminarVenta())
                {
                    MessageBox.Show("Venta eliminada correctamente.");
                    MostrarVentas();
                    idVentaSeleccionada = 0;
                }
                else
                {
                    MessageBox.Show("Error al eliminar la venta.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
'''
    content = content.replace('    }\n}\n', methods + '    }\n}\n')
    with open(path_cs, 'w', encoding='cp1252') as f:
        f.write(content)
        
    with open(path_designer, 'r', encoding='cp1252') as f:
        content = f.read()
        
    content = content.replace('this.btnEliminar.UseVisualStyleBackColor = false;', 'this.btnEliminar.UseVisualStyleBackColor = false;\n            this.btnEliminar.Click += new System.EventHandler(this.btnEliminar_Click);')
    content = content.replace('this.dgvVentasRegistradas.TabIndex = 2;', 'this.dgvVentasRegistradas.TabIndex = 2;\n            this.dgvVentasRegistradas.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvVentas_CellDoubleClick);')

    with open(path_designer, 'w', encoding='cp1252') as f:
        f.write(content)

wire_ventas_del(r'.\Vista\Ventas\frmVentas.cs', r'.\Vista\Ventas\frmVentas.Designer.cs')
