using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data.SqlClient;

namespace AccesoDatos.Repositorios
{
    public abstract class Repositorio
    {
        private readonly string connectionStringSQL;
        public Repositorio()
        {
            connectionStringSQL = ConfigurationManager.ConnectionStrings["connTransaccion"].ToString();
        }

        protected SqlConnection GetSQLConnection()
        {
            return new SqlConnection(connectionStringSQL);
        }
    }
}
