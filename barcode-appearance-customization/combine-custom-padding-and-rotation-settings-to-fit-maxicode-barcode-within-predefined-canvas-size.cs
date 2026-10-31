// Title: Generate MaxiCode barcode with custom padding and rotation within a fixed canvas
// Description: Demonstrates how to create a MaxiCode barcode, apply custom padding and rotate it to fit a predefined 500x500 pixel canvas.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing the use of BarcodeGenerator, EncodeTypes, and AutoSizeMode to produce barcodes with precise layout control. Typical use cases include embedding barcodes in fixed-size graphics, adjusting visual margins, and rotating symbols for design requirements. Developers often need to manipulate padding, rotation, and canvas dimensions to meet branding or packaging specifications.
// Prompt: Combine custom padding and rotation settings to fit a MaxiCode barcode within a predefined canvas size.
// Tags: maxicode, barcode, padding, rotation, canvas, image, png, aspose.barcode, generation

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Example program that generates a MaxiCode barcode with custom padding and rotation
/// to fit a fixed canvas size.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates the barcode and saves it as a PNG file.
    /// </summary>
    static void Main()
    {
        // Define the full path for the output PNG file
        string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "maxicode.png");

        // Sample text to encode in the MaxiCode barcode
        string codeText = "Sample MaxiCode";

        // Initialize the barcode generator for the MaxiCode symbology
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.MaxiCode, codeText))
        {
            // -----------------------------------------------------------------
            // Canvas configuration: set a fixed size of 500x500 pixels
            // -----------------------------------------------------------------
            generator.Parameters.ImageWidth.Pixels = 500f;
            generator.Parameters.ImageHeight.Pixels = 500f;

            // Use the Nearest auto-size mode so the barcode respects the fixed canvas
            // while allowing padding and rotation adjustments
            generator.Parameters.AutoSizeMode = AutoSizeMode.Nearest;

            // -----------------------------------------------------------------
            // Visual appearance settings
            // -----------------------------------------------------------------
            // Define the module (dot) size of the barcode
            generator.Parameters.Barcode.XDimension.Point = 5f;

            // Apply custom padding on all sides (20 points each)
            generator.Parameters.Barcode.Padding.Left.Point = 20f;
            generator.Parameters.Barcode.Padding.Top.Point = 20f;
            generator.Parameters.Barcode.Padding.Right.Point = 20f;
            generator.Parameters.Barcode.Padding.Bottom.Point = 20f;

            // Rotate the entire barcode by 45 degrees
            generator.Parameters.RotationAngle = 45f;

            // Ensure the default aspect ratio for MaxiCode (1:1)
            generator.Parameters.Barcode.MaxiCode.AspectRatio = 1f;

            // -----------------------------------------------------------------
            // Save the generated barcode image as PNG
            // -----------------------------------------------------------------
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        Console.WriteLine($"MaxiCode barcode saved to: {outputPath}");
    }
}