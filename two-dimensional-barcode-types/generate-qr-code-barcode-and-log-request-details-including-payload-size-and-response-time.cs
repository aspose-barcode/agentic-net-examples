// Title: Generate QR Code and Log Payload Size with Response Time
// Description: Demonstrates creating a QR Code barcode from a request payload, logging the payload size, and measuring the generation time.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing how to use the BarcodeGenerator class with EncodeTypes.QR to produce QR Code images. Typical use cases include encoding URLs or data for mobile scanning, where developers often need to log request details such as payload size and benchmark generation performance. The snippet highlights common API members like Parameters.Barcode.QR and the Save method, useful for quick prototyping or integration into larger systems.
// Prompt: Generate QR Code barcode and log request details including payload size and response time.
// Tags: qr code, barcode generation, payload size, response time, aspose.barcode, png output

using System;
using System.IO;
using System.Diagnostics;
using System.Text;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that generates a QR Code barcode, logs payload size, and measures response time.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates QR code from a sample payload, logs details, and saves image.
    /// </summary>
    /// <param name="args">Command‑line arguments (not used).</param>
    static void Main(string[] args)
    {
        // Simulated request payload (e.g., an API endpoint)
        string payload = "https://example.com/api/data?param=123";

        // Log payload size in bytes using UTF‑8 encoding
        int payloadSize = Encoding.UTF8.GetByteCount(payload);
        Console.WriteLine($"Payload size: {payloadSize} bytes");

        // Create a unique temporary folder for the output image
        string outputFolder = Path.Combine(Path.GetTempPath(), "AsposeBarcodeDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputFolder);
        string outputFile = Path.Combine(outputFolder, "qr.png");

        // Start timing the barcode generation and saving process
        Stopwatch stopwatch = Stopwatch.StartNew();

        // Initialize the barcode generator for QR code with the payload as data
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, payload))
        {
            // Optional: set QR error correction level to Medium (LevelM)
            generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelM;

            // Save the generated QR code image to the specified file (PNG format by default)
            generator.Save(outputFile);
        }

        // Stop the timer and report elapsed time
        stopwatch.Stop();

        Console.WriteLine($"QR code generated and saved to: {outputFile}");
        Console.WriteLine($"Response time: {stopwatch.ElapsedMilliseconds} ms");
    }
}