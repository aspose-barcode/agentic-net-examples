// Title: QR Code Generation and Strong Quality Barcode Reading
// Description: Generates a QR code, reads it back, and automatically accepts data when reading quality is 100, demonstrating quality-based validation.
// Prompt: Treat ReadingQuality 100 as strong and automatically accept the decoded data without additional verification.
// Tags: qr, barcode, generation, recognition, readingquality, console

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates creating a QR code, reading it, and handling strong reading quality automatically.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a QR code, reads it, and processes results based on reading quality.
    /// </summary>
    static void Main()
    {
        // Define a temporary file path for the generated barcode image
        string imagePath = Path.Combine(Path.GetTempPath(), "sample_barcode.png");

        // Generate a QR code barcode with known content
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "StrongQualityTest"))
        {
            // Save the barcode image to the temporary file
            generator.Save(imagePath);
        }

        // Verify that the image file was created
        if (!File.Exists(imagePath))
        {
            Console.WriteLine("Failed to create barcode image.");
            return;
        }

        // Read the barcode from the generated image
        using (var reader = new BarCodeReader(imagePath, DecodeType.AllSupportedTypes))
        {
            // Iterate through all detected barcodes
            foreach (var result in reader.ReadBarCodes())
            {
                double readingQuality = result.ReadingQuality;

                // Treat ReadingQuality of 100 as strong and accept automatically
                if (readingQuality == 100.0)
                {
                    Console.WriteLine($"Accepted (Strong Quality): {result.CodeText}");
                }
                else
                {
                    Console.WriteLine($"Detected (Quality {readingQuality}): {result.CodeText}");
                }
            }
        }

        // Clean up the temporary image file
        try
        {
            File.Delete(imagePath);
        }
        catch
        {
            // Ignore any errors during cleanup
        }
    }
}