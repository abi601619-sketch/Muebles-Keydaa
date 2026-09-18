using System;
using System.Windows.Forms;
using Vista.Login;

namespace Vista.Configuracion_Inicial
{
    public partial class ConfiguracionUltimaParte : Form
    {
        public ConfiguracionUltimaParte()
        {
            InitializeComponent();
        }

        private void btnInicioLogin_Click(object sender, EventArgs e)
        {
            frmLogin frm = new frmLogin();
            frm.ShowDialog();
        }
    }
}
