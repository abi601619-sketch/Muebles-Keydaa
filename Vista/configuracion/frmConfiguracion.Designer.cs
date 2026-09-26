namespace Vista.configuracion
{
    partial class frmConfiguracion
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
            this.lblUsuarios = new System.Windows.Forms.Label();
            this.lblSubTexto = new System.Windows.Forms.Label();
            this.lblMensajeInformativoPrincipal = new System.Windows.Forms.Label();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.pnlPedidosRecientes = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pnlInfo = new System.Windows.Forms.Panel();
            this.txtDireccionEmpresa = new System.Windows.Forms.TextBox();
            this.txtCorreoEmpresa = new System.Windows.Forms.TextBox();
            this.txtTelefonoEmpresa = new System.Windows.Forms.TextBox();
            this.txtNombreEmpresa = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lblNombreEmpresa = new System.Windows.Forms.Label();
            this.pnlLogo = new System.Windows.Forms.Panel();
            this.lblAdministrador = new System.Windows.Forms.Label();
            this.pnlBarraInformativa = new System.Windows.Forms.Panel();
            this.pnlContenedor = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label5 = new System.Windows.Forms.Label();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.pbPerfil = new System.Windows.Forms.PictureBox();
            this.btnGuardarCambioInformacion = new System.Windows.Forms.Button();
            this.btnGuardarLogo = new System.Windows.Forms.Button();
            this.picLogo = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.pnlPedidosRecientes.SuspendLayout();
            this.panel1.SuspendLayout();
            this.pnlInfo.SuspendLayout();
            this.pnlLogo.SuspendLayout();
            this.pnlBarraInformativa.SuspendLayout();
            this.pnlContenedor.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbPerfil)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picLogo)).BeginInit();
            this.SuspendLayout();
            // 
            // lblUsuarios
            // 
            this.lblUsuarios.AutoSize = true;
            this.lblUsuarios.Font = new System.Drawing.Font("Times New Roman", 28F, System.Drawing.FontStyle.Bold);
            this.lblUsuarios.ForeColor = System.Drawing.Color.White;
            this.lblUsuarios.Location = new System.Drawing.Point(19, 5);
            this.lblUsuarios.Name = "lblUsuarios";
            this.lblUsuarios.Size = new System.Drawing.Size(473, 43);
            this.lblUsuarios.TabIndex = 1;
            this.lblUsuarios.Text = "Información de la Empresa.";
            // 
            // lblSubTexto
            // 
            this.lblSubTexto.AutoSize = true;
            this.lblSubTexto.Font = new System.Drawing.Font("Times New Roman", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSubTexto.Location = new System.Drawing.Point(108, 77);
            this.lblSubTexto.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblSubTexto.Name = "lblSubTexto";
            this.lblSubTexto.Size = new System.Drawing.Size(265, 21);
            this.lblSubTexto.TabIndex = 3;
            this.lblSubTexto.Text = "Información general de la empresa";
            // 
            // lblMensajeInformativoPrincipal
            // 
            this.lblMensajeInformativoPrincipal.AutoSize = true;
            this.lblMensajeInformativoPrincipal.Font = new System.Drawing.Font("Times New Roman", 30F, System.Drawing.FontStyle.Bold);
            this.lblMensajeInformativoPrincipal.Location = new System.Drawing.Point(100, 32);
            this.lblMensajeInformativoPrincipal.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblMensajeInformativoPrincipal.Name = "lblMensajeInformativoPrincipal";
            this.lblMensajeInformativoPrincipal.Size = new System.Drawing.Size(518, 45);
            this.lblMensajeInformativoPrincipal.TabIndex = 2;
            this.lblMensajeInformativoPrincipal.Text = "Configuración de la Empresa";
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // pnlPedidosRecientes
            // 
            this.pnlPedidosRecientes.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlPedidosRecientes.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(213)))), ((int)(((byte)(153)))), ((int)(((byte)(105)))));
            this.pnlPedidosRecientes.Controls.Add(this.panel1);
            this.pnlPedidosRecientes.Controls.Add(this.lblUsuarios);
            this.pnlPedidosRecientes.Location = new System.Drawing.Point(25, 124);
            this.pnlPedidosRecientes.Margin = new System.Windows.Forms.Padding(2);
            this.pnlPedidosRecientes.Name = "pnlPedidosRecientes";
            this.pnlPedidosRecientes.Size = new System.Drawing.Size(1025, 472);
            this.pnlPedidosRecientes.TabIndex = 8;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.pnlInfo);
            this.panel1.Controls.Add(this.pnlLogo);
            this.panel1.Location = new System.Drawing.Point(27, 63);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(973, 385);
            this.panel1.TabIndex = 2;
            // 
            // pnlInfo
            // 
            this.pnlInfo.BackColor = System.Drawing.Color.WhiteSmoke;
            this.pnlInfo.Controls.Add(this.txtDireccionEmpresa);
            this.pnlInfo.Controls.Add(this.txtCorreoEmpresa);
            this.pnlInfo.Controls.Add(this.txtTelefonoEmpresa);
            this.pnlInfo.Controls.Add(this.txtNombreEmpresa);
            this.pnlInfo.Controls.Add(this.label4);
            this.pnlInfo.Controls.Add(this.btnGuardarCambioInformacion);
            this.pnlInfo.Controls.Add(this.label3);
            this.pnlInfo.Controls.Add(this.label2);
            this.pnlInfo.Controls.Add(this.label1);
            this.pnlInfo.Controls.Add(this.lblNombreEmpresa);
            this.pnlInfo.Location = new System.Drawing.Point(415, 16);
            this.pnlInfo.Name = "pnlInfo";
            this.pnlInfo.Size = new System.Drawing.Size(533, 347);
            this.pnlInfo.TabIndex = 1;
            // 
            // txtDireccionEmpresa
            // 
            this.txtDireccionEmpresa.Location = new System.Drawing.Point(216, 203);
            this.txtDireccionEmpresa.Multiline = true;
            this.txtDireccionEmpresa.Name = "txtDireccionEmpresa";
            this.txtDireccionEmpresa.Size = new System.Drawing.Size(293, 47);
            this.txtDireccionEmpresa.TabIndex = 18;
            this.txtDireccionEmpresa.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtDireccionEmpresa_KeyPress);
            // 
            // txtCorreoEmpresa
            // 
            this.txtCorreoEmpresa.Location = new System.Drawing.Point(216, 156);
            this.txtCorreoEmpresa.Name = "txtCorreoEmpresa";
            this.txtCorreoEmpresa.Size = new System.Drawing.Size(293, 20);
            this.txtCorreoEmpresa.TabIndex = 17;
            this.txtCorreoEmpresa.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCorreoEmpresa_KeyPress);
            // 
            // txtTelefonoEmpresa
            // 
            this.txtTelefonoEmpresa.Location = new System.Drawing.Point(216, 113);
            this.txtTelefonoEmpresa.Name = "txtTelefonoEmpresa";
            this.txtTelefonoEmpresa.Size = new System.Drawing.Size(293, 20);
            this.txtTelefonoEmpresa.TabIndex = 16;
            this.txtTelefonoEmpresa.TextChanged += new System.EventHandler(this.txtTelefonoEmpresa_TextChanged);
            this.txtTelefonoEmpresa.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtTelefonoEmpresa_KeyPress);
            // 
            // txtNombreEmpresa
            // 
            this.txtNombreEmpresa.Location = new System.Drawing.Point(216, 67);
            this.txtNombreEmpresa.Name = "txtNombreEmpresa";
            this.txtNombreEmpresa.Size = new System.Drawing.Size(293, 20);
            this.txtNombreEmpresa.TabIndex = 15;
            this.txtNombreEmpresa.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtNombreEmpresa_KeyPress);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Times New Roman", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(75, 14);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(413, 21);
            this.label4.TabIndex = 14;
            this.label4.Text = "Información sobre la empresa registrada en el sistema.";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Times New Roman", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(19, 213);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(86, 21);
            this.label3.TabIndex = 13;
            this.label3.Text = "Dirección:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Times New Roman", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(19, 156);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(154, 21);
            this.label2.TabIndex = 12;
            this.label2.Text = "Correo eléctronico:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(19, 111);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(78, 21);
            this.label1.TabIndex = 11;
            this.label1.Text = "Tléfono: ";
            // 
            // lblNombreEmpresa
            // 
            this.lblNombreEmpresa.AutoSize = true;
            this.lblNombreEmpresa.Font = new System.Drawing.Font("Times New Roman", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNombreEmpresa.Location = new System.Drawing.Point(19, 68);
            this.lblNombreEmpresa.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblNombreEmpresa.Name = "lblNombreEmpresa";
            this.lblNombreEmpresa.Size = new System.Drawing.Size(190, 21);
            this.lblNombreEmpresa.TabIndex = 10;
            this.lblNombreEmpresa.Text = "Nombre de la Empresa: ";
            // 
            // pnlLogo
            // 
            this.pnlLogo.BackColor = System.Drawing.Color.WhiteSmoke;
            this.pnlLogo.Controls.Add(this.panel2);
            this.pnlLogo.Controls.Add(this.btnGuardarLogo);
            this.pnlLogo.Controls.Add(this.picLogo);
            this.pnlLogo.Location = new System.Drawing.Point(20, 16);
            this.pnlLogo.Name = "pnlLogo";
            this.pnlLogo.Size = new System.Drawing.Size(367, 347);
            this.pnlLogo.TabIndex = 0;
            // 
            // lblAdministrador
            // 
            this.lblAdministrador.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblAdministrador.AutoSize = true;
            this.lblAdministrador.Font = new System.Drawing.Font("Times New Roman", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAdministrador.Location = new System.Drawing.Point(1012, 4);
            this.lblAdministrador.Name = "lblAdministrador";
            this.lblAdministrador.Size = new System.Drawing.Size(38, 14);
            this.lblAdministrador.TabIndex = 29;
            this.lblAdministrador.Text = "Admin";
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
            this.pnlBarraInformativa.TabIndex = 9;
            // 
            // pnlContenedor
            // 
            this.pnlContenedor.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(221)))), ((int)(((byte)(175)))));
            this.pnlContenedor.Controls.Add(this.pictureBox3);
            this.pnlContenedor.Controls.Add(this.pnlBarraInformativa);
            this.pnlContenedor.Controls.Add(this.pnlPedidosRecientes);
            this.pnlContenedor.Controls.Add(this.lblSubTexto);
            this.pnlContenedor.Controls.Add(this.lblMensajeInformativoPrincipal);
            this.pnlContenedor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlContenedor.Location = new System.Drawing.Point(0, 0);
            this.pnlContenedor.Margin = new System.Windows.Forms.Padding(2);
            this.pnlContenedor.Name = "pnlContenedor";
            this.pnlContenedor.Size = new System.Drawing.Size(1102, 627);
            this.pnlContenedor.TabIndex = 7;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(213)))), ((int)(((byte)(153)))), ((int)(((byte)(105)))));
            this.panel2.Controls.Add(this.label5);
            this.panel2.Location = new System.Drawing.Point(0, 1);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(366, 31);
            this.panel2.TabIndex = 13;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Times New Roman", 16F, System.Drawing.FontStyle.Bold);
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(90, 3);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(196, 25);
            this.label5.TabIndex = 3;
            this.label5.Text = "Logo de la empresa";
            // 
            // pictureBox3
            // 
            this.pictureBox3.Image = global::Vista.Properties.Resources.ajuste__1_;
            this.pictureBox3.Location = new System.Drawing.Point(37, 26);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(63, 83);
            this.pictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox3.TabIndex = 10;
            this.pictureBox3.TabStop = false;
            // 
            // pbPerfil
            // 
            this.pbPerfil.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pbPerfil.Image = global::Vista.Properties.Resources.Imagen_perfil_2;
            this.pbPerfil.Location = new System.Drawing.Point(1053, -3);
            this.pbPerfil.Name = "pbPerfil";
            this.pbPerfil.Size = new System.Drawing.Size(26, 26);
            this.pbPerfil.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbPerfil.TabIndex = 10;
            this.pbPerfil.TabStop = false;
            // 
            // btnGuardarCambioInformacion
            // 
            this.btnGuardarCambioInformacion.BackColor = System.Drawing.Color.LightGreen;
            this.btnGuardarCambioInformacion.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.errorProvider1.SetIconAlignment(this.btnGuardarCambioInformacion, System.Windows.Forms.ErrorIconAlignment.TopLeft);
            this.btnGuardarCambioInformacion.Image = global::Vista.Properties.Resources.curriculum;
            this.btnGuardarCambioInformacion.Location = new System.Drawing.Point(116, 273);
            this.btnGuardarCambioInformacion.Name = "btnGuardarCambioInformacion";
            this.btnGuardarCambioInformacion.Padding = new System.Windows.Forms.Padding(50, 0, 0, 0);
            this.btnGuardarCambioInformacion.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.btnGuardarCambioInformacion.Size = new System.Drawing.Size(289, 47);
            this.btnGuardarCambioInformacion.TabIndex = 13;
            this.btnGuardarCambioInformacion.Text = "Guardar Cambios";
            this.btnGuardarCambioInformacion.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnGuardarCambioInformacion.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnGuardarCambioInformacion.UseVisualStyleBackColor = false;
            this.btnGuardarCambioInformacion.Click += new System.EventHandler(this.btnGuardarCambioInformacion_Click);
            // 
            // btnGuardarLogo
            // 
            this.btnGuardarLogo.BackColor = System.Drawing.Color.Transparent;
            this.btnGuardarLogo.FlatAppearance.BorderSize = 2;
            this.btnGuardarLogo.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnGuardarLogo.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGuardarLogo.Image = global::Vista.Properties.Resources.galeria_de_imagenes__1_;
            this.btnGuardarLogo.Location = new System.Drawing.Point(40, 293);
            this.btnGuardarLogo.Name = "btnGuardarLogo";
            this.btnGuardarLogo.Size = new System.Drawing.Size(291, 38);
            this.btnGuardarLogo.TabIndex = 12;
            this.btnGuardarLogo.Text = "Cambiar Logo";
            this.btnGuardarLogo.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnGuardarLogo.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnGuardarLogo.UseVisualStyleBackColor = false;
            this.btnGuardarLogo.Click += new System.EventHandler(this.btnGuardarLogo_Click);
            // 
            // picLogo
            // 
            this.picLogo.Location = new System.Drawing.Point(36, 55);
            this.picLogo.Name = "picLogo";
            this.picLogo.Size = new System.Drawing.Size(296, 214);
            this.picLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picLogo.TabIndex = 11;
            this.picLogo.TabStop = false;
            // 
            // frmConfiguracion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1102, 627);
            this.Controls.Add(this.pnlContenedor);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmConfiguracion";
            this.Text = "frmConfiguracion";
            this.Load += new System.EventHandler(this.frmConfiguracion_Load);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.pnlPedidosRecientes.ResumeLayout(false);
            this.pnlPedidosRecientes.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.pnlInfo.ResumeLayout(false);
            this.pnlInfo.PerformLayout();
            this.pnlLogo.ResumeLayout(false);
            this.pnlBarraInformativa.ResumeLayout(false);
            this.pnlBarraInformativa.PerformLayout();
            this.pnlContenedor.ResumeLayout(false);
            this.pnlContenedor.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbPerfil)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picLogo)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label lblUsuarios;
        private System.Windows.Forms.Label lblSubTexto;
        private System.Windows.Forms.Label lblMensajeInformativoPrincipal;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.Panel pnlContenedor;
        private System.Windows.Forms.Panel pnlBarraInformativa;
        private System.Windows.Forms.Label lblAdministrador;
        private System.Windows.Forms.PictureBox pbPerfil;
        private System.Windows.Forms.Panel pnlPedidosRecientes;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel pnlInfo;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblNombreEmpresa;
        private System.Windows.Forms.Panel pnlLogo;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtDireccionEmpresa;
        private System.Windows.Forms.TextBox txtCorreoEmpresa;
        private System.Windows.Forms.TextBox txtTelefonoEmpresa;
        private System.Windows.Forms.TextBox txtNombreEmpresa;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnGuardarCambioInformacion;
        private System.Windows.Forms.Button btnGuardarLogo;
        private System.Windows.Forms.PictureBox picLogo;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.PictureBox pictureBox3;
    }
}