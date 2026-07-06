// Title: Switch measurement unit between millimeters and pixels for barcode generation
// Description: Demonstrates generating two barcodes in one run, first using millimeters for bar height then switching to pixels.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to control measurement units (millimeters, pixels) via the BarcodeGenerator.Parameters.Barcode.BarHeight properties. Developers often need to switch units to meet layout requirements for different output media, such as print (mm) versus screen (px). The key API classes used are BarcodeGenerator, EncodeTypes, and AutoSizeMode.
// Prompt: Programmatically switch measurement unit from Millimeters to Pixels between two barcode generations in one run.
// Tags: code128, measurement unit, millimeters, pixels, barcode generation, aspose.barcode

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that generates two Code128 barcodes, first using millimeters for bar height
/// and then switching to pixels, demonstrating unit conversion within a single execution.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates two barcode images with different measurement units.
    /// </summary>
    static void Main()
    {
        // ------------------------------------------------------------
        // First barcode: use millimeters for bar height
        // ------------------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123456"))
        {
            // Disable automatic sizing to allow explicit height setting
            generator.Parameters.AutoSizeMode = AutoSizeMode.None;

            // Set bar height explicitly in millimeters (10 mm)
            generator.Parameters.Barcode.BarHeight.Millimeters = 10f;

            // Save the generated barcode image to a PNG file
            generator.Save("barcode_mm.png");
        }

        // ------------------------------------------------------------
        // Second barcode: switch to pixels for bar height
        // ------------------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123456"))
        {
            // Disable automatic sizing to allow explicit height setting
            generator.Parameters.AutoSizeMode = AutoSizeMode.None;

            // Set bar height explicitly in pixels (40 px)
            generator.Parameters.Barcode.BarHeight.Pixels = 40f;

            // Save the generated barcode image to a PNG file
            generator.Save("barcode_px.png");
        }
    }
}