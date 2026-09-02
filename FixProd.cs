using System;
using System.IO;
using System.Text;
class Program {
    static void Main() {
        string file = @".\Modelo\Entidades\Producción.cs";
        string text = File.ReadAllText(file, Encoding.UTF8);
        text = text.Replace("reader[\"Mueble\"].ToString();", "reader[\"Producto\"].ToString();");
        File.WriteAllText(file, text, new UTF8Encoding(false));
    }
}
