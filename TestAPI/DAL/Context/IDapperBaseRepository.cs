

using Microsoft.Data.SqlClient;

namespace DAL.Context
{
    public interface IDapperBaseRepository
    {
        void Execute(string query, object parameters = null);
        T ExecuteScalar<T>(string query, object parameters = null);
        List<T> Query<T>(string query, object parameters = null);
        T QueryFirst<T>(string query, object parameters = null);
        T QueryFirstOrDefault<T>(string query, object parameters = null);
        T QuerySingle<T>(string query, object parameters = null);
        T QuerySingleOrDefault<T>(string query, object parameters = null);
        List<T> QuerySP<T>(string query, object parameters = null);
        void ExecuteSP(string query, object parameters = null);
        SqlConnection GetConnection(string databsse);
    }
}