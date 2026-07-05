// Title: Parallel Barcode Scanning with CPU Utilization Measurement
// Description: Generates a set of barcode images, scans them in parallel, and reports CPU core utilization.
// Prompt: Measure CPU core utilization while scanning a large batch of 10,000 barcode images in parallel.
// Tags: code128, barcode generation, barcode recognition, parallel processing, cpu utilization, aspose.barcode

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates parallel barcode recognition while measuring CPU core utilization.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates sample barcode images, processes them in parallel,
    /// and outputs processing time and CPU utilization statistics.
    /// </summary>
    /// <param name="args">Command‑line arguments (not used).</param>
    static void Main(string[] args)
    {
        // Number of sample barcode images (small safe size for the runner)
        int sampleSize = 5;

        // Generate barcode images in memory
        List<byte[]> barcodeImages = new List<byte[]>();
        for (int i = 0; i < sampleSize; i++)
        {
            // Create a unique code text for each barcode
            string codeText = $"CODE{i + 1:D4}";
            using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
            {
                // Optional visual settings
                generator.Parameters.AutoSizeMode = AutoSizeMode.Interpolation;
                generator.Parameters.ImageWidth.Point = 300f;
                generator.Parameters.ImageHeight.Point = 150f;

                // Generate the barcode image as a bitmap
                using (Bitmap bitmap = generator.GenerateBarCodeImage())
                {
                    // Save bitmap to a memory stream in PNG format
                    using (MemoryStream ms = new MemoryStream())
                    {
                        bitmap.Save(ms, Aspose.Drawing.Imaging.ImageFormat.Png);
                        barcodeImages.Add(ms.ToArray());
                    }
                }
            }
        }

        // Enable multi‑core processing for BarCodeReader
        BarCodeReader.ProcessorSettings.UseAllCores = true;

        int totalBarcodes = 0;                     // Counter for successfully read barcodes
        int coreCount = Environment.ProcessorCount; // Number of logical CPU cores

        // Measure CPU time and elapsed wall‑clock time for the recognition phase
        using (Process process = Process.GetCurrentProcess())
        {
            TimeSpan cpuStart = process.TotalProcessorTime;
            Stopwatch sw = Stopwatch.StartNew();

            // Parallel recognition of the generated images
            Parallel.ForEach(barcodeImages, imageData =>
            {
                using (MemoryStream ms = new MemoryStream(imageData))
                {
                    using (BarCodeReader reader = new BarCodeReader(ms, DecodeType.Code128))
                    {
                        // Iterate over all detected barcodes in the image
                        foreach (var result in reader.ReadBarCodes())
                        {
                            Interlocked.Increment(ref totalBarcodes);
                        }
                    }
                }
            });

            sw.Stop();
            TimeSpan cpuEnd = process.TotalProcessorTime;
            TimeSpan cpuUsed = cpuEnd - cpuStart;

            // Calculate CPU utilization as a percentage of total available core time
            double cpuUtilization = (cpuUsed.TotalMilliseconds /
                                    (sw.Elapsed.TotalMilliseconds * coreCount)) * 100.0;

            // Output results
            Console.WriteLine($"Processed {totalBarcodes} barcodes in {sw.Elapsed.TotalSeconds:F2} seconds.");
            Console.WriteLine($"CPU cores available: {coreCount}");
            Console.WriteLine($"CPU utilization: {cpuUtilization:F2}%");
        }
    }
}