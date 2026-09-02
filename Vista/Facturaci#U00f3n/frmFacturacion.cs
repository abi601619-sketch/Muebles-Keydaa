using Vista.Responsive;
using Modelo.Conexión_DB;
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
using System.Xml.Linq;
using Vista.Clientes;

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

            System.Data.DataTable dtVentas = Modelo.Entidades.DbFactura.CargarVentasParaFactura();
            cbVentas.DataSource = dtVentas;
            cbVentas.DisplayMember = "Display";
            cbVentas.ValueMember = "IdVenta";
        }
        



      


        private void MostrarRegistrosFacturas()
        {
            dgvCotizacionesRegistradas.DataSource = null;
            dgvCotizacionesRegistradas.DataSource = DbFactura.CargarRegistrosFacturas();
        }

        private void MostrarDetalleFactura()
        {
            dgvDetalleFactura.DataSource = null;
            dgvDetalleFactura.DataSource = DbFactura.CargarDetalleFacturas(0);
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

        private void btnBuscarCliente_Click(object sender, EventArgs e)
        {
            frmBuscarCliente modal = new frmBuscarCliente();
            if (modal.ShowDialog() == DialogResult.OK)
            {
                textBox1.Text = modal.NombreClienteSeleccionado;
                txtTelefono.Text = modal.TelefonoClienteSeleccionado;
                txtCorreo.Text = modal.CorreoClienteSeleccionado;
            }
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }
    
        private void btnBuscarPedido_Click(object sender, EventArgs e)
        {
            if (cbVentas.SelectedValue != null && int.TryParse(cbVentas.SelectedValue.ToString(), out int idVenta))
            {
                // Buscar la venta
                System.Data.DataTable dt = Modelo.Entidades.DbFactura.CargarDetalleFacturas(idVenta);
                if (dt.Rows.Count > 0)
                {
                    dgvDetalleFactura.DataSource = dt;
                    // También podemos llenar algunos campos de la UI como el SubTotal y Total
                    if (dt.Columns.Contains("SubTotal") && dt.Columns.Contains("Total a Pagar"))
                    {
                        txtSubTotal2.Text = dt.Rows[0]["SubTotal"].ToString();
                        txtTotal2.Text = dt.Rows[0]["Total a Pagar"].ToString();
                        
                        double subTotal = Convert.ToDouble(dt.Rows[0]["SubTotal"]);
                        double iva = subTotal * 0.13;
                        txtIVA2.Text = Math.Round(iva, 2).ToString();
                    }
                    
                    // Y llenar el cliente
                    if (dt.Columns.Contains("Cliente"))
                    {
                        textBox1.Text = dt.Rows[0]["Cliente"].ToString();
                    }
                }
                else
                {
                    MessageBox.Show("No se encontró ninguna Venta (o Pedido facturable) con ese ID.");
                    dgvDetalleFactura.DataSource = null;
                }
            }
            else
            {
                MessageBox.Show("Ingresa un ID válido.");
            }
        }
    
        private void btnGuardarFactura_Click(object sender, EventArgs e)
        {
            if (cbVentas.SelectedValue != null && int.TryParse(cbVentas.SelectedValue.ToString(), out int idVenta))
            {
                Modelo.Entidades.DbFactura nuevaFactura = new Modelo.Entidades.DbFactura(
                    0, 
                    dtFechaDatosGeneralesFactura.Value, 
                    dateTimePicker3.Value, 
                    idVenta, 
                    txtObservaciones.Text
                );
                
                try
                {
                    nuevaFactura.InsertarFactura();
                    MessageBox.Show("Factura guardada correctamente.");
                    MostrarRegistrosFacturas();
                    System.Data.DataTable dtVentas = Modelo.Entidades.DbFactura.CargarVentasParaFactura();
                    cbVentas.DataSource = dtVentas;
                    cbVentas.DisplayMember = "Display";
                    cbVentas.ValueMember = "IdVenta";
                    // Limpiar UI
                    dgvDetalleFactura.DataSource = null;
                    // cbVentas.SelectedIndex = -1;
                    txtObservaciones.Clear();
                    textBox1.Clear();
                    txtSubTotal2.Clear();
                    txtTotal2.Clear();
                    txtIVA2.Clear();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al guardar la factura: " + ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Por favor, busque y cargue una venta (pedido) antes de guardar la factura.");
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
    }
}

