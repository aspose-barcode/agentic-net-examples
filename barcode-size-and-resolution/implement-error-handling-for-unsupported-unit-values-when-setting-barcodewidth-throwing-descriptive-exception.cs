// Title: Set barcode image width with unit handling and error checking
// Description: Demonstrates how to set the barcode image width using different measurement units and validates input values.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to configure image dimensions via the BarcodeGenerator.Parameters.ImageWidth properties. It covers unit conversion (pixels, millimeters, inches, points), input validation, and AutoSizeMode adjustment—common tasks for developers creating custom-sized barcodes for print or digital media.
// Prompt: Implement error handling for unsupported unit values when setting BarCodeWidth, throwing descriptive exception.
// Tags: barcode symbology, image width, unit conversion, error handling, aspose.barcode, generation

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Example program that generates barcodes and demonstrates setting image width with unit validation.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates a barcode with a valid width, then attempts an invalid width to show error handling.
    /// </summary>
    static void Main()
    {
        // Prepare output path for the valid barcode image
        string outputPath = Path.Combine(Path.GetTempPath(), "barcode.png");

        // Generate a barcode using a valid width and save it
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123456"))
        {
            SetBarcodeImageWidth(generator, 300f, "pixels"); // Set width in pixels
            generator.Save(outputPath, BarCodeImageFormat.Png);
            Console.WriteLine($"Barcode saved to: {outputPath}");
        }

        // Attempt to set an unsupported (negative) width to demonstrate error handling
        try
        {
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "ABC"))
            {
                SetBarcodeImageWidth(generator, -100f, "pixels"); // This will throw
                string invalidPath = Path.Combine(Path.GetTempPath(), "invalid.png");
                generator.Save(invalidPath, BarCodeImageFormat.Png);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }

    /// <summary>
    /// Sets the barcode image width using the specified value and unit, with validation.
    /// </summary>
    /// <param name="generator">The BarcodeGenerator instance to configure.</param>
    /// <param name="value">The numeric width value (must be positive).</param>
    /// <param name="unit">The measurement unit: pixels, millimeters, inches, or points.</param>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when <paramref name="value"/> is not positive.</exception>
    /// <exception cref="ArgumentException">Thrown when <paramref name="unit"/> is not supported.</exception>
    static void SetBarcodeImageWidth(BarcodeGenerator generator, float value, string unit)
    {
        // Validate that the width value is positive
        if (value <= 0f)
        {
            throw new ArgumentOutOfRangeException(nameof(value), "Image width must be a positive number.");
        }

        // Determine which unit to apply and set the corresponding property
        switch (unit?.Trim().ToLowerInvariant())
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
            case "points":
                generator.Parameters.ImageWidth.Point = value;
                break;
            default:
                // Throw a descriptive exception for unsupported units
                throw new ArgumentException(
                    $"Unsupported unit '{unit}'. Supported units are: pixels, millimeters, inches, points.",
                    nameof(unit));
        }

        // Ensure the manually set width takes effect by using Nearest auto‑size mode
        generator.Parameters.AutoSizeMode = AutoSizeMode.Nearest;
    }
}