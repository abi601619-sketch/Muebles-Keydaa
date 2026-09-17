using Datos;
using Modelo.Entidades;
using System;
using System.Data;
using System.Windows.Forms;
using Vista.Responsive;


namespace Vista.Dashboard
{
    public partial class frmInicio : Form
    {

        private static string servidor = "(localdb)\\MSSQLLocalDB";
        private static string baseDeDatos = "MueblesKeyda";

        private string cadena =
            $"Data source={servidor};" +
            $"Initial Catalog={baseDeDatos};" +
            $"Integrated Security=true;";

        private DbDashboard dbDashboard;

        public frmInicio()
        {
            InitializeComponent();
            ResponsiveHelper.Apply(this);

            // Inicializar acceso al Dashboard
            dbDashboard = new DbDashboard(cadena);
        }

        private void CargarIndicadores()
        {
            try
            {
                DataTable datos =
                    dbDashboard.ObtenerIndicadores();

                if (datos.Rows.Count > 0)
                {
                    DataRow fila = datos.Rows[0];

                    // Materiales registrados
                    lblMateriales.Text =
                        Convert.ToInt32(
                            fila["MaterialesRegistrados"]
                        ).ToString();

                    // Clientes registrados
                    lblClientess.Text =
                        Convert.ToInt32(
                            fila["ClientesRegistrados"]
                        ).ToString();

                    // Ventas del mes
                    decimal ventas =
                        Convert.ToDecimal(
                            fila["VentasDelMes"]
                        );

                    lblVentas.Text =
                        ventas.ToString("$#,##0.00");

                    // Cotizaciones registradas
                    lblCotizacioness.Text =
                        Convert.ToInt32(
                            fila["CotizacionesRegistradas"]
                        ).ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Error al cargar los indicadores: "
                    + ex.Message,
                    "Dashboard",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        private void MostrarPedidosRecientes()
        {
            try
            {
                dgvPedidosRecientes.DataSource = null;
                dgvPedidosRecientes.DataSource =
                    DbPedidos.CargarPedidosRecientes();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Error al cargar los pedidos recientes: "
                    + ex.Message,
                    "Dashboard",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        private void frmInicio_Load(object sender, EventArgs e)
        {
            try
            {
                CargarIndicadores();
                MostrarPedidosRecientes();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Error al cargar el Dashboard: "
                    + ex.Message,
                    "Dashboard",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }
    }

}


