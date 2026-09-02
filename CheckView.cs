using System;
using System.Data;
using System.Data.SqlClient;
using Modelo.Conexión_DB;

class Program {
    static void Main() {
        try {
            using (var cn = Conexion.Conectar()) {
                using (var cmd = new SqlCommand("SELECT TOP 1 * FROM VerMaterial", cn)) {
                    using (var reader = cmd.ExecuteReader()) {
                        for (int i=0; i<reader.FieldCount; i++) {
                            Console.WriteLine(reader.GetName(i));
                        }
                    }
                }
            }
        } catch (Exception e) { Console.WriteLine(e.Message); }
    }
}
