// Title: Verify auto‑size of barcode when BarCodeHeight is zero
// Description: Demonstrates that setting BarCodeHeight to zero (by not assigning it) and using AutoSizeMode.Interpolation automatically adjusts the barcode height based on its content.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to control barcode dimensions using the AutoSizeMode property of BarcodeGenerator. It shows default behavior versus interpolation auto‑sizing, a common requirement when developers need dynamic barcode sizing without manually calculating dimensions.
// Prompt: Create unit test verifying barcode with BarCodeHeight zero enables auto‑size based on content, using default units.
// Tags: barcode, code128, autosize, interpolation, generation, unit-test, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that compares default barcode height with height obtained
/// when AutoSizeMode.Interpolation is applied (BarCodeHeight left unset, i.e., zero).
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates two barcodes and validates that interpolation auto‑size
    /// produces a greater image height than the default configuration.
    /// </summary>
    static void Main()
    {
        // Sample barcode text to encode
        const string codeText = "12345678901234567890";

        // --------------------------------------------------------------------
        // Generate barcode using default settings (AutoSizeMode = None)
        // --------------------------------------------------------------------
        int defaultHeight;
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
        {
            // No explicit BarHeight or AutoSizeMode assignment – defaults are used
            using (Bitmap bitmap = generator.GenerateBarCodeImage())
            {
                // Capture the height of the generated image
                defaultHeight = bitmap.Height;

                // Optional: save image to memory stream for visual inspection
                using (var stream = new MemoryStream())
                {
                    bitmap.Save(stream, ImageFormat.Png);
                }
            }
        }

        // --------------------------------------------------------------------
        // Generate barcode with AutoSizeMode.Interpolation (auto‑size based on content)
        // --------------------------------------------------------------------
        int interpolatedHeight;
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
        {
            // Enable interpolation auto‑size; BarHeight remains unset (zero)
            generator.Parameters.AutoSizeMode = AutoSizeMode.Interpolation;

            using (Bitmap bitmap = generator.GenerateBarCodeImage())
            {
                // Capture the height of the interpolated image
                interpolatedHeight = bitmap.Height;

                // Optional: save image to memory stream for visual inspection
                using (var stream = new MemoryStream())
                {
                    bitmap.Save(stream, ImageFormat.Png);
                }
            }
        }

        // --------------------------------------------------------------------
        // Validation: interpolated height should be greater than default height
        // --------------------------------------------------------------------
        if (interpolatedHeight > defaultHeight && interpolatedHeight > 0)
        {
            Console.WriteLine("PASS: AutoSizeMode.Interpolation increased barcode height based on content.");
        }
        else
        {
            Console.WriteLine("FAIL: AutoSizeMode.Interpolation did not adjust barcode height as expected.");
        }
    }
}