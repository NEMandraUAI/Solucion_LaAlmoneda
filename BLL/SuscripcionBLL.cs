using DAL.Repositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class SuscripcionBLL
    {
        private readonly SuscripcionDAL _suscripcionDAL = new SuscripcionDAL();
        public void SuscribirUsuario(int idUsuario, int idSubasta)
        {
            _suscripcionDAL.GuardarSuscripcion(idUsuario, idSubasta);
        }
        public void DesuscribirUsuario(int idUsuario, int idSubasta)
        {
            _suscripcionDAL.EliminarSuscripcion(idUsuario, idSubasta);
        }
        public List<int> ObtenerIdsSuscripciones(int idUsuario)
        {
            return _suscripcionDAL.ObtenerSubastasSuscritasPorUsuario(idUsuario);
        }
    }
}
