// Title: Generate Postnet 2‑state barcode with custom bar height
// Description: Demonstrates creating a Postnet 2‑state postal barcode and setting its bar height to 30 points.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to use the BarcodeGenerator class to produce postal barcodes. It shows configuring barcode parameters such as bar height, and saving the result as an image. Developers working with postal symbologies often need to customize dimensions for printing requirements, and this snippet provides a quick reference.
// Prompt: Generate a Postnet 2‑state postal barcode with overridden BarHeight set to 30 points.
// Tags: postnet, barcode, generation, barheight, png, aspnet, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating a Postnet 2‑state barcode with a custom bar height using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates the barcode and saves it as a PNG file.
    /// </summary>
    static void Main()
    {
        // Define a temporary output directory for the generated image.
        string outputDir = Path.Combine(Path.GetTempPath(), "AsposeBarCodeDemo");

        // Ensure the directory exists.
        Directory.CreateDirectory(outputDir);

        // Build the full path for the output PNG file.
        string outputPath = Path.Combine(outputDir, "PostnetBarcode.png");

        // Initialize the barcode generator with Postnet symbology and the data to encode.
        using (var generator = new BarcodeGenerator(EncodeTypes.Postnet, "123456"))
        {
            // Override the default bar height to 30 points.
            generator.Parameters.Barcode.BarHeight.Point = 30f;

            // Save the generated barcode image in PNG format.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Output the location of the saved barcode image.
        Console.WriteLine("Barcode saved to: " + outputPath);
    }
}