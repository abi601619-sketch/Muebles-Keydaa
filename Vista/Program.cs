using Modelo.Entidades;
using QuestPDF.Infrastructure;
using System;
using System.Windows.Forms;
using Vista.Configuracion_Inicial;
using Vista.Login;

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

            // Crea un objeto para trabajar con los usuarios.
            DbUsuarios usuarios = new DbUsuarios();

            // Comprueba si ya existen usuarios registrados.
            bool existenUsuarios = DbUsuarios.ExistenUsuarios();

            if (existenUsuarios)
            {
                // Si ya existen usuarios,
                // muestra directamente el Login.
                Application.Run(new frmLogin());
            }
            else
            {
                // Si no existen usuarios,
                // muestra la configuración inicial.
                Application.Run(new ConfiguracionInicial());
            }

            Application.Run(new ConfiguracionInicial());
        }
    }
}
