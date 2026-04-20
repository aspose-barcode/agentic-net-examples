using System;
using System.IO;
using System.Net.Mail;
using System.Text;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

class Program
{
    static void Main()
    {
        // Create a barcode generator for QR code with sample text
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "https://example.com"))
        {
            // Set transparent background
            generator.Parameters.BackColor = Color.Transparent;

            // Generate the barcode image
            using (var bitmap = generator.GenerateBarCodeImage())
            {
                // Save the image to a memory stream in PNG format
                using (var imageStream = new MemoryStream())
                {
                    bitmap.Save(imageStream, ImageFormat.Png);
                    byte[] imageBytes = imageStream.ToArray();
                    string base64Image = Convert.ToBase64String(imageBytes);

                    // Build HTML body with embedded image
                    string htmlBody = $"<html><body><h2>Here is your barcode:</h2>" +
                                      $"<img src='data:image/png;base64,{base64Image}' alt='Barcode'/>" +
                                      $"</body></html>";

                    // Create an email message (not sent, just prepared)
                    using (var mail = new MailMessage())
                    {
                        mail.From = new MailAddress("sender@example.com");
                        mail.To.Add("recipient@example.com");
                        mail.Subject = "Barcode with Transparent Background";
                        mail.IsBodyHtml = true;
                        mail.Body = htmlBody;

                        // Output the HTML body to console for verification
                        Console.WriteLine("Generated HTML Email Body:");
                        Console.WriteLine(htmlBody);
                    }
                }
            }
        }
    }
}