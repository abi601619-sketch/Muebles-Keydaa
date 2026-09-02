import os
import re

def fix_ventas(path):
    with open(path, 'r', encoding='cp1252') as f:
        content = f.read()

    new_code = '''
        int idVentaSeleccionada = 0;
        private void dgvVentas_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || dgvVentas.Rows[e.RowIndex].IsNewRow) return;
            DataGridViewRow row = dgvVentas.Rows[e.RowIndex];
            idVentaSeleccionada = Convert.ToInt32(row.Cells["IdVenta"].Value);
            
            cbCliente.Text = row.Cells["Cliente"]?.Value?.ToString() ?? "";
            cbMetodoPago.Text = row.Cells["MetodoDePago"]?.Value?.ToString() ?? "";
            
            if (row.Cells["FechaVenta"]?.Value != DBNull.Value)
                dtFechaVenta.Value = Convert.ToDateTime(row.Cells["FechaVenta"].Value);
                
            txtSubTotal.Text = row.Cells["SubTotal"]?.Value?.ToString() ?? "0";
            btnEditar.Visible = true;
            btnGuardar.Visible = false;
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (idVentaSeleccionada == 0)
            {
                MessageBox.Show("Seleccione una venta para editar.");
                return;
            }
            if (cbCliente.SelectedIndex == -1 || cbMetodoPago.SelectedIndex == -1)
            {
                MessageBox.Show("Seleccione cliente y mtodo de pago.");
                return;
            }

            int idCliente = Convert.ToInt32(cbCliente.SelectedValue);
            int idMetodoPago = Convert.ToInt32(cbMetodoPago.SelectedValue);
            decimal subtotal = Convert.ToDecimal(txtSubTotal.Text);
            
            DbVentas venta = new DbVentas(idVentaSeleccionada, dtFechaVenta.Value, idCliente, idMetodoPago, subtotal);
            if (venta.ActualizarVenta())
            {
                MessageBox.Show("Venta actualizada correctamente.");
                MostrarVentas();
                btnEditar.Visible = true;
                btnGuardar.Visible = true;
                idVentaSeleccionada = 0;
            }
            else
            {
                MessageBox.Show("Error al actualizar la venta.");
            }
        }
'''
    
    # insert before private void CalcularTotal()
    content = content.replace('        private void CalcularTotal()', new_code + '\n        private void CalcularTotal()')

    with open(path, 'w', encoding='cp1252') as f:
        f.write(content)

fix_ventas(r'.\Vista\Ventas\frmVentas.cs')

def wire_ventas_designer(path):
    with open(path, 'r', encoding='cp1252') as f:
        content = f.read()
        
    content = content.replace('this.btnEditar.UseVisualStyleBackColor = false;', 'this.btnEditar.UseVisualStyleBackColor = false;\n            this.btnEditar.Click += new System.EventHandler(this.btnEditar_Click);')
    content = content.replace('this.dgvVentas.SelectionChanged += new System.EventHandler(this.dgvVentas_SelectionChanged);', 'this.dgvVentas.SelectionChanged += new System.EventHandler(this.dgvVentas_SelectionChanged);\n            this.dgvVentas.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvVentas_CellDoubleClick);')
    
    with open(path, 'w', encoding='cp1252') as f:
        f.write(content)

wire_ventas_designer(r'.\Vista\Ventas\frmVentas.Designer.cs')
