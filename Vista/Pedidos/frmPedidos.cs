using Modelo.Entidades;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Vista.Responsive;

namespace Vista.Pedidos
{
    public partial class frmPedidos : Form
    {
        public frmPedidos()
        {
            InitializeComponent();
            ResponsiveHelper.Apply(this);
            dgvDetallesDePedido.AllowUserToDeleteRows = false;
            dgvDetallesDePedido.AllowUserToAddRows = false;
            dgvDetallesDePedido.ReadOnly = true;
            dgvDetallesDePedido.Columns.Add(new DataGridViewButtonColumn
            {
                Name = "EliminarProducto",
                HeaderText = "Eliminar",
                Text = "Eliminar",
                UseColumnTextForButtonValue = true
            });
            dgvDetallesDePedido.CellContentClick += EliminarProducto_Click;
        }
        // PAGINACIÓN
        private DataTable dtPedidos;
        private int paginaActual = 1;
        private int registrosPorPagina = 10;
        private bool buscandoPedidos = false;
        private int totalPaginas = 0;
        string medidaLargo = "0";
        string medidaAncho = "0";
        string medidaAlto = "0";
        string observaciones = "";

        int idPedidoSeleccionado = 0;
        private string estadoOriginal = "";
        private DateTime fechaEntregaOriginal;
        //------------------------------------------------------------------------
        // CONFIGURAR TABLAS DE PEDIDOS
        private void ConfigurarTablas()
        {
            // TABLA DE PEDIDOS REGISTRADOS

            dgvPedidosRegistrados.EnableHeadersVisualStyles = false;

            // Encabezado
            dgvPedidosRegistrados.ColumnHeadersDefaultCellStyle.BackColor =
                Color.FromArgb(121, 75, 45);

            dgvPedidosRegistrados.ColumnHeadersDefaultCellStyle.ForeColor =
                Color.White;

            dgvPedidosRegistrados.ColumnHeadersDefaultCellStyle.Font =
                new Font("Times New Roman", 9, FontStyle.Regular);

            dgvPedidosRegistrados.ColumnHeadersDefaultCellStyle.Alignment =
                DataGridViewContentAlignment.MiddleCenter;

            // Filas
            dgvPedidosRegistrados.DefaultCellStyle.BackColor =
                Color.FromArgb(255, 255, 255);

            dgvPedidosRegistrados.DefaultCellStyle.ForeColor =
                Color.FromArgb(45, 45, 45);

            dgvPedidosRegistrados.DefaultCellStyle.Font =
                new Font("Segoe UI", 9, FontStyle.Regular);

            dgvPedidosRegistrados.DefaultCellStyle.Alignment =
                DataGridViewContentAlignment.MiddleLeft;

            // Filas alternadas
            dgvPedidosRegistrados.AlternatingRowsDefaultCellStyle.BackColor =
                Color.FromArgb(248, 241, 232);

            // Selección
            dgvPedidosRegistrados.DefaultCellStyle.SelectionBackColor =
                Color.FromArgb(224, 193, 157);

            dgvPedidosRegistrados.DefaultCellStyle.SelectionForeColor =
                Color.Black;

            // Bordes
            dgvPedidosRegistrados.CellBorderStyle =
                DataGridViewCellBorderStyle.SingleHorizontal;

            dgvPedidosRegistrados.GridColor =
                Color.FromArgb(220, 220, 220);

            // Alto de filas
            dgvPedidosRegistrados.RowTemplate.Height = 32;

            // Encabezado
            dgvPedidosRegistrados.ColumnHeadersHeight = 30;

            // No permitir modificar la tabla
            dgvPedidosRegistrados.ReadOnly = true;
            dgvPedidosRegistrados.AllowUserToAddRows = false;
            dgvPedidosRegistrados.AllowUserToDeleteRows = false;

            // Seleccionar una fila completa
            dgvPedidosRegistrados.SelectionMode =
                DataGridViewSelectionMode.FullRowSelect;

            dgvPedidosRegistrados.MultiSelect = false;

            // Quitar borde exterior
            dgvPedidosRegistrados.BorderStyle =
                BorderStyle.None;

            // Ajustar columnas
            if (dgvPedidosRegistrados.Columns.Contains("IdPedido"))
            {
                dgvPedidosRegistrados.Columns["IdPedido"].Visible = true;
                dgvPedidosRegistrados.Columns["IdPedido"].Width = 100;
                dgvPedidosRegistrados.Columns["IdPedido"].HeaderText = "N.º de Pedido";
                dgvPedidosRegistrados.Columns["IdPedido"].DefaultCellStyle.Alignment =
                    DataGridViewContentAlignment.MiddleCenter;
            }

            if (dgvPedidosRegistrados.Columns.Contains("Cliente"))
            {
                dgvPedidosRegistrados.Columns["Cliente"].AutoSizeMode =
                    DataGridViewAutoSizeColumnMode.Fill;
            }

            if (dgvPedidosRegistrados.Columns.Contains("FechaDePedido"))
            {
                dgvPedidosRegistrados.Columns["FechaDePedido"].Width = 120;
                dgvPedidosRegistrados.Columns["FechaDePedido"].HeaderText =
                    "Fecha del Pedido";

                dgvPedidosRegistrados.Columns["FechaDePedido"].DefaultCellStyle.Alignment =
                    DataGridViewContentAlignment.MiddleCenter;
            }

            if (dgvPedidosRegistrados.Columns.Contains("FechaDeEntrega"))
            {
                dgvPedidosRegistrados.Columns["FechaDeEntrega"].Width = 120;
                dgvPedidosRegistrados.Columns["FechaDeEntrega"].HeaderText =
                    "Fecha de Entrega";

                dgvPedidosRegistrados.Columns["FechaDeEntrega"].DefaultCellStyle.Alignment =
                    DataGridViewContentAlignment.MiddleCenter;
            }

            if (dgvPedidosRegistrados.Columns.Contains("Estado"))
            {
                dgvPedidosRegistrados.Columns["Estado"].Width = 100;

                dgvPedidosRegistrados.Columns["Estado"].DefaultCellStyle.Alignment =
                    DataGridViewContentAlignment.MiddleCenter;
            }



            // TABLA DE DETALLES DEL PEDIDO


            dgvDetallesDePedido.EnableHeadersVisualStyles = false;

            // Encabezado
            dgvDetallesDePedido.ColumnHeadersDefaultCellStyle.BackColor =
                Color.FromArgb(121, 75, 45);

            dgvDetallesDePedido.ColumnHeadersDefaultCellStyle.ForeColor =
                Color.White;

            dgvDetallesDePedido.ColumnHeadersDefaultCellStyle.Font =
                new Font("Times New Roman", 9, FontStyle.Regular);

            dgvDetallesDePedido.ColumnHeadersDefaultCellStyle.Alignment =
                DataGridViewContentAlignment.MiddleCenter;

            dgvPedidosRegistrados.ColumnHeadersDefaultCellStyle.SelectionBackColor =
    Color.FromArgb(121, 75, 45);

            dgvPedidosRegistrados.ColumnHeadersDefaultCellStyle.SelectionForeColor =
                Color.White;

            // Filas
            dgvDetallesDePedido.DefaultCellStyle.BackColor =
                Color.White;

            dgvDetallesDePedido.DefaultCellStyle.ForeColor =
                Color.FromArgb(45, 45, 45);

            dgvDetallesDePedido.DefaultCellStyle.Font =
                new Font("Times New Roman", 9, FontStyle.Regular);

            dgvDetallesDePedido.DefaultCellStyle.Alignment =
                DataGridViewContentAlignment.MiddleLeft;

            // Filas alternadas
            dgvDetallesDePedido.AlternatingRowsDefaultCellStyle.BackColor =
                Color.FromArgb(248, 241, 232);

            // Selección
            dgvDetallesDePedido.DefaultCellStyle.SelectionBackColor =
                Color.FromArgb(224, 193, 157);

            dgvDetallesDePedido.DefaultCellStyle.SelectionForeColor =
                Color.Black;

            // Bordes
            dgvDetallesDePedido.CellBorderStyle =
                DataGridViewCellBorderStyle.SingleHorizontal;

            dgvDetallesDePedido.GridColor =
                Color.FromArgb(220, 220, 220);

            // Alto de filas
            dgvDetallesDePedido.RowTemplate.Height = 32;

            // Encabezado
            dgvDetallesDePedido.ColumnHeadersHeight = 30;

            // No permitir modificar
            dgvDetallesDePedido.ReadOnly = true;
            dgvDetallesDePedido.AllowUserToAddRows = false;
            dgvDetallesDePedido.AllowUserToDeleteRows = false;

            // Seleccionar fila completa
            dgvDetallesDePedido.SelectionMode =
                DataGridViewSelectionMode.FullRowSelect;

            dgvDetallesDePedido.MultiSelect = false;

            // Quitar borde exterior
            dgvDetallesDePedido.BorderStyle =
                BorderStyle.None;

            // COLUMNAS DE DETALLES

            if (dgvDetallesDePedido.Columns.Contains("IdDetallePedido"))
            {
                dgvDetallesDePedido.Columns["IdDetallePedido"].Visible = false;
            }

            if (dgvDetallesDePedido.Columns.Contains("IdPedido"))
            {
                dgvDetallesDePedido.Columns["IdPedido"].Visible = false;
            }

            if (dgvDetallesDePedido.Columns.Contains("Mueble"))
            {
                dgvDetallesDePedido.Columns["Mueble"].HeaderText = "Mueble";

                dgvDetallesDePedido.Columns["Mueble"].AutoSizeMode =
                    DataGridViewAutoSizeColumnMode.Fill;
            }

            if (dgvDetallesDePedido.Columns.Contains("Cantidad"))
            {
                dgvDetallesDePedido.Columns["Cantidad"].HeaderText = "Cantidad";
                dgvDetallesDePedido.Columns["Cantidad"].Width = 90;

                dgvDetallesDePedido.Columns["Cantidad"].DefaultCellStyle.Alignment =
                    DataGridViewContentAlignment.MiddleCenter;
            }

            if (dgvDetallesDePedido.Columns.Contains("Medidas"))
            {
                dgvDetallesDePedido.Columns["Medidas"].HeaderText = "Medidas";
                dgvDetallesDePedido.Columns["Medidas"].Width = 150;

                dgvDetallesDePedido.Columns["Medidas"].DefaultCellStyle.Alignment =
                    DataGridViewContentAlignment.MiddleCenter;
            }



            // BOTÓN ELIMINAR


            if (dgvDetallesDePedido.Columns.Contains("EliminarProducto"))
            {
                DataGridViewButtonColumn botonEliminar =
                    dgvDetallesDePedido.Columns["EliminarProducto"]
                    as DataGridViewButtonColumn;

                if (botonEliminar != null)
                {
                    botonEliminar.HeaderText = "Eliminar";
                    botonEliminar.Text = "Eliminar";
                    botonEliminar.UseColumnTextForButtonValue = true;
                    botonEliminar.Width = 90;

                    botonEliminar.DefaultCellStyle.BackColor =
                        Color.FromArgb(121, 75, 45);

                    botonEliminar.DefaultCellStyle.ForeColor =
                        Color.White;

                    botonEliminar.DefaultCellStyle.SelectionBackColor =
                        Color.FromArgb(94, 56, 33);

                    botonEliminar.DefaultCellStyle.SelectionForeColor =
                        Color.White;

                    botonEliminar.DefaultCellStyle.Alignment =
                        DataGridViewContentAlignment.MiddleCenter;
                    dgvDetallesDePedido.ColumnHeadersDefaultCellStyle.SelectionBackColor =
    Color.FromArgb(121, 75, 45);

                    dgvDetallesDePedido.ColumnHeadersDefaultCellStyle.SelectionForeColor =
                        Color.White;
                }
            }
        }
        private void ConfigurarTooltips()
        {
            ToolTip toolTip = new ToolTip();

            // Datos del pedido
            toolTip.SetToolTip(dtpFechaPedido, "Seleccione la fecha en que se realizó el pedido.");
            toolTip.SetToolTip(dtpFechaDeEntrega, "Seleccione la fecha en que se entregará el pedido.");
            toolTip.SetToolTip(cbEstado, "Seleccione el estado actual del pedido.");

            // Detalles del producto
            toolTip.SetToolTip(txtMuebleaRealizar, "Mueble que desea agregar al pedido o modificar del pedido.");
            toolTip.SetToolTip(nudCantidad, "Indique la cantidad de muebles.");
            toolTip.SetToolTip(btnDetallePedido, "Ingrese las medidas del producto.");
            toolTip.SetToolTip(btnAgregar, "Agrega el producto a los detalles del pedido.");

            // Botones principales
            toolTip.SetToolTip(btnGuardar, "Guarda el pedido.");
            toolTip.SetToolTip(btnCamcelar, "Cancela el registro del pedido.");


            // Búsqueda
            toolTip.SetToolTip(txtBuscar, "Ingrese el número o información del pedido que desea buscar.");

            // Tablas
            toolTip.SetToolTip(dgvPedidosRegistrados, "Aquí se muestran los productos agregados al pedido.");
            toolTip.SetToolTip(dgvDetallesDePedido, "Aquí se muestran los pedidos registrados.");
        }

