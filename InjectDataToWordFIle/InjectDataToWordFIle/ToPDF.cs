//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace InjectDataToWordFIle
//{
//    public class ToPDF
//    {

//        Conversation opened. 2 messages. 2 messages unread.


//        Skip to content
//        Using Gmail with screen readers

//12 of 14
//(no subject)
//        Inbox


//        יעל גינזבורג
//12 Jan 2023, 07:03 (1 day ago)
//to me

//using GV.Common.Entities.Visit;
//using GV.SendEmail.Interfaces;
//using GV.SendEmail.Models;

//using Microsoft.Extensions.Configuration;
//using Microsoft.Extensions.Localization;
//using PuppeteerSharp;
//using PuppeteerSharp.Media;
//using Serilog;
//using System;
//using System.Collections.Generic;
//using System.Globalization;
//using System.IO;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace GV.SendEmail.Services
//    {
//        public class PdfPuppeteerService : IPdfPuppeteerService
//        {
//            private IConfiguration configuration;
//            private ILogger logger;
//            private IStringLocalizer<PdfPuppeteerService> loc;
//            BrowserFetcher browserFetcher;
//            Browser browser;
//            Page page;
//            private string lang;

//            public PdfPuppeteerService(IConfiguration configuration, Serilog.ILogger logger, IStringLocalizer<PdfPuppeteerService> loc
//    )
//            {

//                this.configuration = configuration;
//                this.logger = logger;
//                this.loc = loc;

//            }

//            public async Task<string> GenerateHtmlBody(Email email, Service serviceData)
//            {


//                var options = new LaunchOptions
//                {
//                    Headless = true,
//                    ExecutablePath = "C:\\Program Files (x86)\\Google\\Chrome\\Application\\chrome.exe"
//                };
//                browserFetcher = new BrowserFetcher();
//                browser = await Puppeteer.LaunchAsync(options);
//                page = await browser.NewPageAsync();

//                string template = await File.ReadAllTextAsync("./Pdf/" + email.templateBody + ".html");


//                var dir = email.lang == "he" || email.lang == "ar" ? "rtl" : "ltr";
//                template = "";//replaceParameters(template, email.visit, serviceData, dir, "");

//                browserFetcher.Dispose();
//                browser.Process.Kill();

//                return template;
//            }
//            public async Task<byte[]> GeneratePdf(Models.Visit visitDetails, Email email, Service serviceData)
//            {

//                try
//                {
//                    var options = new LaunchOptions
//                    {
//                        Headless = true,
//                        ExecutablePath = "C:\\Program Files (x86)\\Google\\Chrome\\Application\\chrome.exe"


//                    };
//                    browserFetcher = new BrowserFetcher();

//                    try
//                    {
//                        browser = await Puppeteer.LaunchAsync(options);
//                    }
//                    catch (Exception e)
//                    {
//                        Log.Error(e.Message);
//                    }

//                    page = await browser.NewPageAsync();
//                    string template;
//                    try
//                    {
//                        template = await File.ReadAllTextAsync("./Pdf/" + email.templateAttachment + ".html");
//                    }
//                    catch (Exception e)
//                    {
//                        template = await File.ReadAllTextAsync("./Pdf/" + email.templateAttachment + ".html");
//                    }
//                    var dir = email.lang == "he" || email.lang == "ar" ? "rtl" : "ltr";
//                    this.lang = email.lang;
//                    template = replaceParameters(template, visitDetails, serviceData, dir, "", email.lang);
//                    string ticksPath = (DateTime.Now.Ticks) + ".html";

//                    await File.WriteAllTextAsync("./" + ticksPath, template);



//                    await page.GoToAsync("file:///" + Path.Combine(Directory.GetCurrentDirectory(), ticksPath).Replace("\\", "/"));

//                    var pdfOptionsJson = getOptionsPDF(false);

//                    byte[] _Pdf = await page.PdfDataAsync(pdfOptionsJson);
//                    //            const h4All = await page.$$('h4');
//                    //const h4Count = h4All.length;
//                    //const fileName = h4All[h4Count - 1];


//                    if (File.Exists("./" + ticksPath)) { File.Delete("./" + ticksPath); }

