using System;
using System.IO;
using System.Text;

class Program {
    static void Main() {
        byte[] bytes = File.ReadAllBytes(@".\Modelo\Entidades\Proveedor.cs");
        string s = Encoding.GetEncoding("Windows-1252").GetString(bytes);
        int idx = s.IndexOf("Ubicaci");
        if (idx > 0) {
            for (int i=idx; i<idx+12; i++) {
                Console.Write("{0:X2} ", bytes[i]);
            }
        }
    }
}
