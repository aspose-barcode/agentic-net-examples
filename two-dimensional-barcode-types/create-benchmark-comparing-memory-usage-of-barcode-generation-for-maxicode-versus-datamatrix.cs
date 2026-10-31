// Title: Benchmark memory usage of MaxiCode vs DataMatrix barcode generation
// Description: Demonstrates how to measure and compare the memory consumption of generating MaxiCode and DataMatrix barcodes using Aspose.BarCode.
// Category-Description: This example belongs to the Aspose.BarCode performance benchmarking category, illustrating the use of BarcodeGenerator, EncodeTypes, and memory measurement via GC. Developers often need to evaluate resource usage when generating different symbologies for high‑throughput or memory‑constrained applications. The snippet shows typical patterns for creating barcodes, saving them to streams, and calculating average memory per generation.
// Prompt: Create a benchmark comparing memory usage of barcode generation for MaxiCode versus DataMatrix.
// Tags: barcode, memory benchmark, maxicode, datamatrix, performance, aspose.barcode, generation, gc

using System;
using System.Diagnostics;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing.Imaging;

/// <summary>
/// Provides a simple benchmark to compare memory usage when generating MaxiCode and DataMatrix barcodes.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Executes the benchmark and prints average memory usage for each symbology.
    /// </summary>
    static void Main()
    {
        const int sampleCount = 5; // Number of iterations per symbology

        // Benchmark memory for MaxiCode generation
        long maxiCodeMemory = BenchmarkMemory(EncodeTypes.MaxiCode, "MaxiCodeSample", sampleCount);
        // Benchmark memory for DataMatrix generation
        long dataMatrixMemory = BenchmarkMemory(EncodeTypes.DataMatrix, "DataMatrixSample", sampleCount);

        // Output average memory per generation (bytes)
        Console.WriteLine($"Average memory used per MaxiCode generation: {maxiCodeMemory / sampleCount} bytes");
        Console.WriteLine($"Average memory used per DataMatrix generation: {dataMatrixMemory / sampleCount} bytes");
    }

    /// <summary>
    /// Runs the memory benchmark for a given barcode type over a specified number of iterations.
    /// </summary>
    /// <param name="encodeType">The barcode symbology to generate.</param>
    /// <param name="codeText">The text to encode in the barcode.</param>
    /// <param name="iterations">How many times to repeat the generation.</param>
    /// <returns>Total memory consumed across all iterations.</returns>
    static long BenchmarkMemory(BaseEncodeType encodeType, string codeText, int iterations)
    {
        long totalMemory = 0;
        for (int i = 0; i < iterations; i++)
        {
            // Measure memory for a single generation and accumulate
            totalMemory += MeasureGenerationMemory(encodeType, codeText);
        }
        return totalMemory;
    }

    /// <summary>
    /// Measures the memory allocated during a single barcode generation.
    /// </summary>
    /// <param name="encodeType">The barcode symbology to generate.</param>
    /// <param name="codeText">The text to encode.</param>
    /// <returns>Memory used (in bytes) for this generation.</returns>
    static long MeasureGenerationMemory(BaseEncodeType encodeType, string codeText)
    {
        // Force a clean GC state before measurement
        GC.Collect();
        GC.WaitForPendingFinalizers();
        GC.Collect();

        long before = GC.GetTotalMemory(true); // Memory before generation

        using (var generator = new BarcodeGenerator(encodeType, codeText))
        {
            // Set a modest module size for the barcode
            generator.Parameters.Barcode.XDimension.Pixels = 5f;

            using (var stream = new MemoryStream())
            {
                // Save the generated barcode to a memory stream in PNG format
                generator.Save(stream, BarCodeImageFormat.Png);
            }
        }

        long after = GC.GetTotalMemory(true); // Memory after generation
        return after - before;
    }
}