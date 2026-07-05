// Title: Barcode XDimension Profiling with Multi‑Core Processing
// Description: Demonstrates generating a batch of Code128 barcodes, then measures processing time with and without UseMinimalXDimension enabled to observe CPU utilization.
// Prompt: Profile the effect of enabling UseMinimalXDimension on multi‑core CPU utilization during batch processing.
// Tags: barcode, code128, xdimension, multithreading, profiling, aspnet.barcode

using System;
using System.Diagnostics;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Example program that creates a small batch of Code128 barcodes,
/// then profiles the read performance with default and minimal XDimension settings
/// while utilizing all available processor cores.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates barcode images, then processes them
    /// using two different XDimension configurations while timing each run.
    /// </summary>
    static void Main()
    {
        // Configure barcode reader to use all available CPU cores.
        BarCodeReader.ProcessorSettings.UseOnlyThisCoresCount = Environment.ProcessorCount;

        // Prepare an output folder for the generated barcode images.
        string outputFolder = Path.Combine(Directory.GetCurrentDirectory(), "Barcodes");
        if (!Directory.Exists(outputFolder))
        {
            Directory.CreateDirectory(outputFolder);
        }

        // Generate a small batch of barcode images (5 items).
        const int batchSize = 5;
        string[] barcodeFiles = new string[batchSize];
        for (int i = 0; i < batchSize; i++)
        {
            string codeText = $"CODE{i + 1:D4}";
            string filePath = Path.Combine(outputFolder, $"barcode{i + 1}.png");

            // Create a barcode generator for Code128 and set optional XDimension.
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
            {
                generator.Parameters.Barcode.XDimension.Point = 2f; // optional XDimension setting
                generator.Save(filePath);
            }

            barcodeFiles[i] = filePath;
        }

        // Process the batch with the default XDimension mode and record elapsed time.
        long defaultTime = ProcessBatch(barcodeFiles, useMinimalXDimension: false);
        Console.WriteLine($"Default XDimension mode processing time: {defaultTime} ms");

        // Process the batch with UseMinimalXDimension mode enabled and record elapsed time.
        long minimalTime = ProcessBatch(barcodeFiles, useMinimalXDimension: true);
        Console.WriteLine($"UseMinimalXDimension mode processing time: {minimalTime} ms");
    }

    /// <summary>
    /// Reads a collection of barcode image files, optionally enabling the UseMinimalXDimension mode,
    /// and returns the total processing time in milliseconds.
    /// </summary>
    /// <param name="files">Array of file paths to barcode images.</param>
    /// <param name="useMinimalXDimension">If true, enables minimal XDimension mode for reading.</param>
    /// <returns>Elapsed time in milliseconds for processing the entire batch.</returns>
    static long ProcessBatch(string[] files, bool useMinimalXDimension)
    {
        Stopwatch sw = Stopwatch.StartNew();

        foreach (string file in files)
        {
            // Open a barcode reader for each image file.
            using (var reader = new BarCodeReader(file, DecodeType.Code128))
            {
                if (useMinimalXDimension)
                {
                    // Enable UseMinimalXDimension mode and set a minimal XDimension value.
                    reader.QualitySettings.XDimension = XDimensionMode.UseMinimalXDimension;
                    reader.QualitySettings.MinimalXDimension = 2f;
                }

                // Iterate through all detected barcodes in the image.
                foreach (var result in reader.ReadBarCodes())
                {
                    // Output detected barcode text (can be suppressed for pure profiling).
                    Console.WriteLine($"Detected: {result.CodeText}");
                }
            }
        }

        sw.Stop();
        return sw.ElapsedMilliseconds;
    }
}