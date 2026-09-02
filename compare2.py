import os
import difflib

dir1 = r'C:\Users\adria\OneDrive\Escritorio\trabajo\C# Muebles Keyda\PTC MODIFICACION 9000'
dir2 = r'C:\Users\adria\OneDrive\Escritorio\muebleskeyda-main\muebleskeyda-main'

def normalize(c):
    return ''.join(c.split())

def compare_dirs(d1, d2):
    for root, dirs, files in os.walk(d2):
        if '.git' in root or 'obj' in root or 'bin' in root or '.vs' in root:
            continue
        for f in files:
            if f.endswith('.cs'):
                path2 = os.path.join(root, f)
                rel_path = os.path.relpath(path2, d2)
                path1 = os.path.join(d1, rel_path)
                
                if os.path.exists(path1):
                    with open(path1, 'r', encoding='utf-8', errors='ignore') as f1, open(path2, 'r', encoding='utf-8', errors='ignore') as f2:
                        c1 = normalize(f1.read())
                        c2 = normalize(f2.read())
                        if c1 != c2:
                            print(f'CHANGED FILE (ignoring whitespace): {rel_path}')
                else:
                    print(f'NEW FILE: {rel_path}')

compare_dirs(dir1, dir2)
