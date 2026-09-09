// Title: Generate and Validate QR Code Barcode in a Unit Test
// Description: Demonstrates how to generate a QR Code barcode, save it as PNG, and verify its content by reading it back. Useful for automated test suites.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It showcases the BarcodeGenerator and BarCodeReader classes to create and decode QR Code barcodes, a common task when integrating barcode handling into unit tests or CI pipelines. Developers often need to programmatically produce barcodes, persist them, and validate their correctness without manual inspection.
// Prompt: Generate a QR Code barcode and integrate generation into unit test suite for validation.
// Tags: qr, barcode, generation, recognition, unit-test, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing.Imaging;
using Aspose.Drawing;

/// <summary>
/// Example program that generates a QR Code barcode, saves it to a temporary file,
/// reads it back to verify the encoded text, and cleans up resources.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that performs the generation and validation steps.
    /// </summary>
    static void Main()
    {
        // ------------------------------------------------------------
        // Prepare a unique temporary folder for test artifacts
        // ------------------------------------------------------------
        string tempFolder = Path.Combine(Path.GetTempPath(), "AsposeBarcodeTest_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Define output file path and the text to encode
        string barcodePath = Path.Combine(tempFolder, "qr.png");
        string originalText = "Hello Aspose QR";

        // ------------------------------------------------------------
        // Generate QR Code using BarcodeGenerator
        // ------------------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, originalText))
        {
            // Set module size (pixel dimension) and error correction level
            generator.Parameters.Barcode.XDimension.Pixels = 4f;
            generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelM;

            // Save the generated barcode as a PNG image
            generator.Save(barcodePath, BarCodeImageFormat.Png);
        }

        // ------------------------------------------------------------
        // Verify that the barcode image was created
        // ------------------------------------------------------------
        if (!File.Exists(barcodePath))
        {
            Console.WriteLine("FAILED: Barcode image was not created.");
            return;
        }

        bool testPassed = false;
        BaseDecodeType decodeType = DecodeType.QR;

        // ------------------------------------------------------------
        // Read and decode the QR Code using BarCodeReader
        // ------------------------------------------------------------
        using (var reader = new BarCodeReader(barcodePath, decodeType))
        {
            try
            {
                var results = reader.ReadBarCodes();

                foreach (var result in results)
                {
                    if (result != null && result.CodeText == originalText)
                    {
                        testPassed = true;
                        Console.WriteLine("PASSED: Decoded text matches original.");
                    }
                    else
                    {
                        Console.WriteLine($"FAILED: Decoded text mismatch. Expected '{originalText}', got '{result?.CodeText}'.");
                    }
                }

                if (results.Length == 0)
                {
                    Console.WriteLine("FAILED: No barcode detected.");
                }
            }
            catch (ArgumentException ex) when (ex.Message.Contains("Image loading failed"))
            {
                Console.WriteLine("FAILED: Unable to load barcode image. " + ex.Message);
            }
        }

        // ------------------------------------------------------------
        // Report overall test result
        // ------------------------------------------------------------
        if (!testPassed)
        {
            Console.WriteLine("FAILED: QR Code validation test failed.");
        }

        // ------------------------------------------------------------
        // Cleanup temporary files and directories
        // ------------------------------------------------------------
        try
        {
            if (File.Exists(barcodePath))
                File.Delete(barcodePath);
            if (Directory.Exists(tempFolder))
                Directory.Delete(tempFolder, true);
        }
        catch
        {
            // Ignored - cleanup failure should not affect test result
        }
    }
}