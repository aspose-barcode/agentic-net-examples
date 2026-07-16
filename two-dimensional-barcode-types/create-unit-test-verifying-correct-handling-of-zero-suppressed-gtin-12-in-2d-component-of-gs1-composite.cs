// Title: GS1 Composite barcode unit test for zero‑suppressed GTIN‑12
// Description: Demonstrates generating a GS1 Composite barcode containing a zero‑suppressed GTIN‑12 and verifies the encoded data using Aspose.BarCode.
// Category-Description: This example belongs to the Aspose.BarCode GS1 Composite operations collection, illustrating how to use BarcodeGenerator with EncodeTypes.GS1CompositeBar, configure linear and 2D components, and validate the result with BarCodeReader. Developers working with GS1 standards often need to embed GTINs, serial numbers, and other AI data in composite symbols for logistics and retail applications.
// Prompt: Create a unit test verifying correct handling of zero‑suppressed GTIN‑12 in the 2D component of GS1 Composite.
// Tags: gs1 composite, barcode generation, barcode recognition, gtin, zero-suppressed, unit test, csharp

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Generates a GS1 Composite barcode containing a zero‑suppressed GTIN‑12 and validates it.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates the barcode, reads it back, and reports the test result.
    /// </summary>
    static void Main()
    {
        // Prepare GTIN‑12 (12 digits) and pad with two leading zeros to satisfy AI (01) requirement (14 digits)
        string gtin12 = "123456789012";
        string paddedGtin = "00" + gtin12; // 14‑digit GTIN for AI (01)

        // Linear component (GS1‑128) – only the GTIN
        string linearComponent = $"(01){paddedGtin}";

        // 2D component – GTIN plus a serial number (AI (21))
        string twoDComponent = $"(01){paddedGtin}(21)A12345678";

        // GS1 Composite codetext: linear | 2D
        string compositeCodeText = $"{linearComponent}|{twoDComponent}";

        // Output file (in the current directory)
        string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "gs1composite.png");

        // -----------------------------------------------------------------
        // Generate the GS1 Composite barcode
        // -----------------------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.GS1CompositeBar, compositeCodeText))
        {
            // Use GS1‑Code128 for the linear part
            generator.Parameters.Barcode.GS1CompositeBar.LinearComponentType = EncodeTypes.GS1Code128;
            // Use CC‑A (MicroPDF417) for the 2D part
            generator.Parameters.Barcode.GS1CompositeBar.TwoDComponentType = TwoDComponentType.CC_A;

            // Reasonable size settings
            generator.Parameters.Barcode.XDimension.Pixels = 3f;
            generator.Parameters.Barcode.BarHeight.Pixels = 100f;

            // Save the image
            generator.Save(outputPath);
        }

        // -----------------------------------------------------------------
        // Read and verify the barcode
        // -----------------------------------------------------------------
        int passed = 0;
        int failed = 0;

        // Ensure the image was created before attempting to read it
        if (!File.Exists(outputPath))
        {
            Console.WriteLine($"FAILED: Barcode image was not created at '{outputPath}'.");
            return;
        }

        using (var reader = new BarCodeReader(outputPath, DecodeType.GS1CompositeBar))
        {
            foreach (var result in reader.ReadBarCodes())
            {
                // The decoded CodeText should contain the padded GTIN‑12 (14 digits)
                if (!string.IsNullOrEmpty(result.CodeText) && result.CodeText.Contains(paddedGtin))
                {
                    passed++;
                }
                else
                {
                    failed++;
                }
            }
        }

        // -----------------------------------------------------------------
        // Report the test outcome
        // -----------------------------------------------------------------
        if (failed == 0 && passed > 0)
        {
            Console.WriteLine($"PASSED: All {passed} barcode(s) correctly contain the zero‑suppressed GTIN‑12.");
        }
        else
        {
            Console.WriteLine($"FAILED: {failed} barcode(s) did not contain the expected GTIN. Passed: {passed}.");
        }

        // Clean up the generated image (optional)
        try
        {
            File.Delete(outputPath);
        }
        catch
        {
            // Ignored – cleanup failure should not affect test result
        }
    }
}