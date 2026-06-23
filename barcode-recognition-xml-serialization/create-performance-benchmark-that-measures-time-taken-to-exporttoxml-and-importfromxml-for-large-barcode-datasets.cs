using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates benchmarking of Aspose.BarCode ExportToXml and ImportFromXml methods.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates a set of barcodes, exports them to XML,
    /// imports them back, measures execution time, and cleans up temporary files.
    /// </summary>
    static void Main()
    {
        // Number of barcode samples to process (kept small for quick execution)
        const int sampleCount = 5;

        // List of barcode symbologies to be used for the samples
        var symbologies = new List<BaseEncodeType>
        {
            EncodeTypes.Code128,
            EncodeTypes.QR,
            EncodeTypes.DataMatrix,
            EncodeTypes.Pdf417,
            EncodeTypes.Aztec
        };

        // If the requested sample count exceeds the predefined symbologies,
        // repeat the first symbology to fill the list.
        while (symbologies.Count < sampleCount)
        {
            symbologies.Add(EncodeTypes.Code128);
        }

        // Create a temporary directory to store generated XML files
        string tempDir = Path.Combine(Path.GetTempPath(), "AsposeBarcodeXmlBenchmark");
        Directory.CreateDirectory(tempDir);

        // Keep track of the generated XML file paths for later import and cleanup
        var xmlFiles = new List<string>();

        // -------------------- Export to XML benchmark --------------------
        var exportStopwatch = Stopwatch.StartNew(); // Start timing export operations

        for (int i = 0; i < sampleCount; i++)
        {
            // Generate a unique text value for each barcode
            string codeText = $"Sample{i + 1}";
            BaseEncodeType type = symbologies[i];

            // Create a barcode generator, export its definition to XML, and store the file path
            using (var generator = new BarcodeGenerator(type, codeText))
            {
                string xmlPath = Path.Combine(tempDir, $"barcode_{i + 1}.xml");
                generator.ExportToXml(xmlPath);
                xmlFiles.Add(xmlPath);
            }
        }

        exportStopwatch.Stop(); // Stop timing export operations

        // -------------------- Import from XML benchmark --------------------
        var importStopwatch = Stopwatch.StartNew(); // Start timing import operations

        foreach (var xmlPath in xmlFiles)
        {
            // Import the barcode definition from the XML file.
            // No further processing is required for the benchmark.
            using (var generator = BarcodeGenerator.ImportFromXml(xmlPath))
            {
                // Intentionally left blank.
            }
        }

        importStopwatch.Stop(); // Stop timing import operations

        // Output benchmark results to the console
        Console.WriteLine($"ExportToXml time for {sampleCount} items: {exportStopwatch.ElapsedMilliseconds} ms");
        Console.WriteLine($"ImportFromXml time for {sampleCount} items: {importStopwatch.ElapsedMilliseconds} ms");

        // -------------------- Cleanup temporary files --------------------
        foreach (var file in xmlFiles)
        {
            try
            {
                File.Delete(file);
            }
            catch
            {
                // Suppress any exceptions during file deletion
            }
        }

        try
        {
            Directory.Delete(tempDir);
        }
        catch
        {
            // Suppress any exceptions during directory deletion
        }
    }
}