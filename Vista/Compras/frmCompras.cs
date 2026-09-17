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


        private int idDetalleEditando = 0;


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

            ConfigurarDetalleCompra();
            CargarComboBoxMateriales();
            CargarComboBoxProveedores();

            DesactivarCopiarPegar(this);

            btnGuardar.Visible = true;
            btnActualizarCompra.Visible = false;

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

            dgvHistorialCompras.Columns["IdCompra"].HeaderText = "N.º de compra";
            dgvHistorialCompras.Columns["FechaCompra"].HeaderText = "Fecha de compra";
            dgvHistorialCompras.Columns["Proveedor"].HeaderText = "Proveedor";
            dgvHistorialCompras.Columns["TotalCompra"].HeaderText = "Total de compra";

            dgvDetalleCompras.Columns["Material"].HeaderText = "Material";
            dgvDetalleCompras.Columns["Cantidad"].HeaderText = "Cantidad";
            dgvDetalleCompras.Columns["PrecioUnitario"].HeaderText = "Precio unitario";
            dgvDetalleCompras.Columns["Subtotal"].HeaderText = "Subtotal";



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

            //ID DETALLE DE COMPRA
            DataGridViewTextBoxColumn idDetalle = new DataGridViewTextBoxColumn();

            idDetalle.Name = "IdDetalleCompraMaterial";
            idDetalle.HeaderText = "IdDetalleCompraMaterial";
            idDetalle.Visible = false;

            dgvDetalleCompras.Columns.Add(idDetalle);

            // ELIMINAR
            DataGridViewButtonColumn eliminar = new DataGridViewButtonColumn();

            eliminar.Name = "Eliminar";
            eliminar.HeaderText = "Eliminar";
            eliminar.Text = "Eliminar";
            eliminar.UseColumnTextForButtonValue = true;

            dgvDetalleCompras.Columns.Add(eliminar);


            // Ajustar columnas
            dgvDetalleCompras.AutoSizeColumnsMode =
                DataGridViewAutoSizeColumnsMode.Fill;


            // Ajustar columnas
            dgvDetalleCompras.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
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

                int indice = dgvDetalleCompras.Rows.Add(detalle.IdMaterial1, nombreMaterial, detalle.Cantidad1, detalle.PrecioUnitario1.ToString("0.00"), subtotal.ToString("0.00"), detalle.IdDetalleCompraMaterial1);
                dgvDetalleCompras.Rows[indice].Tag = detalle;
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

            // Volver al modo nueva compra
            btnGuardar.Visible = true;
            btnActualizarCompra.Visible = false;
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

            int idCompra;
            try
            {
                idCompra = ComprasDb.GuardarCompleta(0, dtpFechaDeCompra.Value, idProveedor, detallesTemporales);
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se pudo guardar la compra: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
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

            modoEdicion = true;

            CargarCompraParaEditar(idCompraSeleccionada);


        }



        private void CargarCompraParaEditar(int idCompra)
        {

            btnGuardar.Visible = false;
            btnActualizarCompra.Visible = true;

            // Limpiar listas anteriores
            detallesOriginales.Clear();
            detallesTemporales.Clear();

            DataTable dtCompra = ComprasDb.ObtenerCompraPorId(idCompra);

            if (dtCompra == null || dtCompra.Rows.Count == 0)
            {
                MessageBox.Show("No se encontró la compra seleccionada.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }

            DataRow filaCompra = dtCompra.Rows[0];

            // Fecha
            dtpFechaDeCompra.Value = Convert.ToDateTime(filaCompra["FechaCompra"]);

            // Proveedor
            int idProveedor = Convert.ToInt32(filaCompra["IdProveedor"]);

            cbProveedor.SelectedValue = idProveedor;

            // Total
            totalCompra = Convert.ToDecimal(filaCompra["TotalCompra"]);

            txtTotalCompra.Text = totalCompra.ToString("0.00");


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
            if (!decimal.TryParse(txtPrecioUnitario.Text, out decimal precio))
            {
                MessageBox.Show("Ingresa un precio válido.");
                return;
            }

            int cantidad = Convert.ToInt32(nudCantidad.Value);

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

            // Mostrar nuevamente los materiales
            MostrarDetallesTemporales();

            // Limpiar SOLO los controles para ingresar/editar otro material
            cbMaterial.SelectedIndex = -1;
            nudCantidad.Value = 1;
            txtPrecioUnitario.Clear();

            // Reiniciar el detalle que se está editando
            idDetalleEditando = 0;



        }


        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (idCompraSeleccionada == 0)
            {
                MessageBox.Show("Seleccione una compra dando doble clic en la tabla inferior.");
                return;
            }

            DialogResult res = MessageBox.Show("¿Desea eliminar esta compra y todos sus materiales? Se revertirá el inventario que sumó esta compra, conservando los demás movimientos.", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
            if (res == DialogResult.Yes)
            {
                ComprasDb compra = new ComprasDb();
                compra.IdCompra1 = idCompraSeleccionada;
                if (compra.EliminarCompra())
                {
                    MessageBox.Show("Compra eliminada correctamente.");
                    MostrarCompras();
                    LimpiarCompra();
                    CargarComboBoxMateriales();
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





        private void btnActualizarCompra_Click(object sender, EventArgs e)
        {
            if (!modoEdicion || idCompraSeleccionada == 0)
            {
                MessageBox.Show(
                    "Primero selecciona una compra para editar.");
                return;
            }

            if (cbProveedor.SelectedIndex == -1)
            {
                MessageBox.Show("Selecciona un proveedor.");
                return;
            }

            if (detallesTemporales.Count == 0)
            {
                MessageBox.Show(
                    "La compra debe tener al menos un material.");
                return;
            }

            try
            {
                int idProveedor =
                    Convert.ToInt32(cbProveedor.SelectedValue);

                CalcularTotalCompra();

                ComprasDb.GuardarCompleta(idCompraSeleccionada, dtpFechaDeCompra.Value,
                    idProveedor, detallesTemporales);

                MessageBox.Show(
                    "Compra actualizada correctamente.",
                    "Compra",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                MostrarCompras();

                LimpiarCompra();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Ocurrió un error al actualizar la compra.\n\n" +
                    ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void btnNueva_Click(object sender, EventArgs e)
        {
            LimpiarCompra();
        }

        private void dgvDetalleCompras_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Validar fila y columna
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
                return;

            // Verificar que se hizo clic en el botón Eliminar
            if (dgvDetalleCompras.Columns[e.ColumnIndex].Name != "Eliminar")
                return;

            DetalleCompraMaterial detalle = dgvDetalleCompras.Rows[e.RowIndex].Tag as DetalleCompraMaterial;
            if (detalle == null)
                return;

            bool ultimo = detallesTemporales.Count == 1;
            // Confirmar eliminación
            DialogResult resultado = MessageBox.Show(
                ultimo && modoEdicion
                    ? "Al eliminar el último material se eliminará automáticamente el registro de esta compra y se revertirá todo el inventario que sumó. Los demás movimientos de inventario se conservarán. ¿Desea continuar?"
                    : "¿Está seguro de quitar este material? Los cambios se aplicarán al guardar o actualizar la compra.",
                "Confirmar eliminación",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning,
                MessageBoxDefaultButton.Button2
            );

            if (resultado != DialogResult.Yes)
                return;

            if (ultimo && modoEdicion)
            {
                var compra = new ComprasDb();
                compra.IdCompra1 = idCompraSeleccionada;
                if (!compra.EliminarCompra())
                    return;
                LimpiarCompra();
                MostrarCompras();
                CargarComboBoxMateriales();
                MessageBox.Show("Compra eliminada e inventario ajustado correctamente.");
                return;
            }

            // Eliminar de la lista temporal
            detallesTemporales.Remove(detalle);

            // Actualizar el DataGridView
            MostrarDetallesTemporales();

            MessageBox.Show(
                "Material quitado de la lista. Guarda o actualiza la compra para aplicar el cambio.",
                "Eliminación",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            );
        }
    }

}

