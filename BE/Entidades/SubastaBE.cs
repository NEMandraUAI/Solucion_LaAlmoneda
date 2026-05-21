using BE.Excepciones;
using BE.Interfaces;
using System.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE.Entidades
{
    public class SubastaBE
    {
        public int Id { get; set; }
        [Browsable(false)]
        public UnidadVentaBE UnidadVenta { get; set; }
        public string Estado { get; set; } = "Abierta";
        public decimal PrecioActual { get; set; }
        [Browsable(false)]
        public UsuarioBE Ganador { get; set; }
        public DateTime? FechaHoraCierre { get; set; }
        [Browsable(false)]
        public byte[] FilaVersion { get; set; }
        public bool TienePujas { get; set; }
        public string Articulo => UnidadVenta?.Nombre;
        private List<INotificable> _suscriptores = new List<INotificable>();
        public bool EstaSuscrito(INotificable suscriptor)
        {
            return _suscriptores.Contains(suscriptor);
        }
        public void Suscribir(INotificable suscriptor)
        {
            if (!EstaSuscrito(suscriptor)) _suscriptores.Add(suscriptor);
        }
        public void Desuscribir(INotificable suscriptor)
        {
            if (EstaSuscrito(suscriptor)) _suscriptores.Remove(suscriptor);
        }
        public void RegistrarPuja(decimal monto, UsuarioBE usuario)
        {
            if (Estado != "Abierta") throw new PujaInvalidaException("La subasta ya se encuentra cerrada.");
            if (!TienePujas && monto < PrecioActual) throw new PujaInvalidaException($"Al ser la primera oferta, debe igualar o superar el precio base de ${PrecioActual}.");
            else if (TienePujas && monto <= PrecioActual) throw new PujaInvalidaException($"Ya existen ofertas. Tu puja debe ser estrictamente superior a ${PrecioActual}.");
            PrecioActual = monto;
            Ganador = usuario;
            TienePujas = true;
        }
        public void Notificar()
        {
            foreach (var suscriptor in _suscriptores)
            {
                suscriptor.Actualizar(this);
            }
        }
        public string NombreDisplay
        {
            get { return $"{UnidadVenta?.Nombre} - Precio Actual: ${PrecioActual}"; }
        }
    }
}
