using System;
using System.IO;
using System.Diagnostics;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.BarCode.Common;

class Program
{
    static void Main()
    {
        // Prepare temporary directory
        string tempDir = Path.Combine(Path.GetTempPath(), "AsposeBarcodeBenchmark");
        Directory.CreateDirectory(tempDir);

        // Generate sample barcode images
        string[] files = new string[5];
        for (int i = 0; i < files.Length; i++)
        {
            string filePath = Path.Combine(tempDir, $"barcode_{i}.png");
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, $"Sample{i}"))
            {
                generator.Save(filePath);
            }
            files[i] = filePath;
        }

        // Benchmark single‑thread (MaxAdditionalAllowedThreads = 0)
        BarCodeReader.ProcessorSettings.MaxAdditionalAllowedThreads = 0;
        long singleThreadTime = RunRecognition(files);

        // Benchmark multi‑thread (MaxAdditionalAllowedThreads = Environment.ProcessorCount)
        BarCodeReader.ProcessorSettings.MaxAdditionalAllowedThreads = Environment.ProcessorCount;
        long multiThreadTime = RunRecognition(files);

        Console.WriteLine($"Single‑thread time (ms): {singleThreadTime}");
        Console.WriteLine($"Multi‑thread time (ms): {multiThreadTime}");

        // Clean up temporary files
        foreach (var file in files)
        {
            if (File.Exists(file))
            {
                File.Delete(file);
            }
        }
        if (Directory.Exists(tempDir))
        {
            Directory.Delete(tempDir, true);
        }
    }

    static long RunRecognition(string[] files)
    {
        var stopwatch = Stopwatch.StartNew();

        foreach (var file in files)
        {
            using (var reader = new BarCodeReader(file, DecodeType.Code128))
            {
                // Iterate through all detected barcodes (if any)
                foreach (var result in reader.ReadBarCodes())
                {
                    // No operation needed; just force enumeration
                }
            }
        }

        stopwatch.Stop();
        return stopwatch.ElapsedMilliseconds;
    }
}