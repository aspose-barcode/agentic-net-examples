using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates memory usage measurement for barcode generation using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Generates a barcode of the specified type and measures the memory usage for each iteration.
    /// </summary>
    /// <param name="encodeType">The barcode symbology to generate.</param>
    /// <param name="codeText">The text to encode in the barcode.</param>
    /// <param name="iterations">Number of times to generate the barcode for measurement.</param>
    /// <returns>A list containing memory differences (in bytes) for each iteration.</returns>
    static List<long> MeasureMemoryUsage(BaseEncodeType encodeType, string codeText, int iterations)
    {
        var memoryDiffs = new List<long>();

        for (int i = 0; i < iterations; i++)
        {
            // Ensure a clean memory state before measurement.
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();

            // Record memory usage before barcode generation.
            long before = GC.GetTotalMemory(true);

            // Generate the barcode and write it to a memory stream.
            using (var generator = new BarcodeGenerator(encodeType, codeText))
            {
                // Set a reasonable resolution for the generated image.
                generator.Parameters.Resolution = 300f;

                using (var ms = new MemoryStream())
                {
                    generator.Save(ms, BarCodeImageFormat.Png);
                }
            }

            // Record memory usage after barcode generation.
            long after = GC.GetTotalMemory(true);

            // Store the difference for this iteration.
            memoryDiffs.Add(after - before);
        }

        return memoryDiffs;
    }

    /// <summary>
    /// Entry point of the program. Executes memory usage benchmarks for MaxiCode and DataMatrix barcodes.
    /// </summary>
    static void Main()
    {
        const int sampleCount = 5;

        // Benchmark MaxiCode generation.
        var maxiCodeDiffs = MeasureMemoryUsage(EncodeTypes.MaxiCode, "Test MaxiCode", sampleCount);
        Console.WriteLine("MaxiCode memory usage (bytes) per generation:");
        foreach (var diff in maxiCodeDiffs)
        {
            Console.WriteLine(diff);
        }

        // Benchmark DataMatrix generation.
        var dataMatrixDiffs = MeasureMemoryUsage(EncodeTypes.DataMatrix, "Test DataMatrix", sampleCount);
        Console.WriteLine("DataMatrix memory usage (bytes) per generation:");
        foreach (var diff in dataMatrixDiffs)
        {
            Console.WriteLine(diff);
        }

        // Compute and display average memory usage for each barcode type.
        double avgMaxi = 0, avgDataMatrix = 0;
        foreach (var d in maxiCodeDiffs) avgMaxi += d;
        foreach (var d in dataMatrixDiffs) avgDataMatrix += d;
        avgMaxi /= sampleCount;
        avgDataMatrix /= sampleCount;

        Console.WriteLine($"Average MaxiCode memory: {avgMaxi:F0} bytes");
        Console.WriteLine($"Average DataMatrix memory: {avgDataMatrix:F0} bytes");
    }
}