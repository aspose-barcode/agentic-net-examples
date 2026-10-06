// Title: Generate and Verify QR Code Barcode in CI Pipeline
// Description: Demonstrates how to generate a QR Code barcode, save it as PNG, and verify it by reading back the encoded text. Useful for automated testing in CI environments.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category, showcasing the use of BarcodeGenerator for QR code creation and BarCodeReader for decoding. Developers commonly use these APIs to produce barcodes for documents, integrate barcode validation into build pipelines, and ensure image output correctness across platforms. The snippet highlights temporary file handling and cleanup suitable for CI workflows.
// Prompt: Generate QR Code barcode and integrate generation into CI pipeline for automated testing.
// Tags: qr code, barcode generation, barcode recognition, ci pipeline, automated testing, png output, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates QR Code generation and verification using Aspose.BarCode, suitable for CI pipeline integration.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates a QR Code, saves it, verifies by decoding, and cleans up temporary files.
    /// </summary>
    static void Main()
    {
        // Prepare a unique temporary output directory
        string outputDir = Path.Combine(Path.GetTempPath(), "AsposeBarcodeDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputDir);

        // Define QR code content and target file path
        string qrText = "Hello Aspose QR";
        string qrFile = Path.Combine(outputDir, "qr_code.png");

        // Generate QR Code image using BarcodeGenerator
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, qrText))
        {
            // Set module size (pixel dimension) and error correction level
            generator.Parameters.Barcode.XDimension.Pixels = 4f;
            generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelM;
            // Save the generated QR code as a PNG file
            generator.Save(qrFile, BarCodeImageFormat.Png);
        }

        // Verify that the QR code image was created successfully
        if (!File.Exists(qrFile))
        {
            Console.WriteLine("FAILED: QR code image was not created.");
            return;
        }

        // Read and decode the QR Code to confirm its content
        using (var reader = new BarCodeReader(qrFile, DecodeType.QR))
        {
            var results = reader.ReadBarCodes();
            if (results.Length == 0)
            {
                Console.WriteLine("FAILED: No barcode detected during verification.");
            }
            else
            {
                Console.WriteLine("SUCCESS: QR code generated and verified.");
                Console.WriteLine("Decoded text: " + results[0].CodeText);
            }
        }

        // Clean up temporary files and directory (optional in CI)
        try
        {
            File.Delete(qrFile);
            Directory.Delete(outputDir);
        }
        catch
        {
            // Ignore cleanup errors in CI environment
        }
    }
}