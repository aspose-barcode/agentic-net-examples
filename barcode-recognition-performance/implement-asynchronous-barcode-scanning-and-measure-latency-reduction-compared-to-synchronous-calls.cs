using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

class Program
{
    static void Main(string[] args)
    {
        // Prepare barcode image
        string filePath = "barcode.png";
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            generator.Save(filePath);
        }

        // Verify file exists
        if (!File.Exists(filePath))
        {
            Console.WriteLine("Failed to create barcode image.");
            return;
        }

        int iterations = 5;
        long totalSyncTicks = 0;
        long totalAsyncTicks = 0;

        for (int i = 0; i < iterations; i++)
        {
            // Synchronous read
            var swSync = Stopwatch.StartNew();
            using (var reader = new BarCodeReader(filePath, DecodeType.Code128))
            {
                foreach (var result in reader.ReadBarCodes())
                {
                    // Access result to ensure processing
                    var _ = result.CodeText;
                }
            }
            swSync.Stop();
            totalSyncTicks += swSync.ElapsedTicks;

            // Asynchronous read using Task.Run
            var swAsync = Stopwatch.StartNew();
            var task = Task.Run(() =>
            {
                using (var reader = new BarCodeReader(filePath, DecodeType.Code128))
                {
                    foreach (var result in reader.ReadBarCodes())
                    {
                        var _ = result.CodeText;
                    }
                }
            });
            task.Wait();
            swAsync.Stop();
            totalAsyncTicks += swAsync.ElapsedTicks;
        }

        double tickToMs = 1000.0 / Stopwatch.Frequency;
        double avgSyncMs = (totalSyncTicks * tickToMs) / iterations;
        double avgAsyncMs = (totalAsyncTicks * tickToMs) / iterations;
        double reduction = (avgSyncMs - avgAsyncMs) / avgSyncMs * 100.0;

        Console.WriteLine($"Average synchronous read time: {avgSyncMs:F3} ms");
        Console.WriteLine($"Average asynchronous read time: {avgAsyncMs:F3} ms");
        Console.WriteLine($"Latency reduction: {reduction:F2}%");
    }
}