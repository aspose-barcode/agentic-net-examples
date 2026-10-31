using System;
using System.Diagnostics;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing.Imaging;

class Program
{
    static void Main()
    {
        const int iterations = 10; // safe sample size for benchmarking
        string codeText = "Sample QR Code for benchmarking";

        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();

        for (int i = 0; i < iterations; i++)
        {
            using (var generator = new BarcodeGenerator(EncodeTypes.QR, codeText))
            {
                generator.Parameters.Barcode.XDimension.Pixels = 4f;
                generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelM;

                using (var memoryStream = new MemoryStream())
                {
                    generator.Save(memoryStream, BarCodeImageFormat.Png);
                    // Optionally, use memoryStream.Length or other processing here
                }
            }
        }

        stopwatch.Stop();

        double totalMs = stopwatch.Elapsed.TotalMilliseconds;
        double avgMs = totalMs / iterations;

        Console.WriteLine($"Generated {iterations} QR codes in {totalMs:F2} ms. Average per barcode: {avgMs:F2} ms.");
    }
}