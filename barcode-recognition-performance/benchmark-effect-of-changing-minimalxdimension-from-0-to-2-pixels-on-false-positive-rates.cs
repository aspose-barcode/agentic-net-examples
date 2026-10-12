// Title: Benchmark MinimalXDimension Impact on Barcode False Positives
// Description: Demonstrates how changing the MinimalXDimension setting influences false positive detection when reading Code128 barcodes.
// Category-Description: This example belongs to the Aspose.BarCode barcode recognition performance tuning category. It showcases the use of BarCodeReader, QualitySettings, and XDimensionMode to adjust MinimalXDimension, a parameter that controls the minimum width of barcode modules. Developers often tweak this setting to improve detection accuracy in noisy or low‑resolution images, and this snippet provides a benchmark pattern for measuring false positive rates.
// Prompt: Benchmark the effect of changing MinimalXDimension from 0 to 2 pixels on false positive rates.
// Tags: barcode, minimalxdimension, false-positive, benchmark, code128, barcodereader, qualitysettings, aspnet, aspose.barcode

using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Provides a benchmark to compare false positive rates when MinimalXDimension is set to 0 versus 2 pixels.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the benchmark application.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for generated images
        string tempFolder = Path.Combine(Path.GetTempPath(), "Benchmark_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Generate a set of sample barcode images
        int barcodeCount = 5;
        List<string> barcodeFiles = new List<string>();
        for (int i = 0; i < barcodeCount; i++)
        {
            string filePath = Path.Combine(tempFolder, $"barcode_{i}.png");
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, $"Test{i}"))
            {
                generator.Save(filePath, BarCodeImageFormat.Png);
            }
            barcodeFiles.Add(filePath);
        }

        // Create a blank image (no barcode) to test false positives
        string blankFile = Path.Combine(tempFolder, "blank.png");
        using (var bitmap = new Bitmap(200, 200))
        {
            using (var graphics = Graphics.FromImage(bitmap))
            {
                graphics.Clear(Color.White);
            }
            bitmap.Save(blankFile, ImageFormat.Png);
        }
        barcodeFiles.Add(blankFile); // Include blank image in the test set

        // Benchmark with MinimalXDimension = 0
        float minimalXDimZero = 0f;
        int falsePositivesZero = RunBenchmark(barcodeFiles, barcodeCount, minimalXDimZero);

        // Benchmark with MinimalXDimension = 2
        float minimalXDimTwo = 2f;
        int falsePositivesTwo = RunBenchmark(barcodeFiles, barcodeCount, minimalXDimTwo);

        // Output benchmark results
        Console.WriteLine($"MinimalXDimension = {minimalXDimZero} pixels, False Positives: {falsePositivesZero}");
        Console.WriteLine($"MinimalXDimension = {minimalXDimTwo} pixels, False Positives: {falsePositivesTwo}");

        // Clean up temporary files and folder
        try
        {
            Directory.Delete(tempFolder, true);
        }
        catch
        {
            // Ignore cleanup errors
        }
    }

    /// <summary>
    /// Runs the false‑positive benchmark for a given MinimalXDimension value.
    /// </summary>
    /// <param name="files">List of image file paths to process.</param>
    /// <param name="expectedBarcodes">Number of barcode images expected in the set.</param>
    /// <param name="minimalXDimension">The MinimalXDimension value to apply during reading.</param>
    /// <returns>The count of false positive detections.</returns>
    static int RunBenchmark(List<string> files, int expectedBarcodes, float minimalXDimension)
    {
        int totalDetected = 0;

        foreach (string file in files)
        {
            if (!File.Exists(file))
                continue;

            // Initialize the barcode reader for Code128 symbology
            using (var reader = new BarCodeReader(file, DecodeType.Code128))
            {
                // Configure quality settings to use the specified MinimalXDimension
                reader.QualitySettings.XDimension = XDimensionMode.UseMinimalXDimension;
                reader.QualitySettings.MinimalXDimension = minimalXDimension;

                // Read all barcodes present in the image
                BarCodeResult[] results = reader.ReadBarCodes();
                if (results != null)
                {
                    totalDetected += results.Length;
                }
            }
        }

        // Calculate false positives (detected barcodes beyond the expected count)
        int falsePositives = totalDetected - expectedBarcodes;
        return falsePositives > 0 ? falsePositives : 0;
    }
}