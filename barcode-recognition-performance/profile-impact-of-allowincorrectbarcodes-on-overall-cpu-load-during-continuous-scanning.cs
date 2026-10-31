// Title: Profile CPU Impact of AllowIncorrectBarcodes During Barcode Scanning
// Description: Demonstrates how to measure CPU usage when reading barcodes with the AllowIncorrectBarcodes setting toggled, using Aspose.BarCode.
// Category-Description: This example belongs to the Aspose.BarCode performance profiling category. It shows how to generate sample Code128 barcodes, read them repeatedly with BarCodeReader, and capture wall‑clock and processor time. Developers working on high‑throughput scanning solutions often need to evaluate the effect of QualitySettings.AllowIncorrectBarcodes on CPU load, making this pattern useful for benchmarking and optimization.
// Prompt: Profile the impact of AllowIncorrectBarcodes on overall CPU load during continuous scanning.
// Tags: barcode, performance, profiling, allowincorrectbarcodes, code128, aspose.barcode, cpu, benchmarking

using System;
using System.IO;
using System.Collections.Generic;
using System.Diagnostics;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Generates sample barcodes, reads them repeatedly with different AllowIncorrectBarcodes settings,
/// and reports wall‑clock and CPU time to help profile performance impact.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Creates temporary barcode images, profiles reading with both false and true
    /// AllowIncorrectBarcodes values, and cleans up generated files.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for generated barcode images
        string tempFolder = Path.Combine(Path.GetTempPath(), "BarcodeProfile_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Generate a set of sample Code128 barcode images
        List<string> barcodeFiles = new List<string>();
        for (int i = 0; i < 5; i++)
        {
            string codeText = "Sample" + i + DateTime.Now.Ticks;
            string filePath = Path.Combine(tempFolder, $"barcode_{i}.png");
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
            {
                generator.Save(filePath, BarCodeImageFormat.Png);
            }
            barcodeFiles.Add(filePath);
        }

        // Number of read iterations per image (controls total workload)
        int readIterations = 20;

        // Profile reading with AllowIncorrectBarcodes set to false
        ProfileReading(barcodeFiles, readIterations, false);

        // Profile reading with AllowIncorrectBarcodes set to true
        ProfileReading(barcodeFiles, readIterations, true);

        // Clean up generated files and temporary folder
        foreach (var file in barcodeFiles)
        {
            if (File.Exists(file))
                File.Delete(file);
        }
        Directory.Delete(tempFolder, true);
    }

    /// <summary>
    /// Reads each barcode file repeatedly, measuring wall‑clock and CPU time.
    /// </summary>
    /// <param name="files">List of barcode image file paths.</param>
    /// <param name="iterations">How many times to loop over the file list.</param>
    /// <param name="allowIncorrect">Value to assign to QualitySettings.AllowIncorrectBarcodes.</param>
    static void ProfileReading(List<string> files, int iterations, bool allowIncorrect)
    {
        // Display current profiling mode
        string mode = allowIncorrect ? "AllowIncorrectBarcodes: true" : "AllowIncorrectBarcodes: false";
        Console.WriteLine(mode);

        // Capture initial CPU time and start a wall‑clock timer
        Process proc = Process.GetCurrentProcess();
        TimeSpan cpuStart = proc.TotalProcessorTime;
        Stopwatch sw = Stopwatch.StartNew();

        int totalBarcodesRead = 0;

        // Perform the reading loop
        for (int iter = 0; iter < iterations; iter++)
        {
            foreach (var path in files)
            {
                if (!File.Exists(path))
                    continue;

                using (var reader = new BarCodeReader(path, DecodeType.Code128))
                {
                    // Apply the AllowIncorrectBarcodes setting for this run
                    reader.QualitySettings.AllowIncorrectBarcodes = allowIncorrect;
                    BarCodeResult[] results = reader.ReadBarCodes();
                    totalBarcodesRead += results.Length;
                }
            }
        }

        // Stop timers and calculate elapsed times
        sw.Stop();
        TimeSpan cpuEnd = proc.TotalProcessorTime;
        TimeSpan cpuUsed = cpuEnd - cpuStart;

        // Output profiling results
        Console.WriteLine($"Total reads performed: {files.Count * iterations}");
        Console.WriteLine($"Barcodes recognized: {totalBarcodesRead}");
        Console.WriteLine($"Elapsed wall time: {sw.Elapsed.TotalSeconds:F2} s");
        Console.WriteLine($"CPU time used: {cpuUsed.TotalSeconds:F2} s");
        Console.WriteLine();
    }
}