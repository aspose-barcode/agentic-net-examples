// Title: Benchmark memory usage of MaxiCode vs DataMatrix barcode generation
// Description: Demonstrates how to measure and compare the memory consumption when generating MaxiCode (Mode 2) and DataMatrix barcodes using Aspose.BarCode.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation performance category, showcasing the use of ComplexBarcodeGenerator for MaxiCode and BarcodeGenerator for DataMatrix. Developers often need to evaluate memory and speed characteristics of different symbologies when processing large batches, and this snippet provides a repeatable benchmark pattern for such assessments.
// Prompt: Create a benchmark comparing memory usage of barcode generation for MaxiCode versus DataMatrix.
// Tags: barcode, memory, benchmark, maximcode, datamatrix, aspnet, aspnetcore, aspose.barcode, generation

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.ComplexBarcode;

/// <summary>
/// Provides a simple benchmark that measures memory usage for generating MaxiCode (Mode 2) and DataMatrix barcodes.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the benchmark application.
    /// </summary>
    static void Main()
    {
        const int sampleCount = 5; // Number of samples for each symbology

        // ------------------------------------------------------------
        // Prepare sample data for MaxiCode (Mode 2)
        // ------------------------------------------------------------
        var maxiCodeSamples = new List<MaxiCodeCodetextMode2>();
        for (int i = 0; i < sampleCount; i++)
        {
            var maxi = new MaxiCodeCodetextMode2
            {
                PostalCode = "524032140",
                CountryCode = 056,
                ServiceCategory = 999
            };
            var secondMessage = new MaxiCodeStandardSecondMessage
            {
                Message = $"Message {i + 1}"
            };
            maxi.SecondMessage = secondMessage;
            maxiCodeSamples.Add(maxi);
        }

        // ------------------------------------------------------------
        // Prepare sample data for DataMatrix
        // ------------------------------------------------------------
        var dataMatrixTexts = new List<string>();
        for (int i = 0; i < sampleCount; i++)
        {
            dataMatrixTexts.Add($"DataMatrix Sample {i + 1}");
        }

        // ------------------------------------------------------------
        // Benchmark MaxiCode generation
        // ------------------------------------------------------------
        Console.WriteLine("MaxiCode generation memory usage (bytes):");
        for (int i = 0; i < sampleCount; i++)
        {
            // Force garbage collection before measurement
            GC.Collect();
            GC.WaitForPendingFinalizers();
            long before = GC.GetTotalMemory(true);

            // Generate MaxiCode barcode and write to a memory stream
            using (var generator = new ComplexBarcodeGenerator(maxiCodeSamples[i]))
            {
                // Explicitly set Mode 2 (already implied by codetext type)
                generator.Parameters.Barcode.MaxiCode.Mode = MaxiCodeMode.Mode2;

                using (var ms = new MemoryStream())
                {
                    generator.Save(ms, BarCodeImageFormat.Png);
                }
            }

            // Force garbage collection after generation
            GC.Collect();
            GC.WaitForPendingFinalizers();
            long after = GC.GetTotalMemory(true);
            Console.WriteLine($"  Sample {i + 1}: {after - before}");
        }

        // ------------------------------------------------------------
        // Benchmark DataMatrix generation
        // ------------------------------------------------------------
        Console.WriteLine("\nDataMatrix generation memory usage (bytes):");
        for (int i = 0; i < sampleCount; i++)
        {
            // Force garbage collection before measurement
            GC.Collect();
            GC.WaitForPendingFinalizers();
            long before = GC.GetTotalMemory(true);

            // Generate DataMatrix barcode and write to a memory stream
            using (var generator = new BarcodeGenerator(EncodeTypes.DataMatrix, dataMatrixTexts[i]))
            {
                // Use a common square version for consistency
                generator.Parameters.Barcode.DataMatrix.DataMatrixVersion = DataMatrixVersion.ECC200_20x20;
                generator.Parameters.AutoSizeMode = AutoSizeMode.Interpolation;

                using (var ms = new MemoryStream())
                {
                    generator.Save(ms, BarCodeImageFormat.Png);
                }
            }

            // Force garbage collection after generation
            GC.Collect();
            GC.WaitForPendingFinalizers();
            long after = GC.GetTotalMemory(true);
            Console.WriteLine($"  Sample {i + 1}: {after - before}");
        }

        Console.WriteLine("\nBenchmark completed.");
    }
}