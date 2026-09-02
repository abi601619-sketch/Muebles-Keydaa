using System;
using System.IO;

class Program {
    static void Main() {
        string file = @".\Modelo\Entidades\Material.cs";
        string content = File.ReadAllText(file, System.Text.Encoding.GetEncoding("Windows-1252"));
        
        string method = @"
        public bool EliminarMaterial()
        {
            SqlConnection conectar = Conexion.Conectar();
            string comando = "DELETE FROM Material WHERE IdMaterial = @IdMaterial;";
            SqlCommand cmd = new SqlCommand(comando, conectar);
            cmd.Parameters.AddWithValue("@IdMaterial", idMaterial1);
            
            try {
                return cmd.ExecuteNonQuery() > 0;
            } catch (Exception) {
                return false;
            } finally {
                conectar.Close();
            }
        }
";
        
        content = content.Replace("public bool ActualizarMaterial()", method + "public bool ActualizarMaterial()");
        File.WriteAllText(file, content, System.Text.Encoding.GetEncoding("Windows-1252"));
    }
}
