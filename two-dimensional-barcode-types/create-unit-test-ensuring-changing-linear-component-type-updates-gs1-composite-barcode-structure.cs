// Title: Unit test for GS1 Composite barcode linear component type change
// Description: Demonstrates generating a GS1 Composite barcode with different linear component types (EAN13, UPCA), saving to PNG, and verifying the encoded linear type via recognition.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category, showcasing how to create GS1 Composite barcodes using BarcodeGenerator, configure linear and 2‑D components, and validate the barcode structure with BarCodeReader. Typical use cases include automated testing of barcode specifications, ensuring correct symbology settings, and validating barcode data in CI pipelines.
// Prompt: Create unit test ensuring changing linear component type updates GS1 Composite barcode structure.
// Tags: barcode symbology, gs1 composite, unit test, generation, recognition, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Contains a simple console‑based unit‑test that generates GS1 Composite barcodes
/// with different linear component types and verifies the encoded type using the
/// Aspose.BarCode recognition API.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates a temporary folder, runs two tests
    /// (EAN13 and UPCA linear components), outputs the results, and cleans up.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary directory for test artifacts
        string tempDir = Path.Combine(Path.GetTempPath(), "GS1CompositeTest_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempDir);

        // Run tests for each linear component type
        bool test1 = RunTest(EncodeTypes.EAN13, tempDir, "Test1");
        bool test2 = RunTest(EncodeTypes.UPCA, tempDir, "Test2");

        // Output test results
        Console.WriteLine($"Test with EAN13 linear component: {(test1 ? "PASSED" : "FAILED")}");
        Console.WriteLine($"Test with UPCA linear component: {(test2 ? "PASSED" : "FAILED")}");

        // Cleanup temporary files and directory
        try { Directory.Delete(tempDir, true); } catch { }
    }

    /// <summary>
    /// Generates a GS1 Composite barcode with the specified linear component type,
    /// saves it as a PNG file, and verifies that the recognized barcode reports the
    /// same linear component type.
    /// </summary>
    /// <param name="linearType">The linear component symbology to encode (EAN13 or UPCA).</param>
    /// <param name="folder">Folder where the barcode image will be saved.</param>
    /// <param name="testName">Base name for the generated image file.</param>
    /// <returns>True if the recognized linear component type matches the requested type; otherwise false.</returns>
    static bool RunTest(BaseEncodeType linearType, string folder, string testName)
    {
        // Prepare linear and 2‑D parts of the GS1 Composite code text
        string linearPart = linearType == EncodeTypes.EAN13 ? "2001234567893" : "001234567895";
        string twoDPart = "(10)ABCD0123";
        string codeText = $"{linearPart}|{twoDPart}";
        string filePath = Path.Combine(folder, testName + ".png");

        // Generate the GS1 Composite barcode image
        using (var generator = new BarcodeGenerator(EncodeTypes.GS1CompositeBar, codeText))
        {
            generator.Parameters.Barcode.XDimension.Pixels = 2f;                     // Set module size
            generator.Parameters.Barcode.CodeTextParameters.Location = CodeLocation.None; // Hide human‑readable text
            generator.Parameters.Barcode.GS1CompositeBar.TwoDComponentType = TwoDComponentType.CC_A; // Set 2‑D component
            generator.Parameters.Barcode.GS1CompositeBar.LinearComponentType = linearType;          // Set linear component
            generator.Save(filePath, BarCodeImageFormat.Png);                       // Save as PNG
        }

        // Read the generated barcode and verify the linear component type
        using (var reader = new BarCodeReader(filePath, DecodeType.GS1CompositeBar))
        {
            foreach (var result in reader.ReadBarCodes())
            {
                var detectedLinearType = result.Extended.GS1CompositeBar.OneDType;
                return detectedLinearType != null && detectedLinearType.Equals(linearType);
            }
        }

        // If no barcode was read, the test fails
        return false;
    }
}