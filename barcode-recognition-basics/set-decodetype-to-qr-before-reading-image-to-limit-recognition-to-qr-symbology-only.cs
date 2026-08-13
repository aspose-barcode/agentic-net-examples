// Title: QR Code Generation and QR-Only Decoding Example
// Description: Demonstrates generating a QR barcode image and then decoding it while restricting recognition to QR symbology.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and recognition category. It showcases the use of BarcodeGenerator for creating QR codes and BarCodeReader with DecodeType.QR to limit decoding to a specific symbology. Developers often need to generate barcodes and later read them efficiently, especially when only one type of barcode is expected, to improve performance and accuracy.
// Prompt: Set DecodeType to QR before reading an image to limit recognition to QR symbology only.
// Tags: barcode symbology, qr, generation, decoding, aspose.barcode, decode type

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Generates a QR barcode image and reads it back, limiting the decoding process to QR symbology only.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates a QR code, saves it as PNG, and then decodes it using QR‑only recognition.
    /// </summary>
    static void Main()
    {
        // Path where the generated QR image will be saved
        string qrImagePath = "qr.png";

        // ------------------------------------------------------------
        // Generate a QR barcode image
        // ------------------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "Hello QR"))
        {
            // Save the QR code as a PNG file
            generator.Save(qrImagePath, BarCodeImageFormat.Png);
        }

        // Verify that the image file was successfully created
        if (!File.Exists(qrImagePath))
        {
            Console.WriteLine("Failed to create QR image.");
            return;
        }

        // ------------------------------------------------------------
        // Read the image, limiting recognition to QR symbology only
        // ------------------------------------------------------------
        using (var reader = new BarCodeReader(qrImagePath, DecodeType.QR))
        {
            // Iterate through all detected barcodes (expected to be one QR code)
            foreach (var result in reader.ReadBarCodes())
            {
                Console.WriteLine($"Detected Type: {result.CodeTypeName}");
                Console.WriteLine($"Decoded Text: {result.CodeText}");
            }
        }
    }
}