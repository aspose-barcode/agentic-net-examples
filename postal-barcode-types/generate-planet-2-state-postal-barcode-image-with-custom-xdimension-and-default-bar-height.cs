// Title: Generate Planet 2‑state postal barcode with custom XDimension
// Description: Demonstrates creating a Planet 2‑state postal barcode image, setting a custom X‑dimension while using the default bar height, and saving it as PNG.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, illustrating how to configure barcode parameters such as XDimension for postal symbologies. It uses the BarcodeGenerator class with EncodeTypes.Planet and shows typical steps: initializing the generator, adjusting parameters, and saving the image. Developers working with postal barcodes often need to customize size metrics while keeping other defaults, making this pattern useful for generating printable barcode assets.
// Prompt: Generate a Planet 2‑state postal barcode image with custom XDimension and default bar height.
// Tags: planet barcode, postal symbology, xdimension, barheight, image generation, png, aspose.barcode, csharp

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generation of a Planet 2‑state postal barcode with a custom X‑dimension.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Creates output folder, generates the barcode, and saves it as PNG.
    /// </summary>
    /// <param name="args">Command‑line arguments (not used).</param>
    static void Main(string[] args)
    {
        // Determine the output directory path relative to the current working directory
        string outputDirectory = Path.Combine(Directory.GetCurrentDirectory(), "Output");

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDirectory);

        // Build the full file path for the generated barcode image
        string outputPath = Path.Combine(outputDirectory, "PlanetBarcode.png");

        // Initialize the barcode generator with Planet symbology and sample data
        using (var generator = new BarcodeGenerator(EncodeTypes.Planet, "123456789"))
        {
            // Set a custom XDimension (module width) in pixels; default bar height is retained
            generator.Parameters.Barcode.XDimension.Pixels = 3f;

            // Save the generated barcode image as PNG
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Output the location of the saved barcode image
        Console.WriteLine($"Planet barcode image saved to: {outputPath}");
    }
}