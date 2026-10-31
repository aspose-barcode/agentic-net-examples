// Title: Batch QR Code generation from URLs
// Description: Demonstrates how to generate QR code barcodes for a list of URLs and save each as a PNG file.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating batch creation of barcodes using the BarcodeGenerator class. It shows how to configure barcode parameters such as XDimension and how to output images in PNG format. Developers often need to automate barcode creation for multiple data items like URLs, product IDs, or inventory codes.
// Prompt: Batch generate barcodes from a list of URLs, using each URL as CodeText and saving as PNG files.
// Tags: qr, barcode, batch, png, aspose.barcodes, generation, code-text

using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode.Generation;

/// <summary>
/// Provides an example that generates QR code barcodes for a collection of URLs
/// and saves each barcode as a PNG image file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Iterates over a predefined list of URLs,
    /// creates a QR code for each, and writes the resulting images to a temporary folder.
    /// </summary>
    /// <param name="args">Command‑line arguments (not used).</param>
    static void Main(string[] args)
    {
        // Define the list of URLs to be encoded as QR codes.
        var urls = new List<string>
        {
            "https://example.com",
            "https://openai.com",
            "https://github.com",
            "https://dotnet.microsoft.com",
            "https://aspose.com"
        };

        // Create a unique temporary directory for the generated barcode images.
        string outputDir = Path.Combine(Path.GetTempPath(), "Barcodes_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputDir);
        Console.WriteLine($"Saving barcodes to: {outputDir}");

        // Iterate over each URL, generate a QR code, and save it as a PNG file.
        for (int i = 0; i < urls.Count; i++)
        {
            string url = urls[i];
            string fileName = $"barcode_{i + 1}.png";
            string filePath = Path.Combine(outputDir, fileName);

            try
            {
                // Initialize the barcode generator with QR encoding and the current URL as the code text.
                using (var generator = new BarcodeGenerator(EncodeTypes.QR, url))
                {
                    // Adjust the size of the QR code modules (pixels per module).
                    generator.Parameters.Barcode.XDimension.Pixels = 4f;

                    // Save the generated barcode image in PNG format.
                    generator.Save(filePath, BarCodeImageFormat.Png);
                }

                Console.WriteLine($"Generated: {filePath}");
            }
            catch (Exception ex)
            {
                // Log any errors that occur during barcode generation for the current URL.
                Console.WriteLine($"Failed to generate barcode for '{url}': {ex.Message}");
            }
        }
    }
}