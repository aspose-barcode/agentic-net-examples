// Title: Read QR Code version and error correction level from detected barcodes
// Description: Generates a QR Code with a specific version and error correction level, then reads the image to extract those QR parameters.
// Category-Description: This example demonstrates Aspose.BarCode generation and recognition for QR codes. It uses BarcodeGenerator to create a QR symbol with defined version and error correction level, and BarCodeReader to decode the image and retrieve extended QR information such as version and error level. Developers working with QR code creation, validation, or metadata extraction commonly use these APIs for tasks like inventory tagging, authentication, or data encoding.
// Prompt: Read QR Code version and error correction level from each detected QR barcode.
// Tags: qr code, version, error correction level, barcode generation, barcode recognition, aspose.barcode, aspose.barcode.generation, aspose.barcode.recognition

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates how to generate a QR Code with a specific version and error correction level,
/// then read the generated image to obtain those QR parameters using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the sample. Generates a QR Code, reads it back, and outputs version and error level.
    /// </summary>
    static void Main()
    {
        // Create a temporary directory for the sample barcode image
        string tempDir = Path.Combine(Path.GetTempPath(), "QrSample_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempDir);
        string barcodePath = Path.Combine(tempDir, "sample_qr.png");

        // Generate a QR Code with specific version (5) and error correction level (H)
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.QR, "Sample QR Text"))
        {
            // Set QR version and error correction level via generator parameters
            generator.Parameters.Barcode.QR.Version = QRVersion.Version05;
            generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelH;

            // Save the generated barcode image to the temporary path
            generator.Save(barcodePath, BarCodeImageFormat.Png);
        }

        // Verify the file exists before attempting to read it
        if (!File.Exists(barcodePath))
        {
            Console.WriteLine("Failed to create barcode image.");
            return;
        }

        // Read the QR Code and output its version and error correction level
        using (BarCodeReader reader = new BarCodeReader(barcodePath, DecodeType.QR))
        {
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                Console.WriteLine("Detected QR Code:");
                Console.WriteLine($"  Code Text: {result.CodeText}");

                // Access extended QR parameters for version and error correction level
                var qrExt = result.Extended.QR;
                Console.WriteLine($"  Version: {qrExt.Version}");
                Console.WriteLine($"  Error Level: {qrExt.ErrorLevel}");
            }
        }

        // Clean up temporary files
        try
        {
            File.Delete(barcodePath);
            Directory.Delete(tempDir);
        }
        catch
        {
            // Ignored - cleanup failure should not affect program exit
        }
    }
}