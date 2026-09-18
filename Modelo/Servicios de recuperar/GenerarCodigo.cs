using System;

namespace Modelo.Servicios
{
    public class GeneradorCodigo
    {
        public static string GenerarCodigo()
        {
            Random random = new Random();

            return random.Next(100000, 1000000)
                         .ToString();
        }
    }
}
