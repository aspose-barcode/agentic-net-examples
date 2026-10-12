// Title: GS1 Composite Barcode Linear Component Type Verification
// Description: Demonstrates a simple test that generates GS1 Composite barcodes with different linear component types and verifies that the decoded barcode correctly reports the selected linear symbology.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and recognition category, focusing on GS1 Composite symbology. It showcases the use of BarcodeGenerator, BarCodeReader, and related parameter classes (GS1CompositeBar, TwoDComponentType) to create composite barcodes, adjust linear component settings, and validate decoded results—common tasks for developers implementing inventory or logistics solutions.
// Prompt: Create unit test ensuring changing linear component type updates GS1 Composite barcode structure.
// Tags: gs1 composite, barcode, linear component, unit test, aspose.barcode, csharp

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Program that generates GS1 Composite barcodes with varying linear component types,
/// reads them back, and verifies that the decoded linear component matches the expected type.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the test program.
    /// </summary>
    static void Main()
    {
        // Create a temporary directory to store generated barcode images
        string tempDir = Path.Combine(Path.GetTempPath(), "GS1CompositeTest_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempDir);

        // Define test cases: each case specifies a linear component type and its corresponding code text
        var testCases = new (BaseEncodeType LinearType, string LinearCode)[]
        {
            (EncodeTypes.EAN13, "2001234567893"),
            (EncodeTypes.UPCA, "001234567895")
        };

        int passed = 0;
        int failed = 0;

        // Iterate over each test case, generate the barcode, decode it, and verify the linear component type
        foreach (var (linearType, linearCode) in testCases)
        {
            // Fixed 2D component data for the composite barcode
            string twoDComponent = "(10)ABCD0123(240)0123456789";
            // Full composite code text: linear part, separator, then 2D part
            string fullCodeText = $"{linearCode}|{twoDComponent}";
            // Path for the generated image file
            string filePath = Path.Combine(tempDir, $"barcode_{linearType}.png");

            // Generate the GS1 Composite barcode with the specified linear component type
            using (var generator = new BarcodeGenerator(EncodeTypes.GS1CompositeBar, fullCodeText))
            {
                generator.Parameters.Barcode.XDimension.Pixels = 2f;
                generator.Parameters.Barcode.CodeTextParameters.Location = CodeLocation.None;
                generator.Parameters.Barcode.GS1CompositeBar.TwoDComponentType = TwoDComponentType.CC_A;
                generator.Parameters.Barcode.GS1CompositeBar.LinearComponentType = linearType;
                generator.Save(filePath, BarCodeImageFormat.Png);
            }

            // Decode the generated barcode using the GS1 Composite decode type
            BaseDecodeType decodeType = DecodeType.GS1CompositeBar;
            using (var reader = new BarCodeReader(filePath, decodeType))
            {
                var results = reader.ReadBarCodes();

                // Verify that at least one result was returned
                if (results.Length == 0)
                {
                    Console.WriteLine($"FAILED: No result for {linearType}");
                    failed++;
                    continue;
                }

                var extended = results[0].Extended.GS1CompositeBar;

                // Verify that extended GS1 Composite data is present
                if (extended == null)
                {
                    Console.WriteLine($"FAILED: No extended GS1CompositeBar data for {linearType}");
                    failed++;
                    continue;
                }

                // Compare the detected linear component type with the expected type
                if (extended.OneDType != null && extended.OneDType.ToString() == linearType.ToString())
                {
                    Console.WriteLine($"PASSED: Linear component {linearType} correctly detected.");
                    passed++;
                }
                else
                {
                    Console.WriteLine($"FAILED: Linear component mismatch. Expected {linearType}, got {extended.OneDType}");
                    failed++;
                }
            }
        }

        // Output a summary of test results
        Console.WriteLine($"Test summary: {passed} passed, {failed} failed.");

        // Cleanup temporary files and directory (optional)
        try { Directory.Delete(tempDir, true); } catch { }
    }
}