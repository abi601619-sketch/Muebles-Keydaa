using Modelo.Entidades;
using System;
using System.Data;
using System.Windows.Forms;
using Vista.Responsive;

namespace Vista.Ventas
{
    public partial class FrmDetalleVenta : Form
    {
        public DetalleVenta DetalleSeleccionado { get; private set; }

        private bool modoEdicion;
        private int idDetalleVenta;
        public FrmDetalleVenta(int idDetalleVenta)
        {
            InitializeComponent();
            ResponsiveHelper.Apply(this);
            modoEdicion = true;
            this.idDetalleVenta = idDetalleVenta;
            txtProducto.Enabled = false;

            CargarDatosDetalle();



        }

        public FrmDetalleVenta()
        {
            InitializeComponent();
            ResponsiveHelper.Apply(this);
            modoEdicion = false;
        }


        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void btnAgregarProducto_Click(object sender, EventArgs e)
        {

            // VERIFICAR PRODUCTO
            if (string.IsNullOrWhiteSpace(txtProducto.Text))
            {
                MessageBox.Show(
                    "Ingresa el producto.",
                    "Aviso",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            // OBTENER CANTIDAD
            int cantidad = Convert.ToInt32(nudCantidad.Value);

            if (cantidad <= 0)
            {
                MessageBox.Show(
                    "La cantidad debe ser mayor a 0.",
                    "Aviso",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            // VALIDAR PRECIO
            if (!decimal.TryParse(txtPrecioUnitario.Text, out decimal precio))
            {
                MessageBox.Show(
                    "Ingresa un precio válido.",
                    "Aviso",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            // VALIDAR PRECIO NEGATIVO
            if (precio < 0)
            {
                MessageBox.Show(
                    "El precio no puede ser negativo.",
                    "Aviso",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }
            //PODER INSERTAR

            if (modoEdicion)
            {
                try
                {
                    DetalleVenta detalle = new DetalleVenta();

                    detalle.IdDetalleVenta1 = idDetalleVenta;
                    detalle.ProductoVendido1 = txtProducto.Text.Trim();
                    detalle.Cantidad1 = cantidad;
                    detalle.PrecioUnitario1 = precio;

                    if (detalle.ActualizarDetalleVenta())
                    {
                        MessageBox.Show(
                            "Los cambios se guardaron correctamente.",
                            "Éxito",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);

                        this.DialogResult = DialogResult.OK;
                        this.Close();

                    }
                    else
                    {
                        MessageBox.Show(
                            "No se pudieron guardar los cambios.",
                            "Aviso",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(
                        "Error al actualizar el detalle: " + ex.Message,
                        "Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }



                return;
            }


            // MODO AGREGAR


            DetalleSeleccionado = new DetalleVenta();

            // Guardar producto
            DetalleSeleccionado.ProductoVendido1 =
                txtProducto.Text.Trim();

            // Guardar cantidad
            DetalleSeleccionado.Cantidad1 =
                cantidad;

            // Guardar precio
            DetalleSeleccionado.PrecioUnitario1 =
                precio;

            // Regresar el detalle a FrmVentas
            this.DialogResult = DialogResult.OK;

            // Cerrar formulario
            this.Close();

            CargarDatosDetalle();

        }



        private void nudCantidad_ValueChanged_1(object sender, EventArgs e)
        {
            CalcularSubtotal();
        }

        private void txtPrecioUnitario_TextChanged_1(object sender, EventArgs e)
        {
            CalcularSubtotal();
        }

        private void CalcularSubtotal()
        {
            if (decimal.TryParse(txtPrecioUnitario.Text, out decimal precio))
            {
                decimal subtotal = nudCantidad.Value * precio;

                txtSubTotal.Text = subtotal.ToString("0.00");
            }
            else
            {
                txtSubTotal.Text = "0.00";
            }
        }



        private void CargarDatosDetalle()
        {

            try
            {
                DbVentas dbDetalle = new DbVentas();

                DataTable dt = dbDetalle.ObtenerDetalleVenta(idDetalleVenta);

                if (dt.Rows.Count > 0)
                {
                    DataRow fila = dt.Rows[0];

                    txtProducto.Text = fila["ProductoVendido"].ToString();

                    nudCantidad.Text = fila["Cantidad"].ToString();

                    txtPrecioUnitario.Text = Convert.ToDecimal(fila["PrecioUnitario"]).ToString("0.00");

                    CalcularSubtotal();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar los datos: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error
                );
            }
        }
    }
}
