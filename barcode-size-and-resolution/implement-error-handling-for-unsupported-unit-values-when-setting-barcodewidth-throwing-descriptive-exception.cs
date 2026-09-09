// Title: Demonstrate setting barcode image width with unit validation
// Description: Shows how to set the barcode width using different measurement units and handles unsupported units by throwing an exception.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating the use of BarcodeGenerator and its Parameters.ImageWidth properties. Developers often need to control barcode dimensions in pixels, millimeters, inches, points, or document units, and must validate unit inputs to avoid runtime errors. The snippet demonstrates typical usage patterns for setting image size and handling invalid unit specifications.
// Prompt: Implement error handling for unsupported unit values when setting BarCodeWidth, throwing descriptive exception.
// Tags: barcode, generation, image width, unit validation, exception handling, aspose.barcode, csharp

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Example program that generates a Code128 barcode and demonstrates width unit handling.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates barcodes with valid and invalid width units, saving results to a temporary folder.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary output directory for the generated barcode images
        string outputDir = Path.Combine(Path.GetTempPath(), "BarcodeExample_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputDir);

        // Initialize a barcode generator for Code128 symbology with sample data
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123456"))
        {
            // -----------------------------------------------------------------
            // Set a supported width using pixels and save the resulting image
            // -----------------------------------------------------------------
            try
            {
                SetBarcodeWidth(generator, 200f, "Pixels");
                generator.Save(Path.Combine(outputDir, "Barcode_200px.png"), BarCodeImageFormat.Png);
                Console.WriteLine("Barcode saved with width 200 pixels.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error setting supported width: " + ex.Message);
            }

            // -----------------------------------------------------------------
            // Attempt to set an unsupported unit (Centimeters) to trigger error handling
            // -----------------------------------------------------------------
            try
            {
                SetBarcodeWidth(generator, 50f, "Centimeters");
                generator.Save(Path.Combine(outputDir, "Barcode_InvalidUnit.png"), BarCodeImageFormat.Png);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Caught expected exception: " + ex.Message);
            }
        }

        Console.WriteLine("Program completed. Output folder: " + outputDir);
    }

    /// <summary>
    /// Configures the barcode image width based on the specified value and unit.
    /// Throws an exception if the unit is not supported.
    /// </summary>
    /// <param name="generator">The BarcodeGenerator instance to configure.</param>
    /// <param name="value">The numeric width value.</param>
    /// <param name="unit">The measurement unit (e.g., Pixels, Millimeters, Inches, Point, Document).</param>
    static void SetBarcodeWidth(BarcodeGenerator generator, float value, string unit)
    {
        if (generator == null) throw new ArgumentNullException(nameof(generator));
        if (string.IsNullOrWhiteSpace(unit)) throw new ArgumentException("Unit must be provided.", nameof(unit));

        // Normalize unit string for case‑insensitive comparison
        switch (unit.Trim().ToLowerInvariant())
        {
            case "pixels":
                generator.Parameters.ImageWidth.Pixels = value;
                break;
            case "millimeters":
                generator.Parameters.ImageWidth.Millimeters = value;
                break;
            case "inches":
                generator.Parameters.ImageWidth.Inches = value;
                break;
            case "point":
                generator.Parameters.ImageWidth.Point = value;
                break;
            case "document":
                generator.Parameters.ImageWidth.Document = value;
                break;
            default:
                // Throw a descriptive exception for unsupported units
                throw new ArgumentException(
                    $"Unsupported unit: {unit}. Supported units are: Pixels, Millimeters, Inches, Point, Document.",
                    nameof(unit));
        }
    }
}