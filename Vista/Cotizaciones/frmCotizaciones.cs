using Modelo.Entidades;
using System;
using System.Drawing;
using System.Windows.Forms;
using Vista.Clientes;
using Vista.Responsive;

namespace Vista.Cotizaciones
{
    public partial class frmCotizaciones : Form
    {
        public frmCotizaciones()
        {
            InitializeComponent();
            ResponsiveHelper.Apply(this);

        }
        private int idClienteSeleccionado = 0;

        private decimal subtotal = 0;
        private decimal iva = 0;
        private decimal total = 0;


        private void btnCotizacionDetalle_Click(object sender, EventArgs e)
        {
            pnlNuevaCotizacion.Visible = true;
            pnlRegistrosCotizaciones.Visible = false;
            pnlBarraCambio.Visible = true;
            pnlBarraCambioRegistros.Visible = false;
        }

        private void btnCotizacionesRegistradas_Click(object sender, EventArgs e)
        {
            pnlRegistrosCotizaciones.Visible = true;
            pnlNuevaCotizacion.Visible = false;
            pnlBarraCambio.Visible = false;
            pnlBarraCambioRegistros.Visible = true;
        }

        private void MostrarCotizacionesRegistradas()
        {
            dgvCotizacionesRegistradas.DataSource = null;
            dgvCotizacionesRegistradas.DataSource = DbCotizacion.CargarCotizacion();
            dgvCotizacionesRegistradas.Columns["IdCotizacion"].HeaderText = "#";
        }


        private void frmCotizaciones_Load(object sender, EventArgs e)
        {
            cbEstado.Items.Add("Pendiente");
            cbEstado.Items.Add("Aprobada");
            cbEstado.Items.Add("Rechazada");

            MostrarCotizacionesRegistradas();
            ConfigurarDetalleCotizacion();

            // Datos del cliente solamente lectura
            txtCliente.ReadOnly = true;
            txtTelefono.ReadOnly = true;
            txtCorreo.ReadOnly = true;
            txtDireccion.ReadOnly = true;
            cbEstado.Enabled = false;

            // Valores iniciales
            dtpFechaCotizacion.Value = DateTime.Now;

            //Tabla de productos de cotizacion vacia

            dgvDetalleDeCotizacion.DataSource = null;
            nudCantidad.Value = 1;
            txtSubTotal.Text = "0.00";
            txtIVA.Text = "0.00";
            txtTotal.Text = "0.00";

            // Estado inicial
            cbEstado.SelectedIndex = 0;
            //Fecha de cotizacion actual
            dtpFechaCotizacion.Value = DateTime.Today;
            dtpFechaCotizacion.Enabled = false;
            //Metodo para validar que no se copie texto, ni tampoco se pegue texto
            DesactivarCopiarPegar(this);

            dgvCotizacionesRegistradas.Columns["IdCotizacion"].HeaderText = "#";


        }
        private void btnBuscarCliente_Click(object sender, EventArgs e)
        {

            using (frmBuscarCliente modal = new frmBuscarCliente())
            {
                if (modal.ShowDialog() == DialogResult.OK)
                {
                    idClienteSeleccionado = modal.IdClienteSeleccionado;

                    txtCliente.Text = modal.NombreClienteSeleccionado;
                    txtTelefono.Text = modal.TelefonoClienteSeleccionado;
                    txtCorreo.Text = modal.CorreoClienteSeleccionado;
                    txtDireccion.Text = modal.DireccionClienteSeleccionado;
                }
            }

            if (idClienteSeleccionado == 0)
            {
                MessageBox.Show("Selecciona un cliente.");
                return;
            }
        }


        private void CalcularTotalCotizacion()
        {
            subtotal = 0;

            foreach (DataGridViewRow fila in dgvDetalleDeCotizacion.Rows)
            {
                if (fila.IsNewRow)
                    continue;

                subtotal += Convert.ToDecimal(fila.Cells["SubTotal"].Value);
            }

            iva = subtotal * 0.13m;

            total = subtotal + iva;

            txtSubTotal.Text = subtotal.ToString("0.00");
            txtIVA.Text = iva.ToString("0.00");
            txtTotal.Text = total.ToString("0.00");
        }

