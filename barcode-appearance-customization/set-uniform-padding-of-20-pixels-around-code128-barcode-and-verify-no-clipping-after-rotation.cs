// Title: Code128 Barcode with Uniform Padding and Rotation
// Description: Demonstrates how to apply a 20‑pixel padding around a Code128 barcode, rotate it, and save the result as PNG while ensuring no clipping occurs.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing the use of BarcodeGenerator, Padding, and RotationAngle properties. Typical use cases include creating printable barcodes with consistent margins and orientation adjustments. Developers often need to control visual layout to avoid clipping when rotating or scaling barcodes, and this snippet illustrates those common requirements.
/// Prompt: Set uniform Padding of 20 pixels around a Code128 barcode and verify no clipping after rotation.
/// Tags: code128, padding, rotation, png, aspose.barcode, barcode-generation

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Generates a Code128 barcode with uniform 20‑pixel padding, rotates it, and saves the image as PNG.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that creates the barcode, applies padding and rotation, saves the image, and outputs its dimensions.
    /// </summary>
    static void Main()
    {
        // Define output directory and ensure it exists
        string outputDir = Path.Combine(Path.GetTempPath(), "BarcodeDemo");
        Directory.CreateDirectory(outputDir);

        // Full path for the generated PNG file
        string outputPath = Path.Combine(outputDir, "Code128_Padded_Rotated.png");

        // Initialize the barcode generator with Code128 symbology and sample data
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "ASPOSE123"))
        {
            // Apply uniform padding of 20 pixels on all four sides
            generator.Parameters.Barcode.Padding.Left.Pixels = 20f;
            generator.Parameters.Barcode.Padding.Top.Pixels = 20f;
            generator.Parameters.Barcode.Padding.Right.Pixels = 20f;
            generator.Parameters.Barcode.Padding.Bottom.Pixels = 20f;

            // Rotate the barcode by 45 degrees
            generator.Parameters.RotationAngle = 45f;

            // Save the rotated barcode image to PNG format
            generator.Save(outputPath, BarCodeImageFormat.Png);

            // Generate a bitmap to verify that the image dimensions include the padding (no clipping)
            using (Bitmap bitmap = generator.GenerateBarCodeImage())
            {
                Console.WriteLine($"Barcode image saved to: {outputPath}");
                Console.WriteLine($"Image dimensions (pixels): Width={bitmap.Width}, Height={bitmap.Height}");
            }
        }
    }
}