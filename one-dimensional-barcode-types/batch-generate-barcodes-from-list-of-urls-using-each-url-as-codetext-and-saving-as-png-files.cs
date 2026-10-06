// Title: Batch generate Code128 barcodes from URLs and save as PNG files
// Description: Demonstrates how to create a barcode for each URL in a list using Aspose.BarCode, saving each image as a PNG file in a temporary folder.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating the use of BarcodeGenerator, EncodeTypes, and BarCodeImageFormat to produce barcode images. Typical scenarios include bulk barcode creation for inventory, marketing URLs, or QR code alternatives. Developers often need to iterate over data collections, configure barcode parameters, and persist images in common formats.
// Prompt: Batch generate barcodes from a list of URLs, using each URL as CodeText and saving as PNG files.
// Tags: barcode symbology, batch generation, png output, aspose.barcode, code128, generation

using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates batch generation of Code128 barcodes from a collection of URLs.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that creates a temporary folder, iterates over a list of URLs,
    /// generates a Code128 barcode for each, and saves the result as a PNG file.
    /// </summary>
    static void Main()
    {
        // Define a sample list of URLs to be encoded as barcodes.
        List<string> urls = new List<string>
        {
            "https://example.com",
            "https://openai.com",
            "https://github.com",
            "https://dotnet.microsoft.com",
            "https://aspose.com"
        };

        // Create a unique temporary output folder for the generated barcode images.
        string outputFolder = Path.Combine(Path.GetTempPath(), "Barcodes_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputFolder);
        Console.WriteLine("Barcodes will be saved to: " + outputFolder);

        int index = 1;
        // Iterate through each URL, generate a barcode, and save it as a PNG file.
        foreach (string url in urls)
        {
            string fileName = $"barcode_{index}.png";
            string filePath = Path.Combine(outputFolder, fileName);
            try
            {
                // Initialize the generator with Code128 symbology and the URL as the code text.
                using (var generator = new BarcodeGenerator(EncodeTypes.Code128, url))
                {
                    // Optional: adjust the barcode's X-dimension (module width) for better readability.
                    generator.Parameters.Barcode.XDimension.Pixels = 2f;

                    // Save the generated barcode image to the specified path in PNG format.
                    generator.Save(filePath, BarCodeImageFormat.Png);
                }
                Console.WriteLine($"Generated barcode for \"{url}\" -> {fileName}");
            }
            catch (Exception ex)
            {
                // Log any errors that occur during barcode generation for the current URL.
                Console.WriteLine($"Failed to generate barcode for \"{url}\": {ex.Message}");
            }
            index++;
        }

        Console.WriteLine("Batch barcode generation completed.");
    }
}