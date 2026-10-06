// Title: Adjust Barcode Padding After Rotation to Prevent Clipping
// Description: Demonstrates how to rotate a barcode image and add sufficient padding so that the rotated barcode edges are not cut off.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing the use of BarcodeGenerator, rotation, and padding APIs. Developers often need to rotate barcodes for layout constraints while ensuring the full barcode remains visible; this snippet illustrates setting rotation angles, applying uniform padding, and optionally drawing a border for visual reference.
// Prompt: Create a script that automatically adjusts padding after rotation to prevent barcode edges from being cut off.
// Tags: barcode symbology, rotation, padding, clipping, png, aspose.barcode, code128, image generation

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode;
using Aspose.Drawing;

/// <summary>
/// Generates a Code128 barcode, rotates it, and adds padding to avoid clipping of the barcode edges.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates a temporary directory, generates the barcode, applies rotation and padding,
    /// saves the image, and writes the output path to the console.
    /// </summary>
    static void Main()
    {
        // Define output directory in the system temporary folder and ensure it exists
        string outputDir = Path.Combine(Path.GetTempPath(), "BarcodePaddingDemo");
        Directory.CreateDirectory(outputDir);

        // Full path for the resulting PNG image
        string outputPath = Path.Combine(outputDir, "rotated_barcode.png");

        // Initialize the barcode generator with Code128 symbology and sample data
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "Sample123"))
        {
            // Rotate the barcode by 45 degrees
            generator.Parameters.RotationAngle = 45f;

            // Apply uniform padding (30 points) on all sides to prevent clipping after rotation
            float paddingPoints = 30f;
            generator.Parameters.Barcode.Padding.Left.Point = paddingPoints;
            generator.Parameters.Barcode.Padding.Top.Point = paddingPoints;
            generator.Parameters.Barcode.Padding.Right.Point = paddingPoints;
            generator.Parameters.Barcode.Padding.Bottom.Point = paddingPoints;

            // Optional: make the border visible for visual reference and set its width
            generator.Parameters.Border.Visible = true;
            generator.Parameters.Border.Width.Point = 1f;

            // Save the generated barcode image as PNG
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Output the location of the saved barcode image
        Console.WriteLine($"Barcode saved to: {outputPath}");
    }
}