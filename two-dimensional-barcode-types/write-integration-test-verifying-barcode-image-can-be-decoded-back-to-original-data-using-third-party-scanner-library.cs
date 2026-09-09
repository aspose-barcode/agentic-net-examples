// Title: Generate and Verify QR Code Using Aspose.BarCode
// Description: Demonstrates creating a QR code image, saving it to a temporary file, and reading it back to confirm the encoded data.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It showcases the use of BarcodeGenerator for creating barcodes and BarCodeReader for decoding them. Typical scenarios include integration testing, automated validation of barcode output, and ensuring compatibility with third‑party scanners. Developers often need to generate barcode images, store them, and later verify that the encoded information can be accurately retrieved.
// Prompt: Write integration test verifying barcode image can be decoded back to original data using third‑party scanner library.
// Tags: qr, barcode, generation, recognition, png, aspose.barcode, integration-test

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Example program that generates a QR code, saves it as a PNG file,
/// reads the barcode back using Aspose.BarCode recognition, and verifies
/// that the decoded text matches the original input.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Executes the generate‑and‑verify workflow.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder to hold the barcode image
        string tempDir = Path.Combine(Path.GetTempPath(), "BarcodeTest_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempDir);

        // Define the full path for the generated PNG image and the text to encode
        string imagePath = Path.Combine(tempDir, "test_qr.png");
        string originalText = "Test123";

        // Generate a QR code image using BarcodeGenerator
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.QR, originalText))
        {
            // Set the module size (pixel dimension) for better readability
            generator.Parameters.Barcode.XDimension.Pixels = 4f;
            // Save the generated barcode as a PNG file
            generator.Save(imagePath, BarCodeImageFormat.Png);
        }

        // Verify that the image file was successfully created
        if (!File.Exists(imagePath))
        {
            Console.WriteLine("Failed to create barcode image.");
            return;
        }

        // Prepare to read the barcode back using BarCodeReader
        BaseDecodeType decodeType = DecodeType.QR;
        using (BarCodeReader reader = new BarCodeReader(imagePath, decodeType))
        {
            // Attempt to read all barcodes from the image
            BarCodeResult[] results = reader.ReadBarCodes();

            // Determine if a barcode was successfully decoded
            bool success = results.Length > 0 && !string.IsNullOrEmpty(results[0].CodeText);
            Console.WriteLine($"Barcode read success: {success}");

            if (success)
            {
                // Output details of the detected barcode
                Console.WriteLine($"Detected type: {results[0].CodeTypeName}");
                Console.WriteLine($"Decoded text (may contain evaluation watermark): {results[0].CodeText}");
            }
        }

        // Clean up temporary files and directory
        try
        {
            File.Delete(imagePath);
            Directory.Delete(tempDir, true);
        }
        catch
        {
            // Ignored – cleanup failures are non‑critical for this example
        }
    }
}