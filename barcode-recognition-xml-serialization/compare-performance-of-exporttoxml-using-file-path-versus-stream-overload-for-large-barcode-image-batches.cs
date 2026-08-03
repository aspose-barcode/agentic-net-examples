// Title: Compare ExportToXml performance: file path vs stream overload
// Description: Demonstrates measuring execution time of Aspose.BarCode ExportToXml using a file path and a stream for a batch of barcodes.
// Category-Description: This example belongs to the Aspose.BarCode generation and serialization category, showcasing how to serialize generated barcodes to XML using the ExportToXml API. It highlights key classes such as BarcodeGenerator, EncodeTypes, and the ExportToXml overloads, which developers commonly use when persisting barcode data for later processing or integration with other systems.
// Prompt: Compare performance of ExportToXml using file path versus stream overload for large barcode image batches.
// Tags: barcode, export, xml, performance, file-path, stream, aspose.barcode, code128, generation

using System;
using System.Diagnostics;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates performance comparison between ExportToXml overloads (file path vs stream) for a batch of Code128 barcodes.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates a set of barcodes, exports each to XML using both overloads, and reports elapsed time.
    /// </summary>
    static void Main()
    {
        const int batchSize = 5; // safe sample size for demonstration

        // Prepare output directory for generated XML files
        string outputDir = Path.Combine(Directory.GetCurrentDirectory(), "ExportXmlDemo");
        Directory.CreateDirectory(outputDir);

        // ------------------------------------------------------------
        // Measure performance of ExportToXml(string) overload
        // ------------------------------------------------------------
        var swPath = Stopwatch.StartNew();
        for (int i = 1; i <= batchSize; i++)
        {
            // Create a barcode generator for Code128 with a unique value
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, $"Sample{i:D4}"))
            {
                // Define XML file path for this barcode
                string xmlPath = Path.Combine(outputDir, $"barcode_path_{i}.xml");

                // Export barcode to XML file; check success
                bool success = generator.ExportToXml(xmlPath);
                if (!success)
                {
                    Console.WriteLine($"Export to file failed for item {i}");
                }
            }
        }
        swPath.Stop();

        // ------------------------------------------------------------
        // Measure performance of ExportToXml(Stream) overload
        // ------------------------------------------------------------
        var swStream = Stopwatch.StartNew();
        for (int i = 1; i <= batchSize; i++)
        {
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, $"Sample{i:D4}"))
            {
                // Define XML file path for this barcode
                string xmlPath = Path.Combine(outputDir, $"barcode_stream_{i}.xml");

                // Open a file stream for writing the XML
                using (var fileStream = new FileStream(xmlPath, FileMode.Create, FileAccess.Write))
                {
                    // Export barcode to the provided stream; check success
                    bool success = generator.ExportToXml(fileStream);
                    if (!success)
                    {
                        Console.WriteLine($"Export to stream failed for item {i}");
                    }
                }
            }
        }
        swStream.Stop();

        // Output timing results for both overloads
        Console.WriteLine($"ExportToXml(string) total time for {batchSize} items: {swPath.ElapsedMilliseconds} ms");
        Console.WriteLine($"ExportToXml(Stream) total time for {batchSize} items: {swStream.ElapsedMilliseconds} ms");
    }
}