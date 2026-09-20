using Modelo.Entidades;
using System;
using System.Drawing;
using System.Net.Mail;
using System.Windows.Forms;
using Vista.Responsive;


namespace Vista.Proveedores
{
    public partial class frmProveedores : Form
    {
        public frmProveedores()
        {
            InitializeComponent();
            ResponsiveHelper.Apply(this);
        }

        private void txtBuscar_Enter(object sender, EventArgs e)
        {
            //Cuando el usuario de enter para escribir, se va a borrar el texto de indicacion
            // Y el texto ya no sera opaco, sera color negro
            if (txtBuscar.Text == "Buscar Proveedor...")
            {
                txtBuscar.Text = "";
                txtBuscar.ForeColor = Color.Black;
            }
        }

        private void txtBuscar_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtBuscar.Text))
            {
                txtBuscar.Text = "Buscar proveedor...";
                txtBuscar.ForeColor = Color.Gray;
            }
        }
        //------------------------------------------------------------------------------------------------------
        //// CONFIGURAR TOOLTIPS
        private void ConfigurarTooltips()
        {
            // Crear ToolTip
            ToolTip toolTip1 = new ToolTip();

            // Propiedades del ToolTip
            toolTip1.AutoPopDelay = 5000;
            toolTip1.InitialDelay = 500;
            toolTip1.ReshowDelay = 200;
            toolTip1.ShowAlways = true;

            // Búsqueda
            toolTip1.SetToolTip(txtBuscar,
                "Buscar un proveedor por nombre, teléfono o correo.");

            // Datos del proveedor
            toolTip1.SetToolTip(txtNombreProveedor,
                "Ingrese el nombre del proveedor.");

            toolTip1.SetToolTip(txtCorreo,
                "Ingrese el correo electrónico del proveedor.");

            toolTip1.SetToolTip(txtTelefono,
                "Ingrese el número de teléfono del proveedor.");

            toolTip1.SetToolTip(txtUbicacion,
                "Ingrese la ubicación o dirección del proveedor.");

            // Estado
            toolTip1.SetToolTip(chkEstado,
                "Indica si el proveedor se encuentra activo.");

            // Botones
            toolTip1.SetToolTip(btnGuardar,
                "Guarda el nuevo proveedor.");

            toolTip1.SetToolTip(btnEditar,
                "Permite editar los datos del proveedor seleccionado.");

            toolTip1.SetToolTip(btnGuardarCambios,
                "Guarda los cambios realizados al proveedor.");

            toolTip1.SetToolTip(btnDesactivar,
                "Desactiva el proveedor seleccionado.");

            // Tabla
            toolTip1.SetToolTip(dgvProveedores,
                "Muestra los proveedores registrados. Haz doble clic en un proveedor para seleccionarlo.");
        }
        //----------------------------------------------------------------------------------------------
        //----------------------EVENTO LOAD DEL FORMULARIO-------------------------------------------------//
        private void frmProveedores_Load(object sender, EventArgs e)
        {
            MostrarProveedor();

            //CONFIGURACION DE TOOLTIPS
            ConfigurarTooltips();
            //Maximo de caracteres admitidos
            txtNombreProveedor.MaxLength = 50;
            txtCorreo.MaxLength = 100;
            txtTelefono.MaxLength = 9;
            txtUbicacion.MaxLength = 200;
            //Navegar con la tecla Tab
            txtNombreProveedor.TabIndex = 1;
            txtCorreo.TabIndex = 2;
            txtTelefono.TabIndex = 3;
            txtUbicacion.TabIndex = 4;
            btnGuardar.TabIndex = 5;
            btnEditar.TabIndex = 6;
            btnDesactivar.TabIndex = 7;

            btnEditar.Visible = false;

            dgvProveedores.Columns["IdProveedor"].Visible = false;

            //Declaramos que el estado del provedor al momento de registrar siempre sea activo, hasta que el usuario lo desactive
            chkEstado.Checked = true;
            chkEstado.Enabled = false;
            chkEstado.Visible = false;
        }
        private void MostrarProveedor()
        {
            dgvProveedores.DataSource = null;
            dgvProveedores.DataSource = DbProveedor.CargarProveedor();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            //Validar que el nombre del proveedor no quede vacío
            if (string.IsNullOrWhiteSpace(txtNombreProveedor.Text))
            {
                MessageBox.Show("Ingrese el nombre del proveedor.");
                txtNombreProveedor.Focus();
                return;
            }
            //Validar que telefono no este vacío
            if (string.IsNullOrWhiteSpace(txtTelefono.Text))
            {
                MessageBox.Show("Ingrese el teléfono del proveedor.");
                txtTelefono.Focus();
                return;
            }
            // Validar que ubicacion no este vacía
            if (string.IsNullOrWhiteSpace(txtUbicacion.Text))
            {
                MessageBox.Show("Ingrese la ubicación del proveedor.");
                txtUbicacion.Focus();
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

            DbProveedor proveedor = new DbProveedor();

            // TRIM elimina los espacios de los extremos
            proveedor.Nombre_Proveedor1 = txtNombreProveedor.Text.Trim();
            proveedor.Telefono1 = txtTelefono.Text.Trim();
            proveedor.Correo1 = txtCorreo.Text.Trim();
            proveedor.Ubicacion1 = txtUbicacion.Text.Trim();
            // Todo proveedor su estado inicial siempre sera activo, hasta que el usuario decida desactivarlo
            proveedor.Estado1 = true;


            if (proveedor.InsertarProveedor())
            {
                MessageBox.Show("Proveedor registrado correctamente.", "Registro exitoso", MessageBoxButtons.OK, MessageBoxIcon.Information);

                Limpiar();
                // Actualiza la tabla de proveedores

                MostrarProveedor();

            }
        }

        private int idProveedorSeleccionado = 0;
        private void dgvProveedor_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            BloquearCampos();
            dgvProveedores.Columns["IdProveedor"].Visible = false;

            if (e.RowIndex >= 0)
            {
                DataGridViewRow fila = dgvProveedores.Rows[e.RowIndex];
                idProveedorSeleccionado = Convert.ToInt32(fila.Cells["IdProveedor"].Value);
                txtNombreProveedor.Text = fila.Cells["Proveedor"].Value.ToString();
                txtTelefono.Text = fila.Cells["Telefono"].Value.ToString();
                txtCorreo.Text = fila.Cells["Correo"].Value.ToString();
                txtUbicacion.Text = fila.Cells["Ubicacion"].Value.ToString();
                chkEstado.Checked = fila.Cells["Estado"].Value.ToString() == "Activo";


                btnEditar.Visible = true;
                btnGuardar.Visible = true;

            }
        }
        private void btnEditar_Click(object sender, EventArgs e)
        {
            btnGuardar.Visible = false;
            btnGuardarCambios.Visible = true;
            chkEstado.Visible = true;
            HabilitarCampos();

        }


        private void HabilitarCampos()
        {
            chkEstado.Enabled = true;

            txtNombreProveedor.ReadOnly = false;
            txtTelefono.ReadOnly = false;
            txtCorreo.ReadOnly = false;
            txtUbicacion.ReadOnly = false;


        }

        private void BloquearCampos()
        {
            chkEstado.Enabled = false;
            txtNombreProveedor.ReadOnly = true;
            txtTelefono.ReadOnly = true;
            txtCorreo.ReadOnly = true;
            txtUbicacion.ReadOnly = true;


        }


        private void Limpiar()
        {
            txtNombreProveedor.Clear();
            txtTelefono.Clear();
            txtCorreo.Clear();
            txtUbicacion.Clear();
            idProveedorSeleccionado = 0;
            btnEditar.Visible = false;
            btnGuardar.Visible = true;
            btnDesactivar.Visible = true;
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

        private void txtCorreo_TextChanged(object sender, EventArgs e)
        {

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

        private void txtNombreProveedor_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar) && e.KeyChar != ' ')
            {
                e.Handled = true;
            }
        }

        private void txtUbicacion_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetterOrDigit(e.KeyChar) && !char.IsControl(e.KeyChar) && e.KeyChar != ' ' && e.KeyChar != '#' && e.KeyChar != '-' && e.KeyChar != '/' &&
           e.KeyChar != '.' && e.KeyChar != ',' && e.KeyChar != '(' && e.KeyChar != ')')
            {
                e.Handled = true;
            }
        }

        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtBuscar.Text == "Buscar proveedor...")
                    return;

                dgvProveedores.DataSource = DbProveedor.BuscarProveedor(txtBuscar.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            dgvProveedores.Columns["IdProveedor"].Visible = false;
        }

        private void btnDesactivar_Click(object sender, EventArgs e)
        {
            if (idProveedorSeleccionado == 0)
            {
                MessageBox.Show("Seleccione un proveedor.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            DbProveedor proveedor = new DbProveedor();

            bool resultado = proveedor.DesactivarProveedor(idProveedorSeleccionado);

            if (resultado)
            {
                MessageBox.Show("El proveedor fue desactivado correctamente.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);

                MostrarProveedor();
            }
            else
            {
                MessageBox.Show("El proveedor ya está inactivo.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnGuardarCambios_Click(object sender, EventArgs e)
        {
            if (idProveedorSeleccionado == 0)
            {
                MessageBox.Show("Seleccione un proveedor para editar.");
                return;
            }

            DbProveedor proveedor = new DbProveedor();
            proveedor.IdProveedor1 = idProveedorSeleccionado;
            proveedor.Nombre_Proveedor1 = txtNombreProveedor.Text.Trim();
            proveedor.Telefono1 = txtTelefono.Text.Trim();
            proveedor.Correo1 = txtCorreo.Text.Trim();
            proveedor.Ubicacion1 = txtUbicacion.Text.Trim();

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

            if (proveedor.ActualizarProveedor())
            {
                MessageBox.Show("Proveedor actualizado correctamente.");
                MostrarProveedor();
                Limpiar();
            }
            else
            {
                MessageBox.Show("Error al actualizar el proveedor.");
            }
            dgvProveedores.Columns["IdProveedor"].Visible = false;
        }



        private void btnNuevo_Click(object sender, EventArgs e)
        {
            Limpiar();
        }
    }
}
