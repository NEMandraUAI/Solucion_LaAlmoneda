using BE.Entidades;
using BE.Excepciones;
using BE.Interfaces;
using DAL.Repositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class SubastaBLL
    {
        private readonly SubastaDAL _subastaDAL;
        private static List<SubastaBE> _cacheSubastas = new List<SubastaBE>();
        private readonly List<INotificable> _observadoresGlobales = new List<INotificable>();
        public static event Action<SubastaBE> NuevaSubastaCreada;
        public static event Action<SubastaBE, string> SubastaFinalizada;
        public SubastaBLL()
        {
            _subastaDAL = new SubastaDAL();
        }
        public List<SubastaBE> ObtenerSubastasActivas()
        {
            var subastasDb = _subastaDAL.ObtenerTodasActivas();
            foreach (var dbSub in subastasDb)
            {
                var enCache = _cacheSubastas.FirstOrDefault(s => s.Id == dbSub.Id);
                if (enCache == null)
                {
                    if (dbSub.UnidadVenta is LoteBE lote)
                        _subastaDAL.CargarComponentesHijos(lote);
                    _cacheSubastas.Add(dbSub);
                }
                else
                {
                    enCache.PrecioActual = dbSub.PrecioActual;
                    enCache.Estado = dbSub.Estado;
                    enCache.FilaVersion = dbSub.FilaVersion;
                    enCache.TienePujas = dbSub.TienePujas;
                    enCache.Ganador = dbSub.Ganador;
                }
            }
            return _cacheSubastas.Where(s => s.Estado == "Abierta").ToList();
        }
        public void ProcesarOferta(SubastaBE subasta, UsuarioBE usuario, decimal montoOferta)
        {
            decimal precioViejo = subasta.PrecioActual;
            UsuarioBE ganadorViejo = subasta.Ganador;
            bool teniaPujas = subasta.TienePujas;
            subasta.RegistrarPuja(montoOferta, usuario);
            PujaBE nuevaPuja = new PujaBE
            {
                Subasta = subasta,
                Usuario = usuario,
                Monto = montoOferta,
                FechaHora = DateTime.Now
            };
            try
            {
                _subastaDAL.GuardarPujaYActualizarSubasta(subasta, nuevaPuja);
                subasta.Notificar();
            }
            catch (ConcurrenciaException)
            {
                subasta.PrecioActual = precioViejo;
                subasta.Ganador = ganadorViejo;
                subasta.TienePujas = teniaPujas;
                throw;
            }
        }
        public void CrearSubasta(UnidadVentaBE item)
        {
            try
            {
                SubastaBE nueva = new SubastaBE
                {
                    UnidadVenta = item,
                    PrecioActual = item.CalcularPrecioBase(),
                    Estado = "Abierta",
                    TienePujas = false
                };
                _subastaDAL.InsertarSubasta(nueva);
                _cacheSubastas.Add(nueva);
                NotificarCreacionGlobal(nueva);
                NuevaSubastaCreada?.Invoke(nueva);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al crear la subasta: " + ex.Message);
            }
        }
        public void CerrarSubastaConGanador(SubastaBE subasta)
        {
            if (!subasta.TienePujas || subasta.Ganador == null)
                throw new ReglaNegocioException("No se puede cerrar con ganador una subasta sin pujas.");
            try
            {
                _subastaDAL.ActualizarEstadoSubasta(subasta, "Cerrada");
                subasta.Estado = "Cerrada";
                subasta.Notificar();
                SubastaFinalizada?.Invoke(subasta, "Cerrada (Ganador: " + subasta.Ganador.Nombre + ")");
            }
            catch (Exception) { throw; }
        }
        public void CancelarSubastaSinGanador(SubastaBE subasta)
        {
            try
            {
                _subastaDAL.ActualizarEstadoSubasta(subasta, "Cancelada");
                subasta.Estado = "Cancelada";
                subasta.Notificar();
                SubastaFinalizada?.Invoke(subasta, "Cancelada sin ganador");
            }
            catch (Exception) { throw; }
        }
        public void SuscribirClienteGlobal(INotificable observador)
        {
            if (!_observadoresGlobales.Contains(observador))
            {
                _observadoresGlobales.Add(observador);
            }
        }
        public void DesuscribirClienteGlobal(INotificable observador)
        {
            if (_observadoresGlobales.Contains(observador))
            {
                _observadoresGlobales.Remove(observador);
            }
        }
        private void NotificarCreacionGlobal(SubastaBE nuevaSubasta)
        {
            foreach (var observador in _observadoresGlobales)
            {
                observador.Actualizar(nuevaSubasta);
            }
        }
    }
}
