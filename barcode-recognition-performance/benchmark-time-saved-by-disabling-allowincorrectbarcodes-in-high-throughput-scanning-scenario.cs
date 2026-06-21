using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing; // required for BarCodeImageFormat

/// <summary>
/// Demonstrates benchmarking of barcode reading with and without tolerant mode (AllowIncorrectBarcodes).
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates sample Code39 barcodes, then measures
    /// the time required to read them with tolerant mode disabled and enabled.
    /// </summary>
    static void Main()
    {
        // --------------------------------------------------------------------
        // Prepare a small set of barcode images (Code39) to simulate a high‑throughput scenario.
        // --------------------------------------------------------------------
        const int sampleCount = 5;
        var barcodeImages = new List<byte[]>();

        for (int i = 0; i < sampleCount; i++)
        {
            // Generate a barcode image and store it as a byte array.
            using (var generator = new BarcodeGenerator(EncodeTypes.Code39FullASCII, "ABC123"))
            {
                // Enable checksum to allow both correct and incorrect reads.
                generator.Parameters.Barcode.IsChecksumEnabled = EnableChecksum.Yes;

                using (var ms = new MemoryStream())
                {
                    // Save the barcode as PNG into the memory stream.
                    generator.Save(ms, BarCodeImageFormat.Png);
                    // Add the resulting byte array to the collection.
                    barcodeImages.Add(ms.ToArray());
                }
            }
        }

        // --------------------------------------------------------------------
        // Benchmark reading with AllowIncorrectBarcodes = false (default behavior).
        // --------------------------------------------------------------------
        var swWithout = Stopwatch.StartNew();

        foreach (var data in barcodeImages)
        {
            using (var ms = new MemoryStream(data))
            using (var reader = new BarCodeReader(ms, DecodeType.AllSupportedTypes))
            {
                // Explicitly disable tolerant mode.
                reader.QualitySettings.AllowIncorrectBarcodes = false;

                // Force the recognition process; results are ignored for the benchmark.
                foreach (var result in reader.ReadBarCodes())
                {
                    // No further processing needed.
                }
            }
        }

        swWithout.Stop();

        // --------------------------------------------------------------------
        // Benchmark reading with AllowIncorrectBarcodes = true (tolerant mode).
        // --------------------------------------------------------------------
        var swWith = Stopwatch.StartNew();

        foreach (var data in barcodeImages)
        {
            using (var ms = new MemoryStream(data))
            using (var reader = new BarCodeReader(ms, DecodeType.AllSupportedTypes))
            {
                // Enable tolerant mode to allow recognition of damaged/incorrect barcodes.
                reader.QualitySettings.AllowIncorrectBarcodes = true;

                // Force the recognition process; results are ignored for the benchmark.
                foreach (var result in reader.ReadBarCodes())
                {
                    // No further processing needed.
                }
            }
        }

        swWith.Stop();

        // --------------------------------------------------------------------
        // Output the measured times.
        // --------------------------------------------------------------------
        Console.WriteLine(
            $"Reading {sampleCount} barcodes without AllowIncorrectBarcodes: {swWithout.Elapsed.TotalMilliseconds} ms");
        Console.WriteLine(
            $"Reading {sampleCount} barcodes with AllowIncorrectBarcodes:    {swWith.Elapsed.TotalMilliseconds} ms");
    }
}