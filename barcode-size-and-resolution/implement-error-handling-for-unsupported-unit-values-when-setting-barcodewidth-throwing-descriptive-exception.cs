// Title: Barcode width validation with error handling
// Description: Demonstrates setting barcode width using Aspose.BarCode while validating input and handling unsupported values.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to configure barcode dimensions using the BarcodeGenerator class. It covers AutoSizeMode, ImageWidth, and error handling for invalid width values, which developers commonly need when creating barcodes for print or digital media.
// Prompt: Implement error handling for unsupported unit values when setting BarCodeWidth, throwing descriptive exception.
// Tags: barcode, symbology, width, validation, error-handling, aspnet, aspose.barcode, generation

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates barcode generation with width validation and error handling using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Validates the width value and applies it to the generator.
    /// Throws <see cref="ArgumentOutOfRangeException"/> if the value is not positive.
    /// </summary>
    /// <param name="generator">The <see cref="BarcodeGenerator"/> instance to configure.</param>
    /// <param name="widthInPoints">Desired barcode width in points (1/72 inch).</param>
    static void SetBarCodeWidth(BarcodeGenerator generator, float widthInPoints)
    {
        // Ensure the width is a positive number.
        if (widthInPoints <= 0f)
        {
            throw new ArgumentOutOfRangeException(
                nameof(widthInPoints),
                "BarCodeWidth must be a positive value. Received: " + widthInPoints);
        }

        // Use Interpolation mode so that ImageWidth controls the barcode size.
        generator.Parameters.AutoSizeMode = AutoSizeMode.Interpolation;
        generator.Parameters.ImageWidth.Point = widthInPoints;
    }

    /// <summary>
    /// Entry point that creates a barcode, applies validated width, and saves the image.
    /// </summary>
    static void Main()
    {
        // Sample barcode generation with width validation.
        try
        {
            // Initialize the generator with a specific symbology and data.
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "Sample123"))
            {
                // Attempt to set an invalid width (uncomment to test exception).
                // SetBarCodeWidth(generator, -50f);

                // Set a valid width (200 points ≈ 2.78 inches).
                SetBarCodeWidth(generator, 200f);

                // Optional: set height for completeness.
                generator.Parameters.ImageHeight.Point = 100f;

                // Save the barcode image to a file.
                generator.Save("barcode.png");
                Console.WriteLine("Barcode generated successfully.");
            }
        }
        catch (ArgumentOutOfRangeException ex)
        {
            // Handle validation errors for barcode dimensions.
            Console.WriteLine("Error: " + ex.Message);
        }
        catch (Exception ex)
        {
            // Handle any unexpected errors.
            Console.WriteLine("Unexpected error: " + ex.Message);
        }
    }
}