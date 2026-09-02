import os

def update_click(path):
    with open(path, 'r', encoding='cp1252') as f:
        content = f.read()
        
    old_code = '''        private void btnBuscarPedido_Click(object sender, EventArgs e)
        {
            if (int.TryParse(txtProductosCotizadosFacturados.Text, out int idVenta))
            {
                // Buscar la venta
                System.Data.DataTable dt = Modelo.Entidades.DbFactura.CargarDetalleFacturas(idVenta);'''
                
    new_code = '''        private void btnBuscarPedido_Click(object sender, EventArgs e)
        {
            if (cbVentas.SelectedValue != null && int.TryParse(cbVentas.SelectedValue.toString(), out int idVenta))
            {
                // Buscar la venta
                System.Data.DataTable dt = Modelo.Entidades.DbFactura.CargarDetalleFacturas(idVenta);'''
                
    content = content.replace(old_code, new_code.replace('.toString()', '.ToString()'))
    
    with open(path, 'w', encoding='cp1252') as f:
        f.write(content)

update_click(r'.\Vista\Facturación\frmFacturacion.cs')
