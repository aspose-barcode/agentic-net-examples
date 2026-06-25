using System;
using System.IO;
using System.Net.Mail;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.ComplexBarcode;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates generating a Royal Mail Mailmark barcode, attaching it to an email,
/// and sending the email via SMTP.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates a Mailmark barcode, creates an email
    /// with the barcode image attached, and sends it using an SMTP client.
    /// </summary>
    static void Main()
    {
        // Create Mailmark data (4‑state Royal Mail barcode)
        var mailmark = new MailmarkCodetext
        {
            // Format = 4 (4‑state), VersionID = 1, Class as string, SupplychainID, ItemID, DestinationPostCodePlusDPS
            Format = 4,
            VersionID = 1,
            Class = "0",
            SupplychainID = 384224,
            ItemID = 16563762,
            DestinationPostCodePlusDPS = "EF61AH8T "
        };

        // Generate the barcode image using ComplexBarcodeGenerator
        var complexGenerator = new ComplexBarcodeGenerator(mailmark);
        using (Bitmap barcodeImage = complexGenerator.GenerateBarCodeImage())
        {
            // Save image to a memory stream in PNG format
            using (var imageStream = new MemoryStream())
            {
                barcodeImage.Save(imageStream, ImageFormat.Png);
                imageStream.Position = 0; // Reset stream position for reading

                // Prepare email message
                using (var mail = new MailMessage())
                {
                    mail.From = new MailAddress("sender@example.com");
                    mail.To.Add("recipient@example.com");
                    mail.Subject = "Postal Barcode Attachment";
                    mail.Body = "Please find the generated postal barcode attached.";

                    // Attach the barcode image
                    var attachment = new Attachment(imageStream, "postal.png", "image/png");
                    mail.Attachments.Add(attachment);

                    // Configure SMTP client (replace with real server details)
                    using (var smtp = new SmtpClient("smtp.example.com", 25))
                    {
                        // Uncomment and set credentials if required
                        // smtp.Credentials = new System.Net.NetworkCredential("username", "password");
                        try
                        {
                            smtp.Send(mail);
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
}