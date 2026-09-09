// Title: Generate Code128 barcode with custom height
// Description: Demonstrates how to set the barcode height to 50 mm while preserving the default XDimension, producing a PNG image.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating the use of BarcodeGenerator, EncodeTypes, and BarCodeImageFormat to customize barcode dimensions. Developers often need to adjust barcode size for printing or UI display while maintaining aspect ratio, and this snippet shows the typical API calls for such tasks.
// Prompt: Set barcode height to 50 mm while keeping default XDimension to preserve aspect ratio.
// Tags: code128, barcode height, xdimension, png, aspose.barcode, generation

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that creates a Code128 barcode image with a specific height.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates a barcode, sets its height to 50 mm,
    /// saves it as a PNG file, and writes the output path to the console.
    /// </summary>
    static void Main()
    {
        // Define and ensure the output directory exists
        string outputDir = Path.Combine(Directory.GetCurrentDirectory(), "Output");
        Directory.CreateDirectory(outputDir);

        // Build the full path for the resulting barcode image
        string outputPath = Path.Combine(outputDir, "barcode.png");

        // Initialize the barcode generator with Code128 symbology and sample data
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128, "Sample"))
        {
            // Set barcode height to 50 millimeters while keeping the default XDimension
            generator.Parameters.Barcode.BarHeight.Millimeters = 50f;

            // Save the generated barcode as a PNG image
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the barcode image was saved
        Console.WriteLine($"Barcode saved to {outputPath}");
    }
}