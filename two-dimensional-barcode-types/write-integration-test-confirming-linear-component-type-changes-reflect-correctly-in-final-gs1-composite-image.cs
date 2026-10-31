// Title: Integration test for GS1 Composite barcode linear component type verification
// Description: Demonstrates generating GS1 Composite barcodes with various linear component types, saving them as PNG images, and verifying that the detected linear type matches the expected one.
// Category-Description: This example belongs to the Aspose.BarCode GS1 Composite operations collection. It showcases the use of BarcodeGenerator to create GS1 Composite barcodes, configuring the TwoDComponentType and LinearComponentType, and employing BarCodeReader to decode and validate the linear component. Developers working with composite symbologies, testing encoding configurations, or automating barcode validation will find these patterns useful.
// Prompt: Write integration test confirming linear component type changes reflect correctly in the final GS1 Composite image.
// Tags: gs1 composite, linear component, png, aspose.barcode, barcode generation, barcode recognition

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Generates GS1 Composite barcodes with different linear component types,
/// saves them as PNG files, and validates that the decoded linear type matches the expected one.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the test application.
    /// </summary>
    static void Main()
    {
        // Create a temporary folder for test images
        string tempFolder = Path.Combine(Path.GetTempPath(), "GS1CompositeTest_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Define test cases: each tuple contains a linear component type and its corresponding code text part
        var testCases = new (BaseEncodeType LinearType, string LinearPart)[]
        {
            (EncodeTypes.EAN8, "12345670"),
            (EncodeTypes.UPCA, "001234567890"),
            (EncodeTypes.EAN13, "2001234567893"),
            (EncodeTypes.UPCE, "04252614"),
            (EncodeTypes.GS1Code128, "(01)98898765432106"),
            (EncodeTypes.DatabarOmniDirectional, "(01)24012345678905"),
            (EncodeTypes.DatabarStackedOmniDirectional, "(01)24012345678905"),
            (EncodeTypes.DatabarStacked, "(01)24012345678905")
        };

        // Iterate over each test case, generate the barcode, and verify the linear component type
        foreach (var (linearType, linearPart) in testCases)
        {
            // Compose the full GS1 Composite code text (linear part + AI for additional data)
            string codeText = $"{linearPart}|(10)ABCD0123";

            // Determine the output file path for the generated image
            string filePath = Path.Combine(tempFolder, $"GS1Composite_{linearType}.png");

            // Generate GS1 Composite barcode with the specified linear component type
            using (var generator = new BarcodeGenerator(EncodeTypes.GS1CompositeBar, codeText))
            {
                generator.Parameters.Barcode.XDimension.Pixels = 2f;
                generator.Parameters.Barcode.CodeTextParameters.Location = CodeLocation.None;
                generator.Parameters.Barcode.GS1CompositeBar.TwoDComponentType = TwoDComponentType.CC_A;
                generator.Parameters.Barcode.GS1CompositeBar.LinearComponentType = linearType;
                generator.Parameters.Barcode.GS1CompositeBar.AllowOnlyGS1Encoding = false;

                // Save the generated barcode as a PNG image
                generator.Save(filePath, BarCodeImageFormat.Png);
            }

            // Read back the barcode image and verify that the detected linear component type matches the expected one
            using (var reader = new BarCodeReader(filePath, DecodeType.GS1CompositeBar))
            {
                bool passed = false;

                foreach (BarCodeResult result in reader.ReadBarCodes())
                {
                    var oneDType = result.Extended.GS1CompositeBar.OneDType;

                    if (oneDType != null && oneDType.ToString() == linearType.ToString())
                    {
                        passed = true;
                    }

                    Console.WriteLine($"File: {Path.GetFileName(filePath)}");
                    Console.WriteLine($"Expected Linear Type: {linearType}");
                    Console.WriteLine($"Detected Linear Type: {oneDType}");
                    Console.WriteLine($"Result: {(passed ? "PASS" : "FAIL")}");
                    Console.WriteLine();
                }

                // If no barcode was detected, indicate failure
                if (!reader.ReadBarCodes().Length.Equals(0) && !passed)
                {
                    Console.WriteLine($"File: {Path.GetFileName(filePath)} - No barcode detected.");
                }
            }
        }

        // Cleanup: optionally delete temporary files
        // Directory.Delete(tempFolder, true);
    }
}