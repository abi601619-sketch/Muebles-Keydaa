using Modelo.Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;
using Vista.Clientes;
using Vista.Responsive;

namespace Vista.Ventas
{
    public partial class frmVentas : Form
    {
        // Lista temporal donde se guardarn los productos
        // antes de guardar la venta en la base de datos
        private List<DetalleVenta> detallesVenta = new List<DetalleVenta>();
        // Variables para la paginación
        private DataTable dtVentas;
        private int paginaActual = 1;
        private int registrosPorPagina = 20;
        private int totalPaginas = 0;
        public frmVentas()
        {
            InitializeComponent();
            ResponsiveHelper.Apply(this);
        }

        private void panel6_Paint(object sender, PaintEventArgs e)
        {

        }

        private void txtBuscar_Enter(object sender, EventArgs e)
        {
            //Cuando el usuario de enter para escribir, se va a borrar el texto de indicacion
            // Y el texto ya no sera opaco, sera color negro
            if (txtBuscar.Text == "Buscar Venta...")
            {
                txtBuscar.Text = "";
                txtBuscar.ForeColor = Color.Black;

            }
        }

        private void txtBuscar_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtBuscar.Text))
            {
                txtBuscar.Text = "Buscar venta...";
                txtBuscar.ForeColor = Color.Gray;
            }
        }
        public void MostrarVentas()
        {
            try
            {
                // Cargar todas las ventas
                dtVentas = DbVentas.CargarVentas();

                // Iniciar desde la primera página
                paginaActual = 1;

                // Calcular cantidad de páginas
                CalcularPaginasVentas();

                // Mostrar la primera página
                MostrarPaginaVentas();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Error al cargar las ventas:\n" + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        // CONFIGURAR DISEÑO DE LAS TABLAS
        private void ConfigurarTablasVentas()
        {

            // TABLA DE VENTAS

            // Encabezado
            dgvVentas.EnableHeadersVisualStyles = false;

            dgvVentas.ColumnHeadersDefaultCellStyle.BackColor =
                Color.FromArgb(121, 75, 45);

            dgvVentas.ColumnHeadersDefaultCellStyle.ForeColor =
                Color.White;

            dgvVentas.ColumnHeadersDefaultCellStyle.Font =
                new Font("Times New Roman", 9, FontStyle.Regular);

            dgvVentas.ColumnHeadersDefaultCellStyle.Alignment =
                DataGridViewContentAlignment.MiddleCenter;

            dgvVentas.ColumnHeadersDefaultCellStyle.SelectionBackColor =
                Color.FromArgb(121, 75, 45);

            dgvVentas.ColumnHeadersDefaultCellStyle.SelectionForeColor =
                Color.White;

            // Filas
            dgvVentas.DefaultCellStyle.BackColor =
                Color.White;

            dgvVentas.DefaultCellStyle.ForeColor =
                Color.FromArgb(45, 45, 45);

            dgvVentas.DefaultCellStyle.Font =
                new Font("Segoe UI", 9, FontStyle.Regular);

            dgvVentas.DefaultCellStyle.Alignment =
                DataGridViewContentAlignment.MiddleLeft;

            // Filas alternadas
            dgvVentas.AlternatingRowsDefaultCellStyle.BackColor =
                Color.FromArgb(248, 241, 232);

            // Selección
            dgvVentas.DefaultCellStyle.SelectionBackColor =
                Color.FromArgb(224, 193, 157);

            dgvVentas.DefaultCellStyle.SelectionForeColor =
                Color.Black;

            // Bordes
            dgvVentas.CellBorderStyle =
                DataGridViewCellBorderStyle.SingleHorizontal;

            dgvVentas.GridColor =
                Color.FromArgb(220, 220, 220);

            // Alto de las filas
            dgvVentas.RowTemplate.Height = 32;

            // Alto del encabezado
            dgvVentas.ColumnHeadersHeight = 30;

            // No permitir modificar
            dgvVentas.ReadOnly = true;

            dgvVentas.AllowUserToAddRows = false;

            dgvVentas.AllowUserToDeleteRows = false;

            // Seleccionar fila completa
            dgvVentas.SelectionMode =
                DataGridViewSelectionMode.FullRowSelect;

            dgvVentas.MultiSelect = false;

            // Quitar borde exterior
            dgvVentas.BorderStyle =
                BorderStyle.None;

            // TABLA DETALLE DE VENTA

            // Encabezado
            dgvDetalleDeVenta.EnableHeadersVisualStyles = false;

            dgvDetalleDeVenta.ColumnHeadersDefaultCellStyle.BackColor =
                Color.FromArgb(121, 75, 45);

            dgvDetalleDeVenta.ColumnHeadersDefaultCellStyle.ForeColor =
                Color.White;

            dgvDetalleDeVenta.ColumnHeadersDefaultCellStyle.Font =
                new Font("Times New Roman", 9, FontStyle.Regular);

            dgvDetalleDeVenta.ColumnHeadersDefaultCellStyle.Alignment =
                DataGridViewContentAlignment.MiddleCenter;

            dgvDetalleDeVenta.ColumnHeadersDefaultCellStyle.SelectionBackColor =
                Color.FromArgb(121, 75, 45);

            dgvDetalleDeVenta.ColumnHeadersDefaultCellStyle.SelectionForeColor =
                Color.White;

            // Filas
            dgvDetalleDeVenta.DefaultCellStyle.BackColor =
                Color.White;

            dgvDetalleDeVenta.DefaultCellStyle.ForeColor =
                Color.FromArgb(45, 45, 45);

            dgvDetalleDeVenta.DefaultCellStyle.Font =
                new Font("Segoe UI", 9, FontStyle.Regular);

            dgvDetalleDeVenta.DefaultCellStyle.Alignment =
                DataGridViewContentAlignment.MiddleLeft;

            // Filas alternadas
            dgvDetalleDeVenta.AlternatingRowsDefaultCellStyle.BackColor =
                Color.FromArgb(248, 241, 232);

            // Selección
            dgvDetalleDeVenta.DefaultCellStyle.SelectionBackColor =
                Color.FromArgb(224, 193, 157);

            dgvDetalleDeVenta.DefaultCellStyle.SelectionForeColor =
                Color.Black;

            // Bordes
            dgvDetalleDeVenta.CellBorderStyle =
                DataGridViewCellBorderStyle.SingleHorizontal;

            dgvDetalleDeVenta.GridColor =
                Color.FromArgb(220, 220, 220);

            // Alto de las filas
            dgvDetalleDeVenta.RowTemplate.Height = 32;

            // Alto del encabezado
            dgvDetalleDeVenta.ColumnHeadersHeight = 30;

            // No permitir modificar
            dgvDetalleDeVenta.ReadOnly = true;

            dgvDetalleDeVenta.AllowUserToAddRows = false;

            dgvDetalleDeVenta.AllowUserToDeleteRows = false;

            // Seleccionar fila completa
            dgvDetalleDeVenta.SelectionMode =
                DataGridViewSelectionMode.FullRowSelect;

            dgvDetalleDeVenta.MultiSelect = false;

            // Quitar borde exterior
            dgvDetalleDeVenta.BorderStyle =
                BorderStyle.None;
        }

        private void MostrarPaginaVentas()
        {
            if (dtVentas == null)
                return;

            DataTable dtPagina = dtVentas.Clone();

            int inicio = (paginaActual - 1) * registrosPorPagina;

            int fin = Math.Min(
                inicio + registrosPorPagina,
                dtVentas.Rows.Count);

            for (int i = inicio; i < fin; i++)
            {
                dtPagina.ImportRow(dtVentas.Rows[i]);
            }


            // Mostrar únicamente los registros de la página actual
            dgvVentas.DataSource = null;
            dgvVentas.DataSource = dtPagina;

            // Configurar columna del número de venta
            if (dgvVentas.Columns.Contains("IdVenta"))
            {
                dgvVentas.Columns["IdVenta"].HeaderText = "N° de Venta";
            }

            // Aplicar diseño
            ConfigurarTablasVentas();

            // Mostrar página actual
            lblPagina.Text =
                $"Página {paginaActual} de {totalPaginas}";

            // Activar o desactivar botones
            btnAnterior.Enabled = paginaActual > 1;
            btnSiguiente.Enabled = paginaActual < totalPaginas;
        }

        private void CalcularPaginasVentas()
        {
            if (dtVentas == null || dtVentas.Rows.Count == 0)
            {
                totalPaginas = 1;
                paginaActual = 1;
                return;
            }

            totalPaginas = (int)Math.Ceiling(
                (double)dtVentas.Rows.Count / registrosPorPagina);

            if (totalPaginas == 0)
                totalPaginas = 1;

            if (paginaActual > totalPaginas)
                paginaActual = totalPaginas;
        }


        private void btnAnterior_Click(object sender, EventArgs e)
        {
            if (paginaActual > 1)
            {
                paginaActual--;

                MostrarPaginaVentas();
            }
        }

        private void btnSiguiente_Click(object sender, EventArgs e)
        {
            if (paginaActual < totalPaginas)
            {
                paginaActual++;

                MostrarPaginaVentas();
            }
        }
        //-----------------------------------------------------------------
        // CONFIGURAR TOOLTIPS
        private void ConfigurarTooltips()
        {
            // Crear ToolTip
            ToolTip toolTip1 = new ToolTip();

            // Propiedades del ToolTip
            toolTip1.AutoPopDelay = 5000;
            toolTip1.InitialDelay = 500;
            toolTip1.ReshowDelay = 200;
            toolTip1.ShowAlways = true;

            // Tooltips de búsqueda
            toolTip1.SetToolTip(txtBuscar,
                "Buscar una venta por número, cliente o información relacionada.");

            toolTip1.SetToolTip(btnBuscarCliente,
                "Buscar y seleccionar el cliente para la venta.");

            toolTip1.SetToolTip(txtMostrarCliente,
                "Muestra el cliente seleccionado para la venta.");

            // Datos de la venta
            toolTip1.SetToolTip(dtFechaVenta,
                "Seleccione la fecha en que se realizó la venta.");

            toolTip1.SetToolTip(cbMetodoPago,
                "Seleccione el método de pago de la venta.");

            // Productos
            toolTip1.SetToolTip(btnAgregarProductos,
                "Agregar productos a la venta.");

            toolTip1.SetToolTip(dgvDetalleDeVenta,
                "Muestra los productos agregados a la venta.");

            // Totales
            toolTip1.SetToolTip(txtSubTotal,
                "Muestra el subtotal de los productos de la venta.");

            toolTip1.SetToolTip(txtIVA,
                "Muestra el IVA correspondiente al subtotal.");

            toolTip1.SetToolTip(txtTotalPagar,
                "Muestra el total a pagar de la venta.");

            // Botones de venta
            toolTip1.SetToolTip(btnGuardar,
                "Guardar la nueva venta.");

            toolTip1.SetToolTip(btnGuardarCambios,
                "Guardar los cambios realizados a la venta seleccionada.");

            toolTip1.SetToolTip(btnEditar,
                "Editar la venta seleccionada.");

            toolTip1.SetToolTip(btnEliminar,
                "Eliminar la venta seleccionada.");

            // Historial de ventas
            toolTip1.SetToolTip(dgvVentas,
                "Muestra las ventas registradas. Haz doble clic en una venta para editarla.");
        }
        //-------------------------------------------------------------------------------------

        private void frmVentas_Load(object sender, EventArgs e)
        {
            //CONFIGURAR TOOLTIPS
            ConfigurarTooltips();

            // Mostrar las ventas existentes
            MostrarVentas();

            //El cliente solo se mostrará en el textBox , no se podrá editar
            txtMostrarCliente.Enabled = false;

            dgvDetalleDeVenta.AutoGenerateColumns = false;

            dgvDetalleDeVenta.Columns["Eliminar"].DisplayIndex =
                dgvDetalleDeVenta.Columns.Count - 1;


            // Cargar metodos de pago
            CargarComboBoxMetodosDePago();

            //Validacion de fecha
            dtFechaVenta.MaxDate = DateTime.Today;

            //Navegar con la tecla Tab
            btnBuscarCliente.TabIndex = 1;
            dtFechaVenta.TabIndex = 2;
            cbMetodoPago.TabIndex = 3;
            btnAgregarProductos.TabIndex = 4;
            txtSubTotal.TabIndex = 5;
            txtTotalPagar.TabIndex = 6;
            btnGuardar.TabIndex = 7;
            btnEditar.TabIndex = 8;
            btnEliminar.TabIndex = 9;

            dgvVentas.Columns["IdVenta"].HeaderText = "N° de Venta";


            dgvDetalleDeVenta.Columns["IdDetalleVenta"].Visible = false;

            dgvDetalleDeVenta.Columns["IdVenta"].Visible = false;

            dgvDetalleDeVenta.Columns["ProductoVendido"].HeaderText = "Producto Vendido";
            dgvDetalleDeVenta.Columns["PrecioUnitario"].HeaderText = "Precio unitario";

            // Aplicar diseño de las tablas
            ConfigurarTablasVentas();
        }

        int idVentaSeleccionada = 0;
        int metodoPagoOriginal = 0;
        DateTime fechaOriginal;
        decimal subtotalOriginal = 0;

        private void dgvVentas_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                // Evitar encabezados o filas nuevas
                if (e.RowIndex < 0 || dgvVentas.Rows[e.RowIndex].IsNewRow)
                    return;

                DataGridViewRow fila = dgvVentas.Rows[e.RowIndex];

                // Obtener ID de la venta
                idVentaSeleccionada = Convert.ToInt32(
                    fila.Cells["IdVenta"].Value);

                // Obtener datos de la venta
                DateTime fechaVenta = Convert.ToDateTime(fila.Cells["Fecha de Venta"].Value);

                string cliente = fila.Cells["Cliente"].Value?.ToString() ?? "";

                string metodoPago = fila.Cells["Metodo de Pago"].Value?.ToString() ?? "";

                // Mostrar datos en el formulario
                dtFechaVenta.Value = fechaVenta;

                txtMostrarCliente.Text = cliente;

                cbMetodoPago.Text = metodoPago;

                // Guardar valores originales
                metodoPagoOriginal = Convert.ToInt32(cbMetodoPago.SelectedValue);

                fechaOriginal = dtFechaVenta.Value;

                // CARGAR SUBTOTAL DE LA VENTA
                txtSubTotal.Text =
                    fila.Cells["SubTotal"]?.Value?.ToString() ?? "0";

                // Guardar subtotal original
                subtotalOriginal = Convert.ToDecimal(
                    txtSubTotal.Text,
                    CultureInfo.InvariantCulture);


                // Cargar los productos de la venta seleccionada
                dgvDetalleDeVenta.DataSource =
                    DetalleVenta.CargarDetalleVenta(idVentaSeleccionada);
                CalcularSubtotalVenta();

                //Mantiene el diseño de las tablas
                ConfigurarTablasVentas();

                // Configurar columnas del detalle
                dgvDetalleDeVenta.Columns["IdDetalleVenta"].Visible = false;
                dgvDetalleDeVenta.Columns["IdVenta"].Visible = false;

                dgvDetalleDeVenta.Columns["ProductoVendido"].HeaderText =
                    "Producto Vendido";

                dgvDetalleDeVenta.Columns["PrecioUnitario"].HeaderText =
                    "Precio unitario";



                // Cambiar a modo edición
                btnEditar.Visible = true;
                btnGuardar.Visible = false;
                btnGuardarCambios.Visible = true;

                dgvVentas.Columns["IdVenta"].HeaderText = "N° de Venta";

                MessageBox.Show(
                    "Venta seleccionada para editar.\n\n" +
                    "Puede modificar la fecha y el método de pago.\n" +
                    "También puede agregar o eliminar productos.",
                    "Editar venta",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Ocurrió un error al seleccionar la venta:\n" +
                    ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void btnGuardarCambios_Click(object sender, EventArgs e)
        {
            try
            {
                // Validar venta
                if (idVentaSeleccionada == 0)
                {
                    MessageBox.Show(
                        "Seleccione una venta.",
                        "Aviso",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);

                    return;
                }

                // Validar método de pago
                if (cbMetodoPago.SelectedIndex == -1)
                {
                    MessageBox.Show(
                        "Seleccione un método de pago.",
                        "Aviso",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);

                    return;
                }

                // Obtener método de pago
                int idMetodoPago =
                    Convert.ToInt32(cbMetodoPago.SelectedValue);

                // Recalcular subtotal desde los productos
                CalcularSubtotalVenta();

                decimal subtotal = Convert.ToDecimal(
                    txtSubTotal.Text,
                    CultureInfo.InvariantCulture);

                // Verificar si hubo cambios
                if (idMetodoPago == metodoPagoOriginal && dtFechaVenta.Value.Date == fechaOriginal.Date && subtotal == subtotalOriginal)
                {
                    MessageBox.Show(
                        "No se realizaron cambios en la venta.",
                        "Sin cambios",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    return;
                }

                // Crear objeto venta
                DbVentas venta = new DbVentas(
                    idVentaSeleccionada,
                    dtFechaVenta.Value,
                    idClienteSeleccionado,
                    idMetodoPago,
                    subtotal
                );

                // Actualizar venta
                if (venta.ActualizarVenta())
                {
                    MessageBox.Show(
                        "Venta actualizada correctamente.",
                        "Éxito",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    // Recargar tablas
                    MostrarVentas();

                    dgvDetalleDeVenta.DataSource =
                        DetalleVenta.CargarDetalleVenta(idVentaSeleccionada);

                    // Recalcular totales
                    CalcularSubtotalVenta();

                    // Salir de edición
                    btnGuardarCambios.Visible = false;
                    btnGuardar.Visible = true;

                    idVentaSeleccionada = 0;
                }
                else
                {
                    MessageBox.Show(
                        "Error al actualizar la venta.",
                        "Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Error:\n" + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }


        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (idVentaSeleccionada == 0)
            {
                MessageBox.Show("Seleccione una venta para editar.");
                return;
            }

        }

        private void CalcularTotal()
        {
            decimal subtotal = 0;

            decimal.TryParse(
                txtSubTotal.Text,
                NumberStyles.Any,
                CultureInfo.InvariantCulture,
                out subtotal);

            decimal iva = subtotal * 0.13m;

            decimal total = subtotal + iva;

            txtIVA.Text = iva.ToString("0.00", CultureInfo.InvariantCulture);
            txtTotalPagar.Text = total.ToString("0.00", CultureInfo.InvariantCulture);
        }


        private void btnGuardar_Click(object sender, EventArgs e)
        {


            // VALIDAR CLIENTE

            if (txtMostrarCliente.Text == "")
            {
                MessageBox.Show(
                    "Seleccione un cliente.",
                    "Campo requerido",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            // VALIDAR METODO DE PAGO

            if (cbMetodoPago.SelectedIndex == -1)
            {
                MessageBox.Show("Seleccione un método de pago.",
                    "Campo requerido",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }


            // VALIDAR QUE  SI HAYA PRODUCTOS

            if (detallesVenta.Count == 0)
            {
                MessageBox.Show(
                    "Agregue al menos un producto a la venta.",
                    "Venta sin productos",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }



            // OBTENER DATOS DE LA VENTA

            DateTime fechaVenta = dtFechaVenta.Value;

            int cliente = idClienteSeleccionado;
            int metodoPago = Convert.ToInt32(cbMetodoPago.SelectedValue);


            // OBTENER SUBTOTAL

            decimal subtotal;

            if (!decimal.TryParse(txtSubTotal.Text, NumberStyles.Any, CultureInfo.InvariantCulture, out subtotal))
            {
                MessageBox.Show("El subtotal debe ser un valor numérico.", "Dato incorrecto", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                return;
            }

            // CREAR OBJETO VENTA

            DbVentas venta = new DbVentas(0, fechaVenta, cliente, metodoPago, subtotal);

            // GUARDAR VENTA

            int idVenta = venta.InsertarVenta();

            // Si devuelve 0 es porque ocurri un error
            if (idVenta <= 0)
            {
                MessageBox.Show("No se pudo guardar la venta.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }

            // GUARDAR DETALLES DE PRODUCTOS
            foreach (DetalleVenta detalle in detallesVenta)
            {
                // Asignar el IdVenta generado
                detalle.IdVenta1 = idVenta;

                // Guardar detalle
                if (!detalle.InsertarDetalleVenta())
                {
                    MessageBox.Show("La venta fue creada, pero ocurri un error " + "al guardar uno de los detalles.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    return;
                }
            }

            //  VALIDAR QUE TODO SE GUARDA CORRECTAMENTE

            MessageBox.Show("Venta registrada correctamente.", "Registro exitoso", MessageBoxButtons.OK, MessageBoxIcon.Information);

            // LIMPIAR FORMULARIO

            detallesVenta.Clear();

            txtMostrarCliente.Clear();
            cbMetodoPago.SelectedIndex = -1;

            txtSubTotal.Clear();
            txtIVA.Clear();
            txtTotalPagar.Clear();

            dgvVentas.DataSource = null;

            // Actualizar el listado de ventas

            MostrarVentas();

        }

        private void txtIVA_TextChanged(object sender, EventArgs e)
        {
            CalcularTotal();
        }

        private void txtSubTotal_TextChanged(object sender, EventArgs e)
        {
            CalcularTotal();
        }



        private void CargarComboBoxMetodosDePago()
        {
            DataTable dtMetodoPago = MetodoPago.CargarMetodosDePago();

            cbMetodoPago.DataSource = dtMetodoPago;
            cbMetodoPago.DisplayMember = "MetodoPago";
            cbMetodoPago.ValueMember = "IdMetodoPago";
            cbMetodoPago.SelectedIndex = -1;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btnAgregarProductos_Click(object sender, EventArgs e)
        {

            // ABRIR FORMULARIO DE DETALLE DE VENTA

            using (FrmDetalleVenta formulario = new FrmDetalleVenta())
            {
                // Abrir FrmDetalleVenta y esperar
                // a que el usuario agregue el producto
                if (formulario.ShowDialog() == DialogResult.OK)
                {
                    // RECIBIR EL PRODUCTO

                    // Obtener el detalle que se guardo en el 
                    // FrmDetalleVenta
                    DetalleVenta detalle = formulario.DetalleSeleccionado;


                    // AGREGAR EL PRODUCTO A LA LISTA

                    // Guardamos temporalmente el producto
                    // en la lista de detalles
                    detallesVenta.Add(detalle);

                    // MOSTRAR LOS PRODUCTOS

                    // Actualizar el DataGridView
                    MostrarDetalles();
                }
            }
        }


        private void MostrarDetalles()
        {
            // MOSTRAR LISTA DE DETALLES

            // Quitar temporalmente el origen de datos
            dgvDetalleDeVenta.DataSource = null;


            // Asignar nuevamente la lista actualizada
            dgvDetalleDeVenta.DataSource = detallesVenta;


            // Aplicar diseño
            ConfigurarTablasVentas();

            // CALCULAR SUBTOTAL

            CalcularSubtotalVenta();


        }


        private void CalcularSubtotalVenta()
        {
            // CALCULAR SUBTOTAL

            decimal subtotal = 0;

            // Recorrer todos los productos agregados
            foreach (DetalleVenta detalle in detallesVenta)
            {
                // Calcular subtotal del producto
                decimal subtotalProducto =
                    detalle.Cantidad1 *
                    detalle.PrecioUnitario1;

                // Sumarlo al subtotal general
                subtotal += subtotalProducto;
            }

            // Mostrar subtotal
            txtSubTotal.Text =
                subtotal.ToString("0.00");

            // Calcular IVA y total
            CalcularTotal();
        }


        private void dgvVentas_SelectionChanged(object sender, EventArgs e)
        {

            if (dgvVentas.CurrentRow == null)
                return;

            int idVenta = Convert.ToInt32(dgvVentas.CurrentRow.Cells["IdVenta"].Value);

            dgvDetalleDeVenta.DataSource = DetalleVenta.CargarDetalleVenta(idVenta);
        }


        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dgvVentas.CurrentRow == null)
            {
                MessageBox.Show("Seleccione una venta de la tabla.");
                return;
            }

            int id = Convert.ToInt32(dgvVentas.CurrentRow.Cells["IdVenta"].Value);

            DialogResult res = MessageBox.Show("Est seguro de eliminar esta venta? Se eliminarn todos los detalles y facturas asociados de forma permanente.", "Confirmar Eliminacin (Cascada)", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (res == DialogResult.Yes)
            {
                Modelo.Entidades.DbVentas venta = new Modelo.Entidades.DbVentas();
                venta.IdVenta1 = id;
                if (venta.EliminarVenta())
                {
                    MessageBox.Show("Venta eliminada correctamente.");
                    MostrarVentas();
                }
                else
                {
                    MessageBox.Show("Error al eliminar la venta.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
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


        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtBuscar.Text == "Buscar venta...")
                    return;

                string texto = txtBuscar.Text.Trim();

                // Si la búsqueda está vacía,
                // mostrar nuevamente todas las ventas
                if (string.IsNullOrWhiteSpace(texto))
                {
                    MostrarVentas();
                    return;
                }

                // Buscar las ventas
                dtVentas = DbVentas.BuscarVenta(texto);

                // Volver a la primera página
                paginaActual = 1;

                // Calcular páginas
                CalcularPaginasVentas();

                // Mostrar resultados paginados
                MostrarPaginaVentas();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Error al buscar la venta:\n" + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }




        private void dgvDetalleDeVenta_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0)
                {
                    int idDetalleVenta = Convert.ToInt32(
                        dgvDetalleDeVenta.Rows[e.RowIndex]
                        .Cells["IdDetalleVenta"].Value
                    );

                    FrmDetalleVenta frm = new FrmDetalleVenta(idDetalleVenta);

                    frm.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Error: " + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }

        }

        private int idClienteSeleccionado = 0;

        private void btnBuscarCliente_Click(object sender, EventArgs e)
        {
            using (frmBuscarCliente modal = new frmBuscarCliente())
            {
                if (modal.ShowDialog() == DialogResult.OK)
                {
                    idClienteSeleccionado = modal.IdClienteSeleccionado;

                    txtMostrarCliente.Text = modal.NombreClienteSeleccionado;

                }
            }

            if (idClienteSeleccionado == 0)
            {
                MessageBox.Show("Selecciona un cliente.");
                return;
            }
        }

        private void pnlInfo_Paint(object sender, PaintEventArgs e)
        {

        }

        private void dgvDetalleDeVenta_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && dgvDetalleDeVenta.Columns[e.ColumnIndex].Name == "Eliminar")
            {
                DialogResult resultado = MessageBox.Show(
                    "¿Desea eliminar este producto de la venta?",
                    "Confirmar eliminación",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question
                );

                if (resultado == DialogResult.Yes)
                {
                    int idDetalle = Convert.ToInt32(dgvDetalleDeVenta.Rows[e.RowIndex].Cells["IdDetalleVenta"].Value
                    );

                    DetalleVenta.EliminarDetalleVenta(idDetalle);

                    MostrarDetalles();

                }
            }
        }

    }
}
