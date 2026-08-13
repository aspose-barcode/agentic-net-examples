// Title: Generate QR Code and Embed as Base64 Image in Email HTML
// Description: Demonstrates creating a QR code with Aspose.BarCode, converting it to a Base64 PNG, and embedding it inline in an HTML email template.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category. It shows how to use the BarcodeGenerator class with EncodeTypes.QR to produce a QR code, adjust its error correction level, save the image in PNG format, and transform the binary data into a Base64 string for inline display. Typical use cases include embedding barcodes in HTML emails, web pages, or reports without external image files. Developers often need to combine barcode creation with image encoding for seamless integration into markup.
// Prompt: Generate QR Code barcode and embed barcode into an email template using inline base64 image.
// Tags: qr code, barcode generation, base64, email, html, aspose.barcode, png

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that generates a QR code, converts it to a Base64 PNG image,
/// and embeds it into an HTML email template.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates the QR code, encodes it, and prints the HTML.
    /// </summary>
    /// <param name="args">Command‑line arguments (not used).</param>
    static void Main(string[] args)
    {
        // Define the content that will be encoded in the QR code.
        string qrContent = "https://example.com";

        // Initialize the QR code generator with the desired content.
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, qrContent))
        {
            // Configure a high error‑correction level to improve scan reliability.
            generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelH;

            // Save the generated barcode to a memory stream in PNG format.
            using (var memoryStream = new MemoryStream())
            {
                generator.Save(memoryStream, BarCodeImageFormat.Png);
                byte[] imageBytes = memoryStream.ToArray();

                // Convert the PNG byte array to a Base64 string for inline embedding.
                string base64Image = Convert.ToBase64String(imageBytes);

                // Build a simple HTML email template that includes the Base64 image.
                string emailHtml = $@"
<html>
  <body>
    <p>Hello,</p>
    <p>Here is your QR code:</p>
    <img src=""data:image/png;base64,{base64Image}"" alt=""QR Code"" />
  </body>
</html>";

                // Output the generated HTML to the console (or redirect as needed).
                Console.WriteLine(emailHtml);
            }
        }
    }
}