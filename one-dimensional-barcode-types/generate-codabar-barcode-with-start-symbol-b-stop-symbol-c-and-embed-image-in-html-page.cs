// Title: Generate Codabar Barcode and Embed in HTML
// Description: Creates a Codabar barcode with start symbol B and stop symbol C, saves it as a PNG file, and embeds the image in a simple HTML page using a base64 data URI.
// Category-Description: This example belongs to the Aspose.BarCode generation category, demonstrating how to use the BarcodeGenerator class with EncodeTypes.Codabar to produce barcodes for web or print scenarios. Typical use cases include creating inventory labels, ticketing codes, or any application requiring Codabar symbology. Developers often need to customize dimensions, start/stop symbols, and embed generated images directly into HTML or other markup.
// Prompt: Generate a Codabar barcode with start symbol B, stop symbol C, and embed the image in an HTML page.
// Tags: codabar, barcode, generation, png, html, base64, aspose.barcode, csharp

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating a Codabar barcode with specific start/stop symbols and embedding it in an HTML page.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates the barcode image, converts it to Base64, and writes an HTML file displaying the barcode.
    /// </summary>
    static void Main()
    {
        // Define a temporary output directory for the generated files
        string outputDir = Path.Combine(Path.GetTempPath(), "CodabarExample");
        Directory.CreateDirectory(outputDir); // Ensure the directory exists

        // Paths for the barcode image and the resulting HTML page
        string imagePath = Path.Combine(outputDir, "codabar.png");
        string htmlPath = Path.Combine(outputDir, "codabar.html");

        // Create a barcode generator for Codabar symbology with the data "123456"
        using (var generator = new BarcodeGenerator(EncodeTypes.Codabar, "123456"))
        {
            // Set the X dimension (module width) to 2 pixels for better readability
            generator.Parameters.Barcode.XDimension.Pixels = 2f;

            // Configure start and stop symbols as required (B and C)
            generator.Parameters.Barcode.Codabar.StartSymbol = CodabarSymbol.B;
            generator.Parameters.Barcode.Codabar.StopSymbol = CodabarSymbol.C;

            // Save the generated barcode as a PNG image
            generator.Save(imagePath, BarCodeImageFormat.Png);
        }

        // Read the saved PNG file and convert its bytes to a Base64 string
        byte[] imgBytes = File.ReadAllBytes(imagePath);
        string base64 = Convert.ToBase64String(imgBytes);

        // Build a simple HTML page that embeds the barcode image using a data URI
        string htmlContent = $"<html><body><h1>Codabar Barcode (Start B, Stop C)</h1>" +
                             $"<img src=\"data:image/png;base64,{base64}\" alt=\"Codabar\"/></body></html>";

        // Write the HTML content to the output file
        File.WriteAllText(htmlPath, htmlContent);

        // Inform the user where the files have been saved
        Console.WriteLine($"Barcode image saved to: {imagePath}");
        Console.WriteLine($"HTML page saved to: {htmlPath}");
    }
}