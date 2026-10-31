using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

class Program
{
    static void Main()
    {
        // Prepare temporary folder for generated images (optional, not used for timing)
        string outputFolder = Path.Combine(Path.GetTempPath(), "DotCodeBenchmark_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputFolder);

        // Define encoding modes to benchmark
        var modes = new List<(string Name, Action<BarcodeGenerator> Configure)>
        {
            ("Auto", gen =>
            {
                gen.Parameters.Barcode.DotCode.EncodeMode = DotCodeEncodeMode.Auto;
                gen.CodeText = "SampleAuto";
            }),
            ("Binary", gen =>
            {
                gen.Parameters.Barcode.DotCode.EncodeMode = DotCodeEncodeMode.Binary;
                byte[] data = { 0xFF, 0xFE, 0xFD, 0xFC, 0xFB };
                gen.SetCodeText(data);
            }),
            ("ECI", gen =>
            {
                gen.Parameters.Barcode.DotCode.EncodeMode = DotCodeEncodeMode.ECI;
                gen.Parameters.Barcode.DotCode.ECIEncoding = ECIEncodings.UTF8;
                gen.CodeText = "SampleECI";
            }),
            ("Extended", gen =>
            {
                gen.Parameters.Barcode.DotCode.EncodeMode = DotCodeEncodeMode.Extended;
                gen.CodeText = "SampleExtended";
            })
        };

        var results = new ConcurrentBag<(string Mode, long Milliseconds)>();

        // Benchmark each mode in parallel
        Parallel.ForEach(modes, modeInfo =>
        {
            const int iterations = 5;
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            for (int i = 0; i < iterations; i++)
            {
                using (var generator = new BarcodeGenerator(EncodeTypes.DotCode))
                {
                    modeInfo.Configure(generator);
                    using (var ms = new MemoryStream())
                    {
                        generator.Save(ms, BarCodeImageFormat.Png);
                        // Optionally write to file to verify generation (not required for timing)
                        // string filePath = Path.Combine(outputFolder, $"{modeInfo.Name}_{i}.png");
                        // File.WriteAllBytes(filePath, ms.ToArray());
                    }
                }
            }

            stopwatch.Stop();
            results.Add((modeInfo.Name, stopwatch.ElapsedMilliseconds));
        });

        // Output benchmark results
        Console.WriteLine("DotCode Generation Benchmark (5 iterations per mode):");
        foreach (var result in results)
        {
            Console.WriteLine($"{result.Mode}: {result.Milliseconds} ms");
        }
    }
}