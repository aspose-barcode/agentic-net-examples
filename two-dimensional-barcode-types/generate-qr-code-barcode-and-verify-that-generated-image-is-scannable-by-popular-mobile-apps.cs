// Title: Generate QR Code and verify its scannability
// Description: This example creates a QR Code image, saves it, and then reads it back to confirm that mobile apps can decode it.
// Category-Description: Demonstrates Aspose.BarCode QR Code generation and recognition, covering the BarcodeGenerator, BarCodeReader, and related parameter classes. Typical use cases include creating scannable QR codes for URLs, product information, or authentication, and validating them programmatically. Developers often need to ensure correct error correction levels, image resolution, and successful decoding across devices.
// Prompt: Generate QR Code barcode and verify that generated image is scannable by popular mobile apps.
// Tags: qr code, generation, recognition, image, aspose.barcode, encode, decode, barcode symbology, output format

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Example program that generates a QR Code barcode, saves it as an image,
/// and verifies its readability using Aspose.BarCode's recognition engine.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates the QR Code, writes it to disk, and reads it back to confirm scannability.
    /// </summary>
    static void Main()
    {
        // Define the full path for the output PNG file.
        string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "qr_code.png");

        // -------------------------------------------------
        // Generate QR Code barcode
        // -------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "https://example.com"))
        {
            // Set a high error correction level (Level H) to improve readability on damaged or low‑quality scans.
            generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelH;

            // Define image resolution (dots per inch). Higher DPI yields a sharper image.
            generator.Parameters.Resolution = 300;

            // Save the generated barcode image to the specified path.
            generator.Save(outputPath);
        }

        // -------------------------------------------------
        // Verify the generated barcode by reading it back
        // -------------------------------------------------
        if (!File.Exists(outputPath))
        {
            Console.WriteLine($"Failed to create barcode image at {outputPath}");
            return;
        }

        // Initialize a reader for QR Code type using the saved image.
        using (var reader = new BarCodeReader(outputPath, DecodeType.QR))
        {
            // Iterate through all detected barcodes (normally one for this example).
            foreach (var result in reader.ReadBarCodes())
            {
                Console.WriteLine($"Detected Type: {result.CodeTypeName}");
                Console.WriteLine($"Code Text   : {result.CodeText}");
                Console.WriteLine($"Confidence  : {result.Confidence}");
                Console.WriteLine($"ReadingQuality: {result.ReadingQuality}");

                // Output the bounding rectangle of the detected barcode (optional diagnostic info).
                var bounds = result.Region.Rectangle;
                Console.WriteLine($"Region      : X={bounds.X}, Y={bounds.Y}, Width={bounds.Width}, Height={bounds.Height}");
            }
        }
    }
}