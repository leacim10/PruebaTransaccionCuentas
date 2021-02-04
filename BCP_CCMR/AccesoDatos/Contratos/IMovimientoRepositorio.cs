using AccesoDatos.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccesoDatos.Contratos
{
    public interface IMovimientoRepositorio:IRepositorioGenerico<MOVIMIENTO>
    {
        IEnumerable<MOVIMIENTO> devolverMovimiento(string nro_cuenta);
        IEnumerable<MOVIMIENTO> devolverSaldo(string nro_cuenta);
    }
}