        private void LimpiarFormulario()
        {
            idClienteSeleccionado = 0;

            txtCliente.Clear();
            txtTelefono.Clear();
            txtCorreo.Clear();
            txtDireccion.Clear();

            txtCondicionesPago.Clear();
            txtCondicionesEntrega.Clear();

            dtpFechaCotizacion.Value = DateTime.Now;

            cbEstado.SelectedIndex = 0;

            subtotal = 0;
            iva = 0;
            total = 0;

            txtSubTotal.Text = "0.00";
            txtIVA.Text = "0.00";
            txtTotal.Text = "0.00";
        }

        private void btnGuardar_Click_1(object sender, EventArgs e)
        {

            if (idClienteSeleccionado == 0)
            {
                MessageBox.Show("Selecciona un cliente.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                return;
            }

            if (string.IsNullOrWhiteSpace(txtCondicionesPago.Text))
            {
                MessageBox.Show("Ingresa las condiciones de pago.");

                txtCondicionesPago.Focus();

                return;
            }

            if (string.IsNullOrWhiteSpace(txtCondicionesEntrega.Text))
            {
                MessageBox.Show("Ingresa las condiciones de entrega.");

                txtCondicionesEntrega.Focus();

                return;
            }


            CalcularTotalCotizacion();

            if (total <= 0)
            {
                MessageBox.Show(
                    "El total de la cotización debe ser mayor que 0.");

                return;
            }

            if (cbEstado.SelectedIndex == -1)
            {
                MessageBox.Show("Selecciona el estado de la cotizaci?n.");
                cbEstado.Focus();
                return;
            }
            cbEstado.Enabled = false;
            string estadoSeleccionado = Convert.ToString(cbEstado.Text);

            DbCotizacion cotizacion = new DbCotizacion(0, dtpFechaCotizacion.Value, idClienteSeleccionado, txtCondicionesPago.Text.Trim(), txtCondicionesEntrega.Text.Trim(), total, estadoSeleccionado);

            int idCotizacion = cotizacion.InsertarCotizacion();

            if (idCotizacion == 0)
            {
                MessageBox.Show("No se pudo registrar la cotizaci?n.");
                return;
            }

            // Guardar detalles
            foreach (DataGridViewRow row in dgvDetalleDeCotizacion.Rows)
            {
                if (row.IsNewRow) continue;
                if (row.Cells["DescripcionMueble"].Value == null) continue;

                ProductosCotizacion prod = new ProductosCotizacion();
                prod.Idcotizacion1 = idCotizacion;
                prod.Descripcion_Del_Mueble1 = row.Cells["DescripcionMueble"].Value.ToString();
                prod.Largo1 = Convert.ToInt32(row.Cells["Largo"].Value);
                prod.Ancho1 = Convert.ToInt32(row.Cells["Ancho"].Value);
                prod.Alto1 = Convert.ToInt32(row.Cells["Alto"].Value);
                prod.Cantidad1 = Convert.ToInt32(row.Cells["Cantidad"].Value);
                prod.PrecioUnitario1 = Convert.ToDouble(row.Cells["PrecioUnitario"].Value);
                prod.SubTotal1 = Convert.ToDouble(row.Cells["SubTotal"].Value);

                prod.InsertarProductoCotizacion();
            }

            MessageBox.Show("Cotización y productos registrados correctamente.\n\n" + "N?mero de cotización: " + idCotizacion, "Cotización", MessageBoxButtons.OK, MessageBoxIcon.Information);

            MostrarCotizacionesRegistradas();

            LimpiarFormulario();
            dgvDetalleDeCotizacion.Rows.Clear();
        }


        private void ConfigurarDetalleCotizacion()
        {
            dgvDetalleDeCotizacion.Columns.Clear();

            dgvDetalleDeCotizacion.Columns.Add("DescripcionMueble", "Producto");

            dgvDetalleDeCotizacion.Columns.Add("Largo", "Largo");

            dgvDetalleDeCotizacion.Columns.Add("Ancho", "Ancho");

            dgvDetalleDeCotizacion.Columns.Add("Alto", "Alto");

            dgvDetalleDeCotizacion.Columns.Add("Cantidad", "Cantidad");

            dgvDetalleDeCotizacion.Columns.Add("PrecioUnitario", "Precio Unitario");

            dgvDetalleDeCotizacion.Columns.Add("SubTotal", "Subtotal");

            dgvDetalleDeCotizacion.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }


        private void LimpiarProducto()
        {
            txtProductosCotizacion.Clear();

            txtLargo.Clear();
            txtAncho.Clear();
            txtAlto.Clear();

            nudCantidad.Value = 1;

            txtPrecioUnitario.Clear();

            txtProductosCotizacion.Focus();
        }




        private void btnAgregar_Click_1(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtProductosCotizacion.Text))
            {
                MessageBox.Show("Ingresa el producto."); txtProductosCotizacion.Focus();
                return;
            }

