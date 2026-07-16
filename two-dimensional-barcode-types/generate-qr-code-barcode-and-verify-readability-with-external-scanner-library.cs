// Title: Generate and Validate QR Code Barcode
// Description: Demonstrates creating a QR Code image and verifying its readability using Aspose.BarCode's generation and recognition APIs.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and recognition category. It showcases the use of BarcodeGenerator for QR Code creation with high error correction, and BarCodeReader for decoding the generated image. Developers commonly use these APIs to embed scannable data in applications and to ensure barcode quality through programmatic validation.
// Prompt: Generate a QR Code barcode and verify readability with external scanner library.
// Tags: qr code, generation, recognition, png, aspose.barcode, encode, decode, error-correction

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating a QR Code barcode image and validating it using Aspose.BarCode recognition.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates a QR Code, saves it as PNG, and reads it back to verify content and quality.
    /// </summary>
    static void Main()
    {
        // Define the full path for the output PNG file
        string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "qr.png");

        // -------------------------------------------------
        // Generate QR Code
        // -------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "https://example.com"))
        {
            // Configure high error correction (Level H) for better resilience
            generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelH;

            // Save the generated barcode as a PNG image
            generator.Save(outputPath);
        }

        // Verify that the image file was successfully created
        if (!File.Exists(outputPath))
        {
            Console.WriteLine("Failed to create QR code image.");
            return;
        }

        // -------------------------------------------------
        // Read and validate the QR Code
        // -------------------------------------------------
        using (var reader = new BarCodeReader(outputPath, DecodeType.QR))
        {
            // Apply high-quality settings to improve detection robustness
            reader.QualitySettings = QualitySettings.HighQuality;

            // Iterate through all detected barcodes (should be one in this case)
            foreach (var result in reader.ReadBarCodes())
            {
                Console.WriteLine($"Barcode Type: {result.CodeTypeName}");
                Console.WriteLine($"Decoded Text: {result.CodeText}");
                Console.WriteLine($"Confidence: {result.Confidence}");
                Console.WriteLine($"Reading Quality: {result.ReadingQuality}");

                // Output the location and size of the detected barcode region
                var rect = result.Region.Rectangle;
                Console.WriteLine($"Region - X:{rect.X}, Y:{rect.Y}, Width:{rect.Width}, Height:{rect.Height}");
            }
        }
    }
}