using Modelo.Entidades;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Vista.Responsive;

namespace Vista.Inventario
{
    public partial class frmInventario : Form
    {
        // PAGINACIÓN
        private DataTable dtInventario;
        private int paginaActual = 1;
        private int registrosPorPagina = 20;
        private int totalPaginas = 0;
        public frmInventario()
        {
            InitializeComponent();
            ResponsiveHelper.Apply(this);
        }

        private void CargarPaginacion()
        {
            try
            {
                // Cargar todos los materiales
                dtInventario = Material.CargarMateriales();

                // Calcular el total de páginas
                totalPaginas = (int)Math.Ceiling((double)dtInventario.Rows.Count / registrosPorPagina);

                // Si no hay registros
                if (totalPaginas == 0)
                {
                    totalPaginas = 1;
                }

                // Evitar que la página actual sea mayor al total
                if (paginaActual > totalPaginas)
                {
                    paginaActual = totalPaginas;
                }

                MostrarPagina();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar el inventario: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void MostrarPagina()
        {
            if (dtInventario == null)
                return;

            DataTable dtPagina = dtInventario.Clone();

            int inicio = (paginaActual - 1) * registrosPorPagina;

            int fin = Math.Min(inicio + registrosPorPagina, dtInventario.Rows.Count);

            for (int i = inicio; i < fin; i++)
            {
                dtPagina.ImportRow(dtInventario.Rows[i]);
            }

            dgvMateriales.DataSource = dtPagina;

            // Encabezados de las columnas
            if (dgvMateriales.Columns.Contains("IdMaterial"))
                dgvMateriales.Columns["IdMaterial"].HeaderText = "#";

            if (dgvMateriales.Columns.Contains("UnidadMedida"))
                dgvMateriales.Columns["UnidadMedida"].HeaderText = "Unidad de medida";

            // Mostrar página actual
            lblPagina.Text = $"Página {paginaActual} de {totalPaginas}";

            // Activar/desactivar botones
            btnAnterior.Enabled = paginaActual > 1;
            btnSiguiente.Enabled = paginaActual < totalPaginas;
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

            if (string.IsNullOrWhiteSpace(txtBuscar.Text))
            {
                txtBuscar.Text = "Buscar Material...";
                txtBuscar.ForeColor = Color.Gray;
            }
        }

        private void MostrarInventario()
        {
            paginaActual = 1;

            CargarPaginacion();

            CargarEstadisticasInventario();
        }

        private void btnAnterior_Click(object sender, EventArgs e)
        {
            if (paginaActual > 1)
            {
                paginaActual--;
                MostrarPagina();
            }
        }

        private void btnSiguiente_Click(object sender, EventArgs e)
        {
            if (paginaActual < totalPaginas)
            {
                paginaActual++;
                MostrarPagina();
            }
        }

        // CONFIGURAR TOOLTIPS
        private void ConfigurarTooltips()
        {
            ToolTip toolTip = new ToolTip();



            // Buscador
            toolTip.SetToolTip(
                txtBuscar,
                "Busca un material por su nombre."
            );

            // Datos del material
            toolTip.SetToolTip(
                txtMaterial,
                "Ingrese el nombre del material."
            );

            toolTip.SetToolTip(
                cbCategorias,
                "Seleccione la categoría a la que pertenece el material."
            );

            toolTip.SetToolTip(
                cbUnidadMedida,
                "Seleccione la unidad de medida del material."
            );

            toolTip.SetToolTip(
                txtCantidad,
                "Ingrese la cantidad disponible del material."
            );

            // Botones
            toolTip.SetToolTip(
                btnNuevo,
                "Limpia el formulario para registrar un nuevo material."
            );

            toolTip.SetToolTip(
                btnGuardar,
                "Guarda el nuevo material en el inventario."
            );

            toolTip.SetToolTip(
                btnEditar,
                "Permite modificar los datos del material seleccionado."
            );

            toolTip.SetToolTip(
                btnGuardarCambios,
                "Guarda los cambios realizados al material."
            );

            // Tabla
            toolTip.SetToolTip(
                dgvMateriales,
                "Muestra los materiales registrados en el inventario. Haz doble clic en un material para seleccionarlo."
            );

            // Estadísticas
            toolTip.SetToolTip(
                lblTotalRegistrados,
                "Cantidad total de materiales registrados."
            );

            toolTip.SetToolTip(
                lblAgotandose,
                "Cantidad de materiales cuyo stock está próximo a agotarse."
            );

            toolTip.SetToolTip(
                lblDisponibles,
                "Cantidad de materiales disponibles actualmente."
            );

            toolTip.SetToolTip(
                lblMaterialesAgotados,
                "Cantidad de materiales que se encuentran agotados."
            );
        }

        private void frmInventario_Load(object sender, EventArgs e)
        {
            MostrarInventario();
            CargarComboBoxCategorias();
            CargarComboBoxUnidadDeMedida();
            DesactivarCopiarPegar(this);
            ConfigurarTooltips();
            btnGuardarCambios.Visible = false;
            btnEditar.Visible = false;

            //Navegar con la tecla Tab
            txtMaterial.TabIndex = 1;
            cbCategorias.TabIndex = 2;
            txtCantidad.TabIndex = 3;
            cbUnidadMedida.TabIndex = 4;
            btnGuardar.TabIndex = 5;
            btnEditar.TabIndex = 6;

            //Maximo de caracteres admitidos
            txtMaterial.MaxLength = 100;

            txtCantidad.MaxLength = 100000;

            CargarEstadisticasInventario();
        }

        //Combo box para cargar categorias
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
            DataTable dtUnidades = new DataTable();
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
            material.UnidadDeMedida1 = Convert.ToInt32(cbUnidadMedida.SelectedValue);
            material.Stock1 = 0;
            material.Categoria1 = cbCategorias.Text;

            bool resultado = material.InsertarMateriales();

            if (resultado)
            {
                MessageBox.Show("Material registrado correctamente.", "Registro exitoso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            MostrarInventario();
            LimpiarFormulario();


        }
        private int idMaterialSeleccionado = 0;
        private void dgvInventario_CellClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (idMaterialSeleccionado == 0)
            {
                MessageBox.Show("Seleccione un material primero.");
                return;
            }
            HabilitarCampos();
            btnGuardarCambios.Visible = true;



        }
        private void BloquearCampos()
        {
            txtMaterial.ReadOnly = true;
            cbUnidadMedida.Enabled = false;
            cbCategorias.Enabled = false;
            txtCantidad.ReadOnly = true;

            btnEditar.Visible = true;
            btnGuardar.Visible = false;


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
            material.UnidadDeMedida1 = Convert.ToInt32(cbUnidadMedida.SelectedValue);
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
            CargarEstadisticasInventario();
            LimpiarFormulario();
        }

        private void dgvMateriales_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {

            if (e.RowIndex < 0 || dgvMateriales.Rows[e.RowIndex].IsNewRow)
                return;

            DataGridViewRow fila = dgvMateriales.Rows[e.RowIndex];

            idMaterialSeleccionado = Convert.ToInt32(fila.Cells["IdMaterial"].Value);

            txtMaterial.Text = fila.Cells["Material"].Value?.ToString() ?? "";

            cbUnidadMedida.Text = fila.Cells["UnidadMedida"].Value?.ToString() ?? "";

            txtCantidad.Text = fila.Cells["Stock"].Value?.ToString() ?? "";

            cbCategorias.Text = fila.Cells["Categoria"].Value?.ToString() ?? "";

            btnEditar.Visible = true;

            dgvMateriales.Columns["IdMaterial"].HeaderText = "#";
            dgvMateriales.Columns["UnidadMedida"].HeaderText = "Unidad de medida";

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

        private void txtMaterial_KeyPress(object sender, KeyPressEventArgs e)
        {
            txtMaterial.MaxLength = 100;
            if (!char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar) && e.KeyChar != ' ')
            {
                e.Handled = true;
            }
        }

        private void txtCantidad_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }



        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtBuscar.Text == "Buscar Material...")
                    return;
                dgvMateriales.DataSource = Material.BuscarMaterial(txtBuscar.Text);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }


        }

        //METODO PARA CARGAR LAS ESTADISTICAS
        private void CargarEstadisticasInventario()
        {
            try
            {
                lblTotalRegistrados.Text = Material.ContarMaterialesTotales().ToString();
                lblAgotandose.Text = Material.ContarMaterialesAgotandose().ToString();
                lblDisponibles.Text = Material.ContarMaterialesDisponibles().ToString();
                lblMaterialesAgotados.Text = Material.ContarMaterialesAgotados().ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar las estadísticas del inventario: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            LimpiarFormulario();
        }

        private void LimpiarFormulario()
        {
            txtMaterial.Clear();

            txtCantidad.Clear();

            btnEditar.Visible = false;
            btnGuardarCambios.Visible = false;
            cbCategorias.SelectedIndex = -1;
            cbUnidadMedida.SelectedIndex = -1;

            txtMaterial.Focus();
        }


    }
}




