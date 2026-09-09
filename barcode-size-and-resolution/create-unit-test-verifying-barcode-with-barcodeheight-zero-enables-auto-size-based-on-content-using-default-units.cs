// Title: Barcode auto-size based on content with default BarCodeHeight
// Description: Demonstrates generating Code128 barcodes without specifying BarCodeHeight, allowing the library to auto‑size the image based on the encoded text.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how the BarcodeGenerator class automatically determines image dimensions when BarCodeHeight is left at its default (zero). Developers often need to create barcodes that adapt to varying data lengths without manually calculating size, especially for dynamic reporting or label printing scenarios. The key API classes used are BarcodeGenerator, EncodeTypes, and BarCodeImageFormat.
// Prompt: Create unit test verifying barcode with BarCodeHeight zero enables auto‑size based on content, using default units.
// Tags: barcode, code128, autosize, barcodheight, generation, png, aspose.barcode, unit-test

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that generates short and long Code128 barcodes without setting BarCodeHeight,
/// then verifies that the resulting images automatically adjust their dimensions based on content.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates temporary barcode images, compares their sizes,
    /// outputs the verification result, and cleans up temporary files.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary directory for the test files
        string tempDir = Path.Combine(Path.GetTempPath(), "BarcodeAutoSizeTest_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempDir);

        // Define file paths for short and long barcode images
        string shortPath = Path.Combine(tempDir, "short.png");
        string longPath = Path.Combine(tempDir, "long.png");

        // Generate a barcode with short text (single character)
        using (BarcodeGenerator genShort = new BarcodeGenerator(EncodeTypes.Code128, "A"))
        {
            // No explicit BarHeight or AutoSizeMode; defaults will be used
            genShort.Save(shortPath, BarCodeImageFormat.Png);
        }

        // Generate a barcode with longer text (multiple characters)
        using (BarcodeGenerator genLong = new BarcodeGenerator(EncodeTypes.Code128, "ABCDEFGHIJKLMNO"))
        {
            // Defaults remain unchanged, allowing auto‑size based on content length
            genLong.Save(longPath, BarCodeImageFormat.Png);
        }

        // Load the generated images to retrieve their dimensions
        int shortWidth, shortHeight, longWidth, longHeight;
        using (Image imgShort = Image.FromFile(shortPath))
        {
            shortWidth = imgShort.Width;
            shortHeight = imgShort.Height;
        }
        using (Image imgLong = Image.FromFile(longPath))
        {
            longWidth = imgLong.Width;
            longHeight = imgLong.Height;
        }

        // Determine whether the longer barcode produced a larger image
        bool widthIncreased = longWidth > shortWidth;
        bool heightIncreased = longHeight > shortHeight;

        // Output verification result
        if (widthIncreased && heightIncreased)
        {
            Console.WriteLine("PASSED: Barcode image size auto‑adjusted based on content.");
        }
        else
        {
            Console.WriteLine("FAILED: Barcode image size did not auto‑adjust as expected.");
            Console.WriteLine($"Short image size: {shortWidth}x{shortHeight}");
            Console.WriteLine($"Long image size: {longWidth}x{longHeight}");
        }

        // Clean up temporary files and directory
        try { File.Delete(shortPath); } catch { }
        try { File.Delete(longPath); } catch { }
        try { Directory.Delete(tempDir, true); } catch { }
    }
}