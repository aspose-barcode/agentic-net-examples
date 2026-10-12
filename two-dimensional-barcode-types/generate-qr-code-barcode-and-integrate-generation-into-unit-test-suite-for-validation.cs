// Title: Generate QR Code and Validate via BarCodeReader in a Unit Test
// Description: This example creates a QR Code image using Aspose.BarCode, saves it to a temporary file, then reads it back to verify the encoded text matches the original.
// Category-Description: Demonstrates Aspose.BarCode barcode generation and recognition workflows, focusing on QR Code creation with BarcodeGenerator and validation using BarCodeReader. Ideal for developers writing automated tests that need to ensure barcode data integrity. Covers typical use cases such as unit testing, CI pipelines, and temporary file handling, highlighting key API classes like BarcodeGenerator, BarCodeReader, and related parameter settings.
// Prompt: Generate a QR Code barcode and integrate generation into unit test suite for validation.
// Tags: qr code, barcode generation, barcode recognition, unit testing, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates QR Code generation and validation using Aspose.BarCode within a self‑contained test scenario.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that creates a QR Code, saves it, reads it back, and reports validation results.
    /// </summary>
    static void Main()
    {
        // --------------------------------------------------------------------
        // Prepare a unique temporary directory and file path for the barcode image
        // --------------------------------------------------------------------
        string tempDir = Path.Combine(Path.GetTempPath(), "AsposeBarcodeQrTest_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempDir);
        string barcodePath = Path.Combine(tempDir, "qr.png");

        // Text to encode into the QR Code
        string originalText = "Aspose QR Code Test";

        // --------------------------------------------------------------------
        // Generate QR Code using BarcodeGenerator
        // --------------------------------------------------------------------
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.QR, originalText))
        {
            // Set module size (pixel dimension) and error correction level
            generator.Parameters.Barcode.XDimension.Pixels = 4f;
            generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelM;

            // Save the generated QR Code as a PNG image
            generator.Save(barcodePath, BarCodeImageFormat.Png);
        }

        // --------------------------------------------------------------------
        // Validate the generated QR Code by reading it back with BarCodeReader
        // --------------------------------------------------------------------
        bool testPassed = false;

        if (File.Exists(barcodePath))
        {
            // Use a decoder that supports all barcode types
            BaseDecodeType decodeType = DecodeType.AllSupportedTypes;

            using (BarCodeReader reader = new BarCodeReader(barcodePath, decodeType))
            {
                var results = reader.ReadBarCodes();

                foreach (var result in results)
                {
                    if (result.CodeText == originalText)
                    {
                        testPassed = true;
                        Console.WriteLine("Test Passed: Decoded text matches original.");
                    }
                    else
                    {
                        Console.WriteLine($"Test Failed: Decoded text '{result.CodeText}' does not match original.");
                    }
                }
            }
        }
        else
        {
            Console.WriteLine("Test Failed: Barcode image file was not created.");
        }

        // Report overall test outcome if no matching result was found
        if (!testPassed)
        {
            Console.WriteLine("QR Code generation/validation test completed with failures.");
        }

        // --------------------------------------------------------------------
        // Cleanup temporary files and directories
        // --------------------------------------------------------------------
        try
        {
            if (File.Exists(barcodePath))
                File.Delete(barcodePath);

            if (Directory.Exists(tempDir))
                Directory.Delete(tempDir);
        }
        catch
        {
            // Ignored – cleanup failures should not affect the test result
        }
    }
}