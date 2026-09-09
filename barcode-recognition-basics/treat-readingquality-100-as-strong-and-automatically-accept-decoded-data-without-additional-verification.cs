// Title: QR Code Generation and Quality-Based Decoding Example
// Description: Demonstrates generating a QR barcode image, saving it, then reading it back while evaluating the reading quality. If the quality is 100, the decoded data is automatically accepted.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and recognition category. It showcases the use of BarcodeGenerator for creating QR codes and BarCodeReader for decoding them, highlighting how to assess the ReadingQuality property to make acceptance decisions. Developers working with barcode scanning, quality assessment, and automated validation will find this pattern useful for building robust barcode processing pipelines.
// Prompt: Treat ReadingQuality 100 as strong and automatically accept the decoded data without additional verification.
// Tags: qr, barcode generation, barcode recognition, readingquality, aspose.barcode, csharp

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates QR barcode generation, saving to a temporary file, and reading it back with quality evaluation.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a QR code, reads it, and decides acceptance based on ReadingQuality.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder to avoid naming collisions.
        string tempFolder = Path.Combine(Path.GetTempPath(), "AsposeBarcodeDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Define the full file path for the generated barcode image.
        string barcodePath = Path.Combine(tempFolder, "sample_qr.png");

        // Generate a QR barcode with the text "HelloWorld" and save it as a PNG file.
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.QR, "HelloWorld"))
        {
            generator.Save(barcodePath, BarCodeImageFormat.Png);
        }

        // Verify that the barcode image was successfully created.
        if (!File.Exists(barcodePath))
        {
            Console.WriteLine("Failed to create barcode image.");
            return;
        }

        // Initialize a reader for QR codes and iterate over all detected barcodes.
        using (BarCodeReader reader = new BarCodeReader(barcodePath, DecodeType.QR))
        {
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                // Retrieve the reading quality reported by the recognizer.
                double quality = result.ReadingQuality;

                // Accept the result automatically when quality is 100; otherwise, reject it.
                if (quality == 100.0)
                {
                    Console.WriteLine($"Accepted (Quality {quality}): {result.CodeText}");
                }
                else
                {
                    Console.WriteLine($"Rejected (Quality {quality}): {result.CodeText}");
                }
            }
        }

        // Attempt to clean up temporary files; ignore any errors during cleanup.
        try
        {
            File.Delete(barcodePath);
            Directory.Delete(tempFolder);
        }
        catch
        {
            // Cleanup failures are non‑critical; they do not affect program logic.
        }
    }
}