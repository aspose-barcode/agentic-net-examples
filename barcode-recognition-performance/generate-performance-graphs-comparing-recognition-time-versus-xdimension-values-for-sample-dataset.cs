// Title: Barcode recognition performance vs XDimension
// Description: Demonstrates measuring barcode recognition time across different XDimension values and visualizing results.
// Category-Description: This example belongs to the Aspose.BarCode performance testing category, illustrating how to use BarcodeGenerator, BarCodeReader, and related parameters to evaluate recognition speed. Developers often need to benchmark barcode settings such as XDimension to optimize scanning performance in high‑throughput applications. The snippet shows generating Code128 barcodes, timing recognition, and presenting results in a table and ASCII graph.
// Prompt: Generate performance graphs comparing recognition time versus XDimension values for a sample dataset.
// Tags: barcode, performance, xdimension, code128, recognition, aspose.barcode, ascii-graph

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Example program that measures barcode recognition time for different XDimension values
/// and displays the results as a table and an ASCII bar graph.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates barcodes with varying XDimension,
    /// measures recognition latency, and outputs performance data.
    /// </summary>
    static void Main()
    {
        // Sample barcode text (same for all tests)
        const string sampleText = "1234567890";

        // XDimension values to test (in points)
        float[] xDimensions = new float[] { 1f, 2f, 3f, 4f, 5f };

        // Number of recognition repetitions for averaging
        const int repetitions = 5;

        // Store average recognition time (ms) for each XDimension
        var results = new List<(float XDim, double AvgTime)>();

        // Iterate over each XDimension value
        foreach (float xDim in xDimensions)
        {
            // Generate barcode with specific XDimension
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, sampleText))
            {
                // Disable auto‑sizing to apply custom XDimension
                generator.Parameters.AutoSizeMode = AutoSizeMode.None;
                generator.Parameters.Barcode.XDimension.Point = xDim;

                // Render barcode to bitmap
                using (Bitmap bitmap = generator.GenerateBarCodeImage())
                {
                    // Warm‑up read (optional, to avoid first‑run overhead)
                    using (var warmReader = new BarCodeReader(bitmap, DecodeType.Code128))
                    {
                        warmReader.ReadBarCodes();
                    }

                    // Measure recognition time over several repetitions
                    double totalMs = 0;
                    for (int i = 0; i < repetitions; i++)
                    {
                        using (var reader = new BarCodeReader(bitmap, DecodeType.Code128))
                        {
                            var sw = Stopwatch.StartNew();
                            var detected = reader.ReadBarCodes();
                            sw.Stop();

                            totalMs += sw.Elapsed.TotalMilliseconds;

                            // Simple validation to ensure detection succeeded
                            if (detected.Length == 0 || string.IsNullOrEmpty(detected[0].CodeText))
                            {
                                Console.WriteLine($"Warning: No barcode detected at XDimension {xDim} on iteration {i + 1}.");
                            }
                        }
                    }

                    // Compute average recognition time for current XDimension
                    double avgMs = totalMs / repetitions;
                    results.Add((xDim, avgMs));
                }
            }
        }

        // Output results as a formatted table
        Console.WriteLine("XDimension (pt) | Avg Recognition Time (ms)");
        Console.WriteLine("----------------|--------------------------");
        foreach (var r in results)
        {
            Console.WriteLine($"{r.XDim,15} | {r.AvgTime,24:F2}");
        }

        // Generate a simple ASCII bar graph to visualize performance
        Console.WriteLine();
        Console.WriteLine("Performance Graph (higher bar = longer time)");

        // Determine the maximum average time to scale bars proportionally
        double maxTime = 0;
        foreach (var r in results)
        {
            if (r.AvgTime > maxTime) maxTime = r.AvgTime;
        }

        // Scale bars to a maximum width of 50 characters
        const int maxBarWidth = 50;
        foreach (var r in results)
        {
            int barLength = maxTime > 0 ? (int)Math.Round(r.AvgTime / maxTime * maxBarWidth) : 0;
            string bar = new string('*', barLength);
            Console.WriteLine($"{r.XDim,5} pt | {bar}");
        }
    }
}