// Title: Performance graph of barcode recognition time vs XDimension values
// Description: Demonstrates measuring recognition time for Code128 barcodes while varying XDimensionMode settings and outputs CSV data for graphing.
// Category-Description: This example belongs to the Aspose.BarCode performance testing category. It shows how to generate barcodes, configure QualitySettings.XDimension, and use BarCodeReader to measure processing time. Developers often need to benchmark recognition speed across different XDimension modes for optimization and reporting.
// Prompt: Generate performance graphs comparing recognition time versus XDimension values for a sample dataset.
// Tags: barcode, performance, xdimension, code128, aspose.barcode, benchmarking, csv

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that generates sample Code128 barcodes, measures recognition time
/// for different XDimensionMode settings, and outputs CSV data suitable for performance graphs.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Creates temporary barcodes, runs recognition benchmarks, prints results, and cleans up.
    /// </summary>
    static void Main()
    {
        // Create a dedicated temporary folder for generated barcode images
        string tempFolder = Path.Combine(Path.GetTempPath(), "Perf_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Generate a set of sample barcode images
        List<string> barcodeFiles = new List<string>();
        int sampleCount = 5;
        for (int i = 1; i <= sampleCount; i++)
        {
            string codeText = $"Sample{i:D2}";
            string filePath = Path.Combine(tempFolder, $"code{i:D2}.png");
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
            {
                generator.Save(filePath, BarCodeImageFormat.Png);
            }
            barcodeFiles.Add(filePath);
        }

        // Define the XDimension modes to be tested
        var modes = new List<(XDimensionMode mode, string name, bool setMinimal)>
        {
            (XDimensionMode.Normal, "Normal", false),
            (XDimensionMode.Small, "Small", false),
            (XDimensionMode.UseMinimalXDimension, "UseMinimalXDimension", true)
        };

        // Output CSV header
        Console.WriteLine("XDimensionMode,TotalTimeMs,AvgTimeMs,TotalRecognized");

        // Iterate over each mode and measure recognition performance
        foreach (var (mode, name, setMinimal) in modes)
        {
            Stopwatch sw = Stopwatch.StartNew();
            int totalRecognized = 0;

            // Process each generated barcode file
            foreach (string file in barcodeFiles)
            {
                if (!File.Exists(file))
                {
                    Console.WriteLine($"Warning: file not found {file}");
                    continue;
                }

                using (var reader = new BarCodeReader(file, DecodeType.Code128))
                {
                    // Apply the current XDimension mode
                    reader.QualitySettings.XDimension = mode;

                    // Optionally set a minimal XDimension value
                    if (setMinimal)
                    {
                        reader.QualitySettings.MinimalXDimension = 1;
                    }

                    // Perform recognition and count results
                    BarCodeResult[] results = reader.ReadBarCodes();
                    totalRecognized += results?.Length ?? 0;
                }
            }

            sw.Stop();
            double avgTime = sw.Elapsed.TotalMilliseconds / barcodeFiles.Count;

            // Output CSV line with aggregated results for this mode
            Console.WriteLine($"{name},{sw.Elapsed.TotalMilliseconds:F2},{avgTime:F2},{totalRecognized}");
        }

        // Clean up temporary files and folder
        try
        {
            foreach (string file in barcodeFiles)
            {
                if (File.Exists(file))
                {
                    File.Delete(file);
                }
            }
            Directory.Delete(tempFolder, true);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Cleanup warning: {ex.Message}");
        }
    }
}