using System;
using System.Reflection;
using System.Windows.Forms;
using System.Data.SqlClient;
using Modelo.Conexión_DB;

// Solo lectura: comprueba la carga de los formularios contra la conexión configurada.
internal static class FormLoadSmokeTests
{
    static void Load(Form form, string handler)
    {
        using (form)
        {
            form.GetType().GetMethod(handler, BindingFlags.Instance | BindingFlags.NonPublic)
                .Invoke(form, new object[] { form, EventArgs.Empty });
            Console.WriteLine("PASS: " + form.GetType().Name);
        }
    }

    [STAThread]
    static int Main()
    {
        try
        {
            Load(new Vista.Compras.frmCompras(), "frmCompras_Load");
            Load(new Vista.Pedidos.frmPedidos(), "frmPedidos_Load");
            Load(new Vista.Pedidos_Secretario.frmPedidosSecretario(), "frmPedidosSecretario_Load");
            using (var connection = Conexion.Conectar())
            using (var command = new SqlCommand("SELECT TOP (1) IdProduccion FROM Produccion ORDER BY IdProduccion", connection))
            {
                object id = command.ExecuteScalar();
                if (id != null)
                    Load(new Vista.Producción.frmMaterialUtilizado(Convert.ToInt32(id), "", DateTime.Today),
                        "frmMaterialUtilizado_Load");
                else Console.WriteLine("SKIP: No hay producción para comprobar el formulario de consumo.");
            }
            return 0;
        }
        catch (Exception error)
        {
            Console.Error.WriteLine(error);
            return 1;
        }
    }
}
