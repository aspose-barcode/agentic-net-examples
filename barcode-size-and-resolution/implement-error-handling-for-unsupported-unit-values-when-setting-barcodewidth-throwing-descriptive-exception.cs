// Title: Set barcode image width with validation
// Description: Demonstrates setting the barcode image width using Aspose.BarCode and validates the width value.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to configure image dimensions via the AutoSizeMode and ImageWidth properties. It shows typical usage of BarcodeGenerator, Parameters, and AutoSizeMode classes for developers needing precise control over barcode size in generated images.
// Prompt: Implement error handling for unsupported unit values when setting BarCodeWidth, throwing descriptive exception.
// Tags: barcode, code128, width, validation, aspose.barcode, generation

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates setting barcode width with validation using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Sets the barcode image width after validating the supplied value.
    /// Throws <see cref="ArgumentOutOfRangeException"/> if the value is not supported.
    /// </summary>
    /// <param name="generator">The <see cref="BarcodeGenerator"/> instance to configure.</param>
    /// <param name="width">Desired barcode width in points (must be greater than zero).</param>
    static void SetBarCodeWidth(BarcodeGenerator generator, float width)
    {
        // Validate that the width is a positive number.
        if (width <= 0f)
        {
            throw new ArgumentOutOfRangeException(
                nameof(width),
                width,
                "BarCodeWidth must be a positive value greater than zero.");
        }

        // When AutoSizeMode is Interpolation, ImageWidth controls the barcode width.
        generator.Parameters.AutoSizeMode = AutoSizeMode.Interpolation;
        generator.Parameters.ImageWidth.Point = width;
    }

    /// <summary>
    /// Entry point demonstrating valid and invalid width handling.
    /// </summary>
    static void Main()
    {
        // Example 1: generate a barcode with a valid width.
        try
        {
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123456"))
            {
                SetBarCodeWidth(generator, 250f); // valid width in points
                generator.Save("valid_barcode.png");
                Console.WriteLine("Barcode generated with width 250pt: valid_barcode.png");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error generating barcode with valid width: {ex.Message}");
        }

        // Example 2: attempt to generate a barcode with an invalid (negative) width.
        try
        {
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "ABCDEF"))
            {
                SetBarCodeWidth(generator, -50f); // invalid width triggers exception
                generator.Save("invalid_barcode.png");
            }
        }
        catch (ArgumentOutOfRangeException ex)
        {
            Console.WriteLine($"Caught expected exception for unsupported width: {ex.Message}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Unexpected error: {ex.Message}");
        }
    }
}