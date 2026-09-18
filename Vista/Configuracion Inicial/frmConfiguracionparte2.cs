using System;
using System.Windows.Forms;

namespace Vista.Configuracion_Inicial
{
    public partial class frmConfiguracionparte2 : Form
    {
        public frmConfiguracionparte2()
        {
            InitializeComponent();
        }

        private void btnAtras_Click(object sender, EventArgs e)
        {
            Configuracion_Inicial frm = new Configuracion_Inicial();
            frm.ShowDialog();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            ConfiguracionUltimaParte frm = new ConfiguracionUltimaParte();
            frm.ShowDialog();
        }
    }
}
