namespace Vista.Ventas
{
    partial class frmVentas
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblDetalleVentas = new System.Windows.Forms.Label();
            this.dgvDetalleDeVenta = new System.Windows.Forms.DataGridView();
            this.pbxBuscar = new System.Windows.Forms.PictureBox();
            this.pnlBarraSuperior = new System.Windows.Forms.Panel();
            this.lblAdministrador = new System.Windows.Forms.Label();
            this.pbPerfil = new System.Windows.Forms.PictureBox();
            this.txtBuscar = new System.Windows.Forms.TextBox();
            this.pnlPedidaDeDatos = new System.Windows.Forms.Panel();
            this.btnAgregarProductos = new System.Windows.Forms.Button();
            this.lblAgregarProductos = new System.Windows.Forms.Label();
            this.cbCliente = new System.Windows.Forms.ComboBox();
            this.txtTotalPagar = new System.Windows.Forms.TextBox();
            this.gbBarraDecorativa = new System.Windows.Forms.GroupBox();
            this.lblTotalPagar = new System.Windows.Forms.Label();
            this.txtIVA = new System.Windows.Forms.TextBox();
            this.lblResumenPago = new System.Windows.Forms.Label();
            this.dtFechaVenta = new System.Windows.Forms.DateTimePicker();
            this.cbMetodoPago = new System.Windows.Forms.ComboBox();
            this.txtSubTotal = new System.Windows.Forms.TextBox();
            this.btnEliminar = new System.Windows.Forms.Button();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.btnEditar = new System.Windows.Forms.Button();
            this.lblIva = new System.Windows.Forms.Label();
            this.lblSubTotal = new System.Windows.Forms.Label();
            this.lblMetodoPago = new System.Windows.Forms.Label();
            this.lblFechaVenta = new System.Windows.Forms.Label();
            this.lblCodigoCliente = new System.Windows.Forms.Label();
            this.lblDetalleVenta = new System.Windows.Forms.Label();
            this.lblSubTexto = new System.Windows.Forms.Label();
            this.lblMensajeInformativoPrincipal = new System.Windows.Forms.Label();
            this.pnlTablaContenido = new System.Windows.Forms.Panel();
            this.lblRegistroVentas = new System.Windows.Forms.Label();
            this.dgvVentas = new System.Windows.Forms.DataGridView();
            this.btnGuardarCambios = new System.Windows.Forms.Button();
            this.pnlHeader.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDetalleDeVenta)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbxBuscar)).BeginInit();
            this.pnlBarraSuperior.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbPerfil)).BeginInit();
            this.pnlPedidaDeDatos.SuspendLayout();
            this.pnlTablaContenido.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvVentas)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlHeader
            // 
            this.pnlHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(221)))), ((int)(((byte)(175)))));
            this.pnlHeader.Controls.Add(this.panel1);
            this.pnlHeader.Controls.Add(this.pbxBuscar);
            this.pnlHeader.Controls.Add(this.pnlBarraSuperior);
            this.pnlHeader.Controls.Add(this.txtBuscar);
            this.pnlHeader.Controls.Add(this.pnlPedidaDeDatos);
            this.pnlHeader.Controls.Add(this.lblSubTexto);
            this.pnlHeader.Controls.Add(this.lblMensajeInformativoPrincipal);
            this.pnlHeader.Controls.Add(this.pnlTablaContenido);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlHeader.Margin = new System.Windows.Forms.Padding(2);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Size = new System.Drawing.Size(1102, 627);
            this.pnlHeader.TabIndex = 1;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(238)))), ((int)(((byte)(235)))));
            this.panel1.Controls.Add(this.lblDetalleVentas);
            this.panel1.Controls.Add(this.dgvDetalleDeVenta);
            this.panel1.Location = new System.Drawing.Point(37, 431);
            this.panel1.Margin = new System.Windows.Forms.Padding(2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(766, 179);
            this.panel1.TabIndex = 4;
            // 
            // lblDetalleVentas
            // 
            this.lblDetalleVentas.AutoSize = true;
            this.lblDetalleVentas.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDetalleVentas.Location = new System.Drawing.Point(14, 6);
            this.lblDetalleVentas.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblDetalleVentas.Name = "lblDetalleVentas";
            this.lblDetalleVentas.Size = new System.Drawing.Size(166, 22);
            this.lblDetalleVentas.TabIndex = 28;
            this.lblDetalleVentas.Text = "Detalle de la venta.";
            // 
            // dgvDetalleDeVenta
            // 
            this.dgvDetalleDeVenta.AllowUserToResizeColumns = false;
            this.dgvDetalleDeVenta.AllowUserToResizeRows = false;
            this.dgvDetalleDeVenta.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvDetalleDeVenta.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvDetalleDeVenta.BackgroundColor = System.Drawing.Color.White;
            this.dgvDetalleDeVenta.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.PeachPuff;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvDetalleDeVenta.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dgvDetalleDeVenta.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvDetalleDeVenta.GridColor = System.Drawing.Color.Black;
            this.dgvDetalleDeVenta.Location = new System.Drawing.Point(14, 37);
            this.dgvDetalleDeVenta.Name = "dgvDetalleDeVenta";
            this.dgvDetalleDeVenta.RowHeadersVisible = false;
            this.dgvDetalleDeVenta.Size = new System.Drawing.Size(741, 122);
            this.dgvDetalleDeVenta.TabIndex = 0;
            // 
            // pbxBuscar
            // 
            this.pbxBuscar.BackColor = System.Drawing.Color.Transparent;
            this.pbxBuscar.Image = global::Vista.Properties.Resources.zoom_5611171;
            this.pbxBuscar.Location = new System.Drawing.Point(932, 56);
            this.pbxBuscar.Name = "pbxBuscar";
            this.pbxBuscar.Size = new System.Drawing.Size(26, 26);
            this.pbxBuscar.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbxBuscar.TabIndex = 27;
            this.pbxBuscar.TabStop = false;
            // 
            // pnlBarraSuperior
            // 
            this.pnlBarraSuperior.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(177)))), ((int)(((byte)(114)))));
            this.pnlBarraSuperior.Controls.Add(this.lblAdministrador);
            this.pnlBarraSuperior.Controls.Add(this.pbPerfil);
            this.pnlBarraSuperior.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlBarraSuperior.Location = new System.Drawing.Point(0, 0);
            this.pnlBarraSuperior.Margin = new System.Windows.Forms.Padding(2);
            this.pnlBarraSuperior.Name = "pnlBarraSuperior";
            this.pnlBarraSuperior.Size = new System.Drawing.Size(1102, 23);
            this.pnlBarraSuperior.TabIndex = 6;
            this.pnlBarraSuperior.Paint += new System.Windows.Forms.PaintEventHandler(this.panel6_Paint);
            // 
            // lblAdministrador
            // 
            this.lblAdministrador.AutoSize = true;
            this.lblAdministrador.Font = new System.Drawing.Font("Times New Roman", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAdministrador.Location = new System.Drawing.Point(1013, 4);
            this.lblAdministrador.Name = "lblAdministrador";
            this.lblAdministrador.Size = new System.Drawing.Size(38, 14);
            this.lblAdministrador.TabIndex = 29;
            this.lblAdministrador.Text = "Admin";
            // 
            // pbPerfil
            // 
            this.pbPerfil.Image = global::Vista.Properties.Resources.Imagen_perfil_2;
            this.pbPerfil.Location = new System.Drawing.Point(1054, -1);
            this.pbPerfil.Name = "pbPerfil";
            this.pbPerfil.Size = new System.Drawing.Size(22, 22);
            this.pbPerfil.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbPerfil.TabIndex = 12;
            this.pbPerfil.TabStop = false;
            // 
            // txtBuscar
            // 
            this.txtBuscar.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBuscar.ForeColor = System.Drawing.Color.Gray;
            this.txtBuscar.Location = new System.Drawing.Point(470, 56);
            this.txtBuscar.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtBuscar.Name = "txtBuscar";
            this.txtBuscar.Size = new System.Drawing.Size(488, 26);
            this.txtBuscar.TabIndex = 0;
            this.txtBuscar.Text = "Buscar Venta...";
            this.txtBuscar.TextChanged += new System.EventHandler(this.txtBuscar_TextChanged);
            this.txtBuscar.Enter += new System.EventHandler(this.txtBuscar_Enter);
            this.txtBuscar.Leave += new System.EventHandler(this.txtBuscar_Leave);
            // 
            // pnlPedidaDeDatos
            // 
            this.pnlPedidaDeDatos.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlPedidaDeDatos.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(215)))), ((int)(((byte)(215)))));
            this.pnlPedidaDeDatos.Controls.Add(this.btnGuardarCambios);
            this.pnlPedidaDeDatos.Controls.Add(this.btnAgregarProductos);
            this.pnlPedidaDeDatos.Controls.Add(this.lblAgregarProductos);
            this.pnlPedidaDeDatos.Controls.Add(this.cbCliente);
            this.pnlPedidaDeDatos.Controls.Add(this.txtTotalPagar);
            this.pnlPedidaDeDatos.Controls.Add(this.gbBarraDecorativa);
            this.pnlPedidaDeDatos.Controls.Add(this.lblTotalPagar);
            this.pnlPedidaDeDatos.Controls.Add(this.txtIVA);
            this.pnlPedidaDeDatos.Controls.Add(this.lblResumenPago);
            this.pnlPedidaDeDatos.Controls.Add(this.dtFechaVenta);
            this.pnlPedidaDeDatos.Controls.Add(this.cbMetodoPago);
            this.pnlPedidaDeDatos.Controls.Add(this.txtSubTotal);
            this.pnlPedidaDeDatos.Controls.Add(this.btnEliminar);
            this.pnlPedidaDeDatos.Controls.Add(this.btnGuardar);
            this.pnlPedidaDeDatos.Controls.Add(this.btnEditar);
            this.pnlPedidaDeDatos.Controls.Add(this.lblIva);
            this.pnlPedidaDeDatos.Controls.Add(this.lblSubTotal);
            this.pnlPedidaDeDatos.Controls.Add(this.lblMetodoPago);
            this.pnlPedidaDeDatos.Controls.Add(this.lblFechaVenta);
            this.pnlPedidaDeDatos.Controls.Add(this.lblCodigoCliente);
            this.pnlPedidaDeDatos.Controls.Add(this.lblDetalleVenta);
            this.pnlPedidaDeDatos.Location = new System.Drawing.Point(833, 107);
            this.pnlPedidaDeDatos.Margin = new System.Windows.Forms.Padding(2);
            this.pnlPedidaDeDatos.Name = "pnlPedidaDeDatos";
            this.pnlPedidaDeDatos.Size = new System.Drawing.Size(241, 399);
            this.pnlPedidaDeDatos.TabIndex = 2;
            // 
            // btnAgregarProductos
            // 
            this.btnAgregarProductos.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.btnAgregarProductos.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAgregarProductos.Location = new System.Drawing.Point(24, 206);
            this.btnAgregarProductos.Margin = new System.Windows.Forms.Padding(2);
            this.btnAgregarProductos.Name = "btnAgregarProductos";
            this.btnAgregarProductos.Size = new System.Drawing.Size(194, 25);
            this.btnAgregarProductos.TabIndex = 30;
            this.btnAgregarProductos.Text = "Agregar Productos";
            this.btnAgregarProductos.UseVisualStyleBackColor = false;
            this.btnAgregarProductos.Click += new System.EventHandler(this.btnAgregarProductos_Click);
            // 
            // lblAgregarProductos
            // 
            this.lblAgregarProductos.AutoSize = true;
            this.lblAgregarProductos.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAgregarProductos.Location = new System.Drawing.Point(23, 188);
            this.lblAgregarProductos.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblAgregarProductos.Name = "lblAgregarProductos";
            this.lblAgregarProductos.Size = new System.Drawing.Size(121, 16);
            this.lblAgregarProductos.TabIndex = 29;
            this.lblAgregarProductos.Text = "Agregar Productos :";
            this.lblAgregarProductos.Click += new System.EventHandler(this.label1_Click);
            // 
            // cbCliente
            // 
            this.cbCliente.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbCliente.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbCliente.Font = new System.Drawing.Font("Times New Roman", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbCliente.FormattingEnabled = true;
            this.cbCliente.Location = new System.Drawing.Point(22, 72);
            this.cbCliente.Name = "cbCliente";
            this.cbCliente.Size = new System.Drawing.Size(193, 22);
            this.cbCliente.TabIndex = 28;
            // 
            // txtTotalPagar
            // 
            this.txtTotalPagar.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtTotalPagar.Enabled = false;
            this.txtTotalPagar.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTotalPagar.Location = new System.Drawing.Point(107, 334);
            this.txtTotalPagar.Margin = new System.Windows.Forms.Padding(2);
            this.txtTotalPagar.Name = "txtTotalPagar";
            this.txtTotalPagar.ReadOnly = true;
            this.txtTotalPagar.Size = new System.Drawing.Size(105, 16);
            this.txtTotalPagar.TabIndex = 27;
            // 
            // gbBarraDecorativa
            // 
            this.gbBarraDecorativa.BackColor = System.Drawing.SystemColors.Desktop;
            this.gbBarraDecorativa.Location = new System.Drawing.Point(13, 324);
            this.gbBarraDecorativa.Margin = new System.Windows.Forms.Padding(2);
            this.gbBarraDecorativa.Name = "gbBarraDecorativa";
            this.gbBarraDecorativa.Padding = new System.Windows.Forms.Padding(2);
            this.gbBarraDecorativa.Size = new System.Drawing.Size(217, 3);
            this.gbBarraDecorativa.TabIndex = 26;
            this.gbBarraDecorativa.TabStop = false;
            // 
            // lblTotalPagar
            // 
            this.lblTotalPagar.AutoSize = true;
            this.lblTotalPagar.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalPagar.Location = new System.Drawing.Point(18, 331);
            this.lblTotalPagar.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblTotalPagar.Name = "lblTotalPagar";
            this.lblTotalPagar.Size = new System.Drawing.Size(83, 16);
            this.lblTotalPagar.TabIndex = 25;
            this.lblTotalPagar.Text = "Total a pagar:";
            // 
            // txtIVA
            // 
            this.txtIVA.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtIVA.Enabled = false;
            this.txtIVA.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtIVA.Location = new System.Drawing.Point(88, 295);
            this.txtIVA.Margin = new System.Windows.Forms.Padding(2);
            this.txtIVA.Name = "txtIVA";
            this.txtIVA.Size = new System.Drawing.Size(105, 16);
            this.txtIVA.TabIndex = 24;
            this.txtIVA.TextChanged += new System.EventHandler(this.txtIVA_TextChanged);
            // 
            // lblResumenPago
            // 
            this.lblResumenPago.AutoSize = true;
            this.lblResumenPago.Font = new System.Drawing.Font("Times New Roman", 16F, System.Drawing.FontStyle.Bold);
            this.lblResumenPago.Location = new System.Drawing.Point(17, 232);
            this.lblResumenPago.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblResumenPago.Name = "lblResumenPago";
            this.lblResumenPago.Size = new System.Drawing.Size(177, 25);
            this.lblResumenPago.TabIndex = 21;
            this.lblResumenPago.Text = "Resumen de pago";
            // 
            // dtFechaVenta
            // 
            this.dtFechaVenta.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtFechaVenta.Location = new System.Drawing.Point(22, 120);
            this.dtFechaVenta.Margin = new System.Windows.Forms.Padding(2);
            this.dtFechaVenta.Name = "dtFechaVenta";
            this.dtFechaVenta.Size = new System.Drawing.Size(196, 20);
            this.dtFechaVenta.TabIndex = 20;
            // 
            // cbMetodoPago
            // 
            this.cbMetodoPago.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbMetodoPago.FormattingEnabled = true;
            this.cbMetodoPago.Location = new System.Drawing.Point(22, 165);
            this.cbMetodoPago.Margin = new System.Windows.Forms.Padding(2);
            this.cbMetodoPago.Name = "cbMetodoPago";
            this.cbMetodoPago.Size = new System.Drawing.Size(196, 21);
            this.cbMetodoPago.TabIndex = 18;
            // 
            // txtSubTotal
            // 
            this.txtSubTotal.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtSubTotal.Enabled = false;
            this.txtSubTotal.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSubTotal.Location = new System.Drawing.Point(85, 267);
            this.txtSubTotal.Margin = new System.Windows.Forms.Padding(2);
            this.txtSubTotal.Name = "txtSubTotal";
            this.txtSubTotal.Size = new System.Drawing.Size(109, 16);
            this.txtSubTotal.TabIndex = 17;
            this.txtSubTotal.TextChanged += new System.EventHandler(this.txtSubTotal_TextChanged);
            // 
            // btnEliminar
            // 
            this.btnEliminar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.btnEliminar.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEliminar.Location = new System.Drawing.Point(151, 362);
            this.btnEliminar.Margin = new System.Windows.Forms.Padding(2);
            this.btnEliminar.Name = "btnEliminar";
            this.btnEliminar.Size = new System.Drawing.Size(63, 25);
            this.btnEliminar.TabIndex = 16;
            this.btnEliminar.Text = "Eliminar";
            this.btnEliminar.UseVisualStyleBackColor = false;
            this.btnEliminar.Click += new System.EventHandler(this.btnEliminar_Click);
            // 
            // btnGuardar
            // 
            this.btnGuardar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.btnGuardar.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGuardar.Location = new System.Drawing.Point(86, 361);
            this.btnGuardar.Margin = new System.Windows.Forms.Padding(2);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(63, 25);
            this.btnGuardar.TabIndex = 15;
            this.btnGuardar.Text = "Guardar";
            this.btnGuardar.UseVisualStyleBackColor = false;
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // btnEditar
            // 
            this.btnEditar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.btnEditar.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEditar.Location = new System.Drawing.Point(23, 361);
            this.btnEditar.Margin = new System.Windows.Forms.Padding(2);
            this.btnEditar.Name = "btnEditar";
            this.btnEditar.Size = new System.Drawing.Size(63, 25);
            this.btnEditar.TabIndex = 14;
            this.btnEditar.Text = "Editar";
            this.btnEditar.UseVisualStyleBackColor = false;
            this.btnEditar.Click += new System.EventHandler(this.btnEditar_Click);
            // 
            // lblIva
            // 
            this.lblIva.AutoSize = true;
            this.lblIva.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblIva.Location = new System.Drawing.Point(23, 296);
            this.lblIva.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblIva.Name = "lblIva";
            this.lblIva.Size = new System.Drawing.Size(31, 16);
            this.lblIva.TabIndex = 12;
            this.lblIva.Text = "IVA:";
            // 
            // lblSubTotal
            // 
            this.lblSubTotal.AutoSize = true;
            this.lblSubTotal.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSubTotal.Location = new System.Drawing.Point(20, 267);
            this.lblSubTotal.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblSubTotal.Name = "lblSubTotal";
            this.lblSubTotal.Size = new System.Drawing.Size(64, 16);
            this.lblSubTotal.TabIndex = 10;
            this.lblSubTotal.Text = "Sub Total:";
            // 
            // lblMetodoPago
            // 
            this.lblMetodoPago.AutoSize = true;
            this.lblMetodoPago.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMetodoPago.Location = new System.Drawing.Point(19, 143);
            this.lblMetodoPago.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblMetodoPago.Name = "lblMetodoPago";
            this.lblMetodoPago.Size = new System.Drawing.Size(101, 16);
            this.lblMetodoPago.TabIndex = 8;
            this.lblMetodoPago.Text = "Método de pago:";
            // 
            // lblFechaVenta
            // 
            this.lblFechaVenta.AutoSize = true;
            this.lblFechaVenta.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFechaVenta.Location = new System.Drawing.Point(19, 101);
            this.lblFechaVenta.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblFechaVenta.Name = "lblFechaVenta";
            this.lblFechaVenta.Size = new System.Drawing.Size(95, 16);
            this.lblFechaVenta.TabIndex = 5;
            this.lblFechaVenta.Text = "Fecha de venta:";
            // 
            // lblCodigoCliente
            // 
            this.lblCodigoCliente.AutoSize = true;
            this.lblCodigoCliente.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCodigoCliente.Location = new System.Drawing.Point(20, 52);
            this.lblCodigoCliente.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblCodigoCliente.Name = "lblCodigoCliente";
            this.lblCodigoCliente.Size = new System.Drawing.Size(52, 16);
            this.lblCodigoCliente.TabIndex = 1;
            this.lblCodigoCliente.Text = "Cliente :";
            // 
            // lblDetalleVenta
            // 
            this.lblDetalleVenta.AutoSize = true;
            this.lblDetalleVenta.Font = new System.Drawing.Font("Times New Roman", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDetalleVenta.Location = new System.Drawing.Point(33, 17);
            this.lblDetalleVenta.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblDetalleVenta.Name = "lblDetalleVenta";
            this.lblDetalleVenta.Size = new System.Drawing.Size(165, 25);
            this.lblDetalleVenta.TabIndex = 0;
            this.lblDetalleVenta.Text = "Detalle de Venta";
            // 
            // lblSubTexto
            // 
            this.lblSubTexto.AutoSize = true;
            this.lblSubTexto.Font = new System.Drawing.Font("Times New Roman", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSubTexto.Location = new System.Drawing.Point(27, 75);
            this.lblSubTexto.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblSubTexto.Name = "lblSubTexto";
            this.lblSubTexto.Size = new System.Drawing.Size(327, 21);
            this.lblSubTexto.TabIndex = 1;
            this.lblSubTexto.Text = "Registro y control de las ventas realizadas.";
            // 
            // lblMensajeInformativoPrincipal
            // 
            this.lblMensajeInformativoPrincipal.AutoSize = true;
            this.lblMensajeInformativoPrincipal.Font = new System.Drawing.Font("Times New Roman", 26F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMensajeInformativoPrincipal.Location = new System.Drawing.Point(25, 33);
            this.lblMensajeInformativoPrincipal.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblMensajeInformativoPrincipal.Name = "lblMensajeInformativoPrincipal";
            this.lblMensajeInformativoPrincipal.Size = new System.Drawing.Size(285, 40);
            this.lblMensajeInformativoPrincipal.TabIndex = 0;
            this.lblMensajeInformativoPrincipal.Text = "Gestión de Ventas";
            // 
            // pnlTablaContenido
            // 
            this.pnlTablaContenido.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlTablaContenido.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(238)))), ((int)(((byte)(235)))));
            this.pnlTablaContenido.Controls.Add(this.lblRegistroVentas);
            this.pnlTablaContenido.Controls.Add(this.dgvVentas);
            this.pnlTablaContenido.Location = new System.Drawing.Point(37, 102);
            this.pnlTablaContenido.Margin = new System.Windows.Forms.Padding(2);
            this.pnlTablaContenido.Name = "pnlTablaContenido";
            this.pnlTablaContenido.Size = new System.Drawing.Size(766, 317);
            this.pnlTablaContenido.TabIndex = 3;
            // 
            // lblRegistroVentas
            // 
            this.lblRegistroVentas.AutoSize = true;
            this.lblRegistroVentas.Cursor = System.Windows.Forms.Cursors.Default;
            this.lblRegistroVentas.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRegistroVentas.Location = new System.Drawing.Point(14, 5);
            this.lblRegistroVentas.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblRegistroVentas.Name = "lblRegistroVentas";
            this.lblRegistroVentas.Size = new System.Drawing.Size(168, 22);
            this.lblRegistroVentas.TabIndex = 29;
            this.lblRegistroVentas.Text = "Registro de Ventas.";
            // 
            // dgvVentas
            // 
            this.dgvVentas.AllowUserToResizeColumns = false;
            this.dgvVentas.AllowUserToResizeRows = false;
            this.dgvVentas.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvVentas.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvVentas.BackgroundColor = System.Drawing.Color.White;
            this.dgvVentas.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.PeachPuff;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvVentas.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.dgvVentas.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvVentas.GridColor = System.Drawing.Color.Black;
            this.dgvVentas.Location = new System.Drawing.Point(14, 30);
            this.dgvVentas.Name = "dgvVentas";
            this.dgvVentas.RowHeadersVisible = false;
            this.dgvVentas.Size = new System.Drawing.Size(741, 269);
            this.dgvVentas.TabIndex = 0;
            this.dgvVentas.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvVentas_CellDoubleClick);
            this.dgvVentas.SelectionChanged += new System.EventHandler(this.dgvVentas_SelectionChanged);
            // 
            // btnGuardarCambios
            // 
            this.btnGuardarCambios.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.btnGuardarCambios.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGuardarCambios.Location = new System.Drawing.Point(86, 361);
            this.btnGuardarCambios.Margin = new System.Windows.Forms.Padding(2);
            this.btnGuardarCambios.Name = "btnGuardarCambios";
            this.btnGuardarCambios.Size = new System.Drawing.Size(63, 25);
            this.btnGuardarCambios.TabIndex = 31;
            this.btnGuardarCambios.Text = "Guardar";
            this.btnGuardarCambios.UseVisualStyleBackColor = false;
            this.btnGuardarCambios.Visible = false;
            this.btnGuardarCambios.Click += new System.EventHandler(this.btnGuardarCambios_Click);
            // 
            // frmVentas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1102, 627);
            this.Controls.Add(this.pnlHeader);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "frmVentas";
            this.Text = "frmVentas";
            this.Load += new System.EventHandler(this.frmVentas_Load);
            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDetalleDeVenta)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbxBuscar)).EndInit();
            this.pnlBarraSuperior.ResumeLayout(false);
            this.pnlBarraSuperior.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbPerfil)).EndInit();
            this.pnlPedidaDeDatos.ResumeLayout(false);
            this.pnlPedidaDeDatos.PerformLayout();
            this.pnlTablaContenido.ResumeLayout(false);
            this.pnlTablaContenido.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvVentas)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.TextBox txtBuscar;
        private System.Windows.Forms.Panel pnlPedidaDeDatos;
        private System.Windows.Forms.TextBox txtTotalPagar;
        private System.Windows.Forms.GroupBox gbBarraDecorativa;
        private System.Windows.Forms.Label lblTotalPagar;
        private System.Windows.Forms.TextBox txtIVA;
        private System.Windows.Forms.Label lblResumenPago;
        private System.Windows.Forms.DateTimePicker dtFechaVenta;
        private System.Windows.Forms.ComboBox cbMetodoPago;
        private System.Windows.Forms.TextBox txtSubTotal;
        private System.Windows.Forms.Button btnEliminar;
        private System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.Button btnEditar;
        private System.Windows.Forms.Label lblIva;
        private System.Windows.Forms.Label lblSubTotal;
        private System.Windows.Forms.Label lblMetodoPago;
        private System.Windows.Forms.Label lblFechaVenta;
        private System.Windows.Forms.Label lblDetalleVenta;
        private System.Windows.Forms.Label lblSubTexto;
        private System.Windows.Forms.Label lblMensajeInformativoPrincipal;
        private System.Windows.Forms.Panel pnlTablaContenido;
        private System.Windows.Forms.Panel pnlBarraSuperior;
        private System.Windows.Forms.Label lblCodigoCliente;
        private System.Windows.Forms.ComboBox cbCliente;
        private System.Windows.Forms.PictureBox pbxBuscar;
        private System.Windows.Forms.DataGridView dgvVentas;
        private System.Windows.Forms.PictureBox pbPerfil;
        private System.Windows.Forms.Label lblAdministrador;
        private System.Windows.Forms.Label lblAgregarProductos;
        private System.Windows.Forms.Button btnAgregarProductos;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblDetalleVentas;
        private System.Windows.Forms.DataGridView dgvDetalleDeVenta;
        private System.Windows.Forms.Label lblRegistroVentas;
        private System.Windows.Forms.Button btnGuardarCambios;
    }
}