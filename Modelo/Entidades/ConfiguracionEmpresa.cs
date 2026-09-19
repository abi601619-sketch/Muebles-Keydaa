namespace Modelo.Entidades

{
    public static class ConfiguracionEmpresa
    {
        public static string Nombre
        {
            get
            {
                return Modelo.Properties.Settings.Default.NombreEmpresa;
            }
        }

        public static string Telefono
        {
            get
            {
                return Modelo.Properties.Settings.Default.TelefonoEmpresa;
            }
        }

        public static string Correo
        {
            get
            {
                return Modelo.Properties.Settings.Default.CorreoEmpresa;
            }
        }

        public static string Direccion
        {
            get
            {
                return Modelo.Properties.Settings.Default.DireccionEmpresa;
            }
        }

        public static string Logo
        {
            get
            {
                return Modelo.Properties.Settings.Default.LogoEmpresa;
            }
        }
    }
}
