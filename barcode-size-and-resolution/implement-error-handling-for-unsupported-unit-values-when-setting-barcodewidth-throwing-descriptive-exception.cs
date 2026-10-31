// Title: Demonstrate setting barcode image width with unit handling and error handling
// Description: Shows how to set the barcode image width using different measurement units and handles unsupported units by throwing a descriptive exception. The example saves a barcode image and illustrates error handling.
// Category-Description: This example belongs to the Aspose.BarCode generation category, focusing on image dimension configuration. It demonstrates using BarcodeGenerator, its Parameters, and ImageWidth properties to control output size. Developers often need to specify barcode dimensions in various units (pixels, millimeters, points, inches, document) and require robust validation for unsupported units.
// Prompt: Implement error handling for unsupported unit values when setting BarCodeWidth, throwing descriptive exception.
// Tags: barcode, code128, image width, unit conversion, error handling, aspnet, aspose.barcode, generation

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates setting barcode image width with unit handling and error handling.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates a barcode, sets its width using supported units, saves it,
    /// then attempts to use an unsupported unit to show error handling.
    /// </summary>
    static void Main()
    {
        // Determine the output file path for the generated barcode image.
        string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "barcode.png");

        try
        {
            // Create a barcode generator with Code128 symbology and sample text.
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "Sample"))
            {
                // Enable automatic sizing based on the nearest supported size.
                generator.Parameters.AutoSizeMode = AutoSizeMode.Nearest;

                // Set the image width using a supported unit (Pixels) and save the barcode.
                SetBarcodeImageWidth(generator, "Pixels", 300f);
                generator.Save(outputPath, BarCodeImageFormat.Png);
                Console.WriteLine($"Barcode saved to {outputPath} with width set in Pixels.");
            }

            // Attempt to set the image width using an unsupported unit to trigger error handling.
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "Sample"))
            {
                generator.Parameters.AutoSizeMode = AutoSizeMode.Nearest;
                SetBarcodeImageWidth(generator, "Centimeters", 100f);
            }
        }
        catch (ArgumentException ex)
        {
            // Handle argument-related errors, such as unsupported units.
            Console.WriteLine($"Argument error: {ex.Message}");
        }
        catch (Exception ex)
        {
            // Handle any unexpected errors.
            Console.WriteLine($"Unexpected error: {ex.Message}");
        }
    }

    /// <summary>
    /// Sets the barcode image width based on the specified unit and value.
    /// Throws an <see cref="ArgumentException"/> if the unit is unsupported.
    /// </summary>
    /// <param name="generator">The <see cref="BarcodeGenerator"/> instance.</param>
    /// <param name="unit">The measurement unit (e.g., Pixels, Millimeters, Point, Inches, Document).</param>
    /// <param name="value">The width value in the specified unit.</param>
    static void SetBarcodeImageWidth(BarcodeGenerator generator, string unit, float value)
    {
        // Validate input parameters.
        if (generator == null) throw new ArgumentNullException(nameof(generator));
        if (string.IsNullOrWhiteSpace(unit)) throw new ArgumentException("Unit must be provided.", nameof(unit));

        // Normalize the unit string for case‑insensitive comparison.
        switch (unit.Trim().ToLowerInvariant())
        {
            case "pixels":
                generator.Parameters.ImageWidth.Pixels = value;
                break;
            case "millimeters":
                generator.Parameters.ImageWidth.Millimeters = value;
                break;
            case "point":
                generator.Parameters.ImageWidth.Point = value;
                break;
            case "inches":
                generator.Parameters.ImageWidth.Inches = value;
                break;
            case "document":
                generator.Parameters.ImageWidth.Document = value;
                break;
            default:
                // Throw a descriptive exception for unsupported units.
                throw new ArgumentException($"Unsupported unit '{unit}'. Supported units are Pixels, Millimeters, Point, Inches, Document.", nameof(unit));
        }
    }
}