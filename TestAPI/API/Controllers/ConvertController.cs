using Microsoft.AspNetCore.Mvc;
using PuppeteerSharp.Media;
using PuppeteerSharp;
using System.IO;
using System.Reflection;
using System.Collections;
using Balbarak.WeasyPrint;
using iTextSharp.text.pdf;
using iTextSharp.text;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConvertController : ControllerBase
    {
        // GET: api/<ConvertController>
        [HttpGet("invoice")]
        public string Get()
        {
            return ConvertToPDF();
        }


        [HttpGet("weasyPrint")]
        public void Get_WeasyPrint()
        {
            GetWeasyPrint();
        }

        //[HttpGet("invoice_stream")]
        //public IActionResult GetStream()
        //{
        //    var pdfFIle = ConvertToPDF();
        //    return File(pdfFIle, "application/pdf", "testFile.pdf");
        //}

        //public static BaseFont GetFont()
        //{
        //   var path = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location) + "\\App_Data\\InvoicePic\\font\\FbHadasaNewBook-Light.otf";
        //    return BaseFont.CreateFont(path, BaseFont., BaseFont.EMBEDDED);
        //}

        //public static BaseFont GetFont()
        //{
        //    string fontName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Fonts), "FbHadasaNewBook-Light.otf");
        //    return BaseFont.CreateFont(fontName, BaseFont.CP1252, BaseFont.EMBEDDED);
        //}
        public static BaseFont GetTahoma()
        {
            var fontName = "FbHadasaNewBook-Light";
            if (!FontFactory.IsRegistered(fontName))
            {
                var fontPath = Environment.GetEnvironmentVariable("SystemRoot") + "\\fonts\\FbHadasaNewBook-Light.otf";
                FontFactory.Register(fontPath, fontName);
            }
            return FontFactory.GetFont(fontName, BaseFont.IDENTITY_H, BaseFont.EMBEDDED).BaseFont;
        }

        private static BaseFont GetFont()
        {
            string fontName = "Fb HadasaNewBook Light";
            string fileName = "FbHadasaNewBook-Light.otf";
            if (!FontFactory.IsRegistered(fontName))
            {
                var fontPath = Environment.GetEnvironmentVariable("SystemRoot") + "\\fonts\\" + fileName;
                FontFactory.Register(fontPath);
            }
            return FontFactory.GetFont(fontName, BaseFont.IDENTITY_H, BaseFont.EMBEDDED).BaseFont;
        }

        private void GetWeasyPrint()
        {
            string binFolder = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location) + "\\App_Data\\InvoicePic\\";
            using (FileStream outFile = new FileStream(@$"{binFolder}\result.pdf", FileMode.Create))
            {
                PdfReader pdfReader = new PdfReader(@$"{binFolder}\form.pdf");
                PdfStamper pdfStamper = new PdfStamper(pdfReader, outFile);


                AcroFields fields = pdfStamper.AcroFields;
                //fields.AddSubstitutionFont(GetFont());
                //rest of the code here
                fields.SetField("date", "1/01/2023");
                fields.SetField("t.z", "123456789");

                //BaseFont bf = GetFont();

                //fields.AddSubstitutionFont(bf);
                    
                    //...
                pdfStamper.Close();
                pdfReader.Close();
            }

            //using (WeasyPrintClient client = new WeasyPrintClient())
            //{
            //    var input = @$"{binFolder}\index01.html";
            //    var output = @$"{binFolder}\test01.pdf";

            //    client.GeneratePdf(input, output);
            //}
        }

        private  string ConvertToPDF()
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
            string ticksPath = (DateTime.Now.Ticks).ToString() ;

            System.IO.File.WriteAllText(binFolder + ticksPath + ".html", template);

            var res = page.GoToAsync(Path.Combine(binFolder + ticksPath + ".html")).Result;

            //await page.AddStyleTagAsync(@"D:\Programing\Moses\InvoicePic\style.css");

            var pdfOptionsJson = getOptionsPDF();

            byte[] _Pdf = page.PdfDataAsync(pdfOptionsJson).Result;

            Stream stream = new MemoryStream(_Pdf);


            System.IO.File.WriteAllBytes(binFolder + ticksPath + ".pdf", _Pdf);

            if (System.IO.File.Exists("./" + ticksPath)) { System.IO.File.Delete("./" + ticksPath); }

            browserFetcher.Dispose();
            browser.Process.Kill();

            return binFolder + ticksPath + ".pdf";
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
