using Modelo.Entidades;
using System;
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



    }
}
