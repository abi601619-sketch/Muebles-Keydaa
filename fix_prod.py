import os
import glob

def fix_prod():
    files = glob.glob(r'.\Modelo\Entidades\Producci*n.cs')
    if not files: return
    path = files[0]
    
    with open(path, 'r', encoding='cp1252') as f:
        content = f.read()
        
    lines = content.split('\n')
    unique_lines = []
    for line in lines:
        if 'using Modelo.Conexi' in line:
            if not any('using Modelo.Conexi' in u for u in unique_lines):
                unique_lines.append(line)
        else:
            unique_lines.append(line)
            
    with open(path, 'w', encoding='cp1252') as f:
        f.write('\n'.join(unique_lines))

fix_prod()
