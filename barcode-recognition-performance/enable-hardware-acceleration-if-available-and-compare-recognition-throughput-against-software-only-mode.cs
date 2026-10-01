// Title: Compare barcode recognition throughput with and without hardware acceleration
// Description: Demonstrates enabling hardware acceleration for Aspose.BarCode recognition and measures the average processing time versus software‑only mode.
// Category-Description: This example belongs to the Aspose.BarCode recognition performance category. It shows how to configure the BarCodeReader processor settings to use all CPU cores (hardware acceleration) or limit to a single core (software‑only), generate a sample Code128 barcode, and benchmark throughput. Developers working on high‑volume scanning or real‑time applications often need to tune these settings for optimal speed.
// Prompt: Enable hardware acceleration if available and compare recognition throughput against software‑only mode.
// Tags: barcode, recognition, performance, hardware acceleration, code128, aspose.barcode, benchmark

using System;
using System.IO;
using System.Diagnostics;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates hardware‑accelerated barcode recognition and compares its throughput to software‑only mode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates a sample barcode, measures recognition time with and without hardware acceleration, and outputs the results.
    /// </summary>
    static void Main()
    {
        // Create a temporary folder for the sample barcode image
        string tempFolder = Path.Combine(Path.GetTempPath(), "AsposeBarcodeSample_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);
        string barcodePath = Path.Combine(tempFolder, "sample.png");

        // Generate a barcode image (Code128) and save it
        GenerateSampleBarcode(barcodePath);

        // Verify the file was created
        if (!File.Exists(barcodePath))
        {
            Console.WriteLine("Failed to create barcode image.");
            return;
        }

        // Compare throughput: hardware acceleration (use all cores) vs software‑only (single core)
        const int iterations = 20;

        long swHardware = MeasureRecognitionThroughput(barcodePath, true, iterations);
        long swSoftware = MeasureRecognitionThroughput(barcodePath, false, iterations);

        Console.WriteLine($"Average recognition time with hardware acceleration (all cores): {swHardware} ms");
        Console.WriteLine($"Average recognition time without hardware acceleration (single core): {swSoftware} ms");

        // Clean up temporary files
        try { Directory.Delete(tempFolder, true); } catch { /* ignore cleanup errors */ }
    }

    // Generates a simple Code128 barcode and saves it as PNG
    private static void GenerateSampleBarcode(string filePath)
    {
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            // Default auto‑size works fine for this example
            generator.Save(filePath, BarCodeImageFormat.Png);
        }
    }

    // Measures average recognition time over a number of iterations
    private static long MeasureRecognitionThroughput(string imagePath, bool useAllCores, int iterations)
    {
        // Configure processor settings before creating the reader
        BarCodeReader.ProcessorSettings.UseAllCores = useAllCores;

        // When not using all cores, limit to a single core
        if (!useAllCores)
        {
            BarCodeReader.ProcessorSettings.UseOnlyThisCoresCount = 1;
        }

        long totalMs = 0;
        for (int i = 0; i < iterations; i++)
        {
            var sw = Stopwatch.StartNew();

            using (var reader = new BarCodeReader(imagePath, DecodeType.AllSupportedTypes))
            {
                // Read all supported barcodes from the image
                var results = reader.ReadBarCodes();

                // Iterate results to ensure the read operation is performed
                foreach (var result in results)
                {
                    // Suppress output; just access the code text
                    var _ = result.CodeText;
                }
            }

            sw.Stop();
            totalMs += sw.ElapsedMilliseconds;
        }

        // Return average time per iteration
        return totalMs / iterations;
    }
}