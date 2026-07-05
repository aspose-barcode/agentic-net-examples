// Title: Barcode MinimalXDimension Throughput Profiling
// Description: Demonstrates how varying the MinimalXDimension setting affects the processing time of a batch of Code128 barcodes.
// Prompt: Profile the impact of increasing MinimalXDimension on overall batch processing throughput in tests.
// Tags: barcode, code128, minimalxdimension, performance, profiling, aspose.barcode

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Generates a small batch of Code128 barcode images, then measures the
/// recognition time for different MinimalXDimension settings.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Creates barcode images, runs recognition
    /// with various MinimalXDimension values, and outputs processing times.
    /// </summary>
    static void Main()
    {
        // ------------------------------------------------------------
        // 1. Prepare a small batch of barcode images (Code128) in memory.
        // ------------------------------------------------------------
        const int batchSize = 5;
        var barcodeImages = new List<byte[]>();

        for (int i = 0; i < batchSize; i++)
        {
            // Generate a barcode with a unique value.
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, $"Sample{i:D4}"))
            {
                // Save the barcode to a memory stream as PNG.
                using (var ms = new MemoryStream())
                {
                    generator.Save(ms, BarCodeImageFormat.Png);
                    barcodeImages.Add(ms.ToArray());
                }
            }
        }

        // ------------------------------------------------------------
        // 2. Define MinimalXDimension values to test.
        // ------------------------------------------------------------
        float[] minimalValues = new float[] { 1f, 2f, 4f, 8f };

        // ------------------------------------------------------------
        // 3. Measure recognition time for each MinimalXDimension setting.
        // ------------------------------------------------------------
        foreach (float minX in minimalValues)
        {
            var stopwatch = Stopwatch.StartNew();

            // Process each barcode image in the batch.
            foreach (byte[] imgData in barcodeImages)
            {
                using (var ms = new MemoryStream(imgData))
                {
                    // Initialize the reader for all supported barcode types.
                    using (var reader = new BarCodeReader(ms, DecodeType.AllSupportedTypes))
                    {
                        // Configure recognition to use the MinimalXDimension mode.
                        reader.QualitySettings.XDimension = XDimensionMode.UseMinimalXDimension;
                        reader.QualitySettings.MinimalXDimension = minX;

                        // Perform recognition (results are not used further in this demo).
                        var results = reader.ReadBarCodes();
                    }
                }
            }

            stopwatch.Stop();
            Console.WriteLine($"MinimalXDimension = {minX} → processing time: {stopwatch.ElapsedMilliseconds} ms for {batchSize} barcodes.");
        }
    }
}