        private void EliminarProducto_Click(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0 ||
                dgvDetallesDePedido.Columns[e.ColumnIndex].Name != "EliminarProducto")
                return;
            var fila = dgvDetallesDePedido.Rows[e.RowIndex];
            if (fila.IsNewRow) return;
            int pedido = Convert.ToInt32(fila.Cells["IdPedido"].Value);
            int detalle = Convert.ToInt32(fila.Cells["IdDetallePedido"].Value);
            try
            {
                bool ultimo = DetallePedidos.CargarDetallesPorPedido(pedido).Rows.Count == 1;
                string aviso = ultimo
                    ? "Al eliminar el último producto, el pedido quedará marcado como Cancelado. El registro del pedido se conservará. ¿Desea continuar?"
                    : "¿Desea eliminar este producto del pedido?";
                if (MessageBox.Show(aviso, "Confirmar eliminación", MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) != DialogResult.Yes)
                    return;
                bool cancelado = DetallePedidos.EliminarDetalle(pedido, detalle, ultimo);
                dgvDetallesDePedido.DataSource = DetallePedidos.CargarDetallesPorPedido(pedido);
                ConfigurarColumnasDetalles();
                MostrarPedidos();
                if (cancelado && idPedidoSeleccionado == pedido)
                {
                    cbEstado.Text = "Cancelado";
                    estadoOriginal = "Cancelado";
                }
                MessageBox.Show(cancelado ? "Producto eliminado. El pedido quedó Cancelado."
                    : "Producto eliminado correctamente.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se pudo eliminar el producto: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }




        private void txtBuscar_Enter(object sender, EventArgs e)
        {
            if (txtBuscar.Text == "Buscar Pedido...")
            {
                txtBuscar.Text = "";
                txtBuscar.ForeColor = Color.Black;
            }
        }

        private void txtBuscar_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtBuscar.Text))
            {
                txtBuscar.Text = "Buscar pedido...";
                txtBuscar.ForeColor = Color.Gray;
            }
        }
        private void CargarPaginacionPedidos()
        {
            try
            {
                // Cargar todos los pedidos
                dtPedidos = DbPedidos.CargarRegistroPedidos();

                // Calcular el total de páginas
                totalPaginas = (int)Math.Ceiling(
                    (double)dtPedidos.Rows.Count / registrosPorPagina
                );

                // Si no existen pedidos
                if (totalPaginas == 0)
                {
                    totalPaginas = 1;
                }

                // Evitar que la página actual sea mayor al total
                if (paginaActual > totalPaginas)
                {
                    paginaActual = totalPaginas;
                }

                MostrarPaginaPedidos();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Error al cargar los pedidos: " + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        private void MostrarPaginaPedidos()
        {
            if (dtPedidos == null)
                return;

            DataTable dtPagina = dtPedidos.Clone();

            int inicio = (paginaActual - 1) * registrosPorPagina;

            int fin = Math.Min(
                inicio + registrosPorPagina,
                dtPedidos.Rows.Count
            );

            for (int i = inicio; i < fin; i++)
            {
                dtPagina.ImportRow(dtPedidos.Rows[i]);
            }

            dgvPedidosRegistrados.DataSource = dtPagina;

            // Encabezados
            if (dgvPedidosRegistrados.Columns.Contains("IdPedido"))
                dgvPedidosRegistrados.Columns["IdPedido"].HeaderText = "N.º de Pedido";

            if (dgvPedidosRegistrados.Columns.Contains("Cliente"))
                dgvPedidosRegistrados.Columns["Cliente"].HeaderText = "Cliente";

            if (dgvPedidosRegistrados.Columns.Contains("FechaDePedido"))
                dgvPedidosRegistrados.Columns["FechaDePedido"].HeaderText = "Fecha del Pedido";

            if (dgvPedidosRegistrados.Columns.Contains("FechaDeEntrega"))
                dgvPedidosRegistrados.Columns["FechaDeEntrega"].HeaderText = "Fecha de Entrega";

            if (dgvPedidosRegistrados.Columns.Contains("Estado"))
                dgvPedidosRegistrados.Columns["Estado"].HeaderText = "Estado";

            // Mostrar página actual
            lblPagina.Text = $"Página {paginaActual} de {totalPaginas}";

            // Habilitar o deshabilitar botones
            btnAnterior.Enabled = paginaActual > 1;
            btnSiguiente.Enabled = paginaActual < totalPaginas;
        }
        private void frmPedidos_Load(object sender, EventArgs e)
        {
            MostrarPedidos();
            MostrarDetallesPedido();

            // Configurar el diseño de las tablas
            ConfigurarTablas();

            ConfigurarTooltips();

            cbEstado.Items.Add("En proceso");
            cbEstado.Items.Add("Finalizado");
            cbEstado.Items.Add("Cancelado");
            cbEstado.SelectedIndex = 0;

            dtpFechaDeEntrega.Value = DateTime.Today;

            txtMuebleaRealizar.MaxLength = 150;

            txtClienteSeleccionado.TabIndex = 1;
            dtpFechaPedido.TabIndex = 2;
            dtpFechaDeEntrega.TabIndex = 3;
            cbEstado.TabIndex = 4;
            txtMuebleaRealizar.TabIndex = 5;
            nudCantidad.TabIndex = 6;
            btnDetallePedido.TabIndex = 7;
            btnAgregar.TabIndex = 8;
            btnGuardar.TabIndex = 9;
            btnCamcelar.TabIndex = 10;

            // Encabezados visibles
            dgvDetallesDePedido.Columns["IdDetallePedido"].Visible = false;
            dgvDetallesDePedido.Columns["IdPedido"].Visible = false;
            dgvDetallesDePedido.Columns["Mueble"].HeaderText = "Mueble";
            dgvDetallesDePedido.Columns["Cantidad"].HeaderText = "Cantidad";
            dgvDetallesDePedido.Columns["Medidas"].HeaderText = "Medidas";

            dgvPedidosRegistrados.Columns["IdPedido"].HeaderText = "N.º de Pedido";
            dgvPedidosRegistrados.Columns["Cliente"].HeaderText = "Cliente";
            dgvPedidosRegistrados.Columns["FechaDePedido"].HeaderText = "Fecha del Pedido";
            dgvPedidosRegistrados.Columns["FechaDeEntrega"].HeaderText = "Fecha de Entrega";
            dgvPedidosRegistrados.Columns["Estado"].HeaderText = "Estado";

        }

        private void MostrarDetallesPedido()
        {
            dgvDetallesDePedido.DataSource = null;
            dgvDetallesDePedido.DataSource = DetallePedidos.CargarDetallesPedidos();

            ConfigurarColumnasDetalles();
        }

        private void MostrarPedidos()
        {
            paginaActual = 1;

            CargarPaginacionPedidos();
        }

        private void btnDeatllePedido_Click(object sender, EventArgs e)
        {
            frmDetallePedido modal = new frmDetallePedido();

            if (modal.ShowDialog() == DialogResult.OK)
            {
                medidaLargo = modal.Largo;
                medidaAncho = modal.Ancho;
                medidaAlto = modal.Alto;
                observaciones = modal.Observaciones;
            }
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMuebleaRealizar.Text))
            {
                MessageBox.Show("Ingresa el nombre del mueble.");
                return;
            }

            if (string.IsNullOrWhiteSpace(nudCantidad.Text))
            {
                MessageBox.Show("Ingrese la cantidad de productos que desea agregar.");
                return;
            }

            if (medidaLargo == "0" && medidaAncho == "0" && medidaAlto == "0")
            {
                MessageBox.Show("Por favor ingresa las medidas del producto dando clic en 'Medidas del producto'.");
                return;
            }

            if (idPedidoSeleccionado <= 0)
            {
                MessageBox.Show("Seleccione un pedido primero.");
                return;
            }

            string medidas = medidaLargo + "x" + medidaAncho + "x" + medidaAlto;

            if (DetallePedidos.InsertarDetalle(
                idPedidoSeleccionado,
                txtMuebleaRealizar.Text,
                Convert.ToInt32(nudCantidad.Value),
                medidas))
            {
                MessageBox.Show("Detalle agregado correctamente.");
                dgvDetallesDePedido.DataSource = null;
                dgvDetallesDePedido.DataSource =
                    DetallePedidos.CargarDetallesPorPedido(idPedidoSeleccionado);
                ConfigurarColumnasDetalles();
            }
            else
            {
                MessageBox.Show("Error al agregar el detalle.");
            }

            txtMuebleaRealizar.Clear();
            nudCantidad.Value = 0;
            medidaLargo = "0";
            medidaAncho = "0";
            medidaAlto = "0";
            observaciones = "";
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (dtpFechaDeEntrega.Value.Date < dtpFechaPedido.Value.Date)
            {
                MessageBox.Show("La fecha de entrega no puede ser anterior a la fecha del pedido.");
                return;
            }

            if (idPedidoSeleccionado <= 0)
            {
                MessageBox.Show("Para actualizar el estado o la fecha, selecciona un pedido de la lista y presiona Guardar.");
                return;
            }

            string nuevoEstado = cbEstado.Text;
            DateTime nuevaFechaEntrega = dtpFechaDeEntrega.Value;

            bool estadoCambio = estadoOriginal != nuevoEstado;
            bool fechaCambio = fechaEntregaOriginal != nuevaFechaEntrega;

            if (!estadoCambio && !fechaCambio)
            {
                MessageBox.Show(
                    "No se realizaron cambios en el pedido.",
                    "Sin cambios",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );

                return;
            }

            if (estadoCambio)
            {
                if (DbPedidos.ActualizarPedidoEstado(idPedidoSeleccionado, nuevoEstado))
                {
                    MessageBox.Show(
                        "El estado del pedido se modificó correctamente a: " + nuevoEstado,
                        "Estado actualizado",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );
                }
                else
                {
                    MessageBox.Show(
                        "Error al actualizar el estado del pedido.",
                        "Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error
                    );
                }
            }

            if (fechaCambio)
            {
                if (DbPedidos.ActualizarPedidoFecha(idPedidoSeleccionado, nuevaFechaEntrega))
                {
                    MessageBox.Show(
                        "La fecha de entrega del pedido se modificó correctamente a: " +
                        nuevaFechaEntrega.ToString("dd/MM/yyyy"),
                        "Fecha actualizada",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );
                }
                else
                {
                    MessageBox.Show(
                        "Error al actualizar la fecha del pedido.",
                        "Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error
                    );
                }
            }

            estadoOriginal = nuevoEstado;
            fechaEntregaOriginal = nuevaFechaEntrega;

            MostrarPedidos();
        }

