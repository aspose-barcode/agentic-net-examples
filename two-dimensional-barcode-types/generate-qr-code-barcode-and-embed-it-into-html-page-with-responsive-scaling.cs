// Title: Generate QR Code and embed in responsive HTML
// Description: Demonstrates creating a QR Code barcode with Aspose.BarCode, converting it to a Base64 PNG, and embedding it in an HTML page that scales responsively.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, focusing on QR Code creation and image export. It showcases the BarcodeGenerator class, EncodeTypes enumeration, and BarCodeImageFormat for rendering barcodes as images, then using standard .NET APIs to embed the image in HTML. Developers often need to generate QR codes for web pages, emails, or mobile apps and require responsive display without separate image files.
// Prompt: Generate QR Code barcode and embed it into an HTML page with responsive scaling.
// Tags: qr code, barcode generation, html embedding, responsive, aspose.barcode, png, base64

using System;
using System.IO;
using System.Text;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that generates a QR Code barcode, encodes it as a Base64 PNG,
/// and writes an HTML file that displays the QR Code with responsive scaling.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        // Prepare a unique temporary output directory
        string outputDir = Path.Combine(Path.GetTempPath(), "QrHtmlDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputDir);

        // Text to encode in the QR Code
        string qrText = "https://example.com";

        // Create a QR Code generator with the desired content
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, qrText))
        {
            // Optional: configure module size and high error correction level
            generator.Parameters.Barcode.XDimension.Pixels = 4f;
            generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelH;

            // Render the QR Code into a memory stream as PNG
            using (var ms = new MemoryStream())
            {
                generator.Save(ms, BarCodeImageFormat.Png);
                ms.Position = 0; // Reset stream position for reading

                // Convert the PNG bytes to a Base64 string for embedding
                string base64 = Convert.ToBase64String(ms.ToArray());

                // Build an HTML page that displays the QR Code image responsively
                string html = $"<html><head><meta charset=\"UTF-8\"><title>QR Code</title></head>" +
                              $"<body style=\"margin:0;display:flex;justify-content:center;align-items:center;height:100vh;\">" +
                              $"<img src=\"data:image/png;base64,{base64}\" style=\"max-width:100%;height:auto;\" alt=\"QR Code\"/>" +
                              $"</body></html>";

                // Write the HTML file to the output directory
                string htmlPath = Path.Combine(outputDir, "qr.html");
                File.WriteAllText(htmlPath, html, Encoding.UTF8);

                // Inform the user where the HTML file was saved
                Console.WriteLine("QR code image and HTML page generated:");
                Console.WriteLine("Image (in memory, not saved as separate file).");
                Console.WriteLine($"HTML file: {htmlPath}");
            }
        }
    }
}