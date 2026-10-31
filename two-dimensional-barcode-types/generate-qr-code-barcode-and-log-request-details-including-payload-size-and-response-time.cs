// Title: Generate QR Code and Log Payload Size with Generation Time
// Description: Demonstrates creating a QR Code barcode using Aspose.BarCode, saving it as a PNG file, and logging both the payload size and the time taken to generate the barcode.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, showcasing how to use the BarcodeGenerator class with EncodeTypes.QR to produce QR Code images. Typical use cases include creating machine‑readable codes for URLs, contact information, or product data, where developers need to measure payload size and performance metrics. Common API classes used are BarcodeGenerator, EncodeTypes, BarCodeImageFormat, and System.Diagnostics.Stopwatch.
// Prompt: Generate QR Code barcode and log request details including payload size and response time.
// Tags: qr code, barcode generation, payload size, performance, aspose.barcode, png

using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that generates a QR Code barcode, saves it as PNG,
/// and logs payload size and generation time.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        // Define the data to encode in the QR Code.
        string payload = "Hello Aspose QR Code!";

        // Calculate the payload size in bytes using UTF‑8 encoding.
        int payloadSize = Encoding.UTF8.GetByteCount(payload);
        Console.WriteLine($"Payload size: {payloadSize} bytes");

        // Build a temporary file path for the generated PNG image.
        string outputPath = Path.Combine(Path.GetTempPath(), "qr_generated.png");

        // Start a stopwatch to measure barcode generation time.
        Stopwatch sw = new Stopwatch();
        sw.Start();

        // Create a BarcodeGenerator for QR Code with the specified payload.
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, payload))
        {
            // Optional: adjust the module (pixel) size of the QR Code.
            generator.Parameters.Barcode.XDimension.Pixels = 4f;

            // Save the generated barcode as a PNG file.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Stop the stopwatch and output performance details.
        sw.Stop();
        Console.WriteLine($"QR Code generated and saved to: {outputPath}");
        Console.WriteLine($"Generation time: {sw.ElapsedMilliseconds} ms");
    }
}