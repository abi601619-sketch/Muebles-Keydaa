using System;
using System.Data.SqlClient;

class Program {
    static void Main() {
        try {
            string cnStr = "Data source=LOWKIPC\\LOWK;Initial Catalog=MueblesKeyda;Integrated Security=true;";
            using (var cn = new SqlConnection(cnStr)) {
                cn.Open();
                using (var cmd = new SqlCommand("SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE = 'BASE TABLE'", cn)) {
                    using (var reader = cmd.ExecuteReader()) {
                        while (reader.Read()) {
                            Console.WriteLine(reader[0]);
                        }
                    }
                }
            }
        } catch (Exception e) { Console.WriteLine(e.Message); }
    }
}
