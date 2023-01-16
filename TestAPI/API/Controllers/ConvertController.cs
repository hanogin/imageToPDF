using Microsoft.AspNetCore.Mvc;
using PuppeteerSharp.Media;
using PuppeteerSharp;
using System.IO;
using System.Reflection;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConvertController : ControllerBase
    {
        // GET: api/<ConvertController>
        [HttpGet("invoice")]
        public byte[] Get()
        {
            return ConvertToPDF();
        }


        private byte[] ConvertToPDF()
        {
            BrowserFetcher browserFetcher;
            Browser browser;
            Page page;
            string lang;

            //string basePath = @"D:\Programing\ImageToPDF\InvoicePic\\";
            string binFolder = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location) + "\\App_Data\\InvoicePic\\";
            //string binFolder = @"C:\c_project\imageToPDF\InvoicePic\";

            var options = new LaunchOptions
            {
                Headless = true,
                ExecutablePath = "C:\\Program Files\\Google\\Chrome\\Application\\chrome.exe"


            };
            browserFetcher = new BrowserFetcher();


            browser = (Browser)Puppeteer.LaunchAsync(options).Result;


            page = (Page)browser.NewPageAsync().Result;

            string template;

            template = System.IO.File.ReadAllText(binFolder + "index.html");

            template = replaceParameters(template);
            string ticksPath = (DateTime.Now.Ticks) + ".html";

            System.IO.File.WriteAllText(binFolder + ticksPath, template);

            var res = page.GoToAsync(Path.Combine(binFolder + ticksPath)).Result;

            //await page.AddStyleTagAsync(@"D:\Programing\Moses\InvoicePic\style.css");

            var pdfOptionsJson = getOptionsPDF();

            byte[] _Pdf = page.PdfDataAsync(pdfOptionsJson).Result;

            System.IO.File.WriteAllBytes(binFolder + ticksPath + ".pdf", _Pdf);

            if (System.IO.File.Exists("./" + ticksPath)) { System.IO.File.Delete("./" + ticksPath); }

            browserFetcher.Dispose();
            browser.Process.Kill();

            return _Pdf;
        }
        private string replaceParameters(string template)
        {
            string result = template.Replace("#invoiceId#", "1001")
                .Replace("#invoiceId#", "1001")
                .Replace("#date#", "תאריך " + "13/01/2023")
                .Replace("#fullName#", "הרינו לאשר כי "  + "שלי גינזבורג")
                .Replace("#tz#", "ת.ז " + "123456789")
                .Replace("#address#", "כתובת: " + "שאגת אריה 17")
                .Replace("#apartment#", "דירה: " + "4")
                .Replace("#city#", "עיר: " + "מודיעין עילית")
                .Replace("#phone#", "טלפון: " + "0527131591")
                .Replace("#mobile#", "נייד: " + "0522610415 ")
                .Replace("#sum#", $"{100000:n0}")
                .Replace("#balance#", $"{5000:n0}");

            return result;
        }
        private PdfOptions getOptionsPDF()
        {
            var pdfOptionsJson = new PdfOptions();
            //pdfOptionsJson.Scale = new decimal(1.0);
            pdfOptionsJson.DisplayHeaderFooter = false;
            pdfOptionsJson.PrintBackground = false;
            pdfOptionsJson.Landscape = false;
            pdfOptionsJson.PageRanges = "";
            pdfOptionsJson.Format = PaperFormat.A4;
            pdfOptionsJson.Width = null;
            pdfOptionsJson.Height = null;
            //pdfOptionsJson.MarginOptions = new MarginOptions { Top = "20" + "px", Left = "0px", Bottom = "20px", Right = "0px" };
            pdfOptionsJson.PreferCSSPageSize = true;

            return pdfOptionsJson;
        }
    }
}