//                    browserFetcher.Dispose();
//                    browser.Process.Kill();
//                    return _Pdf;
//                }
//                catch (Exception ex)
//                {

//                    throw;

//                }

//            }
//            public async Task<byte[]> DownloadPdf(Models.Visit visitDetails, string lang, Service serviceData)
//            {

//                try
//                {
//                    var options = new LaunchOptions
//                    {
//                        Headless = true,
//                        ExecutablePath = "C:\\Program Files (x86)\\Google\\Chrome\\Application\\chrome.exe"


//                    };
//                    browserFetcher = new BrowserFetcher();

//                    try
//                    {
//                        browser = await Puppeteer.LaunchAsync(options);
//                    }
//                    catch (Exception e)
//                    {
//                        Log.Error(e.Message);
//                    }

//                    page = await browser.NewPageAsync();
//                    string template;

//                    template = await File.ReadAllTextAsync("./Pdf/template.html");


//                    var dir = lang == "he" || lang == "ar" ? "rtl" : "ltr";
//                    this.lang = lang;
//                    template = replaceParameters(template, visitDetails, serviceData, dir, "", lang);
//                    string ticksPath = (DateTime.Now.Ticks) + ".html";

//                    await File.WriteAllTextAsync("./" + ticksPath, template);



//                    await page.GoToAsync("file:///" + Path.Combine(Directory.GetCurrentDirectory(), ticksPath).Replace("\\", "/"));

//                    var pdfOptionsJson = getOptionsPDF(false);

//                    byte[] _Pdf = await page.PdfDataAsync(pdfOptionsJson);

//                    if (File.Exists("./" + ticksPath)) { File.Delete("./" + ticksPath); }

//                    browserFetcher.Dispose();
//                    browser.Process.Kill();
//                    return _Pdf;
//                }
//                catch (Exception ex)
//                {

//                    throw;

//                }

//            }

//            public string replaceParameters(string template, Models.Visit visit, Service serviceData, string dir, string branchUrl, string lang)
//            {
//                try
//                {
//                    var a = loc["appointment_details", lang].Value;
//                    var t = template.Replace("#serviceName#", visit.location != null ? $"{a}\r\n" + serviceData.Name : serviceData.Name)
//                                             .Replace("#officeName#", visit.organizationName != null ? visit.organizationName : serviceData.Ministry.Name)
//                                             //.Replace("#date#", visit.dateTime.ToString("dd/M/yyyy"))
//                                             .Replace("#date#", visit.dateTime.ToString("ddd, dd MMMM yyyy", new CultureInfo(lang)))
//                                             .Replace("#time#", visit.dateTime.ToString("HH:mm"))
//                                             .Replace("#serviceLink#", serviceData.GovServiceLink != null ? serviceData.GovServiceLink : "")
//                                             .Replace("#noteComment#", serviceData.Comment != null ? serviceData.Comment : "")
//                                             .Replace("#commentHeader#", serviceData.CommentHeader != null ? serviceData.CommentHeader : "")

//                                             .Replace("#branchName#", visit.location != null ? visit.location.name : "")
//                                             .Replace("#address#", visit.location != null ? visit.location.addresses[0] : "")
//                                             .Replace("#city#", visit.location != null ? visit.location.city : "")
//                                             .Replace("#start#", serviceData.Ministry.ActivityHoursStart != null ? serviceData.Ministry.ActivityHoursStart : "")
//                                             .Replace("#end#", serviceData.Ministry.ActivityHoursEnd != null ? serviceData.Ministry.ActivityHoursEnd : "")
//                                             .Replace("#start1#", serviceData.Ministry.WeekendActivityHoursStart != null ? serviceData.Ministry.WeekendActivityHoursStart : "")
//                                             .Replace("#end1#", serviceData.Ministry.WeekendActivityHoursStart != null ? serviceData.Ministry.WeekendActivityHoursStart : "")
//                                             .Replace("#comment#", serviceData.Ministry.Comment != null ? serviceData.Ministry.Comment : "")
//                                             .Replace("#comment#", serviceData.Ministry.Comment != null ? serviceData.Ministry.Comment : "")
//                                             .Replace("#phoneNumber#", serviceData.Ministry.PhoneNumber != null ? serviceData.Ministry.PhoneNumber : "")
//                                             .Replace("#email#", serviceData.Ministry.Email != null ? serviceData.Ministry.Email : "")
//                                             .Replace("#moreMinistryInfo#", moreInfo(branchUrl))
//                                             .Replace("#moreMinistryInfoB#", moreInfoBody(branchUrl))
//                                             .Replace("#wase#", wazeMobileBody(visit))
//                                             .Replace("#waseBody#", wazeMobileBody(visit))
//                                             .Replace("#map#", map(visit))
//                                             .Replace("#cancel#", cancel(visit.id))
//                                             .Replace("#cancelT#", cancelT(visit.id))
//                                             .Replace("#cancelMobile#", cancelMobile(visit.id))

