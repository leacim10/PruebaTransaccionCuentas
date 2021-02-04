using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccesoDatos.Contratos
{
    public interface IRepositorioGenerico<Entity> where Entity:class
    {
        int añadir(Entity entity);
        int editar(Entity entity);
        int eliminar(int idPK);
        IEnumerable<Entity> devolverTodo();
    }
}
