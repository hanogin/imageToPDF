using PuppeteerSharp.Media;
using PuppeteerSharp;

namespace API.Service
{
    public static class Convert
    {
        static byte[] ConvertToPDF()
        {
            BrowserFetcher browserFetcher;
            Browser browser;
            Page page;
            string lang;

            var options = new LaunchOptions
            {
                Headless = true,
                ExecutablePath = "C:\\Program Files\\Google\\Chrome\\Application\\chrome.exe"


            };
            browserFetcher = new BrowserFetcher();


            browser = (Browser) Puppeteer.LaunchAsync(options).Result;


            page = (Page)browser.NewPageAsync().Result;

            string template;

            template = File.ReadAllText(@"D:\Programing\Moses\InvoicePic\index.html");

            template = replaceParameters(template);
            string ticksPath = (DateTime.Now.Ticks) + ".html";

            File.WriteAllText(@"D:\Programing\Moses\InvoicePic\" + ticksPath, template);

            var res =  page.GoToAsync(Path.Combine(@"D:\Programing\Moses\InvoicePic\" + ticksPath)).Result;

            //await page.AddStyleTagAsync(@"D:\Programing\Moses\InvoicePic\style.css");

            var pdfOptionsJson = getOptionsPDF();

            byte[] _Pdf = page.PdfDataAsync(pdfOptionsJson).Result;

            File.WriteAllBytes(@"D:\Programing\Moses\InvoicePic\" + ticksPath + ".pdf", _Pdf);

            if (File.Exists("./" + ticksPath)) { File.Delete("./" + ticksPath); }

            browserFetcher.Dispose();
            browser.Process.Kill();

            return _Pdf;
        }

        static string replaceParameters(string template)
        {
            string result = template.Replace("#invoiceId#", "1001")
                .Replace("#invoiceId#", "1001")
                .Replace("#date#", "13/01/2023")
                .Replace("#fullName#", "שלי גינזבורג")
                .Replace("#tz#", "123456789")
                .Replace("#address#", "שאגת אריה 17")
                .Replace("#apartment#", "4")
                .Replace("#city#", "מודיעין עילית")
                .Replace("#phone#", "0527131591")
                .Replace("#mobile#", "0522610415 ")
                .Replace("#sum#", "100000")
                .Replace("#balance#", "5000");

            return result;
        }

        static PdfOptions getOptionsPDF()
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
