// Title: Generate QR Code and Log Payload Size with Response Time
// Description: Demonstrates creating a QR Code barcode using Aspose.BarCode, measuring generation time, and logging payload size.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, showcasing how to use BarcodeGenerator with QR symbology. It covers setting error correction, encoding mode, and measuring performance—common tasks for developers integrating barcodes into web services or applications.
// Prompt: Generate QR Code barcode and log request details including payload size and response time.
// Tags: qr code, barcode generation, performance logging, aspose.barcode, encode types

using System;
using System.Diagnostics;
using System.Text;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates QR Code generation and logging of payload size and generation time.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates a QR Code, measures generation time, and outputs payload size and response time.
    /// </summary>
    static void Main()
    {
        // Define the QR code payload (e.g., a URL or API endpoint)
        string codeText = "https://example.com/api/request";

        // Calculate the payload size in bytes using UTF-8 encoding
        int payloadSize = Encoding.UTF8.GetByteCount(codeText);

        // Start a stopwatch to measure the barcode generation time
        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();

        // Create a BarcodeGenerator for QR Code symbology
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.QR))
        {
            // Assign the text to be encoded
            generator.CodeText = codeText;

            // Set a high error correction level (Level H) for better resilience
            generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelH;

            // Use automatic encoding mode to let the library choose the optimal mode
            generator.Parameters.Barcode.QR.EncodeMode = QREncodeMode.Auto;

            // Save the generated QR Code image to a file
            generator.Save("qr.png");
        }

        // Stop the stopwatch and calculate elapsed time in milliseconds
        stopwatch.Stop();
        long responseTimeMs = stopwatch.ElapsedMilliseconds;

        // Output the payload size and generation (response) time to the console
        Console.WriteLine($"Payload size: {payloadSize} bytes");
        Console.WriteLine($"Response time: {responseTimeMs} ms");
    }
}