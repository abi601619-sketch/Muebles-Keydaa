using Modelo.Conexión_DB;
using Modelo.Entidades;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using Vista.Responsive;

namespace Vista.Producción

{
    public partial class frmMaterialUtilizado : Form
    {

        // VARIABLES DEL FORMULARIO
        private int idProduccion;
        private string producto;
        private DateTime fechaEntrega;

        // Tabla temporal donde se almacenan los materiales
        // agregados antes de guardarlos en la base de datos.
        private DataTable dtMateriales;
        public frmMaterialUtilizado(int idProduccion, string producto, DateTime fechaEntrega)
        {
            InitializeComponent();
            ResponsiveHelper.Apply(this);
            this.idProduccion = idProduccion;
            this.producto = producto;
            this.fechaEntrega = fechaEntrega;
            CargarMaterialesUtilizados(idProduccion);
        }
        //--------------------------------------------------------------------------
        // CONFIGURAR DISEÑO DE LA TABLA

        private void ConfigurarTablaMateriales()
        {
            // Encabezado
            dgvMaterialesAgregados.EnableHeadersVisualStyles = false;
            dgvMaterialesAgregados.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(121, 75, 45);
            dgvMaterialesAgregados.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvMaterialesAgregados.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9, FontStyle.Regular);
            dgvMaterialesAgregados.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvMaterialesAgregados.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.FromArgb(121, 75, 45);
            dgvMaterialesAgregados.ColumnHeadersDefaultCellStyle.SelectionForeColor = Color.White;

            // Filas
            dgvMaterialesAgregados.DefaultCellStyle.BackColor = Color.White;
            dgvMaterialesAgregados.DefaultCellStyle.ForeColor = Color.FromArgb(45, 45, 45);
            dgvMaterialesAgregados.DefaultCellStyle.Font = new Font("Segoe UI", 9, FontStyle.Regular);
            dgvMaterialesAgregados.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            // Filas alternadas
            dgvMaterialesAgregados.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(248, 241, 232);

            // Selección
            dgvMaterialesAgregados.DefaultCellStyle.SelectionBackColor = Color.FromArgb(224, 193, 157);
            dgvMaterialesAgregados.DefaultCellStyle.SelectionForeColor = Color.Black;

            // Bordes
            dgvMaterialesAgregados.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgvMaterialesAgregados.GridColor = Color.FromArgb(220, 220, 220);

            // Alto de las filas
            dgvMaterialesAgregados.RowTemplate.Height = 32;

            // Alto del encabezado
            dgvMaterialesAgregados.ColumnHeadersHeight = 30;

            // No permitir modificar
            dgvMaterialesAgregados.ReadOnly = true;
            dgvMaterialesAgregados.AllowUserToAddRows = false;
            dgvMaterialesAgregados.AllowUserToDeleteRows = false;

            // Seleccionar fila completa
            dgvMaterialesAgregados.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvMaterialesAgregados.MultiSelect = false;

            // Quitar borde exterior
            dgvMaterialesAgregados.BorderStyle = BorderStyle.None;

            // Ajustar el contenido
            dgvMaterialesAgregados.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            dgvMaterialesAgregados.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;

            // Centrar verticalmente el contenido
            dgvMaterialesAgregados.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
        }

        //--------------------------------------------------------------------------
        // CONFIGURAR TOOLTIPS

