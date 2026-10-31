// Title: Performance Comparison of ExportToXml: File Path vs Stream Overload
// Description: Demonstrates measuring the execution time of Aspose.BarCode's ExportToXml method when saving to a file versus writing to a memory stream for a batch of barcode images.
// Category-Description: This example belongs to the Aspose.BarCode export operations category, illustrating how to use the BarcodeGenerator class to generate barcodes and export their metadata to XML. It shows typical use cases such as batch processing, performance benchmarking, and choosing between file‑based and stream‑based APIs. Developers working with barcode generation and data interchange often need to decide the most efficient export method for large volumes.
// Prompt: Compare performance of ExportToXml using file path versus stream overload for large barcode image batches.
// Tags: barcode, export, xml, performance, file, stream, batch, aspose.barcode, code128, generation

using System;
using System.IO;
using System.Diagnostics;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates performance measurement of ExportToXml using file path versus stream overload for a batch of barcodes.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that creates a temporary folder, generates a batch of Code128 barcodes,
    /// exports each to XML via file and stream, and prints average execution times.
    /// </summary>
    static void Main()
    {
        // Prepare a unique temporary folder for the XML output files
        string tempFolder = Path.Combine(Path.GetTempPath(), "ExportXmlBatch_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        const int batchSize = 5; // Safe sample size for demonstration
        long totalFileTimeTicks = 0;
        long totalStreamTimeTicks = 0;

        // Process a batch of barcodes
        for (int i = 0; i < batchSize; i++)
        {
            // Create a barcode generator for Code128 with a unique value
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, $"Sample{i + 1}"))
            {
                // Export barcode metadata to an XML file and measure the elapsed time
                string filePath = Path.Combine(tempFolder, $"barcode_{i}_file.xml");
                var swFile = Stopwatch.StartNew();
                generator.ExportToXml(filePath);
                swFile.Stop();
                totalFileTimeTicks += swFile.ElapsedTicks;

                // Export barcode metadata to a memory stream and measure the elapsed time
                using (var ms = new MemoryStream())
                {
                    var swStream = Stopwatch.StartNew();
                    generator.ExportToXml(ms);
                    swStream.Stop();
                    totalStreamTimeTicks += swStream.ElapsedTicks;
                }
            }
        }

        // Calculate average execution time in milliseconds for each overload
        double avgFileMs = (totalFileTimeTicks * 1000.0) / Stopwatch.Frequency / batchSize;
        double avgStreamMs = (totalStreamTimeTicks * 1000.0) / Stopwatch.Frequency / batchSize;

        // Output the performance results
        Console.WriteLine($"Average ExportToXml using file path: {avgFileMs:F3} ms");
        Console.WriteLine($"Average ExportToXml using stream:    {avgStreamMs:F3} ms");
    }
}