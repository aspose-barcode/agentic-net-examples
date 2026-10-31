// Title: Demonstrate FilledBars Property Effect on Barcode Rendering
// Description: Shows how setting the FilledBars property to false creates barcodes with empty (hollow) bars while keeping the overall image dimensions unchanged.
// Category-Description: This example belongs to the Aspose.BarCode image generation category, illustrating the use of BarcodeGenerator, BarcodeParameters, and the FilledBars setting. Developers often need to toggle filled versus empty bars for visual styling or printing requirements, and this snippet demonstrates the typical workflow for generating PNG images and comparing results.
// Prompt: Write a unit test that confirms FilledBars false results in empty bar shapes while preserving dimensions.
// Tags: barcode, code128, filledbars, emptybars, image-comparison, aspnet, aspose.barcode, png, unit-test

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates the effect of the FilledBars property on barcode rendering.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that generates two barcodes (filled and empty bars), compares their dimensions and pixel data, and reports the result.
    /// </summary>
    static void Main()
    {
        // Generate barcode with filled bars (default behavior)
        Bitmap filledBitmap;
        using (var generatorFilled = new BarcodeGenerator(EncodeTypes.Code128, "TEST123"))
        {
            // Explicitly set FilledBars to true (default)
            generatorFilled.Parameters.Barcode.FilledBars = true;
            using (var msFilled = new MemoryStream())
            {
                // Save the barcode image to a memory stream in PNG format
                generatorFilled.Save(msFilled, BarCodeImageFormat.Png);
                msFilled.Position = 0;
                using (var bmp = new Bitmap(msFilled))
                {
                    // Clone the bitmap to keep it after the stream is disposed
                    filledBitmap = new Bitmap(bmp);
                }
            }
        }

        // Generate barcode with empty (hollow) bars
        Bitmap emptyBitmap;
        using (var generatorEmpty = new BarcodeGenerator(EncodeTypes.Code128, "TEST123"))
        {
            // Disable filled bars to produce empty bar shapes
            generatorEmpty.Parameters.Barcode.FilledBars = false;
            using (var msEmpty = new MemoryStream())
            {
                // Save the barcode image to a memory stream in PNG format
                generatorEmpty.Save(msEmpty, BarCodeImageFormat.Png);
                msEmpty.Position = 0;
                using (var bmp = new Bitmap(msEmpty))
                {
                    // Clone the bitmap to keep it after the stream is disposed
                    emptyBitmap = new Bitmap(bmp);
                }
            }
        }

        // Verify that both images have the same dimensions
        bool dimensionsEqual = filledBitmap.Width == emptyBitmap.Width && filledBitmap.Height == emptyBitmap.Height;
        // Verify that the pixel data differs (filled vs. empty bars)
        bool imagesDiffer = !BitmapsAreEqual(filledBitmap, emptyBitmap);

        // Output test result
        if (dimensionsEqual && imagesDiffer)
        {
            Console.WriteLine("PASS: FilledBars false produces empty bars while preserving dimensions.");
        }
        else
        {
            Console.WriteLine("FAIL: Test conditions not met.");
            Console.WriteLine($"Dimensions equal: {dimensionsEqual}");
            Console.WriteLine($"Images differ: {imagesDiffer}");
        }

        // Release bitmap resources
        filledBitmap.Dispose();
        emptyBitmap.Dispose();
    }

    /// <summary>
    /// Compares two bitmaps pixel by pixel.
    /// </summary>
    /// <param name="bmp1">First bitmap.</param>
    /// <param name="bmp2">Second bitmap.</param>
    /// <returns>True if the bitmaps are identical; otherwise, false.</returns>
    static bool BitmapsAreEqual(Bitmap bmp1, Bitmap bmp2)
    {
        if (bmp1.Width != bmp2.Width || bmp1.Height != bmp2.Height)
            return false;

        for (int y = 0; y < bmp1.Height; y++)
        {
            for (int x = 0; x < bmp1.Width; x++)
            {
                if (bmp1.GetPixel(x, y) != bmp2.GetPixel(x, y))
                    return false;
            }
        }
        return true;
    }
}