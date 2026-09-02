namespace Vista.Clientes_Secretario
{
    partial class frmBuscarClienteSecretario
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
            this.pnlBuscarClienteSuperior = new System.Windows.Forms.Panel();
            this.btnCerrarClientes = new System.Windows.Forms.Button();
            this.lblBuscarCliente = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.pbxBuscar = new System.Windows.Forms.PictureBox();
            this.dgvClientesEmpleados = new System.Windows.Forms.DataGridView();
            this.btnSeleccionarClienteEmpleado = new System.Windows.Forms.Button();
            this.btnSalir = new System.Windows.Forms.Button();
            this.pnlBuscarClienteSuperior.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbxBuscar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvClientesEmpleados)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlBuscarClienteSuperior
            // 
            this.pnlBuscarClienteSuperior.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(143)))), ((int)(((byte)(120)))), ((int)(((byte)(103)))));
            this.pnlBuscarClienteSuperior.Controls.Add(this.btnSalir);
            this.pnlBuscarClienteSuperior.Controls.Add(this.btnCerrarClientes);
            this.pnlBuscarClienteSuperior.Controls.Add(this.lblBuscarCliente);
            this.pnlBuscarClienteSuperior.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlBuscarClienteSuperior.Location = new System.Drawing.Point(0, 0);
            this.pnlBuscarClienteSuperior.Name = "pnlBuscarClienteSuperior";
            this.pnlBuscarClienteSuperior.Size = new System.Drawing.Size(537, 35);
            this.pnlBuscarClienteSuperior.TabIndex = 28;
            // 
            // btnCerrarClientes
            // 
            this.btnCerrarClientes.Image = global::Vista.Properties.Resources.Cerrar16px;
            this.btnCerrarClientes.Location = new System.Drawing.Point(641, 4);
            this.btnCerrarClientes.Name = "btnCerrarClientes";
            this.btnCerrarClientes.Size = new System.Drawing.Size(32, 23);
            this.btnCerrarClientes.TabIndex = 1;
            this.btnCerrarClientes.UseVisualStyleBackColor = true;
            this.btnCerrarClientes.Click += new System.EventHandler(this.btnCerrarClientes_Click);
            // 
            // lblBuscarCliente
            // 
            this.lblBuscarCliente.AutoSize = true;
            this.lblBuscarCliente.BackColor = System.Drawing.Color.Transparent;
            this.lblBuscarCliente.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBuscarCliente.ForeColor = System.Drawing.Color.White;
            this.lblBuscarCliente.Location = new System.Drawing.Point(15, 8);
            this.lblBuscarCliente.Name = "lblBuscarCliente";
            this.lblBuscarCliente.Size = new System.Drawing.Size(90, 15);
            this.lblBuscarCliente.TabIndex = 0;
            this.lblBuscarCliente.Text = "Buscar Cliente";
            // 
            // textBox1
            // 
            this.textBox1.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox1.Location = new System.Drawing.Point(17, 64);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(401, 22);
            this.textBox1.TabIndex = 32;
            this.textBox1.Text = "Buscar Cliente...";
            // 
            // pbxBuscar
            // 
            this.pbxBuscar.BackColor = System.Drawing.Color.Transparent;
            this.pbxBuscar.Image = global::Vista.Properties.Resources.zoom_5611171;
            this.pbxBuscar.Location = new System.Drawing.Point(394, 64);
            this.pbxBuscar.Name = "pbxBuscar";
            this.pbxBuscar.Size = new System.Drawing.Size(24, 22);
            this.pbxBuscar.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbxBuscar.TabIndex = 34;
            this.pbxBuscar.TabStop = false;
            // 
            // dgvClientesEmpleados
            // 
            this.dgvClientesEmpleados.BackgroundColor = System.Drawing.Color.White;
            this.dgvClientesEmpleados.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvClientesEmpleados.Location = new System.Drawing.Point(18, 101);
            this.dgvClientesEmpleados.Name = "dgvClientesEmpleados";
            this.dgvClientesEmpleados.Size = new System.Drawing.Size(500, 229);
            this.dgvClientesEmpleados.TabIndex = 35;
            // 
            // btnSeleccionarClienteEmpleado
            // 
            this.btnSeleccionarClienteEmpleado.BackColor = System.Drawing.Color.PaleGreen;
            this.btnSeleccionarClienteEmpleado.Font = new System.Drawing.Font("Times New Roman", 8F, System.Drawing.FontStyle.Bold);
            this.btnSeleccionarClienteEmpleado.Location = new System.Drawing.Point(95, 336);
            this.btnSeleccionarClienteEmpleado.Name = "btnSeleccionarClienteEmpleado";
            this.btnSeleccionarClienteEmpleado.Size = new System.Drawing.Size(336, 37);
            this.btnSeleccionarClienteEmpleado.TabIndex = 29;
            this.btnSeleccionarClienteEmpleado.Text = "Selecionar Cliente";
            this.btnSeleccionarClienteEmpleado.UseVisualStyleBackColor = false;
            this.btnSeleccionarClienteEmpleado.Click += new System.EventHandler(this.btnSeleccionarClienteEmpleado_Click);
            // 
            // btnSalir
            // 
            this.btnSalir.Image = global::Vista.Properties.Resources.Cerrar16px;
            this.btnSalir.Location = new System.Drawing.Point(478, 4);
            this.btnSalir.Name = "btnSalir";
            this.btnSalir.Size = new System.Drawing.Size(47, 23);
            this.btnSalir.TabIndex = 2;
            this.btnSalir.UseVisualStyleBackColor = true;
            this.btnSalir.Click += new System.EventHandler(this.btnSalir_Click);
            // 
            // frmBuscarClienteSecretario
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(227)))), ((int)(((byte)(175)))));
            this.ClientSize = new System.Drawing.Size(537, 385);
            this.Controls.Add(this.btnSeleccionarClienteEmpleado);
            this.Controls.Add(this.pnlBuscarClienteSuperior);
            this.Controls.Add(this.pbxBuscar);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.dgvClientesEmpleados);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmBuscarClienteSecretario";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmBuscarClienteSecretario";
            this.Load += new System.EventHandler(this.frmBuscarClienteSecretario_Load);
            this.pnlBuscarClienteSuperior.ResumeLayout(false);
            this.pnlBuscarClienteSuperior.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbxBuscar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvClientesEmpleados)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel pnlBuscarClienteSuperior;
        private System.Windows.Forms.Button btnCerrarClientes;
        private System.Windows.Forms.Label lblBuscarCliente;
        private System.Windows.Forms.PictureBox pbxBuscar;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.DataGridView dgvClientesEmpleados;
        private System.Windows.Forms.Button btnSeleccionarClienteEmpleado;
        private System.Windows.Forms.Button btnSalir;
    }
}