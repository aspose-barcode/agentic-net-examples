// Title: Demonstrate AllowIncorrectBarcodes effect on barcode confidence
// Description: Generates an EAN13 barcode with an incorrect checksum and shows how the AllowIncorrectBarcodes setting influences the Confidence property of BarCodeResult.
// Prompt: Write unit tests verifying that AllowIncorrectBarcodes returns BarCodeResult.Confidence as null.
// Tags: barcode symbology, ean13, allowincorrectbarcodes, confidence, unit testing, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Example program that creates an EAN13 barcode with an invalid checksum
/// and demonstrates the impact of the <c>AllowIncorrectBarcodes</c> quality setting
/// on the <c>BarCodeResult.Confidence</c> value returned by the reader.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a temporary barcode image,
    /// runs two recognition scenarios (allowing and disallowing incorrect barcodes),
    /// and outputs the results to the console.
    /// </summary>
    static void Main()
    {
        // Prepare a temporary PNG file for the barcode image
        string tempPath = Path.Combine(Path.GetTempPath(), "temp_ean13.png");

        // Create an EAN13 barcode with an incorrect checksum (last digit is wrong)
        using (var generator = new BarcodeGenerator(EncodeTypes.EAN13, "1234567890123"))
        {
            generator.Save(tempPath);
        }

        // Verify that the file was created
        if (!File.Exists(tempPath))
        {
            Console.WriteLine("Failed to create barcode image.");
            return;
        }

        // ------------------------------------------------------------
        // Test 1: AllowIncorrectBarcodes = true
        // ------------------------------------------------------------
        using (var reader = new BarCodeReader(tempPath, DecodeType.EAN13))
        {
            // Enable recognition of incorrect barcodes
            reader.QualitySettings.AllowIncorrectBarcodes = true;

            bool confidenceIsNone = false;
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                // When AllowIncorrectBarcodes is true, the engine should return a result
                // with Confidence set to BarCodeConfidence.None (value 0)
                confidenceIsNone = result.Confidence == BarCodeConfidence.None;
                Console.WriteLine($"Test1 - Confidence: {result.Confidence}");
            }

            Console.WriteLine(confidenceIsNone
                ? "Test1 passed: Confidence is None as expected."
                : "Test1 failed: Confidence is not None.");
        }

        // ------------------------------------------------------------
        // Test 2: AllowIncorrectBarcodes = false
        // ------------------------------------------------------------
        using (var reader = new BarCodeReader(tempPath, DecodeType.EAN13))
        {
            // Disable recognition of incorrect barcodes (default behavior)
            reader.QualitySettings.AllowIncorrectBarcodes = false;

            bool noResult = true;
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                // With AllowIncorrectBarcodes disabled, the reader should not return any result
                noResult = false;
                Console.WriteLine($"Test2 - Unexpected result with Confidence: {result.Confidence}");
            }

            Console.WriteLine(noResult
                ? "Test2 passed: No result returned as expected."
                : "Test2 failed: Result was returned despite AllowIncorrectBarcodes being false.");
        }

        // Clean up temporary file
        try
        {
            File.Delete(tempPath);
        }
        catch
        {
            // Ignore any cleanup errors
        }
    }
}