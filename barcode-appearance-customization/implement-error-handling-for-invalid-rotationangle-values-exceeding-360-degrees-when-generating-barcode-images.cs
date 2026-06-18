using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates barcode generation with rotation angle validation using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates barcodes with valid and invalid rotation angles,
    /// handling any exceptions that may occur.
    /// </summary>
    static void Main()
    {
        // Generate a barcode using a valid rotation angle (45 degrees).
        try
        {
            GenerateBarcode(45f, "barcode_valid.png");
            Console.WriteLine("Barcode generated with valid rotation.");
        }
        catch (Exception ex)
        {
            // Log any unexpected errors that occur during valid barcode generation.
            Console.WriteLine($"Error generating valid barcode: {ex.Message}");
        }

        // Attempt to generate a barcode using an invalid rotation angle (400 degrees).
        try
        {
            GenerateBarcode(400f, "barcode_invalid.png");
            Console.WriteLine("Barcode generated with invalid rotation.");
        }
        catch (ArgumentOutOfRangeException ex)
        {
            // Handle the specific case where the rotation angle is out of the allowed range.
            Console.WriteLine($"Invalid rotation angle: {ex.Message}");
        }
        catch (Exception ex)
        {
            // Handle any other unexpected errors.
            Console.WriteLine($"Unexpected error: {ex.Message}");
        }
    }

    /// <summary>
    /// Generates a barcode image with the specified rotation angle and saves it to the given path.
    /// </summary>
    /// <param name="rotationAngle">The rotation angle in degrees (must be between 0 and 360 inclusive).</param>
    /// <param name="outputPath">The file path where the generated barcode image will be saved.</param>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Thrown when <paramref name="rotationAngle"/> is less than 0 or greater than 360.
    /// </exception>
    static void GenerateBarcode(float rotationAngle, string outputPath)
    {
        // Validate rotation angle: must be within 0 to 360 degrees inclusive.
        if (rotationAngle < 0f || rotationAngle > 360f)
        {
            throw new ArgumentOutOfRangeException(
                nameof(rotationAngle),
                "RotationAngle must be between 0 and 360 degrees inclusive.");
        }

        // Create and configure the barcode generator with Code128 symbology and sample data.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            // Apply the validated rotation angle to the barcode.
            generator.Parameters.RotationAngle = rotationAngle;

            // Save the generated barcode image to the specified file path.
            generator.Save(outputPath);
        }
    }
}