        private void ConfigurarTooltips()
        {
            ToolTip toolTip = new ToolTip();

            // Materiales
            toolTip.SetToolTip(cbMateriales, "Selecciona el material que deseas utilizar en la producción.");

            // Unidad de medida
            toolTip.SetToolTip(txtUnidadMedida, "Muestra la unidad de medida correspondiente al material seleccionado.");

            // Stock disponible
            toolTip.SetToolTip(txtStockDisponible, "Muestra la cantidad disponible del material seleccionado.");

            // Cantidad utilizada
            toolTip.SetToolTip(txtCantidadUtilizada, "Ingresa la cantidad del material que será utilizada en la producción.");

            // Agregar material
            toolTip.SetToolTip(btnAgregarMaterialUtilizado, "Agrega el material y la cantidad utilizada a la lista.");

            // Guardar consumo
            toolTip.SetToolTip(btnGuardarConsumo, "Guarda en la base de datos los materiales utilizados.");

            // Salir
            toolTip.SetToolTip(btnSalir, "Cierra el formulario de materiales utilizados.");

            // ID de producción
            toolTip.SetToolTip(txtIdProduccion, "Muestra el número de producción seleccionada.");

            // Mueble
            toolTip.SetToolTip(txtMuebleProduccion, "Muestra el producto o mueble asociado a la producción.");

            // Fecha de entrega
            toolTip.SetToolTip(dtpFechaEntrega, "Muestra la fecha de entrega establecida para la producción.");

            // Tabla
            toolTip.SetToolTip(dgvMaterialesAgregados, "Muestra los materiales utilizados en la producción.");
        }
        private void btnSalir_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void frmMaterialUtilizado_Load(object sender, EventArgs e)
        {

            // Crear la estructura de la tabla temporal
            InicializarTablaMateriales();

            // Cargar información de la producción
            CargarProduccion();

            // Cargar materiales en el ComboBox
            CargarComboBoxMateriales();

            // Configurar diseño de la tabla
            ConfigurarTablaMateriales();

            // Mostrar ToolTips
            ConfigurarTooltips();

            // Cargar materiales que ya están registrados
            CargarMaterialesUtilizados(idProduccion);

            // Configuración de controles de solo lectura
            txtMuebleProduccion.Enabled = false;
            txtStockDisponible.Enabled = false;
            txtUnidadMedida.Enabled = false;

            // Configuración del orden de navegación con TAB
            cbMateriales.TabIndex = 1;
            txtUnidadMedida.TabIndex = 2;
            txtCantidadUtilizada.TabIndex = 3;
        }

