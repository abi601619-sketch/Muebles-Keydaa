using QuestPDF.Infrastructure;
using System;

using System.Windows.Forms;
using Vista.Dashboard;

namespace Vista
{
    public static class Program
    {
        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            QuestPDF.Settings.License = QuestPDF.Infrastructure.LicenseType.Community;
            QuestPDF.Settings.License = LicenseType.Evaluation;
            Application.Run(new frmDashboard());
        }
    }
}
