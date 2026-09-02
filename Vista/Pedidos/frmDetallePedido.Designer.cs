namespace Vista.Pedidos
{
    partial class frmDetallePedido
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
            this.btnAgregar = new System.Windows.Forms.Button();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.gbMedidas = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lblCm = new System.Windows.Forms.Label();
            this.lblUcm = new System.Windows.Forms.Label();
            this.txtAlto = new System.Windows.Forms.TextBox();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.txtLargo = new System.Windows.Forms.TextBox();
            this.lblAlto = new System.Windows.Forms.Label();
            this.lblAncho = new System.Windows.Forms.Label();
            this.lblLargo = new System.Windows.Forms.Label();
            this.gbDetallePedido = new System.Windows.Forms.GroupBox();
            this.lblDetallePedido = new System.Windows.Forms.Label();
            this.pnlBuscarClienteSuperior = new System.Windows.Forms.Panel();
            this.btnSalir = new System.Windows.Forms.Button();
            this.gbMedidas.SuspendLayout();
            this.gbDetallePedido.SuspendLayout();
            this.pnlBuscarClienteSuperior.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnAgregar
            // 
            this.btnAgregar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.btnAgregar.FlatAppearance.BorderSize = 0;
            this.btnAgregar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAgregar.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Bold);
            this.btnAgregar.Location = new System.Drawing.Point(253, 198);
            this.btnAgregar.Name = "btnAgregar";
            this.btnAgregar.Size = new System.Drawing.Size(121, 27);
            this.btnAgregar.TabIndex = 12;
            this.btnAgregar.Text = "Agregar";
            this.btnAgregar.UseVisualStyleBackColor = false;
            this.btnAgregar.Click += new System.EventHandler(this.btnAgregar_Click);
            // 
            // btnCancelar
            // 
            this.btnCancelar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.btnCancelar.FlatAppearance.BorderSize = 0;
            this.btnCancelar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancelar.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancelar.Location = new System.Drawing.Point(392, 198);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(121, 27);
            this.btnCancelar.TabIndex = 11;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = false;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // gbMedidas
            // 
            this.gbMedidas.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(218)))), ((int)(((byte)(180)))));
            this.gbMedidas.Controls.Add(this.label1);
            this.gbMedidas.Controls.Add(this.lblCm);
            this.gbMedidas.Controls.Add(this.lblUcm);
            this.gbMedidas.Controls.Add(this.txtAlto);
            this.gbMedidas.Controls.Add(this.textBox3);
            this.gbMedidas.Controls.Add(this.txtLargo);
            this.gbMedidas.Controls.Add(this.lblAlto);
            this.gbMedidas.Controls.Add(this.lblAncho);
            this.gbMedidas.Controls.Add(this.lblLargo);
            this.gbMedidas.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Bold);
            this.gbMedidas.Location = new System.Drawing.Point(18, 42);
            this.gbMedidas.Name = "gbMedidas";
            this.gbMedidas.Size = new System.Drawing.Size(469, 70);
            this.gbMedidas.TabIndex = 1;
            this.gbMedidas.TabStop = false;
            this.gbMedidas.Text = "Medidas";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(434, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(26, 17);
            this.label1.TabIndex = 8;
            this.label1.Text = "cm";
            // 
            // lblCm
            // 
            this.lblCm.AutoSize = true;
            this.lblCm.Location = new System.Drawing.Point(288, 29);
            this.lblCm.Name = "lblCm";
            this.lblCm.Size = new System.Drawing.Size(26, 17);
            this.lblCm.TabIndex = 7;
            this.lblCm.Text = "cm";
            // 
            // lblUcm
            // 
            this.lblUcm.AutoSize = true;
            this.lblUcm.Location = new System.Drawing.Point(139, 30);
            this.lblUcm.Name = "lblUcm";
            this.lblUcm.Size = new System.Drawing.Size(26, 17);
            this.lblUcm.TabIndex = 6;
            this.lblUcm.Text = "cm";
            // 
            // txtAlto
            // 
            this.txtAlto.Location = new System.Drawing.Point(372, 27);
            this.txtAlto.Name = "txtAlto";
            this.txtAlto.Size = new System.Drawing.Size(53, 23);
            this.txtAlto.TabIndex = 5;
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(226, 28);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(53, 23);
            this.textBox3.TabIndex = 4;
            // 
            // txtLargo
            // 
            this.txtLargo.Location = new System.Drawing.Point(80, 27);
            this.txtLargo.Name = "txtLargo";
            this.txtLargo.Size = new System.Drawing.Size(53, 23);
            this.txtLargo.TabIndex = 3;
            // 
            // lblAlto
            // 
            this.lblAlto.AutoSize = true;
            this.lblAlto.Location = new System.Drawing.Point(323, 28);
            this.lblAlto.Name = "lblAlto";
            this.lblAlto.Size = new System.Drawing.Size(43, 17);
            this.lblAlto.TabIndex = 2;
            this.lblAlto.Text = "Alto :";
            // 
            // lblAncho
            // 
            this.lblAncho.AutoSize = true;
            this.lblAncho.Location = new System.Drawing.Point(168, 29);
            this.lblAncho.Name = "lblAncho";
            this.lblAncho.Size = new System.Drawing.Size(56, 17);
            this.lblAncho.TabIndex = 1;
            this.lblAncho.Text = "Ancho :";
            // 
            // lblLargo
            // 
            this.lblLargo.AutoSize = true;
            this.lblLargo.Location = new System.Drawing.Point(21, 30);
            this.lblLargo.Name = "lblLargo";
            this.lblLargo.Size = new System.Drawing.Size(53, 17);
            this.lblLargo.TabIndex = 0;
            this.lblLargo.Text = "Largo :";
            // 
            // gbDetallePedido
            // 
            this.gbDetallePedido.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(230)))), ((int)(((byte)(185)))));
            this.gbDetallePedido.Controls.Add(this.gbMedidas);
            this.gbDetallePedido.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbDetallePedido.Location = new System.Drawing.Point(12, 54);
            this.gbDetallePedido.Name = "gbDetallePedido";
            this.gbDetallePedido.Size = new System.Drawing.Size(513, 130);
            this.gbDetallePedido.TabIndex = 10;
            this.gbDetallePedido.TabStop = false;
            this.gbDetallePedido.Text = "Detalle del Pedido";
            // 
            // lblDetallePedido
            // 
            this.lblDetallePedido.AutoSize = true;
            this.lblDetallePedido.BackColor = System.Drawing.Color.Transparent;
            this.lblDetallePedido.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDetallePedido.ForeColor = System.Drawing.Color.White;
            this.lblDetallePedido.Location = new System.Drawing.Point(15, 9);
            this.lblDetallePedido.Name = "lblDetallePedido";
            this.lblDetallePedido.Size = new System.Drawing.Size(112, 15);
            this.lblDetallePedido.TabIndex = 0;
            this.lblDetallePedido.Text = "DETALLE PEDIDO";
            // 
            // pnlBuscarClienteSuperior
            // 
            this.pnlBuscarClienteSuperior.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(129)))), ((int)(((byte)(62)))), ((int)(((byte)(36)))));
            this.pnlBuscarClienteSuperior.Controls.Add(this.btnSalir);
            this.pnlBuscarClienteSuperior.Controls.Add(this.lblDetallePedido);
            this.pnlBuscarClienteSuperior.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlBuscarClienteSuperior.Font = new System.Drawing.Font("Times New Roman", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnlBuscarClienteSuperior.Location = new System.Drawing.Point(0, 0);
            this.pnlBuscarClienteSuperior.Name = "pnlBuscarClienteSuperior";
            this.pnlBuscarClienteSuperior.Size = new System.Drawing.Size(545, 38);
            this.pnlBuscarClienteSuperior.TabIndex = 9;
            // 
            // btnSalir
            // 
            this.btnSalir.Image = global::Vista.Properties.Resources.Cerrar16px;
            this.btnSalir.Location = new System.Drawing.Point(493, 6);
            this.btnSalir.Name = "btnSalir";
            this.btnSalir.Size = new System.Drawing.Size(32, 25);
            this.btnSalir.TabIndex = 1;
            this.btnSalir.UseVisualStyleBackColor = true;
            this.btnSalir.Click += new System.EventHandler(this.btnSalir_Click);
            // 
            // frmDetallePedido
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(221)))), ((int)(((byte)(175)))));
            this.ClientSize = new System.Drawing.Size(545, 237);
            this.Controls.Add(this.btnAgregar);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.gbDetallePedido);
            this.Controls.Add(this.pnlBuscarClienteSuperior);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmDetallePedido";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmDetallePedido";
            this.gbMedidas.ResumeLayout(false);
            this.gbMedidas.PerformLayout();
            this.gbDetallePedido.ResumeLayout(false);
            this.pnlBuscarClienteSuperior.ResumeLayout(false);
            this.pnlBuscarClienteSuperior.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnAgregar;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.GroupBox gbMedidas;
        private System.Windows.Forms.TextBox txtAlto;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.TextBox txtLargo;
        private System.Windows.Forms.Label lblAlto;
        private System.Windows.Forms.Label lblAncho;
        private System.Windows.Forms.Label lblLargo;
        private System.Windows.Forms.GroupBox gbDetallePedido;
        private System.Windows.Forms.Button btnSalir;
        private System.Windows.Forms.Label lblDetallePedido;
        private System.Windows.Forms.Panel pnlBuscarClienteSuperior;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblCm;
        private System.Windows.Forms.Label lblUcm;
    }
}