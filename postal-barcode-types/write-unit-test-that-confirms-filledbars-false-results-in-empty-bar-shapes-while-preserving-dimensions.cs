// Title: Barcode FilledBars Property Effect Unit Test
// Description: Demonstrates a simple unit‑style test that verifies setting FilledBars to false renders empty bar shapes while keeping the image size unchanged.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to configure barcode rendering options such as FilledBars, generate PNG images, and compare bitmap data. Developers working with barcode image customization, visual verification, or automated testing often need to ensure rendering settings affect visual output without altering dimensions.
// Prompt: Write a unit test that confirms FilledBars false results in empty bar shapes while preserving dimensions.
// Tags: barcode, code128, filledbars, image comparison, unit test, aspose.barcode, png, bitmap

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Contains a self‑contained test that validates the visual effect of the <c>FilledBars</c> property
/// on a generated Code128 barcode image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the console application. Executes the test and writes the result to the console.
    /// </summary>
    static void Main()
    {
        // Run the test and output the result.
        bool testPassed = TestFilledBarsEffect();
        Console.WriteLine(testPassed ? "PASSED" : "FAILED");
    }

    // Generates two barcodes: one with default FilledBars (true) and one with FilledBars set to false.
    // Verifies that the image dimensions are identical while the pixel data differs,
    // indicating that bars are rendered empty when FilledBars is false.
    static bool TestFilledBarsEffect()
    {
        // Common barcode settings
        const string codeText = "1234567890";
        const int imageWidth = 300;
        const int imageHeight = 150;

        // Generate barcode with FilledBars = true (default)
        using (var generatorFilled = new BarcodeGenerator(EncodeTypes.Code128, codeText))
        {
            generatorFilled.Parameters.ImageWidth.Point = imageWidth;
            generatorFilled.Parameters.ImageHeight.Point = imageHeight;
            // Ensure default FilledBars (true) – no need to set explicitly
            using (var msFilled = new MemoryStream())
            {
                generatorFilled.Save(msFilled, BarCodeImageFormat.Png);
                msFilled.Position = 0;
                using (var bitmapFilled = new Bitmap(msFilled))
                {
                    // Generate barcode with FilledBars = false
                    using (var generatorEmpty = new BarcodeGenerator(EncodeTypes.Code128, codeText))
                    {
                        generatorEmpty.Parameters.ImageWidth.Point = imageWidth;
                        generatorEmpty.Parameters.ImageHeight.Point = imageHeight;
                        generatorEmpty.Parameters.Barcode.FilledBars = false;
                        using (var msEmpty = new MemoryStream())
                        {
                            generatorEmpty.Save(msEmpty, BarCodeImageFormat.Png);
                            msEmpty.Position = 0;
                            using (var bitmapEmpty = new Bitmap(msEmpty))
                            {
                                // Verify dimensions are the same
                                if (bitmapFilled.Width != bitmapEmpty.Width ||
                                    bitmapFilled.Height != bitmapEmpty.Height)
                                {
                                    Console.WriteLine("Image dimensions differ.");
                                    return false;
                                }

                                // Compare pixel data; expect at least one differing pixel
                                int diffCount = CountDifferentPixels(bitmapFilled, bitmapEmpty);
                                if (diffCount == 0)
                                {
                                    Console.WriteLine("Images are identical; FilledBars may not have effect.");
                                    return false;
                                }

                                // Test succeeded
                                return true;
                            }
                        }
                    }
                }
            }
        }
    }

    // Counts the number of pixels that differ between two bitmaps.
    static int CountDifferentPixels(Bitmap bmp1, Bitmap bmp2)
    {
        int width = bmp1.Width;
        int height = bmp1.Height;
        int diff = 0;

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                if (bmp1.GetPixel(x, y) != bmp2.GetPixel(x, y))
                {
                    diff++;
                }
            }
        }

        return diff;
    }
}