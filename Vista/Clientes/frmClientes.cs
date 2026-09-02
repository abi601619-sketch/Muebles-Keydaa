using Modelo.Entidades;
using System;
using System.Drawing;
using System.Net.Mail;
using System.Windows.Forms;
using Vista.Responsive;

namespace Vista.Pedidos
{
    public partial class frmClientes : Form
    {
        public frmClientes()
        {
            InitializeComponent();

        }


        private void MostrarClientes()
        {
            dgvClientesCorporativos.DataSource = null;
            dgvClientesCorporativos.DataSource = DbCliente.CargarCorporativos();
            ActualizarEstadisticas();
        }
        private void MostrarClientes2()
        {
            dgvClientesIndividuales.DataSource = null;
            dgvClientesIndividuales.DataSource = DbCliente.CargarIndividuales();
            ActualizarEstadisticas();
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

        private void frmClientes_Load(object sender, EventArgs e)
        {
            MostrarClientes();
            MostrarClientes2();
            ActualizarEstadisticas();
            DesactivarCopiarPegar(this);


            pnlRegistroClienteIndividual.Visible = true;
            pnlRegistroClienteCorporativo.Visible = false;

            pnlBarraClienteIndividual.Visible = true;
            pnlBarraClienteCorporativo.Visible = false;

            // Validacion del máximo de caracteres admitidos en los TextBox
            txtDireccion.MaxLength = 200;
            txtApellidos.MaxLength = 40;
            txtNombres.MaxLength = 40;
            txtNombreEmpresa.MaxLength = 40;
            txtNombreEncargado.MaxLength = 40;
            txtDUI.MaxLength = 10;
            txtNIT.MaxLength = 14;
            txtTelefono.MaxLength = 9;
            txtCorreo.MaxLength = 50;

            ResponsiveHelper.Apply(this);

        }

        private void ActualizarEstadisticas()
        {
            lblTotalClientes.Text = DbCliente.ContarClientesTotales().ToString();
            lblClientesActivos.Text = DbCliente.ContarClientesActivos().ToString();
            lblClientesInactivos.Text = DbCliente.ContarClientesInactivos().ToString();
        }

        private void cbTipoCliente_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            if (cbTipoCliente.Text == "Persona Natural")
            {
                txtNombres.TabIndex = 1;
                txtApellidos.TabIndex = 2;
                txtDUI.TabIndex = 3;
                txtTelefono.TabIndex = 4;
                txtCorreo.TabIndex = 5;
                txtDireccion.TabIndex = 6;
                btnEditar.TabIndex = 7;
                btnGuardarIndividual.TabIndex = 8;
            }
            else if (cbTipoCliente.Text == "Empresa")
            {
                txtNombreEmpresa.TabIndex = 1;
                txtNombreEncargado.TabIndex = 2;
                txtNIT.TabIndex = 3;
                txtTelefono.TabIndex = 4;
                txtCorreo.TabIndex = 5;
                txtDireccion.TabIndex = 6;
                btnEditar.TabIndex = 7;
                btnGuardarCorporativo.TabIndex = 8;
            }

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

        private void btnGuardarIndividual_Click(object sender, EventArgs e)
        {
            if (!ValidarCampos())
            {
                return;
            }

            if (txtDUI.Text.Length != 10 || txtDUI.Text[8] != '-')
            {
                MessageBox.Show("El DUI debe tener el formato 12345678-9.");
                return;
            }

            if (txtTelefono.Text.Length != 9 || txtTelefono.Text[4] != '-')
            {
                MessageBox.Show("El teléfono debe tener el formato 1234-5678.");
                return;
            }

            //Validar Correo
            if (string.IsNullOrWhiteSpace(txtCorreo.Text))
            {
                MessageBox.Show("El correo es obligatorio.");
                txtCorreo.Focus();
                return;
            }

            try
            {
                MailAddress correo = new MailAddress(txtCorreo.Text);
            }
            catch
            {
                MessageBox.Show("Ingrese un correo válido.");
                txtCorreo.Focus();
                return;
            }


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

                MostrarClientes2();


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

        private void btnGuardarCorporativo_Click(object sender, EventArgs e)
        {
            if (!ValidarCampos())
            {
                return;
            }

            if (txtNIT.Text.Length != 14)
            {
                MessageBox.Show("El NIT debe tener 14 números.");
                return;
            }

            if (txtTelefono.Text.Length != 9 || txtTelefono.Text[4] != '-')
            {
                MessageBox.Show("El teléfono debe tener el formato 1234-5678.");
                return;
            }

            // Validar correo
            try
            {
                MailAddress correo = new MailAddress(txtCorreo.Text);

                if (correo.Address != txtCorreo.Text)
                {
                    MessageBox.Show("Ingrese un correo válido.");
                    return;
                }
            }
            catch
            {
                MessageBox.Show("Ingrese un correo válido.");
                return;
            }


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

                MostrarClientes();

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




        private int idClienteSeleccionado = 0;
        private int tipoClienteSeleccionado = 0;

        private string identificador1Original;
        private string identificador2Original;
        private string documentoOriginal;
        private string telefonoOriginal;
        private string correoOriginal;
        private string direccionOriginal;

        private string estadoOriginal;

        private void dgvClientesCorporativos_CellClick(object sender, DataGridViewCellEventArgs e)
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
            btnGuardarCambios.Visible = true;
            //Muestra el Combo Box necesario y el otro lo oculta
            gbDatosEmpresa.Visible = true;
            gbPersonaNatural.Visible = false;
            //Muestra la tabla de registros segun el tipo de cliente
            pnlRegistroClienteIndividual.Visible = false;
            pnlRegistroClienteCorporativo.Visible = true;

            //Muestra siempre el boton aunque se quiera editar
            btnGuardarCorporativo.Visible = false;
        }

        private void BloquearCampos()
        {
            cbTipoCliente.Enabled = false;

            txtNombreEmpresa.ReadOnly = true;
            txtNombreEncargado.ReadOnly = true;
            txtNIT.ReadOnly = true;
            txtTelefono.ReadOnly = true;
            txtCorreo.ReadOnly = true;
            txtDireccion.ReadOnly = true;
            cbEstadoCliente.Enabled = false;
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
            cbEstadoCliente.Enabled = true;

        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (idClienteSeleccionado == 0)
            {
                MessageBox.Show("Seleccione un cliente primero.");
                return;
            }

            HabilitarCampos();

            btnEditar.Visible = true;
            btnGuardarCambios.Visible = true;
            btnGuardarCorporativo.Visible = false;
            btnGuardarIndividual.Visible = false;
            cbEstadoCliente.Enabled = true;
        }

        private void dgvClientesIndividuales_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            if (e.RowIndex < 0 || dgvClientesIndividuales.Rows[e.RowIndex].IsNewRow)
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
            estadoOriginal = fila.Cells[7].Value?.ToString() ?? "";

            // Pasamos los datos al formulario
            txtNombres.Text = identificador1Original;
            txtApellidos.Text = identificador2Original;
            txtDUI.Text = documentoOriginal;
            txtTelefono.Text = telefonoOriginal;
            txtCorreo.Text = correoOriginal;
            txtDireccion.Text = direccionOriginal;
            cbEstadoCliente.Text = estadoOriginal;

            BloquearCampos();

            btnEditar.Visible = true;
            btnGuardarCorporativo.Visible = false;
            btnGuardarCambios.Visible = true;
            //Muestra el Combo Box necesario y el otro lo oculta

            gbDatosEmpresa.Visible = false;
            gbPersonaNatural.Visible = true;

            //Muestra la tabla de registros segun el tipo de cliente
            pnlRegistroClienteIndividual.Visible = true;
            pnlRegistroClienteCorporativo.Visible = false;

            btnGuardarCorporativo.Visible = true;
        }

        private void btnGuardarCambios_Click(object sender, EventArgs e)
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

            //Validar Correo
            if (string.IsNullOrWhiteSpace(txtCorreo.Text))
            {
                MessageBox.Show("El correo es obligatorio.");
                txtCorreo.Focus();
                return;
            }

            try
            {
                MailAddress correo = new MailAddress(txtCorreo.Text);
            }
            catch
            {
                MessageBox.Show("Ingrese un correo válido.");
                txtCorreo.Focus();
                return;
            }

            // Datos que comparten ambos tipos de clientes
            cliente.Telefono1 = txtTelefono.Text;
            cliente.Correo1 = txtCorreo.Text;
            cliente.Direccion1 = txtDireccion.Text;
            cliente.Estado1 = cbEstadoCliente.Text;

            // Actualizar en la base de datos
            if (cliente.ActualizarCliente())
            {
                MessageBox.Show("Cliente actualizado correctamente.", "Actualización exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Recargar la tabla correspondiente del tipo de cliente
                if (tipoClienteSeleccionado == 1)
                {
                    MostrarClientes();
                }
                else if (tipoClienteSeleccionado == 2)
                {
                    MostrarClientes2();
                }

                // Volver a bloquear los campos
                BloquearCampos();

                // Volver a mostrar Editar
                btnEditar.Visible = true;
                btnGuardarCambios.Visible = false;
            }
        }

        //-------------------------------------------------VALIDACIONES-----------------------------------------------------------------------//

        private bool ValidarCampos()
        {
            // Validar que haya seleccionado un tipo de cliente
            if (cbTipoCliente.SelectedIndex == -1)
            {
                MessageBox.Show("Debe seleccionar un tipo de cliente.");
                cbTipoCliente.Focus();
                return false;
            }

            // PERSONA NATURAL
            if (cbTipoCliente.Text == "Persona Natural")
            {
                if (string.IsNullOrWhiteSpace(txtNombres.Text))
                {
                    MessageBox.Show("Debe ingresar el nombre del cliente.");
                    txtNombres.Focus();
                    return false;
                }
                if (string.IsNullOrWhiteSpace(txtApellidos.Text))
                {
                    MessageBox.Show("Debe ingresar los apellidos del cliente.");
                    txtApellidos.Focus();
                    return false;
                }

                if (string.IsNullOrWhiteSpace(txtDUI.Text))
                {
                    MessageBox.Show("Debe ingresar el DUI del ciente.");
                    txtDUI.Focus();
                    return false;
                }

                if (string.IsNullOrWhiteSpace(txtTelefono.Text))
                {
                    MessageBox.Show("Debe ingresar el teléfono del cliente.");
                    txtTelefono.Focus();
                    return false;
                }


                if (string.IsNullOrWhiteSpace(txtCorreo.Text))
                {
                    MessageBox.Show("Debe ingresar el Correo del cliente.");
                    txtCorreo.Focus();
                    return false;
                }
                if (string.IsNullOrWhiteSpace(txtDireccion.Text))
                {
                    MessageBox.Show("Debe ingresar la dirección del cliente.");
                    txtDireccion.Focus();
                    return false;
                }
            }

            // EMPRESA
            if (cbTipoCliente.Text == "Empresa")
            {
                if (string.IsNullOrWhiteSpace(txtNombreEmpresa.Text))
                {
                    MessageBox.Show("Debe ingresar el nombre de la empresa.");
                    txtNombreEmpresa.Focus();
                    return false;
                }

                if (string.IsNullOrWhiteSpace(txtNombreEncargado.Text))
                {
                    MessageBox.Show("Debe ingresar el nombre del encargado.");
                    txtNombreEncargado.Focus();
                    return false;
                }

                if (string.IsNullOrWhiteSpace(txtNIT.Text))
                {
                    MessageBox.Show("Debe ingresar el documento de la empresa.");
                    txtNIT.Focus();
                    return false;
                }

                if (string.IsNullOrWhiteSpace(txtTelefono.Text))
                {
                    MessageBox.Show("Debe ingresar el teléfono.");
                    txtTelefono.Focus();
                    return false;
                }

                if (string.IsNullOrWhiteSpace(txtCorreo.Text))
                {
                    MessageBox.Show("Debe ingresar el Correo.");
                    txtCorreo.Focus();
                    return false;
                }
                if (string.IsNullOrWhiteSpace(txtDireccion.Text))
                {
                    MessageBox.Show("Debe ingresar la dirección de la empresa.");
                    txtCorreo.Focus();
                    return false;
                }
            }

            return true;
        }


        private void txtDUI_KeyPress(object sender, KeyPressEventArgs e)
        {

            if (!char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true;
            }

            if (char.IsDigit(e.KeyChar) && txtDUI.Text.Length >= 10)
            {
                e.Handled = true;
            }
        }

        private void txtNIT_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true;
            }

