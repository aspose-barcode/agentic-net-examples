// Title: Barcode Generation with Rotation Angle Validation
// Description: Demonstrates creating a Code128 barcode image, applying a rotation angle, and handling invalid angles that exceed 360 degrees.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing how to use the BarcodeGenerator class to produce barcode images with specific visual transformations. It covers setting rotation angles, validating input ranges, and handling errors—common tasks for developers integrating barcode creation into reporting, labeling, or inventory systems.
// Prompt: Implement error handling for invalid RotationAngle values exceeding 360 degrees when generating barcode images.
// Tags: barcode symbology, rotation, error handling, png, aspose.barcode, generation

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Provides an example of generating barcode images with rotation and validates rotation angles.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates output directory, generates barcodes with valid and invalid rotation angles.
    /// </summary>
    static void Main()
    {
        // Define a temporary output directory for the generated barcode images.
        string outputDir = Path.Combine(Path.GetTempPath(), "BarcodeRotationDemo");
        Directory.CreateDirectory(outputDir);

        // Sample barcode text to encode.
        string codeText = "123456";

        // Generate a barcode with a valid rotation angle (90 degrees).
        float validAngle = 90f;
        string validPath = Path.Combine(outputDir, "barcode_valid.png");
        GenerateBarcodeWithRotation(codeText, validAngle, validPath);

        // Attempt to generate a barcode with an invalid rotation angle (exceeds 360 degrees).
        float invalidAngle = 450f;
        string invalidPath = Path.Combine(outputDir, "barcode_invalid.png");
        GenerateBarcodeWithRotation(codeText, invalidAngle, invalidPath);
    }

    /// <summary>
    /// Generates a barcode image with the specified rotation angle.
    /// Validates the angle to be within 0 to 360 degrees.
    /// </summary>
    /// <param name="codeText">The text to encode.</param>
    /// <param name="angle">Rotation angle in degrees.</param>
    /// <param name="outputPath">File path to save the image.</param>
    static void GenerateBarcodeWithRotation(string codeText, float angle, string outputPath)
    {
        try
        {
            // Validate rotation angle; throw if out of acceptable range.
            if (angle < 0f || angle > 360f)
            {
                throw new ArgumentOutOfRangeException(nameof(angle), angle,
                    "RotationAngle must be between 0 and 360 degrees.");
            }

            // Initialize the barcode generator with Code128 symbology.
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
            {
                // Apply the validated rotation angle.
                generator.Parameters.RotationAngle = angle;

                // Save the generated barcode as a PNG image.
                generator.Save(outputPath, BarCodeImageFormat.Png);
                Console.WriteLine($"Barcode saved to: {outputPath} (RotationAngle={angle})");
            }
        }
        catch (ArgumentOutOfRangeException ex)
        {
            // Handle validation errors for rotation angle.
            Console.WriteLine($"Error: {ex.Message}");
        }
        catch (Exception ex)
        {
            // Handle any other unexpected errors.
            Console.WriteLine($"Unexpected error: {ex.Message}");
        }
    }
}