using Vista.Responsive;
using Modelo.Conexión_DB;
using Modelo.Entidades;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Vista.Clientes_Secretario;
using Vista.Inventario;
using Vista.Iventario_Secretario;
using Vista.Pedidos;
using Vista.Pedidos_Secretario;

namespace Vista.InicioSecretario
{
    public partial class frmInicioSecretario : Form
    {
        public frmInicioSecretario()
        {
            InitializeComponent();
            ResponsiveHelper.Apply(this);
        }

        private void AbrirFormulario(Form formulario)
        {
            pnlContenedor.Controls.Clear();

            formulario.TopLevel = false;
            formulario.FormBorderStyle = FormBorderStyle.None;
            formulario.Dock = DockStyle.Fill;

            pnlContenedor.Controls.Add(formulario);
            pnlContenedor.Tag = formulario;

            formulario.Show();
        }


        private void lblVerDetallesClientes_Click_1(object sender, EventArgs e)
        {
            AbrirFormulario(new frmClientesSecretario());
        }


        private void lblVerDetallesClientes_Click(object sender, EventArgs e)
        {
            AbrirFormulario(new frmClientesSecretario());
        }

        private void lblVerDetallesPedidos_Click(object sender, EventArgs e)
        {
            AbrirFormulario(new frmPedidosSecretario());
        }

        private void lblVerDetallesInventario_Click(object sender, EventArgs e)
        {
            AbrirFormulario(new frmInventarioSecretario());
        }

        private void MostrarPedidosRecientes()
        {
            dgvPedidosRecientes.DataSource = null;
            dgvPedidosRecientes.DataSource = DbPedidos.CargarPedidosRecientes();
        }

        private void frmInicioSecretario_Load(object sender, EventArgs e)
        {
            MostrarPedidosRecientes();
        }
    }
}

