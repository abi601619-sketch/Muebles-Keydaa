import os

def hide_btn(path):
    with open(path, 'r', encoding='cp1252') as f:
        content = f.read()
        
    content = content.replace('this.button1.UseVisualStyleBackColor = false;', 'this.button1.UseVisualStyleBackColor = false;\n            this.button1.Visible = false;')
    
    with open(path, 'w', encoding='cp1252') as f:
        f.write(content)

hide_btn(r'.\Vista\Facturación\frmFacturacion.Designer.cs')
