using System;
using System.IO;

class Program {
    static void Main() {
        string file = @".\Vista\Facturación\frmFacturacion.Designer.cs";
        string text = File.ReadAllText(file);
        
        text = text.Replace("this.lblProductosFactura.Text = \""N de Pedido:\"";", "this.lblProductosFactura.Text = \""N de Venta:\"";");
        text = text.Replace("this.label8.Text = \""Fecha del Pedido :\"";", "this.label8.Text = \""Fecha de la Venta :\"";");
        text = text.Replace("this.btnBuscarPedido.Text = \""Buscar Pedido\"";", "this.btnBuscarPedido.Text = \""Buscar Venta\"";");
        text = text.Replace("this.lblFactura = new System.Windows.Forms.Label();\r\n", "this.lblFactura = new System.Windows.Forms.Label();\r\n            this.txtNumeroFactura.Visible = false;\r\n            this.lblFactura.Visible = false;\r\n");
        
        File.WriteAllText(file, text, new System.Text.UTF8Encoding(true));
    }
}
