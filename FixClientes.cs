using System.IO;
using System.Text;
using System;
using System.Collections.Generic;

class Program {
    static void Main() {
        string file = @".\Vista\Clientes\frmClientes.cs";
        string text = File.ReadAllText(file, Encoding.UTF8);
        
        // Remove ALL mangled usings and correct them
        text = text.Replace("using Modelo.Conexian_DB;", "");
        text = text.Replace("using Modelo.Conexin_DB;", "");
        text = text.Replace("using Modelo.Conexión_DB;", "");
        
        // Add just ONE proper using at the top
        text = "using Modelo.Conexión_DB;\r\n" + text.TrimStart();
        
        File.WriteAllText(file, text, new UTF8Encoding(false));
    }
}
