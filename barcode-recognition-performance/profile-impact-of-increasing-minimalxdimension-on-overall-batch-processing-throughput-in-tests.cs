// Title: Batch processing throughput profiling with varying MinimalXDimension
// Description: Demonstrates how changing the MinimalXDimension setting affects the time required to read a batch of Code128 barcodes.
// Category-Description: This example belongs to the Aspose.BarCode reading and quality‑settings category. It shows how to configure the XDimensionMode and MinimalXDimension via the QualitySettings API, generate sample barcodes, and measure read performance. Developers often use these APIs to fine‑tune barcode recognition speed and accuracy in bulk processing scenarios.
// Prompt: Profile the impact of increasing MinimalXDimension on overall batch processing throughput in tests.
// Tags: barcode symbology, performance, batch processing, minimalxdimension, aspose.barcode, code128, qualitysettings, profiling

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that generates a set of Code128 barcode images,
/// then measures the time required to read them while varying the
/// <c>MinimalXDimension</c> quality setting. Used for profiling
/// the impact of this setting on batch processing throughput.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates temporary barcode files,
    /// iterates over a range of MinimalXDimension values, measures
    /// read performance, and cleans up the generated files.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for the test
        string tempFolder = Path.Combine(Path.GetTempPath(), "BatchXDim_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Generate a small set of sample barcode images
        List<string> barcodeFiles = new List<string>();
        for (int i = 0; i < 5; i++)
        {
            string codeText = "Test" + i;
            string filePath = Path.Combine(tempFolder, $"barcode_{i}.png");
            using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
            {
                // Use default settings; no need to set XDimension here
                generator.Save(filePath, BarCodeImageFormat.Png);
            }
            barcodeFiles.Add(filePath);
        }

        // Define MinimalXDimension values to test (in points)
        float[] minimalXDimensions = new float[] { 0.5f, 1f, 2f, 4f };

        Console.WriteLine($"Batch processing throughput test in folder: {tempFolder}");
        Console.WriteLine();

        // Iterate over each MinimalXDimension value and measure total read time
        foreach (float minXDim in minimalXDimensions)
        {
            // Start timing for the current MinimalXDimension
            Stopwatch sw = Stopwatch.StartNew();

            // Read each generated barcode file with the current quality setting
            foreach (string file in barcodeFiles)
            {
                try
                {
                    using (BarCodeReader reader = new BarCodeReader(file, DecodeType.Code128))
                    {
                        // Configure quality settings to use MinimalXDimension
                        reader.QualitySettings.XDimension = XDimensionMode.UseMinimalXDimension;
                        reader.QualitySettings.MinimalXDimension = minXDim;

                        // Read barcodes (result array may contain multiple entries)
                        BarCodeResult[] results = reader.ReadBarCodes();
                        foreach (BarCodeResult result in results)
                        {
                            // Access properties to ensure results are processed (profiling purpose)
                            string text = result.CodeText;
                            string type = result.CodeTypeName;
                        }
                    }
                }
                catch (ArgumentException ex) when (ex.Message.Contains("Image loading failed"))
                {
                    // Log warning and continue with next file
                    Console.WriteLine($"Warning: Skipping unreadable file '{file}'.");
                }
            }

            // Stop timing and output the elapsed time for this MinimalXDimension
            sw.Stop();
            Console.WriteLine($"MinimalXDimension = {minXDim} pt => Total read time: {sw.ElapsedMilliseconds} ms");
        }

        // Cleanup temporary files and directory
        try
        {
            foreach (string file in barcodeFiles)
            {
                if (File.Exists(file))
                    File.Delete(file);
            }
            Directory.Delete(tempFolder);
        }
        catch (Exception cleanupEx)
        {
            Console.WriteLine($"Cleanup warning: {cleanupEx.Message}");
        }
    }
}