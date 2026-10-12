// Title: Generate UPC‑A barcode with Code128 coupon and attach as PNG to email
// Description: Demonstrates creating a UPC‑A barcode that includes a GS1 Code128 coupon, converting it to a PNG byte array, and adding it as an email attachment.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, showcasing how to use BarcodeGenerator with EncodeTypes.UpcaGs1Code128Coupon, configure image parameters, and retrieve the result as a byte array. Typical use cases include embedding barcodes in communications such as emails or documents. Developers often need to generate barcodes, customize dimensions, and attach the generated images to messages using Aspose.BarCode and System.Net.Mail APIs.
// Prompt: Generate a UPC‑A barcode with a Code128 coupon, retrieve image as byte array, and attach to email.
// Tags: upc-a, code128, coupon, barcode generation, image png, email attachment, aspose.barcode, aspose.drawing

using System;
using System.IO;
using System.Net.Mail;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that generates a UPC‑A barcode with an embedded Code128 coupon,
/// converts the barcode to a PNG byte array, and prepares an email with the barcode as an attachment.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        // Define the barcode text: 12‑digit UPC‑A code with a GS1 Code128 coupon (AI 8102)
        string codeText = "012345678905(8102)03";

        // Generate the barcode image and store it in a byte array
        byte[] imageBytes;
        using (var generator = new BarcodeGenerator(EncodeTypes.UpcaGs1Code128Coupon, codeText))
        {
            // Set the X‑dimension (module width) to 2 pixels for better readability
            generator.Parameters.Barcode.XDimension.Pixels = 2;

            // Save the barcode to a memory stream in PNG format
            using (var ms = new MemoryStream())
            {
                generator.Save(ms, BarCodeImageFormat.Png);
                imageBytes = ms.ToArray(); // Retrieve the raw PNG bytes
            }
        }

        // Create an email message and attach the barcode image
        using (var attachmentStream = new MemoryStream(imageBytes))
        {
            using (var message = new MailMessage())
            {
                // Configure basic email fields
                message.From = new MailAddress("sender@example.com");
                message.To.Add("recipient@example.com");
                message.Subject = "UPC‑A with Code128 Coupon Barcode";
                message.Body = "Please find the generated barcode attached.";

                // Create the attachment from the PNG byte array
                var attachment = new Attachment(attachmentStream, "barcode.png", "image/png");
                message.Attachments.Add(attachment);

                // Output diagnostic information (email is not sent in this example)
                Console.WriteLine($"Email prepared. Barcode attachment size: {imageBytes.Length} bytes.");
                // Note: Sending the email is omitted as no SMTP server is configured.
            }
        }
    }
}