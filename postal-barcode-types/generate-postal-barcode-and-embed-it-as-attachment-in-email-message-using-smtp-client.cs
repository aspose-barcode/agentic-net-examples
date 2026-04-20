using System;
using System.IO;
using System.Net;
using System.Net.Mail;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing.Imaging;

class Program
{
    static void Main()
    {
        // Generate a postal barcode (Postnet) and save it to a memory stream
        using (var barcodeStream = new MemoryStream())
        {
            using (var generator = new BarcodeGenerator(EncodeTypes.Postnet, "12345"))
            {
                // Set short bar height for postal barcode (example value)
                generator.Parameters.Barcode.Postal.ShortBarHeight.Point = 2f;

                // Save barcode image as PNG into the memory stream
                generator.Save(barcodeStream, BarCodeImageFormat.Png);
                barcodeStream.Position = 0;
            }

            // Prepare email message with the barcode as an attachment
            using (var message = new MailMessage())
            {
                message.From = new MailAddress("sender@example.com");
                message.To.Add(new MailAddress("recipient@example.com"));
                message.Subject = "Postal Barcode Attachment";
                message.Body = "Please find the generated postal barcode attached.";

                // Attach the barcode image from the memory stream
                var attachment = new Attachment(barcodeStream, "postal_barcode.png", "image/png");
                message.Attachments.Add(attachment);

                // Configure SMTP client (replace with actual SMTP server details)
                using (var smtp = new SmtpClient("localhost", 25))
                {
                    smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                    smtp.EnableSsl = false;
                    // If authentication is required, set credentials:
                    // smtp.Credentials = new NetworkCredential("username", "password");

                    try
                    {
                        smtp.Send(message);
                        Console.WriteLine("Email sent successfully.");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Failed to send email: " + ex.Message);
                    }
                }
            }
        }
    }
}