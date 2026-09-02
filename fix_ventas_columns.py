import os
import re

def fix_ventas_columns(path):
    with open(path, 'r', encoding='cp1252') as f:
        content = f.read()

    old_code = '''            cbCliente.Text = row.Cells["Cliente"]?.Value?.ToString() ?? "";
            cbMetodoPago.Text = row.Cells["MetodoDePago"]?.Value?.ToString() ?? "";
            
            if (row.Cells["FechaVenta"]?.Value != DBNull.Value)
                dtFechaVenta.Value = Convert.ToDateTime(row.Cells["FechaVenta"].Value);'''
                
    new_code = '''            
            // Buscar los nombres exactos de las columnas en el grid
            string colCliente = "";
            string colMetodo = "";
            string colFecha = "";
            foreach (DataGridViewColumn col in dgvVentas.Columns)
            {
                if (col.Name.Contains("Cliente")) colCliente = col.Name;
                if (col.Name.Contains("Metodo") || col.Name.Contains("Pago")) colMetodo = col.Name;
                if (col.Name.Contains("Fecha")) colFecha = col.Name;
            }

            if (!string.IsNullOrEmpty(colCliente)) cbCliente.Text = row.Cells[colCliente]?.Value?.ToString() ?? "";
            if (!string.IsNullOrEmpty(colMetodo)) cbMetodoPago.Text = row.Cells[colMetodo]?.Value?.ToString() ?? "";
            
            if (!string.IsNullOrEmpty(colFecha) && row.Cells[colFecha]?.Value != DBNull.Value)
                dtFechaVenta.Value = Convert.ToDateTime(row.Cells[colFecha].Value);'''

    content = content.replace(old_code, new_code)

    with open(path, 'w', encoding='cp1252') as f:
        f.write(content)

fix_ventas_columns(r'.\Vista\Ventas\frmVentas.cs')
