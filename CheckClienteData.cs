using System;
using System.Data.SqlClient;

class Program {
    static void Main() {
        try {
            string cnStr = "Data source=LOWKIPC\\LOWK;Initial Catalog=MueblesKeyda;Integrated Security=true;";
            using (var cn = new SqlConnection(cnStr)) {
                cn.Open();
                using (var cmd = new SqlCommand("SELECT TOP 1 * FROM Cliente", cn)) {
                    using (var reader = cmd.ExecuteReader()) {
                        while (reader.Read()) {
                            Console.WriteLine(reader["Identificador1"].ToString() + " " + reader["Identificador2"].ToString());
                        }
                    }
                }
            }
        } catch (Exception e) { Console.WriteLine(e.Message); }
    }
}
