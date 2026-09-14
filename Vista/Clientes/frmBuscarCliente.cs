using Modelo.Entidades;
using System;
using System.Drawing;
using System.Windows.Forms;
using Vista.Responsive;

namespace Vista.Clientes
{
    public partial class frmBuscarCliente : Form
    {
        public frmBuscarCliente()
        {
            InitializeComponent();
            ResponsiveHelper.Apply(this);
        }


        public int IdClienteSeleccionado { get; private set; }
        public string NombreClienteSeleccionado { get; private set; }
        public string TelefonoClienteSeleccionado { get; private set; }

        public string CorreoClienteSeleccionado { get; private set; }

        public string DireccionClienteSeleccionado { get; private set; }


        private void MostrarClientes()
        {
            try
            {
                dgvClientes.DataSource = DbCliente.CargarClientesParaSeleccionar();

                dgvClientes.Columns["#"].Visible = false;

                dgvClientes.Columns["Cliente"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrió un error al cargar los clientes: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        private void frmBuscarCliente_Load(object sender, EventArgs e)
        {
            MostrarClientes();
        }

        private void btnSeleccionarCliente_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvClientes.CurrentRow == null)
                {
                    MessageBox.Show("Selecciona un cliente.");
                    return;
                }

                DataGridViewRow fila = dgvClientes.CurrentRow;

                IdClienteSeleccionado = Convert.ToInt32(fila.Cells["#"].Value);

                NombreClienteSeleccionado = Convert.ToString(fila.Cells["Cliente"].Value);

                TelefonoClienteSeleccionado = Convert.ToString(fila.Cells["Telefono"].Value);

                CorreoClienteSeleccionado = Convert.ToString(fila.Cells["Correo"].Value);

                DireccionClienteSeleccionado = Convert.ToString(fila.Cells["Direccion"].Value);

                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrió un error al seleccionar el cliente: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSlir_Click(object sender, EventArgs e)
        {
            Close();

        }

        private void txtBuscarCliente_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtBuscarCliente.Text == "Buscar Cliente...")
                    return;

                dgvClientes.DataSource = DbCliente.BuscarClientesSeleecion(txtBuscarCliente.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void txtBuscarCliente_Leave(object sender, EventArgs e)
        {

            txtBuscarCliente.Text = "Buscar Cliente...";
            txtBuscarCliente.ForeColor = Color.Gray;
        }

        private void txtBuscarCliente_Enter(object sender, EventArgs e)
        {
            //Cuando el usuario de enter para escribir, se va a borrar el texto de indicacion
            // Y el texto ya no sera opaco, sera color negro
            if (txtBuscarCliente.Text == "Buscar Cliente...")
            {
                txtBuscarCliente.Text = "";
                txtBuscarCliente.ForeColor = Color.Black;

            }
        }
    }
}

