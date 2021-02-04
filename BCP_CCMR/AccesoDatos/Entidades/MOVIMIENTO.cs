using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccesoDatos.Entidades
{
    public class MOVIMIENTO
    {
        public DateTime fecha { get; set; }
        public string nro_cuenta { get; set; }
        public string tipo { get; set; }
        public double importe { get; set; }
    }
}
