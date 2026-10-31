// Title: Batch barcode reading performance test with varying MinimalXDimension
// Description: Demonstrates generating a set of Code128 barcode images, reading them in a batch while adjusting the MinimalXDimension setting, and measuring throughput.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It showcases the use of BarcodeGenerator for creating barcodes, BarCodeReader for decoding, and QualitySettings for fine‑tuning read performance. Typical scenarios include bulk scanning, automated quality testing, and performance profiling where developers need to evaluate how X‑dimension parameters affect processing speed.
// Prompt: Profile the impact of increasing MinimalXDimension on overall batch processing throughput in tests.
// Tags: barcode symbology, generation, recognition, performance, minimalxdimension, batch processing, aspose.barcode, code128, png

using System;
using System.IO;
using System.Diagnostics;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates batch barcode generation and reading while profiling the effect of
/// different MinimalXDimension values on processing time.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates sample barcodes, reads them with varying
    /// MinimalXDimension settings, reports performance metrics, and cleans up temporary files.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for the batch operation
        string batchFolder = Path.Combine(Path.GetTempPath(), "Batch_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(batchFolder);

        // Generate sample barcode images (Code128) and store their file paths
        List<string> barcodeFiles = new List<string>();
        int sampleCount = 5;
        for (int i = 1; i <= sampleCount; i++)
        {
            string codeText = $"Test{i:D3}";
            string filePath = Path.Combine(batchFolder, $"barcode_{i}.png");
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
            {
                generator.Save(filePath, BarCodeImageFormat.Png);
            }
            barcodeFiles.Add(filePath);
        }

        // Define MinimalXDimension values (in pixels) to be tested
        float[] minimalXDimensions = new float[] { 1f, 2f, 3f, 4f, 5f };

        // Iterate over each MinimalXDimension setting and measure read performance
        foreach (float minDim in minimalXDimensions)
        {
            int totalRead = 0;
            Stopwatch sw = new Stopwatch();
            sw.Start();

            // Read each generated barcode file with the current MinimalXDimension
            foreach (string file in barcodeFiles)
            {
                if (!File.Exists(file))
                {
                    Console.WriteLine($"File not found: {file}");
                    continue;
                }

                try
                {
                    using (var reader = new BarCodeReader(file, DecodeType.Code128))
                    {
                        // Apply quality settings: use MinimalXDimension mode and set the value
                        reader.QualitySettings.XDimension = XDimensionMode.UseMinimalXDimension;
                        reader.QualitySettings.MinimalXDimension = minDim;

                        // Perform the read operation and count results
                        BarCodeResult[] results = reader.ReadBarCodes();
                        totalRead += results.Length;
                    }
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine($"Skipping unreadable file '{Path.GetFileName(file)}': {ex.Message}");
                }
            }

            sw.Stop();
            Console.WriteLine($"MinimalXDimension = {minDim} px | Barcodes read: {totalRead} | Time elapsed: {sw.ElapsedMilliseconds} ms");
        }

        // Clean up temporary barcode files and the batch folder
        try
        {
            foreach (string file in barcodeFiles)
            {
                if (File.Exists(file))
                {
                    File.Delete(file);
                }
            }

            if (Directory.Exists(batchFolder))
            {
                Directory.Delete(batchFolder, true);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Cleanup error: {ex.Message}");
        }
    }
}