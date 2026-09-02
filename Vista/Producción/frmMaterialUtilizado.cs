using Modelo.Conexión_DB;
using Modelo.Entidades;
using System;
using System.Data;
using System.Data.SqlClient;
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
                txtStockDisponible.Text =
                    fila["Stock"].ToString();

                // Obtener unidad de medida
                string nombreUnidad =
                    fila["NombreUnidad"].ToString();

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
            txtUnidadMedida.Text = fila["NombreUnidad"].ToString();



            // AGREGAR CADA MATERIAL A LA TABLA TEMPORAL

            dtMateriales.Rows.Add(0, idProduccion, idMaterial, nombreMaterial, cantidad);

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
                foreach (DataGridViewRow fila in dgvMaterialesAgregados.Rows)
                {
                    if (fila.IsNewRow)
                        continue;

                    int idMaterialUtilizado = Convert.ToInt32(fila.Cells["IdMaterialUtilizado"].Value);

                    // Si el Id del material es 0, significa que es un material nuevo
                    if (idMaterialUtilizado == 0)
                    {
                        int idMaterial = Convert.ToInt32(fila.Cells["IdMaterial"].Value);

                        int cantidad = Convert.ToInt32(fila.Cells["Cantidad_Utilizada"].Value);

                        using (SqlConnection conectar = Conexion.Conectar())
                        {
                            string consulta = @"INSERT INTO MaterialUtilizado(Cantidad_Utilizada, IdMaterial,IdProduccion) VALUES(@Cantidad, @IdMaterial, @IdProduccion  )";

                            SqlCommand comando =
                                new SqlCommand(consulta, conectar);

                            comando.Parameters.AddWithValue("@Cantidad", cantidad);

                            comando.Parameters.AddWithValue("@IdMaterial", idMaterial);

                            comando.Parameters.AddWithValue("@IdProduccion", idProduccion);

                            comando.ExecuteNonQuery();
                        }
                    }
                }

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
