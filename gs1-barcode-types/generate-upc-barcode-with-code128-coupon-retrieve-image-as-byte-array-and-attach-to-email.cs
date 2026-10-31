// Title: Generate UPC‑A barcode with Code128 coupon and attach to email
// Description: Demonstrates creating a UPC‑A barcode that includes a Code128 coupon, converting it to a PNG byte array, and adding it as an email attachment.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, showcasing how to use BarcodeGenerator with EncodeTypes.UpcaGs1Code128Coupon, configure image parameters, and retrieve the barcode as a byte array for further processing such as email attachments. Developers often need to embed barcodes in communications, reports, or documents, and this pattern illustrates the typical workflow using Aspose.BarCode and System.Net.Mail APIs.
// Prompt: Generate a UPC‑A barcode with a Code128 coupon, retrieve image as byte array, and attach to email.
// Tags: upc-a, code128, coupon, barcode generation, byte array, email attachment, aspose.barcode, aspnet, c#

using System;
using System.IO;
using System.Net.Mail;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that generates a UPC‑A barcode with an embedded Code128 coupon,
/// converts the barcode image to a PNG byte array, and attaches it to an email message.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates the barcode, prepares the email with the barcode
    /// attached, and writes a confirmation message to the console.
    /// </summary>
    /// <param name="args">Command‑line arguments (not used).</param>
    static void Main(string[] args)
    {
        // Define the barcode text, including the GS1 coupon data.
        string codeText = "123456789012(8102)03";

        // Create a BarcodeGenerator for UPC‑A with a Code128 coupon.
        using (var generator = new BarcodeGenerator(EncodeTypes.UpcaGs1Code128Coupon, codeText))
        {
            // Set the X‑dimension (module width) to 2 pixels for better readability.
            generator.Parameters.Barcode.XDimension.Pixels = 2f;

            // Save the generated barcode to a memory stream in PNG format.
            using (var ms = new MemoryStream())
            {
                generator.Save(ms, BarCodeImageFormat.Png);
                // Retrieve the image bytes from the memory stream.
                byte[] barcodeBytes = ms.ToArray();

                // Prepare an email message and attach the barcode image.
                using (var message = new MailMessage())
                {
                    message.From = new MailAddress("sender@example.com");
                    message.To.Add("recipient@example.com");
                    message.Subject = "UPC‑A with Code128 Coupon Barcode";
                    message.Body = "Please find the barcode attached.";

                    // Create an attachment from the barcode byte array.
                    using (var attachmentStream = new MemoryStream(barcodeBytes))
                    {
                        var attachment = new Attachment(attachmentStream, "barcode.png", "image/png");
                        message.Attachments.Add(attachment);

                        // Email is prepared; sending is omitted for this example.
                        Console.WriteLine($"Email prepared with barcode attachment ({barcodeBytes.Length} bytes).");
                    }
                }
            }
        }
    }
}