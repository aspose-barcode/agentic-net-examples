using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates how to generate a barcode image with a custom width using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Validates and applies the barcode width.
    /// Throws <see cref="ArgumentOutOfRangeException"/> if the value is not supported.
    /// </summary>
    /// <param name="generator">The <see cref="BarcodeGenerator"/> instance to configure.</param>
    /// <param name="width">Desired barcode width in points. Must be greater than zero.</param>
    static void SetBarCodeWidth(BarcodeGenerator generator, float width)
    {
        // Ensure the width is a positive value.
        if (width <= 0f)
        {
            throw new ArgumentOutOfRangeException(nameof(width), "BarCodeWidth must be greater than zero.");
        }

        // ImageWidth controls the resulting BarCodeWidth when AutoSizeMode is set to Interpolation or Nearest.
        generator.Parameters.ImageWidth.Point = width;
    }

    /// <summary>
    /// Entry point of the application. Generates a Code128 barcode and saves it as a PNG file.
    /// </summary>
    static void Main()
    {
        // Define output file path and barcode content.
        const string outputPath = "barcode.png";
        const string codeText = "1234567890";

        try
        {
            // Initialize the barcode generator with the desired symbology and data.
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
            {
                // Use interpolation mode so that ImageWidth influences the final barcode width.
                generator.Parameters.AutoSizeMode = AutoSizeMode.Interpolation;

                // Attempt to set a valid width for the barcode image.
                SetBarCodeWidth(generator, 300f);

                // Save the generated barcode image to the specified file in PNG format.
                generator.Save(outputPath, BarCodeImageFormat.Png);
                Console.WriteLine($"Barcode saved to {outputPath}");
            }
        }
        catch (ArgumentOutOfRangeException ex)
        {
            // Handle unsupported width values.
            Console.WriteLine($"Invalid barcode width: {ex.Message}");
        }
        catch (Exception ex)
        {
            // General error handling for any other exceptions.
            Console.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}