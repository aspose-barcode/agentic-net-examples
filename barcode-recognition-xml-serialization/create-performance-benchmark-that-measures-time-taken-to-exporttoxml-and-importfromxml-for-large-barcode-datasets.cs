// Title: Benchmark ExportToXml and ImportFromXml for large barcode datasets
// Description: Demonstrates measuring performance of exporting and importing barcode definitions to/from XML using Aspose.BarCode.
// Category-Description: This example belongs to the Aspose.BarCode performance benchmarking category, showcasing how to use BarcodeGenerator for Code128 symbology, export barcode settings to XML, and re-import them. Developers often need to evaluate serialization overhead when handling large barcode collections, and this snippet provides a baseline measurement using ExportToXml and ImportFromXml APIs.
// Prompt: Create a performance benchmark that measures time taken to ExportToXml and ImportFromXml for large barcode datasets.
// Tags: barcode, performance, benchmark, exporttoxml, importfromxml, code128, aspose.barcode, serialization

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Program that benchmarks ExportToXml and ImportFromXml performance for a set of barcodes.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates sample barcodes, exports them to XML, re-imports them, and reports elapsed times.
    /// </summary>
    static void Main()
    {
        const int sampleCount = 5;                     // Number of barcode samples to generate
        var xmlFiles = new List<string>();             // Stores paths of generated XML files
        var exportStopwatch = new Stopwatch();         // Measures export duration
        var importStopwatch = new Stopwatch();         // Measures import duration

        // -------------------------------------------------
        // Generate barcode data and export each to an XML file
        // -------------------------------------------------
        exportStopwatch.Start();
        for (int i = 0; i < sampleCount; i++)
        {
            // Create a unique code text for each barcode
            string codeText = $"CODE{i}{new string('X', i + 5)}";

            // Determine temporary file path for the XML representation
            string xmlPath = Path.Combine(Path.GetTempPath(), $"barcode_{i}.xml");
            xmlFiles.Add(xmlPath);

            // Initialize generator with Code128 symbology and the generated text
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
            {
                // Optional: adjust X-dimension for demonstration purposes
                generator.Parameters.Barcode.XDimension.Point = 2f;

                // Export the barcode definition to XML; check success flag
                bool exported = generator.ExportToXml(xmlPath);
                if (!exported)
                {
                    Console.WriteLine($"Failed to export XML for barcode {i}");
                }
            }
        }
        exportStopwatch.Stop();

        // -------------------------------------------------
        // Import each previously exported XML and verify content
        // -------------------------------------------------
        importStopwatch.Start();
        foreach (string xmlPath in xmlFiles)
        {
            // Recreate the generator from the XML file
            using (var generator = BarcodeGenerator.ImportFromXml(xmlPath))
            {
                // Access properties to ensure the object is correctly initialized
                Console.WriteLine($"Imported barcode type: {generator.BarcodeType.TypeName}, CodeText: {generator.CodeText}");
            }
        }
        importStopwatch.Stop();

        // -------------------------------------------------
        // Output benchmark results
        // -------------------------------------------------
        Console.WriteLine($"Export to XML time for {sampleCount} barcodes: {exportStopwatch.ElapsedMilliseconds} ms");
        Console.WriteLine($"Import from XML time for {sampleCount} barcodes: {importStopwatch.ElapsedMilliseconds} ms");

        // -------------------------------------------------
        // Clean up temporary XML files
        // -------------------------------------------------
        foreach (string xmlPath in xmlFiles)
        {
            try
            {
                if (File.Exists(xmlPath))
                {
                    File.Delete(xmlPath);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Could not delete file {xmlPath}: {ex.Message}");
            }
        }
    }
}