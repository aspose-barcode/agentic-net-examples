// Title: Adjust Barcode Padding After Rotation
// Description: This example rotates a Code128 barcode and automatically adjusts its padding to prevent the barcode edges from being cut off when saved as an image.
// Category-Description: Demonstrates Aspose.BarCode generation features such as setting rotation angles, customizing padding, and exporting to PNG. It showcases the BarcodeGenerator class, its Parameters.RotationAngle property, and the Padding settings within Parameters.Barcode. Developers working with barcode rendering often need to rotate barcodes for layout purposes while ensuring the full barcode remains visible; this example provides a reusable pattern for handling that scenario.
// Prompt: Create a script that automatically adjusts padding after rotation to prevent barcode edges from being cut off.
// Tags: barcode symbology, rotation, padding, image output, aspose.barcode, code128, png

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates automatic padding adjustment for a rotated barcode using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a rotated Code128 barcode, adjusts its padding, and saves it as a PNG file.
    /// </summary>
    static void Main()
    {
        // Define a temporary output folder and ensure it exists
        string outputFolder = Path.Combine(Path.GetTempPath(), "AsposeBarcodeDemo");
        Directory.CreateDirectory(outputFolder);

        // Full path for the resulting image file
        string outputPath = Path.Combine(outputFolder, "rotated_barcode.png");

        // Create a barcode generator for Code128 with sample data
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            // Set rotation angle (allowed values: 0, 90, 180, 270)
            generator.Parameters.RotationAngle = 90f;

            // Adjust padding automatically after rotation to avoid clipping
            AdjustPadding(generator);

            // Save the barcode image in PNG format
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the file was saved
        Console.WriteLine($"Barcode saved to: {outputPath}");
    }

    /// <summary>
    /// Adjusts individual padding values based on the current rotation angle
    /// to ensure that barcode edges are not cut off.
    /// </summary>
    /// <param name="generator">The BarcodeGenerator instance to modify.</param>
    static void AdjustPadding(BarcodeGenerator generator)
    {
        // Base padding in points
        float basePadding = 10f;

        // Retrieve the current rotation angle
        float angle = generator.Parameters.RotationAngle;

        // For 90° or 270° rotation the barcode dimensions swap,
        // so we increase horizontal padding slightly.
        if (Math.Abs(angle - 90f) < 0.001f || Math.Abs(angle - 270f) < 0.001f)
        {
            generator.Parameters.Barcode.Padding.Left.Point = basePadding + 5f;
            generator.Parameters.Barcode.Padding.Right.Point = basePadding + 5f;
            generator.Parameters.Barcode.Padding.Top.Point = basePadding;
            generator.Parameters.Barcode.Padding.Bottom.Point = basePadding;
        }
        else // 0° or 180°
        {
            generator.Parameters.Barcode.Padding.Left.Point = basePadding;
            generator.Parameters.Barcode.Padding.Right.Point = basePadding;
            generator.Parameters.Barcode.Padding.Top.Point = basePadding;
            generator.Parameters.Barcode.Padding.Bottom.Point = basePadding;
        }
    }
}