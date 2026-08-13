// Title: Unit test for zero‑suppressed GTIN‑12 handling in GS1 Composite barcode
// Description: Demonstrates generating a GS1 Composite barcode with a zero‑suppressed GTIN‑12 and verifying that the 2D component is correctly recognized.
// Category-Description: Shows how to work with Aspose.BarCode to create and read GS1 Composite barcodes. The example uses BarcodeGenerator, BarCodeReader, and related parameter classes to configure linear and 2D components, a common task for developers implementing GS1 standards in packaging and logistics.
// Prompt: Create a unit test verifying correct handling of zero‑suppressed GTIN‑12 in the 2D component of GS1 Composite.
// Tags: gs1 composite, barcode generation, barcode recognition, gtin-12, zero-suppressed, csharp, unit test, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Example program that generates a GS1 Composite barcode containing a zero‑suppressed GTIN‑12,
/// then reads back the 2D component to verify correct handling.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates the barcode, reads it, and reports the test result.
    /// </summary>
    static void Main()
    {
        // Prepare a temporary folder for the barcode image
        string tempFolder = Path.Combine(Path.GetTempPath(), "Gs1CompositeTest_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);
        string imagePath = Path.Combine(tempFolder, "gs1composite.png");

        // Zero‑suppressed GTIN‑12 (12 digits) padded to 14 digits for AI (01)
        // Example GTIN‑12: 012345678905 -> padded: 00012345678905
        string gtin12Padded = "00012345678905";
        string linearComponent = $"(01){gtin12Padded}";
        string twoDComponent = $"(01){gtin12Padded}";
        string codeText = $"{linearComponent}|{twoDComponent}";

        // Generate GS1 Composite barcode
        using (var generator = new BarcodeGenerator(EncodeTypes.GS1CompositeBar, codeText))
        {
            // Linear part: GS1 Code128
            generator.Parameters.Barcode.GS1CompositeBar.LinearComponentType = EncodeTypes.GS1Code128;
            // 2D part: CC_A (MicroPDF417 variant)
            generator.Parameters.Barcode.GS1CompositeBar.TwoDComponentType = TwoDComponentType.CC_A;

            // Optional: set module size for better readability
            generator.Parameters.Barcode.XDimension.Pixels = 3f;
            generator.Parameters.Barcode.BarHeight.Pixels = 100f;

            // Save the generated barcode image
            generator.Save(imagePath);
        }

        // Verify that the 2D component is correctly recognized
        bool testPassed = false;
        using (var reader = new BarCodeReader(imagePath, DecodeType.GS1CompositeBar))
        {
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                // The 2D component text is available via the extended parameters
                string decodedTwoD = result.Extended.GS1CompositeBar?.TwoDCodeText;
                if (decodedTwoD != null && decodedTwoD.Equals(twoDComponent, StringComparison.Ordinal))
                {
                    testPassed = true;
                    Console.WriteLine("SUCCESS: 2D component correctly decoded as " + decodedTwoD);
                }
                else
                {
                    Console.WriteLine("FAILURE: Expected 2D component '" + twoDComponent + "', but got '" + decodedTwoD + "'");
                }
            }
        }

        // Output overall test result
        if (!testPassed)
        {
            Console.WriteLine("TEST RESULT: FAILED");
        }
        else
        {
            Console.WriteLine("TEST RESULT: PASSED");
        }

        // Cleanup temporary files (optional)
        try
        {
            if (File.Exists(imagePath))
                File.Delete(imagePath);
            if (Directory.Exists(tempFolder))
                Directory.Delete(tempFolder, true);
        }
        catch
        {
            // Ignored – cleanup is not critical for the test outcome
        }
    }
}