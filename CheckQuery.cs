using System;
using System.Data.SqlClient;

class Program {
    static void Main() {
        try {
            string cnStr = "Data source=LOWKIPC\\LOWK;Initial Catalog=MueblesKeyda;Integrated Security=true;";
            using (var cn = new SqlConnection(cnStr)) {
                cn.Open();
                using (var cmd = new SqlCommand("SELECT v.IdVenta, CAST(v.IdVenta AS VARCHAR) + ' - ' + c.Identificador1 + ' ' + ISNULL(c.Identificador2, '') AS Display FROM Venta v INNER JOIN Cliente c ON v.IdCliente = c.IdCliente LEFT JOIN Factura f ON v.IdVenta = f.IdVenta WHERE f.IdFactura IS NULL;", cn)) {
                    using (var reader = cmd.ExecuteReader()) {
                        while (reader.Read()) {
                            Console.WriteLine(reader["Display"].ToString());
                        }
                    }
                }
            }
        } catch (Exception e) { Console.WriteLine(e.Message); }
    }
}
