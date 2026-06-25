using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates generating DataBar Stacked barcodes with varying aspect ratios
/// and validates that the generated image dimensions match the expected ratios.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates barcodes for aspect ratios 8‑15,
    /// checks the resulting image dimensions, and reports pass/fail results.
    /// </summary>
    static void Main()
    {
        int failures = 0;
        const string codeText = "(01)12345678901231"; // valid GTIN for DataBar

        // Iterate over the desired aspect ratios (8 through 15 inclusive)
        for (int ar = 8; ar <= 15; ar++)
        {
            float expectedAspect = ar;

            // Use a memory stream to avoid writing files to disk
            using (var ms = new MemoryStream())
            {
                // Generate a DataBar Stacked barcode with the current aspect ratio
                using (var generator = new BarcodeGenerator(EncodeTypes.DatabarStacked, codeText))
                {
                    generator.Parameters.Barcode.DataBar.AspectRatio = expectedAspect;
                    // Save the barcode image to the memory stream in PNG format
                    generator.Save(ms, BarCodeImageFormat.Png);
                }

                // Reset stream position to the beginning for reading
                ms.Position = 0;

                // Load the generated image from the stream to inspect its dimensions
                using (var bitmap = (Bitmap)Image.FromStream(ms))
                {
                    int width = bitmap.Width;
                    int height = bitmap.Height;
                    float actualAspect = (float)height / width;

                    // Allow a small tolerance due to rounding and padding
                    const float tolerance = 0.2f;
                    if (Math.Abs(actualAspect - expectedAspect) > tolerance)
                    {
                        failures++;
                        Console.WriteLine(
                            $"FAIL: AspectRatio {expectedAspect} -> Actual ratio {actualAspect:F2} (Width={width}, Height={height})");
                    }
                    else
                    {
                        Console.WriteLine(
                            $"PASS: AspectRatio {expectedAspect} matches actual ratio {actualAspect:F2}");
                    }
                }
            }
        }

        // Summarize test results
        if (failures == 0)
        {
            Console.WriteLine("All DataBar stacked aspect ratio tests passed.");
        }
        else
        {
            Console.WriteLine($"FAILED: {failures} test(s) did not meet the expected aspect ratio.");
        }
    }
}