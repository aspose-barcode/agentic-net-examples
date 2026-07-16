// Title: GS1 Composite Barcode Linear Component Type Unit Test
// Description: Demonstrates a unit‑test‑style verification that changing the linear component type of a GS1 Composite barcode updates the decoded structure accordingly.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and recognition category, focusing on GS1 Composite symbology. It shows how to configure the linear component type via BarcodeGenerator, generate a composite barcode, and validate the result using BarCodeReader. Developers working with GS1 Composite barcodes often need to ensure that encoding settings are correctly reflected during decoding, making this pattern useful for automated testing and CI pipelines.
// Prompt: Create unit test ensuring changing linear component type updates GS1 Composite barcode structure.
// Tags: barcode symbology, gs1 composite, linear component type, generation, recognition, unit test, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Example program that verifies the linear component type of a GS1 Composite barcode
/// is correctly encoded and decoded. It iterates over a set of test cases, generates
/// a barcode for each, reads it back, and reports pass/fail results.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Executes the test cases and prints a summary.
    /// </summary>
    static void Main()
    {
        // Define test cases: each case sets a linear component type and expects the same type on read.
        var testCases = new (BaseEncodeType LinearType, BaseDecodeType ExpectedDecode)[]
        {
            (EncodeTypes.GS1Code128, DecodeType.GS1Code128),
            (EncodeTypes.UPCA, DecodeType.UPCA)
        };

        int passed = 0;
        int failed = 0;

        // Iterate through each test case.
        foreach (var (linearType, expectedDecode) in testCases)
        {
            // GS1 Composite codetext: linear part (GTIN) and a simple 2D part.
            string linearComponent = "(01)01234567890123"; // 14‑digit GTIN
            string twoDComponent = "(21)ABC123";
            string codeText = $"{linearComponent}|{twoDComponent}";

            // Generate barcode with the specified linear component type.
            using (var generator = new BarcodeGenerator(EncodeTypes.GS1CompositeBar, codeText))
            {
                // Set the linear component type for the composite barcode.
                generator.Parameters.Barcode.GS1CompositeBar.LinearComponentType = linearType;
                // Use a fixed 2D component type (CC-A) for simplicity.
                generator.Parameters.Barcode.GS1CompositeBar.TwoDComponentType = TwoDComponentType.CC_A;

                // Save the generated barcode to a memory stream.
                using (var ms = new MemoryStream())
                {
                    generator.Save(ms, BarCodeImageFormat.Png);
                    ms.Position = 0; // Reset stream position for reading.

                    // Read the barcode and verify the linear component type.
                    using (var reader = new BarCodeReader(ms, DecodeType.GS1CompositeBar))
                    {
                        var results = reader.ReadBarCodes();

                        // No barcode detected – mark as failed.
                        if (results.Length == 0)
                        {
                            Console.WriteLine($"FAILED: No barcode detected for linear type {linearType.TypeName}.");
                            failed++;
                            continue;
                        }

                        // Extract the decoded result and its extended information.
                        var result = results[0];
                        var extended = result.Extended;
                        var oneDType = extended.GS1CompositeBar.OneDType; // BaseDecodeType

                        // Compare the decoded linear type with the expected value.
                        if (oneDType == expectedDecode)
                        {
                            Console.WriteLine($"PASSED: Linear type {linearType.TypeName} correctly recognized as {oneDType}.");
                            passed++;
                        }
                        else
                        {
                            Console.WriteLine($"FAILED: Linear type {linearType.TypeName} recognized as {oneDType}, expected {expectedDecode}.");
                            failed++;
                        }
                    }
                }
            }
        }

        // Output a summary of the test results.
        Console.WriteLine($"Test summary: {passed} passed, {failed} failed.");
    }
}