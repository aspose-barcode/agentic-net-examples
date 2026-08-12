// Title: Generate QR Code and Verify in CI Pipeline
// Description: Demonstrates generating a QR Code barcode, saving it to a temporary folder, and verifying its content using Aspose.BarCode in an automated CI environment.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It showcases the use of BarcodeGenerator to create a QR Code, and BarCodeReader to decode and validate the barcode. Typical use cases include automated testing, CI/CD pipelines, and quality assurance where barcode assets must be produced and verified programmatically. Developers often need to integrate these APIs to ensure barcode correctness without manual intervention.
// Prompt: Generate QR Code barcode and integrate generation into CI pipeline for automated testing.
// Tags: qr code, barcode generation, barcode recognition, ci, automated testing, aspose.barcode, png, .net

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Example program that creates a QR Code, saves it to a temporary location,
/// and validates the encoded text using Aspose.BarCode APIs. Designed for
/// execution in non‑interactive CI environments.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a QR Code, verifies its content,
    /// and cleans up temporary files. No user interaction is required.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for CI execution
        string tempFolder = Path.Combine(Path.GetTempPath(), "AsposeBarcodeCI_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Define the output file path for the generated QR Code image
        string barcodeFile = Path.Combine(tempFolder, "qr.png");

        // Generate QR Code barcode and save it as a PNG image
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.QR))
        {
            generator.CodeText = "https://example.com";
            generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelM; // Medium error correction
            generator.Parameters.Barcode.XDimension.Point = 3f; // Module size (points)
            generator.Save(barcodeFile);
        }

        // Verify that the barcode image file was successfully created
        if (!File.Exists(barcodeFile))
        {
            Console.WriteLine("FAILED: Barcode image was not created.");
            return;
        }

        // Read back the barcode to ensure it encodes the expected text
        using (BarCodeReader reader = new BarCodeReader(barcodeFile, DecodeType.QR))
        {
            bool matchFound = false;

            // Iterate through all detected barcodes (should be only one)
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                Console.WriteLine($"Detected Type: {result.CodeTypeName}, Text: {result.CodeText}");
                if (result.CodeText == "https://example.com")
                {
                    matchFound = true;
                }
            }

            // Report verification outcome
            if (matchFound)
            {
                Console.WriteLine("SUCCESS: QR code generated and verified.");
            }
            else
            {
                Console.WriteLine("FAILED: QR code content does not match expected value.");
            }
        }

        // Clean up temporary files (optional in CI; comment out if inspection is needed)
        try
        {
            File.Delete(barcodeFile);
            Directory.Delete(tempFolder);
        }
        catch
        {
            // Ignored – cleanup failures should not affect CI result
        }
    }
}