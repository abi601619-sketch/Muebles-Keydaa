import os

def fix_detalle_pedidos(path):
    with open(path, 'r', encoding='cp1252') as f:
        content = f.read()
        
    insert = '''        public static DataTable CargarDetallesPorPedido(int idPedido)
        {
            SqlConnection conectar = Conexion.Conectar();
            string comando = "SELECT * FROM DetallePedido WHERE IdPedido = @IdPedido;";
            SqlDataAdapter adapter = new SqlDataAdapter(comando, conectar);
            adapter.SelectCommand.Parameters.AddWithValue("@IdPedido", idPedido);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            return dt;
        }

        public static bool InsertarDetalle(int idPedido, string mueble, int cantidad, string medidas)
        {
            using (SqlConnection conexion = Conexion.Conectar())
            {
                string query = "INSERT INTO DetallePedido (IdPedido, Mueble, Cantidad, Medidas) VALUES (@IdPedido, @Mueble, @Cantidad, @Medidas)";
                SqlCommand cmd = new SqlCommand(query, conexion);
                cmd.Parameters.AddWithValue("@IdPedido", idPedido);
                cmd.Parameters.AddWithValue("@Mueble", mueble);
                cmd.Parameters.AddWithValue("@Cantidad", cantidad);
                cmd.Parameters.AddWithValue("@Medidas", medidas);
                try {
                    return cmd.ExecuteNonQuery() > 0;
                } catch {
                    return false;
                }
            }
        }
'''
    content = content.replace('public static DataTable CargarDetallesPedidos()', insert + '        public static DataTable CargarDetallesPedidos()')
    
    with open(path, 'w', encoding='cp1252') as f:
        f.write(content)

fix_detalle_pedidos(r'.\Modelo\Entidades\DetallePedidos.cs')
