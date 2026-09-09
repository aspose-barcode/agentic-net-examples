// Title: Benchmark ExportToXml and ImportFromXml for Various Barcode Types
// Description: Demonstrates measuring the performance of exporting barcode generator settings to XML and importing them back, using a set of common symbologies.
// Category-Description: This example belongs to the Aspose.BarCode performance benchmarking category, illustrating how to use BarcodeGenerator.ExportToXml and BarcodeGenerator.ImportFromXml. Developers working with large barcode datasets can benchmark serialization and deserialization times to optimize processing pipelines. Typical use cases include bulk barcode generation, storage, and retrieval where XML representation is required.
// Prompt: Create a performance benchmark that measures time taken to ExportToXml and ImportFromXml for large barcode datasets.
// Tags: barcode symbology, performance benchmark, xml serialization, exporttoxml, importfromxml, aspose.barcode, barcodegenerator

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Provides a simple performance benchmark for exporting and importing barcode generator settings to and from XML.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the benchmark application.
    /// Measures ExportToXml and ImportFromXml execution times for a set of barcode symbologies.
    /// </summary>
    static void Main()
    {
        // Define a list of barcode symbologies to test
        var encodeTypes = new List<BaseEncodeType>
        {
            EncodeTypes.Code128,
            EncodeTypes.QR,
            EncodeTypes.Pdf417,
            EncodeTypes.DataMatrix,
            EncodeTypes.Aztec
        };

        // Containers for exported XML data and timing results
        var exportedData = new List<byte[]>();
        var exportTimes = new List<long>();
        var importTimes = new List<long>();

        // --------------------------------------------------------------------
        // Export each barcode generator to XML (in memory) and record elapsed time
        // --------------------------------------------------------------------
        for (int i = 0; i < encodeTypes.Count; i++)
        {
            string codeText = $"Sample{i + 1}";
            using (var generator = new BarcodeGenerator(encodeTypes[i], codeText))
            {
                // Set a specific X-dimension for consistency
                generator.Parameters.Barcode.XDimension.Pixels = 2f;

                using (var ms = new MemoryStream())
                {
                    var sw = Stopwatch.StartNew();
                    generator.ExportToXml(ms);
                    sw.Stop();

                    exportTimes.Add(sw.ElapsedMilliseconds);
                    exportedData.Add(ms.ToArray());
                }
            }
        }

        // --------------------------------------------------------------------
        // Import each previously exported XML back into a generator and record elapsed time
        // --------------------------------------------------------------------
        for (int i = 0; i < exportedData.Count; i++)
        {
            using (var ms = new MemoryStream(exportedData[i]))
            {
                var sw = Stopwatch.StartNew();
                var importedGenerator = BarcodeGenerator.ImportFromXml(ms);
                sw.Stop();

                importTimes.Add(sw.ElapsedMilliseconds);

                // Verify that the imported generator can produce an image
                using (importedGenerator)
                {
                    using (var imgStream = new MemoryStream())
                    {
                        importedGenerator.Save(imgStream, BarCodeImageFormat.Png);
                    }
                }
            }
        }

        // --------------------------------------------------------------------
        // Output benchmark results to the console
        // --------------------------------------------------------------------
        Console.WriteLine("Export to XML times (ms):");
        for (int i = 0; i < exportTimes.Count; i++)
        {
            Console.WriteLine($"Item {i + 1}: {exportTimes[i]}");
        }

        Console.WriteLine("Import from XML times (ms):");
        for (int i = 0; i < importTimes.Count; i++)
        {
            Console.WriteLine($"Item {i + 1}: {importTimes[i]}");
        }
    }
}