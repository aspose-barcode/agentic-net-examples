using System;
using System.IO;
using System.Net.Mail;
using System.Net.Mime;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating a barcode, embedding it in an HTML email, and saving the email to a pickup directory.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates a barcode, creates an email with the barcode embedded as an inline image,
    /// and writes the email to a temporary pickup directory.
    /// </summary>
    static void Main()
    {
        // Generate a barcode image and keep it in a memory stream
        using (var barcodeStream = new MemoryStream())
        {
            // Create a barcode generator for Code128 with the value "123ABC"
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123ABC"))
            {
                // Save the barcode as PNG into the memory stream
                generator.Save(barcodeStream, BarCodeImageFormat.Png);
            }

            // Reset stream position for reading from the beginning
            barcodeStream.Position = 0;

            // Create a linked resource for the image with a content ID for inline display
            var barcodeImage = new LinkedResource(barcodeStream, MediaTypeNames.Image.Png)
            {
                ContentId = "barcodeImage",
                TransferEncoding = TransferEncoding.Base64
            };

            // Build the HTML body referencing the image via its content ID
            string htmlBody = @"<html>
                                <body>
                                    <h2>Embedded Barcode Example</h2>
                                    <p>Below is the generated barcode:</p>
                                    <img src=""cid:barcodeImage"" alt=""Barcode"" />
                                </body>
                               </html>";

            // Create an alternate view for HTML and attach the linked resource (the barcode image)
            var htmlView = AlternateView.CreateAlternateViewFromString(htmlBody, null, MediaTypeNames.Text.Html);
            htmlView.LinkedResources.Add(barcodeImage);

            // Prepare the email message
            using (var message = new MailMessage())
            {
                message.From = new MailAddress("sender@example.com");
                message.To.Add("recipient@example.com");
                message.Subject = "Barcode Embedded in Email";
                message.IsBodyHtml = true;
                message.AlternateViews.Add(htmlView);

                // Configure SMTP client to write the email to a pickup directory (no actual sending)
                string pickupDir = Path.Combine(Path.GetTempPath(), "EmailPickup");
                Directory.CreateDirectory(pickupDir);

                using (var smtp = new SmtpClient())
                {
                    smtp.DeliveryMethod = SmtpDeliveryMethod.SpecifiedPickupDirectory;
                    smtp.PickupDirectoryLocation = pickupDir;
                    smtp.Send(message);
                }

                // Inform the user where the email was saved
                Console.WriteLine($"Email saved to pickup directory: {pickupDir}");
            }
        }
    }
}