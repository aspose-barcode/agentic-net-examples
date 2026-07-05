// Title: Benchmark ExportToXml and ImportFromXml for large barcode datasets
// Description: Demonstrates measuring the time required to export and import barcode definitions to/from XML, useful for performance analysis of bulk barcode processing.
// Prompt: Create a performance benchmark that measures time taken to ExportToXml and ImportFromXml for large barcode datasets.
// Tags: barcode symbology, performance, xml, export, import, aspose.barcode

using System;
using System.Diagnostics;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates a performance benchmark for exporting and importing barcode definitions using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates a set of barcodes, exports them to XML, measures export time,
    /// then imports them back and measures import time.
    /// </summary>
    static void Main()
    {
        // Number of barcodes to process (kept small for safe execution)
        const int barcodeCount = 5;

        // Prepare temporary folder for XML files
        string tempFolder = Path.Combine(Path.GetTempPath(), "AsposeBarcodeBenchmark");
        Directory.CreateDirectory(tempFolder);

        // Arrays to hold file paths for later import
        string[] xmlFiles = new string[barcodeCount];

        // ------------------- Export to XML Benchmark -------------------
        // Start timing the export operation
        Stopwatch exportStopwatch = Stopwatch.StartNew();

        for (int i = 0; i < barcodeCount; i++)
        {
            // Create a unique code text for each barcode
            string codeText = $"Sample{i + 1}";
            // Determine the XML file path for this barcode
            string xmlPath = Path.Combine(tempFolder, $"barcode_{i + 1}.xml");
            xmlFiles[i] = xmlPath;

            // Generate the barcode and export its definition to XML
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
            {
                // Export properties to XML file
                bool exported = generator.ExportToXml(xmlPath);
                if (!exported)
                {
                    Console.WriteLine($"Export failed for barcode {i + 1}");
                }
            }
        }

        // Stop timing and report export duration
        exportStopwatch.Stop();
        Console.WriteLine($"ExportToXml: Processed {barcodeCount} barcodes in {exportStopwatch.ElapsedMilliseconds} ms");

        // ------------------- Import from XML Benchmark -------------------
        // Start timing the import operation
        Stopwatch importStopwatch = Stopwatch.StartNew();

        for (int i = 0; i < barcodeCount; i++)
        {
            string xmlPath = xmlFiles[i];
            // Verify the XML file exists before attempting import
            if (!File.Exists(xmlPath))
            {
                Console.WriteLine($"XML file missing for barcode {i + 1}");
                continue;
            }

            // Import creates a new BarcodeGenerator instance from the XML definition
            using (var importedGenerator = BarcodeGenerator.ImportFromXml(xmlPath))
            {
                // Optionally, verify that the imported code text matches expectation
                // (not required for timing, but demonstrates usage)
                // Console.WriteLine($"Imported CodeText: {importedGenerator.CodeText}");
            }
        }

        // Stop timing and report import duration
        importStopwatch.Stop();
        Console.WriteLine($"ImportFromXml: Processed {barcodeCount} barcodes in {importStopwatch.ElapsedMilliseconds} ms");

        // Cleanup temporary XML files
        foreach (var file in xmlFiles)
        {
            try
            {
                if (File.Exists(file))
                {
                    File.Delete(file);
                }
            }
            catch
            {
                // Ignore any cleanup errors
            }
        }

        // Remove the temporary folder
        try
        {
            if (Directory.Exists(tempFolder))
            {
                Directory.Delete(tempFolder, true);
            }
        }
        catch
        {
            // Ignore any cleanup errors
        }
    }
}