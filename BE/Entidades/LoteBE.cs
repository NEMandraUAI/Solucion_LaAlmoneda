using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE.Entidades
{
    public class LoteBE : UnidadVentaBE
    {
        public List<UnidadVentaBE> Componentes { get; set; } = new List<UnidadVentaBE>();
        public void Agregar(UnidadVentaBE unidad) => Componentes.Add(unidad);
        public override decimal CalcularPrecioBase() => Componentes.Sum(c => c.CalcularPrecioBase());
        public override string ObtenerDetalle()
        {
            string detalle = $"+ LOTE: {Nombre} (Total Base: ${CalcularPrecioBase()})\n";
            foreach (var comp in Componentes)
            {
                detalle += $"    {comp.ObtenerDetalle()}\n";
            }
            return detalle;
        }
    }
}
