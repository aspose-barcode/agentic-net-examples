using System;
using System.IO;
using System.Text;
using System.Diagnostics;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating a QR code image using Aspose.BarCode and measuring execution time.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates a QR code for a sample URL, saves it to a PNG file, and reports payload size and timing.
    /// </summary>
    static void Main()
    {
        // Define the QR code payload (sample URL)
        string codeText = "https://example.com/api/request";

        // Determine the payload size in bytes using UTF-8 encoding
        int payloadSize = Encoding.UTF8.GetByteCount(codeText);
        Console.WriteLine($"Payload size: {payloadSize} bytes");

        // Build the full path for the output PNG file in the current directory
        string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "qr_code.png");

        // Start a stopwatch to measure generation and saving duration
        Stopwatch stopwatch = Stopwatch.StartNew();

        // Create a barcode generator for QR code with the specified payload
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, codeText))
        {
            // Configure QR code error correction to the highest level (Level H)
            generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelH;

            // Set the image resolution to 300 DPI (float literal)
            generator.Parameters.Resolution = 300f;

            // Save the generated QR code image to the specified file path
            generator.Save(outputPath);
        }

        // Stop the stopwatch after generation and saving are complete
        stopwatch.Stop();

        // Output the location of the saved barcode and the elapsed time
        Console.WriteLine($"Barcode saved to: {outputPath}");
        Console.WriteLine($"Response time: {stopwatch.ElapsedMilliseconds} ms");
    }
}