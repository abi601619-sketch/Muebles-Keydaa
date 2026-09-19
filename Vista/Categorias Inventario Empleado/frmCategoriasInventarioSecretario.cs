using Modelo.Entidades;
using System;
using System.Drawing;
using System.Windows.Forms;
using Vista.Responsive;

namespace Vista.Categorias_Inventario_Empleado
{
    public partial class frmCategoriasInventarioSecretario : Form
    {
        public frmCategoriasInventarioSecretario()
        {
            InitializeComponent();
            ResponsiveHelper.Apply(this);
        }
        // CARGA INICIAL DEL FORMULARIO
        private void frmCategoriasInventarioSecretario_Load(object sender, EventArgs e)
        {
            try
            {
                // Carga las categorías y estadísticas
                MostrarCategorias();

                // Configura los encabezados del DataGridView
                ConfigurarColumnas();

                //Configurar tooltips
                ConfigurarTooltips();
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

        //CONFIGURAR TOOLTIPS---------------------------
        private void ConfigurarTooltips()
        {

            ToolTip toolTip = new ToolTip();

            // Configuración del ToolTip
            toolTip.AutoPopDelay = 5000;
            toolTip.InitialDelay = 500;
            toolTip.ReshowDelay = 200;
            toolTip.ShowAlways = true;
            // Buscador
            toolTip.SetToolTip(
                txtBuscarCategoria,
                "Ingrese el nombre de una categoría para buscarla."
            );

            toolTip.SetToolTip(
                btnBuscar,
                "Busca la categoría ingresada."
            );

        }
        //----------------------------------------------------------

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

    }
}
