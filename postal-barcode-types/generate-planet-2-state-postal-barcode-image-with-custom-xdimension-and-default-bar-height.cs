// Title: Generate Planet 2‑state Postal Barcode with Custom XDimension
// Description: Creates a Planet 2‑state postal barcode image using Aspose.BarCode, applying a custom XDimension while keeping the default bar height.
// Category-Description: This example belongs to the Aspose.BarCode generation category, demonstrating how to configure barcode parameters such as XDimension for postal symbologies. It showcases the use of BarcodeGenerator, EncodeTypes, and BarCodeImageFormat classes, which developers commonly employ to produce printable barcode images for mailing and logistics applications.
// Prompt: Generate a Planet 2‑state postal barcode image with custom XDimension and default bar height.
// Tags: planet, postal, barcode, generation, png, xdimension

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generation of a Planet 2‑state postal barcode with a custom XDimension.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates and saves the barcode image.
    /// </summary>
    static void Main()
    {
        // Define the output file name
        string outputPath = "planet.png";

        // Resolve the full directory path and ensure it exists
        string outputDir = Path.GetDirectoryName(Path.GetFullPath(outputPath));
        if (!Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        // Initialize the barcode generator for Planet symbology with sample data
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Planet, "1234567890"))
        {
            // Set a custom XDimension (module width) in points; 2 points = 0.028 mm
            generator.Parameters.Barcode.XDimension.Point = 2f;

            // No explicit BarHeight is set, so the default height is used

            // Save the generated barcode as a PNG image
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the file was saved
        Console.WriteLine($"Planet barcode saved to {outputPath}");
    }
}