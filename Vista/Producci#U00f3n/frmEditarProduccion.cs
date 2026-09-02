using Vista.Responsive;
using Modelo.Entidades;
using System;
using System.Windows.Forms;

namespace Vista.Producción
{
    public partial class frmEditarProduccion : Form
    {
        private int idProduccion;

        public frmEditarProduccion(int idProduccion)
        {
            InitializeComponent();
            ResponsiveHelper.Apply(this);

            this.idProduccion = idProduccion;

            // Cargar los datos inmediatamente
            CargarProduccion();

            txtCliente.Enabled = false;
            txtCodigoProduccion.Enabled = false;
            txtMuebleRealizar.Enabled = false;

            //Navegacion con la tecla TAB
            dtpFechaEntrega.TabIndex = 1;
            nudProgreso.TabIndex = 2;
            btnCancelar.TabIndex = 3;
            btnGuardarCambios.TabIndex = 4;
        }

        private void CargarProduccion()
        {
            DbProducción produccion = new DbProducción();

            produccion.IdProduccion1 = idProduccion;

            bool encontrado = produccion.ObtenerProduccion();

            if (encontrado)
            {
                txtCodigoProduccion.Text = produccion.IdProduccion1.ToString();

                txtCliente.Text = produccion.Cliente1;

                txtMuebleRealizar.Text = produccion.Mueble1;

                nudProgreso.Value = produccion.Progreso1;

                lblEstado.Text = produccion.Estado1;
            }
            else
            {
                MessageBox.Show("NO se encontró la producción con ID: " + idProduccion, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void nudProgreso_ValueChanged(object sender, EventArgs e)
        {
            int progreso = (int)nudProgreso.Value;

            if (progreso == 0)
            {
                lblEstado.Text = "Pendiente";
            }
            else if (progreso < 100)
            {
                lblEstado.Text = "En producción";
            }
            else
            {
                lblEstado.Text = "Finalizado";
            }
        }



        private void btnCancelar_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void nudProgreso_ValueChanged_1(object sender, EventArgs e)
        {
            int progreso = (int)nudProgreso.Value;

            if (progreso == 0)
            {
                lblEstado.Text = "Pendiente";
            }
            else if (progreso < 100)
            {
                lblEstado.Text = "En producción";
            }
            else
            {
                lblEstado.Text = "Finalizado";
            }
        }

        private void btnGuardarCambios_Click_1(object sender, EventArgs e)
        {
            if (dtpFechaEntrega.Value.Date < DateTime.Today)
            {
                MessageBox.Show("La fecha no puede ser anterior a la fecha actual.");
                dtpFechaEntrega.Focus();
                return;
            }

            DbProducción produccion = new DbProducción();

            produccion.IdProduccion1 = idProduccion;
            produccion.Progreso1 = (int)nudProgreso.Value;

            if (produccion.ActualizarProduccion())
            {
                MessageBox.Show(
                    "Producción actualizada correctamente.",
                    "Éxito",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                DialogResult = DialogResult.OK;
                Close();
            }
        }
    }
}
