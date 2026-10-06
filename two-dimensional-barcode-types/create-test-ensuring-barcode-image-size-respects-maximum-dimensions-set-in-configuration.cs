// Title: Barcode Image Size Validation with Maximum Dimensions
// Description: Demonstrates generating a barcode image while enforcing maximum width and height constraints, then verifies that the output respects those limits.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing how to configure image size parameters (ImageWidth, ImageHeight, AutoSizeMode) and resolution when creating barcodes. Typical use cases include automated testing of barcode rendering, ensuring compliance with layout specifications, and preparing images for print or display. Developers often need to validate that generated barcodes fit within predefined dimensions using classes like BarcodeGenerator and Bitmap.
/// Prompt: Create a test ensuring barcode image size respects maximum dimensions set in configuration.
/// Tags: barcode, code128, image-size, validation, aspose.barcode, generation, png, autosizemode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Generates a barcode image with size constraints, verifies the dimensions, and cleans up temporary files.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a barcode, checks its size against configured maximums, and reports the result.
    /// </summary>
    static void Main()
    {
        // Configuration: maximum allowed dimensions in pixels
        const float maxWidthPixels = 300f;
        const float maxHeightPixels = 200f;

        // Sample barcode data
        const string codeText = "Test123";

        // Prepare a temporary folder for output (optional, not required for the test)
        string tempPath = Path.Combine(Path.GetTempPath(), "BarcodeSizeTest_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempPath);
        string outputFile = Path.Combine(tempPath, "barcode.png");

        // Generate barcode with size constraints
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
        {
            // Set maximum dimensions and sizing mode
            generator.Parameters.ImageWidth.Pixels = maxWidthPixels;
            generator.Parameters.ImageHeight.Pixels = maxHeightPixels;
            generator.Parameters.AutoSizeMode = AutoSizeMode.Nearest;

            // Optional: set resolution for better quality
            generator.Parameters.Resolution = 300f;

            // Save to file (also used for verification)
            generator.Save(outputFile, BarCodeImageFormat.Png);
        }

        // Verify the generated image size
        using (var bitmap = new Bitmap(outputFile))
        {
            int actualWidth = bitmap.Width;
            int actualHeight = bitmap.Height;

            bool withinWidth = actualWidth <= maxWidthPixels;
            bool withinHeight = actualHeight <= maxHeightPixels;

            Console.WriteLine($"Generated barcode size: {actualWidth}x{actualHeight} pixels");
            Console.WriteLine($"Within max width ({maxWidthPixels}px): {withinWidth}");
            Console.WriteLine($"Within max height ({maxHeightPixels}px): {withinHeight}");

            if (withinWidth && withinHeight)
            {
                Console.WriteLine("Test passed: barcode image respects maximum dimensions.");
            }
            else
            {
                Console.WriteLine("Test failed: barcode image exceeds maximum dimensions.");
            }
        }

        // Clean up temporary files
        try
        {
            if (File.Exists(outputFile))
                File.Delete(outputFile);
            if (Directory.Exists(tempPath))
                Directory.Delete(tempPath, true);
        }
        catch
        {
            // Ignored - cleanup failure should not affect test result
        }
    }
}