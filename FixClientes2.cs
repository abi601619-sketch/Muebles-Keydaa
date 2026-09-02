using System.IO;
using System.Text;
using System;
using System.Collections.Generic;

class Program {
    static void Main() {
        string file = @".\Vista\Clientes\frmClientes.cs";
        string[] lines = File.ReadAllLines(file, Encoding.UTF8);
        List<string> newLines = new List<string>();
        
        newLines.Add("using Modelo.Conexión_DB;"); // Add one valid import
        
        foreach (string line in lines) {
            if (line.Contains("using Modelo.Conexi")) {
                continue; // Skip any line with Modelo.Conexi (mangled or not)
            }
            newLines.Add(line);
        }
        
        File.WriteAllLines(file, newLines.ToArray(), new UTF8Encoding(false));
    }
}
