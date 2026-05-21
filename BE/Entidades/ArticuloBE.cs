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
    }
}
