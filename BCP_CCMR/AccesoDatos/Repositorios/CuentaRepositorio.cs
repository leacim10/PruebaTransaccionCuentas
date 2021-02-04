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
    public class CuentaRepositorio : RepositorioMaestro, ICuentaRepositorio
    {
        private string seleccionarCuenta;
        private string seleccionar;
        private string insertar;
        private string actualizar;
        private string borrar;

        public CuentaRepositorio()
        {
            seleccionarCuenta = @"SELECT *
                                FROM CUENTA
                                WHERE nro_cuenta=@nro_cuenta";
            seleccionar = @"SELECT *
                            FROM CUENTA";
            insertar = @"INSERT INTO CUENTA
                        VALUES (@nro_cuenta, @tipo, @moneda, @nombre, @saldo)";
            actualizar = @"UPDATE CUENTA
                        SET saldo=@saldo
                        WHERE nro_cuenta=@nro_cuenta";
            borrar = @"";
        }
        public int añadir(CUENTA entity)
        {
            parametersSQL = new List<SqlParameter>();
            parametersSQL.Add(new SqlParameter("@nro_cuenta", entity.nro_cuenta));
            parametersSQL.Add(new SqlParameter("@tipo", entity.tipo));
            parametersSQL.Add(new SqlParameter("@moneda", entity.moneda));
            parametersSQL.Add(new SqlParameter("@nombre", entity.nombre));
            parametersSQL.Add(new SqlParameter("@saldo", entity.saldo));
            return ExecuteNonQuery(insertar);
        }

        public IEnumerable<CUENTA> devolverCuenta(string nro_cuenta)
        {
            parametersSQL = new List<SqlParameter>();
            parametersSQL.Add(new SqlParameter("@nro_cuenta", nro_cuenta));

            var tableResult = ExecuteReaderParameters(seleccionarCuenta);
            var listCuentas = new List<CUENTA>();
            foreach (DataRow item in tableResult.Rows)
            {
                listCuentas.Add(new CUENTA
                {
                    nro_cuenta = item[0].ToString(),
                    tipo = item[1].ToString(),
                    moneda = item[2].ToString(),
                    nombre = item[3].ToString(),
                    saldo = Convert.ToDouble(item[4]),
                });
            }
            return listCuentas;
        }

        public int editar(CUENTA entity)
        {
            parametersSQL = new List<SqlParameter>();
            parametersSQL.Add(new SqlParameter("@nro_cuenta", entity.nro_cuenta));
            parametersSQL.Add(new SqlParameter("@saldo", entity.saldo));
            return ExecuteNonQuery(actualizar);
        }

        public IEnumerable<CUENTA> devolverTodo()
        {
            var tableResult = ExecuteReader(seleccionar);
            var listCuentas = new List<CUENTA>();
            foreach (DataRow item in tableResult.Rows)
            {
                listCuentas.Add(new CUENTA
                {
                    nro_cuenta = item[0].ToString(),
                    tipo = item[1].ToString(),
                    moneda = item[2].ToString(),
                    nombre = item[3].ToString(),
                    saldo = Convert.ToDouble(item[4]),
                });
            }
            return listCuentas;
        }

        public int eliminar(int idPK)
        {
            throw new NotImplementedException();
        }
    }
}
