namespace Vista.Inventario
{
    partial class frmInventario
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
            this.pnlContenedorPrincipalInventario = new System.Windows.Forms.Panel();
            this.pnlPedidaDeDatos = new System.Windows.Forms.Panel();
            this.btnGuardarCambios = new System.Windows.Forms.Button();
            this.cbCategorias = new System.Windows.Forms.ComboBox();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.cbUnidadMedida = new System.Windows.Forms.ComboBox();
            this.btnEditar = new System.Windows.Forms.Button();
            this.lblMedida = new System.Windows.Forms.Label();
            this.txtCantidad = new System.Windows.Forms.TextBox();
            this.lblCantidad = new System.Windows.Forms.Label();
            this.lblCategoria = new System.Windows.Forms.Label();
            this.txtMaterial = new System.Windows.Forms.TextBox();
            this.lblMaterial = new System.Windows.Forms.Label();
            this.lblDatosMaterial = new System.Windows.Forms.Label();
            this.lblSubTexto = new System.Windows.Forms.Label();
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.btnBuscar = new System.Windows.Forms.Button();
            this.txtBuscar = new System.Windows.Forms.TextBox();
            this.pnlBarraSuperior = new System.Windows.Forms.Panel();
            this.lblAdministrador = new System.Windows.Forms.Label();
            this.pbPerfil = new System.Windows.Forms.PictureBox();
            this.pnlIndicador3 = new System.Windows.Forms.Panel();
            this.lblTotalInventario = new System.Windows.Forms.Label();
            this.pbTotalInventario = new System.Windows.Forms.PictureBox();
            this.pnlIndicador2 = new System.Windows.Forms.Panel();
            this.lblPocasUnidades = new System.Windows.Forms.Label();
            this.pbPocasUnidades = new System.Windows.Forms.PictureBox();
            this.pnlIndicador = new System.Windows.Forms.Panel();
            this.lblAgotados = new System.Windows.Forms.Label();
            this.pbAgotados = new System.Windows.Forms.PictureBox();
            this.pnlIndicador1 = new System.Windows.Forms.Panel();
            this.lblMaterialDisponible = new System.Windows.Forms.Label();
            this.pbDisponibles = new System.Windows.Forms.PictureBox();
            this.lblMensajeInformativoPrincipal = new System.Windows.Forms.Label();
            this.pnlPrincipal = new System.Windows.Forms.Panel();
            this.dgvMateriales = new System.Windows.Forms.DataGridView();
            this.pnlContenedorPrincipalInventario.SuspendLayout();
            this.pnlPedidaDeDatos.SuspendLayout();
            this.pnlHeader.SuspendLayout();
            this.pnlBarraSuperior.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbPerfil)).BeginInit();
            this.pnlIndicador3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbTotalInventario)).BeginInit();
            this.pnlIndicador2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbPocasUnidades)).BeginInit();
            this.pnlIndicador.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbAgotados)).BeginInit();
            this.pnlIndicador1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbDisponibles)).BeginInit();
            this.pnlPrincipal.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMateriales)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlContenedorPrincipalInventario
            // 
            this.pnlContenedorPrincipalInventario.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(221)))), ((int)(((byte)(175)))));
            this.pnlContenedorPrincipalInventario.Controls.Add(this.pnlPedidaDeDatos);
            this.pnlContenedorPrincipalInventario.Controls.Add(this.lblSubTexto);
            this.pnlContenedorPrincipalInventario.Controls.Add(this.pnlHeader);
            this.pnlContenedorPrincipalInventario.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlContenedorPrincipalInventario.Location = new System.Drawing.Point(0, 0);
            this.pnlContenedorPrincipalInventario.Margin = new System.Windows.Forms.Padding(2);
            this.pnlContenedorPrincipalInventario.Name = "pnlContenedorPrincipalInventario";
            this.pnlContenedorPrincipalInventario.Size = new System.Drawing.Size(1102, 627);
            this.pnlContenedorPrincipalInventario.TabIndex = 3;
            // 
            // pnlPedidaDeDatos
            // 
            this.pnlPedidaDeDatos.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlPedidaDeDatos.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(215)))), ((int)(((byte)(215)))));
            this.pnlPedidaDeDatos.Controls.Add(this.btnGuardarCambios);
            this.pnlPedidaDeDatos.Controls.Add(this.cbCategorias);
            this.pnlPedidaDeDatos.Controls.Add(this.btnGuardar);
            this.pnlPedidaDeDatos.Controls.Add(this.cbUnidadMedida);
            this.pnlPedidaDeDatos.Controls.Add(this.btnEditar);
            this.pnlPedidaDeDatos.Controls.Add(this.lblMedida);
            this.pnlPedidaDeDatos.Controls.Add(this.txtCantidad);
            this.pnlPedidaDeDatos.Controls.Add(this.lblCantidad);
            this.pnlPedidaDeDatos.Controls.Add(this.lblCategoria);
            this.pnlPedidaDeDatos.Controls.Add(this.txtMaterial);
            this.pnlPedidaDeDatos.Controls.Add(this.lblMaterial);
            this.pnlPedidaDeDatos.Controls.Add(this.lblDatosMaterial);
            this.pnlPedidaDeDatos.Location = new System.Drawing.Point(6, 117);
            this.pnlPedidaDeDatos.Margin = new System.Windows.Forms.Padding(2);
            this.pnlPedidaDeDatos.Name = "pnlPedidaDeDatos";
            this.pnlPedidaDeDatos.Size = new System.Drawing.Size(220, 365);
            this.pnlPedidaDeDatos.TabIndex = 2;
            // 
            // btnGuardarCambios
            // 
            this.btnGuardarCambios.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.btnGuardarCambios.Font = new System.Drawing.Font("Times New Roman", 12F);
            this.btnGuardarCambios.Location = new System.Drawing.Point(116, 313);
            this.btnGuardarCambios.Margin = new System.Windows.Forms.Padding(2);
            this.btnGuardarCambios.Name = "btnGuardarCambios";
            this.btnGuardarCambios.Size = new System.Drawing.Size(86, 36);
            this.btnGuardarCambios.TabIndex = 23;
            this.btnGuardarCambios.Text = "Guardar";
            this.btnGuardarCambios.UseVisualStyleBackColor = false;
            this.btnGuardarCambios.Click += new System.EventHandler(this.btnGuardarCambios_Click);
            // 
            // cbCategorias
            // 
            this.cbCategorias.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbCategorias.FormattingEnabled = true;
            this.cbCategorias.Location = new System.Drawing.Point(24, 152);
            this.cbCategorias.Name = "cbCategorias";
            this.cbCategorias.Size = new System.Drawing.Size(164, 21);
            this.cbCategorias.TabIndex = 22;
            // 
            // btnGuardar
            // 
            this.btnGuardar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.btnGuardar.Font = new System.Drawing.Font("Times New Roman", 12F);
            this.btnGuardar.Location = new System.Drawing.Point(116, 313);
            this.btnGuardar.Margin = new System.Windows.Forms.Padding(2);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(86, 36);
            this.btnGuardar.TabIndex = 21;
            this.btnGuardar.Text = "Guardar";
            this.btnGuardar.UseVisualStyleBackColor = false;
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // cbUnidadMedida
            // 
            this.cbUnidadMedida.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbUnidadMedida.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbUnidadMedida.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbUnidadMedida.FormattingEnabled = true;
            this.cbUnidadMedida.Location = new System.Drawing.Point(23, 274);
            this.cbUnidadMedida.Margin = new System.Windows.Forms.Padding(2);
            this.cbUnidadMedida.Name = "cbUnidadMedida";
            this.cbUnidadMedida.Size = new System.Drawing.Size(166, 28);
            this.cbUnidadMedida.TabIndex = 18;
            // 
            // btnEditar
            // 
            this.btnEditar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.btnEditar.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEditar.Location = new System.Drawing.Point(24, 313);
            this.btnEditar.Margin = new System.Windows.Forms.Padding(2);
            this.btnEditar.Name = "btnEditar";
            this.btnEditar.Size = new System.Drawing.Size(88, 36);
            this.btnEditar.TabIndex = 14;
            this.btnEditar.Text = "Editar";
            this.btnEditar.UseVisualStyleBackColor = false;
            this.btnEditar.Click += new System.EventHandler(this.btnEditar_Click);
            // 
            // lblMedida
            // 
            this.lblMedida.AutoSize = true;
            this.lblMedida.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMedida.Location = new System.Drawing.Point(20, 240);
            this.lblMedida.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblMedida.Name = "lblMedida";
            this.lblMedida.Size = new System.Drawing.Size(123, 19);
            this.lblMedida.TabIndex = 8;
            this.lblMedida.Text = "Unidad de medida:";
            // 
            // txtCantidad
            // 
            this.txtCantidad.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtCantidad.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCantidad.Location = new System.Drawing.Point(21, 208);
            this.txtCantidad.Margin = new System.Windows.Forms.Padding(2);
            this.txtCantidad.Name = "txtCantidad";
            this.txtCantidad.Size = new System.Drawing.Size(167, 19);
            this.txtCantidad.TabIndex = 6;
            this.txtCantidad.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCantidad_KeyPress);
            // 
            // lblCantidad
            // 
            this.lblCantidad.AutoSize = true;
            this.lblCantidad.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCantidad.Location = new System.Drawing.Point(21, 179);
            this.lblCantidad.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblCantidad.Name = "lblCantidad";
            this.lblCantidad.Size = new System.Drawing.Size(67, 19);
            this.lblCantidad.TabIndex = 5;
            this.lblCantidad.Text = "Cantidad:";
            // 
            // lblCategoria
            // 
            this.lblCategoria.AutoSize = true;
            this.lblCategoria.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCategoria.Location = new System.Drawing.Point(21, 124);
            this.lblCategoria.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblCategoria.Name = "lblCategoria";
            this.lblCategoria.Size = new System.Drawing.Size(71, 19);
            this.lblCategoria.TabIndex = 3;
            this.lblCategoria.Text = "Categoría:";
            // 
            // txtMaterial
            // 
            this.txtMaterial.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtMaterial.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMaterial.Location = new System.Drawing.Point(21, 100);
            this.txtMaterial.Margin = new System.Windows.Forms.Padding(2);
            this.txtMaterial.Name = "txtMaterial";
            this.txtMaterial.Size = new System.Drawing.Size(167, 19);
            this.txtMaterial.TabIndex = 2;
            this.txtMaterial.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtMaterial_KeyPress);
            // 
            // lblMaterial
            // 
            this.lblMaterial.AutoSize = true;
            this.lblMaterial.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMaterial.Location = new System.Drawing.Point(20, 75);
            this.lblMaterial.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblMaterial.Name = "lblMaterial";
            this.lblMaterial.Size = new System.Drawing.Size(62, 19);
            this.lblMaterial.TabIndex = 1;
            this.lblMaterial.Text = "Material:";
            // 
            // lblDatosMaterial
            // 
            this.lblDatosMaterial.AutoSize = true;
            this.lblDatosMaterial.Font = new System.Drawing.Font("Times New Roman", 18F, System.Drawing.FontStyle.Bold);
            this.lblDatosMaterial.Location = new System.Drawing.Point(12, 25);
            this.lblDatosMaterial.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblDatosMaterial.Name = "lblDatosMaterial";
            this.lblDatosMaterial.Size = new System.Drawing.Size(200, 26);
            this.lblDatosMaterial.TabIndex = 0;
            this.lblDatosMaterial.Text = "Datos del material";
            // 
            // lblSubTexto
            // 
            this.lblSubTexto.AutoSize = true;
            this.lblSubTexto.Font = new System.Drawing.Font("Times New Roman", 16F);
            this.lblSubTexto.Location = new System.Drawing.Point(19, 69);
            this.lblSubTexto.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblSubTexto.Name = "lblSubTexto";
            this.lblSubTexto.Size = new System.Drawing.Size(344, 25);
            this.lblSubTexto.TabIndex = 1;
            this.lblSubTexto.Text = "Administración general de materiales.";
            // 
            // pnlHeader
            // 
            this.pnlHeader.Controls.Add(this.btnBuscar);
            this.pnlHeader.Controls.Add(this.txtBuscar);
            this.pnlHeader.Controls.Add(this.pnlBarraSuperior);
            this.pnlHeader.Controls.Add(this.pnlIndicador3);
            this.pnlHeader.Controls.Add(this.pnlIndicador2);
            this.pnlHeader.Controls.Add(this.pnlIndicador);
            this.pnlHeader.Controls.Add(this.pnlIndicador1);
            this.pnlHeader.Controls.Add(this.lblMensajeInformativoPrincipal);
            this.pnlHeader.Controls.Add(this.pnlPrincipal);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlHeader.Margin = new System.Windows.Forms.Padding(2);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Size = new System.Drawing.Size(1102, 627);
            this.pnlHeader.TabIndex = 0;
            // 
            // btnBuscar
            // 
            this.btnBuscar.BackColor = System.Drawing.Color.White;
            this.btnBuscar.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnBuscar.Image = global::Vista.Properties.Resources.lupa;
            this.btnBuscar.Location = new System.Drawing.Point(976, 53);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(75, 29);
            this.btnBuscar.TabIndex = 21;
            this.btnBuscar.UseVisualStyleBackColor = false;
            // 
            // txtBuscar
            // 
            this.txtBuscar.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBuscar.ForeColor = System.Drawing.Color.Gray;
            this.txtBuscar.Location = new System.Drawing.Point(541, 56);
            this.txtBuscar.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtBuscar.Name = "txtBuscar";
            this.txtBuscar.Size = new System.Drawing.Size(488, 26);
            this.txtBuscar.TabIndex = 9;
            this.txtBuscar.Text = "Buscar Material...";
            this.txtBuscar.TextChanged += new System.EventHandler(this.txtBuscar_TextChanged);
            this.txtBuscar.Enter += new System.EventHandler(this.txtBuscar_Enter);
            this.txtBuscar.Leave += new System.EventHandler(this.txtBuscar_Leave);
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
            this.pnlBarraSuperior.TabIndex = 8;
            // 
            // lblAdministrador
            // 
            this.lblAdministrador.AutoSize = true;
            this.lblAdministrador.Font = new System.Drawing.Font("Times New Roman", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAdministrador.Location = new System.Drawing.Point(1013, 4);
            this.lblAdministrador.Name = "lblAdministrador";
            this.lblAdministrador.Size = new System.Drawing.Size(38, 14);
            this.lblAdministrador.TabIndex = 26;
            this.lblAdministrador.Text = "Admin";
            // 
            // pbPerfil
            // 
            this.pbPerfil.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(177)))), ((int)(((byte)(114)))));
            this.pbPerfil.Image = global::Vista.Properties.Resources.user_456283;
            this.pbPerfil.Location = new System.Drawing.Point(1054, -1);
            this.pbPerfil.Name = "pbPerfil";
            this.pbPerfil.Size = new System.Drawing.Size(26, 24);
            this.pbPerfil.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbPerfil.TabIndex = 25;
            this.pbPerfil.TabStop = false;
            // 
            // pnlIndicador3
            // 
            this.pnlIndicador3.BackColor = System.Drawing.Color.White;
            this.pnlIndicador3.Controls.Add(this.lblTotalInventario);
            this.pnlIndicador3.Controls.Add(this.pbTotalInventario);
            this.pnlIndicador3.Location = new System.Drawing.Point(878, 524);
            this.pnlIndicador3.Margin = new System.Windows.Forms.Padding(2);
            this.pnlIndicador3.Name = "pnlIndicador3";
            this.pnlIndicador3.Size = new System.Drawing.Size(201, 77);
            this.pnlIndicador3.TabIndex = 4;
            // 
            // lblTotalInventario
            // 
            this.lblTotalInventario.AutoSize = true;
            this.lblTotalInventario.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalInventario.Location = new System.Drawing.Point(89, 16);
            this.lblTotalInventario.Name = "lblTotalInventario";
            this.lblTotalInventario.Size = new System.Drawing.Size(107, 15);
            this.lblTotalInventario.TabIndex = 6;
            this.lblTotalInventario.Text = "Total de Inventario";
            // 
            // pbTotalInventario
            // 
            this.pbTotalInventario.Image = global::Vista.Properties.Resources.Material_Registrado;
            this.pbTotalInventario.Location = new System.Drawing.Point(17, 13);
            this.pbTotalInventario.Name = "pbTotalInventario";
            this.pbTotalInventario.Size = new System.Drawing.Size(71, 54);
            this.pbTotalInventario.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbTotalInventario.TabIndex = 0;
            this.pbTotalInventario.TabStop = false;
            // 
            // pnlIndicador2
            // 
            this.pnlIndicador2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(186)))), ((int)(((byte)(120)))));
            this.pnlIndicador2.Controls.Add(this.lblPocasUnidades);
            this.pnlIndicador2.Controls.Add(this.pbPocasUnidades);
            this.pnlIndicador2.Location = new System.Drawing.Point(672, 524);
            this.pnlIndicador2.Margin = new System.Windows.Forms.Padding(2);
            this.pnlIndicador2.Name = "pnlIndicador2";
            this.pnlIndicador2.Size = new System.Drawing.Size(201, 77);
            this.pnlIndicador2.TabIndex = 4;
            // 
            // lblPocasUnidades
            // 
            this.lblPocasUnidades.AutoSize = true;
            this.lblPocasUnidades.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPocasUnidades.Location = new System.Drawing.Point(104, 17);
            this.lblPocasUnidades.Name = "lblPocasUnidades";
            this.lblPocasUnidades.Size = new System.Drawing.Size(91, 15);
            this.lblPocasUnidades.TabIndex = 5;
            this.lblPocasUnidades.Text = "Pocas Unidades";
            // 
            // pbPocasUnidades
            // 
            this.pbPocasUnidades.Image = global::Vista.Properties.Resources.material_alerta;
            this.pbPocasUnidades.Location = new System.Drawing.Point(16, 7);
            this.pbPocasUnidades.Name = "pbPocasUnidades";
            this.pbPocasUnidades.Size = new System.Drawing.Size(80, 64);
            this.pbPocasUnidades.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbPocasUnidades.TabIndex = 1;
            this.pbPocasUnidades.TabStop = false;
            // 
            // pnlIndicador
            // 
            this.pnlIndicador.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(162)))), ((int)(((byte)(147)))));
            this.pnlIndicador.Controls.Add(this.lblAgotados);
            this.pnlIndicador.Controls.Add(this.pbAgotados);
            this.pnlIndicador.Location = new System.Drawing.Point(260, 524);
            this.pnlIndicador.Margin = new System.Windows.Forms.Padding(2);
            this.pnlIndicador.Name = "pnlIndicador";
            this.pnlIndicador.Size = new System.Drawing.Size(201, 77);
            this.pnlIndicador.TabIndex = 4;
            // 
            // lblAgotados
            // 
            this.lblAgotados.AutoSize = true;
            this.lblAgotados.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAgotados.Location = new System.Drawing.Point(115, 17);
            this.lblAgotados.Name = "lblAgotados";
            this.lblAgotados.Size = new System.Drawing.Size(57, 15);
            this.lblAgotados.TabIndex = 4;
            this.lblAgotados.Text = "Agotados";
            // 
            // pbAgotados
            // 
            this.pbAgotados.Image = global::Vista.Properties.Resources.material_no_disponible;
            this.pbAgotados.Location = new System.Drawing.Point(19, 7);
            this.pbAgotados.Name = "pbAgotados";
            this.pbAgotados.Size = new System.Drawing.Size(80, 64);
            this.pbAgotados.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbAgotados.TabIndex = 2;
            this.pbAgotados.TabStop = false;
            // 
            // pnlIndicador1
            // 
            this.pnlIndicador1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(165)))), ((int)(((byte)(237)))), ((int)(((byte)(147)))));
            this.pnlIndicador1.Controls.Add(this.lblMaterialDisponible);
            this.pnlIndicador1.Controls.Add(this.pbDisponibles);
            this.pnlIndicador1.Location = new System.Drawing.Point(467, 524);
            this.pnlIndicador1.Margin = new System.Windows.Forms.Padding(2);
            this.pnlIndicador1.Name = "pnlIndicador1";
            this.pnlIndicador1.Size = new System.Drawing.Size(201, 77);
            this.pnlIndicador1.TabIndex = 4;
            // 
            // lblMaterialDisponible
            // 
            this.lblMaterialDisponible.AutoSize = true;
            this.lblMaterialDisponible.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMaterialDisponible.Location = new System.Drawing.Point(119, 19);
            this.lblMaterialDisponible.Name = "lblMaterialDisponible";
            this.lblMaterialDisponible.Size = new System.Drawing.Size(71, 15);
            this.lblMaterialDisponible.TabIndex = 3;
            this.lblMaterialDisponible.Text = "Disponibles";
            // 
            // pbDisponibles
            // 
            this.pbDisponibles.Image = global::Vista.Properties.Resources.material_si_disponible;
            this.pbDisponibles.Location = new System.Drawing.Point(20, 7);
            this.pbDisponibles.Name = "pbDisponibles";
            this.pbDisponibles.Size = new System.Drawing.Size(80, 64);
            this.pbDisponibles.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbDisponibles.TabIndex = 2;
            this.pbDisponibles.TabStop = false;
            // 
            // lblMensajeInformativoPrincipal
            // 
            this.lblMensajeInformativoPrincipal.AutoSize = true;
            this.lblMensajeInformativoPrincipal.Font = new System.Drawing.Font("Times New Roman", 28F, System.Drawing.FontStyle.Bold);
            this.lblMensajeInformativoPrincipal.Location = new System.Drawing.Point(13, 25);
            this.lblMensajeInformativoPrincipal.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblMensajeInformativoPrincipal.Name = "lblMensajeInformativoPrincipal";
            this.lblMensajeInformativoPrincipal.Size = new System.Drawing.Size(373, 43);
            this.lblMensajeInformativoPrincipal.TabIndex = 0;
            this.lblMensajeInformativoPrincipal.Text = "Gestión de Inventario";
            // 
            // pnlPrincipal
            // 
            this.pnlPrincipal.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(213)))), ((int)(((byte)(153)))), ((int)(((byte)(105)))));
            this.pnlPrincipal.Controls.Add(this.dgvMateriales);
            this.pnlPrincipal.Location = new System.Drawing.Point(257, 120);
            this.pnlPrincipal.Margin = new System.Windows.Forms.Padding(2);
            this.pnlPrincipal.Name = "pnlPrincipal";
            this.pnlPrincipal.Size = new System.Drawing.Size(817, 387);
            this.pnlPrincipal.TabIndex = 3;
            // 
            // dgvMateriales
            // 
            this.dgvMateriales.AllowUserToResizeColumns = false;
            this.dgvMateriales.AllowUserToResizeRows = false;
            this.dgvMateriales.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvMateriales.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvMateriales.BackgroundColor = System.Drawing.Color.White;
            this.dgvMateriales.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvMateriales.ColumnHeadersHeight = 30;
            this.dgvMateriales.GridColor = System.Drawing.Color.Black;
            this.dgvMateriales.Location = new System.Drawing.Point(10, 6);
            this.dgvMateriales.Name = "dgvMateriales";
            this.dgvMateriales.ReadOnly = true;
            this.dgvMateriales.RowHeadersWidth = 45;
            this.dgvMateriales.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dgvMateriales.Size = new System.Drawing.Size(797, 371);
            this.dgvMateriales.TabIndex = 0;
            this.dgvMateriales.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvInventario_CellClick);
            this.dgvMateriales.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvMateriales_CellDoubleClick);
            // 
            // frmInventario
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(1102, 627);
            this.Controls.Add(this.pnlContenedorPrincipalInventario);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmInventario";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Inventario";
            this.Load += new System.EventHandler(this.frmInventario_Load);
            this.pnlContenedorPrincipalInventario.ResumeLayout(false);
            this.pnlContenedorPrincipalInventario.PerformLayout();
            this.pnlPedidaDeDatos.ResumeLayout(false);
            this.pnlPedidaDeDatos.PerformLayout();
            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            this.pnlBarraSuperior.ResumeLayout(false);
            this.pnlBarraSuperior.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbPerfil)).EndInit();
            this.pnlIndicador3.ResumeLayout(false);
            this.pnlIndicador3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbTotalInventario)).EndInit();
            this.pnlIndicador2.ResumeLayout(false);
            this.pnlIndicador2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbPocasUnidades)).EndInit();
            this.pnlIndicador.ResumeLayout(false);
            this.pnlIndicador.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbAgotados)).EndInit();
            this.pnlIndicador1.ResumeLayout(false);
            this.pnlIndicador1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbDisponibles)).EndInit();
            this.pnlPrincipal.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvMateriales)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlContenedorPrincipalInventario;
        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label lblMensajeInformativoPrincipal;
        private System.Windows.Forms.Label lblSubTexto;
        private System.Windows.Forms.Panel pnlPedidaDeDatos;
        private System.Windows.Forms.Label lblDatosMaterial;
        private System.Windows.Forms.Label lblMaterial;
        private System.Windows.Forms.TextBox txtMaterial;
        private System.Windows.Forms.Label lblCategoria;
        private System.Windows.Forms.Label lblMedida;
        private System.Windows.Forms.TextBox txtCantidad;
        private System.Windows.Forms.Label lblCantidad;
        private System.Windows.Forms.Button btnEditar;
        private System.Windows.Forms.Panel pnlIndicador2;
        private System.Windows.Forms.Panel pnlIndicador1;
        private System.Windows.Forms.Panel pnlIndicador;
        private System.Windows.Forms.Panel pnlPrincipal;
        private System.Windows.Forms.Panel pnlIndicador3;
        private System.Windows.Forms.ComboBox cbUnidadMedida;
        private System.Windows.Forms.Panel pnlBarraSuperior;
        private System.Windows.Forms.TextBox txtBuscar;
        private System.Windows.Forms.PictureBox pbTotalInventario;
        private System.Windows.Forms.PictureBox pbPocasUnidades;
        private System.Windows.Forms.PictureBox pbAgotados;
        private System.Windows.Forms.PictureBox pbDisponibles;
        private System.Windows.Forms.Label lblMaterialDisponible;
        private System.Windows.Forms.Label lblAgotados;
        private System.Windows.Forms.Label lblPocasUnidades;
        private System.Windows.Forms.Label lblTotalInventario;
        private System.Windows.Forms.PictureBox pbPerfil;
        private System.Windows.Forms.Label lblAdministrador;
        private System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.DataGridView dgvMateriales;
        private System.Windows.Forms.ComboBox cbCategorias;
        private System.Windows.Forms.Button btnGuardarCambios;
        private System.Windows.Forms.Button btnBuscar;
    }
}