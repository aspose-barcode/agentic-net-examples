using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generation of a DotCode barcode with custom padding,
/// then validates that the padding is reflected in the resulting image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates a barcode, reads its dimensions, and verifies padding.
    /// </summary>
    static void Main()
    {
        // --------------------------------------------------------------------
        // Define test parameters
        // --------------------------------------------------------------------
        const string codeText = "123456";          // Text to encode in the barcode
        const float paddingPoints = 20f;           // Desired margin (padding) in points
        const string outputPath = "dotcode_test.png"; // Output image file name

        // --------------------------------------------------------------------
        // Generate DotCode barcode with the specified padding
        // --------------------------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.DotCode, codeText))
        {
            // Apply uniform padding (margin) on all four sides
            generator.Parameters.Barcode.Padding.Left.Point   = paddingPoints;
            generator.Parameters.Barcode.Padding.Top.Point    = paddingPoints;
            generator.Parameters.Barcode.Padding.Right.Point  = paddingPoints;
            generator.Parameters.Barcode.Padding.Bottom.Point = paddingPoints;

            // Save the generated barcode image as PNG
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // --------------------------------------------------------------------
        // Load the generated image to obtain its pixel dimensions
        // --------------------------------------------------------------------
        int imageWidthPixels;
        int imageHeightPixels;
        using (var bitmap = new Bitmap(outputPath))
        {
            imageWidthPixels  = bitmap.Width;
            imageHeightPixels = bitmap.Height;
        }

        // --------------------------------------------------------------------
        // Recognize the barcode to retrieve its bounding box (region) within the image
        // --------------------------------------------------------------------
        RectangleF barcodeRegion;
        using (var reader = new BarCodeReader(outputPath, DecodeType.DotCode))
        {
            var results = reader.ReadBarCodes();
            if (results.Length == 0)
            {
                Console.WriteLine("Failed to read the generated DotCode barcode.");
                return;
            }

            // Region.Rectangle provides the bounding box in points
            barcodeRegion = results[0].Region.Rectangle;
        }

        // --------------------------------------------------------------------
        // Retrieve the resolution (DPI) used during generation (default is 96 DPI)
        // --------------------------------------------------------------------
        float resolutionDpi;
        using (var gen = new BarcodeGenerator(EncodeTypes.DotCode, codeText))
        {
            resolutionDpi = gen.Parameters.Resolution;
        }

        // --------------------------------------------------------------------
        // Convert padding from points to pixels (1 point = 1/72 inch)
        // --------------------------------------------------------------------
        float pointsToPixels      = resolutionDpi / 72f;
        float expectedPaddingPixels = paddingPoints * pointsToPixels;

        // --------------------------------------------------------------------
        // Calculate actual padding based on image size and barcode region size
        // --------------------------------------------------------------------
        float actualHorizontalPadding = (imageWidthPixels  - barcodeRegion.Width)  / 2f;
        float actualVerticalPadding   = (imageHeightPixels - barcodeRegion.Height) / 2f;

        // Allow a small tolerance due to rounding errors
        const float tolerance = 1.0f;

        bool horizontalOk = Math.Abs(actualHorizontalPadding - expectedPaddingPixels) <= tolerance;
        bool verticalOk   = Math.Abs(actualVerticalPadding   - expectedPaddingPixels) <= tolerance;

        // --------------------------------------------------------------------
        // Output verification results
        // --------------------------------------------------------------------
        Console.WriteLine($"Image size (pixels): {imageWidthPixels}x{imageHeightPixels}");
        Console.WriteLine($"Barcode region size (points): {barcodeRegion.Width}x{barcodeRegion.Height}");
        Console.WriteLine($"Resolution (dpi): {resolutionDpi}");
        Console.WriteLine($"Expected padding (pixels): {expectedPaddingPixels:F2}");
        Console.WriteLine($"Actual horizontal padding (pixels): {actualHorizontalPadding:F2}");
        Console.WriteLine($"Actual vertical padding (pixels): {actualVerticalPadding:F2}");

        if (horizontalOk && verticalOk)
        {
            Console.WriteLine("PASS: DotCode margin property correctly influences surrounding whitespace.");
        }
        else
        {
            Console.WriteLine("FAIL: Margin influence does not match expected values.");
        }

        // --------------------------------------------------------------------
        // Clean up the generated file
        // --------------------------------------------------------------------
        try
        {
            File.Delete(outputPath);
        }
        catch
        {
            // Ignore any cleanup errors
        }
    }
}