            int cantidad = Convert.ToInt32(nudCantidad.Value);

            if (cantidad <= 0)
            {
                MessageBox.Show("La cantidad debe ser mayor que 0.");
                return;
            }
            // Out significa que si se puede convertir la el texto lo guardara en una variable, ya que por Text Box se reciben string
            if (!int.TryParse(txtLargo.Text, out int largo) || largo <= 0)
            {
                MessageBox.Show("Ingresa un largo v?lido.");
                txtLargo.Focus();
                return;
            }

            if (!int.TryParse(txtAncho.Text, out int ancho) || ancho <= 0)
            {
                MessageBox.Show("Ingresa un ancho v?lido.");
                //Focus regresa al cursor al error para corregirlo
                txtAncho.Focus();
                return;
            }

            if (!int.TryParse(txtAlto.Text, out int alto) || alto <= 0)
            {
                MessageBox.Show("Ingresa un alto v?lido.");
                txtAlto.Focus();
                return;
            }

            if (!decimal.TryParse(txtPrecioUnitario.Text, out decimal precio))
            {
                MessageBox.Show("Ingresa un precio v?lido.");
                txtPrecioUnitario.Focus();
                return;
            }
            // Validar que el precio no sea 0
            if (precio <= 0)
            {
                MessageBox.Show("El precio debe ser mayor que 0.");
                return;
            }

            decimal subtotal = cantidad * precio;

            dgvDetalleDeCotizacion.Rows.Add(txtProductosCotizacion.Text.Trim(), largo, ancho, alto, cantidad, precio.ToString("0.00"), subtotal.ToString("0.00"));

            CalcularTotalCotizacion();

            LimpiarProducto();
        }

