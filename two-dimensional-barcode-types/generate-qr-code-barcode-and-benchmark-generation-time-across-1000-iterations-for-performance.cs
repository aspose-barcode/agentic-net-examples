using System;
using System.Diagnostics;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

namespace BarcodeBenchmark
{
    class Program
    {
        static void Main()
        {
            const int iterations = 10;
            var stopwatch = new Stopwatch();

            stopwatch.Start();
            for (int i = 0; i < iterations; i++)
            {
                // Create a QR code generator with unique text for each iteration
                using (var generator = new BarcodeGenerator(EncodeTypes.QR, $"Sample {i}"))
                {
                    // Set QR error correction level (optional)
                    generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelM;

                    // Generate the barcode image in memory
                    using (var bitmap = generator.GenerateBarCodeImage())
                    {
                        // Image is generated; no further processing needed for the benchmark
                    }
                }
            }
            stopwatch.Stop();

            Console.WriteLine($"Generated {iterations} QR codes in {stopwatch.ElapsedMilliseconds} ms.");
            Console.WriteLine($"Average time per QR code: {stopwatch.ElapsedMilliseconds / (double)iterations:F2} ms.");
        }
    }
}