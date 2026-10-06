// Title: Generate and Verify QR Code Barcode using Aspose.BarCode
// Description: This example creates a QR Code barcode image, saves it as PNG, and then reads it back to confirm the encoded text matches the original.
// Category-Description: Demonstrates Aspose.BarCode generation and recognition APIs. It uses BarcodeGenerator to produce QR Code images and BarCodeReader to decode them, a common workflow for developers who need to embed scannable data (e.g., URLs, identifiers) in applications and verify correctness automatically. Ideal for batch processing, testing, and integration scenarios where barcode readability must be programmatically ensured.
// Prompt: Generate a QR Code barcode and verify readability with external scanner library.
// Tags: qr code, barcode generation, barcode recognition, png, aspose.barcode, csharp

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates generating a QR Code barcode, saving it, and verifying its readability using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a QR Code, reads it back, and validates the decoded text.
    /// </summary>
    static void Main()
    {
        // --------------------------------------------------------------------
        // Prepare temporary directory and file path for the barcode image
        // --------------------------------------------------------------------
        string tempDir = Path.Combine(Path.GetTempPath(), "AsposeQrDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempDir);
        string barcodePath = Path.Combine(tempDir, "qr.png");

        // --------------------------------------------------------------------
        // Text that will be encoded into the QR Code
        // --------------------------------------------------------------------
        string originalText = "Aspose QR Code Test";

        // --------------------------------------------------------------------
        // Generate QR Code using BarcodeGenerator
        // --------------------------------------------------------------------
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.QR, originalText))
        {
            // Set error correction level to Medium (Level M)
            generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelM;
            // Define module size (pixel dimension)
            generator.Parameters.Barcode.XDimension.Pixels = 5f;
            // Save the generated barcode as a PNG image
            generator.Save(barcodePath, BarCodeImageFormat.Png);
        }

        // --------------------------------------------------------------------
        // Verify that the barcode image was created successfully
        // --------------------------------------------------------------------
        if (!File.Exists(barcodePath))
        {
            Console.WriteLine("Failed to generate barcode image.");
            return;
        }

        // --------------------------------------------------------------------
        // Initialize barcode reader for QR Code decoding
        // --------------------------------------------------------------------
        BaseDecodeType decodeType = DecodeType.QR;
        using (BarCodeReader reader = new BarCodeReader(barcodePath, decodeType))
        {
            // Read all barcodes found in the image
            var results = reader.ReadBarCodes();

            // Check if any barcode was detected
            if (results == null || results.Length == 0)
            {
                Console.WriteLine("No barcode detected.");
                return;
            }

            // Iterate through detection results and validate decoded text
            foreach (var result in results)
            {
                Console.WriteLine($"Decoded Text: {result.CodeText}");
                Console.WriteLine($"Symbology: {result.CodeType}");
                if (result.CodeText == originalText)
                {
                    Console.WriteLine("Verification succeeded: decoded text matches original.");
                }
                else
                {
                    Console.WriteLine("Verification failed: decoded text does not match original.");
                }
            }
        }

        // --------------------------------------------------------------------
        // Cleanup temporary files and directory (optional)
        // --------------------------------------------------------------------
        try
        {
            File.Delete(barcodePath);
            Directory.Delete(tempDir);
        }
        catch
        {
            // Ignore any errors during cleanup
        }
    }
}