// Title: Generate Codabar barcode with custom start/stop symbols and embed in HTML
// Description: Creates a Codabar barcode using start symbol B and stop symbol C, converts it to a PNG image, encodes it as Base64, and embeds it in a simple HTML file.
// Category-Description: This example demonstrates Aspose.BarCode generation of one-dimensional barcodes, focusing on Codabar symbology. It shows how to configure barcode parameters, render the image to a stream, and embed the result in HTML. Developers working with barcode creation, image handling, and web integration commonly use BarcodeGenerator, EncodeTypes, BarCodeImageFormat, and related parameter classes.
// Prompt: Generate a Codabar barcode with start symbol B, stop symbol C, and embed the image in an HTML page.
// Tags: codabar, barcode, generation, png, html, aspose.barcode, aspose.drawing

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates how to generate a Codabar barcode with specific start/stop symbols,
/// convert it to a PNG image, and embed the image directly into an HTML file using Base64 encoding.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates the barcode, creates the HTML, and writes the output file.
    /// </summary>
    static void Main()
    {
        // Define the raw barcode data (without start/stop symbols)
        const string codeText = "123456";

        // Initialize the barcode generator for Codabar symbology
        using (var generator = new BarcodeGenerator(EncodeTypes.Codabar))
        {
            // Assign the data to be encoded
            generator.CodeText = codeText;

            // Configure start and stop symbols as required (B and C)
            generator.Parameters.Barcode.Codabar.StartSymbol = CodabarSymbol.B;
            generator.Parameters.Barcode.Codabar.StopSymbol = CodabarSymbol.C;

            // Render the barcode to a memory stream in PNG format
            using (var ms = new MemoryStream())
            {
                generator.Save(ms, BarCodeImageFormat.Png);
                byte[] imageBytes = ms.ToArray();

                // Convert the PNG bytes to a Base64 string for embedding
                string base64 = Convert.ToBase64String(imageBytes);

                // Build a minimal HTML page that displays the barcode image
                string html = $"<html><body><h2>Codabar Barcode (Start B, Stop C)</h2>" +
                              $"<img src=\"data:image/png;base64,{base64}\" alt=\"Codabar Barcode\"/>" +
                              $"</body></html>";

                // Write the HTML content to a file named 'barcode.html'
                File.WriteAllText("barcode.html", html);
            }
        }

        // Inform the user that the operation completed successfully
        Console.WriteLine("Barcode image embedded in 'barcode.html'.");
    }
}