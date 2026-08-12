// Title: Generate QR Code and embed in responsive HTML page
// Description: This example creates a QR Code barcode, saves it as a PNG image, and generates an HTML file that displays the image with responsive scaling.
// Category-Description: Demonstrates Aspose.BarCode generation of QR Code symbology, configuring error correction and module size, and embedding the resulting image into an HTML page. Key API classes include BarcodeGenerator, EncodeTypes, BarCodeImageFormat, and generator parameters. Typical use cases involve creating scannable QR codes for web pages, marketing materials, or mobile apps where responsive display is required. Developers often need to customize barcode appearance and integrate the output into web content.
// Prompt: Generate QR Code barcode and embed it into an HTML page with responsive scaling.
// Tags: qr code, barcode generation, html embedding, responsive, aspose.barcode, png, csharp

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates generating a QR Code barcode and embedding it into a responsive HTML page.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates the QR Code image, creates an HTML file, and writes output paths to console.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for the output files
        string outputFolder = Path.Combine(Path.GetTempPath(), "AsposeBarcodeDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputFolder);

        // Define file paths for the QR image and the HTML page
        string imagePath = Path.Combine(outputFolder, "qr.png");
        string htmlPath = Path.Combine(outputFolder, "index.html");

        // Generate a QR Code barcode
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "https://example.com"))
        {
            // Set a high error correction level for better readability when the image is scaled
            generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelH;

            // Optionally set the module (dot) size
            generator.Parameters.Barcode.XDimension.Point = 3f;

            // Save the barcode as a PNG image
            generator.Save(imagePath, BarCodeImageFormat.Png);
        }

        // Build an HTML page that displays the QR code responsively
        string htmlContent = $@"<!DOCTYPE html>
<html>
<head>
    <meta charset=""utf-8"">
    <title>QR Code Demo</title>
    <style>
        .qr-img {{
            max-width: 100%;
            height: auto;
            display: block;
            margin: 0 auto;
        }}
        body {{
            font-family: Arial, Helvetica, sans-serif;
            text-align: center;
            padding: 20px;
        }}
    </style>
</head>
<body>
    <h1>Responsive QR Code</h1>
    <img src=""{Path.GetFileName(imagePath)}"" alt=""QR Code"" class=""qr-img"" />
</body>
</html>";

        // Write the HTML content to a file
        File.WriteAllText(htmlPath, htmlContent);

        // Inform the user where the files were saved
        Console.WriteLine("QR code image saved to: " + imagePath);
        Console.WriteLine("HTML page saved to: " + htmlPath);
    }
}