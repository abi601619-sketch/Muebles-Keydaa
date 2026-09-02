import os

def update_load(path):
    with open(path, 'r', encoding='cp1252') as f:
        content = f.read()
        
    new_load = '''        private void frmFacturacion_Load(object sender, EventArgs e)
        {
            MostrarRegistrosFacturas();
            MostrarDetalleFactura();
            
            System.Data.DataTable dtVentas = Modelo.Entidades.DbFactura.CargarVentasParaFactura();
            cbVentas.DataSource = dtVentas;
            cbVentas.DisplayMember = "Display";
            cbVentas.ValueMember = "IdVenta";
        }'''
        
    content = content.replace('        private void frmFacturacion_Load(object sender, EventArgs e)\n        {\n            MostrarRegistrosFacturas();\n            MostrarDetalleFactura();\n        }', new_load)
    
    with open(path, 'w', encoding='cp1252') as f:
        f.write(content)

update_load(r'.\Vista\Facturación\frmFacturacion.cs')
