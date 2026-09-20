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
        // PAGINACIÓN
        private DataTable dtProduccion;
        private int paginaActual = 1;
        private int registrosPorPagina = 20;
        private int totalPaginas = 0;
        private DataTable dtProduccionOriginal;

        public frmProduccion()
        {
            InitializeComponent();
            ResponsiveHelper.Apply(this);
        }
        // CARGA INICIAL DEL FORMULARIO
        private void frmProduccion_Load(object sender, EventArgs e)
        {
            try
            {
                // Carga las producciones
                MostrarProduccion();

                // Ajusta el texto y el tamaño de las filas
                dgvProduccion.DefaultCellStyle.WrapMode =
                    DataGridViewTriState.True;

                dgvProduccion.AutoSizeRowsMode =
                    DataGridViewAutoSizeRowsMode.AllCells;

                // Carga las estadísticas
                ActualizarEstadisticas();

                //Mostrar Tooltips
                ConfigurarTooltips();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Error al cargar producción: " + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }
        //CONFIGURAR TOOLTIPS----------------------------------------------------------
        private void ConfigurarTooltips()
        {
            ToolTip toolTip = new ToolTip();

            // Buscador
            toolTip.SetToolTip(
                txtBuscar,
                "Busca una producción por código o nombre del cliente."
            );

            // Filtro por estado
            toolTip.SetToolTip(
                cbEstados,
                "Selecciona un estado para filtrar las producciones."
            );

            // Limpiar filtros
            toolTip.SetToolTip(
                btnLimpiar,
                "Limpia el buscador y los filtros aplicados."
            );

            // Editar producción
            toolTip.SetToolTip(
                btnEditar,
                "Edita la producción seleccionada."
            );

            // Material utilizado
            toolTip.SetToolTip(
                btnMaterialUtilizado,
                "Consulta los materiales utilizados en la producción seleccionada."
            );

            // Tabla de producciones
            toolTip.SetToolTip(
                dgvProduccion,
                "Muestra las producciones registradas y su información."
            );

            // Estadísticas
            toolTip.SetToolTip(
                lblMostrarRegistrados,
                "Cantidad total de producciones registradas."
            );

            toolTip.SetToolTip(
                lblMostrarPendientes,
                "Cantidad de producciones pendientes."
            );

            toolTip.SetToolTip(
                lblMostrarEnProduccion,
                "Cantidad de producciones que se encuentran en proceso."
            );

            toolTip.SetToolTip(
                lblMostrarFinalizados,
                "Cantidad de producciones finalizadas."
            );
        }
        //---------------------------------------------------------
        // CONFIGURAR COLUMNAS
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
        //---------------------------------------------------------------
        // MOSTRAR PRODUCCIÓN
        public void MostrarProduccion()
        {

            try
            {
                // Obtiene las producciones de la base de datos
                dtProduccionOriginal = DbProducción.CargarProducción();

                dtProduccion = dtProduccionOriginal.Copy();

                paginaActual = 1;

                CalcularPaginas();

                MostrarPaginaProduccion();

                dgvProduccion.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Error al mostrar las producciones: " + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        private void CalcularPaginas()
        {
            int totalRegistros = dtProduccion.Rows.Count;

            totalPaginas = (int)Math.Ceiling(
                (double)totalRegistros / registrosPorPagina
            );

            if (totalPaginas == 0)
            {
                totalPaginas = 1;
            }

            if (paginaActual > totalPaginas)
            {
                paginaActual = totalPaginas;
            }
        }

        private void MostrarPaginaProduccion()
        {
            try
            {
                if (dtProduccion == null)
                    return;

                DataTable dtPagina = dtProduccion.Clone();

                int inicio = (paginaActual - 1) * registrosPorPagina;

                int fin = Math.Min(
                    inicio + registrosPorPagina,
                    dtProduccion.Rows.Count
                );

                for (int i = inicio; i < fin; i++)
                {
                    dtPagina.ImportRow(dtProduccion.Rows[i]);
                }

                dgvProduccion.DataSource = null;
                dgvProduccion.DataSource = dtPagina;

                // Configura las columnas
                ConfigurarColumnasProduccion();

                // Mostrar página actual
                lblPagina.Text =
                    $"Página {paginaActual} de {totalPaginas}";

                // Activar/desactivar botones
                btnAnterior.Enabled =
                    paginaActual > 1;

                btnSiguiente.Enabled =
                    paginaActual < totalPaginas;

                // Ajusta el texto y el tamaño de las filas
                dgvProduccion.DefaultCellStyle.WrapMode =
                    DataGridViewTriState.True;

                dgvProduccion.AutoSizeRowsMode =
                    DataGridViewAutoSizeRowsMode.AllCells;
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Error al mostrar la página: " + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }
        //------------------------------------------------------------------
        // EDITAR PRODUCCIÓN

        private void btnEditar_Click(object sender, EventArgs e)
        {
            try
            {
                // Verifica que exista una producción seleccionada
                if (dgvProduccion.CurrentRow == null)
                {
                    MessageBox.Show("Selecciona una producción.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    return;
                }

                // Obtiene el ID de la producción
                int idProduccion = Convert.ToInt32(dgvProduccion.CurrentRow.Cells["IdProduccion"].Value);

                // Abre el formulario de edición
                frmEditarProduccion formulario = new frmEditarProduccion(idProduccion);

                DialogResult resultado = formulario.ShowDialog();

                // Actualiza la tabla si se guardaron cambios
                if (resultado == DialogResult.OK)
                {
                    MostrarProduccion();
                    ActualizarEstadisticas();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al editar la producción: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        //_---------------------------------------------------------------------------------------
        // BUSCAR PRODUCCIÓN

        // Quita el texto de indicación
        private void txtBuscar_Enter(object sender, EventArgs e)
        {
            if (txtBuscar.Text == "Buscar por código o nombre de cliente...")
            {
                txtBuscar.Text = "";
                txtBuscar.ForeColor = Color.Black;
            }
        }

        // Vuelve a mostrar el texto de indicación
        private void txtBuscar_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtBuscar.Text))
            {
                txtBuscar.Text =
                    "Buscar por código o nombre de cliente...";

                txtBuscar.ForeColor = Color.Gray;
            }
        }


        // Filtra la tabla según el estado y búsqueda
        private void FiltrarTabla()
        {
            try
            {
                if (dtProduccionOriginal == null)
                    return;

                string estado = cbEstados.Text;

                string buscar =
                    txtBuscar.Text == "Buscar por código o nombre de cliente..."
                    ? ""
                    : txtBuscar.Text.Trim();

                string filtro = "1=1";

                // Filtra por estado
                if (!string.IsNullOrWhiteSpace(estado) && estado != "Todos")
                {
                    filtro +=
                        " AND Estado = '" +
                        estado.Replace("'", "''") +
                        "'";
                }

                // Filtra por cliente, producción o pedido
                if (!string.IsNullOrWhiteSpace(buscar))
                {
                    buscar = buscar.Replace("'", "''");

                    filtro +=
                        " AND (Cliente LIKE '%" + buscar + "%' OR " +
                        "Convert(IdProduccion, 'System.String') LIKE '%" + buscar + "%' OR " +
                        "Convert(IdPedido, 'System.String') LIKE '%" + buscar + "%')";
                }

                // Aplicar filtro sobre los datos originales
                DataView vista = new DataView(dtProduccionOriginal);

                vista.RowFilter = filtro;

                // Guardar resultados filtrados
                dtProduccion = vista.ToTable();

                // Volver a la primera página
                paginaActual = 1;

                // Calcular total de páginas
                CalcularPaginas();

                // Mostrar resultados
                MostrarPaginaProduccion();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Error al filtrar las producciones: " + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        // Ejecuta el filtro cuando cambia el estado
        private void cbEstados_SelectedIndexChanged(object sender, EventArgs e)
        {
            FiltrarTabla();
        }
        // Ejecuta el filtro cuando cambia el texto
        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            if (txtBuscar.Text != "Buscar por código o nombre de cliente...")
            {
                FiltrarTabla();
            }
        }
        //------------------------------------------------------------------------------
        // LIMPIAR FILTROS

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            try
            {
                // Reinicia el ComboBox
                cbEstados.SelectedIndex = -1;

                // Reinicia el buscador
                txtBuscar.Text = "Buscar por código o nombre de cliente...";

                txtBuscar.ForeColor = Color.Gray;

                // Elimina el filtro de la tabla
                if (dgvProduccion.DataSource is DataTable dt)
                {
                    dt.DefaultView.RowFilter = "";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al limpiar los filtros: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //---------------------------------------------------------------------------
        // MATERIAL UTILIZADO
        private void btnMaterialUtilizado_Click(object sender, EventArgs e)
        {

            try
            {
                // Verifica que exista una producción seleccionada
                if (dgvProduccion.CurrentRow == null)
                {
                    MessageBox.Show("Selecciona una producción.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    return;
                }

                // Obtiene el ID de la producción
                int idProduccion = Convert.ToInt32(dgvProduccion.CurrentRow.Cells["IdProduccion"].Value);

                // Obtiene el nombre del producto
                string producto = dgvProduccion.CurrentRow.Cells["Producto"].Value?.ToString() ?? "";

                // Obtiene la fecha de entrega
                DateTime fechaEntrega = Convert.ToDateTime(dgvProduccion.CurrentRow.Cells["Fecha de Entrega"].Value);

                // Abre el formulario de materiales utilizados
                frmMaterialUtilizado formulario = new frmMaterialUtilizado(idProduccion, producto, fechaEntrega);

                formulario.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al mostrar los materiales utilizados: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        //-------------------------------------------------------------------
        // ESTADÍSTICAS

        //Metodo para actualizarlas
        private void ActualizarEstadisticas()
        {
            try
            {
                // Total de producciones
                lblMostrarRegistrados.Text = DbProducción.ContarProduccionesTotales().ToString();

                // Producciones pendientes
                lblMostrarPendientes.Text = DbProducción.ContarProduccionesPendientes().ToString();

                // Producciones en proceso
                lblMostrarEnProduccion.Text = DbProducción.ContarProduccionesEnProceso().ToString();

                // Producciones finalizadas
                lblMostrarFinalizados.Text = DbProducción.ContarProduccionesFinalizadas().ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al actualizar las estadísticas: " + ex.Message, "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
    }
}



