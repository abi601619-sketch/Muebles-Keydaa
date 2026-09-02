using Vista.Responsive;
using Modelo.Entidades;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Vista.Ventas
{
    public partial class FrmDetalleVenta : Form
    {
        public DetalleVenta DetalleSeleccionado { get; private set; }

        public FrmDetalleVenta()
        {
            InitializeComponent();
            ResponsiveHelper.Apply(this);
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void btnAgregarProducto_Click(object sender, EventArgs e)
        {
            // ==========================================
            // VALIDAR PRODUCTO
            // ==========================================

            // Verificar que el usuario haya escrito un producto
            if (string.IsNullOrWhiteSpace(txtProducto.Text))
            {
                MessageBox.Show(
                    "Ingresa el producto.",
                    "Aviso",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }


            // ==========================================
            // OBTENER CANTIDAD
            // ==========================================

            // Obtener la cantidad seleccionada en el NumericUpDown
            int cantidad = Convert.ToInt32(nudCantidad.Value);


            // ==========================================
            // VALIDAR CANTIDAD
            // ==========================================

            // Verificar que la cantidad sea mayor que 0
            if (cantidad <= 0)
            {
                MessageBox.Show(
                    "La cantidad debe ser mayor a 0.",
                    "Aviso",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }


            // ==========================================
            // VALIDAR PRECIO
            // ==========================================

            // Intentar convertir el precio escrito a decimal
            if (!decimal.TryParse(
                txtPrecioUnitario.Text,
                out decimal precio))
            {
                MessageBox.Show(
                    "Ingresa un precio válido.",
                    "Aviso",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }


            // ==========================================
            // VALIDAR PRECIO NEGATIVO
            // ==========================================

            // Verificar que el precio no sea negativo
            if (precio < 0)
            {
                MessageBox.Show(
                    "El precio no puede ser negativo.",
                    "Aviso",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }


            // ==========================================
            // CREAR DETALLE DE VENTA
            // ==========================================

            // Crear un nuevo objeto DetalleVenta
            DetalleSeleccionado = new DetalleVenta();


            // Guardar el nombre del producto
            DetalleSeleccionado.ProductoVendido1 =
                txtProducto.Text.Trim();


            // Guardar la cantidad
            DetalleSeleccionado.Cantidad1 =
                cantidad;


            // Guardar el precio unitario
            DetalleSeleccionado.PrecioUnitario1 =
                precio;


            // ==========================================
            // DEVOLVER EL DETALLE A FRMVENTAS
            // ==========================================

            // Indicar que el usuario agregó correctamente
            // el producto
            this.DialogResult = DialogResult.OK;


            // Cerrar FrmDetalleVenta y regresar a FrmVentas
            this.Close();
        
        }

        private void nudCantidad_ValueChanged_1(
            object sender,
            EventArgs e)
        {
            CalcularSubtotal();
        }

        private void txtPrecioUnitario_TextChanged_1(
            object sender,
            EventArgs e)
        {
            CalcularSubtotal();
        }

        private void CalcularSubtotal()
        {
            if (decimal.TryParse(
                txtPrecioUnitario.Text,
                out decimal precio))
            {
                decimal subtotal =
                    nudCantidad.Value * precio;

                txtSubTotal.Text =
                    subtotal.ToString("0.00");
            }
            else
            {
                txtSubTotal.Text = "0.00";
            }
        }
    }
}
