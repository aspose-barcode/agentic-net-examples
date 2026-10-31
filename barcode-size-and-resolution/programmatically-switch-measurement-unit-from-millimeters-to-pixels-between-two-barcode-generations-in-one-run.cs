// Title: Switch measurement unit from millimeters to pixels for barcode generation
// Description: Demonstrates how to change the X-dimension measurement unit of a barcode from millimeters to pixels within a single execution.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating the use of BarcodeGenerator and its Parameters to control barcode dimensions. Developers often need to adjust measurement units for different output requirements, such as print (millimeters) versus screen display (pixels). The snippet shows typical API usage for setting XDimension in both units and saving the results.
// Prompt: Programmatically switch measurement unit from Millimeters to Pixels between two barcode generations in one run.
// Tags: barcode, code128, measurement unit, millimeters, pixels, generation, aspose.barcode, png

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates switching barcode measurement units between millimeters and pixels.
/// </summary>
class Program
{
    /// <summary>
    /// Generates two Code128 barcodes, first using millimeters then pixels, and saves them as PNG files.
    /// </summary>
    static void Main()
    {
        // Define output directory and ensure it exists
        string outputDir = Path.Combine(Directory.GetCurrentDirectory(), "Barcodes");
        Directory.CreateDirectory(outputDir);

        // File paths for the two generated barcodes
        string pathMillimeters = Path.Combine(outputDir, "barcode_mm.png");
        string pathPixels = Path.Combine(outputDir, "barcode_px.png");

        // Create a barcode generator for Code128 with the data "123456"
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123456"))
        {
            // Set X-dimension measurement unit to millimeters (2 mm per module)
            generator.Parameters.Barcode.XDimension.Millimeters = 2f;
            // Save the barcode using millimeter settings
            generator.Save(pathMillimeters, BarCodeImageFormat.Png);

            // Switch X-dimension measurement unit to pixels (3 pixels per module)
            generator.Parameters.Barcode.XDimension.Pixels = 3f;
            // Save the barcode using pixel settings
            generator.Save(pathPixels, BarCodeImageFormat.Png);
        }

        // Inform the user where the barcode images were saved
        Console.WriteLine($"Barcodes saved to: {outputDir}");
    }
}