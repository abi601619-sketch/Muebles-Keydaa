using Modelo.Entidades;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Vista.Responsive;

namespace Vista.Iventario_Secretario
{
    public partial class frmInventarioSecretario : Form
    {
        public frmInventarioSecretario()
        {
            InitializeComponent();
            ResponsiveHelper.Apply(this);
        }

        private void txtBuscar_Enter(object sender, EventArgs e)
        {
            //Cuando el usuario de enter para escribir, se va a borrar el texto de indicacion
            // Y el texto ya no sera opaco, sera color negro
            if (txtBuscar.Text == "Buscar Material...")
            {
                txtBuscar.Text = "";
                txtBuscar.ForeColor = Color.Black;

            }
        }

        private void txtBuscar_Leave(object sender, EventArgs e)
        {
            txtBuscar.Text = "Buscar Material...";
            txtBuscar.ForeColor = Color.Gray;
        }

        public void MostrarInventario()
        {
            dgvMateriales.DataSource = null;
            dgvMateriales.DataSource = Material.CargarMateriales();
        }

        private void frmInventarioSecretario_Load(object sender, EventArgs e)
        {
            MostrarInventario();
            CargarComboBoxCategorias();
            DesactivarCopiarPegar(this);
            btnGuardarCambios.Visible = false;
            //Navegacion con tab
            txtMaterial.TabIndex = 1;
            cbCategorias.TabIndex = 2;
            txtCantidad.TabIndex = 3;
            cbUnidadMedida.TabIndex = 4;
            btnGuardar.TabIndex = 5;

            //Maximo de caracteres admitidos
            txtMaterial.MaxLength = 100;
        }

        private void CargarComboBoxCategorias()
        {
            //Llamar al metodo de las categorias
            DataTable dtCategoria = Categorias.CargarCategorias();
            cbCategorias.DataSource = dtCategoria;
            cbCategorias.DisplayMember = "Nombre_Categoria";
            cbCategorias.ValueMember = "IdCategoria";
            cbCategorias.SelectedIndex = -1;
        }

        //Combo box para mostrar las unidades de medida registradas en la base de datos
        private void CargarComboBoxUnidadDeMedida()
        {
            //Llamar al metodo de las unidades de medida
            DataTable dtUnidadMedida = UnidadMedida.CargarUnidadesDeMedida();
            cbUnidadMedida.DataSource = dtUnidadMedida;
            cbUnidadMedida.DisplayMember = "UnidadMedida";
            cbUnidadMedida.ValueMember = "IdUnidadMedida";
            cbUnidadMedida.SelectedIndex = -1;
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            // Validar nombre del material
            if (string.IsNullOrWhiteSpace(txtMaterial.Text))
            {
                MessageBox.Show("Ingrese el nombre del material.");
                txtMaterial.Focus();
                return;
            }

            // Validar unidad de medida
            if (string.IsNullOrWhiteSpace(cbUnidadMedida.Text))
            {
                MessageBox.Show("Ingrese la unidad de medida.");
                cbUnidadMedida.Focus();
                return;
            }

            // Validar categora
            if (cbCategorias.SelectedIndex == -1)
            {
                MessageBox.Show("Seleccione una categora.");
                cbCategorias.Focus();
                return;
            }

            // Validar stock inicial
            if (string.IsNullOrWhiteSpace(txtCantidad.Text))
            {
                MessageBox.Show("Ingrese el stock inicial.");
                txtCantidad.Focus();
                return;
            }

            // Validar que el stock sea numrico
            if (!int.TryParse(txtCantidad.Text, out int stock))
            {
                MessageBox.Show("El stock inicial debe ser un nmero.");
                txtCantidad.Focus();
                return;
            }
            // Validar que no sea negativo
            if (stock < 0)
            {
                MessageBox.Show("El stock inicial no puede ser negativo.");
                txtCantidad.Focus();
                return;
            }


            //INSERT
            Material material = new Material();

            material.idMaterial1 = 0;
            material.NombreDelMaterial1 = txtMaterial.Text;
            material.UnidadDeMedida1 = cbUnidadMedida.Text;
            material.Stock1 = 0;
            material.Categoria1 = cbCategorias.Text;

            bool resultado = material.InsertarMateriales();

            if (resultado)
            {
                MessageBox.Show("Material registrado correctamente.", "Registro exitoso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            MostrarInventario();
        }

        private int idMaterialSeleccionado = 0;

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (idMaterialSeleccionado == 0)
            {
                MessageBox.Show("Seleccione un material primero.");
                return;
            }
            HabilitarCampos();

        }

        private void BloquearCampos()
        {
            txtMaterial.ReadOnly = true;
            cbUnidadMedida.Enabled = false;
            cbCategorias.Enabled = false;
            txtCantidad.ReadOnly = true;

            btnEditar.Visible = true;
            btnGuardar.Visible = false;

            btnGuardarCambios.Visible = true;
        }

        private void HabilitarCampos()
        {
            txtMaterial.ReadOnly = false;
            cbUnidadMedida.Enabled = true;
            cbCategorias.Enabled = true;
            txtCantidad.ReadOnly = false;

            btnEditar.Visible = true;
            btnGuardar.Visible = false;

            btnGuardarCambios.Visible = true;
        }

        private void btnGuardarCambios_Click(object sender, EventArgs e)
        {
            if (idMaterialSeleccionado == 0)
            {
                MessageBox.Show("No hay ningun material seleccionado.");
                return;
            }

            Material material = new Material();

            material.idMaterial1 = idMaterialSeleccionado;
            material.NombreDelMaterial1 = txtMaterial.Text;
            material.UnidadDeMedida1 = cbUnidadMedida.Text;
            material.Stock1 = Convert.ToInt32(txtCantidad.Text);
            material.Categoria1 = cbCategorias.Text;

            if (material.ActualizarMaterial())
            {
                MessageBox.Show("Material actualizado correctamente.");

                MostrarInventario();

                btnGuardarCambios.Visible = false;
                btnEditar.Visible = true;
                btnGuardar.Visible = true;

                txtMaterial.ReadOnly = true;
                cbUnidadMedida.Enabled = false;
                txtCantidad.ReadOnly = true;
                cbCategorias.Enabled = false;

                idMaterialSeleccionado = 0;
            }
        }

        private void dgvInventario_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || dgvMateriales.Rows[e.RowIndex].IsNewRow)
                return;

            DataGridViewRow fila = dgvMateriales.Rows[e.RowIndex];

            idMaterialSeleccionado = Convert.ToInt32(fila.Cells["IdMaterial"].Value);

            txtMaterial.Text = fila.Cells["Material"].Value?.ToString() ?? "";

            string colUnidad = "";
            foreach (DataGridViewColumn col in dgvMateriales.Columns)
            {
                if (col.Name.Contains("Unidad"))
                {
                    colUnidad = col.Name;
                    break;
                }
            }

            cbUnidadMedida.Text = fila.Cells["UnidadMedida"].Value?.ToString() ?? "";

            txtCantidad.Text = fila.Cells["Stock"].Value?.ToString() ?? "";

            cbCategorias.Text = fila.Cells["Categoria"].Value?.ToString() ?? "";

            btnEditar.Visible = true;
            btnGuardar.Visible = true;

            BloquearCampos();
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
    }

}
