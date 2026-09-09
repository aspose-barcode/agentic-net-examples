// Title: Performance Comparison of ExportToXml: File Path vs Stream Overloads
// Description: Demonstrates measuring the execution time of Aspose.BarCode's ExportToXml method when saving barcode data to a file versus writing to a memory stream for a batch of large barcode images.
// Category-Description: This example belongs to the Aspose.BarCode export operations category, illustrating how to use the BarcodeGenerator class to generate barcodes and export their metadata to XML. It showcases typical use cases such as bulk processing, performance benchmarking, and choosing between file‑based and stream‑based APIs. Developers working with barcode generation and serialization often need to evaluate these overloads to optimize I/O performance.
// Prompt: Compare performance of ExportToXml using file path versus stream overload for large barcode image batches.
// Tags: barcode, export, xml, performance, file, stream, aspose.barcode, code128, batch processing

using System;
using System.IO;
using System.Diagnostics;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates performance measurement of ExportToXml using file path and stream overloads for a batch of barcodes.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates sample barcodes, exports them to XML via file and stream, and reports timing.
    /// </summary>
    static void Main()
    {
        const int sampleCount = 5;

        // Create a temporary folder for XML files
        string tempFolder = Path.Combine(Path.GetTempPath(), "ExportXmlBatch_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Prepare a list of barcode generators with sample data
        List<BarcodeGenerator> generators = new List<BarcodeGenerator>();
        for (int i = 1; i <= sampleCount; i++)
        {
            var gen = new BarcodeGenerator(EncodeTypes.Code128, $"Sample{i}");
            gen.Parameters.Barcode.XDimension.Pixels = 2f;
            generators.Add(gen);
        }

        // Export each barcode to an XML file and measure the elapsed time
        Stopwatch swFile = Stopwatch.StartNew();
        for (int i = 0; i < generators.Count; i++)
        {
            string filePath = Path.Combine(tempFolder, $"gen{i}.xml");
            generators[i].ExportToXml(filePath);
        }
        swFile.Stop();

        // Export each barcode to a memory stream and measure the elapsed time
        Stopwatch swStream = Stopwatch.StartNew();
        foreach (var gen in generators)
        {
            using (var ms = new MemoryStream())
            {
                gen.ExportToXml(ms);
                ms.Position = 0; // Reset position to read from the beginning
                using (var sr = new StreamReader(ms, leaveOpen: true))
                {
                    string _ = sr.ReadToEnd(); // Ensure the stream is fully processed
                }
            }
        }
        swStream.Stop();

        // Output the timing results
        Console.WriteLine($"Export to XML files time: {swFile.ElapsedMilliseconds} ms");
        Console.WriteLine($"Export to XML streams time: {swStream.ElapsedMilliseconds} ms");

        // Cleanup temporary files and directory
        try
        {
            foreach (var file in Directory.GetFiles(tempFolder))
            {
                File.Delete(file);
            }
            Directory.Delete(tempFolder);
        }
        catch
        {
            // Ignore cleanup errors
        }
    }
}