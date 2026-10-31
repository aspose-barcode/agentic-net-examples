// Title: Rotating DataBar Omnidirectional Barcode 90 Degrees and Exporting to PNG
// Description: Demonstrates generating a GS1‑formatted DataBar Omnidirectional barcode, rotating it 90° clockwise, and saving as a PNG image.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to configure barcode parameters such as X‑dimension, bar height, and rotation angle using the BarcodeGenerator class. Typical use cases include creating rotated barcodes for label designs, packaging, or UI components where orientation matters. Developers often need to adjust rotation, size, and output format when integrating barcode generation into automated workflows.
// Prompt: Implement feature rotating generated DataBar barcodes 90 degrees before exporting PNG.
// Tags: databar, rotation, png, aspose.barcode, generation

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Generates a DataBar Omnidirectional barcode, rotates it 90 degrees, and saves it as a PNG file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates the output directory, configures the barcode generator,
    /// applies rotation, and writes the resulting image to disk.
    /// </summary>
    static void Main()
    {
        // Determine a temporary folder for the output image.
        string outputDir = Path.Combine(Path.GetTempPath(), "AsposeBarCodeDemo");
        if (!Directory.Exists(outputDir))
        {
            // Create the directory if it does not already exist.
            Directory.CreateDirectory(outputDir);
        }

        // Full path for the generated PNG file.
        string outputPath = Path.Combine(outputDir, "DatabarRotated90.png");

        // DataBar Omnidirectional requires a GS1‑formatted code text.
        string codeText = "(01)12345678901231";

        // Initialize the barcode generator with the desired symbology and data.
        using (var generator = new BarcodeGenerator(EncodeTypes.DatabarOmniDirectional, codeText))
        {
            // Optional appearance settings: set module size and bar height.
            generator.Parameters.Barcode.XDimension.Pixels = 2;
            generator.Parameters.Barcode.BarHeight.Pixels = 30;

            // Rotate the barcode 90 degrees clockwise.
            generator.Parameters.RotationAngle = 90f;

            // Save the rotated barcode as a PNG image.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the file was saved.
        Console.WriteLine($"Barcode saved to: {outputPath}");
    }
}