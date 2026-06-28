using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating a Codabar barcode using Aspose.BarCode,
/// saving it as an image, embedding it in an HTML file, and displaying file locations.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates a Codabar barcode with specific start/stop symbols,
    /// saves it as PNG, creates an HTML page with the barcode embedded as Base64,
    /// and writes the files to disk.
    /// </summary>
    static void Main()
    {
        // Define output file names
        string imagePath = "codabar.png";
        string htmlPath = "barcode.html";

        // Create a barcode generator for Codabar with the data "123456"
        using (var generator = new BarcodeGenerator(EncodeTypes.Codabar, "123456"))
        {
            // Configure start and stop symbols (B and C respectively)
            generator.Parameters.Barcode.Codabar.StartSymbol = CodabarSymbol.B;
            generator.Parameters.Barcode.Codabar.StopSymbol = CodabarSymbol.C;

            // Save the generated barcode as a PNG image
            generator.Save(imagePath, BarCodeImageFormat.Png);
        }

        // Load the saved image bytes from disk
        byte[] imageBytes = File.ReadAllBytes(imagePath);
        // Convert the image bytes to a Base64 string for embedding in HTML
        string base64Image = Convert.ToBase64String(imageBytes);

        // Build a simple HTML document that displays the barcode image
        string htmlContent = "<html><body>" +
                             "<h2>Codabar Barcode (Start: B, Stop: C)</h2>" +
                             $"<img src=\"data:image/png;base64,{base64Image}\" alt=\"Codabar Barcode\"/>" +
                             "</body></html>";

        // Write the HTML content to the specified file
        File.WriteAllText(htmlPath, htmlContent);

        // Output the full paths of the generated files for user reference
        Console.WriteLine($"Barcode image saved to: {Path.GetFullPath(imagePath)}");
        Console.WriteLine($"HTML page saved to: {Path.GetFullPath(htmlPath)}");
    }
}