import os
import re

def fix_frm_pedidos_ui(path):
    with open(path, 'r', encoding='cp1252') as f:
        content = f.read()

    # Cambiar cell click para cargar detalles
    old_cell = '''if (!string.IsNullOrEmpty(colEntrega) && row.Cells[colEntrega].Value != DBNull.Value && row.Cells[colEntrega].Value != null)
                dtpFechaEntrega.Value = Convert.ToDateTime(row.Cells[colEntrega].Value);
        }'''
    new_cell = '''if (!string.IsNullOrEmpty(colEntrega) && row.Cells[colEntrega].Value != DBNull.Value && row.Cells[colEntrega].Value != null)
                dtpFechaEntrega.Value = Convert.ToDateTime(row.Cells[colEntrega].Value);
                
            dgvDetallesDePedido.DataSource = null;
            dgvDetallesDePedido.DataSource = DetallePedidos.CargarDetallesPorPedido(idPedidoSeleccionado);
        }'''
    content = content.replace(old_cell, new_cell)
    
    # Cambiar btnAgregar_Click
    old_agregar = '''        private void btnAgregar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMuebleaRealizar.Text))
            {
                MessageBox.Show("Ingresa el nombre del mueble.");
                return;
            }
            if (medidaLargo == "0" && medidaAncho == "0" && medidaAlto == "0")
            {
                MessageBox.Show("Por favor ingresa las medidas del producto dando clic en 'Medidas del producto'.");
                return;
            }
            
            DataTable dt = (DataTable)dgvDetallesDePedido.DataSource;
            if (dt != null)
            {
                DataRow newRow = dt.NewRow();
                newRow["Mueble"] = txtMuebleaRealizar.Text;
                newRow["Cantidad"] = numericUpDown1.Value;
                newRow["Medidas"] = medidaLargo + "x" + medidaAncho + "x" + medidaAlto;
                dt.Rows.Add(newRow);
            }
        }'''
        
    new_agregar = '''        private void btnAgregar_Click(object sender, EventArgs e)
        {
            if (idPedidoSeleccionado == 0)
            {
                MessageBox.Show("Selecciona un pedido primero.");
                return;
            }
            if (string.IsNullOrWhiteSpace(txtMuebleaRealizar.Text))
            {
                MessageBox.Show("Ingresa el nombre del mueble.");
                return;
            }
            if (medidaLargo == "0" && medidaAncho == "0" && medidaAlto == "0")
            {
                MessageBox.Show("Por favor ingresa las medidas del producto dando clic en 'Medidas del producto'.");
                return;
            }
            
            string medidas = medidaLargo + "x" + medidaAncho + "x" + medidaAlto;
            if (DetallePedidos.InsertarDetalle(idPedidoSeleccionado, txtMuebleaRealizar.Text, Convert.ToInt32(numericUpDown1.Value), medidas))
            {
                MessageBox.Show("Mueble agregado al pedido.");
                dgvDetallesDePedido.DataSource = null;
                dgvDetallesDePedido.DataSource = DetallePedidos.CargarDetallesPorPedido(idPedidoSeleccionado);
                txtMuebleaRealizar.Text = "";
                numericUpDown1.Value = 1;
            }
            else
            {
                MessageBox.Show("Error al agregar mueble.");
            }
        }'''
        
    content = content.replace(old_agregar, new_agregar)

    with open(path, 'w', encoding='cp1252') as f:
        f.write(content)

fix_frm_pedidos_ui(r'.\Vista\Pedidos\frmPedidos.cs')
