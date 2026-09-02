import os

def fix_compras(path):
    with open(path, 'r', encoding='cp1252') as f:
        content = f.read()
        
    method = '''
        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (idCompraSeleccionada == 0)
            {
                MessageBox.Show("Seleccione una compra dando doble clic en la tabla inferior.");
                return;
            }
            
            DialogResult res = MessageBox.Show("¿Está seguro de eliminar esta compra permanentemente? Se eliminarán también sus detalles.", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (res == DialogResult.Yes)
            {
                Modelo.Entidades.DbCompras compra = new Modelo.Entidades.DbCompras();
                compra.IdCompra1 = idCompraSeleccionada;
                if (compra.EliminarCompra())
                {
                    MessageBox.Show("Compra eliminada correctamente.");
                    MostrarRegistros();
                    idCompraSeleccionada = 0;
                }
                else
                {
                    MessageBox.Show("Error al eliminar la compra.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
'''
    content = content.replace('    }\n\n}\n', method + '    }\n\n}\n')
    
    with open(path, 'w', encoding='cp1252') as f:
        f.write(content)

def fix_ventas(path):
    with open(path, 'r', encoding='cp1252') as f:
        content = f.read()
        
    method = '''
        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dgvVentas.CurrentRow == null)
            {
                MessageBox.Show("Seleccione una venta de la tabla.");
                return;
            }
            
            int id = Convert.ToInt32(dgvVentas.CurrentRow.Cells["IdVenta"].Value);
            
            DialogResult res = MessageBox.Show("¿Está seguro de eliminar esta venta? Se eliminarán todos los detalles y facturas asociados de forma permanente.", "Confirmar Eliminación (Cascada)", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (res == DialogResult.Yes)
            {
                Modelo.Entidades.Ventas venta = new Modelo.Entidades.Ventas();
                venta.IdVenta = id;
                if (venta.EliminarVenta())
                {
                    MessageBox.Show("Venta eliminada correctamente.");
                    MostrarVentas();
                }
                else
                {
                    MessageBox.Show("Error al eliminar la venta.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
'''
    # Find the end of class in Ventas
    #     }\r\n}\r\n or similar
    if '    }\n}\n' in content:
        content = content.replace('    }\n}\n', method + '    }\n}\n')
    elif '    }\n\n}\n' in content:
        content = content.replace('    }\n\n}\n', method + '    }\n\n}\n')
    else:
        # manual append
        content = content[:content.rfind('}')]
        content = content[:content.rfind('}')]
        content = content + method + '    }\n}\n'

    with open(path, 'w', encoding='cp1252') as f:
        f.write(content)

fix_compras(r'.\Vista\Compras\frmCompras.cs')
fix_ventas(r'.\Vista\Ventas\frmVentas.cs')
