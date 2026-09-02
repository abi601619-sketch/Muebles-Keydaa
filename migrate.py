import os

def migrate_textbox_to_combobox(path):
    with open(path, 'r', encoding='cp1252') as f:
        content = f.read()
        
    content = content.replace(
        'this.txtProductosCotizadosFacturados = new System.Windows.Forms.TextBox();', 
        'this.cbVentas = new System.Windows.Forms.ComboBox();'
    )
    content = content.replace(
        'this.pnlDatalledeProductos.Controls.Add(this.txtProductosCotizadosFacturados);', 
        'this.pnlDatalledeProductos.Controls.Add(this.cbVentas);'
    )
    content = content.replace(
        'private System.Windows.Forms.TextBox txtProductosCotizadosFacturados;', 
        'private System.Windows.Forms.ComboBox cbVentas;'
    )
    
    old_props = '''            // txtProductosCotizadosFacturados
            // 
            this.txtProductosCotizadosFacturados.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtProductosCotizadosFacturados.Font = new System.Drawing.Font("Times New Roman", 12F);
            this.txtProductosCotizadosFacturados.Location = new System.Drawing.Point(118, 50);
            this.txtProductosCotizadosFacturados.Margin = new System.Windows.Forms.Padding(2);
            this.txtProductosCotizadosFacturados.Name = "txtProductosCotizadosFacturados";
            this.txtProductosCotizadosFacturados.Size = new System.Drawing.Size(193, 19);
            this.txtProductosCotizadosFacturados.TabIndex = 2;'''
            
    new_props = '''            // cbVentas
            // 
            this.cbVentas.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbVentas.Font = new System.Drawing.Font("Times New Roman", 12F);
            this.cbVentas.Location = new System.Drawing.Point(118, 50);
            this.cbVentas.Margin = new System.Windows.Forms.Padding(2);
            this.cbVentas.Name = "cbVentas";
            this.cbVentas.Size = new System.Drawing.Size(200, 27);
            this.cbVentas.TabIndex = 2;'''
            
    content = content.replace(old_props, new_props)
    
    with open(path, 'w', encoding='cp1252') as f:
        f.write(content)

migrate_textbox_to_combobox(r'.\Vista\Facturación\frmFacturacion.Designer.cs')
