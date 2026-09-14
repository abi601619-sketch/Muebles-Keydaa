using Modelo.Entidades;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Vista.Responsive;

namespace Vista.Producción
{
    public partial class frmProduccion : Form
    {
        public frmProduccion()
        {
            InitializeComponent();
            ResponsiveHelper.Apply(this);
        }

        private void frmProduccion_Load(object sender, EventArgs e)
        {
            MostrarProduccion();

            dgvProduccion.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            dgvProduccion.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;


            ActualizarEstadisticas();
        }

        private void ConfigurarColumnasProduccion()
        {
            dgvProduccion.Columns["IdProduccion"].Visible = false;

            dgvProduccion.Columns["IdPedido"].HeaderText = "N° Pedido";
            dgvProduccion.Columns["Cliente"].HeaderText = "Cliente";
            dgvProduccion.Columns["Producto"].HeaderText = "Producto";
            dgvProduccion.Columns["Largo"].HeaderText = "Largo (cm)";
            dgvProduccion.Columns["Ancho"].HeaderText = "Ancho (cm)";
            dgvProduccion.Columns["Alto"].HeaderText = "Alto (cm)";
            dgvProduccion.Columns["Cantidad"].HeaderText = "Cantidad";
            dgvProduccion.Columns["Progreso"].HeaderText = "Progreso (%)";
            dgvProduccion.Columns["Estado"].HeaderText = "Estado";
        }

        public void MostrarProduccion()
        {
            DataTable datos = DbProducción.CargarProducción();

            dgvProduccion.DataSource = null;

            dgvProduccion.DataSource = datos;
            ConfigurarColumnasProduccion();

            dgvProduccion.Refresh();
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (dgvProduccion.CurrentRow == null)
            {
                MessageBox.Show("Selecciona una producción.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                return;
            }
            int idProduccion = Convert.ToInt32(dgvProduccion.CurrentRow.Cells["IdProduccion"].Value);
            frmEditarProduccion formulario = new frmEditarProduccion(idProduccion);
            DialogResult resultado = formulario.ShowDialog();
            if (resultado == DialogResult.OK)
            {
                dgvProduccion.Columns["IdProduccion"].Visible = false;
                dgvProduccion.Columns["IdPedido"].HeaderText = "N° Pedido";
                dgvProduccion.Columns["Cliente"].HeaderText = "Cliente";
                dgvProduccion.Columns["Producto"].HeaderText = "Producto";
                dgvProduccion.Columns["Largo"].HeaderText = "Largo (cm)";
                dgvProduccion.Columns["Ancho"].HeaderText = "Ancho (cm)";
                dgvProduccion.Columns["Alto"].HeaderText = "Alto (cm)";
                dgvProduccion.Columns["Cantidad"].HeaderText = "Cantidad";
                dgvProduccion.Columns["Progreso"].HeaderText = "Progreso (%)";
                dgvProduccion.Columns["Estado"].HeaderText = "Estado";
                MostrarProduccion();
            }

        }

        private void txtBuscar_Enter(object sender, EventArgs e)
        {
            if (txtBuscar.Text == "Buscar por código o nombre de cliente...")
            {
                txtBuscar.Text = "";
                txtBuscar.ForeColor = Color.Black;
            }
        }

        private void txtBuscar_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtBuscar.Text))
            {
                txtBuscar.Text =
                    "Buscar por código o nombre de cliente...";

                txtBuscar.ForeColor = Color.Gray;
            }
        }



        private void FiltrarTabla()
        {
            if (dgvProduccion.DataSource is System.Data.DataTable dt)
            {
                string estado = cbEstados.Text;
                string buscar = txtBuscar.Text == "Buscar por código o nombre de cliente..." ? "" : txtBuscar.Text;

                string filtro = "1=1";
                if (!string.IsNullOrWhiteSpace(estado) && estado != "Todos")
                    filtro += " AND Estado = '" + estado + "'";

                if (!string.IsNullOrWhiteSpace(buscar))
                {
                    filtro += " AND (Cliente LIKE '%" + buscar + "%' OR Convert(IdProduccion, 'System.String') LIKE '%" + buscar + "%' OR Convert(IdPedido, 'System.String') LIKE '%" + buscar + "%')";
                }

                dt.DefaultView.RowFilter = filtro;
            }
        }

        private void cbEstados_SelectedIndexChanged(object sender, EventArgs e)
        {
            FiltrarTabla();
        }

        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            if (txtBuscar.Text != "Buscar por código o nombre de cliente...")
            {
                FiltrarTabla();
            }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            cbEstados.SelectedIndex = -1;
            txtBuscar.Text = "Buscar por código o nombre de cliente...";
            txtBuscar.ForeColor = Color.Gray;
            if (dgvProduccion.DataSource is System.Data.DataTable dt)
            {
                dt.DefaultView.RowFilter = "";
            }
        }

        private void btnMaterialUtilizado_Click(object sender, EventArgs e)
        {

            if (dgvProduccion.CurrentRow == null)
            {
                MessageBox.Show("Selecciona una producción.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                return;
            }

            int idProduccion = Convert.ToInt32(dgvProduccion.CurrentRow.Cells["IdProduccion"].Value);

            string producto = dgvProduccion.CurrentRow.Cells["Producto"].Value.ToString();

            DateTime fechaEntrega = Convert.ToDateTime(dgvProduccion.CurrentRow.Cells["Fecha de Entrega"].Value);

            frmMaterialUtilizado formulario = new frmMaterialUtilizado(idProduccion, producto, fechaEntrega);

            formulario.ShowDialog();

        }

        //-------------------------------------------------------------------


        //Metodo para actualizarlas
        private void ActualizarEstadisticas()
        {
            lblMostrarRegistrados.Text = DbProducción.ContarProduccionesTotales().ToString();

            lblMostrarPendientes.Text = DbProducción.ContarProduccionesPendientes().ToString();

            lblMostrarEnProduccion.Text = DbProducción.ContarProduccionesEnProceso().ToString();

            lblMostrarFinalizados.Text = DbProducción.ContarProduccionesFinalizadas().ToString();
        }
    }
}



