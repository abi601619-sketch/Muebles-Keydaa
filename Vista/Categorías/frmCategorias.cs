using Modelo.Entidades;
using System;
using System.Drawing;
using System.Windows.Forms;
using Vista.Responsive;

namespace Vista.Categorías
{
    public partial class frmCategorias : Form
    {
        public frmCategorias()
        {
            InitializeComponent();
            ResponsiveHelper.Apply(this);
        }



        private void txtBuscarCategoria_Enter(object sender, EventArgs e)
        {
            //Cuando el usuario de enter para escribir, se va a borrar el texto de indicacion
            // Y el texto ya no sera opaco, sera color negro
            if (txtBuscarCategoria.Text == "Buscar Categoría...")
            {
                txtBuscarCategoria.Text = "";
                txtBuscarCategoria.ForeColor = Color.Black;

            }
        }

        private void txtBuscarCategoria_Leave(object sender, EventArgs e)
        {
            if (txtBuscarCategoria.Text == "")
            {
                txtBuscarCategoria.Text = "Buscar Categoría...";
                txtBuscarCategoria.ForeColor = Color.Gray;
            }
        }

        private void frmCategorias_Load(object sender, EventArgs e)
        {
            MostrarCategorias();
            DesactivarCopiarPegar(this);
            CargarEstadisticasCategorias();
            btnGuardarCambios.Visible = false;
            cbEstado.Enabled = false;
            btnEditar.Visible = false;


            //Maximo de caracteres admitidos
            txtCategoria.MaxLength = 50;
            txtDescripcion.MaxLength = 200;

            //Navegar con la tecla Tab
            txtCategoria.TabIndex = 1;
            txtDescripcion.TabIndex = 2;
            cbEstado.TabIndex = 3;
            btnGuardar.TabIndex = 4;
            btnEditar.TabIndex = 5;

            dgvCategorias.Columns["IdCategoria"].HeaderText = "#";
            dgvCategorias.Columns["Nombre_Categoria"].HeaderText = "Categoría";
            dgvCategorias.Columns["Descripcion"].HeaderText = "Descripción";
        }

