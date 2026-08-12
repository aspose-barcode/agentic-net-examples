// Title: Memory Usage Benchmark for MaxiCode vs DataMatrix Barcode Generation
// Description: Demonstrates how to measure and compare the memory allocated when generating MaxiCode and DataMatrix barcodes using Aspose.BarCode.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation benchmarks category. It shows how to use the BarcodeGenerator class to create barcodes, save them to files, and evaluate memory consumption with .NET's GC. Developers often need to assess performance and resource usage when generating large numbers of barcodes for high‑throughput applications.
// Prompt: Create a benchmark comparing memory usage of barcode generation for MaxiCode versus DataMatrix.
// Tags: barcode, memory benchmark, maximcode, datamatrix, aspose.barcode, generation, performance

using System;
using System.Diagnostics;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Provides a simple benchmark that measures memory usage for generating MaxiCode and DataMatrix barcodes.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that runs the memory usage benchmark and outputs results to the console.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for the benchmark files
        string tempRoot = Path.Combine(Path.GetTempPath(), "BarcodeBenchmark_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempRoot);

        // Number of barcodes to generate for each symbology (kept small for quick execution)
        const int sampleCount = 5;

        // Benchmark MaxiCode memory usage
        Console.WriteLine("Benchmark: Memory usage for MaxiCode generation");
        long maxiBefore = GC.GetTotalMemory(true);
        GenerateBarcodes(EncodeTypes.MaxiCode, sampleCount, Path.Combine(tempRoot, "MaxiCode"));
        long maxiAfter = GC.GetTotalMemory(true);
        Console.WriteLine($"  Memory allocated: {FormatBytes(maxiAfter - maxiBefore)}");

        // Benchmark DataMatrix memory usage
        Console.WriteLine("Benchmark: Memory usage for DataMatrix generation");
        long dmBefore = GC.GetTotalMemory(true);
        GenerateBarcodes(EncodeTypes.DataMatrix, sampleCount, Path.Combine(tempRoot, "DataMatrix"));
        long dmAfter = GC.GetTotalMemory(true);
        Console.WriteLine($"  Memory allocated: {FormatBytes(dmAfter - dmBefore)}");

        // Clean up generated files (optional)
        try
        {
            Directory.Delete(tempRoot, true);
        }
        catch
        {
            // If deletion fails, ignore – the OS will clean up temp files eventually.
        }
    }

    // Generates a set of barcodes of the specified type and saves them into a folder.
    private static void GenerateBarcodes(BaseEncodeType encodeType, int count, string outputFolder)
    {
        Directory.CreateDirectory(outputFolder);

        for (int i = 0; i < count; i++)
        {
            string codeText = $"Sample{i + 1}";
            string filePath = Path.Combine(outputFolder, $"barcode_{i + 1}.png");

            // BarcodeGenerator implements IDisposable, so we wrap it in a using block.
            using (var generator = new BarcodeGenerator(encodeType, codeText))
            {
                // For DataMatrix we can optionally set a version; here we rely on defaults.
                // For MaxiCode we can leave parameters at defaults as well.
                generator.Save(filePath);
            }
        }
    }

    // Helper to format byte sizes into a readable string.
    private static string FormatBytes(long bytes)
    {
        const long KB = 1024;
        const long MB = KB * 1024;
        const long GB = MB * 1024;

        if (bytes >= GB) return $"{bytes / (double)GB:F2} GB";
        if (bytes >= MB) return $"{bytes / (double)MB:F2} MB";
        if (bytes >= KB) return $"{bytes / (double)KB:F2} KB";
        return $"{bytes} B";
    }
}