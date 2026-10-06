// Title: Generate Codabar barcode with custom start/stop symbols and embed in HTML
// Description: This example creates a Codabar barcode using start symbol B and stop symbol C, saves it as a PNG image, converts the image to a Base64 string, and embeds it in a simple HTML page.
// Category-Description: Demonstrates Aspose.BarCode barcode generation (BarcodeGenerator, EncodeTypes) and image handling (BarCodeImageFormat, Aspose.Drawing) for web integration. Typical use cases include creating printable or web‑displayable barcodes with custom symbology settings, converting them to Base64 for inline HTML, and automating file output. Developers working with barcode generation, image processing, or HTML embedding will find this pattern useful.
// Prompt: Generate a Codabar barcode with start symbol B, stop symbol C, and embed the image in an HTML page.
// Tags: codabar, barcode, generation, image, html, base64, aspose.barcode, aspose.drawing

using System;
using System.IO;
using System.Text;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating a Codabar barcode with specific start/stop symbols,
/// converting it to Base64, and embedding it in an HTML page.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example.
    /// </summary>
    static void Main()
    {
        // Define output directory and file paths
        string outputDir = Path.Combine(Path.GetTempPath(), "CodabarExample");
        Directory.CreateDirectory(outputDir);
        string imagePath = Path.Combine(outputDir, "codabar.png");
        string htmlPath = Path.Combine(outputDir, "codabar.html");

        // Generate Codabar barcode with start symbol B and stop symbol C
        using (var generator = new BarcodeGenerator(EncodeTypes.Codabar, "12345"))
        {
            generator.Parameters.Barcode.Codabar.StartSymbol = CodabarSymbol.B;
            generator.Parameters.Barcode.Codabar.StopSymbol = CodabarSymbol.C;
            generator.Parameters.Barcode.XDimension.Pixels = 2f;
            generator.Save(imagePath, BarCodeImageFormat.Png);
        }

        // Read the generated PNG file and convert its bytes to a Base64 string
        byte[] imageBytes = File.ReadAllBytes(imagePath);
        string base64 = Convert.ToBase64String(imageBytes);

        // Build a simple HTML document that embeds the barcode image using a data URI
        StringBuilder htmlBuilder = new StringBuilder();
        htmlBuilder.AppendLine("<!DOCTYPE html>");
        htmlBuilder.AppendLine("<html lang=\"en\">");
        htmlBuilder.AppendLine("<head><meta charset=\"UTF-8\"><title>Codabar Barcode</title></head>");
        htmlBuilder.AppendLine("<body>");
        htmlBuilder.AppendLine("<h2>Codabar Barcode (Start B, Stop C)</h2>");
        htmlBuilder.AppendLine($"<img src=\"data:image/png;base64,{base64}\" alt=\"Codabar Barcode\" />");
        htmlBuilder.AppendLine("</body>");
        htmlBuilder.AppendLine("</html>");

        // Save the HTML file to disk
        File.WriteAllText(htmlPath, htmlBuilder.ToString());

        // Inform the user where the files were saved
        Console.WriteLine($"Barcode image saved to: {imagePath}");
        Console.WriteLine($"HTML page saved to: {htmlPath}");
    }
}