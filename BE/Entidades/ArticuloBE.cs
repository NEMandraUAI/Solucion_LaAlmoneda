using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE.Entidades
{
    public class ArticuloBE : UnidadVentaBE
    {
        public override decimal CalcularPrecioBase() => PrecioBaseHistorico;
        public override string ObtenerDetalle() => $"- Artículo: {Nombre} | Precio Base: ${PrecioBaseHistorico}";
        public override string GenerarReporte(int nivel = 0)
        {
            string tab = new string('\t', nivel);
            return $"{tab}- Artículo: {Nombre} | Precio Base: ${PrecioBaseHistorico:F2}";
        }
    }
}
