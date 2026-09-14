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
        // CARGA INICIAL DEL FORMULARIO
        private void frmCategorias_Load(object sender, EventArgs e)
        {
            try
            {
                // Carga las categorías y estadísticas
                MostrarCategorias();
                CargarEstadisticasCategorias();

                // Desactiva copiar y pegar
                DesactivarCopiarPegar(this);

                // Configuración inicial de botones y controles
                btnGuardarCambios.Visible = false;
                btnEditar.Visible = false;
                cbEstado.Enabled = false;

                // Máximo de caracteres permitidos
                txtCategoria.MaxLength = 50;
                txtDescripcion.MaxLength = 200;

                // Orden de navegación con la tecla Tab
                txtCategoria.TabIndex = 1;
                txtDescripcion.TabIndex = 2;
                cbEstado.TabIndex = 3;
                btnGuardar.TabIndex = 4;
                btnEditar.TabIndex = 5;

                // Configura los encabezados del DataGridView
                ConfigurarColumnas();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar las categorías: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //---------------------------------------------------------
        // MOSTRAR CATEGORÍAS
        public void MostrarCategorias()
        {
            try
            {
                dgvCategorias.DataSource = null;
                dgvCategorias.DataSource = Categorias.CargarCategorias();

                // Actualiza las estadísticas
                CargarEstadisticasCategorias();

                // Configura los nombres de las columnas
                ConfigurarColumnas();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Error al mostrar las categorías: " + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        // Configura los encabezados del DataGridView
        private void ConfigurarColumnas()
        {
            if (dgvCategorias.Columns.Count > 0)
            {
                dgvCategorias.Columns["IdCategoria"].HeaderText = "#";
                dgvCategorias.Columns["Nombre_Categoria"].HeaderText = "Categoría";
                dgvCategorias.Columns["Descripcion"].HeaderText = "Descripción";
            }
        }
        //--------------------------------------------------------------------------
        // REGISTRAR CATEGORÍA

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                // Valida el nombre de la categoría
                if (string.IsNullOrWhiteSpace(txtCategoria.Text))
                {
                    MessageBox.Show(
                        "Debe ingresar el nombre de la categoría."
                    );

                    txtCategoria.Focus();
                    return;
                }

                // Valida la descripción
                if (string.IsNullOrWhiteSpace(txtDescripcion.Text))
                {
                    MessageBox.Show(
                        "Debe ingresar la descripción de la categoría."
                    );

                    txtDescripcion.Focus();
                    return;
                }

                // Valida que se seleccione un estado
                if (cbEstado.SelectedIndex == -1)
                {
                    MessageBox.Show(
                        "Debe seleccionar el estado de la categoría."
                    );

                    cbEstado.Focus();
                    return;
                }

                // Crea el objeto categoría
                Categorias categoria = new Categorias();

                categoria.Nombre_Categoria1 =
                    txtCategoria.Text.Trim();

                categoria.Descripción1 =
                    txtDescripcion.Text.Trim();

                categoria.Estado1 = cbEstado.Text;

                // Inserta la categoría en la base de datos
                bool resultado = categoria.InsertarCategoria();

                if (resultado)
                {
                    MessageBox.Show(
                        "Categoría registrada correctamente.",
                        "Registro exitoso",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );

                    MostrarCategorias();
                    LimpiarFormulario();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Error al registrar la categoría: " + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        //----------------------------------------------------------------------
        // SELECCIONAR CATEGORÍA
        private int idCategoriaSeleccionada = 0;


        private void dgvCategorias_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                // Evita seleccionar el encabezado
                if (e.RowIndex < 0 ||
                    dgvCategorias.Rows[e.RowIndex].IsNewRow)
                {
                    return;
                }

                // Obtiene el ID de la categoría seleccionada
                idCategoriaSeleccionada =
                    Convert.ToInt32(
                        dgvCategorias.Rows[e.RowIndex]
                        .Cells["IdCategoria"].Value
                    );
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Error al seleccionar la categoría: " + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        // DOBLE CLIC PARA EDITAR
        private void dgvCategorias_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                // Evita seleccionar el encabezado
                if (e.RowIndex < 0 ||
                    dgvCategorias.Rows[e.RowIndex].IsNewRow)
                {
                    return;
                }

                DataGridViewRow fila =
                    dgvCategorias.Rows[e.RowIndex];

                // Obtiene el ID de la categoría
                idCategoriaSeleccionada =
                    Convert.ToInt32(
                        fila.Cells["IdCategoria"].Value
                    );

                // Carga los datos en los controles
                txtCategoria.Text =
                    fila.Cells["Nombre_Categoria"]
                    .Value?.ToString() ?? "";

                txtDescripcion.Text =
                    fila.Cells["Descripcion"]
                    .Value?.ToString() ?? "";

                cbEstado.Text =
                    fila.Cells["Estado"]
                    .Value?.ToString() ?? "";

                // Bloquea los campos hasta presionar Editar
                BloquearCampos();

                // Configura los botones
                btnEditar.Visible = true;
                btnGuardarCambios.Visible = false;
                btnGuardar.Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Error al cargar la categoría: " + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }

        }
        //------------------------------------------------------------
        // BOTON DE EDITAR CATEGORÍA
        private void btnEditar_Click(object sender, EventArgs e)
        {
            try
            {
                // Verifica que exista una categoría seleccionada
                if (idCategoriaSeleccionada == 0)
                {
                    MessageBox.Show(
                        "No hay ninguna categoría seleccionada."
                    );

                    return;
                }

                // Habilita los campos para editar
                DesbloquearCampos();

                // Cambia los botones
                btnEditar.Visible = false;
                btnGuardarCambios.Visible = true;
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
        //-----------------------------------------------------------


        // Habilita los campos para poder modificarlos
        private void DesbloquearCampos()
        {
            txtCategoria.ReadOnly = false;
            txtDescripcion.ReadOnly = false;
            cbEstado.Enabled = true;
        }


        // Bloquea los campos cuando no se está editando
        private void BloquearCampos()
        {
            txtCategoria.ReadOnly = true;
            txtDescripcion.ReadOnly = true;
            cbEstado.Enabled = false;
        }
        //--------------------------------------------------------------
        //GUARDAR CAMBIOS
        private void btnGuardarCambios_Click(object sender, EventArgs e)
        {
            try
            {
                // Verifica que exista una categoría seleccionada
                if (idCategoriaSeleccionada == 0)
                {
                    MessageBox.Show(
                        "No hay ninguna categoría seleccionada."
                    );

                    return;
                }

                // Valida el nombre
                if (string.IsNullOrWhiteSpace(txtCategoria.Text))
                {
                    MessageBox.Show(
                        "Debe ingresar el nombre de la categoría."
                    );

                    txtCategoria.Focus();
                    return;
                }

                // Valida la descripción
                if (string.IsNullOrWhiteSpace(txtDescripcion.Text))
                {
                    MessageBox.Show(
                        "Debe ingresar la descripción de la categoría."
                    );

                    txtDescripcion.Focus();
                    return;
                }

                // Valida el estado
                if (cbEstado.SelectedIndex == -1)
                {
                    MessageBox.Show(
                        "Debe seleccionar el estado de la categoría."
                    );

                    cbEstado.Focus();
                    return;
                }

                Categorias categoria = new Categorias();

                // Envía los datos de la categoría seleccionada
                categoria.IdCategoria1 = idCategoriaSeleccionada;
                categoria.Nombre_Categoria1 = txtCategoria.Text.Trim();
                categoria.Descripción1 = txtDescripcion.Text.Trim();
                categoria.Estado1 = cbEstado.Text;

                // Llama al método ActualizarCategoria
                bool resultado = categoria.ActualizarCategoria();

                if (resultado)
                {
                    MessageBox.Show(
                        "Categoría actualizada correctamente.",
                        "Actualización exitosa",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al actualizar la categoría: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error
                );
            }


        }
        //------------------------------------------------------
        //BUSQUEDA
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
            // Vuelve a mostrar el texto de indicación
            if (string.IsNullOrWhiteSpace(txtBuscarCategoria.Text))
            {
                txtBuscarCategoria.Text = "Buscar Categoría...";
                txtBuscarCategoria.ForeColor = Color.Gray;
            }
        }
        // Realiza la búsqueda de categorías
        private void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtBuscarCategoria.Text == "Buscar Categoría...")
                {
                    return;
                }

                dgvCategorias.DataSource = null;

                dgvCategorias.DataSource =
                    Categorias.Buscar(
                        txtBuscarCategoria.Text.Trim()
                    );

                ConfigurarColumnas();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Error al buscar la categoría: " + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }


        }
        // Actualiza la tabla cuando se borra el texto de búsqueda
        private void txtBuscarCategoria_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(
                    txtBuscarCategoria.Text))
                {
                    MostrarCategorias();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Error al actualizar la búsqueda: "
                    + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }


        //-------------------------------------------------------------

        // ESTADÍSTICAS
        private void CargarEstadisticasCategorias()
        {
            try
            {
                // Cantidad total de categorías
                lblCategoriasRegistradas.Text =
                    Categorias.ContarCategoriasTotales()
                    .ToString();

                // Cantidad de categorías activas
                lblCategoriasActivas.Text =
                    Categorias.ContarCategoriasActivas()
                    .ToString();

                // Cantidad de categorías inactivas
                lblCategoriasInactivas.Text =
                    Categorias.ContarCategoriasInactivas()
                    .ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Error al cargar las estadísticas: "
                    + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }
        //-----------------------------------------------------------------------
        // NUEVA CATEGORÍA
        private void btnNueva_Click(object sender, EventArgs e)
        {
            LimpiarFormulario();
        }
        //-----------------------------------------------------------------------
        // Limpia los controles para registrar una nueva categoría
        private void LimpiarFormulario()
        {
            txtCategoria.Clear();
            txtDescripcion.Clear();

            cbEstado.SelectedIndex = -1;

            idCategoriaSeleccionada = 0;

            // Habilita los campos
            txtCategoria.ReadOnly = false;
            txtDescripcion.ReadOnly = false;
            cbEstado.Enabled = true;

            // Configura los botones
            btnGuardar.Visible = true;
            btnEditar.Visible = false;
            btnGuardarCambios.Visible = false;

            txtCategoria.Focus();
        }
        //--------------------------------------------------------------------
        // VALIDACIONES DE TECLADO
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
        //---------------------------------------------------------------
        // DESACTIVAR COPIAR Y PEGAR
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

