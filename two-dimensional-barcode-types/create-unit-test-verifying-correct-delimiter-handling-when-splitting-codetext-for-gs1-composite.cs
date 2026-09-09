// Title: GS1 Composite Barcode Delimiter Handling Test
// Description: This example generates a GS1 Composite barcode, splits the linear and 2‑dimensional parts using the '|' delimiter, and verifies that the reader correctly returns each component.
// Category-Description: Demonstrates Aspose.BarCode generation and recognition of GS1 Composite barcodes. It covers the use of BarcodeGenerator with EncodeTypes.GS1CompositeBar, setting linear and 2D component types, and reading the barcode via BarCodeReader to validate delimiter handling. Ideal for developers needing to test GS1 Composite encoding, component extraction, and unit‑test scenarios.
// Prompt: Create unit test verifying correct delimiter handling when splitting CodeText for GS1 Composite.
// Tags: barcode, gs1, composite, generation, recognition, unit-test, csharp, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates generation and verification of a GS1 Composite barcode,
/// focusing on correct handling of the delimiter that separates the linear
/// and 2‑dimensional components of the CodeText.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a barcode, reads it back,
    /// and validates that the linear and 2‑D parts are correctly split.
    /// </summary>
    static void Main()
    {
        // Prepare test data: linear and 2‑D components with a delimiter.
        string linearPart = "(01)12345678901234";
        string twoDPart = "(21)ABC123";
        string combinedCodeText = $"{linearPart}|{twoDPart}";

        // Create a unique temporary folder for the barcode image.
        string tempFolder = Path.Combine(Path.GetTempPath(), "Gs1CompositeTest_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);
        string barcodePath = Path.Combine(tempFolder, "gs1composite.png");

        // Generate the GS1 Composite barcode using the combined CodeText.
        using (var generator = new BarcodeGenerator(EncodeTypes.GS1CompositeBar, combinedCodeText))
        {
            generator.Parameters.Barcode.XDimension.Pixels = 2f;
            generator.Parameters.Barcode.GS1CompositeBar.LinearComponentType = EncodeTypes.GS1Code128;
            generator.Parameters.Barcode.GS1CompositeBar.TwoDComponentType = TwoDComponentType.CC_C;
            generator.Parameters.Barcode.GS1CompositeBar.AllowOnlyGS1Encoding = false;
            generator.Save(barcodePath, BarCodeImageFormat.Png);
        }

        // Verify that the barcode image file was created successfully.
        if (!File.Exists(barcodePath))
        {
            Console.WriteLine("FAILED: Barcode image was not created.");
            return;
        }

        // Read the barcode and check that the delimiter split is handled correctly.
        bool testPassed = false;
        using (var reader = new BarCodeReader(barcodePath, DecodeType.GS1CompositeBar))
        {
            foreach (var result in reader.ReadBarCodes())
            {
                string readLinear = result.Extended.GS1CompositeBar.OneDCodeText;
                string readTwoD = result.Extended.GS1CompositeBar.TwoDCodeText;

                if (readLinear == linearPart && readTwoD == twoDPart)
                {
                    testPassed = true;
                }
                else
                {
                    Console.WriteLine($"DEBUG: Expected Linear='{linearPart}', Got='{readLinear}'");
                    Console.WriteLine($"DEBUG: Expected 2D='{twoDPart}', Got='{readTwoD}'");
                }
            }
        }

        // Output the test result.
        if (testPassed)
        {
            Console.WriteLine("PASSED: GS1 Composite delimiter handling verified.");
        }
        else
        {
            Console.WriteLine("FAILED: GS1 Composite delimiter handling verification failed.");
        }

        // Cleanup temporary files and directories.
        try
        {
            if (File.Exists(barcodePath))
                File.Delete(barcodePath);
            if (Directory.Exists(tempFolder))
                Directory.Delete(tempFolder, true);
        }
        catch
        {
            // Ignored – cleanup failures do not affect test outcome.
        }
    }
}