        public void MostrarCategorias()
        {
            dgvCategorias.DataSource = null;
            dgvCategorias.DataSource = Categorias.CargarCategorias();
            CargarEstadisticasCategorias();
        }



        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtCategoria.Text))
            {
                MessageBox.Show("Debe ingresar el nombre de la categoría.");
                txtCategoria.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(txtDescripcion.Text))
            {
                MessageBox.Show("Debe ingresar la descripción de la categoría.");
                txtDescripcion.Focus();
                return;
            }

            if (cbEstado.SelectedIndex == -1)
            {
                MessageBox.Show("Debe seleccionar el estado de la categoría.");
                cbEstado.Focus();
                return;
            }

            Categorias categoria = new Categorias();

            categoria.Nombre_Categoria1 = txtCategoria.Text;
            categoria.Descripción1 = txtDescripcion.Text;
            categoria.Estado1 = "Activa";
            cbEstado.Enabled = true;
            if (categoria.InsertarCategoria())
            {
                MessageBox.Show("Categoría registrada correctamente.", "Registro exitoso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            MostrarCategorias();
        }
        private int idCategoriaSeleccionada = 0;


        private void dgvCategorias_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            dgvCategorias.Columns["IdCategoria"].HeaderText = "#";
            dgvCategorias.Columns["Nombre_Categoria"].HeaderText = "Categoría";
            dgvCategorias.Columns["Descripcion"].HeaderText = "Descripción";
            if (e.RowIndex >= 0)
            {
                idCategoriaSeleccionada = Convert.ToInt32(
                    dgvCategorias.Rows[e.RowIndex].Cells["IdCategoria"].Value
                );
            }
        }

        private void BloquearCampos()
        {
            txtCategoria.ReadOnly = true;
            txtDescripcion.ReadOnly = true;
            cbEstado.Enabled = false;
        }

        private void DesbloquearCampos()
        {
            txtCategoria.ReadOnly = false;
            txtDescripcion.ReadOnly = false;
            cbEstado.Enabled = true;
        }

        private void LimpiarFormulario()
        {
            txtCategoria.Clear();
            txtDescripcion.Clear();
            cbEstado.Enabled = false;
            btnGuardarCambios.Visible = false;
            btnGuardar.Visible = true;

            txtCategoria.Focus();
        }

        private void dgvCategorias_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            dgvCategorias.Columns["IdCategoria"].HeaderText = "#";
            dgvCategorias.Columns["Nombre_Categoria"].HeaderText = "Categoría";
            dgvCategorias.Columns["Descripcion"].HeaderText = "Descripción";
            if (e.RowIndex >= 0)
            {
                idCategoriaSeleccionada = Convert.ToInt32(
                    dgvCategorias.Rows[e.RowIndex].Cells["IdCategoria"].Value
                );

                txtCategoria.Text = dgvCategorias.Rows[e.RowIndex]
                    .Cells["Nombre_Categoria"].Value.ToString();

                txtDescripcion.Text = dgvCategorias.Rows[e.RowIndex]
                    .Cells["Descripcion"].Value.ToString();

                cbEstado.Text = dgvCategorias.Rows[e.RowIndex]
                    .Cells["Estado"].Value.ToString();

                // Mantener campos bloqueados
                BloquearCampos();

                // Mostrar Editar
                btnEditar.Visible = true;
                btnGuardarCambios.Visible = false;
                btnGuardar.Visible = false;
            }

        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (idCategoriaSeleccionada == 0)
            {
                MessageBox.Show("No hay ninguna categoría seleccionada.");
                return;
            }

            DesbloquearCampos();

            btnEditar.Visible = false;
            btnGuardarCambios.Visible = true;
        }

        private void btnGuardarCambios_Click(object sender, EventArgs e)
        {
            if (idCategoriaSeleccionada == 0)
            {
                MessageBox.Show("No hay ninguna categoría seleccionada.");
                return;
            }

            Categorias categoria = new Categorias();

            categoria.IdCategoria1 = idCategoriaSeleccionada;
            categoria.Nombre_Categoria1 = txtCategoria.Text;
            categoria.Descripción1 = txtDescripcion.Text;
            categoria.Estado1 = cbEstado.Text;

            categoria.ActualizarCategoria();

            MessageBox.Show("Categoría actualizada correctamente.");

            MostrarCategorias();

            txtCategoria.Clear();
            txtDescripcion.Clear();
            cbEstado.SelectedIndex = -1;

            BloquearCampos();

            btnGuardarCambios.Visible = false;
            btnGuardar.Visible = true;
            btnEditar.Visible = false;

            idCategoriaSeleccionada = 0;


        }

        private void DesactivarCopiarPegar(Control control)
        {
            foreach (Control elemento in control.Controls)
            {
                if (elemento is TextBox)
                {
                    ((TextBox)elemento).ShortcutsEnabled = false;
                }

                if (elemento.HasChildren)
                {
                    DesactivarCopiarPegar(elemento);
                }
            }
        }

        private void txtCategoria_KeyPress(object sender, KeyPressEventArgs e)
        {

            if (!char.IsLetterOrDigit(e.KeyChar) && !char.IsControl(e.KeyChar) && e.KeyChar != ' ')
            {
                e.Handled = true;
            }
        }

        private void txtDescripcion_KeyPress(object sender, KeyPressEventArgs e)
        {
            //letterOrDigith comprueba si es letra o numero
            // IsControl permite espacios
            // ´ ´solo se permite un espacio, no mas 
            // tmb permite ","
            //permite "."
            // permite "-"
            // y permite ()
            if (!char.IsLetterOrDigit(e.KeyChar) && !char.IsControl(e.KeyChar) && e.KeyChar != ' ' && e.KeyChar != ',' && e.KeyChar != '.' && e.KeyChar != '-' && e.KeyChar != '(' && e.KeyChar != ')')
            {
                e.Handled = true;
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                dgvCategorias.DataSource = null;
                dgvCategorias.DataSource = Categorias.Buscar(txtBuscarCategoria.Text);

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }


        }

        private void txtBuscarCategoria_TextChanged(object sender, EventArgs e)
        {
            if (txtBuscarCategoria.Text == "")
            {
                dgvCategorias.DataSource = Categorias.CargarCategorias();
            }
        }

        private void CargarEstadisticasCategorias()
        {

            lblCategoriasRegistradas.Text = Categorias.ContarCategoriasTotales().ToString();

            lblCategoriasActivas.Text = Categorias.ContarCategoriasActivas().ToString();

            lblCategoriasInactivas.Text = Categorias.ContarCategoriasInactivas().ToString();
        }

        private void btnNueva_Click(object sender, EventArgs e)
        {
            LimpiarFormulario();
        }
    }
}

