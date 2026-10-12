// Title: Switching Measurement Units Between Barcode Generations
// Description: Demonstrates how to generate two barcodes in a single run, first using millimeters and then pixels as the measurement unit.
// Category-Description: This example belongs to the Aspose.BarCode generation category, focusing on configuring measurement units via the XDimension property. It shows how to switch between millimeter and pixel units when creating barcodes, a common requirement for developers who need precise control over barcode size for different output media such as print (millimeters) and screen (pixels). Key API classes include BarcodeGenerator, EncodeTypes, and BarCodeImageFormat.
// Prompt: Programmatically switch measurement unit from Millimeters to Pixels between two barcode generations in one run.
// Tags: barcode symbology, measurement unit, generation, png, aspose.barcode, code128

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that creates two Code128 barcodes using different measurement units:
/// the first barcode uses millimeters, the second uses pixels.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates two barcodes with distinct XDimension units and saves them as PNG files.
    /// </summary>
    static void Main()
    {
        // Prepare a temporary output directory for the generated barcode images.
        string outputDir = Path.Combine(Path.GetTempPath(), "BarcodeUnitSwitch");
        Directory.CreateDirectory(outputDir);

        // ------------------------------------------------------------
        // First barcode: measurement unit set to millimeters.
        // ------------------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "MM_UNIT"))
        {
            // Set the X-dimension (module width) to 2 millimeters.
            generator.Parameters.Barcode.XDimension.Millimeters = 2f;

            // Define the file path and save the barcode as a PNG image.
            string mmPath = Path.Combine(outputDir, "barcode_mm.png");
            generator.Save(mmPath, BarCodeImageFormat.Png);

            Console.WriteLine($"Saved millimeter-based barcode to: {mmPath}");
        }

        // ------------------------------------------------------------
        // Second barcode: measurement unit set to pixels.
        // ------------------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "PX_UNIT"))
        {
            // Set the X-dimension (module width) to 3 pixels.
            generator.Parameters.Barcode.XDimension.Pixels = 3f;

            // Define the file path and save the barcode as a PNG image.
            string pxPath = Path.Combine(outputDir, "barcode_px.png");
            generator.Save(pxPath, BarCodeImageFormat.Png);

            Console.WriteLine($"Saved pixel-based barcode to: {pxPath}");
        }
    }
}