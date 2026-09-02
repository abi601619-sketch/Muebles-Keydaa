using System;
using System.IO;
using System.Text;
class Program {
    static void Main() {
        string file = @".\Vista\Facturación\frmFacturacion.Designer.cs";
        string text = File.ReadAllText(file, Encoding.UTF8);
        
        text = text.Replace("this.label1.Text = \"N° FACTURA:\";", "this.label1.Text = \"N° FACTURA:\";\r\n            this.txtNumeroFactura.Visible = false;\r\n            this.label1.Visible = false;");
        text = text.Replace("this.label5.Text = \"N° de Pedido:\";", "this.label5.Text = \"N° de Venta:\";");
        text = text.Replace("this.btnBuscarPedido.Text = \"Buscar Pedido\";", "this.btnBuscarPedido.Text = \"Buscar Venta\";");
        
        File.WriteAllText(file, text, new UTF8Encoding(false));
    }
}
