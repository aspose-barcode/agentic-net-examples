// Title: Rotate DataBar Omni-Directional Barcode 90 Degrees and Export as PNG
// Description: Demonstrates how to generate a DataBar Omni‑Directional barcode, rotate it 90 degrees clockwise, and save the result as a PNG image.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating barcode creation, transformation, and image export. It uses the BarcodeGenerator class with EncodeTypes.DatabarOmniDirectional, modifies the RotationAngle parameter, and saves the image via BarCodeImageFormat. Developers often need to rotate barcodes for specific layout requirements, such as fitting rotated labels or integrating with existing graphics.
// Prompt: Implement feature rotating generated DataBar barcodes 90 degrees before exporting PNG.
// Tags: databar, rotation, png, aspose.barcode, barcode generation, image export

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Generates a DataBar Omni-Directional barcode, rotates it 90 degrees, and saves it as a PNG file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates the output directory, configures the barcode generator,
    /// applies a 90‑degree rotation, and writes the PNG image to disk.
    /// </summary>
    static void Main()
    {
        // Determine the output folder relative to the current working directory
        string outputDir = Path.Combine(Directory.GetCurrentDirectory(), "Output");

        // Ensure the output directory exists
        if (!Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        // Full path for the resulting PNG file
        string outputPath = Path.Combine(outputDir, "DatabarRotated90.png");

        // Initialize the barcode generator with DataBar Omni-Directional symbology and sample data
        using (var generator = new BarcodeGenerator(EncodeTypes.DatabarOmniDirectional, "(01)12345678901231"))
        {
            // Rotate the barcode 90 degrees clockwise
            generator.Parameters.RotationAngle = 90f;

            // Optional: adjust barcode dimensions for better visual quality
            generator.Parameters.Barcode.XDimension.Pixels = 2;
            generator.Parameters.Barcode.BarHeight.Pixels = 60;

            // Save the rotated barcode as a PNG image
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the file was saved
        Console.WriteLine($"Rotated DataBar barcode saved to: {outputPath}");
    }
}