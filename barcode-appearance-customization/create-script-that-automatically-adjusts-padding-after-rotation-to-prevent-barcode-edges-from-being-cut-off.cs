// Title: Automatic Padding Adjustment for Rotated Barcode
// Description: Demonstrates how to increase barcode padding after applying rotation to ensure no edges are clipped.
// Category-Description: This example belongs to the Aspose.BarCode image generation category, illustrating the use of BarcodeGenerator, EncodeTypes, and rotation/padding parameters. Developers often need to rotate barcodes for design layouts while preserving readability; adjusting padding prevents the rotated image from being cut off. The snippet shows how to configure rotation, padding, and optional borders before saving to PNG.
// Prompt: Create a script that automatically adjusts padding after rotation to prevent barcode edges from being cut off.
// Tags: barcode symbology, rotation, padding, code128, png, aspose.barcode, image generation

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Generates a Code128 barcode, rotates it, and adds sufficient padding to avoid clipping.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates a temporary folder, generates a rotated barcode with extra padding,
    /// saves it as a PNG file, and writes the output path to the console.
    /// </summary>
    static void Main()
    {
        // Define a temporary output directory and ensure it exists
        string outputDir = Path.Combine(Path.GetTempPath(), "BarcodePaddingDemo");
        Directory.CreateDirectory(outputDir);

        // Full path for the resulting image file
        string outputPath = Path.Combine(outputDir, "RotatedBarcode.png");

        // Initialize the barcode generator with Code128 symbology and the desired text
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "ASPOSE"))
        {
            // Rotate the barcode by 45 degrees
            generator.Parameters.RotationAngle = 45f;

            // Increase padding on all sides to avoid cutting off after rotation
            generator.Parameters.Barcode.Padding.Left.Point = 30f;
            generator.Parameters.Barcode.Padding.Top.Point = 30f;
            generator.Parameters.Barcode.Padding.Right.Point = 30f;
            generator.Parameters.Barcode.Padding.Bottom.Point = 30f;

            // Optional: make the border visible for visual reference
            generator.Parameters.Border.Visible = true;
            generator.Parameters.Border.Width.Point = 1f;

            // Save the barcode image as PNG
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the image was saved
        Console.WriteLine($"Barcode image saved to: {outputPath}");
    }
}