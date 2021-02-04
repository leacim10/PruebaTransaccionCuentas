using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AccesoDatos.Entidades;
using AccesoDatos.Contratos;
using System.Data;
using System.Data.SqlClient;

namespace AccesoDatos.Repositorios
{
    public class MovimientoRepositorio:RepositorioMaestro,IMovimientoRepositorio
    {
        private string seleccionarMovimiento;
        private string seleccionarSaldo;
        private string seleccionar;
        private string insertar;
        private string actualizar;
        private string borrar;

        public MovimientoRepositorio()
        {
            seleccionarMovimiento = @"SELECT *
                                    FROM MOVIMIENTO
                                    WHERE nro_cuenta=@nro_cuenta";
            seleccionarSaldo = @"";
            seleccionar = @"";
            insertar = @"INSERT INTO MOVIMIENTO
                        VALUES (@fecha, @nro_cuenta, @tipo, @importe)";
            actualizar = @"";
            borrar = @"";
        }
        public int añadir(MOVIMIENTO entity)
        {
            parametersSQL = new List<SqlParameter>();
            parametersSQL.Add(new SqlParameter("@fecha", entity.fecha));
            parametersSQL.Add(new SqlParameter("@nro_cuenta", entity.nro_cuenta));
            parametersSQL.Add(new SqlParameter("@tipo", entity.tipo));
            parametersSQL.Add(new SqlParameter("@importe", entity.importe));
            return ExecuteNonQuery(insertar);
        }

        public int editar(MOVIMIENTO entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<MOVIMIENTO> devolverTodo()
        {
            throw new NotImplementedException();
        }

        public int eliminar(int idPK)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<MOVIMIENTO> devolverMovimiento(string nro_cuenta)
        {
            parametersSQL = new List<SqlParameter>();
            parametersSQL.Add(new SqlParameter("@nro_cuenta", nro_cuenta));

            var tableResult = ExecuteReaderParameters(seleccionarMovimiento);
            var listMovimientos = new List<MOVIMIENTO>();
            foreach(DataRow item in tableResult.Rows)
            {
                listMovimientos.Add(new MOVIMIENTO
                {
                    fecha = Convert.ToDateTime(item[0]),
                    nro_cuenta = item[1].ToString(),
                    tipo = item[2].ToString(),
                    importe = Convert.ToDouble(item[3]),
                });
            }
            return listMovimientos;
        }

        public IEnumerable<MOVIMIENTO> devolverSaldo(string nro_cuenta)
        {
            throw new NotImplementedException();
        }
    }
}
