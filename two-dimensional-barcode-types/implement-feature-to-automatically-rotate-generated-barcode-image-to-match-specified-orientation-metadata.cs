// Title: Automatic Barcode Rotation Based on Orientation Metadata
// Description: Demonstrates generating a barcode image and rotating it according to supplied orientation metadata.
// Category-Description: This example belongs to the Aspose.BarCode image generation category, showcasing how to use BarcodeGenerator, EncodeTypes, and rotation parameters. Developers often need to align barcodes with document layouts or metadata-driven orientations, and this snippet illustrates reading orientation data, validating it, and applying the RotationAngle property before saving the image.
// Prompt: Implement feature to automatically rotate generated barcode image to match specified orientation metadata.
// Tags: barcode symbology, rotation, image generation, code128, aspose.barcode, csharp

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Generates a Code128 barcode and rotates the image based on orientation metadata.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Reads orientation metadata, validates it, creates a barcode,
    /// applies the rotation, and saves the resulting image.
    /// </summary>
    static void Main()
    {
        // Sample barcode data to encode.
        string codeText = "1234567890";

        // Simulated orientation metadata (could be read from a file or other source).
        // Acceptable values: 0, 90, 180, 270.
        string orientationMeta = "90";

        // Try to parse the metadata into a floating‑point rotation angle.
        if (!float.TryParse(orientationMeta, out float rotationAngle))
        {
            Console.WriteLine("Invalid orientation metadata. Using 0 degrees.");
            rotationAngle = 0f;
        }

        // Ensure the rotation angle is one of the supported values.
        if (rotationAngle != 0f && rotationAngle != 90f && rotationAngle != 180f && rotationAngle != 270f)
        {
            Console.WriteLine("Unsupported rotation angle. Using 0 degrees.");
            rotationAngle = 0f;
        }

        // Prepare the output directory.
        string outputDir = Path.Combine(Directory.GetCurrentDirectory(), "Output");
        if (!Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        // Full path for the generated barcode image.
        string outputPath = Path.Combine(outputDir, "rotated_barcode.png");

        // Create and configure the barcode generator.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
        {
            // Apply the validated rotation angle.
            generator.Parameters.RotationAngle = rotationAngle;

            // Optional visual customizations.
            generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Black;
            generator.Parameters.BackColor = Aspose.Drawing.Color.White;

            // Save the barcode image to the specified path.
            generator.Save(outputPath);
        }

        Console.WriteLine($"Barcode saved to: {outputPath}");
    }
}