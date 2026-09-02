using Vista.Responsive;
using System;
using Modelo.Entidades;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Vista.Produccion_Secretario
{
    public partial class frmEditarProduccionSecretario : Form
    {
        private int idProduccionEmpleado;
        public frmEditarProduccionSecretario(int idProduccionEmpleado)
        {
            InitializeComponent();
            ResponsiveHelper.Apply(this);
            this.idProduccionEmpleado = idProduccionEmpleado;
            CargarProduccionEmpleado();

            txtCliente.Enabled = false;
            txtMuebleRealizar.Enabled = false;
            txtCodigoProduccion.Enabled = false;
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void CargarProduccionEmpleado()
        {
            DbProducción produccion = new DbProducción();

            produccion.IdProduccion1 = idProduccionEmpleado;

            bool encontrado = produccion.ObtenerProduccion();

            if (encontrado)
            {
                txtCodigoProduccion.Text = produccion.IdProduccion1.ToString();
                txtCliente.Text = produccion.Cliente1;
                txtMuebleRealizar.Text = produccion.Mueble1;
                nupProgreso.Value = produccion.Progreso1;
                lblEstado.Text = produccion.Estado1;
            }
            else
            {
                MessageBox.Show("No se encontró la producción con ID: " + idProduccionEmpleado, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void nupProgreso_ValueChanged(object sender, EventArgs e)
        {
            int progresoProduccion = (int)nupProgreso.Value;
            if (progresoProduccion == 0)
            {
                lblEstado.Text = "Pendiente";
            }
            else if (progresoProduccion < 100)
            {
                lblEstado.Text = "En producción";
            }
            else
            {
                lblEstado.Text = "Finalizado";
            }
        }

        private void btnGuardarCambios_Click(object sender, EventArgs e)
        {
            if(dtpFechaEntrega.Value.Date < DateTime.Today)
            {
                MessageBox.Show("La fecha no debe de ser anterior a la fecha actual", "Error");
                dtpFechaEntrega.Focus();
                return;
            }
            
            DbProducción produccion = new DbProducción();
            produccion.IdProduccion1 = idProduccionEmpleado;
            produccion.Progreso1 = (int)nupProgreso.Value;
            if (produccion.ActualizarProduccion())
            {
                MessageBox.Show("Producción actualizada correctamente.","Éxito",MessageBoxButtons.OK);
                Close();
            }
        }
    }
}
