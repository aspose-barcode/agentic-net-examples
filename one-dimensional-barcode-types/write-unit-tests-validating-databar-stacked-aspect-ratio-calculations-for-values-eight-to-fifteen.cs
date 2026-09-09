// Title: Validate DataBar Stacked Aspect Ratio Settings
// Description: Demonstrates how to set and verify the aspect ratio for Databar Stacked barcodes using Aspose.BarCode.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, focusing on DataBar symbologies. It showcases the use of BarcodeGenerator, EncodeTypes, and the DataBar parameters (AspectRatio) to ensure correct visual rendering. Developers often need to adjust aspect ratios for stacked DataBar barcodes to meet printing or scanning requirements, and this snippet provides a quick validation pattern.
// Prompt: Write unit tests validating DataBar stacked aspect ratio calculations for values eight to fifteen.
// Tags: databar, stacked, aspectratio, barcode, generation, aspose.barcode, unit-test, csharp

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that validates setting the AspectRatio property for Databar Stacked barcodes
/// across the range of values 8 through 15. It generates PNG images and confirms the property
/// value was applied correctly.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates a temporary output folder, iterates over the
    /// desired aspect ratio values, generates a barcode for each, saves it, and checks that
    /// the generator reports the same ratio that was set.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary directory for the generated barcode images.
        string outputDir = Path.Combine(Path.GetTempPath(), "DataBarAspectRatioTests_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputDir);

        int failures = 0;

        // Loop through aspect ratio values from 8 to 15 inclusive.
        for (int ratio = 8; ratio <= 15; ratio++)
        {
            // Initialize the barcode generator for Databar Stacked symbology with a sample GTIN.
            using (var generator = new BarcodeGenerator(EncodeTypes.DatabarStacked, "(01)12345678901231"))
            {
                // Set a fixed X-dimension (module width) in pixels.
                generator.Parameters.Barcode.XDimension.Pixels = 2;

                // Apply the current aspect ratio to the DataBar parameters.
                generator.Parameters.Barcode.DataBar.AspectRatio = ratio;

                // Build the output file path and save the barcode as a PNG image.
                string filePath = Path.Combine(outputDir, $"DatabarStacked_Aspect{ratio}.png");
                generator.Save(filePath, BarCodeImageFormat.Png);

                // Retrieve the aspect ratio that the generator reports after saving.
                float setRatio = generator.Parameters.Barcode.DataBar.AspectRatio;

                // Verify that the retrieved ratio matches the value we set.
                if (Math.Abs(setRatio - ratio) > 0.0001f)
                {
                    Console.WriteLine($"FAIL: AspectRatio set to {ratio}, but retrieved {setRatio}");
                    failures++;
                }
                else
                {
                    Console.WriteLine($"PASS: AspectRatio {ratio} correctly applied.");
                }
            }
        }

        // Output a summary of the test run.
        Console.WriteLine($"Test completed. Total failures: {failures}");
    }
}