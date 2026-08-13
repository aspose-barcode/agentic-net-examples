// Title: Generate UPC‑A barcode with Code128 coupon and attach as PNG to email
// Description: Demonstrates creating a UPC‑A barcode that includes a GS1‑128 coupon segment, converting it to a PNG byte array, and attaching it to an email message.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, showcasing how to use BarcodeGenerator with EncodeTypes.UpcaGs1Code128Coupon, customize visual parameters, export the image to a memory stream, and integrate the result into .NET email APIs. Developers often need to embed barcodes in communications such as order confirmations or promotional emails, requiring image extraction and attachment handling.
// Prompt: Generate a UPC‑A barcode with a Code128 coupon, retrieve image as byte array, and attach to email.
// Tags: upc-a, code128, coupon, barcode generation, image png, email attachment, aspose.barcode, aspose.drawing

using System;
using System.IO;
using System.Net.Mail;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that creates a UPC‑A barcode with an embedded Code128 coupon,
/// converts the barcode to a PNG byte array, and prepares an email with the image attached.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        // Define the UPC‑A code text that includes a Code128 coupon segment.
        // Example format: "514141100906(8102)03"
        string codeText = "514141100906(8102)03";

        // Initialize the barcode generator for UPC‑A with GS1‑128 coupon symbology.
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.UpcaGs1Code128Coupon, codeText))
        {
            // Optional visual customizations.
            generator.Parameters.Barcode.XDimension.Point = 2f;      // Set module (X) size.
            generator.Parameters.Barcode.BarHeight.Point = 50f;    // Set bar height for the linear part.
            generator.Parameters.BackColor = Aspose.Drawing.Color.White;
            generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Black;

            // Render the barcode to a memory stream in PNG format.
            using (MemoryStream imageStream = new MemoryStream())
            {
                generator.Save(imageStream, BarCodeImageFormat.Png);
                byte[] imageBytes = imageStream.ToArray(); // Retrieve the PNG image as a byte array.

                // Compose an email message and attach the barcode image.
                using (MailMessage mail = new MailMessage())
                {
                    mail.From = new MailAddress("sender@example.com");
                    mail.To.Add("recipient@example.com");
                    mail.Subject = "UPC‑A Barcode with Code128 Coupon";
                    mail.Body = "Please find the generated barcode attached.";

                    // Create an attachment from the image byte array.
                    using (MemoryStream attachmentStream = new MemoryStream(imageBytes))
                    {
                        Attachment attachment = new Attachment(attachmentStream, "barcode.png", "image/png");
                        mail.Attachments.Add(attachment);

                        // Demonstrate email preparation. In CI environments, sending is omitted.
                        using (SmtpClient smtp = new SmtpClient())
                        {
                            // Uncomment and configure the following lines for real email sending.
                            // smtp.Host = "smtp.example.com";
                            // smtp.Port = 587;
                            // smtp.Credentials = new System.Net.NetworkCredential("user", "password");
                            // smtp.EnableSsl = true;
                            // smtp.Send(mail);

                            Console.WriteLine("Email prepared with barcode attachment (size: {0} bytes).", imageBytes.Length);
                        }
                    }
                }
            }
        }
    }
}