// Title: Generate Planet 2-state postal barcode with custom XDimension
// Description: Demonstrates creating a Planet 2‑state postal barcode image, setting a custom X‑dimension while using the default bar height.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to configure barcode parameters such as XDimension for postal symbologies. It uses the BarcodeGenerator class with EncodeTypes.Planet to produce a PNG image. Developers often need to customize size and appearance of barcodes for printing or digital display, and this snippet shows the typical steps: instantiate generator, adjust parameters, and save the image.
// Prompt: Generate a Planet 2‑state postal barcode image with custom XDimension and default bar height.
// Tags: planet barcode, postal symbology, xdimension, image generation, png, aspose.barcode, csharp

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating a Planet 2‑state postal barcode with a custom X‑dimension.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Creates output folder, generates the barcode, saves as PNG, and writes the path to console.
    /// </summary>
    static void Main()
    {
        // Define output directory in the system temporary folder
        string outputDir = Path.Combine(Path.GetTempPath(), "AsposeBarCodeOutput");
        // Ensure the directory exists
        Directory.CreateDirectory(outputDir);
        // Build the full path for the output PNG file
        string outputPath = Path.Combine(outputDir, "PlanetBarcode.png");

        // Initialize the barcode generator for Planet symbology with the data "123456"
        using (var generator = new BarcodeGenerator(EncodeTypes.Planet, "123456"))
        {
            // Set a custom XDimension (module width) in pixels; bar height remains default
            generator.Parameters.Barcode.XDimension.Pixels = 4f;
            // Save the generated barcode image as PNG
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Output the location of the saved barcode image
        Console.WriteLine($"Planet barcode saved to: {outputPath}");
    }
}