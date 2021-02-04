using AccesoDatos.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccesoDatos.Contratos
{
    public interface ICuentaRepositorio:IRepositorioGenerico<CUENTA>
    {
        IEnumerable<CUENTA> devolverCuenta(string nro_cuenta);
    }
}
