// Title: GS1 Composite Linear Component Type Integration Test
// Description: Demonstrates an integration test that generates GS1 Composite barcodes with various linear component types and verifies that the detected linear type matches the expected one.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category, showcasing how to use BarcodeGenerator and BarCodeReader for GS1 Composite barcodes. It illustrates setting linear component types, saving images, and decoding them to validate encoding. Developers working with GS1 Composite symbology often need to confirm that different linear components (EAN-8, UPC-A, etc.) are correctly embedded and recognized.
// Prompt: Write integration test confirming linear component type changes reflect correctly in the final GS1 Composite image.
// Tags: gs1 composite, linear component, barcode generation, barcode recognition, aspose.barcode, integration test, png

using System;
using System.Collections.Generic;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Program that runs an integration test for GS1 Composite barcodes with different linear component types.
/// </summary>
class Program
{
    /// <summary>
    /// Generates GS1 Composite barcodes for each linear component type, saves them, reads them back, and reports whether the detected linear type matches the expected type.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for test files
        string tempFolder = Path.Combine(Path.GetTempPath(), "GS1CompositeTest_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Define linear component types and corresponding valid code texts
        var testData = new Dictionary<BaseEncodeType, string>
        {
            { EncodeTypes.EAN8, "12345670" },                         // 8 digits
            { EncodeTypes.UPCA, "012345678905" },                     // 12 digits
            { EncodeTypes.EAN13, "1234567890123" },                   // 13 digits
            { EncodeTypes.UPCE, "04252614" },                         // 8 digits (example)
            { EncodeTypes.GS1Code128, "(01)12345678901231" },        // GS1 AI example
            { EncodeTypes.DatabarOmniDirectional, "(01)24012345678905" }, // GS1 AI for DataBar
            { EncodeTypes.DatabarStackedOmniDirectional, "(01)24012345678905" },
            { EncodeTypes.DatabarStacked, "(01)24012345678905" }
        };

        var results = new List<string>();

        // Iterate over each linear component type, generate and verify the barcode
        foreach (var kvp in testData)
        {
            BaseEncodeType linearType = kvp.Key;
            string codeText = kvp.Value;

            string fileName = $"{linearType}.png";
            string filePath = Path.Combine(tempFolder, fileName);

            // Generate GS1 Composite barcode with the specified linear component
            using (var generator = new BarcodeGenerator(EncodeTypes.GS1CompositeBar, codeText + "|(10)ABCD0123"))
            {
                generator.Parameters.Barcode.XDimension.Pixels = 2f;
                generator.Parameters.Barcode.CodeTextParameters.Location = CodeLocation.None;
                generator.Parameters.Barcode.GS1CompositeBar.TwoDComponentType = TwoDComponentType.CC_A;
                generator.Parameters.Barcode.GS1CompositeBar.LinearComponentType = linearType;
                generator.Parameters.Barcode.GS1CompositeBar.AllowOnlyGS1Encoding = false;

                generator.Save(filePath, BarCodeImageFormat.Png);
            }

            // Read the generated barcode and verify the linear component type
            using (var reader = new BarCodeReader(filePath, DecodeType.GS1CompositeBar))
            {
                BarCodeResult[] readResults = reader.ReadBarCodes();
                if (readResults.Length == 0)
                {
                    results.Add($"Linear component {linearType}: No barcode detected (FAIL)");
                    continue;
                }

                BarCodeResult result = readResults[0];
                BaseDecodeType detectedLinearType = result.Extended.GS1CompositeBar.OneDType;

                string status = detectedLinearType.ToString() == linearType.ToString() ? "PASS" : "FAIL";
                results.Add($"Linear component {linearType}: Expected {linearType}, Detected {detectedLinearType} - {status}");
            }
        }

        // Output test results
        Console.WriteLine("GS1 Composite Linear Component Type Test Results:");
        foreach (string line in results)
        {
            Console.WriteLine(line);
        }

        // Cleanup: optionally delete temporary files (commented out to allow inspection)
        // Directory.Delete(tempFolder, true);
    }
}