import re

with open(r'Vista\Produccin\frmProduccion.Designer.cs', 'r', encoding='utf-8', errors='ignore') as f:
    text = f.read()

# find panels
for match in re.finditer(r'this\.(\w+)\.Controls\.Add', text):
    print('Container:', match.group(1))
