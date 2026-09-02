import os

def fix_file(path):
    with open(path, 'r', encoding='cp1252') as f:
        content = f.read()
        
    content = content.replace('this.lblProductosFactura.Text = "N de Pedido:";', 'this.lblProductosFactura.Text = "N de Venta:";')
    content = content.replace('this.lblProductosFactura.Text = "N de Pedido:";', 'this.lblProductosFactura.Text = "N de Venta:";')
    content = content.replace('this.label8.Text = "Fecha del Pedido :";', 'this.label8.Text = "Fecha de la Venta :";')
    content = content.replace('this.btnBuscarPedido.Text = "Buscar Pedido";', 'this.btnBuscarPedido.Text = "Buscar Venta";')
    content = content.replace('this.lblFactura = new System.Windows.Forms.Label();\n', 'this.lblFactura = new System.Windows.Forms.Label();\n            this.txtNumeroFactura.Visible = false;\n            this.lblFactura.Visible = false;\n')

    with open(path, 'w', encoding='cp1252') as f:
        f.write(content)

fix_file(r'.\Vista\Facturación\frmFacturacion.Designer.cs')
