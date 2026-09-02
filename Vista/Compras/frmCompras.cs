using Modelo.Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Vista.Responsive;

namespace Vista.Compras
{
    public partial class frmCompras : Form
    {
        public frmCompras()
        {
            InitializeComponent();
            ResponsiveHelper.Apply(this);
        }

        // VARIABLES PARA COMPRA Y DETALLES

        // Id de la compra que estamos editando.
        // 0 significa que estamos creando una compra nueva.
        private int idCompraSeleccionada = 0;

        // Indica si estamos creando o editando.
        private bool modoEdicion = false;

        // Detalles que el usuario est creando/modificando.
        private List<DetalleCompraMaterial> detallesTemporales = new List<DetalleCompraMaterial>();

        // Detalles originales de una compra cuando se carga para editar.
        private List<DetalleCompraMaterial> detallesOriginales = new List<DetalleCompraMaterial>();

        private int IdCompraSeleccionada = 0;
        private bool modoDeEdicion = false;
        private DataTable dtMateriales;





        private decimal totalCompra = 0;

        private void txtBuscar_Enter(object sender, EventArgs e)
        {
            //Cuando el usuario de enter para escribir, se va a borrar el texto de indicacion
            // Y el texto ya no sera opaco, sera color negro
            if (txtBuscar.Text == "Buscar Compra...")
            {
                txtBuscar.Text = "";
                txtBuscar.ForeColor = Color.Black;

            }


        }

