// Title: Custom Padding and Rotation for MaxiCode Barcode
// Description: Generates a MaxiCode barcode, applies custom padding and a 45-degree rotation, and fits it into a 300x300 pixel canvas.
// Category-Description: This example belongs to the Aspose.BarCode generation category, demonstrating how to control canvas size, auto‑size mode, rotation, and padding for barcode images. It uses BarcodeGenerator, EncodeTypes, and related parameter classes to produce a PNG image. Developers often need to adjust these settings to meet layout constraints in packaging, shipping labels, or UI designs.
/// Prompt: Combine custom padding and rotation settings to fit a MaxiCode barcode within a predefined canvas size.
/// Tags: maxicode, barcode, padding, rotation, image, generation, aspose.barcode, png

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates creating a MaxiCode barcode with custom padding and rotation,
/// fitting it into a fixed-size canvas and saving as a PNG image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates the barcode and handles any errors.
    /// </summary>
    /// <param name="args">Command‑line arguments (not used).</param>
    static void Main(string[] args)
    {
        // Output file name
        string outputPath = "maxicode.png";

        try
        {
            // Initialize the barcode generator for MaxiCode with sample data
            using (var generator = new BarcodeGenerator(EncodeTypes.MaxiCode, "Sample MaxiCode"))
            {
                // ------------------------------------------------------------
                // Canvas configuration: set a fixed 300x300 pixel area
                // ------------------------------------------------------------
                generator.Parameters.ImageWidth.Pixels = 300f;
                generator.Parameters.ImageHeight.Pixels = 300f;

                // Use Nearest auto‑size mode to keep the barcode within the fixed canvas
                generator.Parameters.AutoSizeMode = AutoSizeMode.Nearest;

                // ------------------------------------------------------------
                // Visual adjustments: rotation and padding
                // ------------------------------------------------------------
                // Rotate the barcode 45 degrees
                generator.Parameters.RotationAngle = 45f;

                // Apply uniform padding of 10 points on all sides
                generator.Parameters.Barcode.Padding.Left.Point = 10f;
                generator.Parameters.Barcode.Padding.Top.Point = 10f;
                generator.Parameters.Barcode.Padding.Right.Point = 10f;
                generator.Parameters.Barcode.Padding.Bottom.Point = 10f;

                // ------------------------------------------------------------
                // Barcode-specific settings
                // ------------------------------------------------------------
                // Set module (dot) size
                generator.Parameters.Barcode.XDimension.Pixels = 2f;

                // Ensure the MaxiCode maintains a square aspect ratio
                generator.Parameters.Barcode.MaxiCode.AspectRatio = 1f;

                // ------------------------------------------------------------
                // Save the generated barcode image
                // ------------------------------------------------------------
                generator.Save(outputPath, BarCodeImageFormat.Png);
                Console.WriteLine($"MaxiCode barcode saved to: {Path.GetFullPath(outputPath)}");
            }
        }
        catch (Exception ex)
        {
            // Output any errors that occur during generation
            Console.WriteLine($"Error generating MaxiCode: {ex.Message}");
        }
    }
}