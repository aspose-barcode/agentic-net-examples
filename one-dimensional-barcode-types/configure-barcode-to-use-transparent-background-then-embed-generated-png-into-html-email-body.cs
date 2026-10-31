// Title: Generate transparent Code128 barcode PNG and embed in HTML email body
// Description: This example creates a Code128 barcode with a transparent background, saves it as a PNG in memory, converts it to Base64, and embeds it into an HTML email body saved to a file.
// Category-Description: Demonstrates Aspose.BarCode image generation with custom appearance settings, covering barcode generation, image formatting, and embedding graphics in HTML. Uses BarcodeGenerator, BarCodeImageFormat, and Aspose.Drawing.Color classes. Typical for developers who need to create barcodes for emails, reports, or web pages and require transparent backgrounds for seamless integration.
// Prompt: Configure barcode to use transparent background, then embed generated PNG into an HTML email body.
// Tags: code128, barcode generation, transparent background, png, html email, aspose.barcode, aspose.drawing

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Example program that generates a barcode with a transparent background,
/// encodes it as a Base64 PNG, and writes an HTML file containing the image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    /// <param name="args">Command‑line arguments (not used).</param>
    static void Main(string[] args)
    {
        // The text to encode in the barcode.
        string codeText = "1234567890";

        // Initialize the barcode generator for Code128 symbology.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
        {
            // Set the background to transparent and the bar color to black.
            generator.Parameters.BackColor = Aspose.Drawing.Color.Transparent;
            generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Black;

            // Save the barcode image to a memory stream in PNG format.
            using (var ms = new MemoryStream())
            {
                generator.Save(ms, BarCodeImageFormat.Png);

                // Convert the image bytes to a Base64 string for embedding.
                byte[] imageBytes = ms.ToArray();
                string base64 = Convert.ToBase64String(imageBytes);

                // Build a simple HTML email body containing the barcode image.
                string html = $"<html><body><h1>Barcode Email</h1><img src=\"data:image/png;base64,{base64}\" alt=\"Barcode\"/></body></html>";

                // Write the HTML content to a file in the current directory.
                string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "email.html");
                File.WriteAllText(outputPath, html);

                // Inform the user where the HTML file was saved.
                Console.WriteLine($"HTML email body written to: {outputPath}");
            }
        }
    }
}