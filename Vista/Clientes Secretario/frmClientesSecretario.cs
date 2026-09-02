using Modelo.Entidades;
using System;
using System.Drawing;
using System.Windows.Forms;
using Vista.Responsive;

namespace Vista.Clientes_Secretario
{
    public partial class frmClientesSecretario : Form
    {
        public frmClientesSecretario()
        {
            InitializeComponent();
            ResponsiveHelper.Apply(this);
        }

        private void cbTipoCliente_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbTipoCliente.SelectedIndex == 0)
            {
                // Paneles
                pnlRegistroClienteIndividual.Visible = true;
                pnlRegistroClienteCorporativo.Visible = false;

                // Barras
                pnlBarraClienteIndividual.Visible = true;
                pnlBarraClienteCorporativo.Visible = false;

                // Botones
                btnGuardarIndividual.Visible = true;
                btnGuardarCorporativo.Visible = false;

                //Group Box

                gbPersonaNatural.Visible = true;
                gbDatosEmpresa.Visible = false;

            }
            else if (cbTipoCliente.SelectedIndex == 1)
            {
                // Paneles
                pnlRegistroClienteIndividual.Visible = false;
                pnlRegistroClienteCorporativo.Visible = true;

                // Barras
                pnlBarraClienteIndividual.Visible = false;
                pnlBarraClienteCorporativo.Visible = true;

                // Botones
                btnGuardarIndividual.Visible = false;
                btnGuardarCorporativo.Visible = true;

                //Group Box
                gbPersonaNatural.Visible = false;
                gbDatosEmpresa.Visible = true;


            }
        }

        private void txtBuscar_Enter(object sender, EventArgs e)
        {
            //Cuando el usuario de enter para escribir, se va a borrar el texto de indicacion
            // Y el texto ya no sera opaco, sera color negro
            if (txtBuscar.Text == "Buscar Cliente...")
            {
                txtBuscar.Text = "";
                txtBuscar.ForeColor = Color.Black;

            }
        }



        private void txtBuscar_Leave(object sender, EventArgs e)
        {
            txtBuscar.Text = "Buscar Cliente...";
            txtBuscar.ForeColor = Color.Gray;
        }

        private void btnClienteIndividual_Click(object sender, EventArgs e)
        {
            // Barras
            pnlBarraClienteIndividual.Visible = true;
            pnlBarraClienteCorporativo.Visible = false;

            // Paneles
            pnlRegistroClienteIndividual.Visible = true;
            pnlRegistroClienteCorporativo.Visible = false;
        }

        private void btnClienteCorporativo_Click(object sender, EventArgs e)
        {
            // Barras
            pnlBarraClienteIndividual.Visible = false;
            pnlBarraClienteCorporativo.Visible = true;

            // Paneles
            pnlRegistroClienteIndividual.Visible = false;
            pnlRegistroClienteCorporativo.Visible = true;
        }

        private void MostrarClientesIndividuales()
        {
            dgvClientesCorporativos.DataSource = null;
            dgvClientesCorporativos.DataSource = DbCliente.CargarCorporativos();
        }
        private void MostrarClientesCorporativos()
        {
            dgvClientesIndividuales.DataSource = null;
            dgvClientesIndividuales.DataSource = DbCliente.CargarIndividuales();
        }

        private void frmClientesSecretario_Load(object sender, EventArgs e)
        {
            MostrarClientesIndividuales();
            MostrarClientesCorporativos();
            ActualizarEstadisticas();

            pnlRegistroClienteIndividual.Visible = true;
            pnlRegistroClienteCorporativo.Visible = false;

            pnlBarraClienteIndividual.Visible = true;
            pnlBarraClienteCorporativo.Visible = false;

            btnGuardarCambios.Visible = false;
        }

        private void ActualizarEstadisticas()
        {
            lblTotalClientes.Text = DbCliente.ContarClientesTotales().ToString();
            lblClientesActivos.Text = DbCliente.ContarClientesActivos().ToString();
            lblClientesInactivos.Text = DbCliente.ContarClientesInactivos().ToString();
        }


        private void cbTipoCliente_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            if (cbTipoCliente.SelectedIndex == 0)
            {
                // Paneles
                pnlRegistroClienteIndividual.Visible = true;
                pnlRegistroClienteCorporativo.Visible = false;

                // Barras
                pnlBarraClienteIndividual.Visible = true;
                pnlBarraClienteCorporativo.Visible = false;

                // Botones
                btnGuardarIndividual.Visible = true;
                btnGuardarCorporativo.Visible = false;

                //Group Box

                gbPersonaNatural.Visible = true;
                gbDatosEmpresa.Visible = false;

            }
            else if (cbTipoCliente.SelectedIndex == 1)
            {
                // Paneles
                pnlRegistroClienteIndividual.Visible = false;
                pnlRegistroClienteCorporativo.Visible = true;

                // Barras
                pnlBarraClienteIndividual.Visible = false;
                pnlBarraClienteCorporativo.Visible = true;

                // Botones
                btnGuardarIndividual.Visible = false;
                btnGuardarCorporativo.Visible = true;

                //Group Box
                gbPersonaNatural.Visible = false;
                gbDatosEmpresa.Visible = true;


            }
        }



        private void btnGuardarCorporativo_Click(object sender, EventArgs e)
        {
            DbCliente cliente = new DbCliente();

            cliente.TipoCliente1 = 1;
            cliente.Identificador11 = txtNombreEmpresa.Text;
            cliente.Identificador21 = txtNombreEncargado.Text;
            cliente.Documento1 = txtNIT.Text;
            cliente.Telefono1 = txtTelefono.Text;
            cliente.Correo1 = txtCorreo.Text;
            cliente.Direccion1 = txtDireccion.Text;

            if (cliente.InsertarClienteCorporativo())
            {
                MessageBox.Show("Cliente corporativo registrado correctamente.", "Registro exitoso", MessageBoxButtons.OK, MessageBoxIcon.Information);

                MostrarClientesCorporativos();

                // Limpiar campos
                txtNombreEmpresa.Clear();
                txtNombreEncargado.Clear();
                txtNIT.Clear();
                txtTelefono.Clear();
                txtCorreo.Clear();
                txtDireccion.Clear();
            }

        }



        private void pnlPedirInformacion_Paint(object sender, PaintEventArgs e)
        {

        }
        private int idClienteSeleccionado = 0;
        private int tipoClienteSeleccionado = 0;

        private string identificador1Original;
        private string identificador2Original;
        private string documentoOriginal;
        private string telefonoOriginal;
        private string correoOriginal;
        private string direccionOriginal;
        private string estadoOriginal;




        private void BloquearCampos()
        {
            cbTipoCliente.Enabled = false;

            txtNombreEmpresa.ReadOnly = true;
            txtNombreEncargado.ReadOnly = true;
            txtNIT.ReadOnly = true;
            txtTelefono.ReadOnly = true;
            txtCorreo.ReadOnly = true;
            txtDireccion.ReadOnly = true;
        }

        private void HabilitarCampos()
        {
            cbTipoCliente.Enabled = true;
            txtNombreEmpresa.ReadOnly = false;
            txtNombreEncargado.ReadOnly = false;
            txtNIT.ReadOnly = false;
            txtTelefono.ReadOnly = false;
            txtCorreo.ReadOnly = false;
            txtDireccion.ReadOnly = false;
        }

        private void btnEditar_Click_1(object sender, EventArgs e)
        {
            if (idClienteSeleccionado == 0)
            {
                MessageBox.Show("Seleccione un cliente primero.");
                return;
            }

            HabilitarCampos();

            btnEditar.Visible = true;
            btnGuardarCambios.Visible = true;
        }





        private void dgvClientesCorporativos_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || dgvClientesCorporativos.Rows[e.RowIndex].IsNewRow)
                return;

            DataGridViewRow fila = dgvClientesCorporativos.Rows[e.RowIndex];

            idClienteSeleccionado = Convert.ToInt32(fila.Cells["IdCliente"].Value);

            tipoClienteSeleccionado = 1;

            // Guardamos los datos originales
            identificador1Original = fila.Cells["Nombre_De_Empresa"].Value?.ToString() ?? "";
            identificador2Original = fila.Cells["Nombre_Del_Encargado"].Value?.ToString() ?? "";
            documentoOriginal = fila.Cells["NIT"].Value?.ToString() ?? "";
            telefonoOriginal = fila.Cells["Telefono"].Value?.ToString() ?? "";
            correoOriginal = fila.Cells["Correo"].Value?.ToString() ?? "";
            direccionOriginal = fila.Cells["Direccion"].Value?.ToString() ?? "";
            estadoOriginal = fila.Cells["Estado"].Value?.ToString() ?? "";

            // Pasamos los datos al formulario
            txtNombreEmpresa.Text = identificador1Original;
            txtNombreEncargado.Text = identificador2Original;
            txtNIT.Text = documentoOriginal;
            txtTelefono.Text = telefonoOriginal;
            txtCorreo.Text = correoOriginal;
            txtDireccion.Text = direccionOriginal;
            cbEstadoCliente.Text = estadoOriginal;

            BloquearCampos();

            btnEditar.Visible = true;
            btnGuardarCambios.Visible = false;
            //Muestra el Combo Box necesario y el otro lo oculta
            gbDatosEmpresa.Visible = true;
            gbPersonaNatural.Visible = false;
            //Muestra la tabla de registros segun el tipo de cliente
            pnlRegistroClienteIndividual.Visible = false;
            pnlRegistroClienteCorporativo.Visible = true;
        }

        private void btnGuardarCambios_Click_1(object sender, EventArgs e)
        {
            if (idClienteSeleccionado == 0)
            {
                MessageBox.Show("Seleccione un cliente primero.");
                return;
            }

            DbCliente cliente = new DbCliente();

            // ID del cliente que se quiere actualizar
            cliente.IdCliente1 = idClienteSeleccionado;

            cliente.TipoCliente1 = tipoClienteSeleccionado;

            // Si es cliente Individual
            if (tipoClienteSeleccionado == 2)
            {
                cliente.Identificador11 = txtNombres.Text;
                cliente.Identificador21 = txtApellidos.Text;
                cliente.Documento1 = txtDUI.Text;
            }

            // Si es cliente Corporativo
            else if (tipoClienteSeleccionado == 1)
            {
                cliente.Identificador11 = txtNombreEmpresa.Text;
                cliente.Identificador21 = txtNombreEncargado.Text;
                cliente.Documento1 = txtNIT.Text;
            }

            // Datos que comparten ambos tipos de clientes
            cliente.Telefono1 = txtTelefono.Text;
            cliente.Correo1 = txtCorreo.Text;
            cliente.Direccion1 = txtDireccion.Text;

            // Actualizar en la base de datos
            if (cliente.ActualizarCliente())
            {
                MessageBox.Show("Cliente actualizado correctamente.", "Actualización exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Recargar la tabla correspondiente del tipo de cliente
                if (tipoClienteSeleccionado == 1)
                {
                    MostrarClientesCorporativos();
                }
                else if (tipoClienteSeleccionado == 2)
                {
                    MostrarClientesIndividuales();
                }

                // Volver a bloquear los campos
                BloquearCampos();

                // Volver a mostrar Editar
                btnEditar.Visible = true;

                // Ocultar Guardar Cambios
                btnGuardarCambios.Visible = false;
            }
        }

        private void dgvClientesIndividuales_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || ((DataGridView)sender).Rows[e.RowIndex].IsNewRow)
                return;

            DataGridViewRow fila = dgvClientesIndividuales.Rows[e.RowIndex];

            idClienteSeleccionado = Convert.ToInt32(fila.Cells["IdCliente"].Value);

            tipoClienteSeleccionado = 2;

            // Guardamos los datos originales segun las posiciones para que no tenga errores
            identificador1Original = fila.Cells[1].Value?.ToString() ?? "";
            identificador2Original = fila.Cells[2].Value?.ToString() ?? "";
            documentoOriginal = fila.Cells[3].Value?.ToString() ?? "";
            telefonoOriginal = fila.Cells[4].Value?.ToString() ?? "";
            correoOriginal = fila.Cells[5].Value?.ToString() ?? "";
            direccionOriginal = fila.Cells[6].Value?.ToString() ?? "";

            // Pasamos los datos al formulario
            txtNombres.Text = identificador1Original;
            txtApellidos.Text = identificador2Original;
            txtDUI.Text = documentoOriginal;
            txtTelefono.Text = telefonoOriginal;
            txtCorreo.Text = correoOriginal;
            txtDireccion.Text = direccionOriginal;

            BloquearCampos();

            btnEditar.Visible = true;
            btnGuardarCambios.Visible = false;

            //Muestra el Combo Box necesario y el otro lo oculta
            gbDatosEmpresa.Visible = false;
            gbPersonaNatural.Visible = true;
            //Muestra la tabla de registros segun el tipo de cliente
            pnlRegistroClienteIndividual.Visible = true;
            pnlRegistroClienteCorporativo.Visible = false;
        }

        private void btnGuardarIndividual_Click(object sender, EventArgs e)
        {
            DbCliente cliente = new DbCliente();

            cliente.TipoCliente1 = 2;
            cliente.Identificador11 = txtNombres.Text;
            cliente.Identificador21 = txtApellidos.Text;
            cliente.Documento1 = txtDUI.Text;
            cliente.Telefono1 = txtTelefono.Text;
            cliente.Correo1 = txtCorreo.Text;
            cliente.Direccion1 = txtDireccion.Text;
            cliente.Estado1 = cbEstadoCliente.Text;

            if (cliente.InsertarClienteIndividual())
            {
                MessageBox.Show("Cliente individual registrado correctamente.", "Registro exitoso", MessageBoxButtons.OK, MessageBoxIcon.Information);

                MostrarClientesIndividuales();


                // Limpiar campos
                txtNombres.Clear();
                txtApellidos.Clear();
                txtDUI.Clear();
                txtTelefono.Clear();
                txtCorreo.Clear();
                txtDireccion.Clear();
                cbEstadoCliente.SelectedIndex = -1;
            }
        }

        private void btnGuardarCorporativo_Click_1(object sender, EventArgs e)
        {
            DbCliente cliente = new DbCliente();

            cliente.TipoCliente1 = 1;
            cliente.Identificador11 = txtNombreEmpresa.Text;
            cliente.Identificador21 = txtNombreEncargado.Text;
            cliente.Documento1 = txtNIT.Text;
            cliente.Telefono1 = txtTelefono.Text;
            cliente.Correo1 = txtCorreo.Text;
            cliente.Direccion1 = txtDireccion.Text;
            cliente.Estado1 = cbEstadoCliente.Text;

            if (cliente.InsertarClienteCorporativo())
            {
                MessageBox.Show("Cliente corporativo registrado correctamente.", "Registro exitoso", MessageBoxButtons.OK, MessageBoxIcon.Information);

                MostrarClientesCorporativos();

                // Limpiar campos
                txtNombreEmpresa.Clear();
                txtNombreEncargado.Clear();
                txtNIT.Clear();
                txtTelefono.Clear();
                txtCorreo.Clear();
                txtDireccion.Clear();
                cbEstadoCliente.SelectedIndex = -1;
            }

        }

        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            if (txtBuscar.Text == "")
            {
                dgvClientesCorporativos.DataSource = DbCliente.CargarCorporativos();
                dgvClientesIndividuales.DataSource = DbCliente.CargarIndividuales();
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            dgvClientesIndividuales.DataSource = DbCliente.BuscarCliente(txtBuscar.Text, 1);

            dgvClientesCorporativos.DataSource = DbCliente.BuscarCliente(txtBuscar.Text, 2);
        }
    }
}




