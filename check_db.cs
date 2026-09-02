using System;
using System.Data;
using System.Data.SqlClient;

class Program {
    static void Main() {
        try {
            string conString = "Server=.;Database=MueblesKeyda;Integrated Security=True;";
            using (SqlConnection con = new SqlConnection(conString)) {
                con.Open();
                using (SqlCommand cmd = new SqlCommand("SELECT TOP 1 * FROM VerPedido", con)) {
                    using (SqlDataReader reader = cmd.ExecuteReader()) {
                        for(int i=0; i<reader.FieldCount; i++) {
                            Console.WriteLine(reader.GetName(i));
                        }
                    }
                }
            }
        } catch { Console.WriteLine("Could not connect"); }
    }
}
