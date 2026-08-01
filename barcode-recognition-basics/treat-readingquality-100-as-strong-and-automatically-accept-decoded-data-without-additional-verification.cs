// Title: QR Code Generation and Reading with Strong ReadingQuality Handling
// Description: Generates a QR code, saves it as a PNG file, then reads the barcode back and automatically accepts codes with maximum reading quality.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category, demonstrating how to use BarcodeGenerator (for creating barcodes) and BarCodeReader (for decoding). Typical use cases include creating QR codes for data exchange and validating them with high confidence. Developers often need to assess reading quality to decide whether additional verification is required.
// Prompt: Treat ReadingQuality 100 as strong and automatically accept the decoded data without additional verification.
// Tags: qr, generation, recognition, readingquality, aspose.barcode, png

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating a QR code, saving it to a file, and reading it back while
/// automatically accepting results with a ReadingQuality of 100.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a QR code, saves it, and reads it using
    /// Aspose.BarCode APIs, applying a strong quality rule.
    /// </summary>
    static void Main()
    {
        // Define the output path for the generated barcode image.
        string imagePath = "barcode.png";

        // Generate a QR code barcode with the text "StrongQualityTest" and save it as PNG.
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "StrongQualityTest"))
        {
            generator.Save(imagePath);
        }

        // Ensure the image file was created before attempting to read it.
        if (!File.Exists(imagePath))
        {
            Console.WriteLine($"Image file not found: {imagePath}");
            return;
        }

        // Initialize a barcode reader that attempts to decode all supported barcode types.
        using (var reader = new BarCodeReader(imagePath, DecodeType.AllSupportedTypes))
        {
            // Iterate through each detected barcode in the image.
            foreach (var result in reader.ReadBarCodes())
            {
                double quality = result.ReadingQuality;

                // Accept the result automatically if the reading quality is perfect (100).
                if (quality == 100.0)
                {
                    Console.WriteLine($"Accepted: {result.CodeText}");
                }
                else
                {
                    Console.WriteLine($"Rejected (ReadingQuality {quality}): {result.CodeText}");
                }
            }
        }
    }
}