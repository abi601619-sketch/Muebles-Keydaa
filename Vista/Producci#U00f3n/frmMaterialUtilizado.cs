using Vista.Responsive;
using Modelo.Conexión_DB;
using Modelo.Entidades;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Vista.Producción

{
    public partial class frmMaterialUtilizado : Form
    {
        private int idProduccion;
        private string producto;
        private DateTime fechaEntrega;



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

        private void CargarProduccion()
        {

            DbProducción produccion = new DbProducción();

            produccion.IdProduccion1 = idProduccion;

            bool encontrado = produccion.ObtenerProduccion();

            if (encontrado)
            {
                txtIdProduccion.Text = produccion.IdProduccion1.ToString();

                txtMuebleProduccion.Text = produccion.Mueble1;

                dtpFechaEntrega.Text = Convert.ToString(produccion.FechaEntrega1);
            }
            else
            {
                MessageBox.Show("NO se encontró la producción con ID: " + idProduccion, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }

        private void CargarComboBoxMateriales()
        {
            DataTable dtMaterial = Material.CargarMateriales();

            cbMateriales.DataSource = dtMaterial;
            cbMateriales.DisplayMember = "Material";
            cbMateriales.ValueMember = "IdMaterial";
            cbMateriales.SelectedIndex = -1;
        }

        private void CargarComboBoxUnidadDeMedida()
        {
            //Llamar al metodo de las unidades de medida
            DataTable dtUnidadMedida = UnidadMedida.CargarUnidadesDeMedida();
            cbUnidadesDeMedida.DisplayMember = "UnidadMedida";
            cbUnidadesDeMedida.ValueMember = "IdUnidadMedida";
            cbUnidadesDeMedida.SelectedIndex = -1;
        }

        private void frmMaterialUtilizado_Load(object sender, EventArgs e)
        {

            CargarComboBoxMateriales();
            CargarProduccion();
            CargarComboBoxUnidadDeMedida();
            CargarMaterialesUtilizados(idProduccion);

            //Text box de mueble y stock solo de vista, no se permite editar ni borrar
            txtMuebleProduccion.Enabled = false;
            txtStockDisponible.Enabled = false;

            //Poder navegar con la tecla Tab
            cbMateriales.TabIndex = 1;
            cbUnidadesDeMedida.TabIndex = 2;
            txtCantidadUtilizada.TabIndex = 3;
        }
        private DataTable dtMateriales;
        private void CargarMaterialesUtilizados(int idProduccion)
        {
            DataTable dt = new DataTable();

            using (SqlConnection conectar = Conexion.Conectar())
            {
                string consulta = @"
            SELECT *
            FROM VerMaterialesUtilizados
            WHERE IdProduccion = @IdProduccion";

                SqlDataAdapter adapter =
                    new SqlDataAdapter(consulta, conectar);

                adapter.SelectCommand.Parameters.AddWithValue(
                    "@IdProduccion",
                    idProduccion);

                adapter.Fill(dt);
            }

            dgvMaterialesAgregados.DataSource = dt;
        }



        private void btnAgregarMaterialUtilizado_Click(object sender, EventArgs e)
        {

            //Validar que se seleccione un material
            if (cbMateriales.SelectedIndex == -1)
            {
                MessageBox.Show("Selecciona un material.");
                return;
            }
            //Validad que la cantidad utilizada sea numero entero y no sea decimal
            if (!int.TryParse(txtCantidadUtilizada.Text, out int cantidad))
            {
                MessageBox.Show("Ingresa una cantidad válida.");
                return;
            }


            if (cantidad <= 0)
            {
                MessageBox.Show(
                    "La cantidad debe ser mayor que cero.",
                    "Aviso",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }



            string nombreMaterial = cbMateriales.Text;

            DataRowView fila = (DataRowView)cbMateriales.SelectedItem;

            string unidad = fila["NombreUnidad"].ToString();

            int idMaterial = Convert.ToInt32(cbMateriales.SelectedValue);

            dtMateriales.Rows.Add(0, idProduccion, idMaterial, nombreMaterial, cantidad, unidad);

        }

        private void btnGuardarConsumo_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (DataGridViewRow fila in dgvMaterialesAgregados.Rows)
                {
                    if (fila.IsNewRow)
                        continue;

                    int idMaterialUtilizado = Convert.ToInt32(fila.Cells["IdMaterialUtilizado"].Value);

                    // Si es 0, significa que es un material nuevo
                    if (idMaterialUtilizado == 0)
                    {
                        int idMaterial = Convert.ToInt32(fila.Cells["IdMaterial"].Value);

                        int cantidad = Convert.ToInt32(fila.Cells["Cantidad_Utilizada"].Value);

                        using (SqlConnection conectar = Conexion.Conectar())
                        {
                            string consulta = @"
                        INSERT INTO MaterialUtilizado(Cantidad_Utilizada, IdMaterial,IdProduccion)
                        VALUES(@Cantidad, @IdMaterial, @IdProduccion  )";

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
