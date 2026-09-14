using Modelo.Entidades;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Vista.Responsive;

namespace Vista.Facturación
{
    public partial class frmFacturacion : Form
    {
        public frmFacturacion()
        {

            InitializeComponent();
            ResponsiveHelper.Apply(this);
        }



        private void frmFacturacion_Load(object sender, EventArgs e)
        {
            MostrarRegistrosFacturas();
            MostrarDetalleFactura();
            DesactivarCopiarPegar(this);

            //Validamos que las fechas ingresadas esten acorde a la lógica del negocio
            dtFechaDatosGeneralesFactura.Value = DateTime.Today;
            dtFechaDatosGeneralesFactura.Enabled = false;
            dtFechaDatosGeneralesFactura.MinDate = DateTime.Today;
            dtFechaDatosGeneralesFactura.MaxDate = DateTime.Today;
            dtpFechaVencimiento.MinDate = DateTime.Today;

            //El numero de factura inicial estara pendiente, hasta que esta factura se guarde

            txtNumeroFactura.Text = "Pendiente";

            dgvFacturasRegistradas.Columns["IdFactura"].HeaderText = "N° de Factura";
            dgvFacturasRegistradas.Columns["Fecha"].HeaderText = "Fecha de emisión";
        }


        private void MostrarRegistrosFacturas()
        {
            try
            {
                dgvFacturasRegistradas.DataSource = null;
                dgvFacturasRegistradas.DataSource = DbFactura.CargarRegistrosFacturas();

                dgvFacturasRegistradas.Columns["IdFactura"].HeaderText = "N° de Factura";
                dgvFacturasRegistradas.Columns["Fecha"].HeaderText = "Fecha de emisión";

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar las facturas: " + ex.Message);
            }
        }

        private void MostrarDetalleFactura()
        {
            dgvDetalleVenta.DataSource = null;
            dgvDetalleVenta.DataSource = DbFactura.BuscarVentaParaFactura(0);
        }

        private void btnNuevaFactura_Click(object sender, EventArgs e)
        {
            pnlNuevaFactura.Visible = true;
            pnlBarraCambioRegistros.Visible = false;
            pnlContenedorDeCotizacionNueva.Visible = true;
            pnlRegistroCotizacion.Visible = false;
        }

        private void btnRegistrosfacturas_Click(object sender, EventArgs e)
        {
            pnlNuevaFactura.Visible = false;
            pnlBarraCambioRegistros.Visible = true;
            pnlContenedorDeCotizacionNueva.Visible = false;
            pnlRegistroCotizacion.Visible = true;
        }



