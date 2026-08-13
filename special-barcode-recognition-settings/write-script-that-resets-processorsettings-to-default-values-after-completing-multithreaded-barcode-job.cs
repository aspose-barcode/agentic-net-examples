// Title: Multithreaded barcode generation, reading, and ProcessorSettings reset
// Description: Demonstrates generating Code128 barcodes in parallel, reading them, and restoring Aspose.BarCode ProcessorSettings to default values after the job.
// Category-Description: This example belongs to the Aspose.BarCode multithreading and performance tuning category. It showcases the use of BarCodeReader.ProcessorSettings to control CPU core utilization, BarcodeGenerator for creating barcodes, and BarCodeReader for decoding. Developers often need to maximize throughput for bulk barcode processing and then clean up settings to avoid side effects in subsequent operations.
// Prompt: Write a script that resets ProcessorSettings to default values after completing a multithreaded barcode job.
// Tags: code128, multithreading, png, processorsettings, barcodegenerator, barcodereader, aspose.barcode

using System;
using System.IO;
using System.Threading.Tasks;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates multithreaded barcode generation and reading, then resets processor settings.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Configures ProcessorSettings for parallel execution, runs barcode tasks, and restores defaults.
    /// </summary>
    static void Main(string[] args)
    {
        // Enable maximum multithreaded performance for the barcode job
        BarCodeReader.ProcessorSettings.UseAllCores = true;
        // Optionally limit cores (example: half of the available cores)
        BarCodeReader.ProcessorSettings.UseOnlyThisCoresCount = Math.Max(1, Environment.ProcessorCount / 2);

        const int jobCount = 5; // safe sample size
        Task[] tasks = new Task[jobCount];

        for (int i = 0; i < jobCount; i++)
        {
            int index = i; // capture loop variable for closure
            tasks[i] = Task.Run(() =>
            {
                // Generate a simple Code128 barcode in memory
                using (var generator = new BarcodeGenerator(EncodeTypes.Code128, $"CODE{index:D3}"))
                {
                    using (var ms = new MemoryStream())
                    {
                        // Save barcode image as PNG to the memory stream
                        generator.Save(ms, BarCodeImageFormat.Png);
                        ms.Position = 0; // rewind stream for reading

                        // Read the barcode back using a BarCodeReader
                        using (var reader = new BarCodeReader(ms, DecodeType.Code128))
                        {
                            foreach (var result in reader.ReadBarCodes())
                            {
                                Console.WriteLine($"Task {index}: Detected CodeText = {result.CodeText}");
                            }
                        }
                    }
                }
            });
        }

        // Wait for all barcode tasks to complete
        Task.WaitAll(tasks);

        // Reset ProcessorSettings to their default values
        BarCodeReader.ProcessorSettings.UseAllCores = false;
        BarCodeReader.ProcessorSettings.UseOnlyThisCoresCount = 0;

        Console.WriteLine("ProcessorSettings have been reset to default values.");
    }
}