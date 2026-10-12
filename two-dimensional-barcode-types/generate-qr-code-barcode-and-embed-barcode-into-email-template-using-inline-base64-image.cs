// Title: Generate QR Code and embed as Base64 image in email HTML
// Description: Demonstrates creating a QR Code barcode with Aspose.BarCode, converting it to a PNG, encoding it to Base64, and inserting it inline into an HTML email template.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and image handling category. It shows how to use BarcodeGenerator, set QR code parameters, save the barcode to a memory stream in PNG format, and embed the resulting image as a data URI. Developers often need to embed barcodes directly into HTML emails or web pages without external image files, and this pattern illustrates the typical workflow using Aspose.BarCode and Aspose.Drawing.Imaging.
// Prompt: Generate QR Code barcode and embed barcode into an email template using inline base64 image.
// Tags: qr code, barcode generation, base64, html email, aspose.barcode, png, memorystream

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates generating a QR Code barcode and embedding it as a Base64‑encoded PNG image within an HTML email template.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates the QR code, converts it to Base64, and prints the resulting HTML.
    /// </summary>
    static void Main()
    {
        // Text to encode in the QR code
        string codeText = "https://example.com";

        // Initialize the barcode generator for QR code symbology
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, codeText))
        {
            // Set the size of each QR module (pixel dimension)
            generator.Parameters.Barcode.XDimension.Pixels = 4f;

            // Configure error correction level (Level M provides a good balance of data capacity and resilience)
            generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelM;

            // Save the generated barcode to a memory stream in PNG format
            using (var ms = new MemoryStream())
            {
                generator.Save(ms, BarCodeImageFormat.Png);
                ms.Position = 0; // Reset stream position for reading

                // Convert the PNG image bytes to a Base64 string
                string base64 = Convert.ToBase64String(ms.ToArray());

                // Build an HTML email body with the barcode embedded as a data URI
                string emailHtml = $"<html><body><p>Hello, see QR code:</p><img src=\"data:image/png;base64,{base64}\" alt=\"QR Code\"/></body></html>";

                // Output the HTML to the console (could be sent as email body)
                Console.WriteLine(emailHtml);
            }
        }
    }
}