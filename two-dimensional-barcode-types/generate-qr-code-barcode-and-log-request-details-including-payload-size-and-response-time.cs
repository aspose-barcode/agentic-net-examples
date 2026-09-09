// Title: Generate QR Code and log payload size with generation time
// Description: Demonstrates creating a QR Code barcode from a request payload, saving it as PNG, and logging the payload size and generation duration.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, illustrating how to use BarcodeGenerator with QR symbology. It covers setting barcode parameters such as X‑Dimension and error correction level, saving the image, and measuring performance. Developers working with QR codes for data encoding, mobile scanning, or API responses can use this pattern to quickly generate and benchmark barcodes.
// Prompt: Generate QR Code barcode and log request details including payload size and response time.
// Tags: qr code, barcode generation, payload size, performance measurement, aspose.barcode, png output

using System;
using System.IO;
using System.Diagnostics;
using System.Text;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Program that generates a QR Code barcode from a sample payload,
/// saves it as a PNG file, and logs payload size and generation time.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Creates a QR Code, measures generation time, and writes details to console.
    /// </summary>
    static void Main()
    {
        // Simulated request payload (e.g., an API endpoint)
        string payload = "https://example.com/api/data?param=123";

        // Calculate payload size in bytes using UTF‑8 encoding
        long payloadSize = Encoding.UTF8.GetByteCount(payload);

        // Build a temporary file path for the generated QR code image
        string outputPath = Path.Combine(Path.GetTempPath(), "qr_" + Guid.NewGuid().ToString("N") + ".png");

        // Start measuring the barcode generation time
        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();

        // Create a QR Code generator with the payload as the encoded data
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.QR, payload))
        {
            // Set the size of each QR module (pixel dimension)
            generator.Parameters.Barcode.XDimension.Pixels = 4f;

            // Choose error correction level (Level M provides a good balance)
            generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelM;

            // Save the generated QR code as a PNG image
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Stop the timer after generation completes
        stopwatch.Stop();

        // Log the payload size, generation time, and output file location
        Console.WriteLine($"Payload size: {payloadSize} bytes");
        Console.WriteLine($"Generation time: {stopwatch.ElapsedMilliseconds} ms");
        Console.WriteLine($"QR code saved to: {outputPath}");
    }
}