//                                             .Replace("#mapBody#", mapBody(visit))
//                                             .Replace("#list#", buildList(serviceData.Files, dir))
//                                             .Replace("#fee#", fee(serviceData))
//                                             .Replace("#feeBody#", feeBody(serviceData))
//                                             .Replace("#refServiceLink#", serviceData.GovServiceLink != null ? refServiceLink(serviceData.GovServiceLink) : "")
//                                             .Replace("#refLinkBody#", serviceData.GovServiceLink != null ? refLinkBody(serviceData.GovServiceLink) : "")
//                                             .Replace("#showNote#", showNote(serviceData.CommentHeader, serviceData.Comment))
//                                             .Replace("#emailSection#", emailSection(serviceData.Ministry.Email))

//                                             ////microcopy
//                                             .Replace("#my_visits_get_ready_title#", loc["my_visits_get_ready_title", lang])
//                                             .Replace("#more_service_info#", loc["more_service_info", lang])
//                                             .Replace("#contact_details#", loc["contact_details", lang])
//                                             .Replace("#full_office_info#", loc["full_office_info", lang])
//                                                                      .Replace("#telephone_answer_time#", loc["telephone_answer_time", lang])
//                                                                      .Replace("#Sun_to_Thu#", loc["Sun_to_Thu", lang])
//                                                                      .Replace("#between#", loc["between", lang])
//                                                                      .Replace("#to_hour#", loc["to_hour", lang])
//                                                                      .Replace("#Fri_and_holiday_eves#", loc["Fri_and_holiday_eves", lang])
//                                                                      .Replace("#also_in#", loc["also_in", lang])
//                                                                      .Replace("dir='auto'", $"dir='{dir}'");


//                    return t;
//                }

//                catch (Exception ex)
//                {
//                    throw new Exception(ex.Message);

//                }
//            }

//            private string emailSection(string email)
//            {
//                if (email == "") return "";
//                return "<div class='d-flex ' style='margin-top:24px;'>" +
//                        "<div class='d-flex   align-self-center'>" +
//                            "<img class='icon-medium' src='./Pdf/img/email.svg' alt=''>" +

//                        "</div><div class='d-flex flex-column mx-8px ' dir='auto'>" +
//                          "  <span class='blue-link label-16px' style='text-decoration:underline'>" +
//                                email +
//                            "</span>" +

//                       " </div>" +
//                    "</div>";
//            }

//            private string showNote(string header = "", string comment = "")
//            {
//                string section = "";
//                if (header == "" & comment == "") return section;

//                if (header != "" && comment != "")
//                {
//                    section = $"<div class='note br-8px d-flex flex-column mx-88px mb-24px' dir='auto'>" +
//                   $" <div class='my-16px  d-flex flex-column '>" +
//                        $"<div class='d-flex '>" +




//            private PdfOptions getOptionsPDF(bool temporary)
//            {
//                var pdfOptionsJson = new PdfOptions();
//                pdfOptionsJson.Scale = new decimal(1.0);
//                pdfOptionsJson.DisplayHeaderFooter = true;
//                pdfOptionsJson.PrintBackground = true;
//                pdfOptionsJson.Landscape = false;
//                pdfOptionsJson.PageRanges = "";
//                pdfOptionsJson.Format = PaperFormat.A4;
//                pdfOptionsJson.Width = null;
//                pdfOptionsJson.Height = null;
//                pdfOptionsJson.MarginOptions = new MarginOptions { Top = (temporary ? "92" : "20") + "px", Left = "0px", Bottom = "20px", Right = "0px" };
//                pdfOptionsJson.PreferCSSPageSize = false;

