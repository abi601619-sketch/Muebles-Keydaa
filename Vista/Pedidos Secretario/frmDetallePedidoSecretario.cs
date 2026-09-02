using Vista.Responsive;
using System;
using Modelo.Entidades;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Vista.Pedidos_Secretario
{
    public partial class frmDetallePedidoSecretario : Form
    {
        public frmDetallePedidoSecretario()
        {
            InitializeComponent();
            ResponsiveHelper.Apply(this);
        }
        public string Largo;
        public string Ancho;
        public string Alto;
        public string Observaciones;
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            Largo = txtLargo.Text;
            Ancho = txtAncho.Text;
            Alto = txtAlto.Text;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
