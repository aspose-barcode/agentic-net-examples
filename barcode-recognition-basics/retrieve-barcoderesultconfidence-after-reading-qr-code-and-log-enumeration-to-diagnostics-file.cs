using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating a QR code, reading it, and logging the confidence level.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates a QR code, reads it back, and writes the confidence value to a file and console.
    /// </summary>
    static void Main()
    {
        // Sample QR code text to encode
        const string qrText = "https://example.com";

        // Create a barcode generator for QR type with the specified text
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, qrText))
        {
            // Generate the QR code image as a bitmap in memory
            using (Bitmap qrBitmap = generator.GenerateBarCodeImage())
            {
                // Initialize a reader to decode QR codes from the generated bitmap
                using (var reader = new BarCodeReader(qrBitmap, DecodeType.QR))
                {
                    // Iterate through all detected barcodes (should be one in this case)
                    foreach (var result in reader.ReadBarCodes())
                    {
                        // Retrieve the confidence level of the detection
                        BarCodeConfidence confidence = result.Confidence;

                        // Write the confidence value to a diagnostics file (overwrites existing file)
                        using (var writer = new StreamWriter("diagnostics.txt", false))
                        {
                            writer.WriteLine($"Confidence: {confidence}");
                        }

                        // Also output the confidence value to the console for demonstration
                        Console.WriteLine($"Confidence: {confidence}");
                    }
                }
            }
        }
    }
}