using Aspose.Pdf.Forms;
using iTextSharp.text.pdf;
using PuppeteerSharp;
using PuppeteerSharp.Media;
using System;
using System.IO;
using System.Reflection;
using System.Text;

namespace InjectDataToWordFIle
{
    class Program
    {
        static async System.Threading.Tasks.Task Main(string[] args)
        {
            ConvertToPDF();
            //ConverrAspose();
            //BrowserFetcher browserFetcher;
            //Browser browser;
            //Page page;
            //string lang;

            //var options = new LaunchOptions
            //{
            //    Headless = true,
            //    ExecutablePath = "C:\\Program Files\\Google\\Chrome\\Application\\chrome.exe"


            //};
            //browserFetcher = new BrowserFetcher();


            //browser = (Browser)await Puppeteer.LaunchAsync(options);


            //page = (Page)await browser.NewPageAsync();

            //string template;

            //template = File.ReadAllText(@"D:\Programing\Moses\InvoicePic\index.html");

            //template = replaceParameters(template);
            //string ticksPath = (DateTime.Now.Ticks) + ".html";

            //File.WriteAllText(@"D:\Programing\Moses\InvoicePic\" + ticksPath, template);

            //await page.GoToAsync(Path.Combine(@"D:\Programing\Moses\InvoicePic\" + ticksPath));

            ////await page.AddStyleTagAsync(@"D:\Programing\Moses\InvoicePic\style.css");

            //var pdfOptionsJson = getOptionsPDF();

            //byte[] _Pdf = await page.PdfDataAsync(pdfOptionsJson);

            //File.WriteAllBytes(@"D:\Programing\Moses\InvoicePic\" + ticksPath + ".pdf", _Pdf);

            //if (File.Exists("./" + ticksPath)) { File.Delete("./" + ticksPath); }

            //browserFetcher.Dispose();
            //browser.Process.Kill();
        }


        //static string replaceParameters(string template)
        //{
        //    string result = template.Replace("#invoiceId#", "1001")
        //        .Replace("#invoiceId#", "1001")
        //        .Replace("#date#", "13/01/2023")
        //        .Replace("#fullName#", "שלי גינזבורג")
        //        .Replace("#tz#", "123456789")
        //        .Replace("#address#", "שאגת אריה 17")
        //        .Replace("#apartment#", "4")
        //        .Replace("#city#", "מודיעין עילית")
        //        .Replace("#phone#", "0527131591")
        //        .Replace("#mobile#", "0522610415 ")
        //        .Replace("#sum#", "100000")
        //        .Replace("#balance#", "5000");

        //    return result;
        //}

        //static PdfOptions getOptionsPDF()
        //{
        //    var pdfOptionsJson = new PdfOptions();
        //    //pdfOptionsJson.Scale = new decimal(1.0);
        //    pdfOptionsJson.DisplayHeaderFooter = false;
        //    pdfOptionsJson.PrintBackground = false;
        //    pdfOptionsJson.Landscape = false;
        //    pdfOptionsJson.PageRanges = "";
        //    pdfOptionsJson.Format = PaperFormat.A4;
        //    pdfOptionsJson.Width = null;
        //    pdfOptionsJson.Height = null;
        //    //pdfOptionsJson.MarginOptions = new MarginOptions { Top = "20" + "px", Left = "0px", Bottom = "20px", Right = "0px" };
        //    pdfOptionsJson.PreferCSSPageSize = true;

        //    return pdfOptionsJson;
        //}


        private static void ConvertToPDF()
        {
            string basePath = @"C:\c_project\pdfTest\";
            using (FileStream outFile = new FileStream(basePath + "forms.pdf", FileMode.Create))
            {
                // Pdf with form
                PdfReader pdfReader = new PdfReader(basePath + "deposit_with_forms.pdf");
                PdfStamper pdfStamper = new PdfStamper(pdfReader, outFile);

                AcroFields fields = pdfStamper.AcroFields;

                fields.SetField("hebDateTitle", "01/01/2023");
                fields.SetField("fullAddress", "נאגרה 38");
                fields.SetField("cityName", "ירושלים");
                fields.SetField("streetNo", "38");
                fields.SetField("entrance", "10");
                fields.SetField("balance", "5000");
                fields.SetField("totalAmount", "500");


                pdfStamper.Close();
                pdfReader.Close();
            }
        }

        private static void ConverrAspose()
        {
            string basePath = @"C:\c_project\pdfTest\";
            // Load PDF form contents
            FileStream fs = new FileStream(basePath + "result_bold.pdf", FileMode.Open, FileAccess.ReadWrite);


            // Instantiate Document instance with stream holding form file
            Aspose.Pdf.Document pdfDocument = new Aspose.Pdf.Document(fs);
            var f = pdfDocument.FontUtilities;
            
            // Get referecne of particuarl TextBoxField
            TextBoxField txtFld = pdfDocument.Form["t.z"] as TextBoxField;
            
            // Fill form field with arabic text
            txtFld.Value = "בדיקה";
            
            basePath = basePath + "ArabicTextFilling_out.pdf";
            // Save updated document
            pdfDocument.Save(basePath);
        }

    }
}
