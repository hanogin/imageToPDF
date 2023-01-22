using Microsoft.AspNetCore.Mvc;
using PuppeteerSharp.Media;
using PuppeteerSharp;
using System.IO;
using System.Reflection;
using System.Collections;
using Balbarak.WeasyPrint;
using iTextSharp.text.pdf;
using iTextSharp.text;
using System.Text;
using iText.Kernel.Font;
using API.DTO;
using API.Enum;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConvertController : ControllerBase
    {
        // GET: api/<ConvertController>
        [HttpPost("invoice")]
        public byte [] Post(ReqDTO value)
        {
            return ConvertToPDF(value);
        }



        private byte[] ConvertToPDF(ReqDTO value)
        {
            BrowserFetcher browserFetcher;
            Browser browser;
            Page page;
            string lang;
            string template;

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

            // Get content of file
            //template = System.IO.File.ReadAllText(binFolder + "index.html");
            template = GetContentOfFile(value.printOutTypeCode);

            // Inject data to file
            template = replaceParameters(template, value);
            string ticksPath = (DateTime.Now.Ticks).ToString();

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

            return _Pdf;
        }
        private string GetContentOfFile(PrintOutTypeCodes value)
        {
            string binFolder = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location) + "\\App_Data\\InvoicePic\\";
            return System.IO.File.ReadAllText(binFolder + $"{value}.html");
        }
        private string replaceParameters(string template, ReqDTO value)
        {
            string result = template.Replace("#invoiceId#", value.serialNumber)
                .Replace("#date#", "תאריך " + value.reportDetails.hebDateTitle)
                .Replace("#fullName#", "הרינו לאשר כי " + value.personDetails.degreeNameWithExt)
                .Replace("#tz#", $"{value.personDetails.personIdType} " + value.personDetails.personIdNo)
                .Replace("#address#", "כתובת: " + value.personDetails.fullAddress)
                .Replace("#apartment#", "דירה: " + value.personDetails.entrance)
                .Replace("#city#", "עיר: " + value.personDetails.cityName)
                .Replace("#phone#", "טלפון: " + value.personDetails.phoneHome)
                .Replace("#mobile#", "נייד: " + value.personDetails.phoneMobile)
                .Replace("#sum#", $"{value.reportDetails.totalAmount:n0}")
                .Replace("#balance#", $"{value.reportDetails.balance:n0}");

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


        //[HttpGet("addFormToPDF")]
        //public void Get_FormToPDF()
        //{
        //    FormtTOPDF();
        //}

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
        //public static BaseFont GetTahoma()
        //{
        //    var fontName = "FbHadasaNewBook-Light";
        //    if (!FontFactory.IsRegistered(fontName))
        //    {
        //        var fontPath = Environment.GetEnvironmentVariable("SystemRoot") + "\\fonts\\FbHadasaNewBook-Light.otf";
        //        FontFactory.Register(fontPath, fontName);
        //    }
        //    return FontFactory.GetFont(fontName, BaseFont.IDENTITY_H, BaseFont.EMBEDDED).BaseFont;
        //}
        //private static BaseFont GetFont()
        //{
        //    string fontName = "Fb HadasaNewBook Light";
        //    string fileName = "FbHadasaNewBook-Light.otf";
        //    if (!FontFactory.IsRegistered(fontName))
        //    {
        //        var fontPath = Environment.GetEnvironmentVariable("SystemRoot") + "\\fonts\\" + fileName;
        //        FontFactory.Register(fontPath);
        //    }
        //    return FontFactory.GetFont(fontName, BaseFont.IDENTITY_H, BaseFont.EMBEDDED).BaseFont;
        //}
        //private string getPathToFont()
        //{
        //    string fontName = "Fb HadasaNewBook Light";
        //    string fileName = "FbHadasaNewBook-Light.otf";

        //    return Environment.GetEnvironmentVariable("SystemRoot") + "\\fonts\\" + fontName;
        //}
        //private void FormtTOPDF()
        //{
        //    string binFolder = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location) + "\\App_Data\\InvoicePic\\";
        //    using (FileStream outFile = new FileStream(@$"C:\c_project\pdfTest\result.pdf", FileMode.Create))
        //    {
        //        string ARIALUNI_TFF = Path.Combine(@"C:\c_project\imageToPDF\TestAPI\API\bin\Debug\net6.0\App_Data\InvoicePic\font", "FbHadasaNewBook-Bold.otf");

        //        // Set font

        //        //var pathToFInt = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location) + "\\App_Data\\InvoicePic\\font\\FbHadasaNewBook-Bold.otf";
        //        //string ARIALUNI_TFF = getPathToFont();
        //        System.Text.EncodingProvider provider = System.Text.CodePagesEncodingProvider.Instance;
        //        Encoding.RegisterProvider(provider);


        //        //Create a base font object making sure to specify IDENTITY-H
        //        BaseFont bf = BaseFont.CreateFont(ARIALUNI_TFF, BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);

        //        PdfReader pdfReader = new PdfReader(@$"C:\c_project\pdfTest\form.pdf");
        //        PdfStamper pdfStamper = new PdfStamper(pdfReader, outFile);


        //        AcroFields fields = pdfStamper.AcroFields;
        //        //fields.AddSubstitutionFont(GetFont());
        //        //rest of the code here
        //        fields.SetField("date", "test");
        //        fields.SetField("t.z", "test");

        //        fields.AddSubstitutionFont(bf);

        //        //...
        //        pdfStamper.Close();
        //        pdfReader.Close();
        //    }

        //    //using (WeasyPrintClient client = new WeasyPrintClient())
        //    //{
        //    //    var input = @$"{binFolder}\index01.html";
        //    //    var output = @$"{binFolder}\test01.pdf";

        //    //    client.GeneratePdf(input, output);
        //    //}
        //}

    }
}
