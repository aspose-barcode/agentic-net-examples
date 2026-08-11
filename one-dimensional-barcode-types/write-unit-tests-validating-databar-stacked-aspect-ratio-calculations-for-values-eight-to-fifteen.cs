// Title: Validate DataBar Stacked Aspect Ratio Calculations
// Description: Demonstrates how to generate GS1‑DataBar stacked barcodes with varying aspect ratios and verify that the rendered image matches the expected height‑to‑width ratio.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, focusing on DataBar stacked symbology. It shows usage of BarcodeGenerator, EncodeTypes, and DataBar parameters to control aspect ratio, a common requirement when integrating barcodes into print layouts or scanning systems. Developers often need to programmatically validate visual dimensions to meet specification tolerances.
// Prompt: Write unit tests validating DataBar stacked aspect ratio calculations for values eight to fifteen.
// Tags: databar, stacked, aspectratio, barcode, generation, unit-test, aspnet, aspose.barcode

using System;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

namespace DataBarStackedAspectRatioTests
{
    /// <summary>
    /// Executes a series of runtime checks that verify the rendered height‑to‑width ratio of
    /// GS1‑DataBar stacked barcodes for aspect ratios 8 through 15.
    /// </summary>
    class Program
    {
        // Simple tolerance for floating‑point comparison (2 %).
        const float Tolerance = 0.02f;

        /// <summary>
        /// Entry point. Generates barcodes with different aspect ratios, measures the resulting image,
        /// and reports any deviations beyond the allowed tolerance.
        /// </summary>
        static void Main()
        {
            // Aspect ratios to validate (inclusive range 8‑15).
            var ratiosToTest = new List<float> { 8f, 9f, 10f, 11f, 12f, 13f, 14f, 15f };
            var failures = new List<string>();

            // Sample valid GS1‑DataBar stacked code text.
            const string codeText = "(01)12345678901231";

            foreach (float expectedRatio in ratiosToTest)
            {
                try
                {
                    // Create a generator for the DataBar stacked symbology.
                    using (var generator = new BarcodeGenerator(EncodeTypes.DatabarStacked, codeText))
                    {
                        // Apply the test aspect ratio (height / width) to the stacked module.
                        generator.Parameters.Barcode.DataBar.AspectRatio = expectedRatio;

                        // Render the barcode to an Aspose.Drawing.Bitmap.
                        using (Bitmap image = generator.GenerateBarCodeImage())
                        {
                            // Guard against zero dimensions which would invalidate the ratio.
                            if (image.Width == 0 || image.Height == 0)
                                throw new InvalidOperationException("Generated image has zero width or height.");

                            // Compute the actual height‑to‑width ratio of the rendered image.
                            float actualRatio = (float)image.Height / image.Width;

                            // Verify the actual ratio is within the allowed tolerance.
                            if (Math.Abs(actualRatio - expectedRatio) > Tolerance)
                            {
                                failures.Add(
                                    $"AspectRatio {expectedRatio}: expected ≈{expectedRatio:F2}, actual {actualRatio:F2}");
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Record any unexpected exceptions for later reporting.
                    failures.Add($"AspectRatio {expectedRatio}: exception – {ex.Message}");
                }
            }

            // Output the overall test result.
            if (failures.Count == 0)
            {
                Console.WriteLine("PASSED: All DataBar stacked aspect ratio tests succeeded.");
            }
            else
            {
                Console.WriteLine($"FAILED: {failures.Count} test(s) failed.");
                foreach (var msg in failures)
                {
                    Console.WriteLine(msg);
                }
            }
        }
    }
}