// Title: Verify AllowIncorrectBarcodes returns null Confidence in QR barcode reading
// Description: This example generates a QR barcode image, reads it with AllowIncorrectBarcodes enabled, and confirms that the Confidence property of BarCodeResult is null.
// Category-Description: Demonstrates Aspose.BarCode generation and recognition APIs, focusing on quality settings such as AllowIncorrectBarcodes. The example uses BarcodeGenerator, BarCodeReader, and BarCodeResult to show typical use cases where developers need to read potentially malformed barcodes and handle missing confidence values. Ideal for developers searching for barcode validation, confidence handling, and quality configuration in Aspose.BarCode.
// Prompt: Write unit tests verifying that AllowIncorrectBarcodes returns BarCodeResult.Confidence as null.
// Tags: barcode, qr, allowincorrectbarcodes, confidence, generation, recognition, aspose.barcode, csharp

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating a QR barcode, reading it with AllowIncorrectBarcodes enabled,
/// and verifying that the Confidence property of each BarCodeResult is null.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that creates a temporary QR barcode image, runs the AllowIncorrectBarcodes test,
    /// reports the result, and cleans up temporary files.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary directory for test artifacts
        string tempDir = Path.Combine(Path.GetTempPath(), "AsposeBarcodeTest_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempDir);

        // Generate a QR barcode image and save it as PNG
        string qrPath = Path.Combine(tempDir, "qr.png");
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "Test123"))
        {
            generator.Save(qrPath, BarCodeImageFormat.Png);
        }

        // Execute the test that checks Confidence is null when AllowIncorrectBarcodes is true
        bool testPassed = RunAllowIncorrectBarcodesTest(qrPath);

        // Output test result to console
        Console.WriteLine(testPassed
            ? "PASSED: AllowIncorrectBarcodes returns null Confidence."
            : "FAILED: AllowIncorrectBarcodes did not return null Confidence.");

        // Clean up temporary directory and its contents
        try
        {
            Directory.Delete(tempDir, true);
        }
        catch
        {
            // Suppress any cleanup errors
        }
    }

    /// <summary>
    /// Reads the specified QR barcode image with AllowIncorrectBarcodes enabled and verifies that
    /// each BarCodeResult has a null Confidence value.
    /// </summary>
    /// <param name="imagePath">Full path to the QR barcode image.</param>
    /// <returns>True if all results have null Confidence; otherwise, false.</returns>
    static bool RunAllowIncorrectBarcodesTest(string imagePath)
    {
        // Ensure the test image exists before attempting to read it
        if (!File.Exists(imagePath))
        {
            Console.WriteLine("Test image not found.");
            return false;
        }

        // Initialize the barcode reader for QR codes
        using (var reader = new BarCodeReader(imagePath, DecodeType.QR))
        {
            // Enable detection of potentially incorrect barcodes
            reader.QualitySettings.AllowIncorrectBarcodes = true;

            // Read all barcodes from the image
            BarCodeResult[] results = reader.ReadBarCodes();

            // Verify that at least one barcode was detected
            if (results == null || results.Length == 0)
            {
                Console.WriteLine("No barcodes detected.");
                return false;
            }

            // Check each result for a null Confidence value
            foreach (BarCodeResult result in results)
            {
                if (result.Confidence != null)
                {
                    Console.WriteLine($"Barcode '{result.CodeText}' has non-null Confidence: {result.Confidence}");
                    return false;
                }
            }
        }

        // All results passed the null Confidence check
        return true;
    }
}