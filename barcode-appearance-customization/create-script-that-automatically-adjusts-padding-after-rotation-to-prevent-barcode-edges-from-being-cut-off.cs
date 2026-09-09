// Title: Adjust Barcode Padding After Rotation to Prevent Clipping
// Description: Demonstrates how to compute and apply dynamic padding when rotating a barcode image, ensuring the barcode edges remain fully visible.
// Category-Description: This example belongs to the Aspose.BarCode image manipulation category, illustrating the use of BarcodeGenerator, rotation settings, and padding adjustments. Developers often need to rotate barcodes for design layouts while avoiding clipping; the example shows typical API classes and patterns for such tasks.
// Prompt: Create a script that automatically adjusts padding after rotation to prevent barcode edges from being cut off.
// Tags: code128, rotation, padding, png, aspose.barcode, barcode generation

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing.Imaging;

/// <summary>
/// Generates a Code128 barcode, rotates it, and dynamically adjusts padding to avoid clipping.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates a temporary output folder, computes padding based on rotation,
    /// configures the barcode generator, and saves the resulting image.
    /// </summary>
    static void Main()
    {
        // Define a temporary output directory for the generated image
        string outputDir = Path.Combine(Path.GetTempPath(), "BarcodePaddingDemo");
        Directory.CreateDirectory(outputDir);

        // Barcode content and desired rotation angle (in degrees)
        string codeText = "ASPOSE";
        float rotationAngle = 45f;

        // Compute padding using a simple heuristic based on the rotation angle
        double rad = rotationAngle * Math.PI / 180.0;
        float padding = (float)(10 * (Math.Abs(Math.Sin(rad)) + Math.Abs(Math.Cos(rad))));

        // Initialize the barcode generator with Code128 symbology
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
        {
            // Apply the rotation to the barcode
            generator.Parameters.RotationAngle = rotationAngle;

            // Set uniform padding on all sides to prevent clipping after rotation
            generator.Parameters.Barcode.Padding.Left.Point = padding;
            generator.Parameters.Barcode.Padding.Top.Point = padding;
            generator.Parameters.Barcode.Padding.Right.Point = padding;
            generator.Parameters.Barcode.Padding.Bottom.Point = padding;

            // Optional: make the border visible for visual reference
            generator.Parameters.Border.Visible = true;
            generator.Parameters.Border.Width.Pixels = 2;
            generator.Parameters.Border.DashStyle = BorderDashStyle.Solid;

            // Save the rotated barcode image as PNG
            string outputPath = Path.Combine(outputDir, "RotatedBarcode.png");
            generator.Save(outputPath, BarCodeImageFormat.Png);
            Console.WriteLine($"Barcode saved to: {outputPath}");
        }
    }
}