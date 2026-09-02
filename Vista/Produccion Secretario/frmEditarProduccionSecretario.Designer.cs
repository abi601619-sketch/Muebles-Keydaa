namespace Vista.Produccion_Secretario
{
    partial class frmEditarProduccionSecretario
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
            this.btnSalir = new System.Windows.Forms.Button();
            this.lblDetalleProduccion = new System.Windows.Forms.Label();
            this.pnlBuscarClienteSuperior = new System.Windows.Forms.Panel();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.btnGuardarCambios = new System.Windows.Forms.Button();
            this.lblCodigoProduccion = new System.Windows.Forms.Label();
            this.lblMuebleRealizar = new System.Windows.Forms.Label();
            this.lblCliente = new System.Windows.Forms.Label();
            this.lblTextoEstado = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lblProgreso = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtCodigoProduccion = new System.Windows.Forms.TextBox();
            this.txtMuebleRealizar = new System.Windows.Forms.TextBox();
            this.nupProgreso = new System.Windows.Forms.NumericUpDown();
            this.dtpFechaEntrega = new System.Windows.Forms.DateTimePicker();
            this.txtObservaciones = new System.Windows.Forms.TextBox();
            this.txtCliente = new System.Windows.Forms.TextBox();
            this.lblEstado = new System.Windows.Forms.Label();
            this.pnlBuscarClienteSuperior.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nupProgreso)).BeginInit();
            this.SuspendLayout();
            // 
            // btnSalir
            // 
            this.btnSalir.Image = global::Vista.Properties.Resources.Cerrar16px;
            this.btnSalir.Location = new System.Drawing.Point(641, 6);
            this.btnSalir.Name = "btnSalir";
            this.btnSalir.Size = new System.Drawing.Size(32, 25);
            this.btnSalir.TabIndex = 1;
            this.btnSalir.UseVisualStyleBackColor = true;
            this.btnSalir.Click += new System.EventHandler(this.btnSalir_Click);
            // 
            // lblDetalleProduccion
            // 
            this.lblDetalleProduccion.AutoSize = true;
            this.lblDetalleProduccion.BackColor = System.Drawing.Color.Transparent;
            this.lblDetalleProduccion.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDetalleProduccion.ForeColor = System.Drawing.Color.White;
            this.lblDetalleProduccion.Location = new System.Drawing.Point(15, 9);
            this.lblDetalleProduccion.Name = "lblDetalleProduccion";
            this.lblDetalleProduccion.Size = new System.Drawing.Size(140, 15);
            this.lblDetalleProduccion.TabIndex = 0;
            this.lblDetalleProduccion.Text = "EDITAR PRODUCCION";
            // 
            // pnlBuscarClienteSuperior
            // 
            this.pnlBuscarClienteSuperior.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(143)))), ((int)(((byte)(120)))), ((int)(((byte)(103)))));
            this.pnlBuscarClienteSuperior.Controls.Add(this.btnSalir);
            this.pnlBuscarClienteSuperior.Controls.Add(this.lblDetalleProduccion);
            this.pnlBuscarClienteSuperior.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlBuscarClienteSuperior.Font = new System.Drawing.Font("Times New Roman", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnlBuscarClienteSuperior.Location = new System.Drawing.Point(0, 0);
            this.pnlBuscarClienteSuperior.Name = "pnlBuscarClienteSuperior";
            this.pnlBuscarClienteSuperior.Size = new System.Drawing.Size(687, 38);
            this.pnlBuscarClienteSuperior.TabIndex = 13;
            // 
            // btnCancelar
            // 
            this.btnCancelar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.btnCancelar.FlatAppearance.BorderSize = 0;
            this.btnCancelar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancelar.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancelar.Location = new System.Drawing.Point(367, 250);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(121, 27);
            this.btnCancelar.TabIndex = 15;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = false;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // btnGuardarCambios
            // 
            this.btnGuardarCambios.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.btnGuardarCambios.FlatAppearance.BorderSize = 0;
            this.btnGuardarCambios.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGuardarCambios.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Bold);
            this.btnGuardarCambios.Location = new System.Drawing.Point(496, 250);
            this.btnGuardarCambios.Name = "btnGuardarCambios";
            this.btnGuardarCambios.Size = new System.Drawing.Size(177, 27);
            this.btnGuardarCambios.TabIndex = 16;
            this.btnGuardarCambios.Text = "Guardar Cambios";
            this.btnGuardarCambios.UseVisualStyleBackColor = false;
            this.btnGuardarCambios.Click += new System.EventHandler(this.btnGuardarCambios_Click);
            // 
            // lblCodigoProduccion
            // 
            this.lblCodigoProduccion.AutoSize = true;
            this.lblCodigoProduccion.Font = new System.Drawing.Font("Times New Roman", 10F);
            this.lblCodigoProduccion.Location = new System.Drawing.Point(19, 64);
            this.lblCodigoProduccion.Name = "lblCodigoProduccion";
            this.lblCodigoProduccion.Size = new System.Drawing.Size(140, 16);
            this.lblCodigoProduccion.TabIndex = 17;
            this.lblCodigoProduccion.Text = "Codigó de Producción :";
            // 
            // lblMuebleRealizar
            // 
            this.lblMuebleRealizar.AutoSize = true;
            this.lblMuebleRealizar.Font = new System.Drawing.Font("Times New Roman", 10F);
            this.lblMuebleRealizar.Location = new System.Drawing.Point(20, 104);
            this.lblMuebleRealizar.Name = "lblMuebleRealizar";
            this.lblMuebleRealizar.Size = new System.Drawing.Size(103, 16);
            this.lblMuebleRealizar.TabIndex = 18;
            this.lblMuebleRealizar.Text = "Mueble a relizar :";
            // 
            // lblCliente
            // 
            this.lblCliente.AutoSize = true;
            this.lblCliente.Font = new System.Drawing.Font("Times New Roman", 10F);
            this.lblCliente.Location = new System.Drawing.Point(20, 141);
            this.lblCliente.Name = "lblCliente";
            this.lblCliente.Size = new System.Drawing.Size(52, 16);
            this.lblCliente.TabIndex = 19;
            this.lblCliente.Text = "Cliente :";
            // 
            // lblTextoEstado
            // 
            this.lblTextoEstado.AutoSize = true;
            this.lblTextoEstado.Font = new System.Drawing.Font("Times New Roman", 10F);
            this.lblTextoEstado.Location = new System.Drawing.Point(20, 175);
            this.lblTextoEstado.Name = "lblTextoEstado";
            this.lblTextoEstado.Size = new System.Drawing.Size(52, 16);
            this.lblTextoEstado.TabIndex = 20;
            this.lblTextoEstado.Text = "Estado :";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 10F);
            this.label1.Location = new System.Drawing.Point(348, 152);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(97, 16);
            this.label1.TabIndex = 21;
            this.label1.Text = "Observaciones :";
            // 
            // lblProgreso
            // 
            this.lblProgreso.AutoSize = true;
            this.lblProgreso.Font = new System.Drawing.Font("Times New Roman", 10F);
            this.lblProgreso.Location = new System.Drawing.Point(348, 69);
            this.lblProgreso.Name = "lblProgreso";
            this.lblProgreso.Size = new System.Drawing.Size(65, 16);
            this.lblProgreso.TabIndex = 22;
            this.lblProgreso.Text = "Progreso :";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Times New Roman", 10F);
            this.label3.Location = new System.Drawing.Point(348, 120);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(112, 16);
            this.label3.TabIndex = 23;
            this.label3.Text = "Fecha de Entrega :";
            // 
            // txtCodigoProduccion
            // 
            this.txtCodigoProduccion.Location = new System.Drawing.Point(165, 63);
            this.txtCodigoProduccion.Name = "txtCodigoProduccion";
            this.txtCodigoProduccion.ReadOnly = true;
            this.txtCodigoProduccion.Size = new System.Drawing.Size(121, 20);
            this.txtCodigoProduccion.TabIndex = 24;
            // 
            // txtMuebleRealizar
            // 
            this.txtMuebleRealizar.Location = new System.Drawing.Point(129, 104);
            this.txtMuebleRealizar.Name = "txtMuebleRealizar";
            this.txtMuebleRealizar.Size = new System.Drawing.Size(157, 20);
            this.txtMuebleRealizar.TabIndex = 25;
            // 
            // nupProgreso
            // 
            this.nupProgreso.Location = new System.Drawing.Point(430, 69);
            this.nupProgreso.Name = "nupProgreso";
            this.nupProgreso.Size = new System.Drawing.Size(94, 20);
            this.nupProgreso.TabIndex = 28;
            this.nupProgreso.ValueChanged += new System.EventHandler(this.nupProgreso_ValueChanged);
            // 
            // dtpFechaEntrega
            // 
            this.dtpFechaEntrega.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFechaEntrega.Location = new System.Drawing.Point(466, 120);
            this.dtpFechaEntrega.Name = "dtpFechaEntrega";
            this.dtpFechaEntrega.Size = new System.Drawing.Size(119, 20);
            this.dtpFechaEntrega.TabIndex = 29;
            // 
            // txtObservaciones
            // 
            this.txtObservaciones.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtObservaciones.Location = new System.Drawing.Point(357, 177);
            this.txtObservaciones.Multiline = true;
            this.txtObservaciones.Name = "txtObservaciones";
            this.txtObservaciones.Size = new System.Drawing.Size(253, 56);
            this.txtObservaciones.TabIndex = 30;
            // 
            // txtCliente
            // 
            this.txtCliente.Location = new System.Drawing.Point(129, 141);
            this.txtCliente.Name = "txtCliente";
            this.txtCliente.Size = new System.Drawing.Size(157, 20);
            this.txtCliente.TabIndex = 33;
            // 
            // lblEstado
            // 
            this.lblEstado.AutoSize = true;
            this.lblEstado.Font = new System.Drawing.Font("Times New Roman", 10F);
            this.lblEstado.Location = new System.Drawing.Point(179, 179);
            this.lblEstado.Name = "lblEstado";
            this.lblEstado.Size = new System.Drawing.Size(52, 16);
            this.lblEstado.TabIndex = 34;
            this.lblEstado.Text = "Estado :";
            // 
            // frmEditarProduccionSecretario
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(227)))), ((int)(((byte)(175)))));
            this.ClientSize = new System.Drawing.Size(687, 297);
            this.Controls.Add(this.lblEstado);
            this.Controls.Add(this.txtCliente);
            this.Controls.Add(this.txtObservaciones);
            this.Controls.Add(this.dtpFechaEntrega);
            this.Controls.Add(this.nupProgreso);
            this.Controls.Add(this.txtMuebleRealizar);
            this.Controls.Add(this.txtCodigoProduccion);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lblProgreso);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblTextoEstado);
            this.Controls.Add(this.lblCliente);
            this.Controls.Add(this.lblMuebleRealizar);
            this.Controls.Add(this.lblCodigoProduccion);
            this.Controls.Add(this.pnlBuscarClienteSuperior);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.btnGuardarCambios);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmEditarProduccionSecretario";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmEditarProduccion";
            this.pnlBuscarClienteSuperior.ResumeLayout(false);
            this.pnlBuscarClienteSuperior.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nupProgreso)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnSalir;
        private System.Windows.Forms.Label lblDetalleProduccion;
        private System.Windows.Forms.Panel pnlBuscarClienteSuperior;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.Button btnGuardarCambios;
        private System.Windows.Forms.Label lblCodigoProduccion;
        private System.Windows.Forms.Label lblMuebleRealizar;
        private System.Windows.Forms.Label lblCliente;
        private System.Windows.Forms.Label lblTextoEstado;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblProgreso;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtCodigoProduccion;
        private System.Windows.Forms.TextBox txtMuebleRealizar;
        private System.Windows.Forms.NumericUpDown nupProgreso;
        private System.Windows.Forms.DateTimePicker dtpFechaEntrega;
        private System.Windows.Forms.TextBox txtObservaciones;
        private System.Windows.Forms.TextBox txtCliente;
        private System.Windows.Forms.Label lblEstado;
    }
}