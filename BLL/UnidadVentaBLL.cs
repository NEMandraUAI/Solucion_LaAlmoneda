using BE.Entidades;
using BE.Excepciones;
using DAL.Repositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class UnidadVentaBLL
    {
        private readonly UnidadVentaDAL _unidadVentaDAL;
        public UnidadVentaBLL()
        {
            _unidadVentaDAL = new UnidadVentaDAL();
        }
        public void GuardarUnidadVenta(UnidadVentaBE unidad)
        {
            if (unidad == null)
            {
                throw new ReglaNegocioException("La unidad de venta no puede ser nula.");
            }
            if (string.IsNullOrWhiteSpace(unidad.Nombre))
            {
                throw new ReglaNegocioException("El nombre del artículo o lote es obligatorio.");
            }
            if (unidad is ArticuloBE articulo)
            {
                if (articulo.PrecioBaseHistorico <= 0)
                {
                    throw new ReglaNegocioException($"El precio del artículo '{articulo.Nombre}' debe ser mayor a cero.");
                }
            }
            else if (unidad is LoteBE lote)
            {
                if (lote.Componentes == null || lote.Componentes.Count == 0)
                {
                    throw new ReglaNegocioException($"El lote '{lote.Nombre}' debe contener al menos un artículo o sub-lote.");
                }
            }
            try
            {
                _unidadVentaDAL.Guardar(unidad);
            }
            catch (Exception ex)
            {
                throw new Exception("Ocurrió un error en la base de datos al guardar la unidad de venta: " + ex.Message, ex);
            }
        }
        public List<UnidadVentaBE> ObtenerDisponiblesParaSubasta()
        {
            try
            {
                return _unidadVentaDAL.ObtenerDisponiblesParaSubasta();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener las unidades de venta disponibles: " + ex.Message, ex);
            }
        }
    }
}
