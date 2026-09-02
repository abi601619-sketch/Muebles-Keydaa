using System.IO;
using System.Text;
using System;

class Program {
    static void Main() {
        string file = @".\Vista\Categorías\frmCategorias.cs";
        string text = File.ReadAllText(file, Encoding.UTF8);
        File.WriteAllText(file, text, new UTF8Encoding(false));
    }
}
