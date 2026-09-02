import os

def add_ventas_del(path_cs, path_designer, path_model):
    with open(path_model, 'r', encoding='cp1252') as f:
        content = f.read()
        
    method = '''
        public bool EliminarVenta()
        {
            using (SqlConnection conexion = Conexion.Conectar())
            {
                SqlTransaction transaccion = conexion.BeginTransaction();
                try
                {
                    // Eliminar factura si existe
                    string cmdFactura = "DELETE FROM Factura WHERE IdVenta = @IdVenta;";
                    using (SqlCommand cmd = new SqlCommand(cmdFactura, conexion, transaccion))
                    {
                        cmd.Parameters.AddWithValue("@IdVenta", IdVenta);
                        cmd.ExecuteNonQuery();
                    }
                    
                    // Eliminar detalle de venta
                    string cmdDetalle = "DELETE FROM DetalleVenta WHERE IdVenta = @IdVenta;";
                    using (SqlCommand cmd = new SqlCommand(cmdDetalle, conexion, transaccion))
                    {
                        cmd.Parameters.AddWithValue("@IdVenta", IdVenta);
                        cmd.ExecuteNonQuery();
                    }
                    
                    // Eliminar venta
                    string cmdVenta = "DELETE FROM Venta WHERE IdVenta = @IdVenta;";
                    using (SqlCommand cmd = new SqlCommand(cmdVenta, conexion, transaccion))
                    {
                        cmd.Parameters.AddWithValue("@IdVenta", IdVenta);
                        int filas = cmd.ExecuteNonQuery();
                        
                        if (filas > 0)
                        {
                            transaccion.Commit();
                            return true;
                        }
                        else
                        {
                            transaccion.Rollback();
                            return false;
                        }
                    }
                }
                catch (Exception)
                {
                    transaccion.Rollback();
                    return false;
                }
            }
        }
'''
    content = content.replace('    }\n}\n', method + '    }\n}\n')
    with open(path_model, 'w', encoding='cp1252') as f:
        f.write(content)

add_ventas_del(r'.\Vista\Ventas\frmVentas.cs', r'.\Vista\Ventas\frmVentas.Designer.cs', r'.\Modelo\Entidades\Ventas.cs')
