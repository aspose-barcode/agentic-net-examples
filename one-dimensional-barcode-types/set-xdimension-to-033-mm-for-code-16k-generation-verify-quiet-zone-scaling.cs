// Title: Code 16K Barcode Generation with Custom XDimension and Quiet Zone Scaling
// Description: Demonstrates how to set the XDimension to 0.33 mm for a Code 16K barcode and adjust quiet zone coefficients to observe image width changes.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, illustrating the use of BarcodeGenerator, EncodeTypes, and barcode parameter settings such as XDimension and quiet zone coefficients. Developers creating high‑density linear barcodes often need to control module size and quiet zone scaling to meet printing specifications. The snippet shows typical steps: configure parameters, generate PNG images, and compare dimensions.
// Prompt: Set XDimension to 0.33 mm for Code 16K generation, verify quiet zone scaling.
// Tags: code16k, xdimension, quietzone, barcode, generation, png, aspose.barcode, c#

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates setting XDimension and quiet zone coefficients for Code 16K barcodes.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that creates two Code 16K barcodes with different quiet zone scaling,
    /// saves them as PNG files, and prints their image widths.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary directory for output images
        string outputDir = Path.Combine(Path.GetTempPath(), "Code16K_XDimDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputDir);

        // Define file paths for the two generated images
        string path10 = Path.Combine(outputDir, "Code16K_QZ10.png");
        string path20 = Path.Combine(outputDir, "Code16K_QZ20.png");

        // Generate Code 16K barcode with quiet zone coefficient 10
        using (var generator = new BarcodeGenerator(EncodeTypes.Code16K, "Aspose.Barcode"))
        {
            // Set module size to 0.33 mm
            generator.Parameters.Barcode.XDimension.Millimeters = 0.33f;
            // Apply quiet zone scaling (left and right) of 10
            generator.Parameters.Barcode.Code16K.QuietZoneLeftCoef = 10;
            generator.Parameters.Barcode.Code16K.QuietZoneRightCoef = 10;
            // Save the barcode image as PNG
            generator.Save(path10, BarCodeImageFormat.Png);
        }

        // Generate Code 16K barcode with quiet zone coefficient 20
        using (var generator = new BarcodeGenerator(EncodeTypes.Code16K, "Aspose.Barcode"))
        {
            // Set module size to 0.33 mm (same as above)
            generator.Parameters.Barcode.XDimension.Millimeters = 0.33f;
            // Apply quiet zone scaling (left and right) of 20
            generator.Parameters.Barcode.Code16K.QuietZoneLeftCoef = 20;
            generator.Parameters.Barcode.Code16K.QuietZoneRightCoef = 20;
            // Save the barcode image as PNG
            generator.Save(path20, BarCodeImageFormat.Png);
        }

        // Variables to hold image widths for comparison
        int width10;
        int width20;

        // Load the first image and retrieve its width
        using (var bmp10 = new Bitmap(path10))
        {
            width10 = bmp10.Width;
        }

        // Load the second image and retrieve its width
        using (var bmp20 = new Bitmap(path20))
        {
            width20 = bmp20.Width;
        }

        // Output the measured widths and the difference caused by larger quiet zones
        Console.WriteLine($"Image width with quiet zone coefficient 10: {width10} px");
        Console.WriteLine($"Image width with quiet zone coefficient 20: {width20} px");
        Console.WriteLine($"Width increase due to larger quiet zones: {width20 - width10} px");
    }
}