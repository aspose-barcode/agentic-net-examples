// Title: Barcode Generation with Rotation Angle Validation
// Description: Demonstrates how to generate a barcode image with a specified rotation angle and validates that the angle stays within acceptable bounds.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing the use of BarcodeGenerator, rotation settings, and error handling. Developers often need to rotate barcodes for layout purposes while ensuring angles are within -360 to 360 degrees; this snippet illustrates proper validation and exception handling for such scenarios.
// Prompt: Implement error handling for invalid RotationAngle values exceeding 360 degrees when generating barcode images.
// Tags: barcode, rotation, validation, generation, png, aspose.barcode, encode-types, error-handling

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates barcode generation with rotation angle validation using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates a barcode with a valid rotation and attempts one with an invalid rotation to show error handling.
    /// </summary>
    static void Main()
    {
        // Prepare output directory in the temporary folder
        string outputDir = Path.Combine(Path.GetTempPath(), "BarcodeRotationDemo");
        Directory.CreateDirectory(outputDir);

        // Generate a barcode with a valid rotation angle (90 degrees)
        try
        {
            GenerateBarcodeWithRotation(
                EncodeTypes.Code128,
                "VALID90",
                90f,
                Path.Combine(outputDir, "valid_90.png"));
            Console.WriteLine("Generated barcode with valid rotation angle 90°.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error generating valid barcode: {ex.Message}");
        }

        // Attempt to generate a barcode with an invalid rotation angle (400 degrees)
        try
        {
            GenerateBarcodeWithRotation(
                EncodeTypes.Code128,
                "INVALID400",
                400f,
                Path.Combine(outputDir, "invalid_400.png"));
        }
        catch (ArgumentOutOfRangeException ex)
        {
            Console.WriteLine($"Caught expected exception for invalid rotation angle: {ex.Message}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Unexpected error: {ex.Message}");
        }
    }

    /// <summary>
    /// Generates a barcode image with the specified rotation angle after validating the angle.
    /// </summary>
    /// <param name="encodeType">The barcode symbology to use.</param>
    /// <param name="codeText">The text to encode in the barcode.</param>
    /// <param name="rotationAngle">The rotation angle in degrees.</param>
    /// <param name="outputPath">The file path where the image will be saved.</param>
    static void GenerateBarcodeWithRotation(BaseEncodeType encodeType, string codeText, float rotationAngle, string outputPath)
    {
        // Ensure the rotation angle is within the allowed range
        ValidateRotationAngle(rotationAngle);

        // Create the barcode generator, set rotation, and save the image
        using (var generator = new BarcodeGenerator(encodeType, codeText))
        {
            generator.Parameters.RotationAngle = rotationAngle;
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }
    }

    /// <summary>
    /// Validates that the rotation angle is between -360 and 360 degrees.
    /// </summary>
    /// <param name="angle">The rotation angle to validate.</param>
    static void ValidateRotationAngle(float angle)
    {
        if (angle > 360f || angle < -360f)
        {
            throw new ArgumentOutOfRangeException(nameof(angle), "RotationAngle must be between -360 and 360 degrees.");
        }
    }
}