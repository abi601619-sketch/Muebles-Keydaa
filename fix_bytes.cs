using System;
using System.IO;

class Program {
    static void Main() {
        string path = @".\Modelo\Entidades\Proveedor.cs";
        byte[] bytes = File.ReadAllBytes(path);
        
        using (MemoryStream ms = new MemoryStream()) {
            for(int i = 0; i < bytes.Length; i++) {
                if (bytes[i] == 0xC3 && i+1 < bytes.Length && bytes[i+1] == 0xB3) {
                    ms.WriteByte(0xF3);
                    i++;
                } else if (bytes[i] == 0xEF && i+2 < bytes.Length && bytes[i+1] == 0xBF && bytes[i+2] == 0xBD) {
                    ms.WriteByte(0xF3);
                    i+=2;
                } else {
                    ms.WriteByte(bytes[i]);
                }
            }
            File.WriteAllBytes(path, ms.ToArray());
        }
        
        path = @".\Vista\Inventario\frmInventario.cs";
        bytes = File.ReadAllBytes(path);
        
        using (MemoryStream ms = new MemoryStream()) {
            for(int i = 0; i < bytes.Length; i++) {
                if (bytes[i] == 0xC3 && i+1 < bytes.Length && bytes[i+1] == 0xB3) {
                    ms.WriteByte(0xF3);
                    i++;
                } else if (bytes[i] == 0xEF && i+2 < bytes.Length && bytes[i+1] == 0xBF && bytes[i+2] == 0xBD) {
                    ms.WriteByte(0xF3);
                    i+=2;
                } else if (bytes[i] == 0xC3 && i+1 < bytes.Length && bytes[i+1] == 0xBF) {
                    ms.WriteByte(0xF3);
                    i++;
                } else {
                    ms.WriteByte(bytes[i]);
                }
            }
            File.WriteAllBytes(path, ms.ToArray());
        }
    }
}
