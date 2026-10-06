// Title: Benchmark memory usage of MaxiCode vs DataMatrix barcode generation
// Description: Demonstrates how to measure and compare the memory consumption of generating MaxiCode and DataMatrix barcodes using Aspose.BarCode. The example runs multiple iterations and reports average memory increase per generation.
// Category-Description: This example belongs to the Aspose.BarCode performance benchmarking category, illustrating how to use the BarcodeGenerator class with EncodeTypes.MaxiCode and EncodeTypes.DataMatrix. Developers often need to evaluate memory and speed characteristics when generating barcodes in high‑throughput or resource‑constrained environments; this snippet shows typical setup, GC handling, and memory measurement techniques.
// Prompt: Create a benchmark comparing memory usage of barcode generation for MaxiCode versus DataMatrix.
// Tags: barcode, memory benchmark, maximcode, datamatrix, aspnet, aspose.barcode, performance, generation

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates benchmarking memory usage for MaxiCode and DataMatrix barcode generation using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Executes the benchmark and prints average memory increase for each symbology.
    /// </summary>
    static void Main()
    {
        const int iterations = 5;
        Console.WriteLine("Benchmarking barcode generation memory usage...");

        // Run benchmarks for both symbologies
        double avgMaxi = BenchmarkMaxiCode(iterations);
        double avgDataMatrix = BenchmarkDataMatrix(iterations);

        // Output results
        Console.WriteLine($"Average memory increase per MaxiCode generation: {avgMaxi:N0} bytes");
        Console.WriteLine($"Average memory increase per DataMatrix generation: {avgDataMatrix:N0} bytes");
    }

    /// <summary>
    /// Measures average memory increase when generating MaxiCode barcodes.
    /// </summary>
    /// <param name="count">Number of iterations to perform.</param>
    /// <returns>Average memory increase in bytes.</returns>
    static double BenchmarkMaxiCode(int count)
    {
        long totalDiff = 0;
        for (int i = 0; i < count; i++)
        {
            // Ensure a clean memory state before measurement
            GC.Collect();
            GC.WaitForPendingFinalizers();
            long before = GC.GetTotalMemory(true);

            // Generate a MaxiCode barcode and write it to a memory stream
            using (var generator = new BarcodeGenerator(EncodeTypes.MaxiCode, "Sample"))
            {
                generator.Parameters.Barcode.XDimension.Pixels = 5f;
                using (var ms = new MemoryStream())
                {
                    generator.Save(ms, BarCodeImageFormat.Png);
                }
            }

            // Capture memory usage after generation
            GC.Collect();
            GC.WaitForPendingFinalizers();
            long after = GC.GetTotalMemory(true);
            totalDiff += (after - before);
        }
        return (double)totalDiff / count;
    }

    /// <summary>
    /// Measures average memory increase when generating DataMatrix barcodes.
    /// </summary>
    /// <param name="count">Number of iterations to perform.</param>
    /// <returns>Average memory increase in bytes.</returns>
    static double BenchmarkDataMatrix(int count)
    {
        long totalDiff = 0;
        for (int i = 0; i < count; i++)
        {
            // Ensure a clean memory state before measurement
            GC.Collect();
            GC.WaitForPendingFinalizers();
            long before = GC.GetTotalMemory(true);

            // Generate a DataMatrix barcode with specific version and ECC settings
            using (var generator = new BarcodeGenerator(EncodeTypes.DataMatrix, "Sample"))
            {
                generator.Parameters.Barcode.XDimension.Pixels = 5f;
                generator.Parameters.Barcode.DataMatrix.Version = DataMatrixVersion.ECC200_32x32;
                generator.Parameters.Barcode.DataMatrix.EccType = DataMatrixEccType.Ecc200;
                using (var ms = new MemoryStream())
                {
                    generator.Save(ms, BarCodeImageFormat.Png);
                }
            }

            // Capture memory usage after generation
            GC.Collect();
            GC.WaitForPendingFinalizers();
            long after = GC.GetTotalMemory(true);
            totalDiff += (after - before);
        }
        return (double)totalDiff / count;
    }
}