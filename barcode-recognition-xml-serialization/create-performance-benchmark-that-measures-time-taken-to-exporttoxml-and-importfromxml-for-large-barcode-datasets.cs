// Title: Benchmark ExportToXml and ImportFromXml for large barcode datasets
// Description: Demonstrates measuring the performance of exporting and importing barcode definitions to and from XML using Aspose.BarCode. Useful for evaluating throughput when handling many barcodes.
// Category-Description: This example belongs to the Aspose.BarCode performance benchmarking category, showcasing the use of BarcodeGenerator, ExportToXml, and ImportFromXml APIs. Developers often need to assess serialization speed for bulk barcode processing, such as batch generation, storage, or transmission scenarios. The snippet illustrates typical patterns for timing these operations in .NET applications.
// Prompt: Create a performance benchmark that measures time taken to ExportToXml and ImportFromXml for large barcode datasets.
// Tags: barcode, performance, benchmark, exporttoxml, importfromxml, aspose.barcode, serialization

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates a performance benchmark for ExportToXml and ImportFromXml using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates sample barcodes, measures export and import times, and outputs results.
    /// </summary>
    static void Main()
    {
        // Prepare a collection of sample barcode definitions (symbology type and text).
        var samples = new List<(BaseEncodeType Encode, string Text)>
        {
            (EncodeTypes.QR, "SampleQR1"),
            (EncodeTypes.Code128, "CODE12812345"),
            (EncodeTypes.Pdf417, "PDF417_SAMPLE_TEXT"),
            (EncodeTypes.DataMatrix, "DMATRIX123"),
            (EncodeTypes.Aztec, "AZTEC_SAMPLE")
        };

        // Create a BarcodeGenerator for each sample and apply a simple parameter change.
        var generators = new List<BarcodeGenerator>();
        foreach (var (encode, text) in samples)
        {
            var gen = new BarcodeGenerator(encode, text);
            // Adjust the X-dimension to a specific pixel size.
            gen.Parameters.Barcode.XDimension.Pixels = 2f;
            generators.Add(gen);
        }

        // Benchmark the ExportToXml operation for all generators.
        var exportStreams = new List<MemoryStream>();
        var swExport = Stopwatch.StartNew();
        foreach (var gen in generators)
        {
            var ms = new MemoryStream();
            gen.ExportToXml(ms);
            ms.Position = 0; // Reset stream position for subsequent reading.
            exportStreams.Add(ms);
        }
        swExport.Stop();

        // Benchmark the ImportFromXml operation for all previously exported streams.
        var importedGenerators = new List<BarcodeGenerator>();
        var swImport = Stopwatch.StartNew();
        foreach (var ms in exportStreams)
        {
            var importedGen = BarcodeGenerator.ImportFromXml(ms);
            importedGenerators.Add(importedGen);
        }
        swImport.Stop();

        // Output the measured times to the console.
        Console.WriteLine($"ExportToXml total time for {generators.Count} items: {swExport.ElapsedMilliseconds} ms");
        Console.WriteLine($"ImportFromXml total time for {exportStreams.Count} items: {swImport.ElapsedMilliseconds} ms");

        // Clean up resources: dispose generators and memory streams.
        foreach (var gen in generators)
        {
            using (gen) { }
        }

        foreach (var gen in importedGenerators)
        {
            using (gen) { }
        }

        foreach (var ms in exportStreams)
        {
            using (ms) { }
        }
    }
}