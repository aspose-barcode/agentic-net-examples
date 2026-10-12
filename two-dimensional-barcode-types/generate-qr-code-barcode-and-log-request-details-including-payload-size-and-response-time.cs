// Title: Generate QR Code and Log Payload Size with Aspose.BarCode
// Description: Demonstrates creating a QR Code barcode from a text payload, logging the payload size, and measuring the generation time.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, showcasing how to use BarcodeGenerator with EncodeTypes.QR, configure QR parameters such as module size and error correction, and save the result as a PNG image. Developers often need to generate QR codes for URLs, identifiers, or data exchange and require performance metrics like payload size and generation time. The key API classes used are BarcodeGenerator, EncodeTypes, QRErrorLevel, and BarCodeImageFormat.
// Prompt: Generate QR Code barcode and log request details including payload size and response time.
// Tags: qr code, barcode generation, png output, aspose.barcode, encode types, performance logging

using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates QR Code generation using Aspose.BarCode and logs payload size and generation time.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates a QR Code from a sample payload, logs its size, measures generation time, and saves the image.
    /// </summary>
    static void Main()
    {
        // Define the text to encode in the QR Code
        string payload = "Hello Aspose QR Code!";

        // Convert payload to UTF-8 bytes and log its size
        byte[] payloadBytes = Encoding.UTF8.GetBytes(payload);
        Console.WriteLine($"Payload size: {payloadBytes.Length} bytes");

        // Determine a temporary file path for the generated PNG image
        string outputPath = Path.Combine(Path.GetTempPath(), "AsposeQRCode.png");

        // Start a stopwatch to measure barcode generation time
        Stopwatch sw = new Stopwatch();
        sw.Start();

        // Create a BarcodeGenerator for QR encoding with the given payload
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.QR, payload))
        {
            // Set the module (pixel) size of the QR Code
            generator.Parameters.Barcode.XDimension.Pixels = 4f;

            // Optionally set the error correction level (Level M provides a good balance)
            generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelM;

            // Save the generated QR Code as a PNG image to the specified path
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Stop the stopwatch and log the elapsed time
        sw.Stop();
        Console.WriteLine($"Generation time: {sw.ElapsedMilliseconds} ms");
        Console.WriteLine($"QR Code saved to: {outputPath}");
    }
}