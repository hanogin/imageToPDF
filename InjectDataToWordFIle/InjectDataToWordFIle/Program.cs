using iTextSharp.text.pdf;
using System;
using System.IO;
using System.Reflection;
using System.Text;

namespace InjectDataToWordFIle
{
    class Program
    {
        static void Main(string[] args)
        {
            ConvertToPDF();
        }

        private static void ConvertToPDF()
        {
            string basePath = @"C:\c_project\pdfTest\";
            using (FileStream outFile = new FileStream(basePath + "formsResult.pdf", FileMode.Create))
            {
                // Pdf with form
                PdfReader pdfReader = new PdfReader(basePath + "receiving_donation_with_form.pdf");
                PdfStamper pdfStamper = new PdfStamper(pdfReader, outFile);

                AcroFields fields = pdfStamper.AcroFields;

                fields.SetField("entityCode", "1001");


                // First row
                int basadPadding = 80;
                string date = "01/01/2023";
                string besad_date = "בסד".PadRight(basadPadding) + "תאריך " + date;

                fields.SetField("besad_date", besad_date); // בסד ותאריך

                // Second row
                int fullNamePadding = 60;
                string identityType = "ת.ז";
                string id = "300397007";
                string fullName = "חנוך גינזבורג";
                string fullName_IdentityType = "נתקבל מאת: " + fullName.PadRight(fullNamePadding - fullName.Length) + identityType.PadRight(4)  + id;

                fields.SetField("fullName_IdentityType", fullName_IdentityType); // שם התורם


                // third row
                string addressLabel = "כתובת: ";
                string address = "נגארה 38 ירושלים";
                string fullAddress = addressLabel + address;

                fields.SetField("address", fullAddress); // כתובת


                // Forth row - pgone mobile
                int phonePading = 50;
                string phone = "039669578";
                string mobile = "0584782299";
                string fullMobile_Phone = "טלפון: " + phone.PadRight(phonePading - phone.Length) + "נייד: " + mobile;
                fields.SetField("phone_mobile", fullMobile_Phone); // טלפון ופלאפון


                fields.SetField("totalAmount", $"{5000:n}"); // סך התרומה
                fields.SetField("creditCardNumber", "1234-1234-1234-1234");

                pdfStamper.Close();
                pdfReader.Close();
            }
        }

        //private static void ConverrAspose()
        //{
        //    string basePath = @"C:\c_project\pdfTest\";
        //    // Load PDF form contents
        //    FileStream fs = new FileStream(basePath + "result_bold.pdf", FileMode.Open, FileAccess.ReadWrite);


        //    // Instantiate Document instance with stream holding form file
        //    Aspose.Pdf.Document pdfDocument = new Aspose.Pdf.Document(fs);
        //    var f = pdfDocument.FontUtilities;
            
        //    // Get referecne of particuarl TextBoxField
        //    TextBoxField txtFld = pdfDocument.Form["t.z"] as TextBoxField;
            
        //    // Fill form field with arabic text
        //    txtFld.Value = "בדיקה";
            
        //    basePath = basePath + "ArabicTextFilling_out.pdf";
        //    // Save updated document
        //    pdfDocument.Save(basePath);
        //}

    }
}
