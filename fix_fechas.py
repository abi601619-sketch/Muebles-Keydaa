import os

def fix_pedidos_fechas(path):
    with open(path, 'r', encoding='cp1252') as f:
        content = f.read()
        
    old_cell = '''        private void dgvPedidosRegistrados_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || ((DataGridView)sender).Rows[e.RowIndex].IsNewRow) return;
            DataGridViewRow row = dgvPedidosRegistrados.Rows[e.RowIndex];
            idPedidoSeleccionado = Convert.ToInt32(row.Cells["IdPedido"].Value);
            string estado = row.Cells["Estado"].Value?.ToString();
            comboBox1.Text = estado;
        }'''
        
    new_cell = '''        private void dgvPedidosRegistrados_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || ((DataGridView)sender).Rows[e.RowIndex].IsNewRow) return;
            DataGridViewRow row = dgvPedidosRegistrados.Rows[e.RowIndex];
            idPedidoSeleccionado = Convert.ToInt32(row.Cells["IdPedido"].Value);
            string estado = row.Cells["Estado"].Value?.ToString();
            comboBox1.Text = estado;
            
            // Buscar los nombres exactos de las columnas para las fechas
            string colPedido = "";
            string colEntrega = "";
            foreach (DataGridViewColumn col in dgvPedidosRegistrados.Columns)
            {
                if (col.Name.Contains("Fecha") && col.Name.Contains("Pedido")) colPedido = col.Name;
                if (col.Name.Contains("Fecha") && col.Name.Contains("Entrega")) colEntrega = col.Name;
            }
            
            if (!string.IsNullOrEmpty(colPedido) && row.Cells[colPedido].Value != DBNull.Value && row.Cells[colPedido].Value != null)
                dateTimePicker1.Value = Convert.ToDateTime(row.Cells[colPedido].Value);
                
            if (!string.IsNullOrEmpty(colEntrega) && row.Cells[colEntrega].Value != DBNull.Value && row.Cells[colEntrega].Value != null)
                dtpFechaEntrega.Value = Convert.ToDateTime(row.Cells[colEntrega].Value);
        }'''
        
    content = content.replace(old_cell, new_cell)
    
    with open(path, 'w', encoding='cp1252') as f:
        f.write(content)

fix_pedidos_fechas(r'.\Vista\Pedidos\frmPedidos.cs')
