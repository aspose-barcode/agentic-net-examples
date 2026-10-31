// Title: Barcode Generation with Rotation Angle Validation
// Description: Demonstrates generating Code128 barcodes with various rotation angles, including handling of invalid angles exceeding 360 degrees.
// Category-Description: This example belongs to the Aspose.BarCode image generation category, showcasing how to use BarcodeGenerator, set rotation angles, and save barcodes as PNG files. Developers often need to rotate barcodes for layout purposes while ensuring angle values stay within supported limits. The snippet illustrates validation, error handling, and typical API usage for barcode image creation.
// Prompt: Implement error handling for invalid RotationAngle values exceeding 360 degrees when generating barcode images.
// Tags: barcode symbology, rotation, image generation, error handling, aspose.barcode, code128, png

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates barcode generation with rotation angle validation and error handling.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates barcodes at multiple rotation angles,
    /// including an invalid angle to showcase validation logic.
    /// </summary>
    static void Main()
    {
        // Sample barcode text to encode.
        const string codeText = "ASPOSE";

        // Rotation angles to test; includes an invalid value (400°).
        int[] angles = { 0, 90, 180, 270, 400 };

        // Create an output directory in the temporary folder.
        string outputDir = Path.Combine(Path.GetTempPath(), "BarcodeRotationDemo");
        Directory.CreateDirectory(outputDir);

        // Generate a barcode for each specified angle.
        foreach (int angle in angles)
        {
            GenerateBarcodeWithRotation(codeText, angle, outputDir);
        }

        Console.WriteLine("Barcode generation completed.");
    }

    /// <summary>
    /// Generates a Code128 barcode with the specified rotation angle and saves it as a PNG file.
    /// If the angle exceeds the allowed range, it defaults to 0° and logs a warning.
    /// </summary>
    /// <param name="text">The text to encode in the barcode.</param>
    /// <param name="angleDegrees">Desired rotation angle in degrees.</param>
    /// <param name="outputDirectory">Directory where the barcode image will be saved.</param>
    static void GenerateBarcodeWithRotation(string text, int angleDegrees, string outputDirectory)
    {
        // Validate rotation angle: allowed range is -360 to 360 degrees.
        if (Math.Abs(angleDegrees) > 360)
        {
            Console.WriteLine($"Invalid rotation angle {angleDegrees}°. It must be within -360 to 360 degrees. Using 0° instead.");
            angleDegrees = 0;
        }

        // Construct the output file name and path.
        string fileName = $"Barcode_Rotation_{angleDegrees}.png";
        string filePath = Path.Combine(outputDirectory, fileName);

        // Initialize the barcode generator with Code128 symbology.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, text))
        {
            // Apply the validated rotation angle (property expects a float).
            generator.Parameters.RotationAngle = (float)angleDegrees;

            try
            {
                // Save the generated barcode as a PNG image.
                generator.Save(filePath, BarCodeImageFormat.Png);
                Console.WriteLine($"Saved barcode with rotation {angleDegrees}° to: {filePath}");
            }
            catch (Exception ex)
            {
                // Log any errors that occur during barcode generation or saving.
                Console.WriteLine($"Failed to generate barcode with rotation {angleDegrees}°: {ex.Message}");
            }
        }
    }
}