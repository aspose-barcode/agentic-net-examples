// Title: Multi‑Threaded vs Single‑Threaded Barcode Recognition Benchmark
// Description: Demonstrates how to enable multi‑threaded barcode recognition using Aspose.BarCode and compares its throughput against single‑threaded execution on a set of sample images.
// Category-Description: This example belongs to the Aspose.BarCode recognition performance category, showcasing the use of BarCodeReader with ProcessorSettings to control core utilization. Developers often need to benchmark or optimize barcode scanning in high‑volume scenarios, and this snippet illustrates typical API classes (BarCodeReader, DecodeType, ProcessorSettings) and common use cases such as throughput measurement and parallel processing.
// Prompt: Enable multi‑threaded recognition and compare throughput against single‑threaded execution on a set of images.
// Tags: barcode symbology, recognition, performance, multithreading, aspose.barcode, barcodereader, decode type

using System;
using System.IO;
using System.Diagnostics;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Example program that generates sample barcodes, then measures and compares
/// single‑threaded and multi‑threaded barcode recognition performance using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates sample barcodes, runs recognition
    /// with different core counts, and outputs the elapsed times.
    /// </summary>
    static void Main()
    {
        // Prepare a folder for sample barcode images
        string folderPath = Path.Combine(Directory.GetCurrentDirectory(), "Barcodes");
        if (!Directory.Exists(folderPath))
        {
            Directory.CreateDirectory(folderPath);
        }

        // Generate a small set of sample barcode images (5 items)
        GenerateSampleBarcodes(folderPath);

        // Measure single‑threaded recognition (force 1 core)
        double singleThreadMs = RunRecognition(folderPath, 1);
        Console.WriteLine($"Single‑threaded recognition time: {singleThreadMs:F2} ms");

        // Measure multi‑threaded recognition (use all available cores)
        int coreCount = Environment.ProcessorCount;
        double multiThreadMs = RunRecognition(folderPath, coreCount);
        Console.WriteLine($"Multi‑threaded ({coreCount} cores) recognition time: {multiThreadMs:F2} ms");
    }

    // Generates 5 Code128 barcode images with simple texts
    private static void GenerateSampleBarcodes(string folder)
    {
        for (int i = 1; i <= 5; i++)
        {
            string codeText = $"Sample{i:D2}";
            string filePath = Path.Combine(folder, $"barcode_{i}.png");
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
            {
                // Optional: set a modest size
                generator.Parameters.Barcode.XDimension.Point = 2f;
                generator.Save(filePath);
            }
        }
    }

    // Runs recognition on all PNG files in the folder using the specified core count
    private static double RunRecognition(string folder, int coreCount)
    {
        // Configure the processor cores for BarCodeReader
        BarCodeReader.ProcessorSettings.UseOnlyThisCoresCount = coreCount;

        var stopwatch = Stopwatch.StartNew();

        // Iterate through each PNG file and read barcodes
        foreach (string file in Directory.GetFiles(folder, "*.png"))
        {
            using (var reader = new BarCodeReader(file, DecodeType.AllSupportedTypes))
            {
                foreach (var result in reader.ReadBarCodes())
                {
                    // For benchmarking we just iterate through results
                    // (Optionally, you could count or log them)
                    string _ = result.CodeText;
                }
            }
        }

        stopwatch.Stop();
        return stopwatch.Elapsed.TotalMilliseconds;
    }
}