using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace DAL.Context
{
    public class DapperBaseRepository : IDapperBaseRepository
    {
        private string _connection;
        private IConfiguration _configuration;
        public DapperBaseRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            _connection = configuration.GetConnectionString("DefaultConnection");
        }
        public List<T> Query<T>(string query, object parameters = null)
        {
            using (SqlConnection conn
                   = new SqlConnection(_connection))
            {
                return conn.Query<T>(query, parameters).ToList();
            }
        }



        public List<T> QuerySP<T>(string query, object parameters = null)
        {
            using (SqlConnection conn
                   = new SqlConnection(_connection))
            {
                return conn.Query<T>(query, parameters, commandType: CommandType.StoredProcedure).ToList();
            }
        }

        public T QueryFirst<T>(string query, object parameters = null)
        {
            using (SqlConnection conn
                   = new SqlConnection(_connection))
            {
                return conn.QueryFirst<T>(query, parameters);
            }
        }

        public T QueryFirstOrDefault<T>(string query, object parameters = null)
        {
            using (SqlConnection conn
                     = new SqlConnection(_connection))
            {
                return conn.QueryFirstOrDefault<T>(query, parameters);
            }
        }

        public T QuerySingle<T>(string query, object parameters = null)
        {
            using (SqlConnection conn
                   = new SqlConnection(_connection))
            {
                return conn.QuerySingle<T>(query, parameters);
            }
        }

        public T QuerySingleOrDefault<T>(string query, object parameters = null)
        {
            using (SqlConnection conn
                          = new SqlConnection(_connection))
            {
                return conn.QuerySingleOrDefault<T>(query, parameters);
            }
        }

        public void Execute(string query, object parameters = null)
        {
            using (SqlConnection conn
                   = new SqlConnection(_connection))
            {
                conn.Execute(query, parameters);
            }
        }

        public void ExecuteSp(string query, object parameters = null)
        {

            using (SqlConnection conn
                   = new SqlConnection(_connection))
            {
                conn.Execute(query, parameters, commandType: CommandType.StoredProcedure);
            }
        }

        public void ExecuteSP(string query, object parameters = null)
        {
            throw new System.NotImplementedException();
        }

        public SqlConnection GetConnection(string databse)
        {
            if (string.IsNullOrEmpty(databse))
            {
                return new SqlConnection(_connection);
            }
            else
            {
                return new SqlConnection(_configuration.GetConnectionString(databse));
            }
        }

        public T ExecuteScalar<T>(string query, object parameters = null)
        {
            using (SqlConnection conn
             = new SqlConnection(_connection))
            {
                return conn.ExecuteScalar<T>(query, parameters);
            }
        }
    }
}
