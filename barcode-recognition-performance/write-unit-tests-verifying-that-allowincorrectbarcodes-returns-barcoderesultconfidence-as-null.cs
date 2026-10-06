// Title: Demonstrate AllowIncorrectBarcodes handling with Aspose.BarCode
// Description: Shows how to generate a Code128 barcode, read it with AllowIncorrectBarcodes enabled, and verify that the Confidence property is null or None.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It illustrates the use of BarcodeGenerator for creating barcodes and BarCodeReader for decoding them, focusing on the QualitySettings.AllowIncorrectBarcodes property. Developers often need to handle imperfect scans; this snippet demonstrates checking BarCodeResult.Confidence when incorrect barcodes are permitted, a common scenario in automated testing and batch processing.
// Prompt: Write unit tests verifying that AllowIncorrectBarcodes returns BarCodeResult.Confidence as null.
// Tags: barcode, code128, allowincorrectbarcodes, confidence, aspose.barcode, generation, recognition, unit-test

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Example program demonstrating the effect of AllowIncorrectBarcodes on confidence values when reading barcodes with Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates a barcode, runs verification, and cleans up temporary files.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for test files
        string tempFolder = Path.Combine(Path.GetTempPath(), "BarCodeTest_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Define the full path for the generated barcode image
        string barcodePath = Path.Combine(tempFolder, "code128.png");

        // Generate a simple Code128 barcode and save it as PNG
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128, "123456"))
        {
            generator.Save(barcodePath, BarCodeImageFormat.Png);
        }

        // Run the verification test that checks AllowIncorrectBarcodes behavior
        VerifyAllowIncorrectBarcodes(barcodePath);

        // Clean up temporary files and folder
        try
        {
            if (File.Exists(barcodePath))
                File.Delete(barcodePath);
            Directory.Delete(tempFolder, true);
        }
        catch
        {
            // Ignored - cleanup failure should not affect test result
        }
    }

    static void VerifyAllowIncorrectBarcodes(string imagePath)
    {
        bool testPassed = true;

        // Initialize a reader for the generated barcode with AllowIncorrectBarcodes set to true
        using (BarCodeReader reader = new BarCodeReader(imagePath, DecodeType.Code128))
        {
            reader.QualitySettings.AllowIncorrectBarcodes = true;
            BarCodeResult[] results = reader.ReadBarCodes();

            // Iterate through each result and verify Confidence is null or None
            foreach (BarCodeResult result in results)
            {
                bool isNull = result.Confidence == null;
                bool isNone = false;
                try
                {
                    isNone = result.Confidence == BarCodeConfidence.None;
                }
                catch { }

                if (!isNull && !isNone)
                {
                    testPassed = false;
                    Console.WriteLine($"[Fail] Expected Confidence null or None, got {result.Confidence}");
                }
            }
        }

        // Output the overall test result
        Console.WriteLine(testPassed ? "AllowIncorrectBarcodes test passed" : "AllowIncorrectBarcodes test failed");
    }
}