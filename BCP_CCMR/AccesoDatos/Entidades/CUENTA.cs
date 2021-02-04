using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccesoDatos.Entidades
{
    public class CUENTA
    {
        public string nro_cuenta { get; set; }
        public string tipo { get; set; }
        public string moneda { get; set; }
        public string nombre { get; set; }
        public double saldo { get; set; }
    }
}
