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

            // CONFIGURACIÓN DEL DATA GRID
            ConfigurarDataGrid();

            // TOOLTIPS
            ConfigurarTooltips();
        }


        public int IdClienteSeleccionado { get; private set; }
        public string NombreClienteSeleccionado { get; private set; }
        public string TelefonoClienteSeleccionado { get; private set; }

        public string CorreoClienteSeleccionado { get; private set; }

        public string DireccionClienteSeleccionado { get; private set; }


        // ----------------------------------------------------------------------
        // CONFIGURACIÓN DEL DATA GRID

        private void ConfigurarDataGrid()
        {
            dgvClientes.AutoGenerateColumns = true;

            dgvClientes.AllowUserToAddRows = false;
            dgvClientes.AllowUserToDeleteRows = false;
            dgvClientes.AllowUserToResizeRows = false;

            dgvClientes.ReadOnly = true;

            dgvClientes.MultiSelect = false;

            dgvClientes.SelectionMode =
                DataGridViewSelectionMode.FullRowSelect;

            dgvClientes.RowHeadersVisible = false;

            dgvClientes.BackgroundColor =
                Color.White;

            dgvClientes.BorderStyle =
                BorderStyle.None;

            dgvClientes.CellBorderStyle =
                DataGridViewCellBorderStyle.SingleHorizontal;

            dgvClientes.GridColor =
                Color.FromArgb(225, 225, 225);

            dgvClientes.EnableHeadersVisualStyles = false;

            dgvClientes.ColumnHeadersHeight = 38;

            dgvClientes.RowTemplate.Height = 32;

            dgvClientes.AutoSizeColumnsMode =
                DataGridViewAutoSizeColumnsMode.Fill;


            // ------------------------------------------------------------------
            // ENCABEZADO

            dgvClientes.ColumnHeadersDefaultCellStyle =
                new DataGridViewCellStyle
                {
                    BackColor =
                        Color.FromArgb(121, 78, 48),

                    ForeColor =
                        Color.White,

                    Font =
                        new Font(
                            "Times New Roman",
                            10,
                            FontStyle.Bold
                        ),

                    Alignment =
                        DataGridViewContentAlignment.MiddleCenter,

                    SelectionBackColor =
                        Color.FromArgb(121, 78, 48),

                    SelectionForeColor =
                        Color.White
                };


            // ------------------------------------------------------------------
            // FILAS

            dgvClientes.DefaultCellStyle =
                new DataGridViewCellStyle
                {
                    BackColor =
                        Color.White,

                    ForeColor =
                        Color.FromArgb(55, 55, 55),

                    Font =
                        new Font(
                            "Times New Roman",
                            10
                        ),

                    Alignment =
                        DataGridViewContentAlignment.MiddleCenter,

                    SelectionBackColor =
                        Color.FromArgb(238, 215, 185),

                    SelectionForeColor =
                        Color.FromArgb(60, 45, 35),

                    Padding =
                        new Padding(5)
                };


            // ------------------------------------------------------------------
            // FILAS ALTERNADAS

            dgvClientes.AlternatingRowsDefaultCellStyle =
                new DataGridViewCellStyle
                {
                    BackColor =
                        Color.FromArgb(
                            250,
                            246,
                            240
                        ),

                    ForeColor =
                        Color.FromArgb(
                            55,
                            55,
                            55
                        ),

                    Font =
                        new Font(
                            "Times New Roman",
                            10
                        ),

                    SelectionBackColor =
                        Color.FromArgb(
                            238,
                            215,
                            185
                        ),

                    SelectionForeColor =
                        Color.FromArgb(
                            60,
                            45,
                            35
                        )
                };
        }


        // ----------------------------------------------------------------------
        // FORMATEAR DATA GRID

        private void FormatearDataGrid()
        {
            if (dgvClientes.Columns.Count == 0)
                return;


            // Ocultar ID

            if (dgvClientes.Columns.Contains("#"))
            {
                dgvClientes.Columns["#"].Visible = false;
            }


            // ------------------------------------------------------------------
            // CLIENTE

            if (dgvClientes.Columns.Contains("Cliente"))
            {
                dgvClientes.Columns["Cliente"].HeaderText =
                    "Cliente";

                dgvClientes.Columns["Cliente"].AutoSizeMode =
                    DataGridViewAutoSizeColumnMode.Fill;
            }


            // ------------------------------------------------------------------
            // TELÉFONO

            if (dgvClientes.Columns.Contains("Telefono"))
            {
                dgvClientes.Columns["Telefono"].HeaderText =
                    "Teléfono";
            }


            // ------------------------------------------------------------------
            // CORREO

            if (dgvClientes.Columns.Contains("Correo"))
            {
                dgvClientes.Columns["Correo"].HeaderText =
                    "Correo";
            }


            // ------------------------------------------------------------------
            // DIRECCIÓN

            if (dgvClientes.Columns.Contains("Direccion"))
            {
                dgvClientes.Columns["Direccion"].HeaderText =
                    "Dirección";
            }


            // ------------------------------------------------------------------
            // ESTADO

            if (dgvClientes.Columns.Contains("Estado"))
            {
                dgvClientes.Columns["Estado"].HeaderText =
                    "Estado";
            }


            // ------------------------------------------------------------------
            // EVITAR ORDENAMIENTO

            foreach (
                DataGridViewColumn columna
                in dgvClientes.Columns)
            {
                columna.SortMode =
                    DataGridViewColumnSortMode.NotSortable;
            }


            dgvClientes.AutoSizeColumnsMode =
                DataGridViewAutoSizeColumnsMode.Fill;
        }


        // ----------------------------------------------------------------------
        // MOSTRAR CLIENTES

        private void MostrarClientes()
        {
            try
            {
                dgvClientes.DataSource = null;

                dgvClientes.DataSource =
                    DbCliente.CargarClientesParaSeleccionar();

                FormatearDataGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Ocurrió un error al cargar los clientes: " +
                    ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }


        // ----------------------------------------------------------------------
        // TOOLTIP

        private void ConfigurarTooltips()
        {
            ToolTip tooltip =
                new ToolTip();

            tooltip.AutoPopDelay =
                5000;

            tooltip.InitialDelay =
                500;

            tooltip.ReshowDelay =
                200;

            tooltip.ShowAlways =
                true;


            tooltip.SetToolTip(
                txtBuscarCliente,
                "Buscar un cliente por nombre, teléfono, correo o dirección."
            );


            tooltip.SetToolTip(
                dgvClientes,
                "Seleccione el cliente que desea utilizar."
            );


            tooltip.SetToolTip(
                btnSeleccionarCliente,
                "Seleccionar el cliente marcado."
            );


            tooltip.SetToolTip(
                btnSlir,
                "Cerrar esta ventana."
            );
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
        // ----------------------------------------------------------------------
        // BOTON DE SALIR
        private void btnSlir_Click(object sender, EventArgs e)
        {
            Close();

        }
        // ----------------------------------------------------------------------
        // AL SELECCIONAR UN CLIENTE
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
        // ----------------------------------------------------------------------
        // BUSQUEDA
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

