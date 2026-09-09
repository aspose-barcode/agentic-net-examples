// Title: Benchmark recognition of stacked vs single linear barcodes
// Description: Demonstrates generating Code128 and DataBarStacked barcodes, then measuring recognition time for each type to assess scalability.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and recognition category. It shows how to use BarcodeGenerator to create linear and stacked barcodes, BarCodeReader to decode them, and Stopwatch to benchmark performance. Developers working on high‑volume scanning or performance testing can use these APIs to evaluate algorithm speed across different symbologies.
// Prompt: Benchmark recognition of stacked linear barcodes versus single barcodes to evaluate algorithm scalability.
// Tags: barcode, generation, recognition, benchmark, linear, stacked, code128, databarstacked, performance, aspose.barcode

using System;
using System.IO;
using System.Diagnostics;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates barcode generation and recognition benchmarking for single and stacked linear barcodes.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates barcodes, measures recognition times, outputs averages, and cleans up temporary files.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for generated barcode images
        string tempFolder = Path.Combine(Path.GetTempPath(), "Benchmark_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Prepare collections to hold file paths of generated barcodes
        List<string> singleBarcodes = new List<string>();
        List<string> stackedBarcodes = new List<string>();

        // -------------------------------------------------
        // Generate single linear barcodes (Code128)
        // -------------------------------------------------
        for (int i = 0; i < 5; i++)
        {
            string codeText = $"CODE128-{i:D4}";
            string filePath = Path.Combine(tempFolder, $"single_{i}.png");

            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
            {
                generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Black;
                generator.Parameters.BackColor = Aspose.Drawing.Color.White;
                generator.Save(filePath, BarCodeImageFormat.Png);
            }

            singleBarcodes.Add(filePath);
        }

        // -------------------------------------------------
        // Generate stacked linear barcodes (DataBarStacked)
        // -------------------------------------------------
        for (int i = 0; i < 5; i++)
        {
            // DataBarStacked expects numeric data; use a 14‑digit number
            string codeText = (12345678901234L + i).ToString();
            string filePath = Path.Combine(tempFolder, $"stacked_{i}.png");

            using (var generator = new BarcodeGenerator(EncodeTypes.DatabarStacked, codeText))
            {
                generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Black;
                generator.Parameters.BackColor = Aspose.Drawing.Color.White;
                generator.Save(filePath, BarCodeImageFormat.Png);
            }

            stackedBarcodes.Add(filePath);
        }

        // -------------------------------------------------
        // Benchmark recognition for single barcodes
        // -------------------------------------------------
        long singleTotalTicks = 0;
        int singleCount = 0;

        foreach (string file in singleBarcodes)
        {
            if (!File.Exists(file))
            {
                Console.WriteLine($"File not found: {file}");
                continue;
            }

            using (var reader = new BarCodeReader(file, DecodeType.AllSupportedTypes))
            {
                Stopwatch sw = Stopwatch.StartNew();
                var results = reader.ReadBarCodes();
                sw.Stop();

                singleTotalTicks += sw.ElapsedTicks;
                singleCount++;

                foreach (var result in results)
                {
                    Console.WriteLine($"Single: {Path.GetFileName(file)} => {result.CodeText}");
                }
            }
        }

        // -------------------------------------------------
        // Benchmark recognition for stacked barcodes
        // -------------------------------------------------
        long stackedTotalTicks = 0;
        int stackedCount = 0;

        foreach (string file in stackedBarcodes)
        {
            if (!File.Exists(file))
            {
                Console.WriteLine($"File not found: {file}");
                continue;
            }

            using (var reader = new BarCodeReader(file, DecodeType.AllSupportedTypes))
            {
                Stopwatch sw = Stopwatch.StartNew();
                var results = reader.ReadBarCodes();
                sw.Stop();

                stackedTotalTicks += sw.ElapsedTicks;
                stackedCount++;

                foreach (var result in results)
                {
                    Console.WriteLine($"Stacked: {Path.GetFileName(file)} => {result.CodeText}");
                }
            }
        }

        // -------------------------------------------------
        // Output benchmark summary
        // -------------------------------------------------
        if (singleCount > 0)
        {
            double singleAvgMs = (singleTotalTicks * 1000.0) / Stopwatch.Frequency / singleCount;
            Console.WriteLine($"Average recognition time for single linear barcodes: {singleAvgMs:F3} ms");
        }

        if (stackedCount > 0)
        {
            double stackedAvgMs = (stackedTotalTicks * 1000.0) / Stopwatch.Frequency / stackedCount;
            Console.WriteLine($"Average recognition time for stacked linear barcodes: {stackedAvgMs:F3} ms");
        }

        // -------------------------------------------------
        // Clean up temporary files and folder
        // -------------------------------------------------
        try
        {
            foreach (var file in singleBarcodes) File.Delete(file);
            foreach (var file in stackedBarcodes) File.Delete(file);
            Directory.Delete(tempFolder, true);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Cleanup warning: {ex.Message}");
        }
    }
}