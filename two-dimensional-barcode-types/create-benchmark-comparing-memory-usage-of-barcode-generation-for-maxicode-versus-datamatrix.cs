// Title: Memory Benchmark for MaxiCode vs DataMatrix Barcode Generation
// Description: Demonstrates how to measure and compare the memory consumption of generating multiple MaxiCode and DataMatrix barcodes using Aspose.BarCode.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation performance category. It shows how to use BarcodeGenerator, EncodeTypes, and BarCodeImageFormat to create barcodes, and how to employ .NET GC and GetTotalMemory for memory profiling. Developers looking to benchmark barcode creation, optimize resource usage, or compare different symbologies can reference this pattern.
/// Prompt: Create a benchmark comparing memory usage of barcode generation for MaxiCode versus DataMatrix.
/// Tags: barcode, memory benchmark, maxicode, datamatrix, aspose.barcode, performance, generation, c#

using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Provides a simple benchmark that compares the memory usage of generating
/// MaxiCode and DataMatrix barcodes using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the benchmark application.
    /// Measures memory consumption for a set number of generated barcodes
    /// for each specified symbology and outputs the results.
    /// </summary>
    /// <param name="args">Command‑line arguments (not used).</param>
    static void Main(string[] args)
    {
        const int sampleCount = 5; // Number of barcodes to generate per symbology

        // Measure memory for MaxiCode generation
        long maxiCodeMemory = MeasureMemory(() =>
            GenerateBarcodes(EncodeTypes.MaxiCode, "Sample MaxiCode Text", sampleCount));

        // Measure memory for DataMatrix generation
        long dataMatrixMemory = MeasureMemory(() =>
            GenerateBarcodes(EncodeTypes.DataMatrix, "Sample DataMatrix Text", sampleCount));

        // Output the measured memory usage
        Console.WriteLine($"Memory used for generating {sampleCount} MaxiCode barcodes: {maxiCodeMemory} bytes");
        Console.WriteLine($"Memory used for generating {sampleCount} DataMatrix barcodes: {dataMatrixMemory} bytes");
    }

    /// <summary>
    /// Executes an action while measuring the difference in total managed memory before and after its execution.
    /// </summary>
    /// <param name="action">The code block whose memory usage is to be measured.</param>
    /// <returns>The amount of memory (in bytes) allocated during the action.</returns>
    static long MeasureMemory(Action action)
    {
        // Force a full garbage collection to get a clean baseline
        GC.Collect();
        GC.WaitForPendingFinalizers();
        GC.Collect();

        long before = GC.GetTotalMemory(true); // Memory before action
        action();                               // Execute the benchmarked code
        long after = GC.GetTotalMemory(true);  // Memory after action

        return after - before; // Return the delta
    }

    /// <summary>
    /// Generates a specified number of barcodes of a given type and stores them in memory streams.
    /// The streams are retained to ensure the memory allocation is accounted for.
    /// </summary>
    /// <param name="encodeType">The barcode symbology to generate.</param>
    /// <param name="codeText">The text/content to encode in each barcode.</param>
    /// <param name="count">How many barcodes to generate.</param>
    static void GenerateBarcodes(BaseEncodeType encodeType, string codeText, int count)
    {
        var streams = new List<MemoryStream>(); // Holds generated images to prevent GC optimization

        for (int i = 0; i < count; i++)
        {
            // Create a new generator for each barcode instance
            using (var generator = new BarcodeGenerator(encodeType, codeText))
            {
                // Set a modest XDimension to keep image size reasonable
                generator.Parameters.Barcode.XDimension.Point = 2f;

                // For DataMatrix, optionally set a specific version
                if (encodeType == EncodeTypes.DataMatrix)
                {
                    generator.Parameters.Barcode.DataMatrix.Version = DataMatrixVersion.ECC200_32x32;
                }

                // Save the generated barcode to a memory stream in PNG format
                using (var ms = new MemoryStream())
                {
                    generator.Save(ms, BarCodeImageFormat.Png);
                    streams.Add(ms); // Retain the stream to ensure memory allocation is counted
                }
            }
        }

        // Defensive check: inform if no barcodes were generated (should never happen with valid input)
        if (streams.Count == 0)
        {
            Console.WriteLine("No barcodes generated.");
        }
    }
}