using Modelo.Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;
using Vista.Responsive;

namespace Vista.Ventas
{
    public partial class frmVentas : Form
    {
        // Lista temporal donde se guardarn los productos
        // antes de guardar la venta en la base de datos
        private List<DetalleVenta> detallesVenta = new List<DetalleVenta>();

        public frmVentas()
        {
            InitializeComponent();
            ResponsiveHelper.Apply(this);
        }

        private void panel6_Paint(object sender, PaintEventArgs e)
        {

        }

        private void txtBuscar_Enter(object sender, EventArgs e)
        {
            //Cuando el usuario de enter para escribir, se va a borrar el texto de indicacion
            // Y el texto ya no sera opaco, sera color negro
            if (txtBuscar.Text == "Buscar Venta...")
            {
                txtBuscar.Text = "";
                txtBuscar.ForeColor = Color.Black;

            }
        }

        private void txtBuscar_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtBuscar.Text))
            {
                txtBuscar.Text = "Buscar venta...";
                txtBuscar.ForeColor = Color.Gray;
            }
        }
        public void MostrarVentas()
        {
            dgvVentas.DataSource = null;
            dgvVentas.DataSource = DbVentas.CargarVentas();
        }

        private void frmVentas_Load(object sender, EventArgs e)
        {
            // Mostrar las ventas existentes
            MostrarVentas();

            // Cargar clientes
            CargarComboBoxClientes();

            // Cargar metodos de pago
            CargarComboBoxMetodosDePago();

            //Validacion de fecha
            dtFechaVenta.MaxDate = DateTime.Today;

            //Navegar con la tecla Tab
            cbCliente.TabIndex = 1;
            dtFechaVenta.TabIndex = 2;
            cbMetodoPago.TabIndex = 3;
            btnAgregarProductos.TabIndex = 4;
            txtSubTotal.TabIndex = 5;
            txtTotalPagar.TabIndex = 6;
            btnGuardar.TabIndex = 7;
            btnEditar.TabIndex = 8;
            btnEliminar.TabIndex = 9;
        }

        int idVentaSeleccionada = 0;
        private void dgvVentas_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || dgvVentas.Rows[e.RowIndex].IsNewRow) return;
            DataGridViewRow row = dgvVentas.Rows[e.RowIndex];
            idVentaSeleccionada = Convert.ToInt32(row.Cells["IdVenta"].Value);


            // Buscar los nombres exactos de las columnas en el grid
            string colCliente = "";
            string colMetodo = "";
            string colFecha = "";
            foreach (DataGridViewColumn col in dgvVentas.Columns)
            {
                if (col.Name.Contains("Cliente")) colCliente = col.Name;
                if (col.Name.Contains("Metodo") || col.Name.Contains("Pago")) colMetodo = col.Name;
                if (col.Name.Contains("Fecha")) colFecha = col.Name;
            }

            if (!string.IsNullOrEmpty(colCliente)) cbCliente.Text = row.Cells[colCliente]?.Value?.ToString() ?? "";
            if (!string.IsNullOrEmpty(colMetodo)) cbMetodoPago.Text = row.Cells[colMetodo]?.Value?.ToString() ?? "";

            if (!string.IsNullOrEmpty(colFecha) && row.Cells[colFecha]?.Value != DBNull.Value)
                dtFechaVenta.Value = Convert.ToDateTime(row.Cells[colFecha].Value);

            txtSubTotal.Text = row.Cells["SubTotal"]?.Value?.ToString() ?? "0";
            btnEditar.Visible = true;
            btnGuardar.Visible = false;
            btnGuardarCambios.Visible = true;
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (idVentaSeleccionada == 0)
            {
                MessageBox.Show("Seleccione una venta para editar.");
                return;
            }

        }

        private void CalcularTotal()
        {
            decimal subtotal = 0;

            decimal.TryParse(
                txtSubTotal.Text,
                NumberStyles.Any,
                CultureInfo.InvariantCulture,
                out subtotal);

            decimal iva = subtotal * 0.13m;

            decimal total = subtotal + iva;

            txtIVA.Text = iva.ToString("0.00", CultureInfo.InvariantCulture);
            txtTotalPagar.Text = total.ToString("0.00", CultureInfo.InvariantCulture);
        }


        private void btnGuardar_Click(object sender, EventArgs e)
        {


            // VALIDAR CLIENTE

            if (cbCliente.SelectedIndex == -1)
            {
                MessageBox.Show(
                    "Seleccione un cliente.",
                    "Campo requerido",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            // VALIDAR METODO DE PAGO

            if (cbMetodoPago.SelectedIndex == -1)
            {
                MessageBox.Show(
                    "Seleccione un método de pago.",
                    "Campo requerido",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }


            // VALIDAR QUE  SI HAYA PRODUCTOS

            if (detallesVenta.Count == 0)
            {
                MessageBox.Show(
                    "Agregue al menos un producto a la venta.",
                    "Venta sin productos",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }



            // OBTENER DATOS DE LA VENTA

            DateTime fechaVenta = dtFechaVenta.Value;

            int cliente = Convert.ToInt32(cbCliente.SelectedValue);

            int metodoPago = Convert.ToInt32(cbMetodoPago.SelectedValue);


            // OBTENER SUBTOTAL

            decimal subtotal;

            if (!decimal.TryParse(txtSubTotal.Text, NumberStyles.Any, CultureInfo.InvariantCulture, out subtotal))
            {
                MessageBox.Show("El subtotal debe ser un valor numérico.", "Dato incorrecto", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                return;
            }

            // CREAR OBJETO VENTA

            DbVentas venta = new DbVentas(0, fechaVenta, cliente, metodoPago, subtotal);

            // GUARDAR VENTA

            int idVenta = venta.InsertarVenta();

            // Si devuelve 0 es porque ocurri un error
            if (idVenta <= 0)
            {
                MessageBox.Show("No se pudo guardar la venta.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }

            // GUARDAR DETALLES DE PRODUCTOS
            foreach (DetalleVenta detalle in detallesVenta)
            {
                // Asignar el IdVenta generado
                detalle.IdVenta1 = idVenta;

                // Guardar detalle
                if (!detalle.InsertarDetalleVenta())
                {
                    MessageBox.Show("La venta fue creada, pero ocurri un error " + "al guardar uno de los detalles.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    return;
                }
            }

            //  VALIDAR QUE TODO SE GUARDA CORRECTAMENTE

            MessageBox.Show("Venta registrada correctamente.", "Registro exitoso", MessageBoxButtons.OK, MessageBoxIcon.Information);

            // LIMPIAR FORMULARIO

            detallesVenta.Clear();

            cbCliente.SelectedIndex = -1;
            cbMetodoPago.SelectedIndex = -1;

            txtSubTotal.Clear();
            txtIVA.Clear();
            txtTotalPagar.Clear();

            dgvVentas.DataSource = null;

            // Actualizar el listado de ventas

            MostrarVentas();

        }

        private void txtIVA_TextChanged(object sender, EventArgs e)
        {
            CalcularTotal();
        }

        private void txtSubTotal_TextChanged(object sender, EventArgs e)
        {
            CalcularTotal();
        }

        private void CargarComboBoxClientes()
        {
            DataTable dtCliente = DbCliente.CargarTodosLosClientes();

            cbCliente.DataSource = dtCliente;
            cbCliente.DisplayMember = "NombreCliente";
            cbCliente.ValueMember = "IdCliente";
            cbCliente.SelectedIndex = -1;
        }

        private void CargarComboBoxMetodosDePago()
        {
            DataTable dtMetodoPago = MetodoPago.CargarMetodosDePago();

            cbMetodoPago.DataSource = dtMetodoPago;
            cbMetodoPago.DisplayMember = "MetodoPago";
            cbMetodoPago.ValueMember = "IdMetodoPago";
            cbMetodoPago.SelectedIndex = -1;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btnAgregarProductos_Click(object sender, EventArgs e)
        {

            // ABRIR FORMULARIO DE DETALLE DE VENTA

            using (FrmDetalleVenta formulario = new FrmDetalleVenta())
            {
                // Abrir FrmDetalleVenta y esperar
                // a que el usuario agregue el producto
                if (formulario.ShowDialog() == DialogResult.OK)
                {
                    // RECIBIR EL PRODUCTO

                    // Obtener el detalle que se guardo en el 
                    // FrmDetalleVenta
                    DetalleVenta detalle = formulario.DetalleSeleccionado;


                    // AGREGAR EL PRODUCTO A LA LISTA

                    // Guardamos temporalmente el producto
                    // en la lista de detalles
                    detallesVenta.Add(detalle);

                    // MOSTRAR LOS PRODUCTOS

                    // Actualizar el DataGridView
                    MostrarDetalles();
                }
            }
        }


        private void MostrarDetalles()
        {
            // MOSTRAR LISTA DE DETALLES

            // Quitar temporalmente el origen de datos
            dgvDetalleDeVenta.DataSource = null;


            // Asignar nuevamente la lista actualizada
            dgvDetalleDeVenta.DataSource = detallesVenta;

            // CALCULAR SUBTOTAL

            CalcularSubtotalVenta();
        }


        private void CalcularSubtotalVenta()
        {
            // CALCULAR SUBTOTAL

            decimal subtotal = 0;

            // Recorrer todos los productos agregados
            foreach (DetalleVenta detalle in detallesVenta)
            {
                // Calcular subtotal del producto
                decimal subtotalProducto =
                    detalle.Cantidad1 *
                    detalle.PrecioUnitario1;

                // Sumarlo al subtotal general
                subtotal += subtotalProducto;
            }

            // Mostrar subtotal
            txtSubTotal.Text =
                subtotal.ToString("0.00");

            // Calcular IVA y total
            CalcularTotal();
        }


        private void dgvVentas_SelectionChanged(object sender, EventArgs e)
        {

            if (dgvVentas.CurrentRow == null)
                return;

            int idVenta = Convert.ToInt32(
                dgvVentas.CurrentRow.Cells["IdVenta"].Value
            );

            dgvDetalleDeVenta.DataSource = DetalleVenta.CargarDetalleVenta(idVenta);
        }


        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dgvVentas.CurrentRow == null)
            {
                MessageBox.Show("Seleccione una venta de la tabla.");
                return;
            }

            int id = Convert.ToInt32(dgvVentas.CurrentRow.Cells["IdVenta"].Value);

            DialogResult res = MessageBox.Show("Est seguro de eliminar esta venta? Se eliminarn todos los detalles y facturas asociados de forma permanente.", "Confirmar Eliminacin (Cascada)", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (res == DialogResult.Yes)
            {
                Modelo.Entidades.DbVentas venta = new Modelo.Entidades.DbVentas();
                venta.IdVenta1 = id;
                if (venta.EliminarVenta())
                {
                    MessageBox.Show("Venta eliminada correctamente.");
                    MostrarVentas();
                }
                else
                {
                    MessageBox.Show("Error al eliminar la venta.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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


        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtBuscar.Text == "Buscar venta...")
                    return;

                dgvVentas.DataSource = DbVentas.BuscarVenta(txtBuscar.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnGuardarCambios_Click(object sender, EventArgs e)
        {
            if (cbCliente.SelectedIndex == -1 || cbMetodoPago.SelectedIndex == -1)
            {
                MessageBox.Show("Seleccione cliente y método de pago.");
                return;
            }

            int idCliente = Convert.ToInt32(cbCliente.SelectedValue);
            int idMetodoPago = Convert.ToInt32(cbMetodoPago.SelectedValue);
            decimal subtotal = Convert.ToDecimal(txtSubTotal.Text);

            DbVentas venta = new DbVentas(idVentaSeleccionada, dtFechaVenta.Value, idCliente, idMetodoPago, subtotal);
            if (venta.ActualizarVenta())
            {
                MessageBox.Show("Venta actualizada correctamente.");
                MostrarVentas();
                btnEditar.Visible = true;
                btnGuardar.Visible = true;
                idVentaSeleccionada = 0;
            }
            else
            {
                MessageBox.Show("Error al actualizar la venta.");
            }
        }
    }
}
