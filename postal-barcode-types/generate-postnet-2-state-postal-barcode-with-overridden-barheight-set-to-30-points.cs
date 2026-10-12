// Title: Generate Postnet 2‑state barcode with custom bar height
// Description: Demonstrates creating a Postnet 2‑state postal barcode and setting its bar height to 30 points, then saving it as a PNG image.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to use the BarcodeGenerator class with EncodeTypes.Postnet to produce postal barcodes. Typical use cases include printing mailing labels or integrating barcode generation into shipping workflows. Developers often need to customize barcode dimensions such as bar height, and this snippet shows the essential API calls for those scenarios.
// Prompt: Generate a Postnet 2‑state postal barcode with overridden BarHeight set to 30 points.
// Tags: postnet, barcode, generation, barheight, png, aspnet, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that generates a Postnet 2‑state barcode with a custom bar height
/// and saves it as a PNG file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates the output folder, generates the barcode,
    /// applies a 30‑point bar height, saves the image, and writes the result path to the console.
    /// </summary>
    static void Main()
    {
        // Define a temporary directory for the output image
        string outputDir = Path.Combine(Path.GetTempPath(), "PostnetExample");
        Directory.CreateDirectory(outputDir);

        // Build the full file path for the PNG image
        string outputPath = Path.Combine(outputDir, "PostnetBarcode.png");

        // Initialize the barcode generator for Postnet with the sample data "123456"
        using (var generator = new BarcodeGenerator(EncodeTypes.Postnet, "123456"))
        {
            // Override the default bar height to 30 points
            generator.Parameters.Barcode.BarHeight.Point = 30f;

            // Save the generated barcode as a PNG file
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the barcode image was saved
        Console.WriteLine($"Postnet barcode saved to: {outputPath}");
    }
}