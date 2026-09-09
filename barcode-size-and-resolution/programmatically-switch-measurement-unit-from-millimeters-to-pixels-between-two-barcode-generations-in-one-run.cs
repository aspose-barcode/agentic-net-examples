// Title: Switch measurement unit between Millimeters and Pixels for barcode generation
// Description: Demonstrates generating two barcodes in a single execution, first using millimeters and then switching to pixels for the second barcode.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to configure measurement units via the BarcodeGenerator.Parameters.Barcode.XDimension property. Developers often need to control barcode dimensions in different units (e.g., millimeters for print layouts, pixels for screen rendering). The example shows typical usage of EncodeTypes, BarCodeImageFormat, and unit switching within one generator instance.
// Prompt: Programmatically switch measurement unit from Millimeters to Pixels between two barcode generations in one run.
// Tags: barcode symbology, measurement unit, code128, png, aspose.barcode, generation

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Generates two Code128 barcodes, first using millimeters as the measurement unit and then switching to pixels,
/// saving each barcode as a PNG image in a temporary folder.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates a temporary output directory, generates the barcodes with different units,
    /// and writes the file paths to the console.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for output files
        string outputFolder = Path.Combine(Path.GetTempPath(), "BarcodeUnitSwitch_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputFolder);

        // Define file paths for the two output images
        string mmPath = Path.Combine(outputFolder, "barcode_mm.png");
        string pxPath = Path.Combine(outputFolder, "barcode_px.png");

        // Generate barcode with measurement unit set to Millimeters
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123456"))
        {
            // Set X-dimension to 2 millimeters
            generator.Parameters.Barcode.XDimension.Millimeters = 2f;
            // Save the first barcode image
            generator.Save(mmPath, BarCodeImageFormat.Png);

            // Switch measurement unit to Pixels for the next generation
            generator.Parameters.Barcode.XDimension.Pixels = 3f;
            // Save the second barcode image
            generator.Save(pxPath, BarCodeImageFormat.Png);
        }

        // Output the locations of the generated barcode images
        Console.WriteLine("Barcodes generated:");
        Console.WriteLine("Millimeters unit: " + mmPath);
        Console.WriteLine("Pixels unit: " + pxPath);
    }
}