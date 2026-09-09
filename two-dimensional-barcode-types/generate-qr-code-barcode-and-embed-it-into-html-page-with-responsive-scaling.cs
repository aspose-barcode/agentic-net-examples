// Title: Generate QR Code and embed in responsive HTML page
// Description: Demonstrates creating a QR Code barcode image using Aspose.BarCode and embedding it into an HTML file that scales responsively.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, showcasing how to use the BarcodeGenerator class with QR symbology, configure parameters like XDimension and error correction level, and save the barcode as an image. Typical use cases include generating QR codes for URLs, product information, or authentication, and integrating them into web pages with responsive design. Developers often need to generate barcode images programmatically and embed them in HTML for cross‑platform display.
// Prompt: Generate QR Code barcode and embed it into an HTML page with responsive scaling.
// Tags: qr code, barcode generation, html embedding, responsive design, aspose.barcode, encode types, png output

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating a QR Code barcode image and embedding it into a responsive HTML page.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates QR code, saves image, creates HTML, and writes output paths.
    /// </summary>
    static void Main()
    {
        // Define output directory and ensure it exists
        string outputDir = Path.Combine(Directory.GetCurrentDirectory(), "output");
        Directory.CreateDirectory(outputDir);

        // Paths for the QR image and the HTML file
        string qrImagePath = Path.Combine(outputDir, "qr.png");
        string htmlPath = Path.Combine(outputDir, "qr.html");
        string codeText = "https://example.com";

        // Generate QR Code barcode with specific parameters
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, codeText))
        {
            // Set module size (XDimension) in pixels
            generator.Parameters.Barcode.XDimension.Pixels = 4f;
            // Use high error correction level
            generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelH;
            // Save the barcode as a PNG image
            generator.Save(qrImagePath, BarCodeImageFormat.Png);
        }

        // Build HTML content that references the generated QR image and scales responsively
        string htmlContent = $@"<!DOCTYPE html>
<html>
<head>
<meta charset=""UTF-8"">
<title>QR Code</title>
<style>
    .qr-img {{
        max-width: 100%;
        height: auto;
        display: block;
        margin: 0 auto;
    }}
</style>
</head>
<body>
<img src=""{Path.GetFileName(qrImagePath)}"" class=""qr-img"" alt=""QR Code""/>
</body>
</html>";

        // Write the HTML file to disk
        File.WriteAllText(htmlPath, htmlContent);

        // Output the locations of the generated files
        Console.WriteLine($"Generated QR code image at: {qrImagePath}");
        Console.WriteLine($"Generated HTML page at: {htmlPath}");
    }
}