        private void txtBuscar_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtBuscar.Text))
            {
                txtBuscar.Text = "Buscar compra...";
                txtBuscar.ForeColor = Color.Gray;
            }

        }

        private void frmCompras_Load(object sender, EventArgs e)
        {
            MostrarCompras();
            CargarComboBoxMateriales();
            CargarComboBoxProveedores();
            ConfigurarDetalleCompra();
            DesactivarCopiarPegar(this);

            nudCantidad.Minimum = 1;
            nudCantidad.Value = 1;

            txtTotalCompra.Text = "0.00";
            //Navegar con TabIndex
            cbProveedor.TabIndex = 0;
            dtpFechaDeCompra.TabIndex = 1;
            cbMaterial.TabIndex = 2;
            nudCantidad.TabIndex = 3;
            txtPrecioUnitario.TabIndex = 4;
            btnAgregarProductos.TabIndex = 5;
            btnActualizar.TabIndex = 6;

            // No se podra seleccionar fechas futuras
            dtpFechaDeCompra.MaxDate = DateTime.Today;
            dtpFechaDeCompra.Value = DateTime.Today;


            dgvHistorialCompras.DataSource = ComprasDb.CargarComprasRegistradas();
        }
        private void MostrarCompras()
        {
            dgvHistorialCompras.DataSource = null;
            dgvHistorialCompras.DataSource = ComprasDb.CargarComprasRegistradas();
        }



        private void CargarComboBoxMateriales()
        {
            DataTable dtMaterial = Material.CargarMateriales();

            cbMaterial.DataSource = dtMaterial;
            cbMaterial.DisplayMember = "Material";
            cbMaterial.ValueMember = "IdMaterial";
            cbMaterial.SelectedIndex = -1;
        }

        private void CargarComboBoxProveedores()
        {
            DataTable dtProveedor = DbProveedor.CargarProveedor();

            cbProveedor.DataSource = dtProveedor;
            cbProveedor.DisplayMember = "Proveedor";
            cbProveedor.ValueMember = "IdProveedor";
            cbProveedor.SelectedIndex = -1;
        }


        private string ObtenerNombreMaterial(int idMaterial)
        {
            DataTable dt = Material.CargarMateriales();

            foreach (DataRow fila in dt.Rows)
            {
                if (Convert.ToInt32(fila["IdMaterial"]) == idMaterial)
                {
                    return fila["Material"].ToString();
                }
            }

            return "";
        }

        private void ConfigurarDetalleCompra()
        {
            dgvDetalleCompras.Columns.Clear();

            dgvDetalleCompras.AutoGenerateColumns = false;
            dgvDetalleCompras.AllowUserToAddRows = false;
            dgvDetalleCompras.ReadOnly = true;

            dgvDetalleCompras.ColumnHeadersVisible = true;
            dgvDetalleCompras.RowHeadersVisible = false;

            // ID MATERIAL
            DataGridViewTextBoxColumn idMaterial =
                new DataGridViewTextBoxColumn();

            idMaterial.Name = "IdMaterial";
            idMaterial.HeaderText = "IdMaterial";
            idMaterial.Visible = false;

            dgvDetalleCompras.Columns.Add(idMaterial);


            // MATERIAL
            DataGridViewTextBoxColumn material = new DataGridViewTextBoxColumn();

            material.Name = "Material";
            material.HeaderText = "Material";

            dgvDetalleCompras.Columns.Add(material);


            // CANTIDAD
            DataGridViewTextBoxColumn cantidad = new DataGridViewTextBoxColumn();

            cantidad.Name = "Cantidad";
            cantidad.HeaderText = "Cantidad";

            dgvDetalleCompras.Columns.Add(cantidad);


            // PRECIO UNITARIO
            DataGridViewTextBoxColumn precio = new DataGridViewTextBoxColumn();

            precio.Name = "PrecioUnitario";
            precio.HeaderText = "Precio Unitario";

            dgvDetalleCompras.Columns.Add(precio);


            // SUBTOTAL
            DataGridViewTextBoxColumn subtotal = new DataGridViewTextBoxColumn();

            subtotal.Name = "Subtotal";
            subtotal.HeaderText = "Subtotal";

            dgvDetalleCompras.Columns.Add(subtotal);

            DataGridViewTextBoxColumn idDetalle = new DataGridViewTextBoxColumn();

            idDetalle.Name = "IdDetalleCompraMaterial";
            idDetalle.HeaderText = "IdDetalleCompraMaterial";
            idDetalle.Visible = false;

            dgvDetalleCompras.Columns.Add(idDetalle);


            // Ajustar columnas
            dgvDetalleCompras.AutoSizeColumnsMode =
                DataGridViewAutoSizeColumnsMode.Fill;
        }


        private void btnAgregarProductos_Click_1(object sender, EventArgs e)
        {
            if (cbMaterial.SelectedIndex == -1)
            {
                MessageBox.Show("Selecciona un material.");
                return;
            }

            if (string.IsNullOrWhiteSpace(txtPrecioUnitario.Text))
            {
                MessageBox.Show("Ingresa el precio unitario.");
                txtPrecioUnitario.Focus();
                return;
            }

            if (!decimal.TryParse(txtPrecioUnitario.Text, out decimal precio))
            {
                MessageBox.Show("Ingresa un precio vlido.");
                txtPrecioUnitario.Focus();
                return;
            }

            if (precio <= 0)
            {
                MessageBox.Show("El precio unitario debe ser mayor que 0.");
                return;
            }

            int cantidad = Convert.ToInt32(nudCantidad.Value);

            if (cantidad <= 0)
            {
                MessageBox.Show("La cantidad debe ser mayor que 0.");
                return;
            }

            int idMaterial = Convert.ToInt32(cbMaterial.SelectedValue);

            // Verificar si ya est en la lista
            foreach (DetalleCompraMaterial detalle
            in detallesTemporales)
            {
                if (detalle.IdMaterial1 == idMaterial)
                {
                    MessageBox.Show("Este material ya est agregado.");

                    return;
                }
            }
            string nombreMaterial = cbMaterial.Text;

            // Crear detalle
            DetalleCompraMaterial nuevoDetalle = new DetalleCompraMaterial(0, idCompraSeleccionada, idMaterial, cantidad, precio);

            // Agregar a la lista temporal
            detallesTemporales.Add(nuevoDetalle);

            // Mostrar en el DataGridView
            MostrarDetallesTemporales();

            // Limpiar controles
            cbMaterial.SelectedIndex = -1;
            nudCantidad.Value = 1;
            txtPrecioUnitario.Clear();

        }

        private void MostrarDetallesTemporales()
        {
            dgvDetalleCompras.Rows.Clear();

            foreach (DetalleCompraMaterial detalle in detallesTemporales)
            {
                string nombreMaterial = ObtenerNombreMaterial(detalle.IdMaterial1);

                decimal subtotal =
                    detalle.Cantidad1 * detalle.PrecioUnitario1;

                dgvDetalleCompras.Rows.Add(detalle.IdMaterial1, nombreMaterial, detalle.Cantidad1, detalle.PrecioUnitario1.ToString("0.00"), subtotal.ToString("0.00")
                );
            }

            CalcularTotalCompra();
        }
        private void CalcularTotalCompra()
        {
            totalCompra = 0;

            foreach (DetalleCompraMaterial detalle in detallesTemporales)
            {
                totalCompra += detalle.Cantidad1 * detalle.PrecioUnitario1;
            }

            txtTotalCompra.Text = totalCompra.ToString("0.00");
        }


        private void LimpiarCompra()
        {
            cbProveedor.SelectedIndex = -1;
            cbMaterial.SelectedIndex = -1;

            nudCantidad.Value = 1;

            txtPrecioUnitario.Clear();

            dtpFechaDeCompra.Value = DateTime.Today;

            detallesTemporales.Clear();
            detallesOriginales.Clear();

            dgvDetalleCompras.Rows.Clear();

            idCompraSeleccionada = 0;
            modoEdicion = false;

            totalCompra = 0;

            txtTotalCompra.Text = "0.00";
        }


        private void btnGuardar_Click(object sender, EventArgs e)
        {
            // Validar que se seleccion un proveedor
            if (cbProveedor.SelectedIndex == -1)
            {
                MessageBox.Show("Selecciona un proveedor."); cbProveedor.Focus();
                return;
            }

            // Validar que haya materiales
            if (dgvDetalleCompras.Rows.Count == 0)
            {
                MessageBox.Show("Agrega al menos un material.", "Validacin", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            // Validar fecha
            if (dtpFechaDeCompra.Value == null)
            {
                MessageBox.Show("Debe ingresar la fecha de la compra.", "Validacin", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                dtpFechaDeCompra.Focus();
                return;
            }

            // Volver a calcular el total
            CalcularTotalCompra();

            int idProveedor = Convert.ToInt32(cbProveedor.SelectedValue);

            // Crear el registro de compra
            ComprasDb compra = new ComprasDb(0, dtpFechaDeCompra.Value, totalCompra, idProveedor);

            // Insertar la compra

            int idCompra = compra.InsertarCompra();

            if (idCompra == 0)
            {
                MessageBox.Show("No se pudo registrar la compra.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                return;
            }

            foreach (DetalleCompraMaterial detalle in detallesTemporales)
            {
                // Dar el IdCompra que acabamos de obtener
                detalle.IdCompra1 = idCompra;


                // Guardar detalle
                bool resultadoExitoso = detalle.InsertarDetalleCompra();


                if (!resultadoExitoso)
                {
                    MessageBox.Show("Ocurri un error al guardar " +
                        "el detalle de la compra.");

                    return;
                }

                // ACTUALIZAR STOCK

                Material material = new Material();

                material.idMaterial1 = detalle.IdMaterial1;


                bool stockActualizado = material.ActualizarStock(detalle.Cantidad1);


                if (!stockActualizado)
                {
                    MessageBox.Show("El detalle se guard, pero no se pudo " +
                        "actualizar el stock del material.");

                    return;
                }
            }


            MessageBox.Show("Compra registrada correctamente.\n\n" + "Nmero de compra: " + idCompra, "Compra", MessageBoxButtons.OK, MessageBoxIcon.Information);

            // Actualizar historial de compras ya actualizado
            MostrarCompras();

            // Limpiar formulario de compras
            LimpiarCompra();
        }

        private void dgvHistorialCompras_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {

            if (e.RowIndex < 0 || ((DataGridView)sender).Rows[e.RowIndex].IsNewRow)
                return;

            idCompraSeleccionada = Convert.ToInt32(dgvHistorialCompras.Rows[e.RowIndex].Cells["IdCompra"].Value);

            CargarCompraParaEditar(idCompraSeleccionada);
        }



        private void CargarCompraParaEditar(int idCompra)
        {
            // Limpiar listas anteriores
            detallesOriginales.Clear();
            detallesTemporales.Clear();

            // CARGAR DETALLES DE LA COMPRA

            DataTable dt = DetalleCompraMaterial.CargarDetallesPorCompra(idCompra);


            foreach (DataRow fila in dt.Rows)
            {
                DetalleCompraMaterial detalle = new DetalleCompraMaterial(Convert.ToInt32(fila["IdDetalleCompraMaterial"]),

                        Convert.ToInt32(fila["IdCompra"]),

                        Convert.ToInt32(fila["IdMaterial"]),

                        Convert.ToInt32(fila["Cantidad"]),

                        Convert.ToDecimal(fila["PrecioUnitario"])
                    );

                detallesOriginales.Add(detalle);
            }

            // COPIAR ORIGINALES A TEMPORALES


            foreach (DetalleCompraMaterial original in detallesOriginales)
            {
                DetalleCompraMaterial temporal = new DetalleCompraMaterial(original.IdDetalleCompraMaterial1, original.IdCompra1, original.IdMaterial1, original.Cantidad1, original.PrecioUnitario1
            );

                detallesTemporales.Add(temporal);
            }



            // MOSTRAR TEMPORALES

            MostrarDetallesTemporales();

        }

        private int idDetalleEditando = 0;

        private void dgvDetalleCompras_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || ((DataGridView)sender).Rows[e.RowIndex].IsNewRow)
                return;

            DataGridViewRow fila = dgvDetalleCompras.Rows[e.RowIndex];
            idDetalleEditando = Convert.ToInt32(fila.Cells["IdDetalleCompraMaterial"].Value);

            int idMaterial = Convert.ToInt32(fila.Cells["IdMaterial"].Value);

            int cantidad = Convert.ToInt32(fila.Cells["Cantidad"].Value);

            decimal precio = Convert.ToDecimal(fila.Cells["PrecioUnitario"].Value);

            // Pasar informacin al formulario de la izquierda
            cbMaterial.SelectedValue = idMaterial;

            nudCantidad.Value = cantidad;

            txtPrecioUnitario.Text = precio.ToString("0.00");
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {

            if (idDetalleEditando == 0)
            {
                MessageBox.Show("Selecciona un detalle para editar.");
                return;
            }

            if (cbMaterial.SelectedIndex == -1)
            {
                MessageBox.Show("Selecciona un material.");
                return;
            }

            if (string.IsNullOrWhiteSpace(txtPrecioUnitario.Text))
            {
                MessageBox.Show("Ingresa el precio unitario.");
                return;
            }

            if (!decimal.TryParse(txtPrecioUnitario.Text, out decimal precio))
            {
                MessageBox.Show("Ingresa un precio vlido.");
                return;
            }

            if (precio <= 0)
            {
                MessageBox.Show("El precio debe ser mayor que 0.");
                return;
            }

            int cantidad = Convert.ToInt32(nudCantidad.Value);

            if (cantidad <= 0)
            {
                MessageBox.Show("La cantidad debe ser mayor que 0.");
                return;
            }

            int idMaterial = Convert.ToInt32(cbMaterial.SelectedValue);

            // Buscar el detalle que estamos editando
            foreach (DetalleCompraMaterial detalle in detallesTemporales)
            {
                if (detalle.IdDetalleCompraMaterial1 == idDetalleEditando)
                {
                    detalle.IdMaterial1 = idMaterial;
                    detalle.Cantidad1 = cantidad;
                    detalle.PrecioUnitario1 = precio;

                    break;
                }
            }

            // Volver a mostrar los detalles
            MostrarDetallesTemporales();

            // Limpiar seleccin de edicin
            idDetalleEditando = 0;

            cbMaterial.SelectedIndex = -1;
            nudCantidad.Value = 1;
            txtPrecioUnitario.Clear();

            cbMaterial.Focus();
        }


        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (idCompraSeleccionada == 0)
            {
                MessageBox.Show("Seleccione una compra dando doble clic en la tabla inferior.");
                return;
            }

            DialogResult res = MessageBox.Show("Est seguro de eliminar esta compra permanentemente? Se eliminarn tambin sus detalles.", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (res == DialogResult.Yes)
            {
                Modelo.Entidades.ComprasDb compra = new Modelo.Entidades.ComprasDb();
                compra.IdCompra1 = idCompraSeleccionada;
                if (compra.EliminarCompra())
                {
                    MessageBox.Show("Compra eliminada correctamente.");
                    MostrarCompras();
                    idCompraSeleccionada = 0;
                }
                else
                {
                    MessageBox.Show("Error al eliminar la compra.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
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

        private void txtPrecioUnitario_KeyPress(object sender, KeyPressEventArgs e)
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
                // Si el texto no realiza alguna busqueda, recarga la tabla
                if (txtBuscar.Text == "Buscar compra...")
                    return;

                dgvHistorialCompras.DataSource = ComprasDb.Buscar(txtBuscar.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {

        }
    }

}

