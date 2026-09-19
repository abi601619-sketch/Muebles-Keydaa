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
        // Guarda el ID del material seleccionado para poder editarlo
        private int idMaterialSeleccionado = 0;
        public frmInventarioSecretario()
        {
            InitializeComponent();
            ResponsiveHelper.Apply(this);
        }
        // Evento LOAD Se ejecuta cuando se abre el formulario
        private void frmInventarioSecretario_Load(object sender, EventArgs e)
        {
            try
            {
                // Carga los datos iniciales del formulario
                MostrarInventario();
                CargarComboBoxCategorias();
                CargarComboBoxUnidadDeMedida();
                ConfigurarTooltips();

                // Desactiva copiar y pegar en los campos de texto
                DesactivarCopiarPegar(this);

                // Oculta los botones que no se necesitan al iniciar
                btnGuardarCambios.Visible = false;
                btnEditar.Visible = false;

                // Define el orden de navegación con la tecla Tab
                txtMaterial.TabIndex = 1;
                cbCategorias.TabIndex = 2;
                txtCantidad.TabIndex = 3;
                cbUnidadMedida.TabIndex = 4;
                btnGuardar.TabIndex = 5;
                btnEditar.TabIndex = 6;

                // Define el máximo de caracteres permitidos
                txtMaterial.MaxLength = 100;
                txtCantidad.MaxLength = 100000;

                // Deja el formulario listo para registrar un material
                LimpiarFormulario();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Error al cargar el inventario: " + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        //CONFIGURAR TOOLTIPS--------------------------
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
        //-------------------------------------------------------------------
        // Metodo que carga los materiales en el DataGridView
        private void MostrarInventario()
        {
            try
            {
                dgvMateriales.DataSource = null;
                dgvMateriales.DataSource = Material.CargarMateriales();

                // Cambia los nombres de los encabezados
                dgvMateriales.Columns["IdMaterial"].HeaderText = "#";
                dgvMateriales.Columns["UnidadMedida"].HeaderText = "Unidad de medida";

                // Actualiza las cantidades mostradas en las estadísticas
                CargarEstadisticasInventario();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Error al mostrar los materiales: " + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }
        //------------------------------------------------------------------------
        //COMO BOXS

        // Carga las categorías disponibles en el ComboBox de categorias
        private void CargarComboBoxCategorias()
        {
            try
            {
                DataTable dtCategoria = Categorias.CargarCategorias();

                cbCategorias.DataSource = dtCategoria;
                cbCategorias.DisplayMember = "Nombre_Categoria";
                cbCategorias.ValueMember = "IdCategoria";
                cbCategorias.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Error al cargar las categorías: " + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        //Combo box para mostrar las unidades de medida registradas en la base de datos
        private void CargarComboBoxUnidadDeMedida()
        {
            try
            {
                DataTable dtUnidadMedida =
                    UnidadMedida.CargarUnidadesDeMedida();

                cbUnidadMedida.DataSource = dtUnidadMedida;
                cbUnidadMedida.DisplayMember = "UnidadMedida";
                cbUnidadMedida.ValueMember = "IdUnidadMedida";
                cbUnidadMedida.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Error al cargar las unidades de medida: " + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }
        //---------------------------------------------------------------------------
        //Boton de registrar materiales
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                // Verifica que se haya escrito el nombre
                if (string.IsNullOrWhiteSpace(txtMaterial.Text))
                {
                    MessageBox.Show("Ingrese el nombre del material.");
                    txtMaterial.Focus();
                    return;
                }

                // Verifica que se haya seleccionado una unidad
                if (cbUnidadMedida.SelectedIndex == -1)
                {
                    MessageBox.Show("Seleccione la unidad de medida.");
                    cbUnidadMedida.Focus();
                    return;
                }

                // Verifica que se haya seleccionado una categoría
                if (cbCategorias.SelectedIndex == -1)
                {
                    MessageBox.Show("Seleccione una categoría.");
                    cbCategorias.Focus();
                    return;
                }

                // Verifica que se haya ingresado el stock
                if (string.IsNullOrWhiteSpace(txtCantidad.Text))
                {
                    MessageBox.Show("Ingrese el stock inicial.");
                    txtCantidad.Focus();
                    return;
                }

                // Verifica que el stock sea un número entero
                if (!int.TryParse(txtCantidad.Text, out int stock))
                {
                    MessageBox.Show("El stock inicial debe ser un número.");
                    txtCantidad.Focus();
                    return;
                }

                // Evita que se registren cantidades negativas
                if (stock < 0)
                {
                    MessageBox.Show("El stock inicial no puede ser negativo.");
                    txtCantidad.Focus();
                    return;
                }

                // Crea un nuevo objeto Material
                Material material = new Material();

                material.idMaterial1 = 0;
                material.NombreDelMaterial1 = txtMaterial.Text.Trim();
                material.UnidadDeMedida1 =
                    Convert.ToInt32(cbUnidadMedida.SelectedValue);
                material.Stock1 = stock;
                material.Categoria1 = cbCategorias.Text;

                // Guarda el material en la base de datos
                bool resultado = material.InsertarMateriales();

                if (resultado)
                {
                    MessageBox.Show(
                        "Material registrado correctamente.",
                        "Registro exitoso",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );

                    // Actualiza la tabla y limpia los campos
                    MostrarInventario();
                    LimpiarFormulario();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Error al registrar el material: " + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }
        //------------------------------------------------------------------
        //Metodo para editar
        private void dgvInventario_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                // Evita seleccionar el encabezado o una fila nueva
                if (e.RowIndex < 0 ||
                    dgvMateriales.Rows[e.RowIndex].IsNewRow)
                {
                    return;
                }

                DataGridViewRow fila = dgvMateriales.Rows[e.RowIndex];

                // Obtiene el ID del material seleccionado
                idMaterialSeleccionado =
                    Convert.ToInt32(fila.Cells["IdMaterial"].Value);

                // Muestra los datos del material en los controles
                txtMaterial.Text =
                    fila.Cells["Material"].Value?.ToString() ?? "";

                cbUnidadMedida.Text =
                    fila.Cells["UnidadMedida"].Value?.ToString() ?? "";

                txtCantidad.Text =
                    fila.Cells["Stock"].Value?.ToString() ?? "";

                cbCategorias.Text =
                    fila.Cells["Categoria"].Value?.ToString() ?? "";

                // Muestra el botón para editar
                btnEditar.Visible = true;

                // Bloquea los campos hasta presionar Editar
                BloquearCampos();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Error al seleccionar el material: " + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }
        // Permite modificar el material seleccionado


        private void btnEditar_Click(object sender, EventArgs e)
        {
            try
            {
                // Verifica que exista un material seleccionado
                if (idMaterialSeleccionado == 0)
                {
                    MessageBox.Show("Seleccione un material primero.");
                    return;
                }

                // Habilita los campos para modificarlos
                HabilitarCampos();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Error al habilitar la edición: " + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }

        }
        //-----------------------------------------------------------------------
        // Habilita los controles para editar
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
        // Bloquea los controles cuando no se está editando
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


        // Guarda los cambios realizados al material
        private void btnGuardarCambios_Click(object sender, EventArgs e)
        {
            try
            {
                // Verifica que exista un material seleccionado
                if (idMaterialSeleccionado == 0)
                {
                    MessageBox.Show("No hay ningún material seleccionado.");
                    return;
                }

                // Valida el nombre del material
                if (string.IsNullOrWhiteSpace(txtMaterial.Text))
                {
                    MessageBox.Show("Ingrese el nombre del material.");
                    txtMaterial.Focus();
                    return;
                }

                // Valida la unidad de medida
                if (cbUnidadMedida.SelectedIndex == -1)
                {
                    MessageBox.Show("Seleccione la unidad de medida.");
                    cbUnidadMedida.Focus();
                    return;
                }

                // Valida la categoría
                if (cbCategorias.SelectedIndex == -1)
                {
                    MessageBox.Show("Seleccione una categoría.");
                    cbCategorias.Focus();
                    return;
                }

                // Valida que se haya ingresado el stock
                if (string.IsNullOrWhiteSpace(txtCantidad.Text))
                {
                    MessageBox.Show("Ingrese el stock.");
                    txtCantidad.Focus();
                    return;
                }

                // Valida que el stock sea numérico
                if (!int.TryParse(txtCantidad.Text, out int stock))
                {
                    MessageBox.Show("El stock debe ser un número.");
                    txtCantidad.Focus();
                    return;
                }

                // Evita cantidades negativas
                if (stock < 0)
                {
                    MessageBox.Show("El stock no puede ser negativo.");
                    txtCantidad.Focus();
                    return;
                }

                // Crea el objeto con los datos modificados
                Material material = new Material();

                material.idMaterial1 = idMaterialSeleccionado;
                material.NombreDelMaterial1 = txtMaterial.Text.Trim();
                material.UnidadDeMedida1 =
                    Convert.ToInt32(cbUnidadMedida.SelectedValue);
                material.Stock1 = stock;
                material.Categoria1 = cbCategorias.Text;

                // Actualiza el material en la base de datos
                bool resultado = material.ActualizarMaterial();

                if (resultado)
                {
                    MessageBox.Show(
                        "Material actualizado correctamente.",
                        "Actualización exitosa",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );

                    // Actualiza la tabla y limpia los controles
                    MostrarInventario();
                    LimpiarFormulario();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Error al actualizar el material: " + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }
        //------------------------------------------------------------------------
        //METODO DE BUSQUEDA
        // Quita el texto de indicación del buscador
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
        // Vuelve a mostrar el texto de indicación
        private void txtBuscar_Leave(object sender, EventArgs e)
        {
            txtBuscar.Text = "Buscar Material...";
            txtBuscar.ForeColor = Color.Gray;
        }
        // Busca materiales mientras se escribe
        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            try
            {
                // No realiza la búsqueda si está el texto de indicación
                if (txtBuscar.Text == "Buscar Material...")
                {
                    return;
                }

                // Muestra los resultados encontrados
                dgvMateriales.DataSource =
                    Material.BuscarMaterial(txtBuscar.Text.Trim());
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Error al buscar el material: " + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }
        //----------------------------------------------------------------------
        // Carga las estadísticas del inventario
        private void CargarEstadisticasInventario()
        {
            try
            {
                lblTotalRegistrados.Text =
                    Material.ContarMaterialesTotales().ToString();

                lblAgotandose.Text =
                    Material.ContarMaterialesAgotandose().ToString();

                lblDisponibles.Text =
                    Material.ContarMaterialesDisponibles().ToString();

                lblMaterialesAgotados.Text =
                    Material.ContarMaterialesAgotados().ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Error al cargar las estadísticas del inventario: "
                    + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }
        //-----------------------------------------------------------
        // Prepara el formulario para registrar un nuevo material
        private void btnNuevo_Click(object sender, EventArgs e)
        {
            LimpiarFormulario();
        }

        // Limpia los campos del formulario
        private void LimpiarFormulario()
        {
            txtMaterial.Clear();
            txtCantidad.Clear();

            cbCategorias.SelectedIndex = -1;
            cbUnidadMedida.SelectedIndex = -1;

            idMaterialSeleccionado = 0;

            // Habilita los campos para un nuevo registro
            txtMaterial.ReadOnly = false;
            cbUnidadMedida.Enabled = true;
            cbCategorias.Enabled = true;
            txtCantidad.ReadOnly = false;

            // Configura los botones
            btnGuardar.Visible = true;
            btnEditar.Visible = false;
            btnGuardarCambios.Visible = false;

            txtMaterial.Focus();
        }
        //------------------------------------------------------------
        // Permite únicamente letras y espacios en el nombre
        private void txtMaterial_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) &&
              !char.IsControl(e.KeyChar) &&
              e.KeyChar != ' ')
            {
                e.Handled = true;
            }
        }
        // Permite únicamente números en el stock
        private void txtCantidad_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) &&
             !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }
        //---------------------------------------------------
        // Desactiva las opciones de copiar y pegar en los TextBox


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
