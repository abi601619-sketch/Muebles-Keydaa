namespace Vista.Recuperar_Contraseña
{
    partial class frmRecuperarClave
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
            this.btnRecuperarClave = new System.Windows.Forms.Button();
            this.lblBienvenida = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.btnVerContrasena = new System.Windows.Forms.Button();
            this.btnCodigoRecuperar = new System.Windows.Forms.Button();
            this.lblNuevaContra = new System.Windows.Forms.Label();
            this.txtConfirmarContrasena = new System.Windows.Forms.TextBox();
            this.txtCodigo = new System.Windows.Forms.TextBox();
            this.lblDireccion = new System.Windows.Forms.Label();
            this.lblCorreo = new System.Windows.Forms.Label();
            this.txtNuevaContrasena = new System.Windows.Forms.TextBox();
            this.txtCorreoRecuperacion = new System.Windows.Forms.TextBox();
            this.lblCorreoRecuperacion = new System.Windows.Forms.Label();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // btnRecuperarClave
            // 
            this.btnRecuperarClave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.btnRecuperarClave.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRecuperarClave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnRecuperarClave.Location = new System.Drawing.Point(128, 295);
            this.btnRecuperarClave.Name = "btnRecuperarClave";
            this.btnRecuperarClave.Size = new System.Drawing.Size(381, 36);
            this.btnRecuperarClave.TabIndex = 10;
            this.btnRecuperarClave.Text = "Cambiar contraseña";
            this.btnRecuperarClave.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.btnRecuperarClave.UseVisualStyleBackColor = false;
            this.btnRecuperarClave.Click += new System.EventHandler(this.btnRecuperarClave_Click);
            // 
            // lblBienvenida
            // 
            this.lblBienvenida.Font = new System.Drawing.Font("Times New Roman", 30F, System.Drawing.FontStyle.Bold);
            this.lblBienvenida.Location = new System.Drawing.Point(154, 23);
            this.lblBienvenida.Name = "lblBienvenida";
            this.lblBienvenida.Size = new System.Drawing.Size(473, 54);
            this.lblBienvenida.TabIndex = 7;
            this.lblBienvenida.Text = "¿Olvidaste tu contraseña?";
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.panel3.Controls.Add(this.btnVerContrasena);
            this.panel3.Controls.Add(this.btnCodigoRecuperar);
            this.panel3.Controls.Add(this.btnRecuperarClave);
            this.panel3.Controls.Add(this.lblNuevaContra);
            this.panel3.Controls.Add(this.txtConfirmarContrasena);
            this.panel3.Controls.Add(this.txtCodigo);
            this.panel3.Controls.Add(this.lblDireccion);
            this.panel3.Controls.Add(this.lblCorreo);
            this.panel3.Controls.Add(this.txtNuevaContrasena);
            this.panel3.Controls.Add(this.txtCorreoRecuperacion);
            this.panel3.Controls.Add(this.lblCorreoRecuperacion);
            this.panel3.Location = new System.Drawing.Point(74, 80);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(632, 359);
            this.panel3.TabIndex = 17;
            // 
            // btnVerContrasena
            // 
            this.btnVerContrasena.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btnVerContrasena.FlatAppearance.BorderSize = 0;
            this.btnVerContrasena.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnVerContrasena.Image = global::Vista.Properties.Resources.ojo__1_;
            this.btnVerContrasena.Location = new System.Drawing.Point(553, 190);
            this.btnVerContrasena.Name = "btnVerContrasena";
            this.btnVerContrasena.Size = new System.Drawing.Size(28, 17);
            this.btnVerContrasena.TabIndex = 21;
            this.btnVerContrasena.UseVisualStyleBackColor = false;
            this.btnVerContrasena.Click += new System.EventHandler(this.btnVerContrasena_Click);
            // 
            // btnCodigoRecuperar
            // 
            this.btnCodigoRecuperar.BackColor = System.Drawing.Color.Linen;
            this.btnCodigoRecuperar.Location = new System.Drawing.Point(203, 75);
            this.btnCodigoRecuperar.Name = "btnCodigoRecuperar";
            this.btnCodigoRecuperar.Size = new System.Drawing.Size(246, 31);
            this.btnCodigoRecuperar.TabIndex = 20;
            this.btnCodigoRecuperar.Text = "Enviar código";
            this.btnCodigoRecuperar.UseVisualStyleBackColor = false;
            this.btnCodigoRecuperar.Click += new System.EventHandler(this.btnCodigoRecuperar_Click);
            // 
            // lblNuevaContra
            // 
            this.lblNuevaContra.Font = new System.Drawing.Font("Times New Roman", 12F);
            this.lblNuevaContra.Location = new System.Drawing.Point(15, 165);
            this.lblNuevaContra.Name = "lblNuevaContra";
            this.lblNuevaContra.Size = new System.Drawing.Size(143, 22);
            this.lblNuevaContra.TabIndex = 19;
            this.lblNuevaContra.Text = "Nueva contraseña:";
            this.lblNuevaContra.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // txtConfirmarContrasena
            // 
            this.txtConfirmarContrasena.BackColor = System.Drawing.Color.WhiteSmoke;
            this.txtConfirmarContrasena.Location = new System.Drawing.Point(30, 248);
            this.txtConfirmarContrasena.Multiline = true;
            this.txtConfirmarContrasena.Name = "txtConfirmarContrasena";
            this.txtConfirmarContrasena.Size = new System.Drawing.Size(551, 20);
            this.txtConfirmarContrasena.TabIndex = 18;
            // 
            // txtCodigo
            // 
            this.txtCodigo.BackColor = System.Drawing.Color.WhiteSmoke;
            this.txtCodigo.Location = new System.Drawing.Point(30, 137);
            this.txtCodigo.Name = "txtCodigo";
            this.txtCodigo.Size = new System.Drawing.Size(264, 20);
            this.txtCodigo.TabIndex = 17;
            // 
            // lblDireccion
            // 
            this.lblDireccion.Font = new System.Drawing.Font("Times New Roman", 12F);
            this.lblDireccion.Location = new System.Drawing.Point(26, 223);
            this.lblDireccion.Name = "lblDireccion";
            this.lblDireccion.Size = new System.Drawing.Size(146, 22);
            this.lblDireccion.TabIndex = 16;
            this.lblDireccion.Text = "Confirmar contraseña:";
            this.lblDireccion.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lblCorreo
            // 
            this.lblCorreo.Font = new System.Drawing.Font("Times New Roman", 12F);
            this.lblCorreo.Location = new System.Drawing.Point(29, 109);
            this.lblCorreo.Name = "lblCorreo";
            this.lblCorreo.Size = new System.Drawing.Size(61, 22);
            this.lblCorreo.TabIndex = 15;
            this.lblCorreo.Text = "Código:";
            this.lblCorreo.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // txtNuevaContrasena
            // 
            this.txtNuevaContrasena.BackColor = System.Drawing.Color.WhiteSmoke;
            this.txtNuevaContrasena.Location = new System.Drawing.Point(30, 190);
            this.txtNuevaContrasena.Name = "txtNuevaContrasena";
            this.txtNuevaContrasena.Size = new System.Drawing.Size(551, 20);
            this.txtNuevaContrasena.TabIndex = 14;
            // 
            // txtCorreoRecuperacion
            // 
            this.txtCorreoRecuperacion.BackColor = System.Drawing.Color.WhiteSmoke;
            this.txtCorreoRecuperacion.Location = new System.Drawing.Point(28, 40);
            this.txtCorreoRecuperacion.Name = "txtCorreoRecuperacion";
            this.txtCorreoRecuperacion.Size = new System.Drawing.Size(551, 20);
            this.txtCorreoRecuperacion.TabIndex = 12;
            // 
            // lblCorreoRecuperacion
            // 
            this.lblCorreoRecuperacion.Font = new System.Drawing.Font("Times New Roman", 12F);
            this.lblCorreoRecuperacion.Location = new System.Drawing.Point(24, 15);
            this.lblCorreoRecuperacion.Name = "lblCorreoRecuperacion";
            this.lblCorreoRecuperacion.Size = new System.Drawing.Size(72, 22);
            this.lblCorreoRecuperacion.TabIndex = 11;
            this.lblCorreoRecuperacion.Text = "Correo:";
            this.lblCorreoRecuperacion.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // frmRecuperarClave
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(786, 476);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.lblBienvenida);
            this.Name = "frmRecuperarClave";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "RECUPERAR CONTRASEÑA";
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnRecuperarClave;
        private System.Windows.Forms.Label lblBienvenida;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label lblNuevaContra;
        private System.Windows.Forms.TextBox txtConfirmarContrasena;
        private System.Windows.Forms.TextBox txtCodigo;
        private System.Windows.Forms.Label lblDireccion;
        private System.Windows.Forms.Label lblCorreo;
        private System.Windows.Forms.TextBox txtNuevaContrasena;
        private System.Windows.Forms.TextBox txtCorreoRecuperacion;
        private System.Windows.Forms.Label lblCorreoRecuperacion;
        private System.Windows.Forms.Button btnCodigoRecuperar;
        private System.Windows.Forms.Button btnVerContrasena;
        private System.Windows.Forms.ErrorProvider errorProvider1;
    }
}