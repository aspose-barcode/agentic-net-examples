// Title: Verify auto‑sizing of barcode when BarCodeHeight is zero
// Description: Demonstrates that a barcode generated with default BarCodeHeight automatically adjusts its width based on the encoded content while keeping height constant.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how the BarcodeGenerator class determines image dimensions without explicit size settings. Developers often need to ensure barcodes scale correctly for varying data lengths, using default units and auto‑size behavior.
// Prompt: Create unit test verifying barcode with BarCodeHeight zero enables auto‑size based on content, using default units.
// Tags: barcode, autosize, code128, generation, unit-test, aspose.barcode, default-units

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates and validates that a barcode generated with default BarCodeHeight (zero) automatically sizes its width based on the encoded text.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that creates short and long Code128 barcodes, compares their dimensions, and reports the auto‑size result.
    /// </summary>
    static void Main()
    {
        // Create a temporary directory for any file output (optional, useful for visual inspection)
        string tempDir = Path.Combine(Path.GetTempPath(), "BarcodeAutoSizeTest_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempDir);

        // Define short and long code texts to test width scaling
        string shortText = "A";
        string longText = "ABCDEFGHIJKLMNO";

        // Variables to hold generated image dimensions
        int shortWidth, longWidth;
        int shortHeight, longHeight;

        // Generate barcode for the short text without setting BarCodeHeight (defaults to zero)
        using (var genShort = new BarcodeGenerator(EncodeTypes.Code128, shortText))
        {
            using (Bitmap bmp = genShort.GenerateBarCodeImage())
            {
                shortWidth = bmp.Width;
                shortHeight = bmp.Height;

                // Optionally save the image for manual verification
                string path = Path.Combine(tempDir, "short.png");
                bmp.Save(path, ImageFormat.Png);
            }
        }

        // Generate barcode for the long text without setting BarCodeHeight (defaults to zero)
        using (var genLong = new BarcodeGenerator(EncodeTypes.Code128, longText))
        {
            using (Bitmap bmp = genLong.GenerateBarCodeImage())
            {
                longWidth = bmp.Width;
                longHeight = bmp.Height;

                // Optionally save the image for manual verification
                string path = Path.Combine(tempDir, "long.png");
                bmp.Save(path, ImageFormat.Png);
            }
        }

        // Verify that the longer text results in a wider barcode (auto‑sized) while height remains unchanged
        bool widthIncreased = longWidth > shortWidth;
        bool heightEqual = longHeight == shortHeight; // Height should stay the same for the same symbology

        // Output the measured dimensions
        Console.WriteLine($"Short barcode size:  {shortWidth}x{shortHeight}");
        Console.WriteLine($"Long barcode size:   {longWidth}x{longHeight}");
        Console.WriteLine();

        // Report test result based on dimension comparison
        if (widthIncreased && heightEqual)
        {
            Console.WriteLine("PASSED: BarCodeHeight zero (default) enables auto-size based on content.");
        }
        else
        {
            Console.WriteLine("FAILED: Auto-size behavior not as expected.");
        }

        // Clean up temporary files and directory
        try
        {
            Directory.Delete(tempDir, true);
        }
        catch
        {
            // Ignore cleanup errors (e.g., files in use)
        }
    }
}