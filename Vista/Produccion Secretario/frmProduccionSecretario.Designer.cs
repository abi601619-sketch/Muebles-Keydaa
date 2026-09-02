namespace Vista.Produccion_Secretario
{
    partial class frmProduccionSecretario
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.lblFinalizados = new System.Windows.Forms.Label();
            this.pnlIndicador2 = new System.Windows.Forms.Panel();
            this.pbFinalizados = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.pnlContenedorTabla = new System.Windows.Forms.Panel();
            this.dgvProduccion = new System.Windows.Forms.DataGridView();
            this.btnEditar = new System.Windows.Forms.Button();
            this.pnlIndicador1 = new System.Windows.Forms.Panel();
            this.pbCancelados = new System.Windows.Forms.PictureBox();
            this.lblPendientes = new System.Windows.Forms.Label();
            this.pnlIndicador3 = new System.Windows.Forms.Panel();
            this.pbPendientes = new System.Windows.Forms.PictureBox();
            this.lblMensajeInformativoPrincipal = new System.Windows.Forms.Label();
            this.lblRegistrados = new System.Windows.Forms.Label();
            this.lblSubTexto = new System.Windows.Forms.Label();
            this.btnLimpiar = new System.Windows.Forms.Button();
            this.cbEstados = new System.Windows.Forms.ComboBox();
            this.pnlIndicador4 = new System.Windows.Forms.Panel();
            this.pbTotalRegistros = new System.Windows.Forms.PictureBox();
            this.txtBuscar = new System.Windows.Forms.TextBox();
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.pbxBuscar = new System.Windows.Forms.PictureBox();
            this.pnlBarraInformativa = new System.Windows.Forms.Panel();
            this.lblAdministrador = new System.Windows.Forms.Label();
            this.pbPerfil = new System.Windows.Forms.PictureBox();
            this.btnMaterialUtilizado = new System.Windows.Forms.Button();
            this.pnlIndicador2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbFinalizados)).BeginInit();
            this.pnlContenedorTabla.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvProduccion)).BeginInit();
            this.pnlIndicador1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbCancelados)).BeginInit();
            this.pnlIndicador3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbPendientes)).BeginInit();
            this.pnlIndicador4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbTotalRegistros)).BeginInit();
            this.pnlHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbxBuscar)).BeginInit();
            this.pnlBarraInformativa.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbPerfil)).BeginInit();
            this.SuspendLayout();
            // 
            // lblFinalizados
            // 
            this.lblFinalizados.AutoSize = true;
            this.lblFinalizados.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFinalizados.Location = new System.Drawing.Point(106, 17);
            this.lblFinalizados.Name = "lblFinalizados";
            this.lblFinalizados.Size = new System.Drawing.Size(83, 19);
            this.lblFinalizados.TabIndex = 1;
            this.lblFinalizados.Text = "Finalizados";
            // 
            // pnlIndicador2
            // 
            this.pnlIndicador2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(165)))), ((int)(((byte)(237)))), ((int)(((byte)(147)))));
            this.pnlIndicador2.Controls.Add(this.pbFinalizados);
            this.pnlIndicador2.Controls.Add(this.lblFinalizados);
            this.pnlIndicador2.Location = new System.Drawing.Point(272, 134);
            this.pnlIndicador2.Margin = new System.Windows.Forms.Padding(2);
            this.pnlIndicador2.Name = "pnlIndicador2";
            this.pnlIndicador2.Size = new System.Drawing.Size(221, 77);
            this.pnlIndicador2.TabIndex = 8;
            // 
            // pbFinalizados
            // 
            this.pbFinalizados.Image = global::Vista.Properties.Resources.Trabajo_finalizado;
            this.pbFinalizados.Location = new System.Drawing.Point(24, 1);
            this.pbFinalizados.Name = "pbFinalizados";
            this.pbFinalizados.Size = new System.Drawing.Size(68, 74);
            this.pbFinalizados.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbFinalizados.TabIndex = 2;
            this.pbFinalizados.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(107, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(86, 19);
            this.label1.TabIndex = 0;
            this.label1.Text = "Cancelados";
            // 
            // pnlContenedorTabla
            // 
            this.pnlContenedorTabla.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlContenedorTabla.BackColor = System.Drawing.Color.White;
            this.pnlContenedorTabla.Controls.Add(this.btnMaterialUtilizado);
            this.pnlContenedorTabla.Controls.Add(this.dgvProduccion);
            this.pnlContenedorTabla.Controls.Add(this.btnEditar);
            this.pnlContenedorTabla.Location = new System.Drawing.Point(29, 268);
            this.pnlContenedorTabla.Margin = new System.Windows.Forms.Padding(2);
            this.pnlContenedorTabla.Name = "pnlContenedorTabla";
            this.pnlContenedorTabla.Size = new System.Drawing.Size(1045, 337);
            this.pnlContenedorTabla.TabIndex = 3;
            // 
            // dgvProduccion
            // 
            this.dgvProduccion.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvProduccion.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.PeachPuff;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvProduccion.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvProduccion.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvProduccion.Location = new System.Drawing.Point(17, 14);
            this.dgvProduccion.Name = "dgvProduccion";
            this.dgvProduccion.ReadOnly = true;
            this.dgvProduccion.Size = new System.Drawing.Size(1011, 258);
            this.dgvProduccion.TabIndex = 4;
            // 
            // btnEditar
            // 
            this.btnEditar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnEditar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(89)))), ((int)(((byte)(61)))), ((int)(((byte)(21)))));
            this.btnEditar.FlatAppearance.BorderSize = 0;
            this.btnEditar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEditar.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEditar.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnEditar.Location = new System.Drawing.Point(19, 289);
            this.btnEditar.Name = "btnEditar";
            this.btnEditar.Size = new System.Drawing.Size(219, 40);
            this.btnEditar.TabIndex = 1;
            this.btnEditar.Text = "Editar";
            this.btnEditar.UseVisualStyleBackColor = false;
            this.btnEditar.Click += new System.EventHandler(this.btnEditar_Click);
            // 
            // pnlIndicador1
            // 
            this.pnlIndicador1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(162)))), ((int)(((byte)(147)))));
            this.pnlIndicador1.Controls.Add(this.pbCancelados);
            this.pnlIndicador1.Controls.Add(this.label1);
            this.pnlIndicador1.Location = new System.Drawing.Point(48, 134);
            this.pnlIndicador1.Margin = new System.Windows.Forms.Padding(2);
            this.pnlIndicador1.Name = "pnlIndicador1";
            this.pnlIndicador1.Size = new System.Drawing.Size(219, 77);
            this.pnlIndicador1.TabIndex = 7;
            // 
            // pbCancelados
            // 
            this.pbCancelados.Image = global::Vista.Properties.Resources.Trabajo_cancelado;
            this.pbCancelados.Location = new System.Drawing.Point(29, 5);
            this.pbCancelados.Name = "pbCancelados";
            this.pbCancelados.Size = new System.Drawing.Size(67, 67);
            this.pbCancelados.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbCancelados.TabIndex = 1;
            this.pbCancelados.TabStop = false;
            // 
            // lblPendientes
            // 
            this.lblPendientes.AutoSize = true;
            this.lblPendientes.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPendientes.Location = new System.Drawing.Point(110, 18);
            this.lblPendientes.Name = "lblPendientes";
            this.lblPendientes.Size = new System.Drawing.Size(82, 19);
            this.lblPendientes.TabIndex = 2;
            this.lblPendientes.Text = "Pendientes";
            // 
            // pnlIndicador3
            // 
            this.pnlIndicador3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(186)))), ((int)(((byte)(120)))));
            this.pnlIndicador3.Controls.Add(this.pbPendientes);
            this.pnlIndicador3.Controls.Add(this.lblPendientes);
            this.pnlIndicador3.Location = new System.Drawing.Point(498, 133);
            this.pnlIndicador3.Margin = new System.Windows.Forms.Padding(2);
            this.pnlIndicador3.Name = "pnlIndicador3";
            this.pnlIndicador3.Size = new System.Drawing.Size(221, 77);
            this.pnlIndicador3.TabIndex = 6;
            // 
            // pbPendientes
            // 
            this.pbPendientes.Image = global::Vista.Properties.Resources.Reloj_Pendiente;
            this.pbPendientes.Location = new System.Drawing.Point(26, 4);
            this.pbPendientes.Name = "pbPendientes";
            this.pbPendientes.Size = new System.Drawing.Size(62, 68);
            this.pbPendientes.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbPendientes.TabIndex = 3;
            this.pbPendientes.TabStop = false;
            // 
            // lblMensajeInformativoPrincipal
            // 
            this.lblMensajeInformativoPrincipal.AutoSize = true;
            this.lblMensajeInformativoPrincipal.Font = new System.Drawing.Font("Times New Roman", 26F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMensajeInformativoPrincipal.Location = new System.Drawing.Point(25, 41);
            this.lblMensajeInformativoPrincipal.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblMensajeInformativoPrincipal.Name = "lblMensajeInformativoPrincipal";
            this.lblMensajeInformativoPrincipal.Size = new System.Drawing.Size(534, 40);
            this.lblMensajeInformativoPrincipal.TabIndex = 0;
            this.lblMensajeInformativoPrincipal.Text = "Control de producción de trabajos";
            // 
            // lblRegistrados
            // 
            this.lblRegistrados.AutoSize = true;
            this.lblRegistrados.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRegistrados.Location = new System.Drawing.Point(94, 17);
            this.lblRegistrados.Name = "lblRegistrados";
            this.lblRegistrados.Size = new System.Drawing.Size(128, 19);
            this.lblRegistrados.TabIndex = 3;
            this.lblRegistrados.Text = "Total Registrados";
            // 
            // lblSubTexto
            // 
            this.lblSubTexto.AutoSize = true;
            this.lblSubTexto.Font = new System.Drawing.Font("Times New Roman", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSubTexto.Location = new System.Drawing.Point(35, 90);
            this.lblSubTexto.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblSubTexto.Name = "lblSubTexto";
            this.lblSubTexto.Size = new System.Drawing.Size(383, 21);
            this.lblSubTexto.TabIndex = 1;
            this.lblSubTexto.Text = "Seguimiento de los trabajos que están en proceso.";
            // 
            // btnLimpiar
            // 
            this.btnLimpiar.Location = new System.Drawing.Point(797, 228);
            this.btnLimpiar.Margin = new System.Windows.Forms.Padding(2);
            this.btnLimpiar.Name = "btnLimpiar";
            this.btnLimpiar.Size = new System.Drawing.Size(159, 31);
            this.btnLimpiar.TabIndex = 11;
            this.btnLimpiar.Text = "Limpiar Filtros";
            this.btnLimpiar.UseVisualStyleBackColor = true;
            this.btnLimpiar.Click += new System.EventHandler(this.btnLimpiar_Click);
            // 
            // cbEstados
            // 
            this.cbEstados.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbEstados.Font = new System.Drawing.Font("Times New Roman", 14F);
            this.cbEstados.FormattingEnabled = true;
            this.cbEstados.Items.AddRange(new object[] {
            "Todos los estados",
            "Finalizados",
            "Pendientes",
            "En Proceso"});
            this.cbEstados.Location = new System.Drawing.Point(53, 231);
            this.cbEstados.Margin = new System.Windows.Forms.Padding(2);
            this.cbEstados.Name = "cbEstados";
            this.cbEstados.Size = new System.Drawing.Size(217, 29);
            this.cbEstados.TabIndex = 9;
            this.cbEstados.SelectedIndexChanged += new System.EventHandler(this.cbEstados_SelectedIndexChanged);
            // 
            // pnlIndicador4
            // 
            this.pnlIndicador4.BackColor = System.Drawing.Color.White;
            this.pnlIndicador4.Controls.Add(this.pbTotalRegistros);
            this.pnlIndicador4.Controls.Add(this.lblRegistrados);
            this.pnlIndicador4.Location = new System.Drawing.Point(724, 133);
            this.pnlIndicador4.Margin = new System.Windows.Forms.Padding(2);
            this.pnlIndicador4.Name = "pnlIndicador4";
            this.pnlIndicador4.Size = new System.Drawing.Size(232, 77);
            this.pnlIndicador4.TabIndex = 5;
            // 
            // pbTotalRegistros
            // 
            this.pbTotalRegistros.Image = global::Vista.Properties.Resources.Total_de_trabajos;
            this.pbTotalRegistros.Location = new System.Drawing.Point(25, 5);
            this.pbTotalRegistros.Name = "pbTotalRegistros";
            this.pbTotalRegistros.Size = new System.Drawing.Size(62, 66);
            this.pbTotalRegistros.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbTotalRegistros.TabIndex = 4;
            this.pbTotalRegistros.TabStop = false;
            // 
            // txtBuscar
            // 
            this.txtBuscar.Font = new System.Drawing.Font("Times New Roman", 14F);
            this.txtBuscar.ForeColor = System.Drawing.Color.Gray;
            this.txtBuscar.Location = new System.Drawing.Point(296, 231);
            this.txtBuscar.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtBuscar.Name = "txtBuscar";
            this.txtBuscar.Size = new System.Drawing.Size(488, 29);
            this.txtBuscar.TabIndex = 13;
            this.txtBuscar.Text = "Buscar por código o nombre de cliente...";
            this.txtBuscar.TextChanged += new System.EventHandler(this.txtBuscar_TextChanged);
            this.txtBuscar.Enter += new System.EventHandler(this.txtBuscar_Enter);
            this.txtBuscar.Leave += new System.EventHandler(this.txtBuscar_Leave);
            // 
            // pnlHeader
            // 
            this.pnlHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(227)))), ((int)(((byte)(175)))));
            this.pnlHeader.Controls.Add(this.pbxBuscar);
            this.pnlHeader.Controls.Add(this.txtBuscar);
            this.pnlHeader.Controls.Add(this.pnlBarraInformativa);
            this.pnlHeader.Controls.Add(this.lblSubTexto);
            this.pnlHeader.Controls.Add(this.btnLimpiar);
            this.pnlHeader.Controls.Add(this.cbEstados);
            this.pnlHeader.Controls.Add(this.pnlIndicador4);
            this.pnlHeader.Controls.Add(this.lblMensajeInformativoPrincipal);
            this.pnlHeader.Controls.Add(this.pnlIndicador3);
            this.pnlHeader.Controls.Add(this.pnlContenedorTabla);
            this.pnlHeader.Controls.Add(this.pnlIndicador1);
            this.pnlHeader.Controls.Add(this.pnlIndicador2);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlHeader.Margin = new System.Windows.Forms.Padding(2);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Size = new System.Drawing.Size(1102, 627);
            this.pnlHeader.TabIndex = 2;
            // 
            // pbxBuscar
            // 
            this.pbxBuscar.BackColor = System.Drawing.Color.Transparent;
            this.pbxBuscar.Location = new System.Drawing.Point(758, 231);
            this.pbxBuscar.Name = "pbxBuscar";
            this.pbxBuscar.Size = new System.Drawing.Size(26, 28);
            this.pbxBuscar.TabIndex = 27;
            this.pbxBuscar.TabStop = false;
            // 
            // pnlBarraInformativa
            // 
            this.pnlBarraInformativa.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(177)))), ((int)(((byte)(114)))));
            this.pnlBarraInformativa.Controls.Add(this.lblAdministrador);
            this.pnlBarraInformativa.Controls.Add(this.pbPerfil);
            this.pnlBarraInformativa.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlBarraInformativa.Location = new System.Drawing.Point(0, 0);
            this.pnlBarraInformativa.Margin = new System.Windows.Forms.Padding(2);
            this.pnlBarraInformativa.Name = "pnlBarraInformativa";
            this.pnlBarraInformativa.Size = new System.Drawing.Size(1102, 23);
            this.pnlBarraInformativa.TabIndex = 12;
            // 
            // lblAdministrador
            // 
            this.lblAdministrador.AutoSize = true;
            this.lblAdministrador.Font = new System.Drawing.Font("Times New Roman", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAdministrador.Location = new System.Drawing.Point(998, 4);
            this.lblAdministrador.Name = "lblAdministrador";
            this.lblAdministrador.Size = new System.Drawing.Size(53, 14);
            this.lblAdministrador.TabIndex = 27;
            this.lblAdministrador.Text = "Secretario";
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
            // btnMaterialUtilizado
            // 
            this.btnMaterialUtilizado.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnMaterialUtilizado.BackColor = System.Drawing.Color.SandyBrown;
            this.btnMaterialUtilizado.FlatAppearance.BorderSize = 0;
            this.btnMaterialUtilizado.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMaterialUtilizado.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMaterialUtilizado.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnMaterialUtilizado.Location = new System.Drawing.Point(809, 284);
            this.btnMaterialUtilizado.Name = "btnMaterialUtilizado";
            this.btnMaterialUtilizado.Size = new System.Drawing.Size(219, 40);
            this.btnMaterialUtilizado.TabIndex = 5;
            this.btnMaterialUtilizado.Text = "Gestion de Material Utilizado";
            this.btnMaterialUtilizado.UseVisualStyleBackColor = false;
            this.btnMaterialUtilizado.Click += new System.EventHandler(this.btnMaterialUtilizado_Click);
            // 
            // frmProduccionSecretario
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1102, 627);
            this.Controls.Add(this.pnlHeader);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmProduccionSecretario";
            this.Text = "frmProduccionSecretario";
            this.Load += new System.EventHandler(this.frmProduccionSecretario_Load);
            this.pnlIndicador2.ResumeLayout(false);
            this.pnlIndicador2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbFinalizados)).EndInit();
            this.pnlContenedorTabla.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvProduccion)).EndInit();
            this.pnlIndicador1.ResumeLayout(false);
            this.pnlIndicador1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbCancelados)).EndInit();
            this.pnlIndicador3.ResumeLayout(false);
            this.pnlIndicador3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbPendientes)).EndInit();
            this.pnlIndicador4.ResumeLayout(false);
            this.pnlIndicador4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbTotalRegistros)).EndInit();
            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbxBuscar)).EndInit();
            this.pnlBarraInformativa.ResumeLayout(false);
            this.pnlBarraInformativa.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbPerfil)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblFinalizados;
        private System.Windows.Forms.Panel pnlIndicador2;
        private System.Windows.Forms.PictureBox pbFinalizados;
        private System.Windows.Forms.PictureBox pbCancelados;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel pnlContenedorTabla;
        private System.Windows.Forms.Panel pnlIndicador1;
        private System.Windows.Forms.PictureBox pbPendientes;
        private System.Windows.Forms.Label lblPendientes;
        private System.Windows.Forms.Panel pnlIndicador3;
        private System.Windows.Forms.PictureBox pbTotalRegistros;
        private System.Windows.Forms.Label lblMensajeInformativoPrincipal;
        private System.Windows.Forms.Label lblRegistrados;
        private System.Windows.Forms.Label lblSubTexto;
        private System.Windows.Forms.Button btnLimpiar;
        private System.Windows.Forms.ComboBox cbEstados;
        private System.Windows.Forms.Panel pnlIndicador4;
        private System.Windows.Forms.PictureBox pbPerfil;
        private System.Windows.Forms.PictureBox pbxBuscar;
        private System.Windows.Forms.TextBox txtBuscar;
        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Panel pnlBarraInformativa;
        private System.Windows.Forms.Button btnEditar;
        private System.Windows.Forms.Label lblAdministrador;
        private System.Windows.Forms.DataGridView dgvProduccion;
        private System.Windows.Forms.Button btnMaterialUtilizado;
    }
}