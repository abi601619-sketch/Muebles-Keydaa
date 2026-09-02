import os

def fix_material_update(path):
    with open(path, 'r', encoding='cp1252') as f:
        content = f.read()
        
    search = 'UPDATE Material SET NombreDelMaterial = @NombreDelMaterial, IdUnidadDeMedida = @IdUnidad, Categoria = (SELECT TOP 1 IdCategoria FROM Categoria WHERE Nombre_Categoria = @Categoria) WHERE IdMaterial = @IdMaterial;'
    replace = 'UPDATE Material SET NombreDelMaterial = @NombreDelMaterial, IdUnidadDeMedida = @IdUnidad, Stock = @Stock, Categoria = (SELECT TOP 1 IdCategoria FROM Categoria WHERE Nombre_Categoria = @Categoria) WHERE IdMaterial = @IdMaterial;'
    
    content = content.replace(search, replace)
    
    # Need to add parameter for @Stock
    search_param = 'comandoObjeto.Parameters.AddWithValue("@IdUnidad", ObtenerIdUnidadDeMedida(UnidadDeMedida1));'
    replace_param = 'comandoObjeto.Parameters.AddWithValue("@IdUnidad", ObtenerIdUnidadDeMedida(UnidadDeMedida1));\n                    comandoObjeto.Parameters.AddWithValue("@Stock", Stock1);'
    
    content = content.replace(search_param, replace_param)
    
    with open(path, 'w', encoding='cp1252') as f:
        f.write(content)
        print("Fixed Material update")

fix_material_update(r'.\Modelo\Entidades\Material.cs')
