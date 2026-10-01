// Title: Read barcode with AllowIncorrectBarcodes and verify Confidence is null
// Description: Generates a Code128 barcode, reads it using the AllowIncorrectBarcodes quality setting, and checks that each BarCodeResult's Confidence property is null.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It demonstrates the use of BarcodeGenerator to create barcodes, BarCodeReader to decode them, and QualitySettings.AllowIncorrectBarcodes to handle potentially corrupted barcodes. Developers often need to read imperfect barcodes and inspect result metadata such as Confidence, making this pattern common in validation and testing scenarios.
// Prompt: Write unit tests verifying that AllowIncorrectBarcodes returns BarCodeResult.Confidence as null.
// Tags: barcode, code128, generation, recognition, allowincorrectbarcodes, confidence, null, unit-test, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating a barcode, reading it with AllowIncorrectBarcodes enabled,
/// and verifying that the Confidence property of each result is null.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates a temporary barcode image, reads it,
    /// and outputs test results based on the Confidence value.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for the test files
        string tempFolder = Path.Combine(Path.GetTempPath(), "BarcodeTest_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Path for the generated barcode image
        string barcodePath = Path.Combine(tempFolder, "sample.png");

        // Generate a simple Code128 barcode and save it as PNG
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "12345"))
        {
            generator.Save(barcodePath, BarCodeImageFormat.Png);
        }

        // Read the barcode with AllowIncorrectBarcodes enabled
        using (var reader = new BarCodeReader(barcodePath, DecodeType.Code128))
        {
            // Enable the setting that allows reading of incorrect barcodes
            reader.QualitySettings.AllowIncorrectBarcodes = true;

            // Perform the read operation
            BarCodeResult[] results = reader.ReadBarCodes();

            // Verify that each result has Confidence == null
            foreach (var result in results)
            {
                // The Confidence property is expected to be nullable.
                // If it is not null, the test fails.
                if (result.Confidence != null)
                {
                    Console.WriteLine("Test FAILED: Confidence is not null (value = " + result.Confidence + ").");
                }
                else
                {
                    Console.WriteLine("Test PASSED: Confidence is null as expected.");
                }
            }
        }

        // Clean up temporary files
        try
        {
            if (File.Exists(barcodePath))
                File.Delete(barcodePath);
            Directory.Delete(tempFolder, true);
        }
        catch
        {
            // Ignored – cleanup failure should not affect test outcome
        }
    }
}