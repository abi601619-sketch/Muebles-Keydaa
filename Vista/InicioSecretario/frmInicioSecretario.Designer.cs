namespace Vista.InicioSecretario
{
    partial class frmInicioSecretario
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.pnlContenedor = new System.Windows.Forms.Panel();
            this.chartPedidosEstado = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.panel2 = new System.Windows.Forms.Panel();
            this.chartInventarioEstado = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblPedidosActivos = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.lblPedidosAc = new System.Windows.Forms.Label();
            this.pnlProductosInventario = new System.Windows.Forms.Panel();
            this.lblMateriales = new System.Windows.Forms.Label();
            this.pbInventario = new System.Windows.Forms.PictureBox();
            this.lblMaterialesRegistrados = new System.Windows.Forms.Label();
            this.pnlpPedidosActivos = new System.Windows.Forms.Panel();
            this.lblClientess = new System.Windows.Forms.Label();
            this.pbClientes = new System.Windows.Forms.PictureBox();
            this.lblClientes = new System.Windows.Forms.Label();
            this.picLogo = new System.Windows.Forms.PictureBox();
            this.pnlBarra = new System.Windows.Forms.Panel();
            this.lblSecretario = new System.Windows.Forms.Label();
            this.pbPerfil = new System.Windows.Forms.PictureBox();
            this.pnlPedidosRecientes = new System.Windows.Forms.Panel();
            this.lblPedidosRecientes = new System.Windows.Forms.Label();
            this.dgvPedidosRecientes = new System.Windows.Forms.DataGridView();
            this.lblSubTexto = new System.Windows.Forms.Label();
            this.lblMensajeInformativoPrincipal = new System.Windows.Forms.Label();
            this.pnlContenedor.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartPedidosEstado)).BeginInit();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartInventarioEstado)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.pnlProductosInventario.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbInventario)).BeginInit();
            this.pnlpPedidosActivos.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbClientes)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picLogo)).BeginInit();
            this.pnlBarra.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbPerfil)).BeginInit();
            this.pnlPedidosRecientes.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPedidosRecientes)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlContenedor
            // 
            this.pnlContenedor.AutoSize = true;
            this.pnlContenedor.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(227)))), ((int)(((byte)(175)))));
            this.pnlContenedor.Controls.Add(this.chartPedidosEstado);
            this.pnlContenedor.Controls.Add(this.panel2);
            this.pnlContenedor.Controls.Add(this.panel1);
            this.pnlContenedor.Controls.Add(this.pnlProductosInventario);
            this.pnlContenedor.Controls.Add(this.pnlpPedidosActivos);
            this.pnlContenedor.Controls.Add(this.picLogo);
            this.pnlContenedor.Controls.Add(this.pnlBarra);
            this.pnlContenedor.Controls.Add(this.pnlPedidosRecientes);
            this.pnlContenedor.Controls.Add(this.lblSubTexto);
            this.pnlContenedor.Controls.Add(this.lblMensajeInformativoPrincipal);
            this.pnlContenedor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlContenedor.Location = new System.Drawing.Point(0, 0);
            this.pnlContenedor.Margin = new System.Windows.Forms.Padding(2);
            this.pnlContenedor.Name = "pnlContenedor";
            this.pnlContenedor.Size = new System.Drawing.Size(1102, 627);
            this.pnlContenedor.TabIndex = 6;
            // 
            // chartPedidosEstado
            // 
            chartArea1.Name = "ChartArea1";
            this.chartPedidosEstado.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.chartPedidosEstado.Legends.Add(legend1);
            this.chartPedidosEstado.Location = new System.Drawing.Point(653, 428);
            this.chartPedidosEstado.Name = "chartPedidosEstado";
            series1.ChartArea = "ChartArea1";
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            this.chartPedidosEstado.Series.Add(series1);
            this.chartPedidosEstado.Size = new System.Drawing.Size(438, 187);
            this.chartPedidosEstado.TabIndex = 36;
            this.chartPedidosEstado.Text = "char";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(213)))), ((int)(((byte)(153)))), ((int)(((byte)(105)))));
            this.panel2.Controls.Add(this.chartInventarioEstado);
            this.panel2.Location = new System.Drawing.Point(26, 224);
            this.panel2.Margin = new System.Windows.Forms.Padding(2);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(609, 374);
            this.panel2.TabIndex = 32;
            // 
            // chartInventarioEstado
            // 
            chartArea2.Name = "ChartArea1";
            this.chartInventarioEstado.ChartAreas.Add(chartArea2);
            legend2.Name = "Legend1";
            this.chartInventarioEstado.Legends.Add(legend2);
            this.chartInventarioEstado.Location = new System.Drawing.Point(24, 13);
            this.chartInventarioEstado.Name = "chartInventarioEstado";
            series2.ChartArea = "ChartArea1";
            series2.Legend = "Legend1";
            series2.Name = "Series1";
            this.chartInventarioEstado.Series.Add(series2);
            this.chartInventarioEstado.Size = new System.Drawing.Size(573, 345);
            this.chartInventarioEstado.TabIndex = 0;
            this.chartInventarioEstado.Text = " chartPedidosEstado";
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.Color.LightSalmon;
            this.panel1.Controls.Add(this.lblPedidosActivos);
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Controls.Add(this.lblPedidosAc);
            this.panel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.panel1.Location = new System.Drawing.Point(663, 113);
            this.panel1.Margin = new System.Windows.Forms.Padding(2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(303, 83);
            this.panel1.TabIndex = 33;
            // 
            // lblPedidosActivos
            // 
            this.lblPedidosActivos.AutoSize = true;
            this.lblPedidosActivos.Location = new System.Drawing.Point(143, 54);
            this.lblPedidosActivos.Name = "lblPedidosActivos";
            this.lblPedidosActivos.Size = new System.Drawing.Size(35, 13);
            this.lblPedidosActivos.TabIndex = 6;
            this.lblPedidosActivos.Text = "label5";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::Vista.Properties.Resources.carro__1_;
            this.pictureBox1.Location = new System.Drawing.Point(13, 9);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(84, 61);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 2;
            this.pictureBox1.TabStop = false;
            // 
            // lblPedidosAc
            // 
            this.lblPedidosAc.AutoSize = true;
            this.lblPedidosAc.Font = new System.Drawing.Font("Times New Roman", 14F);
            this.lblPedidosAc.Location = new System.Drawing.Point(103, 15);
            this.lblPedidosAc.Name = "lblPedidosAc";
            this.lblPedidosAc.Size = new System.Drawing.Size(129, 21);
            this.lblPedidosAc.TabIndex = 2;
            this.lblPedidosAc.Text = "Pedidos activos";
            // 
            // pnlProductosInventario
            // 
            this.pnlProductosInventario.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlProductosInventario.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(170)))), ((int)(((byte)(255)))));
            this.pnlProductosInventario.Controls.Add(this.lblMateriales);
            this.pnlProductosInventario.Controls.Add(this.pbInventario);
            this.pnlProductosInventario.Controls.Add(this.lblMaterialesRegistrados);
            this.pnlProductosInventario.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnlProductosInventario.Location = new System.Drawing.Point(346, 113);
            this.pnlProductosInventario.Margin = new System.Windows.Forms.Padding(2);
            this.pnlProductosInventario.Name = "pnlProductosInventario";
            this.pnlProductosInventario.Size = new System.Drawing.Size(303, 83);
            this.pnlProductosInventario.TabIndex = 32;
            // 
            // lblMateriales
            // 
            this.lblMateriales.AutoSize = true;
            this.lblMateriales.Location = new System.Drawing.Point(147, 59);
            this.lblMateriales.Name = "lblMateriales";
            this.lblMateriales.Size = new System.Drawing.Size(39, 16);
            this.lblMateriales.TabIndex = 5;
            this.lblMateriales.Text = "label4";
            // 
            // pbInventario
            // 
            this.pbInventario.Image = global::Vista.Properties.Resources.ProductosEnInvetario128px;
            this.pbInventario.Location = new System.Drawing.Point(18, 8);
            this.pbInventario.Name = "pbInventario";
            this.pbInventario.Size = new System.Drawing.Size(89, 67);
            this.pbInventario.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbInventario.TabIndex = 2;
            this.pbInventario.TabStop = false;
            // 
            // lblMaterialesRegistrados
            // 
            this.lblMaterialesRegistrados.Font = new System.Drawing.Font("Times New Roman", 14F);
            this.lblMaterialesRegistrados.Location = new System.Drawing.Point(128, 9);
            this.lblMaterialesRegistrados.Name = "lblMaterialesRegistrados";
            this.lblMaterialesRegistrados.Size = new System.Drawing.Size(111, 44);
            this.lblMaterialesRegistrados.TabIndex = 2;
            this.lblMaterialesRegistrados.Text = "Materiales Registrados";
            // 
            // pnlpPedidosActivos
            // 
            this.pnlpPedidosActivos.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlpPedidosActivos.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(165)))), ((int)(((byte)(237)))), ((int)(((byte)(147)))));
            this.pnlpPedidosActivos.Controls.Add(this.lblClientess);
            this.pnlpPedidosActivos.Controls.Add(this.pbClientes);
            this.pnlpPedidosActivos.Controls.Add(this.lblClientes);
            this.pnlpPedidosActivos.Location = new System.Drawing.Point(26, 113);
            this.pnlpPedidosActivos.Margin = new System.Windows.Forms.Padding(2);
            this.pnlpPedidosActivos.Name = "pnlpPedidosActivos";
            this.pnlpPedidosActivos.Size = new System.Drawing.Size(303, 83);
            this.pnlpPedidosActivos.TabIndex = 34;
            // 
            // lblClientess
            // 
            this.lblClientess.AutoSize = true;
            this.lblClientess.Location = new System.Drawing.Point(148, 59);
            this.lblClientess.Name = "lblClientess";
            this.lblClientess.Size = new System.Drawing.Size(35, 13);
            this.lblClientess.TabIndex = 4;
            this.lblClientess.Text = "label3";
            // 
            // pbClientes
            // 
            this.pbClientes.Image = global::Vista.Properties.Resources.Clientes_Registrados128px;
            this.pbClientes.Location = new System.Drawing.Point(16, 9);
            this.pbClientes.Name = "pbClientes";
            this.pbClientes.Size = new System.Drawing.Size(111, 67);
            this.pbClientes.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbClientes.TabIndex = 1;
            this.pbClientes.TabStop = false;
            // 
            // lblClientes
            // 
            this.lblClientes.Font = new System.Drawing.Font("Times New Roman", 14F);
            this.lblClientes.Location = new System.Drawing.Point(133, 9);
            this.lblClientes.Name = "lblClientes";
            this.lblClientes.Size = new System.Drawing.Size(105, 44);
            this.lblClientes.TabIndex = 0;
            this.lblClientes.Text = "Clientes \r\nRegistrados";
            // 
            // picLogo
            // 
            this.picLogo.Location = new System.Drawing.Point(883, 21);
            this.picLogo.Name = "picLogo";
            this.picLogo.Size = new System.Drawing.Size(219, 104);
            this.picLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picLogo.TabIndex = 35;
            this.picLogo.TabStop = false;
            // 
            // pnlBarra
            // 
            this.pnlBarra.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(177)))), ((int)(((byte)(114)))));
            this.pnlBarra.Controls.Add(this.lblSecretario);
            this.pnlBarra.Controls.Add(this.pbPerfil);
            this.pnlBarra.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlBarra.Location = new System.Drawing.Point(0, 0);
            this.pnlBarra.Margin = new System.Windows.Forms.Padding(2);
            this.pnlBarra.Name = "pnlBarra";
            this.pnlBarra.Size = new System.Drawing.Size(1102, 23);
            this.pnlBarra.TabIndex = 9;
            // 
            // lblSecretario
            // 
            this.lblSecretario.AutoSize = true;
            this.lblSecretario.Font = new System.Drawing.Font("Times New Roman", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSecretario.Location = new System.Drawing.Point(997, 4);
            this.lblSecretario.Name = "lblSecretario";
            this.lblSecretario.Size = new System.Drawing.Size(53, 14);
            this.lblSecretario.TabIndex = 29;
            this.lblSecretario.Text = "Secretario";
            // 
            // pbPerfil
            // 
            this.pbPerfil.Image = global::Vista.Properties.Resources.Imagen_perfil_2;
            this.pbPerfil.Location = new System.Drawing.Point(1053, 0);
            this.pbPerfil.Name = "pbPerfil";
            this.pbPerfil.Size = new System.Drawing.Size(26, 26);
            this.pbPerfil.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbPerfil.TabIndex = 10;
            this.pbPerfil.TabStop = false;
            // 
            // pnlPedidosRecientes
            // 
            this.pnlPedidosRecientes.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(213)))), ((int)(((byte)(153)))), ((int)(((byte)(105)))));
            this.pnlPedidosRecientes.Controls.Add(this.lblPedidosRecientes);
            this.pnlPedidosRecientes.Controls.Add(this.dgvPedidosRecientes);
            this.pnlPedidosRecientes.Location = new System.Drawing.Point(653, 211);
            this.pnlPedidosRecientes.Margin = new System.Windows.Forms.Padding(2);
            this.pnlPedidosRecientes.Name = "pnlPedidosRecientes";
            this.pnlPedidosRecientes.Size = new System.Drawing.Size(438, 203);
            this.pnlPedidosRecientes.TabIndex = 31;
            // 
            // lblPedidosRecientes
            // 
            this.lblPedidosRecientes.AutoSize = true;
            this.lblPedidosRecientes.Font = new System.Drawing.Font("Times New Roman", 22F, System.Drawing.FontStyle.Bold);
            this.lblPedidosRecientes.ForeColor = System.Drawing.Color.White;
            this.lblPedidosRecientes.Location = new System.Drawing.Point(97, 1);
            this.lblPedidosRecientes.Name = "lblPedidosRecientes";
            this.lblPedidosRecientes.Size = new System.Drawing.Size(244, 35);
            this.lblPedidosRecientes.TabIndex = 1;
            this.lblPedidosRecientes.Text = "Pedidos Recientes";
            // 
            // dgvPedidosRecientes
            // 
            this.dgvPedidosRecientes.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvPedidosRecientes.BackgroundColor = System.Drawing.Color.White;
            this.dgvPedidosRecientes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvPedidosRecientes.Location = new System.Drawing.Point(17, 39);
            this.dgvPedidosRecientes.Name = "dgvPedidosRecientes";
            this.dgvPedidosRecientes.Size = new System.Drawing.Size(399, 150);
            this.dgvPedidosRecientes.TabIndex = 0;
            // 
            // lblSubTexto
            // 
            this.lblSubTexto.AutoSize = true;
            this.lblSubTexto.Font = new System.Drawing.Font("Times New Roman", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSubTexto.Location = new System.Drawing.Point(21, 77);
            this.lblSubTexto.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblSubTexto.Name = "lblSubTexto";
            this.lblSubTexto.Size = new System.Drawing.Size(183, 21);
            this.lblSubTexto.TabIndex = 3;
            this.lblSubTexto.Text = "Bienvenido, Secretario.";
            // 
            // lblMensajeInformativoPrincipal
            // 
            this.lblMensajeInformativoPrincipal.AutoSize = true;
            this.lblMensajeInformativoPrincipal.Font = new System.Drawing.Font("Times New Roman", 30F, System.Drawing.FontStyle.Bold);
            this.lblMensajeInformativoPrincipal.Location = new System.Drawing.Point(17, 29);
            this.lblMensajeInformativoPrincipal.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblMensajeInformativoPrincipal.Name = "lblMensajeInformativoPrincipal";
            this.lblMensajeInformativoPrincipal.Size = new System.Drawing.Size(207, 45);
            this.lblMensajeInformativoPrincipal.TabIndex = 2;
            this.lblMensajeInformativoPrincipal.Text = "Dashboard";
            // 
            // frmInicioSecretario
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1102, 627);
            this.Controls.Add(this.pnlContenedor);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmInicioSecretario";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmInicioSecretario";
            this.Load += new System.EventHandler(this.frmInicioSecretario_Load);
            this.pnlContenedor.ResumeLayout(false);
            this.pnlContenedor.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartPedidosEstado)).EndInit();
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chartInventarioEstado)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.pnlProductosInventario.ResumeLayout(false);
            this.pnlProductosInventario.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbInventario)).EndInit();
            this.pnlpPedidosActivos.ResumeLayout(false);
            this.pnlpPedidosActivos.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbClientes)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picLogo)).EndInit();
            this.pnlBarra.ResumeLayout(false);
            this.pnlBarra.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbPerfil)).EndInit();
            this.pnlPedidosRecientes.ResumeLayout(false);
            this.pnlPedidosRecientes.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPedidosRecientes)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel pnlContenedor;
        private System.Windows.Forms.Panel pnlBarra;
        private System.Windows.Forms.Label lblSecretario;
        private System.Windows.Forms.PictureBox pbPerfil;
        private System.Windows.Forms.Label lblSubTexto;
        private System.Windows.Forms.Label lblMensajeInformativoPrincipal;
        private System.Windows.Forms.Panel pnlPedidosRecientes;
        private System.Windows.Forms.Label lblPedidosRecientes;
        private System.Windows.Forms.DataGridView dgvPedidosRecientes;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblPedidosActivos;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label lblPedidosAc;
        private System.Windows.Forms.Panel pnlProductosInventario;
        private System.Windows.Forms.Label lblMateriales;
        private System.Windows.Forms.PictureBox pbInventario;
        private System.Windows.Forms.Label lblMaterialesRegistrados;
        private System.Windows.Forms.Panel pnlpPedidosActivos;
        private System.Windows.Forms.Label lblClientess;
        private System.Windows.Forms.PictureBox pbClientes;
        private System.Windows.Forms.Label lblClientes;
        private System.Windows.Forms.PictureBox picLogo;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartPedidosEstado;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartInventarioEstado;
    }
}