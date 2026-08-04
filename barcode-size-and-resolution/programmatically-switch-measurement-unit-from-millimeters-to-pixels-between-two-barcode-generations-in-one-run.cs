// Title: Switch Measurement Unit Between Millimeters and Pixels for Barcode Generation
// Description: Demonstrates generating two barcodes in one execution, first using millimeters and then pixels as measurement units.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to configure measurement units via the Parameters.Barcode properties. It showcases the use of BarcodeGenerator, EncodeTypes, and BarCodeImageFormat classes, common in scenarios where precise sizing is required for different output media. Developers often need to switch between physical (mm) and screen (pixel) units when creating barcodes for print and digital displays.
// Prompt: Programmatically switch measurement unit from Millimeters to Pixels between two barcode generations in one run.
// Tags: code128, measurement-unit, generation, png, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that generates two Code128 barcodes, first using millimeter units
/// and then using pixel units, demonstrating how to switch measurement units at runtime.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Creates an output folder, generates two barcodes
    /// with different measurement units, and saves them as PNG files.
    /// </summary>
    static void Main()
    {
        // Ensure the output directory exists
        string outputDir = Path.Combine(Directory.GetCurrentDirectory(), "Barcodes");
        if (!Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        // ------------------------------------------------------------
        // First barcode: measurement unit set to millimeters
        // ------------------------------------------------------------
        using (var generatorMm = new BarcodeGenerator(EncodeTypes.Code128, "FirstMM"))
        {
            // Configure X-dimension and bar height in millimeters
            generatorMm.Parameters.Barcode.XDimension.Millimeters = 0.5f;
            generatorMm.Parameters.Barcode.BarHeight.Millimeters = 10f;

            // Save the barcode image as PNG
            string filePathMm = Path.Combine(outputDir, "barcode_mm.png");
            generatorMm.Save(filePathMm, BarCodeImageFormat.Png);
            Console.WriteLine($"Saved barcode with millimeter units to: {filePathMm}");
        }

        // ------------------------------------------------------------
        // Second barcode: measurement unit set to pixels
        // ------------------------------------------------------------
        using (var generatorPx = new BarcodeGenerator(EncodeTypes.Code128, "SecondPx"))
        {
            // Configure X-dimension and bar height in pixels
            generatorPx.Parameters.Barcode.XDimension.Pixels = 2f;
            generatorPx.Parameters.Barcode.BarHeight.Pixels = 40f;

            // Save the barcode image as PNG
            string filePathPx = Path.Combine(outputDir, "barcode_px.png");
            generatorPx.Save(filePathPx, BarCodeImageFormat.Png);
            Console.WriteLine($"Saved barcode with pixel units to: {filePathPx}");
        }
    }
}