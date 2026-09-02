import os

def update_guardar(path):
    with open(path, 'r', encoding='cp1252') as f:
        content = f.read()
        
    old_code = '''                    nuevaFactura.InsertarFactura();
                    MessageBox.Show("Factura guardada correctamente.");
                    // Limpiar UI'''
                    
    new_code = '''                    nuevaFactura.InsertarFactura();
                    MessageBox.Show("Factura guardada correctamente.");
                    MostrarRegistrosFacturas();
                    System.Data.DataTable dtVentas = Modelo.Entidades.DbFactura.CargarVentasParaFactura();
                    cbVentas.DataSource = dtVentas;
                    cbVentas.DisplayMember = "Display";
                    cbVentas.ValueMember = "IdVenta";
                    // Limpiar UI'''
                    
    content = content.replace(old_code, new_code)
    
    with open(path, 'w', encoding='cp1252') as f:
        f.write(content)

update_guardar(r'.\Vista\Facturación\frmFacturacion.cs')
