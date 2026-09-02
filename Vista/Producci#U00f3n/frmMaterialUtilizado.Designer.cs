namespace Vista.Producción
{
    partial class frmMaterialUtilizado
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.gbInformacionGeneral = new System.Windows.Forms.GroupBox();
            this.dtpFechaEntrega = new System.Windows.Forms.DateTimePicker();
            this.txtMuebleProduccion = new System.Windows.Forms.TextBox();
            this.lblFecha = new System.Windows.Forms.Label();
            this.txtIdProduccion = new System.Windows.Forms.TextBox();
            this.lblProduccion = new System.Windows.Forms.Label();
            this.gbAgregarMateriales = new System.Windows.Forms.GroupBox();
            this.btnAgregarMaterialUtilizado = new System.Windows.Forms.Button();
            this.lblCantidadUtilizada = new System.Windows.Forms.Label();
            this.cbUnidadesDeMedida = new System.Windows.Forms.ComboBox();
            this.cbMateriales = new System.Windows.Forms.ComboBox();
            this.lblStockDisponible = new System.Windows.Forms.Label();
            this.txtCantidadUtilizada = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtStockDisponible = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.gbMaterialesAgregados = new System.Windows.Forms.GroupBox();
            this.dgvMaterialesAgregados = new System.Windows.Forms.DataGridView();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.btnSalir = new System.Windows.Forms.Button();
            this.btnGuardarConsumo = new System.Windows.Forms.Button();
            this.gbInformacionGeneral.SuspendLayout();
            this.gbAgregarMateriales.SuspendLayout();
            this.gbMaterialesAgregados.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMaterialesAgregados)).BeginInit();
            this.SuspendLayout();
            // 
            // gbInformacionGeneral
            // 
            this.gbInformacionGeneral.BackColor = System.Drawing.Color.Tan;
            this.gbInformacionGeneral.Controls.Add(this.dtpFechaEntrega);
            this.gbInformacionGeneral.Controls.Add(this.txtMuebleProduccion);
            this.gbInformacionGeneral.Controls.Add(this.lblFecha);
            this.gbInformacionGeneral.Controls.Add(this.txtIdProduccion);
            this.gbInformacionGeneral.Controls.Add(this.lblProduccion);
            this.gbInformacionGeneral.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbInformacionGeneral.Location = new System.Drawing.Point(18, 19);
            this.gbInformacionGeneral.Name = "gbInformacionGeneral";
            this.gbInformacionGeneral.Size = new System.Drawing.Size(443, 65);
            this.gbInformacionGeneral.TabIndex = 0;
            this.gbInformacionGeneral.TabStop = false;
            this.gbInformacionGeneral.Text = "Informacion General";
            // 
            // dtpFechaEntrega
            // 
            this.dtpFechaEntrega.Enabled = false;
            this.dtpFechaEntrega.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFechaEntrega.Location = new System.Drawing.Point(348, 24);
            this.dtpFechaEntrega.Name = "dtpFechaEntrega";
            this.dtpFechaEntrega.Size = new System.Drawing.Size(84, 21);
            this.dtpFechaEntrega.TabIndex = 4;
            // 
            // txtMuebleProduccion
            // 
            this.txtMuebleProduccion.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMuebleProduccion.Location = new System.Drawing.Point(155, 26);
            this.txtMuebleProduccion.Name = "txtMuebleProduccion";
            this.txtMuebleProduccion.Size = new System.Drawing.Size(134, 21);
            this.txtMuebleProduccion.TabIndex = 1;
            // 
            // lblFecha
            // 
            this.lblFecha.AutoSize = true;
            this.lblFecha.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFecha.Location = new System.Drawing.Point(300, 26);
            this.lblFecha.Name = "lblFecha";
            this.lblFecha.Size = new System.Drawing.Size(42, 15);
            this.lblFecha.TabIndex = 3;
            this.lblFecha.Text = "Fecha:";
            // 
            // txtIdProduccion
            // 
            this.txtIdProduccion.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtIdProduccion.Location = new System.Drawing.Point(88, 27);
            this.txtIdProduccion.Name = "txtIdProduccion";
            this.txtIdProduccion.Size = new System.Drawing.Size(58, 21);
            this.txtIdProduccion.TabIndex = 2;
            // 
            // lblProduccion
            // 
            this.lblProduccion.AutoSize = true;
            this.lblProduccion.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblProduccion.Location = new System.Drawing.Point(10, 28);
            this.lblProduccion.Name = "lblProduccion";
            this.lblProduccion.Size = new System.Drawing.Size(69, 15);
            this.lblProduccion.TabIndex = 1;
            this.lblProduccion.Text = "Producción:";
            // 
            // gbAgregarMateriales
            // 
            this.gbAgregarMateriales.BackColor = System.Drawing.Color.Tan;
            this.gbAgregarMateriales.Controls.Add(this.btnAgregarMaterialUtilizado);
            this.gbAgregarMateriales.Controls.Add(this.lblCantidadUtilizada);
            this.gbAgregarMateriales.Controls.Add(this.cbUnidadesDeMedida);
            this.gbAgregarMateriales.Controls.Add(this.cbMateriales);
            this.gbAgregarMateriales.Controls.Add(this.lblStockDisponible);
            this.gbAgregarMateriales.Controls.Add(this.txtCantidadUtilizada);
            this.gbAgregarMateriales.Controls.Add(this.label5);
            this.gbAgregarMateriales.Controls.Add(this.txtStockDisponible);
            this.gbAgregarMateriales.Controls.Add(this.label6);
            this.gbAgregarMateriales.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbAgregarMateriales.Location = new System.Drawing.Point(18, 107);
            this.gbAgregarMateriales.Name = "gbAgregarMateriales";
            this.gbAgregarMateriales.Size = new System.Drawing.Size(443, 132);
            this.gbAgregarMateriales.TabIndex = 5;
            this.gbAgregarMateriales.TabStop = false;
            this.gbAgregarMateriales.Text = "AGREGAR MATERIAL";
            // 
            // btnAgregarMaterialUtilizado
            // 
            this.btnAgregarMaterialUtilizado.BackColor = System.Drawing.Color.Transparent;
            this.btnAgregarMaterialUtilizado.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnAgregarMaterialUtilizado.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAgregarMaterialUtilizado.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAgregarMaterialUtilizado.Location = new System.Drawing.Point(121, 90);
            this.btnAgregarMaterialUtilizado.Name = "btnAgregarMaterialUtilizado";
            this.btnAgregarMaterialUtilizado.Size = new System.Drawing.Size(208, 26);
            this.btnAgregarMaterialUtilizado.TabIndex = 8;
            this.btnAgregarMaterialUtilizado.Text = "Agregar";
            this.btnAgregarMaterialUtilizado.UseVisualStyleBackColor = false;
            this.btnAgregarMaterialUtilizado.Click += new System.EventHandler(this.btnAgregarMaterialUtilizado_Click);
            // 
            // lblCantidadUtilizada
            // 
            this.lblCantidadUtilizada.AutoSize = true;
            this.lblCantidadUtilizada.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCantidadUtilizada.Location = new System.Drawing.Point(210, 63);
            this.lblCantidadUtilizada.Name = "lblCantidadUtilizada";
            this.lblCantidadUtilizada.Size = new System.Drawing.Size(109, 15);
            this.lblCantidadUtilizada.TabIndex = 7;
            this.lblCantidadUtilizada.Text = "Cantidad utilizada:";
            // 
            // cbUnidadesDeMedida
            // 
            this.cbUnidadesDeMedida.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbUnidadesDeMedida.FormattingEnabled = true;
            this.cbUnidadesDeMedida.Location = new System.Drawing.Point(338, 25);
            this.cbUnidadesDeMedida.Name = "cbUnidadesDeMedida";
            this.cbUnidadesDeMedida.Size = new System.Drawing.Size(99, 23);
            this.cbUnidadesDeMedida.TabIndex = 6;
            // 
            // cbMateriales
            // 
            this.cbMateriales.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbMateriales.FormattingEnabled = true;
            this.cbMateriales.Location = new System.Drawing.Point(68, 25);
            this.cbMateriales.Name = "cbMateriales";
            this.cbMateriales.Size = new System.Drawing.Size(142, 23);
            this.cbMateriales.TabIndex = 5;
            // 
            // lblStockDisponible
            // 
            this.lblStockDisponible.AutoSize = true;
            this.lblStockDisponible.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStockDisponible.Location = new System.Drawing.Point(10, 61);
            this.lblStockDisponible.Name = "lblStockDisponible";
            this.lblStockDisponible.Size = new System.Drawing.Size(101, 15);
            this.lblStockDisponible.TabIndex = 4;
            this.lblStockDisponible.Text = "Stock disponible :";
            // 
            // txtCantidadUtilizada
            // 
            this.txtCantidadUtilizada.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCantidadUtilizada.Location = new System.Drawing.Point(319, 61);
            this.txtCantidadUtilizada.Name = "txtCantidadUtilizada";
            this.txtCantidadUtilizada.Size = new System.Drawing.Size(115, 21);
            this.txtCantidadUtilizada.TabIndex = 1;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(220, 28);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(105, 15);
            this.label5.TabIndex = 3;
            this.label5.Text = "Unidad de Medida:";
            // 
            // txtStockDisponible
            // 
            this.txtStockDisponible.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtStockDisponible.Location = new System.Drawing.Point(121, 61);
            this.txtStockDisponible.Name = "txtStockDisponible";
            this.txtStockDisponible.ReadOnly = true;
            this.txtStockDisponible.Size = new System.Drawing.Size(89, 21);
            this.txtStockDisponible.TabIndex = 2;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(10, 28);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(60, 15);
            this.label6.TabIndex = 1;
            this.label6.Text = "Material :";
            // 
            // gbMaterialesAgregados
            // 
            this.gbMaterialesAgregados.BackColor = System.Drawing.Color.Tan;
            this.gbMaterialesAgregados.Controls.Add(this.dgvMaterialesAgregados);
            this.gbMaterialesAgregados.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbMaterialesAgregados.Location = new System.Drawing.Point(18, 258);
            this.gbMaterialesAgregados.Name = "gbMaterialesAgregados";
            this.gbMaterialesAgregados.Size = new System.Drawing.Size(443, 215);
            this.gbMaterialesAgregados.TabIndex = 9;
            this.gbMaterialesAgregados.TabStop = false;
            this.gbMaterialesAgregados.Text = "MATERIALES AGREGADOS";
            // 
            // dgvMaterialesAgregados
            // 
            this.dgvMaterialesAgregados.AllowUserToResizeColumns = false;
            this.dgvMaterialesAgregados.AllowUserToResizeRows = false;
            this.dgvMaterialesAgregados.BackgroundColor = System.Drawing.Color.White;
            this.dgvMaterialesAgregados.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.Bisque;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvMaterialesAgregados.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.dgvMaterialesAgregados.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvMaterialesAgregados.Location = new System.Drawing.Point(13, 23);
            this.dgvMaterialesAgregados.Name = "dgvMaterialesAgregados";
            this.dgvMaterialesAgregados.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dgvMaterialesAgregados.RowHeadersVisible = false;
            this.dgvMaterialesAgregados.Size = new System.Drawing.Size(421, 179);
            this.dgvMaterialesAgregados.TabIndex = 0;
            // 
            // btnCancelar
            // 
            this.btnCancelar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.btnCancelar.Font = new System.Drawing.Font("Times New Roman", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancelar.Location = new System.Drawing.Point(309, 479);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(73, 33);
            this.btnCancelar.TabIndex = 10;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = false;
            // 
            // btnSalir
            // 
            this.btnSalir.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.btnSalir.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btnSalir.FlatAppearance.BorderSize = 4;
            this.btnSalir.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.btnSalir.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSalir.Location = new System.Drawing.Point(386, 479);
            this.btnSalir.Margin = new System.Windows.Forms.Padding(4);
            this.btnSalir.Name = "btnSalir";
            this.btnSalir.Padding = new System.Windows.Forms.Padding(6);
            this.btnSalir.Size = new System.Drawing.Size(74, 34);
            this.btnSalir.TabIndex = 11;
            this.btnSalir.Text = "Salir";
            this.btnSalir.UseVisualStyleBackColor = false;
            this.btnSalir.Click += new System.EventHandler(this.btnSalir_Click);
            // 
            // btnGuardarConsumo
            // 
            this.btnGuardarConsumo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.btnGuardarConsumo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnGuardarConsumo.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGuardarConsumo.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnGuardarConsumo.Location = new System.Drawing.Point(173, 480);
            this.btnGuardarConsumo.Name = "btnGuardarConsumo";
            this.btnGuardarConsumo.Size = new System.Drawing.Size(130, 33);
            this.btnGuardarConsumo.TabIndex = 12;
            this.btnGuardarConsumo.Text = "Guardar Consumo";
            this.btnGuardarConsumo.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.btnGuardarConsumo.UseVisualStyleBackColor = false;
            this.btnGuardarConsumo.Click += new System.EventHandler(this.btnGuardarConsumo_Click);
            // 
            // frmMaterialUtilizado
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(221)))), ((int)(((byte)(175)))));
            this.ClientSize = new System.Drawing.Size(483, 527);
            this.Controls.Add(this.btnGuardarConsumo);
            this.Controls.Add(this.btnSalir);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.gbMaterialesAgregados);
            this.Controls.Add(this.gbAgregarMateriales);
            this.Controls.Add(this.gbInformacionGeneral);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximumSize = new System.Drawing.Size(483, 527);
            this.MinimumSize = new System.Drawing.Size(483, 527);
            this.Name = "frmMaterialUtilizado";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmMaterialUtilizado";
            this.Load += new System.EventHandler(this.frmMaterialUtilizado_Load);
            this.gbInformacionGeneral.ResumeLayout(false);
            this.gbInformacionGeneral.PerformLayout();
            this.gbAgregarMateriales.ResumeLayout(false);
            this.gbAgregarMateriales.PerformLayout();
            this.gbMaterialesAgregados.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvMaterialesAgregados)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gbInformacionGeneral;
        private System.Windows.Forms.TextBox txtMuebleProduccion;
        private System.Windows.Forms.Label lblFecha;
        private System.Windows.Forms.TextBox txtIdProduccion;
        private System.Windows.Forms.Label lblProduccion;
        private System.Windows.Forms.GroupBox gbAgregarMateriales;
        private System.Windows.Forms.ComboBox cbUnidadesDeMedida;
        private System.Windows.Forms.ComboBox cbMateriales;
        private System.Windows.Forms.Label lblStockDisponible;
        private System.Windows.Forms.TextBox txtCantidadUtilizada;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtStockDisponible;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnAgregarMaterialUtilizado;
        private System.Windows.Forms.Label lblCantidadUtilizada;
        private System.Windows.Forms.GroupBox gbMaterialesAgregados;
        private System.Windows.Forms.DataGridView dgvMaterialesAgregados;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.Button btnSalir;
        private System.Windows.Forms.Button btnGuardarConsumo;
        private System.Windows.Forms.DateTimePicker dtpFechaEntrega;
    }
}