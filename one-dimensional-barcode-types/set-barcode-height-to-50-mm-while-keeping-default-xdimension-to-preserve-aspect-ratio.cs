// Title: Set barcode height to 50 mm while preserving aspect ratio
// Description: Demonstrates how to generate a Code128 barcode image with a specific bar height of 50 mm, leaving the XDimension at its default value to keep the barcode's proportions.
// Category-Description: This example belongs to the Aspose.BarCode image generation category, illustrating the use of BarcodeGenerator and its Parameters to control barcode dimensions. Typical use cases include creating printable barcodes with precise size requirements for labels, packaging, or inventory systems. Developers often need to adjust bar height or module size while maintaining aspect ratio, using classes such as BarcodeGenerator, EncodeTypes, and BarCodeImageFormat.
// Prompt: Set barcode height to 50 mm while keeping default XDimension to preserve aspect ratio.
// Tags: code128, set-height, png, barcodegenerator, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Generates a Code128 barcode image with a custom bar height.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Creates output folder, configures barcode generator, sets bar height, saves PNG file, and writes the result path.
    /// </summary>
    static void Main()
    {
        // Determine the output directory relative to the current working folder
        string outputDir = Path.Combine(Directory.GetCurrentDirectory(), "Output");
        // Ensure the directory exists
        Directory.CreateDirectory(outputDir);
        // Build the full file path for the resulting PNG image
        string outputPath = Path.Combine(outputDir, "barcode.png");

        // Initialize the barcode generator with Code128 symbology and the data to encode
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128, "123456"))
        {
            // Set bar height to 50 mm while preserving the default XDimension (module width)
            generator.Parameters.Barcode.BarHeight.Millimeters = 50f;
            // Save the generated barcode as a PNG file
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the barcode image was saved
        Console.WriteLine($"Barcode saved to: {outputPath}");
    }
}