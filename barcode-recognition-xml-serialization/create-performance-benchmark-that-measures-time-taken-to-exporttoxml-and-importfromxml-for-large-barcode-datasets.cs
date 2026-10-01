// Title: Performance Benchmark for ExportToXml and ImportFromXml with Aspose.BarCode
// Description: Demonstrates measuring execution time of exporting barcode configurations to XML and importing them back for a set of Code128 barcodes.
// Category-Description: This example belongs to the Aspose.BarCode serialization category, showcasing how to use the BarcodeGenerator class to serialize and deserialize barcode settings via XML. Typical use cases include batch processing, configuration persistence, and performance testing of export/import operations. Developers often need to benchmark these APIs to ensure scalability for large barcode datasets.
// Prompt: Create a performance benchmark that measures time taken to ExportToXml and ImportFromXml for large barcode datasets.
// Tags: barcode symbology, export, import, xml, performance, benchmark, aspose.barcode, code128, generation, recognition

using System;
using System.IO;
using System.Diagnostics;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Provides a simple performance benchmark that measures the time required to export and import
/// barcode configurations to and from XML using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that creates a temporary dataset of Code128 barcodes, benchmarks ExportToXml and
    /// ImportFromXml operations, outputs elapsed times, and cleans up temporary files.
    /// </summary>
    static void Main()
    {
        // Prepare a temporary folder for generated XML files
        string tempFolder = Path.Combine(Path.GetTempPath(), "BarcodeBenchmark_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Generate sample data: 10 Code128 barcodes with distinct texts
        List<string> codeTexts = new List<string>();
        for (int i = 1; i <= 10; i++)
        {
            codeTexts.Add("CODE128_SAMPLE_" + i);
        }

        // Store paths of the exported XML files for later import
        List<string> xmlPaths = new List<string>();

        // -------------------- Benchmark ExportToXml --------------------
        Stopwatch exportStopwatch = Stopwatch.StartNew();
        foreach (string text in codeTexts)
        {
            string xmlPath = Path.Combine(tempFolder, $"barcode_{text}.xml");
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, text))
            {
                // Serialize the barcode configuration to an XML file
                generator.ExportToXml(xmlPath);
            }
            xmlPaths.Add(xmlPath);
        }
        exportStopwatch.Stop();

        // -------------------- Benchmark ImportFromXml --------------------
        Stopwatch importStopwatch = Stopwatch.StartNew();
        foreach (string xmlPath in xmlPaths)
        {
            using (var importedGenerator = BarcodeGenerator.ImportFromXml(xmlPath))
            {
                // The generator is now populated from XML; further processing could be done here.
                // Example (commented out): generate an image to validate the import.
                // using (var bitmap = importedGenerator.GenerateBarCodeImage()) { }
            }
        }
        importStopwatch.Stop();

        // Output benchmark results
        Console.WriteLine($"ExportToXml total time for {codeTexts.Count} barcodes: {exportStopwatch.ElapsedMilliseconds} ms");
        Console.WriteLine($"ImportFromXml total time for {codeTexts.Count} barcodes: {importStopwatch.ElapsedMilliseconds} ms");

        // Clean up temporary files and directory
        try
        {
            foreach (string file in Directory.GetFiles(tempFolder))
            {
                File.Delete(file);
            }
            Directory.Delete(tempFolder);
        }
        catch
        {
            // Ignoring cleanup failures as they are non‑critical for the benchmark
        }
    }
}