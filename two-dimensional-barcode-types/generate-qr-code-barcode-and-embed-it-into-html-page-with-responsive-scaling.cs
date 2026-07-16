// Title: Generate QR Code and embed in responsive HTML
// Description: Demonstrates creating a QR Code PNG using Aspose.BarCode and embedding it in an HTML page that scales responsively.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to use the BarcodeGenerator class to produce QR Code barcodes, configure size and error correction, and save the image. Typical use cases include creating scannable codes for URLs, contact info, or product data and displaying them on web pages with responsive design. Developers often need to generate barcode images programmatically and integrate them into HTML or other UI layers.
// Prompt: Generate QR Code barcode and embed it into an HTML page with responsive scaling.
// Tags: qr code, barcode generation, html embedding, responsive design, aspose.barcode, png

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that generates a QR Code image and creates an HTML page
/// that displays the image with responsive scaling.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a QR Code PNG file and writes
    /// an HTML file that references the image using responsive CSS.
    /// </summary>
    static void Main()
    {
        // Define output file paths (saved in the current working directory)
        string imagePath = Path.Combine(Directory.GetCurrentDirectory(), "qr.png");
        string htmlPath  = Path.Combine(Directory.GetCurrentDirectory(), "qr.html");

        // ------------------------------------------------------------
        // Generate a QR Code image using Aspose.BarCode
        // ------------------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.QR))
        {
            // Text to encode in the QR Code
            generator.CodeText = "https://example.com";

            // Configure image size via interpolation mode
            generator.Parameters.AutoSizeMode = AutoSizeMode.Interpolation;
            generator.Parameters.ImageWidth.Point  = 300f; // 300 points width
            generator.Parameters.ImageHeight.Point = 300f; // 300 points height

            // Optional: set error correction level (Level M = ~15% error recovery)
            generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelM;

            // Save the generated barcode as a PNG file
            generator.Save(imagePath);
        }

        // ------------------------------------------------------------
        // Build a simple HTML page that displays the QR code responsively
        // ------------------------------------------------------------
        string htmlContent = "<!DOCTYPE html>" +
                             "<html><head><meta charset=\"UTF-8\"><title>QR Code</title></head>" +
                             "<body style=\"margin:0;display:flex;justify-content:center;align-items:center;height:100vh;\">" +
                             $"<img src=\"{Path.GetFileName(imagePath)}\" style=\"max-width:100%;height:auto;\" alt=\"QR Code\"/>" +
                             "</body></html>";

        // Write the HTML content to the output file
        File.WriteAllText(htmlPath, htmlContent);
    }
}