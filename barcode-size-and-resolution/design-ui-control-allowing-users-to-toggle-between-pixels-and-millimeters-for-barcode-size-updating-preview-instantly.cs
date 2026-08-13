// Title: Barcode size unit toggle demonstration
// Description: Shows how to generate barcodes using pixel and millimeter units, illustrating the logic behind a UI control that lets users switch units and see instant preview updates.
// Category-Description: This example belongs to the Aspose.BarCode generation category, focusing on size and measurement settings. It demonstrates using BarcodeGenerator, EncodeTypes, and the Parameters property to configure XDimension, ImageWidth, and ImageHeight in different units. Developers often need to switch between pixels and physical units like millimeters when creating barcodes for screen display versus print, making this a common scenario in UI-driven barcode design tools.
// Prompt: Design UI control allowing users to toggle between Pixels and Millimeters for barcode size, updating preview instantly.
// Tags: barcode, size, unit, pixels, millimeters, generation, aspose.barcode, code128

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating barcodes with size specified in pixels and millimeters.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates two barcode images using different measurement units.
    /// </summary>
    static void Main()
    {
        // Define common barcode data and output file names
        const string codeText = "123456";
        const string outputPixels = "barcode_pixels.png";
        const string outputMillimeters = "barcode_mm.png";

        // ------------------------------------------------------------
        // Generate barcode with size specified in Pixels
        // ------------------------------------------------------------
        using (var generatorPixels = new BarcodeGenerator(EncodeTypes.Code128, codeText))
        {
            // Set module (X) dimension in pixels
            generatorPixels.Parameters.Barcode.XDimension.Pixels = 2f;

            // Optionally set overall image dimensions in pixels
            generatorPixels.Parameters.ImageWidth.Pixels = 300f;
            generatorPixels.Parameters.ImageHeight.Pixels = 100f;

            // Save the barcode image
            generatorPixels.Save(outputPixels);
            Console.WriteLine($"Barcode saved with pixel units: {outputPixels}");
        }

        // ------------------------------------------------------------
        // Generate barcode with size specified in Millimeters
        // ------------------------------------------------------------
        using (var generatorMm = new BarcodeGenerator(EncodeTypes.Code128, codeText))
        {
            // Set module (X) dimension in millimeters
            generatorMm.Parameters.Barcode.XDimension.Millimeters = 0.5f;

            // Optionally set overall image dimensions in millimeters
            generatorMm.Parameters.ImageWidth.Millimeters = 80f;
            generatorMm.Parameters.ImageHeight.Millimeters = 30f;

            // Save the barcode image
            generatorMm.Save(outputMillimeters);
            Console.WriteLine($"Barcode saved with millimeter units: {outputMillimeters}");
        }
    }
}