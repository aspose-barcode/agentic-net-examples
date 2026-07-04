// Title: Automatic Padding Adjustment After Barcode Rotation
// Description: Demonstrates how to rotate a barcode and automatically increase padding to avoid clipping of edges.
// Prompt: Create a script that automatically adjusts padding after rotation to prevent barcode edges from being cut off.
// Tags: barcode, rotation, padding, code128, aspose.barcode, image output

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Example program that generates a Code128 barcode, rotates it,
/// and automatically adjusts padding to prevent the barcode edges from being cut off.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates a rotated barcode with dynamic padding and saves it as an image file.
    /// </summary>
    static void Main()
    {
        // Define the barcode text to encode.
        const string codeText = "Sample123";

        // Initialize the barcode generator with Code128 symbology.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
        {
            // Rotate the barcode by 90 degrees (clockwise).
            generator.Parameters.RotationAngle = 90f;

            // Set a base uniform padding (in points) around the barcode.
            float basePadding = 5f;
            generator.Parameters.Barcode.Padding.Left.Point   = basePadding;
            generator.Parameters.Barcode.Padding.Top.Point    = basePadding;
            generator.Parameters.Barcode.Padding.Right.Point  = basePadding;
            generator.Parameters.Barcode.Padding.Bottom.Point = basePadding;

            // Determine the effective rotation angle within a 0‑180° range.
            float rotation = generator.Parameters.RotationAngle % 180f;
            if (rotation < 0) rotation += 180f; // Normalize negative angles.

            // If the rotation is not a multiple of 180°, add extra padding to avoid clipping.
            if (Math.Abs(rotation) > 0.1f) // Non‑zero rotation threshold.
            {
                // Extra padding (in points) – adjust this value as needed for your use case.
                float extraPadding = 10f;

                generator.Parameters.Barcode.Padding.Left.Point   += extraPadding;
                generator.Parameters.Barcode.Padding.Top.Point    += extraPadding;
                generator.Parameters.Barcode.Padding.Right.Point  += extraPadding;
                generator.Parameters.Barcode.Padding.Bottom.Point += extraPadding;
            }

            // Optional: set background and bar colors for better visibility.
            generator.Parameters.BackColor          = Aspose.Drawing.Color.White;
            generator.Parameters.Barcode.BarColor   = Aspose.Drawing.Color.Black;

            // Save the rotated barcode image to a file.
            const string outputPath = "rotated_barcode.png";
            generator.Save(outputPath);
            Console.WriteLine($"Barcode saved to {outputPath}");
        }
    }
}