        private void label6_Click(object sender, EventArgs e)
        {

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


        //Metodo para buscar venta para generar factura y usar los datos de esa venta
        private void btnBuscarVenta_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(txtnVenta.Text.Trim(), out int idVenta))
            {
                MessageBox.Show("Ingresa un número de venta válido.", "Venta", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                return;
            }

            DataTable dt = DbFactura.BuscarVentaParaFactura(idVenta);

            if (dt.Rows.Count == 0)
            {
                MessageBox.Show("La venta no existe o ya tiene una factura.", "Venta no disponible", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                return;
            }

            DataRow fila = dt.Rows[0];

            MessageBox.Show("Venta encontrada correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

            // Datos del cliente en los TextBox
            txtMostrarCliente.Text = fila["Cliente"].ToString();
            txtDui.Text = fila["Documento"].ToString();
            txtTelefono.Text = fila["Telefono"].ToString();
            txtCorreo.Text = fila["Correo"].ToString();

            // FECHA DE VENTA
            dtpFechaVenta.Text = Convert.ToDateTime(
                fila["Fecha de Venta"]
            ).ToString("dd/MM/yyyy");

            // SUBTOTAL
            txtSubTotal.Text = Convert.ToDecimal(
                fila["SubTotal"]
            ).ToString("0.00");

            // DESCUENTO
            txtDescuento.Text = "0.00";

            // CALCULAR IVA Y TOTAL
            CalcularTotales();

            DataTable detalle = DbFactura.CargarDetalleVentaParaFactura(idVenta);

            dgvDetalleVenta.DataSource = null;
            dgvDetalleVenta.DataSource = detalle;

            // Calcular cantidad total de productos
            int cantidadProductos = CalcularCantidadProductos();

            // Mostrar cantidad en la pestaña verde
            lblTotalDeProductos.Text = cantidadProductos.ToString();


        }

        private void CalcularTotales()
        {
            // Obtener el subtotal 
            if (!decimal.TryParse(txtSubTotal.Text, out decimal subtotal))
                return;

            // Obtener el descuento
            if (!decimal.TryParse(txtDescuento.Text, out decimal descuento))
                descuento = 0;

            // Validar que el descuento no puede ser un numero negativo
            if (descuento < 0)
            {
                descuento = 0;
                txtDescuento.Text = "0.00";
            }

            // El descuento no puede superar el subtotal
            if (descuento > subtotal)
            {
                MessageBox.Show("El descuento no puede ser mayor que el subtotal.", "Descuento inválido", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                descuento = 0;
                //Declarar el valor del descuento inicial
                txtDescuento.Text = "";
            }

            // Restar el descuento al subtotal de la vista
            decimal subtotalConDescuento = subtotal - descuento;

            // Calcular IVA del 13% ya con el descuento aplicado
            decimal iva = subtotalConDescuento * 0.13m;

            // Calcular total de la venta para mostrarlo en la factura
            decimal total = subtotalConDescuento + iva;

            // Mostrar resultados finales
            txtIVA.Text = iva.ToString("0.00");
            txtTotal.Text = total.ToString("0.00");
        }

        private void txtDescuento_TextChanged(object sender, EventArgs e)
        {
            CalcularTotales();
        }

        //Método para limpiar los controles
        private void btnLimpiarFactura_Click(object sender, EventArgs e)
        {
            txtnVenta.Text = null;
            txtMostrarCliente.Text = null;
            txtTelefono.Text = null;
            txtDui.Text = null;
            txtCorreo.Text = null;
            txtNumeroFactura.Text = null;
            txtSubTotal.Text = null;
            txtIVA.Text = null;
            txtDescuento.Text = null;
            txtTotal.Text = null;
            txtObservaciones.Text = null;
            //Limpia la tabla
            dgvDetalleVenta.Rows.Clear();
        }

        private void txtTotal_TextChanged(object sender, EventArgs e)
        {
            lblTotalAPagar.Text = "Total a pagar: $ " + txtTotal.Text;
        }

        //Metodo para calcular la cantidad total de la venta
        private int CalcularCantidadProductos()
        {
            int cantidadTotal = 0;
            //Recorre todas las filas de la tabla y las acumula dentro de la variable

            foreach (DataGridViewRow fila in dgvDetalleVenta.Rows)
            {
                if (fila.IsNewRow)
                    continue;
                //Tomma en cuenta cada valor que se encuentre en la columna de cantidad y lo guarda en otra variable
                // para luego poder sumar los productos

                if (fila.Cells["Cantidad"].Value != null)
                {
                    if (int.TryParse(fila.Cells["Cantidad"].Value.ToString(), out int cantidad))
                    {
                        cantidadTotal += cantidad;
                    }
                }
            }

            return cantidadTotal;
        }



        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtBuscar.Text == "Buscar por número de factura...")
                {
                    return;
                }

                string buscar = txtBuscar.Text.Trim();

                if (string.IsNullOrWhiteSpace(buscar))
                {
                    MostrarRegistrosFacturas();
                    return;
                }

                dgvFacturasRegistradas.DataSource =
                    DbFactura.BuscarFacturas(buscar);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void txtBuscar_Leave(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtBuscar.Text))
                {
                    txtBuscar.Text = "Buscar por número de factura...";
                    txtBuscar.ForeColor = Color.Gray;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void txtBuscar_Enter(object sender, EventArgs e)
        {
            try
            {
                if (txtBuscar.Text == "Buscar por número de factura...")
                {
                    txtBuscar.Text = "";
                    txtBuscar.ForeColor = Color.Black;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            // Vaciar el buscador
            txtBuscar.Text = "";

            // Volver a mostrar el texto de indicación
            txtBuscar.Text = "Buscar por número de factura...";
            txtBuscar.ForeColor = Color.Gray;

            // Recargar todas las facturas
            dgvFacturasRegistradas.DataSource = DbFactura.CargarRegistrosFacturas();
        }

        private void dgvFacturasRegistradas_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            int idFactura = Convert.ToInt32(dgvFacturasRegistradas.Rows[e.RowIndex].Cells["IdFactura"].Value);

            frmEditarFactura formulario = new frmEditarFactura(idFactura);

            formulario.ShowDialog();


            MostrarRegistrosFacturas();
            dgvFacturasRegistradas.Columns["IdFactura"].HeaderText = "N° de Factura";
            dgvFacturasRegistradas.Columns["Fecha"].HeaderText = "Fecha de emisión";
        }


        private void GuardarFactura()
        {
            try
            {
                // 1. Validar número de venta
                if (string.IsNullOrWhiteSpace(txtnVenta.Text))
                {
                    MessageBox.Show("Debe ingresar el número de venta.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    txtnVenta.Focus();
                    return;
                }

                // 2. Convertir el número de venta a entero
                int idVenta;

                if (!int.TryParse(txtnVenta.Text.Trim(), out idVenta))
                {
                    MessageBox.Show("El número de venta debe ser un número válido.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    txtnVenta.Focus();
                    return;
                }

                // 3. Obtener las fechas
                DateTime fechaEmision = dtFechaDatosGeneralesFactura.Value;
                DateTime fechaVencimiento = dtpFechaVencimiento.Value;

                // 4. Validar que la fecha de vencimiento no sea menor
                if (fechaVencimiento < fechaEmision)
                {
                    MessageBox.Show("La fecha de vencimiento no puede ser menor que la fecha de emisión.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    return;
                }

                //Obtener observaciones
                string observaciones = txtObservaciones.Text.Trim();

                //Crear objeto Nueva Factura
                DbFactura factura = new DbFactura();

                factura.FechaEmisión1 = fechaEmision;
                factura.FechaVencimiento1 = fechaVencimiento;
                factura.Venta1 = idVenta;
                factura.Observaciones1 = observaciones;

                //Guardar en la base de datos
                factura.InsertarFactura();

                // 8. Mostrar mensaje
                MessageBox.Show("La factura se guardó correctamente.", "Factura guardada", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // 9. Limpiar formulario
                LimpiarFormulario();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrió un error al guardar la factura:\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void LimpiarFormulario()
        {
            // Limpiar número de venta
            txtnVenta.Clear();

            // Limpiar datos del cliente
            txtMostrarCliente.Clear();
            txtTelefono.Clear();
            txtDui.Clear();
            txtCorreo.Clear();

            // Restablecer fechas
            dtFechaDatosGeneralesFactura.Value = DateTime.Now;
            dtpFechaVencimiento.Value = DateTime.Now;

            // Limpiar número de factura
            txtNumeroFactura.Clear();

            // Limpiar observaciones
            txtObservaciones.Clear();

            // Limpiar detalles de productos
            dgvDetalleVenta.DataSource = null;
            dgvDetalleVenta.Rows.Clear();

            // Limpiar resumen de pago
            txtSubTotal.Clear();
            txtIVA.Clear();
            txtDescuento.Clear();
            txtTotal.Clear();

            // Restablecer total a pagar
            lblTotalAPagar.Text = "Total a pagar $ : 0.00";
        }

        private void btnGuardarFactura_Click(object sender, EventArgs e)
        {
            GuardarFactura();
            MostrarRegistrosFacturas();
        }
    }
}

