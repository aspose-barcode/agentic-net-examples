// Title: Impact of MinimalXDimension on Batch Barcode Decoding Throughput
// Description: Demonstrates how varying the MinimalXDimension setting affects the time required to decode a batch of Code128 barcodes.
// Category-Description: This example belongs to the Aspose.BarCode performance profiling category, illustrating the use of BarCodeReader, BarcodeGenerator, and quality settings to measure decoding speed. Developers often need to benchmark different rendering parameters to optimize throughput in high‑volume scanning scenarios.
// Prompt: Profile the impact of increasing MinimalXDimension on overall batch processing throughput in tests.
// Tags: code128, decoding, performance, minimalxdimension, qualitysettings, barcodegenerator, barcodereader, aspose.barcode

using System;
using System.Collections.Generic;
using System.Diagnostics;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Shows how changing the MinimalXDimension influences batch processing time when decoding Code128 barcodes.
/// </summary>
class Program
{
    /// <summary>
    /// Generates a small batch of Code128 barcodes, then measures decoding time for several MinimalXDimension values.
    /// </summary>
    static void Main()
    {
        const int batchSize = 5; // Number of barcodes to generate for the test batch

        // ------------------------------------------------------------
        // 1. Generate sample barcode images in memory (Code128)
        // ------------------------------------------------------------
        var barcodes = new List<Bitmap>();
        for (int i = 0; i < batchSize; i++)
        {
            string codeText = $"CODE{i:D4}";
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
            {
                // Generate a bitmap with default settings
                Bitmap bitmap = generator.GenerateBarCodeImage();
                barcodes.Add(bitmap);
            }
        }

        // ------------------------------------------------------------
        // 2. Define MinimalXDimension values to evaluate
        // ------------------------------------------------------------
        float[] minimalXDimensions = new float[] { 1f, 2f, 4f, 8f };

        // ------------------------------------------------------------
        // 3. Process the batch for each MinimalXDimension setting
        // ------------------------------------------------------------
        foreach (float minX in minimalXDimensions)
        {
            var stopwatch = Stopwatch.StartNew(); // Start timing for current setting

            // Decode each barcode image using the current MinimalXDimension
            foreach (var image in barcodes)
            {
                using (var reader = new BarCodeReader(image, DecodeType.Code128))
                {
                    // Apply quality settings: use MinimalXDimension mode with the specified value
                    reader.QualitySettings.XDimension = XDimensionMode.UseMinimalXDimension;
                    reader.QualitySettings.MinimalXDimension = minX;

                    // Trigger the decoding process; results are not needed for this benchmark
                    foreach (var result in reader.ReadBarCodes())
                    {
                        // No action required; iteration ensures processing occurs
                    }
                }
            }

            stopwatch.Stop(); // Stop timing

            // Output the elapsed time for the current MinimalXDimension
            Console.WriteLine($"MinimalXDimension = {minX} px, Processing Time = {stopwatch.ElapsedMilliseconds} ms");
        }

        // ------------------------------------------------------------
        // 4. Clean up generated bitmap resources
        // ------------------------------------------------------------
        foreach (var bmp in barcodes)
        {
            bmp.Dispose();
        }
    }
}