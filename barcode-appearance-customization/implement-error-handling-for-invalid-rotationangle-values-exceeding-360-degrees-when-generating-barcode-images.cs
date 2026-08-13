// Title: Barcode Generation with Rotation Angle Validation
// Description: Demonstrates generating a Code128 barcode image while validating the rotation angle to ensure it stays within 0‑360 degrees.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to configure barcode parameters such as rotation, handle invalid input, and save images. It utilizes the BarcodeGenerator, EncodeTypes, and BarCodeImageFormat classes—key APIs for developers creating barcodes with custom orientation and robust error handling. Typical use cases include generating printable barcodes for inventory, shipping, or retail applications where rotation must be controlled.
// Prompt: Implement error handling for invalid RotationAngle values exceeding 360 degrees when generating barcode images.
// Tags: barcode symbology, generation, rotation, validation, png, aspose.barcode, code128

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Provides an example of generating Code128 barcodes with rotation angle validation using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a valid barcode and demonstrates handling of an invalid rotation angle.
    /// </summary>
    static void Main()
    {
        // Ensure the output directory exists
        string outputDir = Path.Combine(Directory.GetCurrentDirectory(), "Barcodes");
        if (!Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        // -------------------------
        // Generate a barcode with a valid rotation angle
        // -------------------------
        try
        {
            string validPath = Path.Combine(outputDir, "valid.png");
            GenerateBarcode("1234567890", 45f, validPath);
            Console.WriteLine($"Valid barcode saved to: {validPath}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error generating valid barcode: {ex.Message}");
        }

        // -------------------------
        // Attempt to generate a barcode with an invalid rotation angle (exceeds 360 degrees)
        // -------------------------
        try
        {
            string invalidPath = Path.Combine(outputDir, "invalid.png");
            GenerateBarcode("1234567890", 400f, invalidPath);
            Console.WriteLine($"Invalid barcode saved to: {invalidPath}");
        }
        catch (ArgumentOutOfRangeException ex)
        {
            // Expected exception for out-of-range rotation angle
            Console.WriteLine($"Caught expected exception for invalid rotation angle: {ex.Message}");
        }
        catch (Exception ex)
        {
            // Any other unexpected errors
            Console.WriteLine($"Unexpected error: {ex.Message}");
        }
    }

    /// <summary>
    /// Generates a barcode image with the specified text, rotation angle, and output path.
    /// </summary>
    /// <param name="codeText">The data to encode in the barcode.</param>
    /// <param name="rotationAngle">The rotation angle in degrees (0‑360 inclusive).</param>
    /// <param name="outputPath">The file path where the barcode image will be saved.</param>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when rotationAngle is outside the 0‑360 range.</exception>
    static void GenerateBarcode(string codeText, float rotationAngle, string outputPath)
    {
        // Validate rotation angle (must be between 0 and 360 inclusive)
        if (rotationAngle < 0f || rotationAngle > 360f)
        {
            throw new ArgumentOutOfRangeException(nameof(rotationAngle),
                $"RotationAngle must be between 0 and 360 degrees. Provided value: {rotationAngle}");
        }

        // Create and configure the barcode generator
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
        {
            generator.Parameters.RotationAngle = rotationAngle;
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }
    }
}