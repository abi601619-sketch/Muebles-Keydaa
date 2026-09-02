import os
import glob

def fix_all_dgv():
    for root, dirs, files in os.walk(r'.\Vista'):
        for file in files:
            if file.endswith('.Designer.cs'):
                path = os.path.join(root, file)
                with open(path, 'r', encoding='cp1252') as f:
                    content = f.read()
                
                if 'DataGridViewColumnHeadersHeightSizeMode.AutoSize' in content:
                    content = content.replace('DataGridViewColumnHeadersHeightSizeMode.AutoSize', 'DataGridViewColumnHeadersHeightSizeMode.DisableResizing')
                    with open(path, 'w', encoding='cp1252') as f:
                        f.write(content)
                        print(f"Fixed {path}")

fix_all_dgv()
