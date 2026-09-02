import os

def fix_file(path, replacements):
    with open(path, 'r', encoding='utf-8') as f:
        content = f.read()
        
    for old, new in replacements:
        content = content.replace(old, new)
        
    with open(path, 'w', encoding='utf-8-sig') as f:
        f.write(content)

# Fix Facturacion Designer
fix_file(r'.\Vista\Facturación\frmFacturacion.Designer.cs', [
    ('this.lblProductosFactura.Text = "N de Pedido:";', 'this.lblProductosFactura.Text = "N° de Venta:";'),
    ('this.lblProductosFactura.Text = "N° de Pedido:";', 'this.lblProductosFactura.Text = "N° de Venta:";'),
    ('this.label8.Text = "Fecha del Pedido :";', 'this.label8.Text = "Fecha de la Venta :";'),
    ('this.lblFactura = new System.Windows.Forms.Label();\n', 'this.lblFactura = new System.Windows.Forms.Label();\n            this.txtNumeroFactura.Visible = false;\n            this.lblFactura.Visible = false;\n')
])

# Fix Cotizacion
fix_file(r'.\Modelo\Entidades\Cotizacion.cs', [
    ('catch (Exception) { return false; }', 'catch (Exception) {\n                        return false;\n                    }')
])