        private void btnCamcelar_Click(object sender, EventArgs e)
        {
            ((DataTable)dgvDetallesDePedido.DataSource)?.RejectChanges();

            txtClienteSeleccionado.Text = "Seleccionar Cliente";
            txtMuebleaRealizar.Clear();
            nudCantidad.Value = 0;
        }

        private void dgvPedidosRegistrados_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                // Verificar que sea una fila válida
                if (e.RowIndex < 0)
                    return;

                DataGridViewRow fila =
                    dgvPedidosRegistrados.Rows[e.RowIndex];

                if (fila.IsNewRow)
                    return;

                // Obtener el ID del pedido seleccionado
                if (fila.Cells["IdPedido"].Value == null ||
                    fila.Cells["IdPedido"].Value == DBNull.Value)
                {
                    return;
                }

                idPedidoSeleccionado =
                    Convert.ToInt32(fila.Cells["IdPedido"].Value);

                // CARGAR ESTADO

                string estado =
                    fila.Cells["Estado"].Value?.ToString() ?? "";

                cbEstado.Text = estado;

                estadoOriginal = estado;

                // CARGAR FECHA DEL PEDIDO              

                if (fila.Cells["FechaDePedido"].Value != null &&
                    fila.Cells["FechaDePedido"].Value != DBNull.Value)
                {
                    dtpFechaPedido.Value =
                        Convert.ToDateTime(
                            fila.Cells["FechaDePedido"].Value);
                }


