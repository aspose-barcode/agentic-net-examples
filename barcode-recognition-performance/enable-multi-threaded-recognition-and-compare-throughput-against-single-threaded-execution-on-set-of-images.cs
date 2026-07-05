// Title: Multi‑Threaded vs Single‑Threaded Barcode Recognition Throughput
// Description: Demonstrates generating Code128 barcodes in memory, then recognizing them using Aspose.BarCode in both single‑threaded and multi‑threaded modes to compare processing time.
// Prompt: Enable multi‑threaded recognition and compare throughput against single‑threaded execution on a set of images.
// Tags: barcode, code128, recognition, multithreading, throughput, aspose.barcode, csharp

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Example program that generates a set of Code128 barcodes, then
/// reads them using Aspose.BarCode in single‑threaded and multi‑threaded
/// configurations to compare processing throughput.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        const int sampleCount = 5; // Number of barcode images to generate (kept small for demo)

        // Store generated barcode bitmaps for later recognition
        var barcodes = new List<Bitmap>();

        // ------------------------------------------------------------
        // Generate sample barcode images in memory
        // ------------------------------------------------------------
        for (int i = 0; i < sampleCount; i++)
        {
            string codeText = $"CODE{i:D4}";

            // Create a barcode generator for Code128 with the specified text
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
            {
                // Generate the barcode as a bitmap using default settings
                Bitmap bmp = generator.GenerateBarCodeImage();

                // Keep a copy for recognition tests
                barcodes.Add(bmp);
            }
        }

        // ------------------------------------------------------------
        // Single‑threaded recognition
        // ------------------------------------------------------------
        // Restrict processing to a single core
        BarCodeReader.ProcessorSettings.UseOnlyThisCoresCount = 1;

        var swSingle = Stopwatch.StartNew(); // Start timing
        int totalResultsSingle = 0;

        // Process each bitmap sequentially
        foreach (var bmp in barcodes)
        {
            using (var reader = new BarCodeReader(bmp, DecodeType.Code128))
            {
                // Count all decoded results
                foreach (var result in reader.ReadBarCodes())
                {
                    totalResultsSingle++;
                }
            }
        }

        swSingle.Stop(); // Stop timing

        // ------------------------------------------------------------
        // Multi‑threaded recognition
        // ------------------------------------------------------------
        // Allow the library to use all available processor cores
        BarCodeReader.ProcessorSettings.UseOnlyThisCoresCount = Environment.ProcessorCount;

        var swMulti = Stopwatch.StartNew(); // Start timing
        int totalResultsMulti = 0;

        // Process each bitmap in parallel
        Parallel.ForEach(barcodes, bmp =>
        {
            using (var reader = new BarCodeReader(bmp, DecodeType.Code128))
            {
                foreach (var result in reader.ReadBarCodes())
                {
                    // Increment counter in a thread‑safe manner
                    System.Threading.Interlocked.Increment(ref totalResultsMulti);
                }
            }
        });

        swMulti.Stop(); // Stop timing

        // ------------------------------------------------------------
        // Output comparison results
        // ------------------------------------------------------------
        Console.WriteLine($"Single‑threaded:  {swSingle.ElapsedMilliseconds} ms, total results = {totalResultsSingle}");
        Console.WriteLine($"Multi‑threaded:   {swMulti.ElapsedMilliseconds} ms, total results = {totalResultsMulti}");

        // ------------------------------------------------------------
        // Clean up generated bitmaps
        // ------------------------------------------------------------
        foreach (var bmp in barcodes)
        {
            bmp.Dispose();
        }
    }
}