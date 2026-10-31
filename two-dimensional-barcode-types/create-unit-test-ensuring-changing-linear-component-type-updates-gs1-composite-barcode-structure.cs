// Title: GS1 Composite Barcode Linear Component Type Test
// Description: Demonstrates generating a GS1 Composite barcode with different linear component types and verifies that the decoded structure reflects the change.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category, focusing on GS1 Composite symbology. It shows how to configure the LinearComponentType and TwoDComponentType of a GS1CompositeBar, generate the barcode image, and use BarCodeReader to decode and validate the OneDType. Developers working with GS1 Composite barcodes can use this pattern to create unit tests for barcode structure validation.
// Prompt: Create unit test ensuring changing linear component type updates GS1 Composite barcode structure.
// Tags: gs1 composite barcode, linear component type, unit test, generation, recognition, aspnet.barcode, csharp

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Example program that generates GS1 Composite barcodes with varying linear component types,
/// then reads them back to verify that the decoded OneDType matches the expected type.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates temporary files, runs the test cases, and reports the results.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary directory for generated barcode images
        string tempDir = Path.Combine(Path.GetTempPath(), "GS1CompositeTest_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempDir);

        // Define test cases for different linear component types
        var linearTests = new[]
        {
            new { Encode = EncodeTypes.EAN13, LinearCode = "2001234567893", ExpectedDecode = DecodeType.EAN13 },
            new { Encode = EncodeTypes.GS1Code128, LinearCode = "(01)98898765432106", ExpectedDecode = DecodeType.GS1Code128 }
        };

        // Common 2D component data for all tests
        string twoDCode = "(10)ABCD0123";
        bool allPassed = true;

        // Iterate over each test case
        foreach (var test in linearTests)
        {
            // Build a unique file path for the barcode image
            string filePath = Path.Combine(tempDir, $"barcode_{test.Encode.GetType().Name}_{Guid.NewGuid().ToString("N")}.png");
            // Combine linear and 2D components into a single GS1 Composite string
            string fullCode = $"{test.LinearCode}|{twoDCode}";

            // Generate the GS1 Composite barcode image
            using (var generator = new BarcodeGenerator(EncodeTypes.GS1CompositeBar, fullCode))
            {
                // Set the linear component type according to the current test case
                generator.Parameters.Barcode.GS1CompositeBar.LinearComponentType = test.Encode;
                // Use a fixed 2D component type (CC-A) for simplicity
                generator.Parameters.Barcode.GS1CompositeBar.TwoDComponentType = TwoDComponentType.CC_A;
                // Save the generated barcode as a PNG file
                generator.Save(filePath, BarCodeImageFormat.Png);
            }

            // Read and decode the generated barcode
            using (var reader = new BarCodeReader(filePath, DecodeType.GS1CompositeBar))
            {
                var results = reader.ReadBarCodes();
                if (results.Length == 0)
                {
                    Console.WriteLine($"FAILED: No result for {test.Encode}");
                    allPassed = false;
                    continue;
                }

                // Extract the detected OneD type from the decoded result
                var oneDType = results[0].Extended.GS1CompositeBar.OneDType;
                Console.WriteLine($"Generated with LinearComponentType {test.Encode}, Detected OneDType {oneDType}");

                // Verify that the detected type matches the expected decode type
                if (!oneDType.Equals(test.ExpectedDecode))
                {
                    Console.WriteLine($"FAILED: Expected {test.ExpectedDecode} but got {oneDType}");
                    allPassed = false;
                }
            }
        }

        // Output overall test result
        Console.WriteLine(allPassed ? "All tests passed." : "Some tests failed.");
    }
}