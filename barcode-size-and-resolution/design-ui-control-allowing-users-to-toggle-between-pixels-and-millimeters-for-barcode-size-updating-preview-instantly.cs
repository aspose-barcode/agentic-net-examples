// Title: Barcode size unit toggle demonstration
// Description: Shows how to generate barcodes with dimensions specified in pixels or millimeters, illustrating the core logic behind a UI toggle control.
// Category-Description: This example belongs to the Aspose.BarCode image generation category, demonstrating the use of BarcodeGenerator, AutoSizeMode, and size unit properties (Pixels, Millimeters). Developers often need to switch measurement units for barcode rendering in UI applications, printing, or labeling scenarios. The snippet provides a reference for implementing unit toggles and instant preview updates.
// Prompt: Design UI control allowing users to toggle between Pixels and Millimeters for barcode size, updating preview instantly.
// Tags: barcode, size, unit, pixels, millimeters, generation, aspose.barcode, autosizemode, preview

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating barcode images with size specified in pixels and millimeters.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates two barcode files using different size units and prints their paths.
    /// </summary>
    static void Main()
    {
        // NOTE: The original request was for a UI control to toggle units.
        // The snippet runner does not support UI frameworks, so we demonstrate the core logic
        // by generating two barcode images: one sized in pixels and one sized in millimeters.
        // The images are saved to the current directory and their file names are printed.

        // Barcode content and symbology
        const string codeText = "1234567890";
        var encodeType = EncodeTypes.Code128;

        // ---------- Generate barcode with size specified in pixels ----------
        using (var generatorPixels = new BarcodeGenerator(encodeType))
        {
            // Set the text to encode
            generatorPixels.CodeText = codeText;

            // Use interpolation mode to control exact image size
            generatorPixels.Parameters.AutoSizeMode = AutoSizeMode.Interpolation;

            // Set image size in pixels
            generatorPixels.Parameters.ImageWidth.Pixels = 300f;   // 300 pixels width
            generatorPixels.Parameters.ImageHeight.Pixels = 150f; // 150 pixels height

            // Optional: set background and bar colors
            generatorPixels.Parameters.BackColor = Color.White;
            generatorPixels.Parameters.Barcode.BarColor = Color.Black;

            // Save the barcode image
            const string pixelFile = "barcode_pixels.png";
            generatorPixels.Save(pixelFile);
            Console.WriteLine($"Barcode saved with pixel dimensions: {pixelFile}");
        }

        // ---------- Generate barcode with size specified in millimeters ----------
        using (var generatorMillimeters = new BarcodeGenerator(encodeType))
        {
            // Set the text to encode
            generatorMillimeters.CodeText = codeText;

            // Use interpolation mode to control exact image size
            generatorMillimeters.Parameters.AutoSizeMode = AutoSizeMode.Interpolation;

            // Set image size in millimeters
            generatorMillimeters.Parameters.ImageWidth.Millimeters = 80f;   // 80 mm width
            generatorMillimeters.Parameters.ImageHeight.Millimeters = 40f; // 40 mm height

            // Optional: set background and bar colors
            generatorMillimeters.Parameters.BackColor = Color.White;
            generatorMillimeters.Parameters.Barcode.BarColor = Color.Black;

            // Save the barcode image
            const string mmFile = "barcode_millimeters.png";
            generatorMillimeters.Save(mmFile);
            Console.WriteLine($"Barcode saved with millimeter dimensions: {mmFile}");
        }

        // End of demonstration
    }
}