using System;
using System.Diagnostics;
using System.Text;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

class Program
{
    static void Main()
    {
        // Simulated request payload
        string payload = "{\"action\":\"generate\",\"type\":\"qr\",\"data\":\"Sample QR Code\"}";

        // Calculate payload size in bytes (UTF-8)
        int payloadSize = Encoding.UTF8.GetByteCount(payload);

        // Start measuring response time
        var stopwatch = Stopwatch.StartNew();

        // Generate QR Code barcode
        using (var generator = new BarcodeGenerator(EncodeTypes.QR))
        {
            generator.CodeText = payload;

            // Set high error correction level
            generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelH;

            // Save the barcode image to a file
            generator.Save("qr.png");
        }

        // Stop measuring
        stopwatch.Stop();
        long responseTimeMs = stopwatch.ElapsedMilliseconds;

        // Log request details
        Console.WriteLine($"Payload size: {payloadSize} bytes");
        Console.WriteLine($"Response time: {responseTimeMs} ms");
        Console.WriteLine("QR code image saved as 'qr.png'.");
    }
}