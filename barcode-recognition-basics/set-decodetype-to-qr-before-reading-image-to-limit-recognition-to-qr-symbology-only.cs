// Title: QR Code Generation and QR-Only Decoding Example
// Description: Demonstrates generating a QR code image and then reading it using Aspose.BarCode with DecodeType set to QR to restrict recognition to QR symbology only.
// Prompt: Set DecodeType to QR before reading an image to limit recognition to QR symbology only.
// Tags: qr, barcode, generation, recognition, decode, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Generates a QR code image (if missing) and reads it using a QR‑only decoder.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates a QR code image and decodes it with DecodeType.QR.
    /// </summary>
    static void Main()
    {
        // Path to the QR code image file
        const string imagePath = "qr.png";

        // ------------------------------------------------------------
        // Generate a QR code image if it does not already exist
        // ------------------------------------------------------------
        if (!File.Exists(imagePath))
        {
            // Initialize the generator with QR symbology and sample text
            using (var generator = new BarcodeGenerator(EncodeTypes.QR, "Hello QR"))
            {
                // Save the generated QR code to a PNG file
                generator.Save(imagePath);
            }
        }

        // ------------------------------------------------------------
        // Verify that the image file exists before attempting to read it
        // ------------------------------------------------------------
        if (!File.Exists(imagePath))
        {
            Console.WriteLine($"Image file not found: {imagePath}");
            return;
        }

        // ------------------------------------------------------------
        // Create a BarCodeReader limited to QR symbology only
        // ------------------------------------------------------------
        using (var reader = new BarCodeReader(imagePath, DecodeType.QR))
        {
            // Iterate through all detected barcodes (expected: only QR codes)
            foreach (var result in reader.ReadBarCodes())
            {
                Console.WriteLine($"Detected QR Code: {result.CodeText}");
            }
        }
    }
}