            if (char.IsDigit(e.KeyChar) && txtNIT.Text.Length >= 14)
            {
                e.Handled = true;
            }
        }

        private void txtTelefono_KeyPress(object sender, KeyPressEventArgs e)
        {

            if (char.IsDigit(e.KeyChar) && txtTelefono.Text.Length >= 9)
            {
                e.Handled = true;
            }

            if (!char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true;
            }

        }

        private void txtTelefono_TextChanged(object sender, EventArgs e)
        {
            string texto = txtTelefono.Text.Replace("-", "");

            if (texto.Length > 4)
            {
                txtTelefono.Text = texto.Insert(4, "-");
                txtTelefono.SelectionStart = txtTelefono.Text.Length;
            }
        }

        private void txtDUI_TextChanged(object sender, EventArgs e)
        {
            string texto = txtDUI.Text.Replace("-", "");

            if (texto.Length > 8)
            {
                txtDUI.Text = texto.Insert(8, "-");
                txtDUI.SelectionStart = txtDUI.Text.Length;
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

        private void txtNombres_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar) && e.KeyChar != ' ')
            {
                e.Handled = true;
            }
        }

        private void txtApellidos_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar) && e.KeyChar != ' ')
            {
                e.Handled = true;
            }
        }

        private void txtDireccion_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetterOrDigit(e.KeyChar) && !char.IsControl(e.KeyChar) && e.KeyChar != ' ' && e.KeyChar != '#' && e.KeyChar != '-' && e.KeyChar != '/' &&
            e.KeyChar != '.' && e.KeyChar != ',' && e.KeyChar != '(' && e.KeyChar != ')')
            {
                e.Handled = true;
            }
        }
        //--------------------------  FIN VALIDACIONES -------------------------------------//

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            dgvClientesIndividuales.DataSource = DbCliente.BuscarCliente(txtBuscar.Text, 1);

            dgvClientesCorporativos.DataSource = DbCliente.BuscarCliente(txtBuscar.Text, 2);
        }

        private void txtBuscar_Leave_1(object sender, EventArgs e)
        {
            txtBuscar.Text = "Buscar Cliente...";
            txtBuscar.ForeColor = Color.Gray;
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

        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            if (txtBuscar.Text == "")
            {
                dgvClientesCorporativos.DataSource = DbCliente.CargarCorporativos();

                dgvClientesIndividuales.DataSource = DbCliente.CargarIndividuales();
            }
        }
    }
}




