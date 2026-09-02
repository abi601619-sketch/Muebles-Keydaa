using System;
using System.IO;
using System.Text;
class Program {
    static void Main() {
        string file = @".\Modelo\Entidades\Categorias.cs";
        string text = File.ReadAllText(file, Encoding.UTF8);
        text = text.Replace("UPDATE Categoria SET Estado = 'Inactiva' WHERE IdCategoria = @IdCategoria", "DELETE FROM Categoria WHERE IdCategoria = @IdCategoria");
        File.WriteAllText(file, text, new UTF8Encoding(false));
    }
}
