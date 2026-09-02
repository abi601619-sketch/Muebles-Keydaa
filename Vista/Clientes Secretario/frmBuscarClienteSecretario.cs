using Modelo.Entidades;
using System;
using System.Windows.Forms;
using Vista.Responsive;

namespace Vista.Clientes_Secretario
{
    public partial class frmBuscarClienteSecretario : Form
    {
        public frmBuscarClienteSecretario()
        {
            InitializeComponent();
            ResponsiveHelper.Apply(this);
        }

        public int IdClienteSeleccionado;
        public string NombreClienteSeleccionado;
        public string TelefonoClienteSeleccionado;
        public string CorreoClienteSeleccionado;
        public string DireccionClienteSeleccionado;

        private void MostrarClientesEmpelado()
        {
            dgvClientesEmpleados.DataSource = DbCliente.CargarClientesParaSeleccionar();
            dgvClientesEmpleados.Columns["IdCliente"].Visible = false;
            dgvClientesEmpleados.Columns["Cliente"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }

        private void btnCerrarClientes_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void frmBuscarClienteSecretario_Load(object sender, EventArgs e)
        {
            MostrarClientesEmpelado();
        }

        private void btnSeleccionarClienteEmpleado_Click(object sender, EventArgs e)
        {
            if (dgvClientesEmpleados.CurrentRow == null)
            {
                MessageBox.Show("Selecciona un cliente.");
                return;
            }

            DataGridViewRow fila = dgvClientesEmpleados.CurrentRow;

            IdClienteSeleccionado = Convert.ToInt32(fila.Cells["IdCliente"].Value);
            NombreClienteSeleccionado = fila.Cells["Cliente"].Value.ToString();
            TelefonoClienteSeleccionado = fila.Cells["Telefono"].Value.ToString();
            CorreoClienteSeleccionado = fila.Cells["Correo"].Value.ToString();
            DireccionClienteSeleccionado = fila.Cells["Direccion"].Value.ToString();
            DialogResult = DialogResult.OK;
            Close();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
