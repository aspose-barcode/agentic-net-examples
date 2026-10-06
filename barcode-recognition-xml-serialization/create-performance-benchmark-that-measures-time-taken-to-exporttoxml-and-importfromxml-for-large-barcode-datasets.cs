// Title: Performance benchmark for ExportToXml and ImportFromXml with large barcode datasets
// Description: Demonstrates measuring the time required to export barcode definitions to XML and import them back using Aspose.BarCode, useful for evaluating performance on sizable collections.
// Category-Description: This example belongs to the Aspose.BarCode performance testing category, showcasing how to use BarcodeGenerator, ExportToXml, and ImportFromXml for bulk barcode operations. Developers often need to serialize large numbers of barcodes for storage, transmission, or later regeneration, and measuring the execution time helps in capacity planning and optimization.
// Prompt: Create a performance benchmark that measures time taken to ExportToXml and ImportFromXml for large barcode datasets.
// Tags: barcode symbology, performance, export, import, xml, aspose.barcode, benchmark, generation

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Provides a simple performance benchmark that measures the time taken to export
/// barcode definitions to XML and import them back using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the benchmark application.
    /// </summary>
    /// <param name="args">Command‑line arguments (not used).</param>
    static void Main(string[] args)
    {
        // Define the number of barcode samples to process.
        int sampleCount = 5;

        // Prepare a list of barcode symbologies to test.
        List<BaseEncodeType> encodeTypes = new List<BaseEncodeType>
        {
            EncodeTypes.QR,
            EncodeTypes.Code128,
            EncodeTypes.DataMatrix,
            EncodeTypes.Pdf417,
            EncodeTypes.Aztec
        };

        // Collection that will hold the XML streams generated for each barcode.
        List<MemoryStream> xmlStreams = new List<MemoryStream>();

        // -------------------- ExportToXml Benchmark --------------------
        Stopwatch exportSw = Stopwatch.StartNew();

        for (int i = 0; i < sampleCount; i++)
        {
            // Cycle through the prepared encode types.
            BaseEncodeType encode = encodeTypes[i % encodeTypes.Count];

            // Create a relatively long code text to simulate a large dataset.
            string codeText = $"Sample{i + 1}_LongText_{new string('X', 50)}";

            // Generate the barcode and export its definition to an XML stream.
            using (BarcodeGenerator generator = new BarcodeGenerator(encode, codeText))
            {
                // Example configuration (optional): set X‑dimension to 2 pixels.
                generator.Parameters.Barcode.XDimension.Pixels = 2;

                // Export the barcode definition to a memory stream.
                MemoryStream ms = new MemoryStream();
                generator.ExportToXml(ms);
                ms.Position = 0; // Reset stream position for later import.
                xmlStreams.Add(ms);
            }
        }

        exportSw.Stop();

        // -------------------- ImportFromXml Benchmark --------------------
        Stopwatch importSw = Stopwatch.StartNew();

        foreach (MemoryStream ms in xmlStreams)
        {
            // Import the barcode definition from the XML stream.
            using (BarcodeGenerator generator = BarcodeGenerator.ImportFromXml(ms))
            {
                // Optional: generate an image to verify the imported object works.
                // generator.Save("temp.png", BarCodeImageFormat.Png);
            }

            // Dispose the memory stream after import.
            ms.Dispose();
        }

        importSw.Stop();

        // Output benchmark results.
        Console.WriteLine($"ExportToXml total time: {exportSw.ElapsedMilliseconds} ms");
        Console.WriteLine($"ImportFromXml total time: {importSw.ElapsedMilliseconds} ms");
    }
}