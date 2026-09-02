using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo.Entidades
{
    public class TipoCliente
    {
        private int IdTipoCliente;
        private string Tipo_Cliente;

        public TipoCliente(int idTipoCliente, string tipo_Cliente)
        {
            IdTipoCliente1=idTipoCliente;
            Tipo_Cliente1=tipo_Cliente;
        }

        public int IdTipoCliente1 { get => IdTipoCliente; set => IdTipoCliente=value; }
        public string Tipo_Cliente1 { get => Tipo_Cliente; set => Tipo_Cliente=value; }
    }
}
