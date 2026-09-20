using Modelo.Entidades;
using System;
using System.Drawing;
using System.Net.Mail;
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

        // VARIABLES

        private int idClienteSeleccionado = 0;
        private int tipoClienteSeleccionado = 0;

        // Datos originales del cliente seleccionado
        private string identificador1Original;
        private string identificador2Original;
        private string documentoOriginal;
        private string telefonoOriginal;
        private string correoOriginal;
        private string direccionOriginal;
        private string estadoOriginal;

        private bool modoEdicion = false;

        // MOSTRAR CLIENTES
        private void MostrarClientesIndividuales()
        {

        }
        private void MostrarClientesCorporativos()
        {

        }
        //----------------------------------------------------------------------
        // ACTUALIZAR LAS ESTADISTICAS DE LOS CLIENTES
        private void ActualizarEstadisticas()
        {
            try
            {
                lblTotalClientes.Text =
                    DbCliente.ContarClientesTotales().ToString();

                lblClientesActivos.Text =
                    DbCliente.ContarClientesActivos().ToString();

                lblClientesInactivos.Text =
                    DbCliente.ContarClientesInactivos().ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Ocurrió un error al actualizar las estadísticas.\n" + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }
        //----------------------------------------------------------------------------
        //METODOS DE BLOQUEAR Y HABILITAR CAMPOS


        private void BloquearCampos()
        {
            // Cliente corporativo
            txtNombreEmpresa.Enabled = false;
            txtNombreEncargado.Enabled = false;
            txtNIT.Enabled = false;

            // Cliente individual
            txtNombres.Enabled = false;
            txtApellidos.Enabled = false;
            txtDUI.Enabled = false;

            // Campos compartidos
            txtTelefono.Enabled = false;
            txtCorreo.Enabled = false;
            txtDireccion.Enabled = false;

            // Estado
            cbEstadoCliente.Enabled = false;

            // El tipo de cliente no se puede cambiar
            cbTipoCliente.Enabled = false;
        }

        private void HabilitarCampos()
        {
            // Cliente corporativo
            txtNombreEmpresa.Enabled = true;
            txtNombreEncargado.Enabled = true;
            txtNIT.Enabled = true;

            // Cliente individual
            txtNombres.Enabled = true;
            txtApellidos.Enabled = true;
            txtDUI.Enabled = true;

            // Campos compartidos
            txtTelefono.Enabled = true;
            txtCorreo.Enabled = true;
            txtDireccion.Enabled = true;

            // Estado
            cbEstadoCliente.Enabled = true;

            // El tipo de cliente no se puede cambiar
            cbTipoCliente.Enabled = false;
        }
        //-----------------------------------------------------
        // VALIDACIONES

        private bool ValidarCampos()
        {
            // Al registrar se debe seleccionar un tipo de cliente
            if (!modoEdicion)
            {
                if (cbTipoCliente.SelectedIndex == -1)
                {
                    MessageBox.Show("Seleccione un tipo de cliente.");
                    return false;
                }
            }
            // SI ES PERSONA NATURAL


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
                    MessageBox.Show("Debe ingresar el DUI del cliente.");
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
                    MessageBox.Show("Debe ingresar el correo del cliente.");
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
                    MessageBox.Show("Debe ingresar el correo.");
                    txtCorreo.Focus();
                    return false;
                }

                if (string.IsNullOrWhiteSpace(txtDireccion.Text))
                {
                    MessageBox.Show("Debe ingresar la dirección de la empresa.");
                    txtDireccion.Focus();
                    return false;
                }
            }

            return true;
        }

        private bool ValidarCorreo()
        {
            try
            {
                MailAddress correo = new MailAddress(txtCorreo.Text);

                if (correo.Address != txtCorreo.Text)
                {
                    MessageBox.Show("Ingrese un correo válido.");
                    txtCorreo.Focus();
                    return false;
                }

                return true;
            }
            catch
            {
                MessageBox.Show("Ingrese un correo válido.");
                txtCorreo.Focus();
                return false;
            }
        }



        // DESACTIVAR COPIAR Y PEGAR

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

        // VALIDACIONES DE LOS TEXTBOX

        private void txtDUI_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Solo permite números y borrar
            if (!char.IsDigit(e.KeyChar) &&
                e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true;
            }

            // Máximo 10 caracteres
            if (char.IsDigit(e.KeyChar) &&
                txtDUI.Text.Length >= 10)
            {
                e.Handled = true;
            }
        }

        private void txtNIT_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Solo permite números y borrar
            if (!char.IsDigit(e.KeyChar) &&
                e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true;
            }

            // Máximo 14 caracteres
            if (char.IsDigit(e.KeyChar) &&
                txtNIT.Text.Length >= 14)
            {
                e.Handled = true;
            }
        }

        private void txtTelefono_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Solo permite números y borrar
            if (!char.IsDigit(e.KeyChar) &&
                e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true;
            }

            // Máximo 9 caracteres
            if (char.IsDigit(e.KeyChar) &&
                txtTelefono.Text.Length >= 9)
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

        private void txtNombres_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Solo permite letras, espacios y borrar
            if (!char.IsLetter(e.KeyChar) &&
                !char.IsControl(e.KeyChar) &&
                e.KeyChar != ' ')
            {
                e.Handled = true;
            }
        }

        private void txtApellidos_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Solo permite letras, espacios y borrar
            if (!char.IsLetter(e.KeyChar) &&
                !char.IsControl(e.KeyChar) &&
                e.KeyChar != ' ')
            {
                e.Handled = true;
            }
        }

        private void txtDireccion_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Permite letras, números y caracteres comunes de una dirección
            if (!char.IsLetterOrDigit(e.KeyChar) &&
                !char.IsControl(e.KeyChar) &&
                e.KeyChar != ' ' &&
                e.KeyChar != '#' &&
                e.KeyChar != '-' &&
                e.KeyChar != '/' &&
                e.KeyChar != '.' &&
                e.KeyChar != ',' &&
                e.KeyChar != '(' &&
                e.KeyChar != ')')
            {
                e.Handled = true;
            }
        }
        //-------------------------------------------------------------------------
        // CAMBIAR ENTRE PERSONA NATURAL Y EMPRESA


        private void btnClienteIndividual_Click(object sender, EventArgs e)
        {
            // Barras
            pnlBarraClienteIndividual.Visible = true;
            pnlBarraClienteCorporativo.Visible = false;

            // Paneles
            pnlRegistroClienteIndividual.Visible = true;
            pnlRegistroClienteCorporativo.Visible = false;

            //Barra de busqueda
            txtBuscarCorporativo.Visible = false;
            txtBuscarIndividual.Visible = true;
        }

        private void btnClienteCorporativo_Click(object sender, EventArgs e)
        {
            // Barras
            pnlBarraClienteIndividual.Visible = false;
            pnlBarraClienteCorporativo.Visible = true;

            // Paneles
            pnlRegistroClienteIndividual.Visible = false;
            pnlRegistroClienteCorporativo.Visible = true;

            //Barra de busqueda
            txtBuscarCorporativo.Visible = true;
            txtBuscarIndividual.Visible = false;
        }
        //------------------------------------------------------------------------------
        // CARGA DEL FORMULARIO
        private void frmClientesSecretario_Load(object sender, EventArgs e)
        {
            try
            {
                // Cargar los clientes
                MostrarClientesIndividuales();
                MostrarClientesCorporativos();
                ActualizarEstadisticas();

                // Desactivar copiar y pegar
                DesactivarCopiarPegar(this);

                // Mostrar persona natural al iniciar
                pnlRegistroClienteIndividual.Visible = true;
                pnlRegistroClienteCorporativo.Visible = false;

                pnlBarraClienteIndividual.Visible = true;
                pnlBarraClienteCorporativo.Visible = false;

                // Ocultar botones de edición
                btnEditar.Visible = false;
                btnGuardarCambios.Visible = false;

                // Límites de los campos
                txtDireccion.MaxLength = 200;
                txtApellidos.MaxLength = 40;
                txtNombres.MaxLength = 40;
                txtNombreEmpresa.MaxLength = 40;
                txtNombreEncargado.MaxLength = 40;
                txtDUI.MaxLength = 10;
                txtNIT.MaxLength = 14;
                txtTelefono.MaxLength = 9;
                txtCorreo.MaxLength = 50;


                // TABLA DE PERSONAS NATURALES

                dgvClientesIndividuales.Columns["IdCliente"].Visible = false;

                dgvClientesIndividuales.Columns["Nombre"].HeaderText = "Nombre";
                dgvClientesIndividuales.Columns["Apellidos"].HeaderText = "Apellidos";
                dgvClientesIndividuales.Columns["DUI"].HeaderText = "DUI";
                dgvClientesIndividuales.Columns["Telefono"].HeaderText = "Teléfono";
                dgvClientesIndividuales.Columns["Correo"].HeaderText = "Correo";
                dgvClientesIndividuales.Columns["Direccion"].HeaderText = "Dirección";
                dgvClientesIndividuales.Columns["Estado"].HeaderText = "Estado";

                // TABLA DE EMPRESAS


                dgvClientesCorporativos.Columns["IdCliente"].Visible = false;

                dgvClientesCorporativos.Columns["Nombre_De_Empresa"].HeaderText = "Empresa";
                dgvClientesCorporativos.Columns["Nombre_Del_Encargado"].HeaderText = "Encargado";
                dgvClientesCorporativos.Columns["NIT"].HeaderText = "NIT";
                dgvClientesCorporativos.Columns["Telefono"].HeaderText = "Teléfono";
                dgvClientesCorporativos.Columns["Correo"].HeaderText = "Correo";
                dgvClientesCorporativos.Columns["Direccion"].HeaderText = "Dirección";
                dgvClientesCorporativos.Columns["Estado"].HeaderText = "Estado";

                // Estado desactivado al comenzar
                cbEstadoCliente.Enabled = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrió un error al cargar el formulario.\n" + ex.Message, "Error", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }
        //-----------------------------------------------------------------------------------------------
        // CAMBIO DE TIPO DE CLIENTE
        private void cbTipoCliente_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            // PERSONA NATURAL


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


            // EMPRESA

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
            // EMPRESA
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
            // PERSONA NATURAL
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
        //--------------------------------------------------------------
        // REGISTRAR CLIENTE INDIVIDUAL
        private void btnGuardarIndividual_Click(object sender, EventArgs e)
        {

            // Validar campos obligatorios
            if (!ValidarCampos())
                return;

            // Validar DUI
            if (txtDUI.Text.Length != 10 || txtDUI.Text[8] != '-')
            {
                MessageBox.Show("El DUI debe tener el formato 12345678-9.");
                txtDUI.Focus();
                return;
            }

            // Validar teléfono
            if (txtTelefono.Text.Length != 9 || txtTelefono.Text[4] != '-')
            {
                MessageBox.Show("El teléfono debe tener el formato 1234-5678.");
                txtTelefono.Focus();
                return;
            }

            // Validar correo
            if (!ValidarCorreo())
                return;

            try
            {
                DbCliente cliente = new DbCliente();

                cliente.TipoCliente1 = 2;
                cliente.Identificador11 = txtNombres.Text.Trim();
                cliente.Identificador21 = txtApellidos.Text.Trim();
                cliente.Documento1 = txtDUI.Text.Trim();
                cliente.Telefono1 = txtTelefono.Text.Trim();
                cliente.Correo1 = txtCorreo.Text.Trim();
                cliente.Direccion1 = txtDireccion.Text.Trim();
                cliente.Estado1 = "Activo";

                // Guardar cliente
                if (cliente.InsertarClienteIndividual())
                {
                    MessageBox.Show("Cliente individual registrado correctamente.", "Registro exitoso", MessageBoxButtons.OK, MessageBoxIcon.Information
                    );

                    MostrarClientesIndividuales();
                    LimpiarFormularioCliente();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrió un error al registrar el cliente.\n" + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        // REGISTRAR CLIENTE CORPORATIVO
        private void btnGuardarCorporativo_Click_1(object sender, EventArgs e)
        {
            // Validar campos obligatorios
            if (!ValidarCampos())
                return;

            // Validar NIT
            if (txtNIT.Text.Length != 14)
            {
                MessageBox.Show("El NIT debe tener 14 números.");
                txtNIT.Focus();
                return;
            }

            // Validar teléfono
            if (txtTelefono.Text.Length != 9 ||
                txtTelefono.Text[4] != '-')
            {
                MessageBox.Show(
                    "El teléfono debe tener el formato 1234-5678."
                );
                txtTelefono.Focus();
                return;
            }

            // Validar correo
            if (!ValidarCorreo())
                return;

            try
            {
                DbCliente cliente = new DbCliente();

                cliente.TipoCliente1 = 1;
                cliente.Identificador11 = txtNombreEmpresa.Text.Trim();
                cliente.Identificador21 = txtNombreEncargado.Text.Trim();
                cliente.Documento1 = txtNIT.Text.Trim();
                cliente.Telefono1 = txtTelefono.Text.Trim();
                cliente.Correo1 = txtCorreo.Text.Trim();
                cliente.Direccion1 = txtDireccion.Text.Trim();
                cliente.Estado1 = "Activo";

                // Guardar cliente
                if (cliente.InsertarClienteCorporativo())
                {
                    MessageBox.Show(
                        "Cliente corporativo registrado correctamente.",
                        "Registro exitoso",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );

                    MostrarClientesCorporativos();
                    LimpiarFormularioCliente();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Ocurrió un error al registrar el cliente.\n" + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }

        }

        //----------------------------------------------------------------------
        // SELECCIONAR CLIENTE CORPORATIVO
        private void dgvClientesCorporativos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                // Revisar que se haya seleccionado una fila válida
                if (e.RowIndex < 0 ||
                    dgvClientesCorporativos.Rows[e.RowIndex].IsNewRow)
                    return;

                DataGridViewRow fila =
                    dgvClientesCorporativos.Rows[e.RowIndex];

                // Obtener el ID del cliente
                idClienteSeleccionado =
                    Convert.ToInt32(fila.Cells["IdCliente"].Value);

                tipoClienteSeleccionado = 1;

                // Guardar los datos originales
                identificador1Original =
                    fila.Cells["Nombre_De_Empresa"].Value?.ToString() ?? "";

                identificador2Original =
                    fila.Cells["Nombre_Del_Encargado"].Value?.ToString() ?? "";

                documentoOriginal =
                    fila.Cells["NIT"].Value?.ToString() ?? "";

                telefonoOriginal =
                    fila.Cells["Telefono"].Value?.ToString() ?? "";

                correoOriginal =
                    fila.Cells["Correo"].Value?.ToString() ?? "";

                direccionOriginal =
                    fila.Cells["Direccion"].Value?.ToString() ?? "";

                estadoOriginal =
                    fila.Cells["Estado"].Value?.ToString() ?? "";

                // Mostrar los datos en el formulario
                txtNombreEmpresa.Text = identificador1Original;
                txtNombreEncargado.Text = identificador2Original;
                txtNIT.Text = documentoOriginal;
                txtTelefono.Text = telefonoOriginal;
                txtCorreo.Text = correoOriginal;
                txtDireccion.Text = direccionOriginal;
                cbEstadoCliente.Text = estadoOriginal;

                // Bloquear campos hasta presionar Editar
                BloquearCampos();

                modoEdicion = false;

                // Mostrar botones
                btnEditar.Visible = true;
                btnGuardarCambios.Visible = false;

                btnGuardarCorporativo.Visible = false;
                btnGuardarIndividual.Visible = false;

                // Mostrar datos de empresa
                gbDatosEmpresa.Visible = true;
                gbPersonaNatural.Visible = false;

                // Mostrar tabla corporativa
                pnlRegistroClienteIndividual.Visible = false;
                pnlRegistroClienteCorporativo.Visible = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Ocurrió un error al seleccionar el cliente.\n" + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        // SELECCIONAR CLIENTE INDIVIDUAL
        private void dgvClientesIndividuales_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                // Revisar que se haya seleccionado una fila válida
                if (e.RowIndex < 0 ||
                    dgvClientesIndividuales.Rows[e.RowIndex].IsNewRow)
                    return;

                DataGridViewRow fila =
                    dgvClientesIndividuales.Rows[e.RowIndex];

                // Obtener el ID del cliente
                idClienteSeleccionado =
                    Convert.ToInt32(fila.Cells["IdCliente"].Value);

                tipoClienteSeleccionado = 2;

                // Guardar los datos originales
                identificador1Original =
                    fila.Cells[1].Value?.ToString() ?? "";

                identificador2Original =
                    fila.Cells[2].Value?.ToString() ?? "";

                documentoOriginal =
                    fila.Cells[3].Value?.ToString() ?? "";

                telefonoOriginal =
                    fila.Cells[4].Value?.ToString() ?? "";

                correoOriginal =
                    fila.Cells[5].Value?.ToString() ?? "";

                direccionOriginal =
                    fila.Cells[6].Value?.ToString() ?? "";

                estadoOriginal =
                    fila.Cells[7].Value?.ToString() ?? "";

                // Mostrar los datos en el formulario
                txtNombres.Text = identificador1Original;
                txtApellidos.Text = identificador2Original;
                txtDUI.Text = documentoOriginal;
                txtTelefono.Text = telefonoOriginal;
                txtCorreo.Text = correoOriginal;
                txtDireccion.Text = direccionOriginal;
                cbEstadoCliente.Text = estadoOriginal;

                // Bloquear campos hasta presionar Editar
                BloquearCampos();

                modoEdicion = false;

                // Mostrar botones
                btnEditar.Visible = true;
                btnGuardarIndividual.Visible = false;
                btnGuardarCorporativo.Visible = false;
                btnGuardarCambios.Visible = false;

                // Mostrar datos de persona natural
                gbDatosEmpresa.Visible = false;
                gbPersonaNatural.Visible = true;

                // Mostrar tabla individual
                pnlRegistroClienteIndividual.Visible = true;
                pnlRegistroClienteCorporativo.Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Ocurrió un error al seleccionar el cliente.\n" + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        //-------------------------------------------------------------------------------
        // BOTÓN EDITAR

        private void btnEditar_Click_1(object sender, EventArgs e)
        {
            if (idClienteSeleccionado == 0)
            {
                MessageBox.Show("Seleccione un cliente primero.");
                return;
            }

            // Activar modo edición
            modoEdicion = true;

            // Habilitar los campos
            HabilitarCampos();

            // Ocultar botón Editar
            btnEditar.Visible = false;

            // Mostrar botón Guardar cambios
            btnGuardarCambios.Visible = true;

            // Ocultar botones de registro
            btnGuardarCorporativo.Visible = false;
            btnGuardarIndividual.Visible = false;
        }

        //----------------------------------------------------------------------------------
        //BOTON GUARDAR CAMBIOS
        private void btnGuardarCambios_Click_1(object sender, EventArgs e)
        {
            if (idClienteSeleccionado == 0)
            {
                MessageBox.Show("Seleccione un cliente primero.");
                return;
            }

            if (!modoEdicion)
            {
                MessageBox.Show(
                    "Debe presionar Editar antes de guardar cambios."
                );
                return;
            }

            // Revisar si realmente se modificó algún dato
            if (!HayCambios())
            {
                MessageBox.Show(
                    "No se han realizado cambios en los datos del cliente.",
                    "Sin cambios",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );
                return;
            }

            // Validar campos
            if (!ValidarCampos())
                return;

            // Validar correo
            if (!ValidarCorreo())
                return;

            // VALIDAR SEGÚN EL TIPO DE CLIENTE

            // Persona natural
            if (tipoClienteSeleccionado == 2)
            {
                if (txtDUI.Text.Length != 10 ||
                    txtDUI.Text[8] != '-')
                {
                    MessageBox.Show(
                        "El DUI debe tener el formato 12345678-9."
                    );
                    txtDUI.Focus();
                    return;
                }
            }

            // Empresa
            else if (tipoClienteSeleccionado == 1)
            {
                if (txtNIT.Text.Length != 14)
                {
                    MessageBox.Show(
                        "El NIT debe tener 14 números."
                    );
                    txtNIT.Focus();
                    return;
                }
            }

            // Validar teléfono
            if (txtTelefono.Text.Length != 9 ||
                txtTelefono.Text[4] != '-')
            {
                MessageBox.Show(
                    "El teléfono debe tener el formato 1234-5678."
                );
                txtTelefono.Focus();
                return;
            }

            try
            {
                DbCliente cliente = new DbCliente();

                cliente.IdCliente1 = idClienteSeleccionado;
                cliente.TipoCliente1 = tipoClienteSeleccionado;

                // PERSONA NATURAL


                if (tipoClienteSeleccionado == 2)
                {
                    cliente.Identificador11 =
                        txtNombres.Text.Trim();

                    cliente.Identificador21 =
                        txtApellidos.Text.Trim();

                    cliente.Documento1 =
                        txtDUI.Text.Trim();
                }

                // EMPRESA

                else if (tipoClienteSeleccionado == 1)
                {
                    cliente.Identificador11 =
                        txtNombreEmpresa.Text.Trim();

                    cliente.Identificador21 =
                        txtNombreEncargado.Text.Trim();

                    cliente.Documento1 =
                        txtNIT.Text.Trim();
                }

                // Datos que comparten ambos tipos
                cliente.Telefono1 =
                    txtTelefono.Text.Trim();

                cliente.Correo1 =
                    txtCorreo.Text.Trim();

                cliente.Direccion1 =
                    txtDireccion.Text.Trim();

                cliente.Estado1 =
                    cbEstadoCliente.Text.Trim();

                // Actualizar los datos
                if (cliente.ActualizarCliente())
                {
                    MessageBox.Show(
                        "Cliente actualizado correctamente.",
                        "Actualización exitosa",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );

                    // Actualizar la tabla correspondiente
                    if (tipoClienteSeleccionado == 1)
                    {
                        MostrarClientesCorporativos();
                    }
                    else if (tipoClienteSeleccionado == 2)
                    {
                        MostrarClientesIndividuales();
                    }

                    // Dejar el formulario listo para otro cliente
                    LimpiarFormularioCliente();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Ocurrió un error al actualizar el cliente.\n" + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        //------------------------------------------------------------------------

        // COMPROBAR SI HUBO CAMBIOS
        private bool HayCambios()
        {
            string identificador1Actual;
            string identificador2Actual;
            string documentoActual;

            // Obtener los datos actuales
            if (tipoClienteSeleccionado == 2)
            {
                // Persona natural
                identificador1Actual =
                    txtNombres.Text.Trim();

                identificador2Actual =
                    txtApellidos.Text.Trim();

                documentoActual =
                    txtDUI.Text.Trim();
            }
            else
            {
                // Empresa
                identificador1Actual =
                    txtNombreEmpresa.Text.Trim();

                identificador2Actual =
                    txtNombreEncargado.Text.Trim();

                documentoActual =
                    txtNIT.Text.Trim();
            }

            string telefonoActual =
                txtTelefono.Text.Trim();

            string correoActual =
                txtCorreo.Text.Trim();

            string direccionActual =
                txtDireccion.Text.Trim();

            string estadoActual =
                cbEstadoCliente.Text.Trim();

            // Comparar los datos actuales con los originales
            return
                identificador1Actual != identificador1Original ||
                identificador2Actual != identificador2Original ||
                documentoActual != documentoOriginal ||
                telefonoActual != telefonoOriginal ||
                correoActual != correoOriginal ||
                direccionActual != direccionOriginal ||
                estadoActual != estadoOriginal;
        }
        //----------------------------------------------------------------
        //LIMPIAR FORMULARIO
        private void LimpiarFormularioCliente()
        {
            // Habilitar los campos para registrar un nuevo cliente
            HabilitarCampos();

            // Permitir seleccionar nuevamente el tipo de cliente
            cbTipoCliente.Enabled = true;

            // Limpiar persona natural
            txtNombres.Clear();
            txtApellidos.Clear();
            txtDUI.Clear();

            // Limpiar empresa
            txtNombreEmpresa.Clear();
            txtNombreEncargado.Clear();
            txtNIT.Clear();

            // Limpiar campos compartidos
            txtTelefono.Clear();
            txtCorreo.Clear();
            txtDireccion.Clear();

            // Reiniciar estado
            cbEstadoCliente.Text = "Activo";
            cbEstadoCliente.Enabled = false;

            // Reiniciar datos seleccionados
            idClienteSeleccionado = 0;
            tipoClienteSeleccionado = 0;
            modoEdicion = false;

            // Ocultar botones de edición
            btnEditar.Visible = false;
            btnGuardarCambios.Visible = false;

            // Mostrar botón Guardar según el tipo seleccionado
            if (cbTipoCliente.SelectedIndex == 0)
            {
                btnGuardarIndividual.Visible = true;
                btnGuardarCorporativo.Visible = false;
            }
            else if (cbTipoCliente.SelectedIndex == 1)
            {
                btnGuardarIndividual.Visible = false;
                btnGuardarCorporativo.Visible = true;
            }
        }
        //------------------------------------------------------------------------

        //BUSCAR CLIENTES

        private void txtBuscarCorporativo_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtBuscarCorporativo.Text == "Buscar Cliente...")
                    return;

                dgvClientesCorporativos.DataSource = DbCliente.BuscarClienteCorporativo(txtBuscarCorporativo.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void txtBuscarCorporativo_Leave(object sender, EventArgs e)
        {
            txtBuscarCorporativo.Text = "Buscar Cliente...";
            txtBuscarCorporativo.ForeColor = Color.Gray;
        }

        private void txtBuscarCorporativo_Enter(object sender, EventArgs e)
        {
            //Cuando el usuario de enter para escribir, se va a borrar el texto de indicacion
            // Y el texto ya no sera opaco, sera color negro
            if (txtBuscarCorporativo.Text == "Buscar Cliente...")
            {
                txtBuscarCorporativo.Text = "";
                txtBuscarCorporativo.ForeColor = Color.Black;

            }
        }

        private void txtBuscarIndividual_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtBuscarIndividual.Text == "Buscar Cliente...")
                    return;

                dgvClientesIndividuales.DataSource = DbCliente.BuscarClienteIndividual(txtBuscarIndividual.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void txtBuscarIndividual_Leave(object sender, EventArgs e)
        {
            txtBuscarIndividual.Text = "Buscar Cliente...";
            txtBuscarIndividual.ForeColor = Color.Gray;
        }

        private void txtBuscarIndividual_Enter(object sender, EventArgs e)
        {
            //Cuando el usuario de enter para escribir, se va a borrar el texto de indicacion
            // Y el texto ya no sera opaco, sera color negro
            if (txtBuscarIndividual.Text == "Buscar Cliente...")
            {
                txtBuscarIndividual.Text = "";
                txtBuscarIndividual.ForeColor = Color.Black;

            }
        }

        //-----------------------------------------------------------------------------------

        //BOTON DE NUEVO CLIENTE

        private void btnNuevoCliente_Click(object sender, EventArgs e)
        {
            LimpiarFormularioCliente();
        }


    }
}




