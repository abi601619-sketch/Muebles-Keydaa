using System;
using System.IO;
using System.Text;
class Program {
    static void Main() {
        string file = @".\Modelo\Entidades\Cotizacion.cs";
        string text = File.ReadAllText(file, Encoding.UTF8);
        text = text.Replace("catch (Exception e) { System.Windows.Forms.MessageBox.Show(e.Message);\r\n                    {\r\n                        return false;\r\n                    }", "catch (Exception) { return false; }");
        File.WriteAllText(file, text, new UTF8Encoding(false));
    }
}
