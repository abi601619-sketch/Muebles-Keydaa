using System;
using System.Data.SqlClient;

class Program {
    static void Main() {
        try {
            string cnStr = "Data source=LOWKIPC\\LOWK;Initial Catalog=MueblesKeyda;Integrated Security=true;";
            using (var cn = new SqlConnection(cnStr)) {
                cn.Open();
                using (var cmd = new SqlCommand("SELECT TOP 1 * FROM VerVentas", cn)) {
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
