
using Microsoft.Data.SqlClient;
using System.Data;

namespace API.Dal
{
    public class SpRequest
    {
        private IConfiguration _configuration;

        public SpRequest(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        //public DalParameter[] Params { get; set; }
        public DataSet GetData(string StoredProcedure, DalParameter[] Params = null)
        {
            DataSet ds;
            if (Params != null && Params.Length > 0)
                ds = SqlCommand(GetSqlParameter(Params), StoredProcedure);
            else
                ds = SqlCommand(StoredProcedure);
            return ds.Tables.Count > 0 ? ds : null;
        }
        private SqlParameter[] GetSqlParameter(DalParameter[] Params)
        {
            SqlParameter[] parm = new SqlParameter[Params.Length];
            for (int i = 0; i < Params.Length; i++)
            {
                parm[i] = new SqlParameter
                {
                    ParameterName = Params[i].ParameterName,
                    Direction = Params[i].Direction,
                    DbType = Params[i].DbType,
                    Value = Params[i].Value
                };
            }
            return parm;
        }

        private DataSet SqlCommand(SqlParameter[] Params, string StoredProcedure)
        {
            using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                DataSet ds = new DataSet();
                SqlCommand cmd = new SqlCommand
                {
                    Connection = con,
                    CommandType = CommandType.StoredProcedure,
                    CommandText = StoredProcedure
                };
                cmd.Parameters.AddRange(Params);
                SqlDataAdapter da = new SqlDataAdapter
                {
                    SelectCommand = cmd
                };
                da.Fill(ds);
                return ds;
            }

        }

        private DataSet SqlCommand(string StoredProcedure)
        {
            using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                DataSet ds = new DataSet();
                SqlCommand cmd = new SqlCommand
                {
                    Connection = con,
                    CommandType = CommandType.StoredProcedure,
                    CommandText = StoredProcedure
                };
                SqlDataAdapter da = new SqlDataAdapter
                {
                    SelectCommand = cmd
                };
                da.Fill(ds);
                return ds;
            }
        }
    }
}
