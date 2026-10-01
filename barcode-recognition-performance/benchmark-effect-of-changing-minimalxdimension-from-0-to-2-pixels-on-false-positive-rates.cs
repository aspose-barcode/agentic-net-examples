// Title: Benchmark MinimalXDimension Impact on Barcode Detection Accuracy
// Description: Demonstrates how changing the MinimalXDimension setting from 0 to 2 pixels influences true‑positive and false‑positive detection rates when reading Code128 barcodes.
// Category-Description: This example belongs to the Aspose.BarCode barcode recognition performance category. It shows how to configure QualitySettings (XDimensionMode and MinimalXDimension) and measure detection accuracy across generated barcode and blank images. Developers working with barcode scanning optimization, false‑positive reduction, and parameter benchmarking can use this pattern with classes such as BarCodeReader, BarcodeGenerator, and QualitySettings.
// Prompt: Benchmark the effect of changing MinimalXDimension from 0 to 2 pixels on false positive rates.
// Tags: barcode, code128, minimalxdimension, falsepositive, benchmark, qualitysettings, generation, recognition, aspose.barcode

using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates benchmarking of MinimalXDimension impact on barcode detection accuracy.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that generates sample barcodes and blanks, runs detection with different MinimalXDimension values,
    /// and reports true/false positive rates.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for generated images
        string tempFolder = Path.Combine(Path.GetTempPath(), "BarcodeBenchmark_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Prepare sample data collections
        int barcodeCount = 5;
        int blankCount = 5;
        List<string> barcodeFiles = new List<string>();
        List<string> blankFiles = new List<string>();

        // Generate barcode images using Code128 symbology
        for (int i = 0; i < barcodeCount; i++)
        {
            string codeText = "CODE" + i;
            string filePath = Path.Combine(tempFolder, $"barcode_{i}.png");
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
            {
                // Minimal configuration; let size adapt automatically
                generator.Save(filePath, BarCodeImageFormat.Png);
            }
            barcodeFiles.Add(filePath);
        }

        // Generate blank images (no barcode) for false‑positive testing
        for (int i = 0; i < blankCount; i++)
        {
            string filePath = Path.Combine(tempFolder, $"blank_{i}.png");
            using (var bitmap = new Bitmap(200, 100))
            {
                using (var graphics = Aspose.Drawing.Graphics.FromImage(bitmap))
                {
                    graphics.Clear(Aspose.Drawing.Color.White);
                }
                bitmap.Save(filePath, Aspose.Drawing.Imaging.ImageFormat.Png);
            }
            blankFiles.Add(filePath);
        }

        // Benchmark settings: test MinimalXDimension values of 0 and 2 pixels
        float[] minimalXValues = new float[] { 0f, 2f };
        foreach (float minimalX in minimalXValues)
        {
            int falsePositives = 0;
            int truePositives = 0;

            // Process barcode images (expect detection)
            foreach (string file in barcodeFiles)
            {
                using (var reader = new BarCodeReader(file))
                {
                    // Apply quality settings to use the specified MinimalXDimension
                    reader.QualitySettings.XDimension = XDimensionMode.UseMinimalXDimension;
                    reader.QualitySettings.MinimalXDimension = minimalX;

                    BarCodeResult[] results = reader.ReadBarCodes();
                    if (results != null && results.Length > 0)
                    {
                        truePositives++;
                    }
                }
            }

            // Process blank images (expect no detection)
            foreach (string file in blankFiles)
            {
                using (var reader = new BarCodeReader(file))
                {
                    // Apply the same quality settings for consistency
                    reader.QualitySettings.XDimension = XDimensionMode.UseMinimalXDimension;
                    reader.QualitySettings.MinimalXDimension = minimalX;

                    BarCodeResult[] results = reader.ReadBarCodes();
                    if (results != null && results.Length > 0)
                    {
                        falsePositives++;
                    }
                }
            }

            // Calculate and display false‑positive rate for the current MinimalXDimension
            double falsePositiveRate = (double)falsePositives / blankCount * 100.0;
            Console.WriteLine($"MinimalXDimension = {minimalX} pixels:");
            Console.WriteLine($"  True Positives : {truePositives} / {barcodeCount}");
            Console.WriteLine($"  False Positives: {falsePositives} / {blankCount}");
            Console.WriteLine($"  False Positive Rate: {falsePositiveRate:F2}%");
            Console.WriteLine();
        }

        // Clean up temporary files and folder
        try
        {
            Directory.Delete(tempFolder, true);
        }
        catch
        {
            // If cleanup fails, ignore – files will be removed by the OS eventually.
        }
    }
}