using System;
using System.Data;

namespace Vista.Reportes
{
    internal class VentasDocumentoPDF
    {
        private DataTable ventas;
        private DataTable estadisticas;
        private DateTime fechaInicio;
        private DateTime fechaFin;
        private object rutaLogo;

        public VentasDocumentoPDF(DataTable ventas, DataTable estadisticas, DateTime fechaInicio, DateTime fechaFin, object rutaLogo)
        {
            this.ventas = ventas;
            this.estadisticas = estadisticas;
            this.fechaInicio = fechaInicio;
            this.fechaFin = fechaFin;
            this.rutaLogo = rutaLogo;
        }
    }
}