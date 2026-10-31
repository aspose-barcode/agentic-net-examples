// Title: Generate QR Code and embed in responsive HTML
// Description: This example creates a QR Code barcode for a URL, encodes it as PNG, converts it to a Base64 string, and embeds it in an HTML page that scales responsively.
// Category-Description: Demonstrates Aspose.BarCode barcode generation (BarcodeGenerator, EncodeTypes), image handling (BarCodeImageFormat, MemoryStream), and embedding techniques (Base64, HTML). Typical use cases include creating scannable QR codes for web pages, marketing materials, or mobile apps where developers need to generate barcodes on‑the‑fly and display them in responsive web content. This example belongs to the “Barcode Generation and Rendering” collection of Aspose.BarCode samples, useful for developers working with QR, DataMatrix, or other 2‑D symbologies.
// Prompt: Generate QR Code barcode and embed it into an HTML page with responsive scaling.
// Tags: qr code, barcode generation, html embedding, responsive, aspose.barcode, png, base64

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates generating a QR Code barcode and embedding it into a responsive HTML page.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that creates the QR Code, saves it as an embedded image in HTML, and writes the file.
    /// </summary>
    static void Main()
    {
        // Prepare output directory
        string outputDir = Path.Combine(Directory.GetCurrentDirectory(), "output");
        if (!Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        // Generate QR Code barcode for the specified URL
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "https://example.com"))
        {
            // Optional: configure module size and error correction level
            generator.Parameters.Barcode.XDimension.Pixels = 4f;
            generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelH;

            // Save barcode image to a memory stream in PNG format
            using (var ms = new MemoryStream())
            {
                generator.Save(ms, BarCodeImageFormat.Png);
                ms.Position = 0;

                // Convert the PNG image to a Base64 string for embedding
                string base64 = Convert.ToBase64String(ms.ToArray());

                // Build responsive HTML that embeds the Base64 image
                string htmlContent = $@"<!DOCTYPE html>
<html>
<head>
    <meta charset=""UTF-8"">
    <title>QR Code</title>
    <style>
        body {{ margin:0; display:flex; justify-content:center; align-items:center; height:100vh; background:#f0f0f0; }}
        img {{ max-width:100%; height:auto; }}
    </style>
</head>
<body>
    <img src=""data:image/png;base64,{base64}"" alt=""QR Code"" />
</body>
</html>";

                // Write the HTML file to the output directory
                string htmlPath = Path.Combine(outputDir, "qr.html");
                File.WriteAllText(htmlPath, htmlContent);
                Console.WriteLine($"QR code HTML generated at: {htmlPath}");
            }
        }
    }
}