//                return pdfOptionsJson;
//            }


//        }

//    }

//    }
//}


//using PuppeteerSharp;
//using PuppeteerSharp.Media;
//using System;
//using System.IO;

//namespace InjectDataToWordFIle
//{
//    class Program
//    {
//        static async System.Threading.Tasks.Task Main(string[] args)
//        {
//            BrowserFetcher browserFetcher;
//            Browser browser;
//            Page page;
//            string lang;

//            var options = new LaunchOptions
//            {
//                Headless = true,
//                ExecutablePath = "C:\\Program Files\\Google\\Chrome\\Application\\chrome.exe"


//            };
//            browserFetcher = new BrowserFetcher();


//            browser = (Browser)await Puppeteer.LaunchAsync(options);



//            page = (Page)await browser.NewPageAsync();




//            string template;

//            template = File.ReadAllText(@"D:\Programing\Moses\InvoicePic\index.html");

//            template = replaceParameters(template);
//            string ticksPath = (DateTime.Now.Ticks) + ".html";

//            File.WriteAllText(@"D:\Programing\Moses\InvoicePic\" + ticksPath, template);

//            await page.GoToAsync("http://127.0.0.1:5500/" + ticksPath);

//            //string ticksPath = (DateTime.Now.Ticks) + ".html";

//            await page.PdfAsync(@"D:\Programing\Moses\InvoicePic\" + ticksPath + ".pdf", new PdfOptions
//            {
//                Format = PaperFormat.A4,
//            });


//            //await page.GoToAsync(Path.Combine(@"D:\Programing\Moses\InvoicePic\" + ticksPath));

//            //await page.AddStyleTagAsync(@"D:\Programing\Moses\InvoicePic\style.css");

//            //var pdfOptionsJson = getOptionsPDF();

//            //byte[] _Pdf = await page.PdfDataAsync(pdfOptionsJson);

//            //File.WriteAllBytes(@"D:\Programing\Moses\InvoicePic\" + ticksPath + ".pdf", _Pdf);

//            //if (File.Exists("./" + ticksPath)) { File.Delete("./" + ticksPath); }

//            browserFetcher.Dispose();
//            browser.Process.Kill();
//        }


//        static string replaceParameters(string template)
//        {
//            string result = template.Replace("#invoiceId#", "1001")
//                .Replace("#invoiceId#", "1001")
//                .Replace("#date#", "13/01/2023")
//                .Replace("#fullName#", "גינזבורג שלי")
//                .Replace("#tz#", "123456789")
//                .Replace("#address#", "שאגת אריה 17")
//                .Replace("#apartment#", "4")
//                .Replace("#city#", "מודיעין עילית")
//                .Replace("#phone#", "0527131591")
//                .Replace("#mobile#", "0522610415 ")
//                .Replace("#sum#", "1765")
//                .Replace("#balance#", "5000");

//            return result;
//        }

//        static PdfOptions getOptionsPDF()
//        {
//            var pdfOptionsJson = new PdfOptions();
//            //pdfOptionsJson.Scale = new decimal(1.0);
//            pdfOptionsJson.DisplayHeaderFooter = false;
//            pdfOptionsJson.PrintBackground = true;
//            pdfOptionsJson.Landscape = false;
//            pdfOptionsJson.PageRanges = "";
//            pdfOptionsJson.Format = PaperFormat.A4;
//            pdfOptionsJson.Width = null;
//            pdfOptionsJson.Height = null;
//            //pdfOptionsJson.MarginOptions = new MarginOptions { Top = (temporary ? "92" : "20") + "px", Left = "0px", Bottom = "20px", Right = "0px" };
//            pdfOptionsJson.PreferCSSPageSize = false;

//            return pdfOptionsJson;
//        }

//    }
//}
