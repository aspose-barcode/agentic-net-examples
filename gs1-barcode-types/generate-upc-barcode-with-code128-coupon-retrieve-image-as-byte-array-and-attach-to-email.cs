// Title: Generate UPC‑A barcode with Code128 coupon and email attachment
// Description: Demonstrates creating a UPC‑A barcode that includes a Code128 coupon, converting it to a PNG byte array, and attaching it to an email message.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and image handling category. It showcases the use of BarcodeGenerator with EncodeTypes.UpcaGs1Code128Coupon, saving the barcode to a memory stream, and integrating the resulting image into a System.Net.Mail email. Developers often need to generate barcodes on‑the‑fly and embed them in communications such as order confirmations or promotional emails.
// Prompt: Generate a UPC‑A barcode with a Code128 coupon, retrieve image as byte array, and attach to email.
// Tags: upc-a, code128, barcode generation, image byte array, email attachment, aspose.barcode, system.net.mail

using System;
using System.IO;
using System.Net;
using System.Net.Mail;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that generates a UPC‑A barcode with an embedded Code128 coupon,
/// converts the barcode image to a byte array, and attaches it to an email message.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        // Create a UPC‑A barcode that includes a Code128 coupon.
        // Example codetext: "514141100906(8102)03"
        using (var generator = new BarcodeGenerator(EncodeTypes.UpcaGs1Code128Coupon, "514141100906(8102)03"))
        {
            // Save the generated barcode image to a memory stream in PNG format.
            using (var imageStream = new MemoryStream())
            {
                generator.Save(imageStream, BarCodeImageFormat.Png);
                byte[] imageBytes = imageStream.ToArray(); // Convert stream to byte array.

                // Prepare an email message and attach the barcode image.
                using (var mailMessage = new MailMessage())
                {
                    mailMessage.From = new MailAddress("sender@example.com");
                    mailMessage.To.Add("recipient@example.com");
                    mailMessage.Subject = "UPC‑A with Code128 Coupon Barcode";
                    mailMessage.Body = "Please find the generated barcode attached.";

                    // Attach the image using a new memory stream based on the byte array.
                    using (var attachmentStream = new MemoryStream(imageBytes))
                    {
                        var attachment = new Attachment(attachmentStream, "barcode.png", "image/png");
                        mailMessage.Attachments.Add(attachment);

                        // Send the email using an SMTP client.
                        // Replace host, port, and credentials with real values when deploying.
                        using (var smtpClient = new SmtpClient("smtp.example.com"))
                        {
                            smtpClient.Port = 25;
                            // smtpClient.Credentials = new NetworkCredential("username", "password");
                            // smtpClient.EnableSsl = true;

                            // Uncomment the line below to actually send the email.
                            // smtpClient.Send(mailMessage);
                        }
                    }
                }
            }
        }
    }
}