        private void dgvDetalleDeCotizacion_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || ((DataGridView)sender).Rows[e.RowIndex].IsNewRow)
                return;

            int idCotizacion = Convert.ToInt32(dgvDetalleDeCotizacion.Rows[e.RowIndex].Cells["IdCotizacion"].Value);

            dgvDetalleDeCotizacion.DataSource = null;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (dgvCotizacionesRegistradas.CurrentRow != null)
            {
                int idCotizacion = Convert.ToInt32(dgvCotizacionesRegistradas.CurrentRow.Cells["IdCotizacion"].Value);
                string estado = dgvCotizacionesRegistradas.CurrentRow.Cells["Estado"].Value?.ToString();

                if (estado != "Aprobada")
                {
                    MessageBox.Show("Solo las cotizaciones Aprobadas pueden convertirse en Pedidos.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                DateTime fechaEntrega = DateTime.Now.AddDays(15);
                bool exito = DbPedidos.ConvertirCotizacionAPedido(idCotizacion, fechaEntrega);
                if (exito)
                {
                    MessageBox.Show("¡La cotización se ha convertido en Pedido exitosamente!\nFecha estimada de entrega: " + fechaEntrega.ToShortDateString(), "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("Por favor, selecciona una cotización de la tabla primero.");
            }
        }

        private void dgvCotizacionesRegistradas_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || dgvCotizacionesRegistradas.Rows[e.RowIndex].IsNewRow) return;
            DataGridViewRow row = dgvCotizacionesRegistradas.Rows[e.RowIndex];
            lblNumeroSelec.Text = row.Cells["IdCotizacion"].Value?.ToString();
            label2.Text = row.Cells["Cliente"].Value?.ToString();
            lblEtsado.Text = row.Cells["Estado"].Value?.ToString();
            lblMostrarTotal.Text = "$" + row.Cells["Total"].Value?.ToString();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dgvCotizacionesRegistradas.CurrentRow != null)
            {
                int idCotizacion = Convert.ToInt32(dgvCotizacionesRegistradas.CurrentRow.Cells["IdCotizacion"].Value);
                DialogResult dialogResult = MessageBox.Show("¿Estas seguro de que deseas eliminar la cotización #" + idCotizacion + "?", "Confirmar Eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (dialogResult == DialogResult.Yes)
                {
                    DbCotizacion cotizacion = new DbCotizacion();
                    cotizacion.IdCotizacion1 = idCotizacion;
                    if (cotizacion.EliminarCotizacion())
                    {
                        MessageBox.Show("Cotización eliminada exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        MostrarCotizacionesRegistradas();
                    }
                    else
                    {
                        MessageBox.Show("Error al eliminar la cotización.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Por favor, selecciona una cotización de la tabla para eliminar.");
            }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            cbEstado.Enabled = false;
            if (dgvCotizacionesRegistradas.CurrentRow != null)
            {
                int idCotizacion = Convert.ToInt32(dgvCotizacionesRegistradas.CurrentRow.Cells["IdCotizacion"].Value);
                string estadoActual = dgvCotizacionesRegistradas.CurrentRow.Cells["Estado"].Value?.ToString();

                DialogResult result = MessageBox.Show("¿Deseas cambiar el estado de la cotización? #" + idCotizacion + "?\n\nPresiona SI para marcarla como 'Aprobada'.\nPresiona NO para marcarla como 'Rechazada'.\nPresiona CANCELAR para no hacer nada.",
                    "Cambiar Estado",
                    MessageBoxButtons.YesNoCancel,
                    MessageBoxIcon.Question);

                string nuevoEstado = "";
                if (result == DialogResult.Yes) nuevoEstado = "Aprobada";
                else if (result == DialogResult.No) nuevoEstado = "Rechazada";
                else return;

                DbCotizacion cot = new DbCotizacion();
                cot.IdCotizacion1 = idCotizacion;

                if (cot.ActualizarEstado(nuevoEstado))
                {
                    MessageBox.Show("El estado se actualizó exitosamente a: " + nuevoEstado, "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    MostrarCotizacionesRegistradas();
                }
                else
                {
                    MessageBox.Show("Ocurrió un error al actualizar el estado.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Por favor selecciona una cotización primero.");
            }
        }
        //Metodo para validar que no se pueda ni copiar ni pegar en los formularios
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
                if (txtBuscar.Text == "Buscar por código de cotización...")
                {
                    return;
                }

                string buscar = txtBuscar.Text.Trim();

                if (string.IsNullOrWhiteSpace(buscar))
                {
                    MostrarCotizacionesRegistradas();
                    return;
                }

                dgvCotizacionesRegistradas.DataSource = null;
                dgvCotizacionesRegistradas.DataSource = DbCotizacion.BuscarCotizacion(buscar);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtBuscar_Enter(object sender, EventArgs e)
        {

            try
            {
                if (txtBuscar.Text == "Buscar por código de cotización...")
                {
                    txtBuscar.Text = "";
                    txtBuscar.ForeColor = Color.Black;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void txtBuscar_Leave(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtBuscar.Text))
                {
                    txtBuscar.Text = "Buscar por código de cotización...";
                    txtBuscar.ForeColor = Color.Gray;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}



