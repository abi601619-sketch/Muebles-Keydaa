import os

def insert_method(path):
    with open(path, 'r', encoding='cp1252') as f:
        content = f.read()
        
    method = '''
        public static DataTable CargarVentasParaFactura()
        {
            SqlConnection conectar = Conexion.Conectar();
            string comando = "SELECT v.IdVenta, CAST(v.IdVenta AS VARCHAR) + ' - ' + c.Nombre_Cliente AS Display FROM Venta v INNER JOIN Clientes c ON v.IdCliente = c.IdCliente LEFT JOIN Factura f ON v.IdVenta = f.IdVenta WHERE f.IdFactura IS NULL;";
            SqlDataAdapter adapter = new SqlDataAdapter(comando, conectar);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            return dt;
        }
'''
    
    # insert before CargarRegistrosFacturas
    content = content.replace('        public static DataTable CargarRegistrosFacturas()', method + '\n        public static DataTable CargarRegistrosFacturas()')

    with open(path, 'w', encoding='cp1252') as f:
        f.write(content)

insert_method(r'.\Modelo\Entidades\Factura.cs')
