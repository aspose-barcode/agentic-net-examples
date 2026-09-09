// Title: Benchmark MinimalXDimension impact on false positive barcode detection
// Description: Demonstrates how changing the MinimalXDimension setting from 0 to 2 pixels influences false positive rates when recognizing Code128 barcodes in generated images.
// Category-Description: This example belongs to the Aspose.BarCode recognition performance category. It shows how to configure the XDimension quality settings of BarCodeReader, generate test barcodes with BarcodeGenerator, and measure detection accuracy. Developers working with barcode scanning optimization often adjust MinimalXDimension to reduce noise‑induced false detections, and this snippet provides a reproducible benchmark for such scenarios.
// Prompt: Benchmark the effect of changing MinimalXDimension from 0 to 2 pixels on false positive rates.
// Tags: barcode, code128, minimalxdimension, false positive, benchmark, generation, recognition, aspose.barcode

using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates benchmarking the impact of MinimalXDimension on false positive barcode detection rates.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates test images, runs benchmarks with different MinimalXDimension values, and outputs results.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for generated test images
        string tempFolder = Path.Combine(Path.GetTempPath(), "Benchmark_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Prepare a collection to hold file paths and their expected barcode text (null for blank image)
        var testFiles = new List<(string Path, string Expected)>();

        // Generate three Code128 barcode images with distinct texts
        var barcodeTexts = new[] { "TEST01", "TEST02", "TEST03" };
        foreach (var text in barcodeTexts)
        {
            string filePath = Path.Combine(tempFolder, $"barcode_{text}.png");
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, text))
            {
                generator.Save(filePath, BarCodeImageFormat.Png);
            }
            testFiles.Add((filePath, text));
        }

        // Create a blank image (no barcode) to test false positive detection
        string blankPath = Path.Combine(tempFolder, "blank.png");
        using (var bitmap = new Bitmap(200, 100))
        {
            using (var graphics = Graphics.FromImage(bitmap))
            {
                graphics.Clear(Aspose.Drawing.Color.White);
            }
            bitmap.Save(blankPath, Aspose.Drawing.Imaging.ImageFormat.Png);
        }
        testFiles.Add((blankPath, null));

        // Run benchmark with MinimalXDimension set to 0 pixels
        int falsePositivesZero = RunBenchmark(testFiles, 0f);
        // Run benchmark with MinimalXDimension set to 2 pixels
        int falsePositivesTwo = RunBenchmark(testFiles, 2f);

        // Output the false positive counts for each setting
        Console.WriteLine($"False positives with MinimalXDimension = 0: {falsePositivesZero}");
        Console.WriteLine($"False positives with MinimalXDimension = 2: {falsePositivesTwo}");

        // Attempt to clean up the temporary folder; ignore any errors
        try
        {
            Directory.Delete(tempFolder, true);
        }
        catch
        {
            // Cleanup errors are non‑critical for this demo
        }
    }

    /// <summary>
    /// Executes the barcode recognition benchmark for a set of files using a specified MinimalXDimension.
    /// </summary>
    /// <param name="files">List of tuples containing image paths and expected barcode text (null for no barcode).</param>
    /// <param name="minimalXDimension">The MinimalXDimension value (in pixels) to apply during recognition.</param>
    /// <returns>The total number of false positive detections.</returns>
    static int RunBenchmark(List<(string Path, string Expected)> files, float minimalXDimension)
    {
        int falsePositives = 0;

        foreach (var (filePath, expected) in files)
        {
            // Skip missing files gracefully
            if (!File.Exists(filePath))
                continue;

            // Initialize the barcode reader for Code128 symbology
            using (var reader = new BarCodeReader(filePath, DecodeType.Code128))
            {
                // Configure quality settings to use MinimalXDimension
                reader.QualitySettings.XDimension = XDimensionMode.UseMinimalXDimension;
                reader.QualitySettings.MinimalXDimension = minimalXDimension;

                // Perform barcode detection
                BarCodeResult[] results = reader.ReadBarCodes();

                if (expected == null)
                {
                    // No barcode expected; any detection counts as a false positive
                    falsePositives += results.Length;
                }
                else
                {
                    // Compare each detected barcode text with the expected value
                    foreach (var result in results)
                    {
                        if (!string.Equals(result.CodeText, expected, StringComparison.Ordinal))
                        {
                            falsePositives++;
                        }
                    }
                }
            }
        }

        return falsePositives;
    }
}