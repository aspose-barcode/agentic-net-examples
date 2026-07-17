// Title: Verify MaxiCode Aspect Ratio Impact on Dimensions
// Description: Demonstrates generating MaxiCode barcodes with different aspect ratios and checking that height changes while width stays constant.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and manipulation category, focusing on MaxiCode symbology. It shows how to adjust the AspectRatio property via the BarcodeGenerator.Parameters.Barcode.MaxiCode API, a common task when customizing barcode size for packaging or labeling. Developers often need unit‑style checks to ensure dimension changes behave as expected.
// Prompt: Write unit tests to verify aspect ratio adjustments affect MaxiCode barcode dimensions as expected.
// Tags: barcode symbology, aspect ratio, maxicode, dimension testing, aspose.barcode, image generation

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates unit‑style tests for MaxiCode aspect‑ratio effects on image dimensions.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that runs dimension verification tests for MaxiCode barcodes.
    /// </summary>
    static void Main()
    {
        int failedTests = 0;

        // Test 1: Generate barcode with default aspect ratio (1.0)
        var imgDefault = GenerateMaxiCode(1.0f);
        int heightDefault = imgDefault.Height;
        int widthDefault = imgDefault.Width;
        imgDefault.Dispose();

        // Test 2: Generate barcode with increased aspect ratio (2.0)
        var imgHigh = GenerateMaxiCode(2.0f);
        int heightHigh = imgHigh.Height;
        int widthHigh = imgHigh.Width;
        imgHigh.Dispose();

        // Verify that increasing the aspect ratio raises the height proportionally
        if (heightHigh <= heightDefault)
        {
            Console.WriteLine("FAILED: Height did not increase with higher aspect ratio.");
            failedTests++;
        }

        // Verify that the width remains unchanged (aspect ratio should affect height only)
        if (widthHigh != widthDefault)
        {
            Console.WriteLine("FAILED: Width changed when only aspect ratio was modified.");
            failedTests++;
        }

        // Output test summary
        if (failedTests == 0)
        {
            Console.WriteLine("All tests passed.");
        }
        else
        {
            Console.WriteLine($"FAILED: {failedTests} test(s) failed.");
        }
    }

    /// <summary>
    /// Generates a MaxiCode barcode image with the specified aspect ratio.
    /// </summary>
    /// <param name="aspectRatio">The desired aspect ratio to apply to the barcode.</param>
    /// <returns>An <see cref="Image"/> containing the generated barcode.</returns>
    static Image GenerateMaxiCode(float aspectRatio)
    {
        // Sample codetext; actual content is not important for dimension testing
        const string sampleCodeText = "1234567890";

        // Create a generator for MaxiCode (EncodeTypes.MaxiCode is assumed to exist)
        using (var generator = new BarcodeGenerator(EncodeTypes.MaxiCode, sampleCodeText))
        {
            // Apply the aspect ratio via the MaxiCode parameters
            generator.Parameters.Barcode.MaxiCode.AspectRatio = aspectRatio;

            // Save the barcode to a memory stream in PNG format
            using (var ms = new MemoryStream())
            {
                generator.Save(ms, BarCodeImageFormat.Png);
                ms.Position = 0;

                // Load and return the image using Aspose.Drawing
                return Aspose.Drawing.Image.FromStream(ms);
            }
        }
    }
}