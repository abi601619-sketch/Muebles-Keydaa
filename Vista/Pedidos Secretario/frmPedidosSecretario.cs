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
using Vista.Clientes;
using Vista.Clientes_Secretario;

namespace Vista.Pedidos_Secretario
{
    public partial class frmPedidosSecretario : Form
    {
        public frmPedidosSecretario()
        {
            InitializeComponent();
            ResponsiveHelper.Apply(this);
        }

      

        private void btnSeleccionarCliente_Click(object sender, EventArgs e)
        {
            frmBuscarClienteSecretario modal = new frmBuscarClienteSecretario();
            modal.ShowDialog();
        }

        private void btnDeatllePedido_Click(object sender, EventArgs e)
        {
            frmDetallePedidoSecretario modal = new frmDetallePedidoSecretario();
            modal.ShowDialog();
        }

        private void txtBuscar_Enter(object sender, EventArgs e)
        {
            //Cuando el usuario de enter para escribir, se va a borrar el texto de indicacion
            // Y el texto ya no sera opaco, sera color negro
            if (txtBuscar.Text=="Buscar Pedido...")
            {
                txtBuscar.Text="";
                txtBuscar.ForeColor=Color.Black;

            }
        }

        private void txtBuscar_Leave(object sender, EventArgs e)
        {
            txtBuscar.Text="Buscar Pedido...";
            txtBuscar.ForeColor=Color.Gray;
        }

        private void MostrarDetallesPedidos()
        {
            dgvDetallesDePedido.DataSource = null;
            dgvDetallesDePedido.DataSource = DetallePedidos.CargarDetallesPedidos();
        }
        private void MostrarPedidosRegistrados()
        {
            dgvPedidosRegistrados.DataSource = null;
            dgvPedidosRegistrados.DataSource = DbPedidos.CargarRegistroPedidos();
        }

        private void frmPedidosSecretario_Load(object sender, EventArgs e)
        {
            MostrarPedidosRegistrados();
            MostrarDetallesPedidos();
        }

        
    }
}

