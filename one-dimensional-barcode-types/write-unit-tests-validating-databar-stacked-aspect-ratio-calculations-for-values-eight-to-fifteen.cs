// Title: DataBar Stacked Aspect Ratio Height Validation
// Description: Generates DataBar stacked barcodes for aspect ratios 8‑15 and verifies that the resulting image height grows with the aspect ratio.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, focusing on the DataBar stacked symbology. It demonstrates using BarcodeGenerator, setting XDimension and DataBar.AspectRatio, and rendering the barcode to a Bitmap. Developers often need to adjust aspect ratios to meet size requirements for packaging or retail labeling, and this pattern shows how to programmatically test those settings.
// Prompt: Write unit tests validating DataBar stacked aspect ratio calculations for values eight to fifteen.
// Tags: barcode, databar, stacked, aspectratio, generation, aspose.barcode, testing

using System;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating DataBar stacked barcodes with varying aspect ratios
/// and validates that image height increases as the aspect ratio grows.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that creates barcodes for aspect ratios 8‑15, records image heights,
    /// and verifies monotonic increase.
    /// </summary>
    static void Main()
    {
        // Define the aspect ratios to test.
        List<int> aspectRatios = new List<int> { 8, 9, 10, 11, 12, 13, 14, 15 };

        // Dictionary to store the resulting image heights keyed by aspect ratio.
        Dictionary<int, int> heights = new Dictionary<int, int>();

        // Iterate over each aspect ratio, generate a barcode, and capture its height.
        foreach (int aspect in aspectRatios)
        {
            // Create a generator for the DataBar stacked symbology with a sample GTIN.
            using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.DatabarStacked, "(01)12345678901231"))
            {
                // Set the module size (X dimension) in pixels.
                generator.Parameters.Barcode.XDimension.Pixels = 2;

                // Apply the current aspect ratio to the DataBar settings.
                generator.Parameters.Barcode.DataBar.AspectRatio = aspect;

                // Generate the barcode image.
                using (Bitmap bitmap = generator.GenerateBarCodeImage())
                {
                    // Record the image height for later verification.
                    int height = bitmap.Height;
                    heights[aspect] = height;
                    Console.WriteLine($"Aspect Ratio {aspect}: Image Height = {height}");
                }
            }
        }

        // Verify that the recorded heights increase monotonically with the aspect ratio.
        bool allIncreasing = true;
        int previousHeight = -1;

        foreach (int aspect in aspectRatios)
        {
            int currentHeight = heights[aspect];

            // If the current height is not greater than the previous, flag a failure.
            if (previousHeight != -1 && currentHeight <= previousHeight)
            {
                allIncreasing = false;
                Console.WriteLine($"FAILED: Height for aspect {aspect} ({currentHeight}) is not greater than previous height ({previousHeight}).");
            }

            previousHeight = currentHeight;
        }

        // Output the overall test result.
        if (allIncreasing)
        {
            Console.WriteLine("ALL TESTS PASSED: Image heights increase with aspect ratio from 8 to 15.");
        }
        else
        {
            Console.WriteLine("ONE OR MORE TESTS FAILED: Height monotonicity check failed.");
        }
    }
}