using Modelo.Entidades;
using System;
using System.Data;
using System.Windows.Forms;

namespace Vista.Facturación
{
    public partial class frmEditarFactura : Form
    {
        private int idFactura;
        public frmEditarFactura()
        {
            InitializeComponent();
        }

        public frmEditarFactura(int idFactura)
        {
            InitializeComponent();

            this.idFactura = idFactura;
        }

        private void frmEditarFactura_Load(object sender, EventArgs e)
        {
            CargarFactura();

            txtNumeroFactura.ReadOnly = true;
            txtSubTotal.ReadOnly = true;
            txtIVA.ReadOnly = true;
            txtTotal.ReadOnly = true;

            dtFechaEmision.Enabled = false;
        }


        private void CargarFactura()
        {
            DataTable dt = DbFactura.CargarFacturaPorId(idFactura);

            if (dt.Rows.Count == 0)
            {
                MessageBox.Show("No se encontró la factura.");
                return;
            }

            DataRow fila = dt.Rows[0];

            txtNumeroFactura.Text = fila["IdFactura"].ToString();

            dtFechaEmision.Value = Convert.ToDateTime(fila["FechaEmision"]);

            dtpFechaVencimiento.Value = Convert.ToDateTime(fila["FechaVencimiento"]);

            txtSubTotal.Text = Convert.ToDecimal(fila["SubTotal"]).ToString("0.00");

            if (fila["Descuento"] == DBNull.Value)
                txtDescuento.Text = "";
            else
                txtDescuento.Text = Convert.ToDecimal(fila["Descuento"]).ToString("0.00");

            txtIVA.Text = Convert.ToDecimal(fila["IVA"]).ToString("0.00");

            txtTotal.Text = Convert.ToDecimal(fila["Total"]).ToString("0.00");

            txtObservaciones.Text = fila["Observaciones"] == DBNull.Value ? "" : fila["Observaciones"].ToString();
        }

        private void txtDescuento_TextChanged(object sender, EventArgs e)
        {
            CalcularTotales();
        }

        private void CalcularTotales()
        {
            if (!decimal.TryParse(txtSubTotal.Text, out decimal subtotal))
                return;

            decimal descuento = 0;

            if (!string.IsNullOrWhiteSpace(txtDescuento.Text))
            {
                decimal.TryParse(txtDescuento.Text, out descuento);
            }

            if (descuento > subtotal)
                descuento = subtotal;

            decimal baseImponible = subtotal - descuento;

            decimal iva = baseImponible * 0.13m;

            decimal total = baseImponible + iva;

            txtIVA.Text = iva.ToString("0.00");
            txtTotal.Text = total.ToString("0.00");
        }

        private void btnGuardarCambios_Click(object sender, EventArgs e)
        {
            decimal? descuento = null;

            if (!string.IsNullOrWhiteSpace(txtDescuento.Text))
            {
                if (!decimal.TryParse(
                    txtDescuento.Text,
                    out decimal valorDescuento))
                {
                    MessageBox.Show("Ingrese un descuento válido.");
                    return;
                }

                descuento = valorDescuento;
            }

            DbFactura.ActualizarFactura(idFactura, dtpFechaVencimiento.Value, descuento, txtObservaciones.Text);

            MessageBox.Show("Factura actualizada correctamente.", "Factura", MessageBoxButtons.OK, MessageBoxIcon.Information);

            Close();
        }
    }
}
