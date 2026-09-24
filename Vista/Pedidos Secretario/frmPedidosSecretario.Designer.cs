namespace Vista.Pedidos_Secretario
{
    partial class frmPedidosSecretario
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.pnlContenedorPrincipalInventario = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblSubTexto = new System.Windows.Forms.Label();
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.pnlRegistros = new System.Windows.Forms.Panel();
            this.lblPagina = new System.Windows.Forms.Label();
            this.btnAnterior = new System.Windows.Forms.Button();
            this.btnSiguiente = new System.Windows.Forms.Button();
            this.pnlTitulo2 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.dgvPedidosRegistrados = new System.Windows.Forms.DataGridView();
            this.pbxBuscar = new System.Windows.Forms.PictureBox();
            this.pnlBarraInformativa = new System.Windows.Forms.Panel();
            this.lblSercretario = new System.Windows.Forms.Label();
            this.pictureBox4 = new System.Windows.Forms.PictureBox();
            this.txtBuscar = new System.Windows.Forms.TextBox();
            this.lblMensajeInformativoPedidos = new System.Windows.Forms.Label();
            this.pnlDEtalles = new System.Windows.Forms.Panel();
            this.pnlTitulo1 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.dgvDetallesDePedido = new System.Windows.Forms.DataGridView();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.pnlContenedorPrincipalInventario.SuspendLayout();
            this.panel1.SuspendLayout();
            this.pnlHeader.SuspendLayout();
            this.pnlRegistros.SuspendLayout();
            this.pnlTitulo2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPedidosRegistrados)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbxBuscar)).BeginInit();
            this.pnlBarraInformativa.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).BeginInit();
            this.pnlDEtalles.SuspendLayout();
            this.pnlTitulo1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDetallesDePedido)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlContenedorPrincipalInventario
            // 
            this.pnlContenedorPrincipalInventario.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(221)))), ((int)(((byte)(175)))));
            this.pnlContenedorPrincipalInventario.Controls.Add(this.panel1);
            this.pnlContenedorPrincipalInventario.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlContenedorPrincipalInventario.Location = new System.Drawing.Point(0, 0);
            this.pnlContenedorPrincipalInventario.Margin = new System.Windows.Forms.Padding(2);
            this.pnlContenedorPrincipalInventario.Name = "pnlContenedorPrincipalInventario";
            this.pnlContenedorPrincipalInventario.Size = new System.Drawing.Size(1102, 627);
            this.pnlContenedorPrincipalInventario.TabIndex = 5;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(221)))), ((int)(((byte)(175)))));
            this.panel1.Controls.Add(this.lblSubTexto);
            this.panel1.Controls.Add(this.pnlHeader);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1102, 627);
            this.panel1.TabIndex = 5;
            // 
            // lblSubTexto
            // 
            this.lblSubTexto.AutoSize = true;
            this.lblSubTexto.Font = new System.Drawing.Font("Times New Roman", 13F);
            this.lblSubTexto.Location = new System.Drawing.Point(16, 66);
            this.lblSubTexto.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblSubTexto.Name = "lblSubTexto";
            this.lblSubTexto.Size = new System.Drawing.Size(276, 20);
            this.lblSubTexto.TabIndex = 1;
            this.lblSubTexto.Text = "Administración y registro de pedidos.";
            // 
            // pnlHeader
            // 
            this.pnlHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(227)))), ((int)(((byte)(175)))));
            this.pnlHeader.Controls.Add(this.pnlRegistros);
            this.pnlHeader.Controls.Add(this.pbxBuscar);
            this.pnlHeader.Controls.Add(this.pnlBarraInformativa);
            this.pnlHeader.Controls.Add(this.txtBuscar);
            this.pnlHeader.Controls.Add(this.lblMensajeInformativoPedidos);
            this.pnlHeader.Controls.Add(this.pnlDEtalles);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlHeader.Margin = new System.Windows.Forms.Padding(2);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Size = new System.Drawing.Size(1102, 627);
            this.pnlHeader.TabIndex = 0;
            // 
            // pnlRegistros
            // 
            this.pnlRegistros.BackColor = System.Drawing.Color.Bisque;
            this.pnlRegistros.Controls.Add(this.lblPagina);
            this.pnlRegistros.Controls.Add(this.btnAnterior);
            this.pnlRegistros.Controls.Add(this.btnSiguiente);
            this.pnlRegistros.Controls.Add(this.pnlTitulo2);
            this.pnlRegistros.Controls.Add(this.dgvPedidosRegistrados);
            this.pnlRegistros.Location = new System.Drawing.Point(19, 97);
            this.pnlRegistros.Margin = new System.Windows.Forms.Padding(2);
            this.pnlRegistros.Name = "pnlRegistros";
            this.pnlRegistros.Size = new System.Drawing.Size(1060, 289);
            this.pnlRegistros.TabIndex = 4;
            // 
            // lblPagina
            // 
            this.lblPagina.AutoSize = true;
            this.lblPagina.Font = new System.Drawing.Font("Times New Roman", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPagina.ForeColor = System.Drawing.Color.Black;
            this.lblPagina.Location = new System.Drawing.Point(53, 265);
            this.lblPagina.Name = "lblPagina";
            this.lblPagina.Size = new System.Drawing.Size(39, 13);
            this.lblPagina.TabIndex = 13;
            this.lblPagina.Text = "label1";
            // 
            // btnAnterior
            // 
            this.btnAnterior.FlatAppearance.BorderSize = 0;
            this.btnAnterior.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAnterior.Image = global::Vista.Properties.Resources.hacia_atras_negro2;
            this.btnAnterior.Location = new System.Drawing.Point(16, 260);
            this.btnAnterior.Name = "btnAnterior";
            this.btnAnterior.Size = new System.Drawing.Size(30, 23);
            this.btnAnterior.TabIndex = 12;
            this.btnAnterior.UseVisualStyleBackColor = true;
            this.btnAnterior.Click += new System.EventHandler(this.btnAnterior_Click);
            // 
            // btnSiguiente
            // 
            this.btnSiguiente.FlatAppearance.BorderSize = 0;
            this.btnSiguiente.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSiguiente.Image = global::Vista.Properties.Resources.hacia_adelante_negro;
            this.btnSiguiente.Location = new System.Drawing.Point(140, 260);
            this.btnSiguiente.Name = "btnSiguiente";
            this.btnSiguiente.Size = new System.Drawing.Size(30, 23);
            this.btnSiguiente.TabIndex = 11;
            this.btnSiguiente.UseVisualStyleBackColor = true;
            this.btnSiguiente.Click += new System.EventHandler(this.btnSiguiente_Click);
            // 
            // pnlTitulo2
            // 
            this.pnlTitulo2.BackColor = System.Drawing.Color.SaddleBrown;
            this.pnlTitulo2.Controls.Add(this.label3);
            this.pnlTitulo2.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTitulo2.Location = new System.Drawing.Point(0, 0);
            this.pnlTitulo2.Name = "pnlTitulo2";
            this.pnlTitulo2.Size = new System.Drawing.Size(1060, 25);
            this.pnlTitulo2.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(14, 4);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(146, 19);
            this.label3.TabIndex = 2;
            this.label3.Text = "Pedidos Registrados";
            // 
            // dgvPedidosRegistrados
            // 
            this.dgvPedidosRegistrados.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvPedidosRegistrados.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvPedidosRegistrados.BackgroundColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.PeachPuff;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvPedidosRegistrados.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvPedidosRegistrados.Location = new System.Drawing.Point(11, 34);
            this.dgvPedidosRegistrados.Name = "dgvPedidosRegistrados";
            this.dgvPedidosRegistrados.ReadOnly = true;
            this.dgvPedidosRegistrados.Size = new System.Drawing.Size(1039, 224);
            this.dgvPedidosRegistrados.TabIndex = 0;
            this.dgvPedidosRegistrados.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvPedidosRegistrados_CellClick);
            this.dgvPedidosRegistrados.SelectionChanged += new System.EventHandler(this.dgvPedidosRegistrados_SelectionChanged);
            // 
            // pbxBuscar
            // 
            this.pbxBuscar.BackColor = System.Drawing.Color.Transparent;
            this.pbxBuscar.Image = global::Vista.Properties.Resources.zoom_5611171;
            this.pbxBuscar.Location = new System.Drawing.Point(966, 56);
            this.pbxBuscar.Name = "pbxBuscar";
            this.pbxBuscar.Size = new System.Drawing.Size(26, 26);
            this.pbxBuscar.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbxBuscar.TabIndex = 26;
            this.pbxBuscar.TabStop = false;
            // 
            // pnlBarraInformativa
            // 
            this.pnlBarraInformativa.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(177)))), ((int)(((byte)(114)))));
            this.pnlBarraInformativa.Controls.Add(this.lblSercretario);
            this.pnlBarraInformativa.Controls.Add(this.pictureBox4);
            this.pnlBarraInformativa.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlBarraInformativa.Location = new System.Drawing.Point(0, 0);
            this.pnlBarraInformativa.Margin = new System.Windows.Forms.Padding(2);
            this.pnlBarraInformativa.Name = "pnlBarraInformativa";
            this.pnlBarraInformativa.Size = new System.Drawing.Size(1102, 23);
            this.pnlBarraInformativa.TabIndex = 7;
            // 
            // lblSercretario
            // 
            this.lblSercretario.AutoSize = true;
            this.lblSercretario.Font = new System.Drawing.Font("Times New Roman", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSercretario.Location = new System.Drawing.Point(1013, 4);
            this.lblSercretario.Name = "lblSercretario";
            this.lblSercretario.Size = new System.Drawing.Size(38, 14);
            this.lblSercretario.TabIndex = 29;
            this.lblSercretario.Text = "Admin";
            // 
            // pictureBox4
            // 
            this.pictureBox4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(177)))), ((int)(((byte)(114)))));
            this.pictureBox4.Image = global::Vista.Properties.Resources.user_456283;
            this.pictureBox4.Location = new System.Drawing.Point(1054, -1);
            this.pictureBox4.Name = "pictureBox4";
            this.pictureBox4.Size = new System.Drawing.Size(26, 24);
            this.pictureBox4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox4.TabIndex = 25;
            this.pictureBox4.TabStop = false;
            // 
            // txtBuscar
            // 
            this.txtBuscar.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBuscar.ForeColor = System.Drawing.Color.Gray;
            this.txtBuscar.Location = new System.Drawing.Point(504, 56);
            this.txtBuscar.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtBuscar.Name = "txtBuscar";
            this.txtBuscar.Size = new System.Drawing.Size(488, 26);
            this.txtBuscar.TabIndex = 8;
            this.txtBuscar.Text = "Buscar Pedido...";
            this.txtBuscar.TextChanged += new System.EventHandler(this.txtBuscar_TextChanged);
            this.txtBuscar.Enter += new System.EventHandler(this.txtBuscar_Enter_1);
            this.txtBuscar.Leave += new System.EventHandler(this.txtBuscar_Leave_1);
            // 
            // lblMensajeInformativoPedidos
            // 
            this.lblMensajeInformativoPedidos.AutoSize = true;
            this.lblMensajeInformativoPedidos.Font = new System.Drawing.Font("Times New Roman", 24F, System.Drawing.FontStyle.Bold);
            this.lblMensajeInformativoPedidos.Location = new System.Drawing.Point(13, 26);
            this.lblMensajeInformativoPedidos.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblMensajeInformativoPedidos.Name = "lblMensajeInformativoPedidos";
            this.lblMensajeInformativoPedidos.Size = new System.Drawing.Size(274, 36);
            this.lblMensajeInformativoPedidos.TabIndex = 0;
            this.lblMensajeInformativoPedidos.Text = "Gestión de Pedidos";
            // 
            // pnlDEtalles
            // 
            this.pnlDEtalles.BackColor = System.Drawing.Color.Bisque;
            this.pnlDEtalles.Controls.Add(this.pnlTitulo1);
            this.pnlDEtalles.Controls.Add(this.dgvDetallesDePedido);
            this.pnlDEtalles.Location = new System.Drawing.Point(19, 394);
            this.pnlDEtalles.Margin = new System.Windows.Forms.Padding(2);
            this.pnlDEtalles.Name = "pnlDEtalles";
            this.pnlDEtalles.Size = new System.Drawing.Size(1060, 225);
            this.pnlDEtalles.TabIndex = 3;
            // 
            // pnlTitulo1
            // 
            this.pnlTitulo1.BackColor = System.Drawing.Color.SaddleBrown;
            this.pnlTitulo1.Controls.Add(this.label2);
            this.pnlTitulo1.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTitulo1.Location = new System.Drawing.Point(0, 0);
            this.pnlTitulo1.Name = "pnlTitulo1";
            this.pnlTitulo1.Size = new System.Drawing.Size(1060, 25);
            this.pnlTitulo1.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(17, 4);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(138, 19);
            this.label2.TabIndex = 1;
            this.label2.Text = "Detalles del Pedido";
            // 
            // dgvDetallesDePedido
            // 
            this.dgvDetallesDePedido.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvDetallesDePedido.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvDetallesDePedido.BackgroundColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.PeachPuff;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvDetallesDePedido.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvDetallesDePedido.Location = new System.Drawing.Point(11, 35);
            this.dgvDetallesDePedido.Name = "dgvDetallesDePedido";
            this.dgvDetallesDePedido.ReadOnly = true;
            this.dgvDetallesDePedido.Size = new System.Drawing.Size(1038, 175);
            this.dgvDetallesDePedido.TabIndex = 0;
            // 
            // frmPedidosSecretario
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1102, 627);
            this.Controls.Add(this.pnlContenedorPrincipalInventario);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmPedidosSecretario";
            this.Text = "frmPedidosSecretario";
            this.Load += new System.EventHandler(this.frmPedidosSecretario_Load);
            this.pnlContenedorPrincipalInventario.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            this.pnlRegistros.ResumeLayout(false);
            this.pnlRegistros.PerformLayout();
            this.pnlTitulo2.ResumeLayout(false);
            this.pnlTitulo2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPedidosRegistrados)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbxBuscar)).EndInit();
            this.pnlBarraInformativa.ResumeLayout(false);
            this.pnlBarraInformativa.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).EndInit();
            this.pnlDEtalles.ResumeLayout(false);
            this.pnlTitulo1.ResumeLayout(false);
            this.pnlTitulo1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDetallesDePedido)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel pnlContenedorPrincipalInventario;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblSubTexto;
        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Panel pnlRegistros;
        private System.Windows.Forms.Panel pnlTitulo2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DataGridView dgvPedidosRegistrados;
        private System.Windows.Forms.PictureBox pbxBuscar;
        private System.Windows.Forms.Panel pnlBarraInformativa;
        private System.Windows.Forms.Label lblSercretario;
        private System.Windows.Forms.PictureBox pictureBox4;
        private System.Windows.Forms.TextBox txtBuscar;
        private System.Windows.Forms.Label lblMensajeInformativoPedidos;
        private System.Windows.Forms.Panel pnlDEtalles;
        private System.Windows.Forms.Panel pnlTitulo1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridView dgvDetallesDePedido;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Label lblPagina;
        private System.Windows.Forms.Button btnAnterior;
        private System.Windows.Forms.Button btnSiguiente;
    }
}