                // CARGAR FECHA DE ENTREGA


                if (fila.Cells["FechaDeEntrega"].Value != null &&
                    fila.Cells["FechaDeEntrega"].Value != DBNull.Value)
                {
                    fechaEntregaOriginal =
                        Convert.ToDateTime(
                            fila.Cells["FechaDeEntrega"].Value);

                    dtpFechaDeEntrega.Value =
                        fechaEntregaOriginal;
                }

                // CARGAR CLIENTE

                if (fila.Cells["Cliente"].Value != null)
                {
                    txtClienteSeleccionado.Text =
                        fila.Cells["Cliente"].Value.ToString();

                    txtClienteSeleccionado.ForeColor =
                        Color.Black;

                    txtClienteSeleccionado.Enabled = false;
                }


                // CARGAR DETALLES DEL PEDIDO

                DataTable detalles =
                    DetallePedidos.CargarDetallesPorPedido(
                        idPedidoSeleccionado);

                dgvDetallesDePedido.DataSource = null;

                dgvDetallesDePedido.DataSource = detalles;

                // CONFIGURAR TABLA DE DETALLES
                ConfigurarColumnasDetalles();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Ocurrió un error al cargar los detalles del pedido.\n" +
                    ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }
        private void ConfigurarColumnasDetalles()
        {
            if (dgvDetallesDePedido.Columns.Contains("IdDetallePedido"))
            {
                dgvDetallesDePedido.Columns["IdDetallePedido"].Visible = false;
            }

            if (dgvDetallesDePedido.Columns.Contains("IdPedido"))
            {
                dgvDetallesDePedido.Columns["IdPedido"].Visible = false;
            }

            if (dgvDetallesDePedido.Columns.Contains("Mueble"))
            {
                dgvDetallesDePedido.Columns["Mueble"].HeaderText = "Mueble";
            }

            if (dgvDetallesDePedido.Columns.Contains("Cantidad"))
            {
                dgvDetallesDePedido.Columns["Cantidad"].HeaderText = "Cantidad";
            }

            if (dgvDetallesDePedido.Columns.Contains("Medidas"))
            {
                dgvDetallesDePedido.Columns["Medidas"].HeaderText = "Medidas";
            }

            if (dgvDetallesDePedido.Columns.Contains("EliminarProducto"))
            {
                dgvDetallesDePedido.Columns["EliminarProducto"].HeaderText =
                    "Eliminar";
            }
        }
        private void txtMuebleaRealizar_KeyPress(
            object sender,
            KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) &&
                !char.IsControl(e.KeyChar) &&
                e.KeyChar != ' ')
            {
                e.Handled = true;
            }
        }

        private void txtBuscar_TextChanged(
            object sender,
            EventArgs e)
        {
            try
            {
                if (txtBuscar.Text == "Buscar pedido...")
                    return;

                string texto = txtBuscar.Text.Trim();

                // Si el buscador está vacío
                if (string.IsNullOrWhiteSpace(texto))
                {
                    buscandoPedidos = false;
                    paginaActual = 1;

                    CargarPaginacionPedidos();

                    return;
                }

                buscandoPedidos = true;

                // Buscar pedidos
                dtPedidos = DbPedidos.BuscarPedido(texto);

                paginaActual = 1;

                // Calcular páginas de los resultados
                totalPaginas = (int)Math.Ceiling(
                    (double)dtPedidos.Rows.Count / registrosPorPagina
                );

                if (totalPaginas == 0)
                {
                    totalPaginas = 1;
                }

                MostrarPaginaPedidos();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
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





        private void btnAnterior_Click(object sender, EventArgs e)
        {
            if (paginaActual > 1)
            {
                paginaActual--;

                MostrarPaginaPedidos();
            }
        }

        private void btnSiguiente_Click(object sender, EventArgs e)
        {
            if (paginaActual < totalPaginas)
            {
                paginaActual++;

                MostrarPaginaPedidos();
            }
        }

        private void dgvPedidosRegistrados_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            DataGridViewRow fila =
                dgvPedidosRegistrados.Rows[e.RowIndex];

            int idPedido =
                Convert.ToInt32(fila.Cells["IdPedido"].Value);

            string cliente =
                fila.Cells["Cliente"].Value?.ToString() ?? "";

            idPedidoSeleccionado = idPedido;

            txtClienteSeleccionado.Text = cliente;
            txtClienteSeleccionado.ForeColor = Color.Black;
            txtClienteSeleccionado.Enabled = false;
        }
    }
}
