// Title: Adjust barcode padding after rotation to avoid clipping
// Description: Demonstrates how to calculate and apply extra padding to a rotated barcode so that its edges are not cut off.
// Category-Description: This example belongs to the Aspose.BarCode image manipulation category, illustrating the use of BarcodeGenerator, EncodeTypes, and BarCodeImageFormat to rotate barcodes and dynamically adjust padding. Developers often need to rotate barcodes for design layouts while ensuring the full code remains visible; this snippet shows the typical workflow for calculating required padding based on image dimensions.
// Prompt: Create a script that automatically adjusts padding after rotation to prevent barcode edges from being cut off.
// Tags: code128, rotation, padding, png, barcodegenerator, parameters, aspnet.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates automatic padding adjustment for a rotated barcode to prevent clipping.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates a Code128 barcode, rotates it, computes required padding, and saves as PNG.
    /// </summary>
    static void Main()
    {
        // Define the output file path
        string outputPath = "rotated_barcode.png";

        // Initialize a barcode generator for Code128 with sample text
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "Sample123"))
        {
            // Set the desired rotation angle (e.g., 45 degrees)
            float rotationAngle = 45f;
            generator.Parameters.RotationAngle = rotationAngle;

            // Generate a temporary barcode image to obtain its original dimensions
            using (var bitmap = generator.GenerateBarCodeImage())
            {
                // Original width and height in pixels
                int width = bitmap.Width;
                int height = bitmap.Height;

                // Calculate the diagonal length needed to contain the rotated image
                double diagonal = Math.Sqrt(width * width + height * height);

                // Determine extra space required on each side after rotation
                double extraPixels = (diagonal - Math.Max(width, height)) / 2.0;

                // Convert extra pixels to points (1 point = 1/72 inch, default DPI = 96)
                float extraPoints = (float)(extraPixels * 72.0 / 96.0);

                // Apply uniform padding on all sides based on the calculated extra space
                generator.Parameters.Barcode.Padding.Left.Point = extraPoints;
                generator.Parameters.Barcode.Padding.Top.Point = extraPoints;
                generator.Parameters.Barcode.Padding.Right.Point = extraPoints;
                generator.Parameters.Barcode.Padding.Bottom.Point = extraPoints;
            }

            // Save the rotated barcode with the adjusted padding to a PNG file
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Output the full path of the saved barcode image
        Console.WriteLine($"Barcode saved to '{Path.GetFullPath(outputPath)}'");
    }
}