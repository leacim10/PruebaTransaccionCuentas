using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccesoDatos.Repositorios
{
    public abstract class RepositorioMaestro:Repositorio
    {
        protected List<SqlParameter> parametersSQL;

        protected int ExecuteNonQuery(string transaccionSQL)
        {
            using (var connection = GetSQLConnection())
            {
                connection.Open();
                using (var command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = transaccionSQL;
                    command.CommandType = CommandType.Text;
                    foreach (SqlParameter item in parametersSQL)
                    {
                        command.Parameters.Add(item);
                    }
                    int resultado = command.ExecuteNonQuery();
                    parametersSQL.Clear();
                    return resultado;
                }
            }
        }
        protected DataTable ExecuteReaderParameters(string transaccionSQL)
        {
            using (var connection = GetSQLConnection())
            {
                connection.Open();
                using (var command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = transaccionSQL;
                    command.CommandType = CommandType.Text;
                    foreach (SqlParameter item in parametersSQL)
                    {
                        command.Parameters.Add(item);
                    }
                    SqlDataReader reader = command.ExecuteReader();
                    using (var table = new DataTable())
                    {
                        table.Load(reader);
                        reader.Dispose();
                        return table;
                    }
                }
            }
        }

        protected DataTable ExecuteReader(string transaccionSQL)
        {
            using (var connection = GetSQLConnection())
            {
                connection.Open();
                using (var command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = transaccionSQL;
                    command.CommandType = CommandType.Text;

                    SqlDataReader reader = command.ExecuteReader();
                    using (var table = new DataTable())
                    {
                        table.Load(reader);
                        reader.Dispose();
                        return table;
                    }
                }
            }
        }
    }
}
