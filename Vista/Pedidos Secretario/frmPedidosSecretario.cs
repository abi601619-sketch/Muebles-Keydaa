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
            dgvDetallesDePedido.AllowUserToDeleteRows = false;
            dgvDetallesDePedido.AllowUserToAddRows = false;
            dgvDetallesDePedido.ReadOnly = true;
            dgvDetallesDePedido.Columns.Add(new DataGridViewButtonColumn
            {
                Name = "EliminarProducto", HeaderText = "Eliminar", Text = "Eliminar",
                UseColumnTextForButtonValue = true
            });
            dgvDetallesDePedido.CellContentClick += EliminarProducto_Click;
        }

      

        private void EliminarProducto_Click(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0 ||
                dgvDetallesDePedido.Columns[e.ColumnIndex].Name != "EliminarProducto")
                return;
            var fila = dgvDetallesDePedido.Rows[e.RowIndex];
            if (fila.IsNewRow) return;
            int pedido = Convert.ToInt32(fila.Cells["IdPedido"].Value);
            int detalle = Convert.ToInt32(fila.Cells["IdDetallePedido"].Value);
            try
            {
                bool ultimo = DetallePedidos.CargarDetallesPorPedido(pedido).Rows.Count == 1;
                string aviso = ultimo
                    ? "Al eliminar el último producto, el pedido quedará marcado como Cancelado. El registro del pedido se conservará. ¿Desea continuar?"
                    : "¿Desea eliminar este producto del pedido?";
                if (MessageBox.Show(aviso, "Confirmar eliminación", MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) != DialogResult.Yes)
                    return;
                bool cancelado = DetallePedidos.EliminarDetalle(pedido, detalle, ultimo);
                dgvDetallesDePedido.DataSource = DetallePedidos.CargarDetallesPorPedido(pedido);
                MostrarPedidosRegistrados();

                MessageBox.Show(cancelado ? "Producto eliminado. El pedido quedó Cancelado."
                    : "Producto eliminado correctamente.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se pudo eliminar el producto: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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

