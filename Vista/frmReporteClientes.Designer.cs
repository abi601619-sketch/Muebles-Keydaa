namespace Vista
{
    partial class frmReporteClientes
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
            this.reportViewerClientes = new Microsoft.Reporting.WinForms.ReportViewer();
            this.SuspendLayout();
            // 
            // reportViewerClientes
            // 
            this.reportViewerClientes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.reportViewerClientes.LocalReport.ReportEmbeddedResource = "Vista.rptReporteClientes.rdlc";
            this.reportViewerClientes.Location = new System.Drawing.Point(0, 0);
            this.reportViewerClientes.Name = "reportViewerClientes";
            this.reportViewerClientes.ServerReport.BearerToken = null;
            this.reportViewerClientes.Size = new System.Drawing.Size(987, 583);
            this.reportViewerClientes.TabIndex = 0;
            this.reportViewerClientes.Load += new System.EventHandler(this.reportViewerClientes_Load);
            // 
            // frmReporteClientes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(987, 583);
            this.Controls.Add(this.reportViewerClientes);
            this.Name = "frmReporteClientes";
            this.Text = "frmReporteClientes";
            this.Load += new System.EventHandler(this.frmReporteClientes_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer reportViewerClientes;
    }
}