using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE.Entidades
{
    public abstract class UnidadVentaBE
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public decimal PrecioBaseHistorico { get; set; }
        public abstract decimal CalcularPrecioBase();
        public abstract string ObtenerDetalle();
        public abstract string GenerarReporte(int nivel = 0);
    }
}
