// Title: Performance measurement of barcode recognition time across XDimension values
// Description: Demonstrates how to generate Code128 barcodes with varying XDimension settings, recognize them, and record the time taken for each recognition.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It showcases the use of BarcodeGenerator for creating barcodes, BarCodeReader for decoding, and common performance‑testing patterns. Developers often need to benchmark barcode parameters such as XDimension to optimize scanning speed in high‑throughput applications.
// Prompt: Generate performance graphs comparing recognition time versus XDimension values for a sample dataset.
// Tags: barcode symbology, performance, recognition, xdimension, code128, aspose.barcode, generation, csv output

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Generates barcodes with different XDimension values, measures recognition time,
/// and outputs the results in a CSV‑like format for further analysis or graphing.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates temporary barcode images, records recognition performance,
    /// prints the data, and cleans up the temporary files.
    /// </summary>
    static void Main()
    {
        // Create a dedicated temporary folder for generated barcode images
        string tempFolder = Path.Combine(Path.GetTempPath(), "PerfXDim_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Sample XDimension values (points) to test
        float[] xDimensions = new float[] { 0.5f, 1f, 2f, 3f, 5f };

        // List to hold performance results: XDimension, elapsed time (ms), and number of barcodes found
        var results = new List<(float XDim, long TimeMs, int Count)>();

        // Iterate over each XDimension value, generate a barcode, and measure recognition time
        foreach (float xDim in xDimensions)
        {
            // Build a unique file name for the current XDimension
            string filePath = Path.Combine(tempFolder, $"barcode_{xDim}.png");

            // Generate a Code128 barcode with the specified XDimension
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "Sample123"))
            {
                generator.Parameters.Barcode.XDimension.Point = xDim;
                generator.Save(filePath, BarCodeImageFormat.Png);
            }

            // Verify that the barcode image was successfully created before attempting recognition
            if (!File.Exists(filePath))
            {
                Console.WriteLine($"Failed to create barcode image at {filePath}");
                continue;
            }

            // Start timing the recognition process
            var stopwatch = Stopwatch.StartNew();
            int foundCount = 0;

            // Read and decode the barcode from the generated image
            using (var reader = new BarCodeReader(filePath, DecodeType.Code128))
            {
                var barcodes = reader.ReadBarCodes();
                foundCount = barcodes?.Length ?? 0;
            }

            // Stop the timer and record the elapsed milliseconds
            stopwatch.Stop();

            // Store the result for later output
            results.Add((xDim, stopwatch.ElapsedMilliseconds, foundCount));
        }

        // Output the collected performance data in CSV format (XDimension,TimeMs,BarcodesFound)
        Console.WriteLine("XDimension(Point),RecognitionTimeMs,BarcodesFound");
        foreach (var r in results)
        {
            Console.WriteLine($"{r.XDim},{r.TimeMs},{r.Count}");
        }

        // Attempt to delete all temporary files and the folder; ignore any cleanup errors
        try
        {
            foreach (var file in Directory.GetFiles(tempFolder))
            {
                File.Delete(file);
            }
            Directory.Delete(tempFolder);
        }
        catch
        {
            // Ignored - cleanup failure should not affect program exit
        }
    }
}