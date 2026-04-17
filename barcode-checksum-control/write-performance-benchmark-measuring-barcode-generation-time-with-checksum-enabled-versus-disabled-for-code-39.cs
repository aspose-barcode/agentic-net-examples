using System;
using System.Diagnostics;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

class Program
{
    // Number of barcodes to generate for each test (kept small for quick execution)
    const int Iterations = 20;
    // Sample Code39 text (valid for both checksum modes)
    const string SampleText = "CODE39TEST";

    static void Main()
    {
        // Benchmark with checksum enabled
        long enabledTicks = BenchmarkGeneration(EnableChecksum.Yes);
        // Benchmark with checksum disabled
        long disabledTicks = BenchmarkGeneration(EnableChecksum.No);

        Console.WriteLine($"Code39 generation with checksum ENABLED:  {enabledTicks} ms");
        Console.WriteLine($"Code39 generation with checksum DISABLED: {disabledTicks} ms");
    }

    // Measures the time required to generate a set of barcodes with the specified checksum setting
    static long BenchmarkGeneration(EnableChecksum checksumSetting)
    {
        var stopwatch = Stopwatch.StartNew();

        for (int i = 0; i < Iterations; i++)
        {
            // Create a new generator for Code39
            using (var generator = new BarcodeGenerator(EncodeTypes.Code39, SampleText))
            {
                // Apply checksum setting
                generator.Parameters.Barcode.IsChecksumEnabled = checksumSetting;

                // Save to a memory stream to avoid file I/O overhead
                using (var ms = new MemoryStream())
                {
                    generator.Save(ms, BarCodeImageFormat.Png);
                    // Optionally, the stream could be used further; here we just discard it
                }
            }
        }

        stopwatch.Stop();
        return stopwatch.ElapsedMilliseconds;
    }
}