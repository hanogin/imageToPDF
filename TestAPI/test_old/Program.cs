using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace test_old
{
    internal class Program
    {
        static void Main(string[] args)
        {
            HttpResponseMessage result = null;

            using (var client = new HttpClient())
            {
                HttpResponseMessage response = client.GetAsync("http://192.168.60.1/htmlTpPDF/api/Convert/invoice").Result;
                byte[] arr = response.Content.ReadAsByteArrayAsync().Result;

                MemoryStream stream = new MemoryStream(arr);
            }


            using (var client = new HttpClient())
            {
                HttpResponseMessage response = client.GetAsync("http://192.168.60.1/htmlTpPDF/api/Convert/invoice_stream").Result;
                var stream = response.Content.ReadAsStreamAsync();
            }
        }
    }
}
