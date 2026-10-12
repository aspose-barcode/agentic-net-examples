// Title: Verify auto‑sizing of barcode images when BarCodeHeight is zero
// Description: Demonstrates that a barcode generated with default height automatically adjusts its image size based on the length of the encoded text.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how the BarcodeGenerator class determines image dimensions without explicit BarCodeHeight or AutoSizeMode settings. Developers often need to create barcodes that adapt to varying content lengths, ensuring optimal readability and layout in reports or UI components. The snippet shows typical usage of EncodeTypes, BarCodeImageFormat, and image size extraction for validation.
// Prompt: Create unit test verifying barcode with BarCodeHeight zero enables auto‑size based on content, using default units.
// Tags: barcode, autosize, code128, image, unit-test, aspose.barcode, generation

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Contains the entry point for the barcode auto‑size verification example.
/// </summary>
class Program
{
    /// <summary>
    /// Generates two barcodes of different lengths, compares their image dimensions, and reports whether auto‑sizing adapts to content.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary directory for test artifacts.
        string tempDir = Path.Combine(Path.GetTempPath(), "BarcodeAutoSizeTest_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempDir);

        // Define output file paths for short and long barcode images.
        string pathShort = Path.Combine(tempDir, "short.png");
        string pathLong = Path.Combine(tempDir, "long.png");

        // Generate barcodes with differing text lengths.
        GenerateBarcode("A", pathShort);
        GenerateBarcode("ABCDEFGHIJKLMN", pathLong);

        // Retrieve image dimensions for each generated barcode.
        var sizeShort = GetImageSize(pathShort);
        var sizeLong = GetImageSize(pathLong);

        // Output the measured dimensions to the console.
        Console.WriteLine($"Short code size: {sizeShort.Width}x{sizeShort.Height}");
        Console.WriteLine($"Long code size: {sizeLong.Width}x{sizeLong.Height}");

        // Verify that the image size changes with content length.
        if (sizeShort.Width != sizeLong.Width || sizeShort.Height != sizeLong.Height)
        {
            Console.WriteLine("PASS: Image size adapts to content length.");
        }
        else
        {
            Console.WriteLine("FAIL: Image size does not adapt to content length.");
        }

        // Clean up temporary files and directory.
        try
        {
            File.Delete(pathShort);
            File.Delete(pathLong);
            Directory.Delete(tempDir);
        }
        catch
        {
            // Ignore cleanup errors.
        }
    }

    /// <summary>
    /// Generates a barcode image using the default height (zero) which enables auto‑sizing.
    /// </summary>
    /// <param name="codeText">The text to encode in the barcode.</param>
    /// <param name="outputPath">The file path where the barcode image will be saved.</param>
    static void GenerateBarcode(string codeText, string outputPath)
    {
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
        {
            // No BarHeight or AutoSizeMode set; defaults allow auto‑sizing based on content.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }
    }

    /// <summary>
    /// Retrieves the width and height of an image file.
    /// </summary>
    /// <param name="imagePath">The path to the image file.</param>
    /// <returns>A tuple containing the image width and height in pixels.</returns>
    static (int Width, int Height) GetImageSize(string imagePath)
    {
        using (var image = Image.FromFile(imagePath))
        {
            var bitmap = (Bitmap)image;
            return (bitmap.Width, bitmap.Height);
        }
    }
}