// Title: ExportToXml Performance: File Path vs Stream Overload
// Description: Demonstrates measuring the execution time of Aspose.BarCode's ExportToXml method when saving to a file path versus a memory stream for a batch of barcode generators.
// Category-Description: This example belongs to the barcode generation and export performance category of Aspose.BarCode. It showcases the use of BarcodeGenerator and its ExportToXml overloads to serialize barcode settings to XML, a common requirement when persisting configurations for later reuse or integration. Developers often need to compare file‑based and stream‑based approaches to choose the most efficient method for large‑scale batch processing.
// Prompt: Compare performance of ExportToXml using file path versus stream overload for large barcode image batches.
// Tags: barcode generation, export, xml, performance, file path, stream, aspose.barcode, code128

using System;
using System.IO;
using System.Diagnostics;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Provides a performance comparison between the file‑path and stream overloads of <c>BarcodeGenerator.ExportToXml</c>.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a set of barcode generators, exports each to XML using both
    /// a file path and a memory stream, and reports the elapsed time for each approach.
    /// </summary>
    static void Main()
    {
        const int sampleCount = 5; // Number of barcode generators to create for the test batch

        // Create a unique temporary folder for file‑based XML exports
        string tempFolder = Path.Combine(Path.GetTempPath(), "ExportXmlBatch_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Prepare a list of barcode generators with distinct Code128 values
        List<BarcodeGenerator> generators = new List<BarcodeGenerator>();
        for (int i = 0; i < sampleCount; i++)
        {
            string codeText = $"CODE{i:D4}";
            var gen = new BarcodeGenerator(EncodeTypes.Code128, codeText);
            // Optional: set X‑dimension to control barcode module size
            gen.Parameters.Barcode.XDimension.Point = 2f;
            generators.Add(gen);
        }

        // Measure ExportToXml using the file‑path overload
        Stopwatch swFile = new Stopwatch();
        swFile.Start();
        for (int i = 0; i < generators.Count; i++)
        {
            string xmlPath = Path.Combine(tempFolder, $"gen{i}.xml");
            generators[i].ExportToXml(xmlPath);
        }
        swFile.Stop();

        // Measure ExportToXml using the stream overload (MemoryStream)
        Stopwatch swStream = new Stopwatch();
        swStream.Start();
        for (int i = 0; i < generators.Count; i++)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                generators[i].ExportToXml(ms);
                // Reset position if further processing of the stream is required
                ms.Position = 0;
            }
        }
        swStream.Stop();

        // Output the timing results to the console
        Console.WriteLine($"ExportToXml (file path) elapsed: {swFile.ElapsedMilliseconds} ms");
        Console.WriteLine($"ExportToXml (stream) elapsed: {swStream.ElapsedMilliseconds} ms");

        // Clean up: dispose all barcode generators
        foreach (var gen in generators)
        {
            gen.Dispose();
        }

        // Delete temporary files and folder; ignore any errors during cleanup
        try
        {
            Directory.Delete(tempFolder, true);
        }
        catch
        {
            // Suppress cleanup exceptions
        }
    }
}