        //Aqui se inicaliza una tabla temporal en la que se guardaran los materiales
        private void InicializarTablaMateriales()
        {
            dtMateriales = new DataTable();

            // Crear las columnas que utilizará el DataGridView
            //Tambien typeof declara el tipo de valor que seran
            dtMateriales.Columns.Add("#", typeof(int));
            dtMateriales.Columns.Add("[N° Produccion]", typeof(int));
            dtMateriales.Columns.Add("Material", typeof(int));
            dtMateriales.Columns.Add("[Cantidad Utilizada]", typeof(int));
            dtMateriales.Columns.Add("Unidad", typeof(string));

            // Vincular la tabla con el DataGridView
            dgvMaterialesAgregados.DataSource = dtMateriales;
            dgvMaterialesAgregados.ReadOnly = true;
            dgvMaterialesAgregados.AllowUserToDeleteRows = false;
            dgvMaterialesAgregados.AllowUserToAddRows = false;
        }
        // Metodo para traer y cargar la informacion del formulario de producción
        private void CargarProduccion()
        {
            DbProducción produccion = new DbProducción();

            // Indicar qué producción queremos buscar
            produccion.IdProduccion1 = idProduccion;

            // Buscar la producción
            bool encontrado = produccion.ObtenerProduccion();

            if (encontrado)
            {
                // Mostrar el ID de producción
                txtIdProduccion.Text = produccion.IdProduccion1.ToString();

                // Mostrar el mueble
                txtMuebleProduccion.Text = produccion.Mueble1;

                // Mostrar la fecha de entrega
                dtpFechaEntrega.Value = produccion.FechaEntrega1;
            }
            else
            {
                MessageBox.Show("No se encontró la producción con ID: " + idProduccion, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        //Cargar ComboBox de materiales
        private void CargarComboBoxMateriales()
        {
            DataTable dtMaterial = Material.CargarMateriales();

            cbMateriales.DataSource = dtMaterial;

            // Texto que verá el usuario
            cbMateriales.DisplayMember = "Material";

            // Valor interno del ComboBox
            cbMateriales.ValueMember = "IdMaterial";

            // No seleccionar ningún material inicialmente
            cbMateriales.SelectedIndex = -1;
        }

        //METODO PARA CARGAR LOS MATERIALES UTILIZADOS
        private void CargarMaterialesUtilizados(int idProduccion)
        {
            DataTable dt = new DataTable();

            using (SqlConnection conectar = Conexion.Conectar())
            {
                string consulta = @" SELECT * FROM VerMaterialesUtilizados WHERE IdProduccion = @IdProduccion";

                using (SqlDataAdapter adapter = new SqlDataAdapter(consulta, conectar))
                {
                    adapter.SelectCommand.Parameters.AddWithValue("@IdProduccion", idProduccion);

                    adapter.Fill(dt);
                }
            }

            // Actualizar la tabla temporal con los registros
            // que ya existen en la base de datos.
            dtMateriales = dt;

            // Mostrar los registros en el DataGridView
            dgvMaterialesAgregados.DataSource = dtMateriales;
            dgvMaterialesAgregados.ReadOnly = true;
            dgvMaterialesAgregados.AllowUserToDeleteRows = false;
            dgvMaterialesAgregados.AllowUserToAddRows = false;
        }

        // CAMBIO DE MATERIAL SELECCIONADO

        private void cbMateriales_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Evitar errores mientras se está cargando el ComboBox
            if (cbMateriales.SelectedIndex == -1 || cbMateriales.SelectedItem == null)
            {
                txtStockDisponible.Clear();

                return;
            }
            try
            {
                DataRowView fila = cbMateriales.SelectedItem as DataRowView;

                if (fila == null)
                    return;

                // Obtener stock del material
                txtStockDisponible.Text = fila["Stock"].ToString();

                // Obtener unidad de medida
                string nombreUnidad = fila["UnidadMedida"].ToString();

                // Buscar esa unidad en el ComboBox
                txtUnidadMedida.Text = nombreUnidad;
            }
            catch
            {
                // Evitar errores mientras el ComboBox se está cargando
            }
        }

        // AGREGAR MATERIAL A LA LISTA
        private void btnAgregarMaterialUtilizado_Click_1(object sender, EventArgs e)
        {
            // VALIDAR MATERIAL SELECCIONADO

            if (cbMateriales.SelectedIndex == -1)
            {
                MessageBox.Show("Selecciona un material.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                return;
            }
            // VALIDAR CANTIDAD DEL MATERIAL

            if (!int.TryParse(txtCantidadUtilizada.Text, out int cantidad))
            {
                MessageBox.Show("Ingresa una cantidad válida.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                return;
            }

            // Validar que la cantidad sea mayor que cero
            if (cantidad <= 0)
            {
                MessageBox.Show("La cantidad debe ser mayor que cero.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                return;
            }

            // OBTENER STOCK

            if (!int.TryParse(txtStockDisponible.Text, out int stock))
            {
                MessageBox.Show("No se pudo obtener el stock del material.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }

            // VALIDAR QUE LA CANTIDAD UTILIZADA NO SUPERE EL STOCK DISPONIBLE

            if (cantidad > stock)
            {
                MessageBox.Show("La cantidad utilizada no puede ser mayor que el stock disponible.", "Stock insuficiente", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                return;
            }

            // OBTENER DATOS DEL MATERIAL

            DataRowView fila = (DataRowView)cbMateriales.SelectedItem;

            int idMaterial = Convert.ToInt32(cbMateriales.SelectedValue);

            string nombreMaterial = fila["Material"].ToString();

            // Mostrar la unidad de medida correspondiente
            txtUnidadMedida.Text = fila["UnidadMedida"].ToString();

            // AGREGAR CADA MATERIAL A LA TABLA TEMPORAL

            DataRow nueva = dtMateriales.NewRow();
            nueva["IdMaterialUtilizado"] = 0;
            nueva["IdProduccion"] = idProduccion;
            nueva["IdMaterial"] = idMaterial;
            nueva["NombreDelMaterial"] = nombreMaterial;
            nueva["Cantidad_Utilizada"] = cantidad;
            nueva["UnidadMedida"] = fila["UnidadMedida"];
            dtMateriales.Rows.Add(nueva);

            // Limpiar los controles después de agregar
            cbMateriales.SelectedIndex = -1;
            txtUnidadMedida.Clear();
            txtStockDisponible.Clear();
            txtCantidadUtilizada.Clear();
        }
        private void btnGuardarConsumo_Click(object sender, EventArgs e)
        {
            try
            //RECORRE TODAS LAS FILAS DEL DATA GRID VIEW
            {
                DataTable pendientes = dtMateriales.Clone();
                foreach (DataRow fila in dtMateriales.Rows)
                {
                    if (Convert.ToInt32(fila["IdMaterialUtilizado"]) == 0)
                        pendientes.ImportRow(fila);
                }
                if (pendientes.Rows.Count == 0)
                {
                    MessageBox.Show("Agrega un material antes de guardar.");
                    return;
                }
                MaterialUtilizado.GuardarConsumo(idProduccion, pendientes);
                // Marcar los nuevos registros como guardados incluso si falla la recarga.
                foreach (DataRow fila in dtMateriales.Rows)
                    if (Convert.ToInt32(fila["IdMaterialUtilizado"]) == 0)
                        fila["IdMaterialUtilizado"] = -1;
                CargarComboBoxMateriales();

                MessageBox.Show("Materiales guardados correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Volver a cargar los datos de SQL
                CargarMaterialesUtilizados(idProduccion);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al guardar los materiales:\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


    }
}
