// Title: Verify barcode image width when setting ImageWidth in pixels
// Description: Demonstrates how to set the barcode image width in pixels using Aspose.BarCode and validates the generated image dimensions.
// Category-Description: This example belongs to the Aspose.BarCode image generation category, illustrating the use of BarcodeGenerator, ImageWidth, and AutoSizeMode to control output size. Typical scenarios include unit testing visual dimensions, creating fixed‑size barcodes for UI layouts, and ensuring compliance with design specifications. Developers often need to verify that pixel‑based size settings produce the expected image dimensions across formats.
// Prompt: Design unit test verifying BarCodeWidth set in Pixels yields correct pixel width after generation.
// Tags: barcode, code128, imagewidth, pixels, autosizemode, png, aspose.barcode, aspose.drawing, unit-test, image-validation

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Example program that generates a Code128 barcode with a specified pixel width,
/// then verifies that the resulting PNG image matches the expected width.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that creates a temporary barcode image, checks its width, reports the result,
    /// and cleans up temporary files.
    /// </summary>
    static void Main()
    {
        // Prepare a unique temporary folder for the test artifacts
        string tempFolder = Path.Combine(Path.GetTempPath(), "BarcodeWidthTest_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);
        string barcodePath = Path.Combine(tempFolder, "barcode.png");

        // Define the expected width in pixels (float for API, int for comparison)
        float expectedWidthPixels = 250f;
        int expectedWidthInt = (int)expectedWidthPixels;

        // Generate the barcode image with ImageWidth set in pixels
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "Test123"))
        {
            generator.Parameters.ImageWidth.Pixels = expectedWidthPixels;
            generator.Parameters.AutoSizeMode = AutoSizeMode.Nearest;
            generator.Save(barcodePath, BarCodeImageFormat.Png);
        }

        // Load the generated PNG and read its actual width
        int actualWidth;
        using (var bitmap = new Bitmap(barcodePath))
        {
            actualWidth = bitmap.Width;
        }

        // Report the verification result
        if (actualWidth == expectedWidthInt)
        {
            Console.WriteLine($"PASSED: Barcode image width is {actualWidth} pixels as expected.");
        }
        else
        {
            Console.WriteLine($"FAILED: Expected width {expectedWidthInt} pixels but got {actualWidth} pixels.");
        }

        // Clean up temporary files and folder; ignore any cleanup errors
        try
        {
            if (File.Exists(barcodePath))
                File.Delete(barcodePath);
            if (Directory.Exists(tempFolder))
                Directory.Delete(tempFolder, true);
        }
        catch
        {
            // Ignored - cleanup failure should not affect test result
        }
    }
}