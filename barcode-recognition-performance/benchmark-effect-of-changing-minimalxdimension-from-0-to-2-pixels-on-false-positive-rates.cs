// Title: Benchmark MinimalXDimension impact on barcode false positives
// Description: Demonstrates how changing the MinimalXDimension setting from 0 to 2 pixels influences the false‑positive detection rate when scanning images that contain barcodes versus pure noise.
// Category-Description: This example belongs to the Aspose.BarCode recognition performance category. It shows how to generate barcode and noise images, configure the BarCodeReader’s QualitySettings (XDimensionMode and MinimalXDimension), and measure false‑positive rates. Developers working with barcode scanning optimization, image preprocessing, or quality tuning can use similar patterns to benchmark and fine‑tune detection parameters.
// Prompt: Benchmark the effect of changing MinimalXDimension from 0 to 2 pixels on false positive rates.
// Tags: barcode, minimalxdimension, false-positive, benchmark, code128, aspose.barcode, image-generation, qualitysettings

using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates benchmarking the impact of MinimalXDimension on false positive rates when recognizing Code128 barcodes.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates barcode and noise images, runs recognition with two MinimalXDimension settings, and prints false positive rates.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for generated images
        string tempFolder = Path.Combine(Path.GetTempPath(), "BarcodeBenchmark_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Generate barcode images (Code128) and collect their file paths
        var barcodeFiles = new List<string>();
        for (int i = 1; i <= 5; i++)
        {
            string filePath = Path.Combine(tempFolder, $"barcode_{i}.png");
            GenerateBarcode(filePath, $"CODE{i:D3}");
            barcodeFiles.Add(filePath);
        }

        // Generate noise images (plain white) and collect their file paths
        var noiseFiles = new List<string>();
        for (int i = 1; i <= 5; i++)
        {
            string filePath = Path.Combine(tempFolder, $"noise_{i}.png");
            GenerateNoiseImage(filePath);
            noiseFiles.Add(filePath);
        }

        // Combine barcode and noise files into a single collection for testing
        var allFiles = new List<string>();
        allFiles.AddRange(barcodeFiles);
        allFiles.AddRange(noiseFiles);
        var barcodeFileSet = new HashSet<string>(barcodeFiles); // Quick lookup to identify true barcode images

        // Benchmark with MinimalXDimension = 0 pixels
        double falsePositiveRateZero = Benchmark(allFiles, barcodeFileSet, 0f);
        Console.WriteLine($"MinimalXDimension = 0 pixels, False Positive Rate: {falsePositiveRateZero:P2}");

        // Benchmark with MinimalXDimension = 2 pixels
        double falsePositiveRateTwo = Benchmark(allFiles, barcodeFileSet, 2f);
        Console.WriteLine($"MinimalXDimension = 2 pixels, False Positive Rate: {falsePositiveRateTwo:P2}");
    }

    /// <summary>
    /// Generates a Code128 barcode image and saves it to the specified path.
    /// </summary>
    /// <param name="path">File path where the barcode image will be saved.</param>
    /// <param name="text">Text to encode in the barcode.</param>
    static void GenerateBarcode(string path, string text)
    {
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, text))
        {
            generator.Save(path, BarCodeImageFormat.Png);
        }
    }

    /// <summary>
    /// Generates a plain white PNG image to act as noise (non‑barcode) and saves it to the specified path.
    /// </summary>
    /// <param name="path">File path where the noise image will be saved.</param>
    static void GenerateNoiseImage(string path)
    {
        using (var bitmap = new Bitmap(200, 200))
        {
            using (var graphics = Graphics.FromImage(bitmap))
            {
                graphics.Clear(Color.White);
            }
            bitmap.Save(path, ImageFormat.Png);
        }
    }

    /// <summary>
    /// Benchmarks false positive detection for a set of images using a specific MinimalXDimension value.
    /// </summary>
    /// <param name="files">All image file paths to be processed.</param>
    /// <param name="barcodeFileSet">Set containing file paths that are known barcodes.</param>
    /// <param name="minimalXDimension">MinimalXDimension value (in pixels) to apply during recognition.</param>
    /// <returns>False positive rate as a double (0‑1).</returns>
    static double Benchmark(List<string> files, HashSet<string> barcodeFileSet, float minimalXDimension)
    {
        int falsePositives = 0;
        int nonBarcodeCount = 0;

        foreach (string file in files)
        {
            bool isBarcode = barcodeFileSet.Contains(file);
            using (var reader = new BarCodeReader(file, DecodeType.Code128))
            {
                // Configure quality settings to use the specified MinimalXDimension
                reader.QualitySettings.XDimension = XDimensionMode.UseMinimalXDimension;
                reader.QualitySettings.MinimalXDimension = minimalXDimension;

                // Attempt to read barcodes from the image
                BarCodeResult[] results = reader.ReadBarCodes();
                int detected = results.Length;

                // If the image is not a barcode, count any detections as false positives
                if (!isBarcode)
                {
                    nonBarcodeCount++;
                    if (detected > 0)
                    {
                        falsePositives++;
                    }
                }
            }
        }

        // Return the proportion of non‑barcode images that were incorrectly identified as containing a barcode
        return nonBarcodeCount == 0 ? 0.0 : (double)falsePositives / nonBarcodeCount;
    }
}