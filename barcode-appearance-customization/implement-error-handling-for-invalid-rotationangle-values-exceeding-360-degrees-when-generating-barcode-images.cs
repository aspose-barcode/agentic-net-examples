// Title: Generating Barcodes with Rotation and Validating Rotation Angles
// Description: Demonstrates how to generate barcode images with various rotation angles while validating that the angles stay within acceptable bounds.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating the use of BarcodeGenerator, EncodeTypes, and rotation parameters. Developers often need to rotate barcodes for layout requirements, and must ensure rotation values are within -360 to 360 degrees to avoid runtime errors. The snippet shows error handling for out‑of‑range angles, a common task when programmatically creating barcodes.
// Prompt: Implement error handling for invalid RotationAngle values exceeding 360 degrees when generating barcode images.
// Tags: barcode symbology, rotation, error handling, png output, aspnet, aspose.barcode, generation

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates barcode generation with rotation angle validation.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that creates barcodes at various rotation angles, handling invalid values.
    /// </summary>
    static void Main()
    {
        // Define a set of rotation angles, including values that exceed the valid range.
        float[] angles = new float[] { 0f, 90f, 180f, 400f, -450f };

        // Create a unique temporary folder to store the generated barcode images.
        string outputFolder = Path.Combine(Path.GetTempPath(), "Barcodes_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputFolder);

        // Process each angle: validate, generate barcode, and handle any errors.
        foreach (float angle in angles)
        {
            try
            {
                // Ensure the rotation angle is within the allowed -360 to 360 degree range.
                ValidateRotationAngle(angle);

                // Initialize the barcode generator with Code128 symbology and sample text.
                using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "SampleText"))
                {
                    // Apply the validated rotation angle.
                    generator.Parameters.RotationAngle = angle;

                    // Build the output file path and save the barcode as a PNG image.
                    string filePath = Path.Combine(outputFolder, $"Barcode_Rotation_{angle}.png");
                    generator.Save(filePath, BarCodeImageFormat.Png);

                    Console.WriteLine($"Generated barcode with rotation {angle}° at: {filePath}");
                }
            }
            catch (ArgumentOutOfRangeException ex)
            {
                // Handle cases where the rotation angle is outside the permitted range.
                Console.WriteLine($"Invalid rotation angle {angle}°: {ex.Message}");
            }
            catch (Exception ex)
            {
                // Handle any other unexpected errors during barcode generation.
                Console.WriteLine($"Error generating barcode with rotation {angle}°: {ex.Message}");
            }
        }

        Console.WriteLine("Processing completed.");
    }

    /// <summary>
    /// Validates that the rotation angle is within the inclusive range of -360 to 360 degrees.
    /// </summary>
    /// <param name="angle">The rotation angle to validate.</param>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when the angle is outside the valid range.</exception>
    static void ValidateRotationAngle(float angle)
    {
        if (Math.Abs(angle) > 360f)
        {
            throw new ArgumentOutOfRangeException(nameof(angle), "RotationAngle must be within -360 to 360 degrees.");
        }
    }
}