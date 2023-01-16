using System.Data;

namespace API.Dal
{
    public class ResponseData
    {
        public dynamic Data { get; set; }

        public ResponseData(DataSet data)
        {
            List<DataTable> arr = new List<DataTable>();
            foreach (DataTable item in data.Tables)
            {
                arr.Add(item);
            }

            //if(arr.Count == 0)
            //{
            //    this.Data = arr.ToArray();
            //}
            this.Data = arr.ToArray();
            this.Data = this.Data[0];
        }
        //public ResponseData() { }
    }
}
