// Title: Compare ExportToXml performance using file path vs stream for large barcode batches
// Description: Demonstrates measuring the execution time of Aspose.BarCode's ExportToXml method when saving to a file path versus a memory stream, useful for evaluating performance in bulk barcode processing.
// Category-Description: This example belongs to the Aspose.BarCode export operations category, showcasing how to generate barcodes and export their metadata to XML using the BarcodeGenerator class. Typical use cases include batch processing of barcode data for inventory, shipping, or reporting systems where developers need to choose the most efficient output method. The snippet highlights performance testing with Stopwatch and common API classes such as BarcodeGenerator, EncodeTypes, and ExportToXml.
// Prompt: Compare performance of ExportToXml using file path versus stream overload for large barcode image batches.
// Tags: barcode, export, xml, performance, file, stream, batch, code128, aspose.barcode

using System;
using System.IO;
using System.Diagnostics;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Example program that benchmarks ExportToXml using a file path versus a stream overload.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates a small batch of Code128 barcodes, exports each to XML via file and stream,
    /// measures the elapsed time for each approach, and outputs the results.
    /// </summary>
    static void Main()
    {
        // Prepare a temporary folder for file‑based XML exports
        string tempFolder = Path.Combine(Path.GetTempPath(), "ExportXmlBatch_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Create sample barcode data (small batch for safe execution)
        List<string> codeTexts = new List<string>();
        for (int i = 0; i < 5; i++)
        {
            codeTexts.Add("CODE" + i.ToString("D4"));
        }

        // ------------------------------------------------------------
        // Measure ExportToXml using the file path overload
        // ------------------------------------------------------------
        Stopwatch swFile = new Stopwatch();
        swFile.Start();
        for (int i = 0; i < codeTexts.Count; i++)
        {
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeTexts[i]))
            {
                // Optional parameter: set barcode color
                generator.Parameters.Barcode.BarColor = Color.Black;

                string xmlPath = Path.Combine(tempFolder, $"barcode_{i}.xml");
                generator.ExportToXml(xmlPath);
            }
        }
        swFile.Stop();

        // ------------------------------------------------------------
        // Measure ExportToXml using the stream overload
        // ------------------------------------------------------------
        Stopwatch swStream = new Stopwatch();
        swStream.Start();
        for (int i = 0; i < codeTexts.Count; i++)
        {
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeTexts[i]))
            {
                generator.Parameters.Barcode.BarColor = Color.Black;

                using (var ms = new MemoryStream())
                {
                    generator.ExportToXml(ms);
                    // Reset position if further processing is needed
                    ms.Position = 0;

                    // Example: read XML string (optional, not used for timing)
                    using (var sr = new StreamReader(ms, leaveOpen: true))
                    {
                        string xmlContent = sr.ReadToEnd();
                        // Discard xmlContent; it's only read to simulate typical usage
                    }
                }
            }
        }
        swStream.Stop();

        // Output the timing results
        Console.WriteLine($"ExportToXml (file path) elapsed: {swFile.ElapsedMilliseconds} ms");
        Console.WriteLine($"ExportToXml (stream)     elapsed: {swStream.ElapsedMilliseconds} ms");

        // Clean up temporary files
        try
        {
            Directory.Delete(tempFolder, true);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Cleanup failed: {ex.Message}");
        }
    }
}