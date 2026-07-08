// Title: DataBar Stacked Aspect Ratio Validation
// Description: Demonstrates unit-test-like validation of DataBar stacked barcode aspect ratios for values 8‑15, ensuring the property is set correctly and barcode generation succeeds.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, focusing on DataBar stacked symbology. It showcases usage of BarcodeGenerator, EncodeTypes, and DataBar parameters to adjust aspect ratios, a common requirement when customizing barcode size for packaging or labeling applications. Developers often need to verify that aspect ratio settings are applied without errors.
// Prompt: Write unit tests validating DataBar stacked aspect ratio calculations for values eight to fifteen.
// Tags: databar, stacked, aspectratio, png, barcodegenerator, aspose.barcode

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that validates DataBar stacked aspect ratio settings (8‑15) by generating PNG images in memory.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Iterates through aspect ratios, sets the property, and attempts barcode generation.
    /// </summary>
    static void Main()
    {
        int failures = 0;

        // Loop through the required aspect ratio values (8 to 15 inclusive)
        for (int ratio = 8; ratio <= 15; ratio++)
        {
            // Create a generator for DataBar stacked symbology with a valid GTIN payload
            using (var generator = new BarcodeGenerator(EncodeTypes.DatabarStacked, "(01)12345678901231"))
            {
                // Apply the current aspect ratio to the DataBar parameters
                generator.Parameters.Barcode.DataBar.AspectRatio = (float)ratio;

                // Verify that the aspect ratio property was set accurately
                if (Math.Abs(generator.Parameters.Barcode.DataBar.AspectRatio - ratio) > 0.001f)
                {
                    Console.WriteLine($"FAILED: Expected AspectRatio {ratio}, but got {generator.Parameters.Barcode.DataBar.AspectRatio}");
                    failures++;
                    continue;
                }

                // Attempt to generate the barcode and save it to a memory stream (PNG format)
                try
                {
                    using (var ms = new System.IO.MemoryStream())
                    {
                        generator.Save(ms, BarCodeImageFormat.Png);
                    }
                    Console.WriteLine($"PASSED: AspectRatio {ratio}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"FAILED: Exception for AspectRatio {ratio}: {ex.Message}");
                    failures++;
                }
            }
        }

        // Summarize test results
        if (failures == 0)
        {
            Console.WriteLine("All tests passed.");
        }
        else
        {
            Console.WriteLine($"FAILED: {failures} test(s) failed.");
        }
    }
}