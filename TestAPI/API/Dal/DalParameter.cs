using Newtonsoft.Json;
using System.Data;

namespace API.Dal
{
    //[JsonObject]
    public class DalParameter
    {
        public string ParameterName { get; set; }
        public ParameterDirection Direction { get; set; }
        public DbType DbType { get; set; }
        public object Value { get; set; }

    }
}
