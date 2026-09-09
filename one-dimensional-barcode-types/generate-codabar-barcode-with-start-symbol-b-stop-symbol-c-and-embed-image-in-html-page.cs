// Title: Generate Codabar barcode with custom start/stop symbols and embed in HTML
// Description: This example creates a Codabar barcode using start symbol B and stop symbol C, saves it as a PNG file, and embeds the image directly into an HTML page via a Base64 data URI.
// Category-Description: Demonstrates Aspose.BarCode generation for Codabar symbology, covering the use of BarcodeGenerator, EncodeTypes, and BarCodeImageFormat classes. Typical scenarios include creating printable barcodes, embedding them in web pages, or integrating barcode images into reports. Developers working with barcode creation often need to customize symbology parameters and output formats, making this example a useful reference for quick implementation.
// Prompt: Generate a Codabar barcode with start symbol B, stop symbol C, and embed the image in an HTML page.
// Tags: codabar, barcode generation, html embedding, png, aspose.barcode, csharp

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates how to generate a Codabar barcode with specific start/stop symbols,
/// save it as an image, and embed the image into an HTML file using a Base64 data URI.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates the barcode, writes the PNG file,
    /// creates an HTML file with the embedded image, and outputs the file locations.
    /// </summary>
    static void Main()
    {
        // --------------------------------------------------------------------
        // Prepare the output directory where the PNG and HTML files will be saved.
        // --------------------------------------------------------------------
        string outputDir = Path.Combine(Directory.GetCurrentDirectory(), "output");
        if (!Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        // --------------------------------------------------------------------
        // Define barcode content and target file paths.
        // --------------------------------------------------------------------
        string codeText = "12345";
        string imagePath = Path.Combine(outputDir, "codabar.png");
        string htmlPath = Path.Combine(outputDir, "codabar.html");

        // --------------------------------------------------------------------
        // Create a BarcodeGenerator for Codabar symbology.
        // --------------------------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.Codabar, codeText))
        {
            // Optional: adjust the module (X) dimension for better visual quality.
            generator.Parameters.Barcode.XDimension.Pixels = 2f;

            // Set the start and stop symbols to B and C respectively.
            generator.Parameters.Barcode.Codabar.StartSymbol = CodabarSymbol.B;
            generator.Parameters.Barcode.Codabar.StopSymbol = CodabarSymbol.C;

            // ----------------------------------------------------------------
            // Save the barcode as a PNG file on disk.
            // ----------------------------------------------------------------
            generator.Save(imagePath, BarCodeImageFormat.Png);

            // ----------------------------------------------------------------
            // Also save the barcode to a memory stream to obtain a Base64 string.
            // ----------------------------------------------------------------
            using (var ms = new MemoryStream())
            {
                generator.Save(ms, BarCodeImageFormat.Png);
                byte[] imageBytes = ms.ToArray();
                string base64 = Convert.ToBase64String(imageBytes);

                // ----------------------------------------------------------------
                // Build a simple HTML page that embeds the barcode image using a data URI.
                // ----------------------------------------------------------------
                string htmlContent = $"<html><head><title>Codabar Barcode</title></head><body>" +
                                     $"<h2>Codabar Barcode (Start B, Stop C)</h2>" +
                                     $"<img src=\"data:image/png;base64,{base64}\" alt=\"Codabar Barcode\"/>" +
                                     $"</body></html>";

                // Write the HTML content to the output file.
                File.WriteAllText(htmlPath, htmlContent);
            }
        }

        // Inform the user where the files have been saved.
        Console.WriteLine($"Barcode image saved to: {imagePath}");
        Console.WriteLine($"HTML file saved to: {htmlPath}");
    }
}