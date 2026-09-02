import os
import re

def fix_pedidos(path):
    with open(path, 'r', encoding='cp1252') as f:
        content = f.read()

    # Fix dtp assignments in CellClick
    content = content.replace(
'''            if (!string.IsNullOrEmpty(colPedido) && row.Cells[colPedido].Value != DBNull.Value && row.Cells[colPedido].Value != null)
                dtpFechaDeEntrega.Value = Convert.ToDateTime(row.Cells[colPedido].Value);
                
            if (!string.IsNullOrEmpty(colEntrega) && row.Cells[colEntrega].Value != DBNull.Value && row.Cells[colEntrega].Value != null)
                dtpFechaPedido.Value = Convert.ToDateTime(row.Cells[colEntrega].Value);''',
'''            if (!string.IsNullOrEmpty(colPedido) && row.Cells[colPedido].Value != DBNull.Value && row.Cells[colPedido].Value != null)
                dtpFechaPedido.Value = Convert.ToDateTime(row.Cells[colPedido].Value);
                
            if (!string.IsNullOrEmpty(colEntrega) && row.Cells[colEntrega].Value != DBNull.Value && row.Cells[colEntrega].Value != null)
                dtpFechaDeEntrega.Value = Convert.ToDateTime(row.Cells[colEntrega].Value);'''
    )

    # Fix btnGuardar_Click fechaEntrega source
    content = content.replace(
'''                string nuevoEstado = cbEstado.Text;
                DateTime fechaEntrega = dtpFechaPedido.Value;''',
'''                string nuevoEstado = cbEstado.Text;
                DateTime fechaEntrega = dtpFechaDeEntrega.Value;'''
    )

    # Fix btnAgregar_Click
    old_agregar = '''            DataTable dt = (DataTable)dgvDetallesDePedido.DataSource;
            if (dt != null)
            {
                DataRow newRow = dt.NewRow();
                newRow["Mueble"] = txtMuebleaRealizar.Text;
                newRow["Cantidad"] = nudCantidad.Value;
                newRow["Medidas"] = medidaLargo + "x" + medidaAncho + "x" + medidaAlto;
                dt.Rows.Add(newRow);
            }'''
            
    new_agregar = '''            if (idPedidoSeleccionado <= 0)
            {
                MessageBox.Show("Seleccione un pedido primero.");
                return;
            }
            
            string medidas = medidaLargo + "x" + medidaAncho + "x" + medidaAlto;
            if (DetallePedidos.InsertarDetalle(idPedidoSeleccionado, txtMuebleaRealizar.Text, Convert.ToInt32(nudCantidad.Value), medidas))
            {
                MessageBox.Show("Detalle agregado correctamente.");
                dgvDetallesDePedido.DataSource = null;
                dgvDetallesDePedido.DataSource = DetallePedidos.CargarDetallesPorPedido(idPedidoSeleccionado);
            }
            else
            {
                MessageBox.Show("Error al agregar el detalle.");
            }'''

    content = content.replace(old_agregar, new_agregar)

    with open(path, 'w', encoding='cp1252') as f:
        f.write(content)

fix_pedidos(r'.\Vista\Pedidos\frmPedidos.cs')
