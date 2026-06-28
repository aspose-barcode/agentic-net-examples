using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates barcode generation and decoding performance
/// with Aspose.BarCode using different processor core settings.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates sample barcodes, then measures decoding time
    /// with <c>UseAllCores</c> enabled and disabled.
    /// </summary>
    static void Main()
    {
        const int sampleCount = 5; // safe sample size for the runner

        // Generate a collection of barcode images stored as byte arrays.
        List<byte[]> barcodeImages = GenerateSampleBarcodes(sampleCount);

        // Benchmark decoding with all processor cores enabled.
        BarCodeReader.ProcessorSettings.UseAllCores = true;
        TimeSpan timeAllCores = MeasureDecodingTime(barcodeImages);
        Console.WriteLine($"Decoding with UseAllCores = true: {timeAllCores.TotalMilliseconds} ms");

        // Benchmark decoding with only a single core used.
        BarCodeReader.ProcessorSettings.UseAllCores = false;
        TimeSpan timeSingleCore = MeasureDecodingTime(barcodeImages);
        Console.WriteLine($"Decoding with UseAllCores = false: {timeSingleCore.TotalMilliseconds} ms");
    }

    /// <summary>
    /// Generates a list of barcode images (PNG) stored as byte arrays.
    /// </summary>
    /// <param name="count">Number of barcodes to generate.</param>
    /// <returns>List of byte arrays, each representing a PNG barcode image.</returns>
    private static List<byte[]> GenerateSampleBarcodes(int count)
    {
        var images = new List<byte[]>();

        // Create each barcode image and add its byte representation to the list.
        for (int i = 0; i < count; i++)
        {
            string codeText = $"Sample{i}";

            // Initialize a barcode generator for Code128 with the specified text.
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
            {
                // Save the generated barcode to a memory stream in PNG format.
                using (var ms = new MemoryStream())
                {
                    generator.Save(ms, BarCodeImageFormat.Png);
                    images.Add(ms.ToArray()); // Store the image bytes.
                }
            }
        }

        return images;
    }

    /// <summary>
    /// Measures the total time required to decode all provided barcode images.
    /// </summary>
    /// <param name="images">List of barcode image byte arrays to decode.</param>
    /// <returns>Elapsed time as a <see cref="TimeSpan"/>.</returns>
    private static TimeSpan MeasureDecodingTime(List<byte[]> images)
    {
        var stopwatch = Stopwatch.StartNew();

        // Iterate over each image and decode its barcode(s).
        foreach (var imgData in images)
        {
            // Load the image bytes into a memory stream for the reader.
            using (var ms = new MemoryStream(imgData))
            {
                // Initialize the barcode reader for all supported types.
                using (var reader = new BarCodeReader(ms, DecodeType.AllSupportedTypes))
                {
                    // ReadBarCodes returns an array of results; iterate to ensure processing.
                    foreach (var result in reader.ReadBarCodes())
                    {
                        // Access result properties to simulate work; output can be omitted.
                        Console.WriteLine($"Decoded: {result.CodeText}");
                    }
                }
            }
        }

        stopwatch.Stop();
        return stopwatch.Elapsed;
    }
}