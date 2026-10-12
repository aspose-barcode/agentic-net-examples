// Title: Validate DataBar Stacked Aspect Ratio Height Calculations
// Description: Demonstrates how to generate DataBar Stacked barcodes with varying aspect ratios and verifies that the resulting image heights are positive and increase monotonically.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, focusing on DataBar (GS1 DataBar) symbology. It showcases the use of BarcodeGenerator, EncodeTypes, and the DataBar parameters to control aspect ratio, a common requirement when fine‑tuning barcode size for printing or scanning. Developers often need to validate that aspect‑ratio settings produce expected dimensions, especially for stacked variants used on small items.
// Prompt: Write unit tests validating DataBar stacked aspect ratio calculations for values eight to fifteen.
// Tags: databar, stacked, aspectratio, barcode, generation, aspose.barcode, validation

using System;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Example program that generates DataBar Stacked barcodes for a range of aspect ratios
/// and checks that the generated image heights are positive and increase monotonically.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Iterates over aspect ratios 8‑15, creates a barcode for each,
    /// records the image height, and validates monotonic growth.
    /// </summary>
    static void Main()
    {
        // Define the aspect ratios to test.
        int[] aspectRatios = { 8, 9, 10, 11, 12, 13, 14, 15 };
        // Store the resulting heights for later comparison.
        List<int> heights = new List<int>();
        // Flag indicating whether all checks have passed.
        bool passed = true;

        // Generate a barcode for each aspect ratio and capture its height.
        foreach (int ratio in aspectRatios)
        {
            // Create a generator for the DataBar Stacked symbology with a sample GS1-128 payload.
            using (var generator = new BarcodeGenerator(EncodeTypes.DatabarStacked, "(01)12345678901231"))
            {
                // Set a fixed X‑dimension (module width) in pixels.
                generator.Parameters.Barcode.XDimension.Pixels = 2;
                // Apply the current aspect ratio.
                generator.Parameters.Barcode.DataBar.AspectRatio = ratio;

                // Generate the barcode image.
                using (Bitmap bitmap = generator.GenerateBarCodeImage())
                {
                    // Verify that the generated image has a positive height.
                    if (bitmap.Height <= 0)
                    {
                        Console.WriteLine($"FAILED: Height non-positive for aspect ratio {ratio}");
                        passed = false;
                    }
                    // Record the height for monotonicity check.
                    heights.Add(bitmap.Height);
                }
            }
        }

        // Ensure that each subsequent height is greater than the previous one.
        for (int i = 1; i < heights.Count; i++)
        {
            if (heights[i] <= heights[i - 1])
            {
                Console.WriteLine($"FAILED: Height not increasing from ratio {aspectRatios[i - 1]} to {aspectRatios[i]} (heights {heights[i - 1]} -> {heights[i]})");
                passed = false;
            }
        }

        // Output the overall result.
        if (passed)
        {
            Console.WriteLine("PASSED: All aspect ratio height calculations are monotonic and positive.");
        }
        else
        {
            Console.WriteLine("FAILED: One or more aspect ratio height checks failed.");
        }
    }
}