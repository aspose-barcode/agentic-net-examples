using System;
using System.Diagnostics;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.BarCode.Common;
using Aspose.Drawing;

class Program
{
    const int ImageCount = 30;
    const string OutputFolder = "Barcodes";

    static void Main()
    {
        // Ensure output folder exists
        if (!Directory.Exists(OutputFolder))
            Directory.CreateDirectory(OutputFolder);

        // Generate sample barcode images
        GenerateBarcodes();

        // Warm up to avoid JIT impact
        ProcessBatch(useMinimalXDimension: false);

        // Profile without UseMinimalXDimension
        var resultWithout = Profile(() => ProcessBatch(useMinimalXDimension: false));

        // Profile with UseMinimalXDimension
        var resultWith = Profile(() => ProcessBatch(useMinimalXDimension: true));

        // Display results
        Console.WriteLine("=== Profiling Results ===");
        Console.WriteLine($"Without UseMinimalXDimension:  Elapsed = {resultWithout.ElapsedMilliseconds} ms, CPU = {resultWithout.CpuMilliseconds} ms");
        Console.WriteLine($"With UseMinimalXDimension:     Elapsed = {resultWith.ElapsedMilliseconds} ms, CPU = {resultWith.CpuMilliseconds} ms");
    }

    static void GenerateBarcodes()
    {
        for (int i = 0; i < ImageCount; i++)
        {
            string filePath = Path.Combine(OutputFolder, $"code_{i}.png");
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128))
            {
                generator.CodeText = $"CODE{i:D4}";
                generator.Save(filePath);
            }
        }
    }

    static void ProcessBatch(bool useMinimalXDimension)
    {
        // Configure processor settings for multi‑core usage
        BarCodeReader.ProcessorSettings.UseAllCores = true;
        BarCodeReader.ProcessorSettings.MaxAdditionalAllowedThreads = Environment.ProcessorCount * 2;

        string[] files = Directory.GetFiles(OutputFolder, "*.png");
        foreach (string file in files)
        {
            using (var reader = new BarCodeReader(file, DecodeType.Code128))
            {
                // Use high‑performance quality preset
                reader.QualitySettings = QualitySettings.HighPerformance;

                if (useMinimalXDimension)
                {
                    // Enable UseMinimalXDimension mode
                    reader.QualitySettings.XDimension = XDimensionMode.UseMinimalXDimension;
                    // Set a minimal X dimension (pixels)
                    reader.QualitySettings.MinimalXDimension = 2;
                }

                // Perform recognition (results are ignored, we only measure performance)
                foreach (BarCodeResult result in reader.ReadBarCodes())
                {
                    // No operation needed; just iterate to force processing
                }
            }
        }
    }

    // Helper to measure wall‑clock time and CPU time
    static (long ElapsedMilliseconds, long CpuMilliseconds) Profile(Action action)
    {
        var process = Process.GetCurrentProcess();
        var startCpu = process.TotalProcessorTime;
        var sw = Stopwatch.StartNew();

        action();

        sw.Stop();
        var endCpu = process.TotalProcessorTime;
        long cpuMs = (long)(endCpu - startCpu).TotalMilliseconds;
        return (sw.ElapsedMilliseconds, cpuMs);
    }
}