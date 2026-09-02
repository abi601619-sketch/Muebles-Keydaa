using System;
using System.IO;
using System.Text;

class Program {
    static void Main() {
        byte[] bytes = File.ReadAllBytes(@".\Modelo\Entidades\Proveedor.cs");
        for(int i = 0; i < bytes.Length; i++) {
            if (bytes[i] > 127) {
                Console.WriteLine("Pos {0}: {1:X2}", i, bytes[i]);
            }
        }
    }
}
