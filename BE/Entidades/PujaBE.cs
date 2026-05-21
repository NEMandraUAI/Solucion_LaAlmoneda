using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE.Entidades
{
    public class PujaBE
    {
        public int Id { get; set; }
        public SubastaBE Subasta { get; set; }
        public UsuarioBE Usuario { get; set; }
        public decimal Monto { get; set; }
        public DateTime FechaHora { get; set; }
    }
}
