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
            this.btnRecuperarClave = new System.Windows.Forms.Button();
            this.lblBienvenida = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.lblNuevaContra = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.lblDireccion = new System.Windows.Forms.Label();
            this.lblCorreo = new System.Windows.Forms.Label();
            this.txtTelefono = new System.Windows.Forms.TextBox();
            this.txtNombreEmpresa = new System.Windows.Forms.TextBox();
            this.lblCorreoRecuperacion = new System.Windows.Forms.Label();
            this.btnCodigoRecuperar = new System.Windows.Forms.Button();
            this.panel3.SuspendLayout();
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
            this.panel3.Controls.Add(this.btnCodigoRecuperar);
            this.panel3.Controls.Add(this.btnRecuperarClave);
            this.panel3.Controls.Add(this.lblNuevaContra);
            this.panel3.Controls.Add(this.textBox2);
            this.panel3.Controls.Add(this.textBox1);
            this.panel3.Controls.Add(this.lblDireccion);
            this.panel3.Controls.Add(this.lblCorreo);
            this.panel3.Controls.Add(this.txtTelefono);
            this.panel3.Controls.Add(this.txtNombreEmpresa);
            this.panel3.Controls.Add(this.lblCorreoRecuperacion);
            this.panel3.Location = new System.Drawing.Point(74, 80);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(632, 359);
            this.panel3.TabIndex = 17;
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
            // textBox2
            // 
            this.textBox2.BackColor = System.Drawing.Color.WhiteSmoke;
            this.textBox2.Location = new System.Drawing.Point(30, 248);
            this.textBox2.Multiline = true;
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(551, 20);
            this.textBox2.TabIndex = 18;
            // 
            // textBox1
            // 
            this.textBox1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.textBox1.Location = new System.Drawing.Point(30, 137);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(264, 20);
            this.textBox1.TabIndex = 17;
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
            // txtTelefono
            // 
            this.txtTelefono.BackColor = System.Drawing.Color.WhiteSmoke;
            this.txtTelefono.Location = new System.Drawing.Point(30, 190);
            this.txtTelefono.Name = "txtTelefono";
            this.txtTelefono.Size = new System.Drawing.Size(551, 20);
            this.txtTelefono.TabIndex = 14;
            // 
            // txtNombreEmpresa
            // 
            this.txtNombreEmpresa.BackColor = System.Drawing.Color.WhiteSmoke;
            this.txtNombreEmpresa.Location = new System.Drawing.Point(28, 40);
            this.txtNombreEmpresa.Name = "txtNombreEmpresa";
            this.txtNombreEmpresa.Size = new System.Drawing.Size(551, 20);
            this.txtNombreEmpresa.TabIndex = 12;
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
            // btnCodigoRecuperar
            // 
            this.btnCodigoRecuperar.BackColor = System.Drawing.Color.Linen;
            this.btnCodigoRecuperar.Location = new System.Drawing.Point(203, 75);
            this.btnCodigoRecuperar.Name = "btnCodigoRecuperar";
            this.btnCodigoRecuperar.Size = new System.Drawing.Size(246, 31);
            this.btnCodigoRecuperar.TabIndex = 20;
            this.btnCodigoRecuperar.Text = "Enviar código";
            this.btnCodigoRecuperar.UseVisualStyleBackColor = false;
            // 
            // frmRecuperarClave
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(786, 476);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.lblBienvenida);
            this.Name = "frmRecuperarClave";
            this.Text = "RECUPERAR CONTRASEÑA";
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnRecuperarClave;
        private System.Windows.Forms.Label lblBienvenida;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label lblNuevaContra;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label lblDireccion;
        private System.Windows.Forms.Label lblCorreo;
        private System.Windows.Forms.TextBox txtTelefono;
        private System.Windows.Forms.TextBox txtNombreEmpresa;
        private System.Windows.Forms.Label lblCorreoRecuperacion;
        private System.Windows.Forms.Button btnCodigoRecuperar;
    }
}