using System;
using System.IO;
using System.Net.Mail;
using System.Net.Mime;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

class Program
{
    static void Main()
    {
        // Generate a barcode image in memory
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            // Optional: customize appearance
            generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Black;
            generator.Parameters.BackColor = Aspose.Drawing.Color.White;

            // Create bitmap and save to a memory stream as PNG
            using (Bitmap bitmap = generator.GenerateBarCodeImage())
            using (var imageStream = new MemoryStream())
            {
                bitmap.Save(imageStream, ImageFormat.Png);
                imageStream.Position = 0; // reset for reading

                // Build the email message
                using (var message = new MailMessage())
                {
                    message.From = new MailAddress("sender@example.com");
                    message.To.Add(new MailAddress("recipient@example.com"));
                    message.Subject = "Barcode Embedded in Email";

                    // HTML body referencing the embedded image via Content-ID
                    string htmlBody = @"<html><body>
                                        <h2>Here is your barcode:</h2>
                                        <img src=""cid:barcodeImage"" alt=""Barcode""/>
                                        </body></html>";

                    // Create an alternate view for HTML
                    AlternateView htmlView = AlternateView.CreateAlternateViewFromString(
                        htmlBody, null, MediaTypeNames.Text.Html);

                    // Create a linked resource for the barcode image
                    LinkedResource barcodeResource = new LinkedResource(imageStream, MediaTypeNames.Image.Png)
                    {
                        ContentId = "barcodeImage",
                        TransferEncoding = TransferEncoding.Base64
                    };

                    // Attach the image to the HTML view
                    htmlView.LinkedResources.Add(barcodeResource);
                    message.AlternateViews.Add(htmlView);

                    // Send the email to a pickup directory (no SMTP server required)
                    using (var client = new SmtpClient())
                    {
                        client.DeliveryMethod = SmtpDeliveryMethod.SpecifiedPickupDirectory;
                        client.PickupDirectoryLocation = Path.GetTempPath(); // writes .eml file
                        client.Send(message);
                    }
                }
            }
        }
    }
}