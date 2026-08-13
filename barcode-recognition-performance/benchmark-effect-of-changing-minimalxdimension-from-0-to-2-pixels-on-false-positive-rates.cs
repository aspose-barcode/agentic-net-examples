// Title: Benchmark MinimalXDimension impact on false positive rate
// Description: Demonstrates how changing MinimalXDimension from 0 to 2 pixels influences barcode recognition false positives using Aspose.BarCode.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and recognition category. It showcases the use of BarcodeGenerator for creating Code128 barcodes, BarCodeReader for decoding, and QualitySettings to adjust MinimalXDimension. Developers often need to fine‑tune X‑dimension parameters to improve scan reliability in automated image processing pipelines.
// Prompt: Benchmark the effect of changing MinimalXDimension from 0 to 2 pixels on false positive rates.
// Tags: code128, minimalxdimension, false-positive, benchmark, generation, recognition, aspnet, aspose.barcode

using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Generates a set of Code128 barcodes, then benchmarks recognition false‑positive rates
/// while varying the MinimalXDimension setting (0 px vs 2 px).
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Creates barcode images, runs recognition with two MinimalXDimension values,
    /// and prints the false‑positive rate for each configuration.
    /// </summary>
    static void Main()
    {
        // Prepare output folder for generated barcode images
        string folder = Path.Combine(Directory.GetCurrentDirectory(), "Barcodes");
        if (!Directory.Exists(folder))
            Directory.CreateDirectory(folder);

        // Create sample data strings (Test0 … Test4)
        var samples = new List<string>();
        for (int i = 0; i < 5; i++)
            samples.Add("Test" + i);

        // Generate PNG barcodes with a fixed XDimension of 2 pixels
        var imagePaths = new List<string>();
        for (int i = 0; i < samples.Count; i++)
        {
            string filePath = Path.Combine(folder, $"barcode_{i}.png");
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, samples[i]))
            {
                // Set module (X) size to 2 pixels
                generator.Parameters.Barcode.XDimension.Point = 2f;
                // Disable auto‑sizing to keep the XDimension exact
                generator.Parameters.AutoSizeMode = AutoSizeMode.None;
                // Save as PNG
                generator.Save(filePath, BarCodeImageFormat.Png);
            }
            imagePaths.Add(filePath);
        }

        // Benchmark recognition using MinimalXDimension = 0 px and 2 px
        float[] minimalValues = new float[] { 0f, 2f };
        foreach (float minimal in minimalValues)
        {
            int falsePositives = 0;

            // Test each generated image
            foreach (var path in imagePaths)
            {
                // Extract the original sample index from the file name
                string expected = Path.GetFileNameWithoutExtension(path).Replace("barcode_", "");
                int idx = int.Parse(expected);
                string expectedText = samples[idx];

                // Decode and validate; count as false positive if validation fails
                bool success = ReadAndValidate(path, expectedText, minimal);
                if (!success)
                    falsePositives++;
            }

            // Compute and display false‑positive percentage
            double falseRate = (double)falsePositives / samples.Count * 100.0;
            Console.WriteLine($"MinimalXDimension = {minimal} px -> False Positive Rate: {falseRate:F1}% ({falsePositives}/{samples.Count})");
        }
    }

    /// <summary>
    /// Reads a barcode image using the specified MinimalXDimension and verifies that the decoded text matches the expected value.
    /// </summary>
    /// <param name="imagePath">Path to the barcode image file.</param>
    /// <param name="expectedText">The text that should be decoded from the barcode.</param>
    /// <param name="minimalXDimension">MinimalXDimension value (in pixels) to apply during recognition.</param>
    /// <returns>True if the barcode is successfully decoded and matches the expected text; otherwise false.</returns>
    static bool ReadAndValidate(string imagePath, string expectedText, float minimalXDimension)
    {
        if (!File.Exists(imagePath))
            return false;

        using (var reader = new BarCodeReader(imagePath, DecodeType.Code128))
        {
            // Configure recognition to respect MinimalXDimension
            reader.QualitySettings.XDimension = XDimensionMode.UseMinimalXDimension;
            reader.QualitySettings.MinimalXDimension = minimalXDimension;

            // Attempt to read barcodes; return true only on exact text match
            foreach (var result in reader.ReadBarCodes())
            {
                return string.Equals(result.CodeText, expectedText, StringComparison.Ordinal);
            }
        }

        // No barcode detected or text mismatch
        return false;
    }
}