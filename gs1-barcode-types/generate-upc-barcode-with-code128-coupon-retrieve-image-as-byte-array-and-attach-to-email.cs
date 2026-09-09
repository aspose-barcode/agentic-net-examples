// Title: Generate UPC‑A barcode with Code128 coupon and email attachment
// Description: Demonstrates creating a UPC‑A barcode that includes a GS1 Code128 coupon, converting it to a PNG byte array, and attaching it to an email message.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, showcasing the use of BarcodeGenerator with EncodeTypes.UpcaGs1Code128Coupon, image export via BarCodeImageFormat, and integration with System.Net.Mail for email delivery. Developers often need to produce combined symbology barcodes and send them as attachments in automated notifications or order processing workflows.
// Prompt: Generate a UPC‑A barcode with a Code128 coupon, retrieve image as byte array, and attach to email.
// Tags: upc-a, code128, coupon, barcode, image, email, aspose.barcode, png

using System;
using System.IO;
using System.Net.Mail;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that generates a UPC‑A barcode with an embedded Code128 coupon,
/// converts the barcode image to a PNG byte array, and prepares an email with the image attached.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        // Define the barcode text, including the GS1 coupon segment.
        string codeText = "123456789012(8110)ASPOSE";

        // Generate the barcode and capture the PNG image as a byte array.
        byte[] imageBytes;
        using (var generator = new BarcodeGenerator(EncodeTypes.UpcaGs1Code128Coupon, codeText))
        {
            // Set the X-dimension (module width) to 2 pixels for better readability.
            generator.Parameters.Barcode.XDimension.Pixels = 2;

            // Save the barcode to a memory stream in PNG format.
            using (var ms = new MemoryStream())
            {
                generator.Save(ms, BarCodeImageFormat.Png);
                imageBytes = ms.ToArray(); // Extract the byte array from the stream.
            }
        }

        // Compose an email message and attach the barcode image.
        using (var message = new MailMessage())
        {
            message.From = new MailAddress("sender@example.com");
            message.To.Add("recipient@example.com");
            message.Subject = "UPC‑A with Code128 Coupon Barcode";
            message.Body = "Please find the generated barcode attached.";

            // Create an attachment from the image byte array.
            using (var attachmentStream = new MemoryStream(imageBytes))
            {
                var attachment = new Attachment(attachmentStream, "barcode.png", "image/png");
                message.Attachments.Add(attachment);

                // Configure the SMTP client (placeholder configuration).
                using (var client = new SmtpClient("localhost"))
                {
                    // client.Send(message); // Uncomment when a valid SMTP server is available.
                    Console.WriteLine($"Email prepared with barcode attachment ({imageBytes.Length} bytes).");
                }
            }
        }
    }
}