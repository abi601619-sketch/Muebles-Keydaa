import os

def fix_pedidos_update(path):
    with open(path, 'r', encoding='cp1252') as f:
        content = f.read()
        
    old_meth = '''public static bool ActualizarEstadoPedido(int idPedido, string nuevoEstado)
        {
            using (System.Data.SqlClient.SqlConnection conexion = Conexion.Conectar())
            {
                string query = "UPDATE Pedido SET Estado = @Estado WHERE IdPedido = @IdPedido";
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand(query, conexion);
                cmd.Parameters.AddWithValue("@Estado", nuevoEstado);
                cmd.Parameters.AddWithValue("@IdPedido", idPedido);'''
                
    new_meth = '''public static bool ActualizarPedido(int idPedido, string nuevoEstado, DateTime fechaEntrega)
        {
            using (System.Data.SqlClient.SqlConnection conexion = Conexion.Conectar())
            {
                string query = "UPDATE Pedido SET Estado = @Estado, FechaDeEntrega = @FechaEntrega WHERE IdPedido = @IdPedido";
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand(query, conexion);
                cmd.Parameters.AddWithValue("@Estado", nuevoEstado);
                cmd.Parameters.AddWithValue("@FechaEntrega", fechaEntrega);
                cmd.Parameters.AddWithValue("@IdPedido", idPedido);'''
                
    content = content.replace(old_meth, new_meth)
    
    with open(path, 'w', encoding='cp1252') as f:
        f.write(content)

fix_pedidos_update(r'.\Modelo\Entidades\Pedidos.cs')

def fix_frm_pedidos(path):
    with open(path, 'r', encoding='cp1252') as f:
        content = f.read()
        
    old_call = 'if (DbPedidos.ActualizarEstadoPedido(idPedidoSeleccionado, nuevoEstado))'
    new_call = 'DateTime fechaEntrega = dtpFechaEntrega.Value;\n                if (DbPedidos.ActualizarPedido(idPedidoSeleccionado, nuevoEstado, fechaEntrega))'
    
    content = content.replace(old_call, new_call)
    
    with open(path, 'w', encoding='cp1252') as f:
        f.write(content)

fix_frm_pedidos(r'.\Vista\Pedidos\frmPedidos.cs')
