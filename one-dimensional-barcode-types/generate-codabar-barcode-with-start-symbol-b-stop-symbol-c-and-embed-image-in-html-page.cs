// Title: Generate Codabar Barcode with Custom Start/Stop Symbols and Embed in HTML
// Description: This example creates a Codabar barcode using start symbol B and stop symbol C, saves it as a PNG image, and embeds the image in a simple HTML page.
// Category-Description: Demonstrates Aspose.BarCode generation for the Codabar symbology. It showcases the BarcodeGenerator class, setting Codabar-specific parameters (start/stop symbols), saving the barcode as an image, and producing an HTML file that references the image. Ideal for developers needing to integrate barcode images into web content or reports.
// Prompt: Generate a Codabar barcode with start symbol B, stop symbol C, and embed the image in an HTML page.
// Tags: codabar, barcode, generation, png, html, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates how to generate a Codabar barcode with custom start/stop symbols
/// and embed the resulting image into an HTML page using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates the barcode, saves it as PNG,
    /// creates an HTML file that references the image, and writes the HTML to disk.
    /// </summary>
    static void Main()
    {
        // Define file names for the barcode image and the HTML page.
        string imagePath = "codabar.png";
        string htmlPath = "barcode.html";

        // Create a Codabar barcode generator inside a using block to ensure proper disposal.
        using (var generator = new BarcodeGenerator(EncodeTypes.Codabar))
        {
            // Set the data to encode (the raw numeric string without start/stop symbols).
            generator.CodeText = "123456";

            // Configure the Codabar start and stop symbols: B for start, C for stop.
            generator.Parameters.Barcode.Codabar.StartSymbol = CodabarSymbol.B;
            generator.Parameters.Barcode.Codabar.StopSymbol = CodabarSymbol.C;

            // Save the generated barcode as a PNG image to the specified path.
            generator.Save(imagePath);
        }

        // Build a minimal HTML document that embeds the generated barcode image.
        string htmlContent = $@"<!DOCTYPE html>
<html>
<head>
    <meta charset=""UTF-8"">
    <title>Codabar Barcode</title>
</head>
<body>
    <h1>Codabar Barcode (Start: B, Stop: C)</h1>
    <img src=""{imagePath}"" alt=""Codabar Barcode"" />
</body>
</html>";

        // Write the HTML content to a file on disk.
        File.WriteAllText(htmlPath, htmlContent);
    }
}