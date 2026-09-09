// Title: Generate QR Code with fallback to Binary encoding mode
// Description: Demonstrates creating a QR Code barcode, handling auto‑selection failures by switching to Binary encoding, and verifying the result by reading it back.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and recognition category. It showcases the BarcodeGenerator class for QR Code creation, the QREncodeMode enumeration for specifying encoding, and the BarCodeReader class for decoding. Developers often need to generate QR codes with Unicode data and provide fallback strategies when automatic mode selection cannot encode the content, making this pattern useful for robust barcode generation pipelines.
// Prompt: Generate a QR Code barcode and provide fallback encoding mode when auto selection fails.
// Tags: qr code, fallback encoding, barcode generation, barcode recognition, aspose.barcode, c#, png

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates QR Code generation with a fallback encoding mode and subsequent decoding.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that creates a QR Code, falls back to Binary mode if needed, and reads the barcode.
    /// </summary>
    static void Main()
    {
        // Define output file path and the text to encode (includes Unicode characters)
        string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "qr_fallback.png");
        string codeText = "Sample Text with Unicode 漢字";

        // Attempt to generate QR Code using the default (Auto) encoding mode
        try
        {
            using (var generator = new BarcodeGenerator(EncodeTypes.QR, codeText))
            {
                // Save the generated QR Code as a PNG image
                generator.Save(outputPath, BarCodeImageFormat.Png);
                Console.WriteLine($"QR Code generated successfully (Auto mode) at: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Auto mode failed – log the error and switch to Binary encoding mode
            Console.WriteLine($"Auto mode generation failed: {ex.Message}");
            Console.WriteLine("Falling back to Binary encoding mode.");

            using (var generator = new BarcodeGenerator(EncodeTypes.QR, codeText))
            {
                // Explicitly set the QR Code encoding mode to Binary
                generator.Parameters.Barcode.QR.EncodeMode = QREncodeMode.Binary;
                // Save the QR Code generated with Binary mode
                generator.Save(outputPath, BarCodeImageFormat.Png);
                Console.WriteLine($"QR Code generated successfully (Binary mode) at: {outputPath}");
            }
        }

        // Verify the generated barcode by reading it back
        try
        {
            BaseDecodeType decodeType = DecodeType.QR;
            using (var reader = new BarCodeReader(outputPath, decodeType))
            {
                // Iterate through all detected barcodes (should be only one)
                foreach (BarCodeResult result in reader.ReadBarCodes())
                {
                    Console.WriteLine($"Decoded text: {result.CodeText}");
                }
            }
        }
        catch (Exception readEx)
        {
            // Log any errors that occur during barcode reading
            Console.WriteLine($"Failed to read barcode: {readEx.Message}");
        }
    }
}