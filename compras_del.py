import os

def add_compras_del(path_cs, path_designer, path_model):
    with open(path_model, 'r', encoding='cp1252') as f:
        content = f.read()
        
    method = '''
        public bool EliminarCompra()
        {
            using (SqlConnection conexion = Conexion.Conectar())
            {
                SqlTransaction transaccion = conexion.BeginTransaction();
                try
                {
                    string cmdDetalle = "DELETE FROM DetalleCompraMaterial WHERE IdCompra = @IdCompra;";
                    using (SqlCommand cmd = new SqlCommand(cmdDetalle, conexion, transaccion))
                    {
                        cmd.Parameters.AddWithValue("@IdCompra", IdCompra1);
                        cmd.ExecuteNonQuery();
                    }
                    
                    string cmdCompra = "DELETE FROM Compras WHERE IdCompra = @IdCompra;";
                    using (SqlCommand cmd = new SqlCommand(cmdCompra, conexion, transaccion))
                    {
                        cmd.Parameters.AddWithValue("@IdCompra", IdCompra1);
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
        
    with open(path_designer, 'r', encoding='cp1252') as f:
        content = f.read()
        
    # I will repurpose btnCancelar to be btnEliminar in Compras!
    content = content.replace('this.btnCancelar.Text = "Cancelar";', 'this.btnCancelar.Text = "Eliminar Compra";\n            this.btnCancelar.BackColor = System.Drawing.Color.LightCoral;')
    content = content.replace('this.btnCancelar.UseVisualStyleBackColor = true;', 'this.btnCancelar.UseVisualStyleBackColor = false;\n            this.btnCancelar.Click += new System.EventHandler(this.btnEliminar_Click);')
    content = content.replace('this.dgvComprasRegistradas.TabIndex = 2;', 'this.dgvComprasRegistradas.TabIndex = 2;\n            this.dgvComprasRegistradas.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvComprasRegistradas_CellDoubleClick);')
    with open(path_designer, 'w', encoding='cp1252') as f:
        f.write(content)
        
    with open(path_cs, 'r', encoding='cp1252') as f:
        content = f.read()
        
    methods_cs = '''
        private int idCompraSeleccionada = 0;
        
        private void dgvComprasRegistradas_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow fila = dgvComprasRegistradas.Rows[e.RowIndex];
                idCompraSeleccionada = Convert.ToInt32(fila.Cells["IdCompra"].Value);
            }
        }
        
        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (idCompraSeleccionada == 0)
            {
                MessageBox.Show("Seleccione una compra dando doble clic en la tabla inferior.");
                return;
            }
            
            DialogResult res = MessageBox.Show("¿Está seguro de eliminar esta compra permanentemente? Se eliminarán también sus detalles.", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (res == DialogResult.Yes)
            {
                DbCompras compra = new DbCompras();
                compra.IdCompra1 = idCompraSeleccionada;
                if (compra.EliminarCompra())
                {
                    MessageBox.Show("Compra eliminada correctamente.");
                    MostrarRegistros();
                    idCompraSeleccionada = 0;
                }
                else
                {
                    MessageBox.Show("Error al eliminar la compra.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
'''
    content = content.replace('    }\n}\n', methods_cs + '    }\n}\n')
    with open(path_cs, 'w', encoding='cp1252') as f:
        f.write(content)

add_compras_del(r'.\Vista\Compras\frmCompras.cs', r'.\Vista\Compras\frmCompras.Designer.cs', r'.\Modelo\Entidades\Compras.cs')
