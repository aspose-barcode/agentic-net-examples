// Title: QR Code Generation and QR-Only Decoding Example
// Description: Demonstrates generating a QR code image and reading it back while restricting the decoder to QR symbology only.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It showcases the use of BarcodeGenerator to create QR codes and BarCodeReader to decode them. Developers often need to generate barcodes for data encoding and later scan images, sometimes limiting recognition to specific symbologies for performance or accuracy. The key API classes illustrated are BarcodeGenerator, BarCodeImageFormat, BarCodeReader, DecodeType, and BarCodeResult.
// Prompt: Set DecodeType to QR before reading an image to limit recognition to QR symbology only.
// Tags: qr, barcode, generation, recognition, decode, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Generates a QR code image, reads it back using QR‑only decoding, and cleans up temporary files.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates a temporary directory, generates a QR code, decodes it,
    /// and then removes all temporary artifacts.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for the demo files
        string tempDir = Path.Combine(Path.GetTempPath(), "AsposeBarcodeDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempDir);

        // Define the full path for the QR code image
        string imagePath = Path.Combine(tempDir, "qr.png");

        // -------------------------------------------------
        // Generate a QR code image and save it as PNG
        // -------------------------------------------------
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.QR, "https://example.com"))
        {
            generator.Save(imagePath, BarCodeImageFormat.Png);
        }

        // Verify that the image was created successfully
        if (!File.Exists(imagePath))
        {
            Console.WriteLine("Failed to create barcode image.");
            return;
        }

        // -------------------------------------------------
        // Read the image, limiting recognition to QR symbology only
        // -------------------------------------------------
        using (BarCodeReader reader = new BarCodeReader(imagePath, DecodeType.QR))
        {
            BarCodeResult[] results = reader.ReadBarCodes();

            if (results.Length == 0)
            {
                Console.WriteLine("No QR code detected.");
            }
            else
            {
                foreach (BarCodeResult result in results)
                {
                    Console.WriteLine($"Detected Type: {result.CodeTypeName}");
                    Console.WriteLine($"Code Text: {result.CodeText}");
                }
            }
        }

        // -------------------------------------------------
        // Cleanup temporary files and directory
        // -------------------------------------------------
        try
        {
            File.Delete(imagePath);
            Directory.Delete(tempDir);
        }
        catch
        {
            // Ignore any errors that occur during cleanup
        }
    }
}