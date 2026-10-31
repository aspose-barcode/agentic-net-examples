// Title: Demonstrate AllowIncorrectBarcodes effect on BarCodeResult Confidence
// Description: Generates a QR code, reads it with AllowIncorrectBarcodes set to false and true, and checks that the Confidence property is None (null equivalent) in both cases.
// Category-Description: This example belongs to the Aspose.BarCode recognition category, illustrating how QualitySettings.AllowIncorrectBarcodes influences the BarCodeResult.Confidence value. It uses BarcodeGenerator, BarCodeReader, and BarCodeResult classes, common for developers needing to validate barcode confidence when tolerating imperfect scans. Suitable for unit‑test scenarios and CI pipelines.
// Prompt: Write unit tests verifying that AllowIncorrectBarcodes returns BarCodeResult.Confidence as null.
// Tags: barcode, qr, allowincorrectbarcodes, confidence, unit-test, aspose.barcode, generation, recognition

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates how the AllowIncorrectBarcodes setting affects the <see cref="BarCodeResult.Confidence"/> value
/// when reading a generated QR code. The example is useful as a reference for writing unit tests that verify
/// confidence handling in barcode recognition scenarios.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a QR code, reads it twice with different
    /// <c>AllowIncorrectBarcodes</c> settings, and asserts that the confidence reported is <c>None</c>.
    /// </summary>
    static void Main()
    {
        // ----------------------------------------------------------------------
        // Setup: create a temporary folder and define the output file path
        // ----------------------------------------------------------------------
        string tempFolder = Path.Combine(Path.GetTempPath(), "AsposeBarcodeTest_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);
        string barcodePath = Path.Combine(tempFolder, "barcode.png");

        // ----------------------------------------------------------------------
        // Generate a simple QR code (valid barcode) and save it as PNG
        // ----------------------------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "Test"))
        {
            generator.Save(barcodePath, BarCodeImageFormat.Png);
        }

        // ----------------------------------------------------------------------
        // Read the barcode with AllowIncorrectBarcodes = false
        // ----------------------------------------------------------------------
        BarCodeResult[] resultsFalse;
        using (var reader = new BarCodeReader(barcodePath, DecodeType.QR))
        {
            reader.QualitySettings.AllowIncorrectBarcodes = false;
            resultsFalse = reader.ReadBarCodes();
        }

        // ----------------------------------------------------------------------
        // Read the barcode with AllowIncorrectBarcodes = true
        // ----------------------------------------------------------------------
        BarCodeResult[] resultsTrue;
        using (var reader = new BarCodeReader(barcodePath, DecodeType.QR))
        {
            reader.QualitySettings.AllowIncorrectBarcodes = true;
            resultsTrue = reader.ReadBarCodes();
        }

        // ----------------------------------------------------------------------
        // Helper method to assert that the Confidence property is None (i.e., null equivalent)
        // ----------------------------------------------------------------------
        void AssertConfidenceIsNone(BarCodeResult result, string scenario)
        {
            if (result == null)
            {
                Console.WriteLine($"{scenario}: No result returned.");
                return;
            }

            // BarCodeConfidence enum default (None) represents lack of confidence.
            if (result.Confidence == BarCodeConfidence.None)
            {
                Console.WriteLine($"{scenario}: Passed (Confidence is None).");
            }
            else
            {
                Console.WriteLine($"{scenario}: Failed (Confidence = {result.Confidence}).");
            }
        }

        // ----------------------------------------------------------------------
        // Perform assertions for both scenarios
        // ----------------------------------------------------------------------
        Console.WriteLine("Testing with AllowIncorrectBarcodes = false");
        if (resultsFalse.Length > 0)
            AssertConfidenceIsNone(resultsFalse[0], "AllowIncorrectBarcodes false");
        else
            Console.WriteLine("AllowIncorrectBarcodes false: No barcodes detected.");

        Console.WriteLine("Testing with AllowIncorrectBarcodes = true");
        if (resultsTrue.Length > 0)
            AssertConfidenceIsNone(resultsTrue[0], "AllowIncorrectBarcodes true");
        else
            Console.WriteLine("AllowIncorrectBarcodes true: No barcodes detected.");

        // ----------------------------------------------------------------------
        // Cleanup temporary files and folder
        // ----------------------------------------------------------------------
        try
        {
            if (File.Exists(barcodePath))
                File.Delete(barcodePath);
            Directory.Delete(tempFolder, true);
        }
        catch
        {
            // Ignored - cleanup failure should not affect test outcome
        }
    }
}