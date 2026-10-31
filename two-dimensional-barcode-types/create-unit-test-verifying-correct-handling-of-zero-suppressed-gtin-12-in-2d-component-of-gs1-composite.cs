// Title: Verify Zero‑Suppressed GTIN‑12 in GS1 Composite 2D Component
// Description: Demonstrates generating a GS1 Composite barcode with a zero‑suppressed GTIN‑12 in the 2D component and validates it by reading the barcode back.
// Category-Description: This example belongs to the Aspose.BarCode GS1 Composite operations collection. It shows how to use BarcodeGenerator with EncodeTypes.GS1CompositeBar, configure linear and 2D components, and employ BarCodeReader with DecodeType.GS1CompositeBar. Developers working with GS1 standards often need to create composite barcodes and verify correct encoding of application identifiers such as zero‑suppressed GTIN‑12.
// Prompt: Create a unit test verifying correct handling of zero‑suppressed GTIN‑12 in the 2D component of GS1 Composite.
// Tags: barcode symbology, gs1 composite, zero-suppressed, gtin-12, unit test, csharp, aspose.barcode, generation, recognition

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Generates a GS1 Composite barcode containing a zero‑suppressed GTIN‑12 in the 2D component,
/// then reads the barcode back to verify that the 2D component matches the expected value.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Performs barcode generation, saves the image, and validates the result.
    /// </summary>
    static void Main()
    {
        // --------------------------------------------------------------------
        // Prepare a temporary folder and file path for the generated barcode image
        // --------------------------------------------------------------------
        string tempFolder = Path.Combine(Path.GetTempPath(), "GS1CompositeTest_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);
        string barcodePath = Path.Combine(tempFolder, "gs1composite.png");

        // --------------------------------------------------------------------
        // Define the linear (UPCA) and 2D (zero‑suppressed GTIN‑12) components
        // --------------------------------------------------------------------
        string linearComponent = "001234567895";               // GTIN‑12 for the UPCA linear part
        string twoDComponent = "(01)00123456789012";           // Zero‑suppressed GTIN‑12 padded to 14 digits
        string codeText = $"{linearComponent}|{twoDComponent}";

        // --------------------------------------------------------------------
        // Generate the GS1 Composite barcode with the specified components
        // --------------------------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.GS1CompositeBar, codeText))
        {
            generator.Parameters.Barcode.XDimension.Pixels = 2;                         // Set module size
            generator.Parameters.Barcode.CodeTextParameters.Location = CodeLocation.None; // Hide human‑readable text
            generator.Parameters.Barcode.GS1CompositeBar.LinearComponentType = EncodeTypes.UPCA;
            generator.Parameters.Barcode.GS1CompositeBar.TwoDComponentType = TwoDComponentType.CC_A;

            generator.Save(barcodePath, BarCodeImageFormat.Png);                         // Save as PNG
        }

        // --------------------------------------------------------------------
        // Verify the barcode by reading it back and checking the 2D component
        // --------------------------------------------------------------------
        bool testPassed = false;

        if (File.Exists(barcodePath))
        {
            using (var reader = new BarCodeReader(barcodePath, DecodeType.GS1CompositeBar))
            {
                foreach (var result in reader.ReadBarCodes())
                {
                    string readTwoD = result.Extended.GS1CompositeBar.TwoDCodeText;
                    if (readTwoD == twoDComponent)
                    {
                        testPassed = true;    // Expected 2D component found
                        break;
                    }
                }
            }
        }
        else
        {
            Console.WriteLine("FAILED: Barcode image was not created.");
            return;
        }

        // --------------------------------------------------------------------
        // Output the test result
        // --------------------------------------------------------------------
        Console.WriteLine(testPassed
            ? "PASS: Zero‑suppressed GTIN‑12 correctly handled in 2D component."
            : "FAIL: 2D component does not match expected zero‑suppressed GTIN‑12.");
    }
}