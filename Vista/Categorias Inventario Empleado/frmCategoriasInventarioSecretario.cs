using Modelo.Entidades;
using System;
using System.Data;
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

        // DATOS PARA LA PAGINACIÓN
        private DataTable dtCategorias;
        private int paginaActual = 1;
        private int registrosPorPagina = 10;
        private int totalPaginas = 0;

        // CARGA INICIAL DEL FORMULARIO
        private void frmCategoriasInventarioSecretario_Load(object sender, EventArgs e)
        {
            try
            {
                // Carga las categorías y estadísticas
                MostrarCategorias();
                CargarEstadisticasCategorias();

                // CONFIGURAR TOOLTIPS
                ConfigurarTooltips();

                // Configuración inicial de la tabla
                ConfigurarColumnas();
                ConfigurarTablaCategorias();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar las categorías: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //-------------------------------------------------------------------------
        // CONFIGURAR TOOLTIPS
        private void ConfigurarTooltips()
        {
            ToolTip toolTip = new ToolTip();

            // Configuración del ToolTip
            toolTip.AutoPopDelay = 5000;
            toolTip.InitialDelay = 500;
            toolTip.ReshowDelay = 200;
            toolTip.ShowAlways = true;

            // Buscador
            toolTip.SetToolTip(txtBuscarCategoria, "Ingrese el nombre de una categoría para buscarla.");
            toolTip.SetToolTip(btnBuscar, "Busca la categoría ingresada.");

            // Paginación
            toolTip.SetToolTip(btnAnterior, "Muestra la página anterior.");
            toolTip.SetToolTip(btnSiguiente, "Muestra la página siguiente.");

            // Tabla
            toolTip.SetToolTip(dgvCategorias, "Muestra las categorías registradas.");
        }

        //---------------------------------------------------------
        // MOSTRAR CATEGORÍAS

        public void MostrarCategorias()
        {
            try
            {
                // Cargar todas las categorías
                dtCategorias = Categorias.CargarCategorias();

                // Iniciar desde la primera página
                paginaActual = 1;

                // Calcular cantidad de páginas
                CalcularPaginasCategorias();

                // Mostrar la primera página
                MostrarPaginaCategorias();

                // Actualiza las estadísticas
                CargarEstadisticasCategorias();

                // Configura los nombres de las columnas
                ConfigurarColumnas();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al mostrar las categorías: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //---------------------------------------------------------
        // CALCULAR PAGINACIÓN

        private void CalcularPaginasCategorias()
        {
            if (dtCategorias == null || dtCategorias.Rows.Count == 0)
            {
                totalPaginas = 1;
                paginaActual = 1;
                return;
            }

            totalPaginas = (int)Math.Ceiling((double)dtCategorias.Rows.Count / registrosPorPagina);

            if (totalPaginas == 0)
                totalPaginas = 1;

            if (paginaActual > totalPaginas)
                paginaActual = totalPaginas;
        }

        //---------------------------------------------------------
        // MOSTRAR PÁGINA DE CATEGORÍAS

        private void MostrarPaginaCategorias()
        {
            if (dtCategorias == null)
                return;

            DataTable dtPagina = dtCategorias.Clone();

            int inicio = (paginaActual - 1) * registrosPorPagina;

            int fin = Math.Min(inicio + registrosPorPagina, dtCategorias.Rows.Count);

            for (int i = inicio; i < fin; i++)
            {
                dtPagina.ImportRow(dtCategorias.Rows[i]);
            }

            // Mostrar únicamente los registros de la página actual
            dgvCategorias.DataSource = null;
            dgvCategorias.DataSource = dtPagina;

            // Configura los nombres de las columnas
            ConfigurarColumnas();

            // Configurar diseño de la tabla
            ConfigurarTablaCategorias();

            // Mostrar página actual
            lblPagina.Text = $"Página {paginaActual} de {totalPaginas}";

            // Activar o desactivar botones
            btnAnterior.Enabled = paginaActual > 1;

            btnSiguiente.Enabled = paginaActual < totalPaginas;
        }

        //---------------------------------------------------------
        // CONFIGURAR COLUMNAS

        private void ConfigurarColumnas()
        {
            if (dgvCategorias.Columns.Count > 0)
            {
                dgvCategorias.Columns["IdCategoria"].HeaderText = "#";
                dgvCategorias.Columns["Nombre_Categoria"].HeaderText = "Categoría";
                dgvCategorias.Columns["Descripcion"].HeaderText = "Descripción";
                dgvCategorias.Columns["Estado"].HeaderText = "Estado";
            }
        }


        //---------------------------------------------------------
        // CONFIGURAR DISEÑO DE LA TABLA
        private void ConfigurarTablaCategorias()
        {
            // Encabezado
            dgvCategorias.EnableHeadersVisualStyles = false;

            dgvCategorias.ColumnHeadersDefaultCellStyle.BackColor =
                Color.FromArgb(121, 75, 45);

            dgvCategorias.ColumnHeadersDefaultCellStyle.ForeColor =
                Color.White;

            dgvCategorias.ColumnHeadersDefaultCellStyle.Font =
                new Font("Times New Roman", 9, FontStyle.Regular);

            dgvCategorias.ColumnHeadersDefaultCellStyle.Alignment =
                DataGridViewContentAlignment.MiddleCenter;

            dgvCategorias.ColumnHeadersDefaultCellStyle.SelectionBackColor =
                Color.FromArgb(121, 75, 45);

            dgvCategorias.ColumnHeadersDefaultCellStyle.SelectionForeColor =
                Color.White;

            // Filas
            dgvCategorias.DefaultCellStyle.BackColor =
                Color.White;

            dgvCategorias.DefaultCellStyle.ForeColor =
                Color.FromArgb(45, 45, 45);

            dgvCategorias.DefaultCellStyle.Font =
                new Font("Segoe UI", 9, FontStyle.Regular);

            dgvCategorias.DefaultCellStyle.Alignment =
                DataGridViewContentAlignment.MiddleLeft;

            // Filas alternadas
            dgvCategorias.AlternatingRowsDefaultCellStyle.BackColor =
                Color.FromArgb(248, 241, 232);

            // Selección
            dgvCategorias.DefaultCellStyle.SelectionBackColor =
                Color.FromArgb(224, 193, 157);

            dgvCategorias.DefaultCellStyle.SelectionForeColor =
                Color.Black;

            // Bordes
            dgvCategorias.CellBorderStyle =
                DataGridViewCellBorderStyle.SingleHorizontal;

            dgvCategorias.GridColor =
                Color.FromArgb(220, 220, 220);

            // Alto de las filas
            dgvCategorias.RowTemplate.Height = 32;

            // Alto del encabezado
            dgvCategorias.ColumnHeadersHeight = 30;

            // No permitir modificar
            dgvCategorias.ReadOnly = true;

            dgvCategorias.AllowUserToAddRows = false;

            dgvCategorias.AllowUserToDeleteRows = false;

            // Seleccionar fila completa
            dgvCategorias.SelectionMode =
                DataGridViewSelectionMode.FullRowSelect;

            dgvCategorias.MultiSelect = false;

            // Quitar borde exterior
            dgvCategorias.BorderStyle =
                BorderStyle.None;

            dgvCategorias.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            // Ocultar cuadrito de la izquierda
            dgvCategorias.RowHeadersVisible = false;
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

                string textoBusqueda =
                    txtBuscarCategoria.Text.Trim();

                if (string.IsNullOrWhiteSpace(textoBusqueda))
                {
                    MostrarCategorias();
                    return;
                }

                dtCategorias = Categorias.Buscar(textoBusqueda);

                paginaActual = 1;

                CalcularPaginasCategorias();

                MostrarPaginaCategorias();

                ConfigurarColumnas();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al buscar la categoría: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                MessageBox.Show("Error al actualizar la búsqueda: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //-------------------------------------------------------------

        // ESTADÍSTICAS
        private void CargarEstadisticasCategorias()
        {
            try
            {
                lblCategoriasRegistradas.Text = Categorias.ContarCategoriasTotales().ToString();
                lblCategoriasActivas.Text = Categorias.ContarCategoriasActivas().ToString();
                lblCategoriasInactivas.Text = Categorias.ContarCategoriasInactivas().ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar las estadísticas: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnAnterior_Click(object sender, EventArgs e)
        {
            if (paginaActual > 1)
            {
                paginaActual--;

                MostrarPaginaCategorias();
            }
        }
        private void btnSiguiente_Click(object sender, EventArgs e)
        {
            if (paginaActual < totalPaginas)
            {
                paginaActual++;

                MostrarPaginaCategorias();
            }
        }
    }
}
