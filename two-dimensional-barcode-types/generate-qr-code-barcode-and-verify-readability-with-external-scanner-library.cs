// Title: Generate QR Code and verify readability using Aspose.BarCode
// Description: Demonstrates creating a QR Code barcode image with Aspose.BarCode, then reading it back with the built‑in barcode recognizer to confirm the encoded text matches the original.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and recognition category. It shows how to use BarcodeGenerator to produce a QR Code, configure its parameters (X‑dimension, error correction level), save the image, and then employ BarCodeReader with DecodeType.QR to decode and validate the barcode. Developers working with QR Code creation, image output, and automated verification can use this pattern for testing, batch processing, or integration scenarios.
// Prompt: Generate a QR Code barcode and verify readability with external scanner library.
// Tags: qr code, barcode generation, barcode recognition, aspose.barcode, c#, console

using System;
using System.IO;
using System.Linq;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates QR Code generation and verification using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the demo. Generates a QR Code, reads it back, and validates the content.
    /// </summary>
    static void Main()
    {
        // Prepare a unique temporary folder for the demo files.
        string tempFolder = Path.Combine(Path.GetTempPath(), "AsposeBarcodeDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Define the output image path and the text to encode.
        string qrPath = Path.Combine(tempFolder, "qr.png");
        string originalText = "Hello Aspose QR";

        // Generate QR Code with specific parameters.
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, originalText))
        {
            // Set module size (X dimension) and high error correction level.
            generator.Parameters.Barcode.XDimension.Pixels = 8f;
            generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelH;

            // Save the generated QR Code as a PNG image.
            generator.Save(qrPath, BarCodeImageFormat.Png);
        }

        // Verify that the image file was created successfully.
        if (!File.Exists(qrPath))
        {
            Console.WriteLine("Failed to generate QR code image.");
            return;
        }

        // Read and decode the QR Code from the saved image.
        using (var reader = new BarCodeReader(qrPath, DecodeType.QR))
        {
            var results = reader.ReadBarCodes();
            var first = results.FirstOrDefault();

            // Compare the decoded text with the original input.
            if (first != null && first.CodeText == originalText)
            {
                Console.WriteLine("QR code read successfully. Decoded text matches original.");
            }
            else if (first != null)
            {
                Console.WriteLine($"QR code read, but text mismatch. Expected: '{originalText}', Got: '{first.CodeText}'.");
            }
            else
            {
                Console.WriteLine("No QR code detected in the generated image.");
            }
        }

        // Cleanup temporary files and folder (optional).
        try
        {
            File.Delete(qrPath);
            Directory.Delete(tempFolder);
        }
        catch
        {
            // Ignored - cleanup not critical for demo.
        }
    }
}