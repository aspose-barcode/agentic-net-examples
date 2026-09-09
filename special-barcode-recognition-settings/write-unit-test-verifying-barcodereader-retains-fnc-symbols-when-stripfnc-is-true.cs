// Title: Unit test for retaining FNC symbols with StripFNC enabled
// Description: Demonstrates how to generate a Code128 barcode containing FNC characters, read it with StripFNC set to true, and verify that the FNC symbols are preserved in the decoded text.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and recognition category. It shows usage of BarcodeGenerator for creating barcodes, BarCodeReader for decoding, and the StripFNC setting to control handling of function characters. Developers working with Code128 or other symbologies that embed FNC symbols can use this pattern to ensure proper data extraction in unit tests or validation scenarios.
// Prompt: Write a unit test verifying BarCodeReader retains FNC symbols when StripFNC is true.
// Tags: code128, fnc, stripfnc, barcode, generation, recognition, unit-test, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Example program that generates a Code128 barcode with embedded FNC characters,
/// reads it back with StripFNC enabled, and validates that the FNC symbols are retained.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Executes the barcode generation, reading, and verification steps.
    /// </summary>
    static void Main()
    {
        // ------------------------------------------------------------
        // Prepare a temporary folder to store the generated barcode image
        // ------------------------------------------------------------
        string tempFolder = Path.Combine(Path.GetTempPath(), "FncTest_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);
        string imagePath = Path.Combine(tempFolder, "code128fnc.png");

        // ------------------------------------------------------------
        // Define FNC characters for Code128 (FNC1, FNC2, FNC3)
        // ------------------------------------------------------------
        char fnc1 = (char)200; // FNC1
        char fnc2 = (char)201; // FNC2
        char fnc3 = (char)202; // FNC3
        string codeText = "Aspose" + fnc1 + fnc2 + fnc3;

        // ------------------------------------------------------------
        // Generate the barcode image using BarcodeGenerator
        // ------------------------------------------------------------
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
        {
            generator.Parameters.Barcode.XDimension.Pixels = 2f;
            generator.Save(imagePath, BarCodeImageFormat.Png);
        }

        // ------------------------------------------------------------
        // Verify that setting StripFNC = true retains FNC symbols in the decoded text
        // ------------------------------------------------------------
        bool testPassed = false;
        using (BarCodeReader reader = new BarCodeReader(imagePath, DecodeType.Code128))
        {
            // Enable stripping of FNC characters while preserving their placeholders
            reader.BarcodeSettings.StripFNC = true;

            // Read all barcodes from the image
            BarCodeResult[] results = reader.ReadBarCodes();

            if (results.Length > 0)
            {
                string readText = results[0].CodeText;

                // Check for the presence of FNC placeholders in the decoded string
                if (readText.Contains("<FNC1>") && readText.Contains("<FNC2>") && readText.Contains("<FNC3>"))
                {
                    testPassed = true;
                }
            }
        }

        // ------------------------------------------------------------
        // Output the test result
        // ------------------------------------------------------------
        Console.WriteLine(testPassed ? "PASSED: FNC symbols retained when StripFNC is true." : "FAILED: FNC symbols were not retained.");
    }
}