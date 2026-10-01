// Title: Barcode scanning benchmark for animated GIF frames
// Description: Demonstrates how to measure CPU usage while scanning each frame of an animated GIF for barcodes using Aspose.BarCode.
// Category-Description: This example belongs to the Aspose.BarCode performance benchmarking category. It shows how to load GIF images with Aspose.Drawing, iterate through frames, and use BarCodeReader to detect barcodes. Developers often need to evaluate processing time for barcode recognition in multi‑frame images such as animated GIFs, and this snippet provides a template for measuring CPU consumption per frame.
// Prompt: Create a performance benchmark that records CPU usage during barcode scanning of animated GIF frames.
// Tags: barcode, performance, benchmark, cpu, gif, animation, barcodereader, aspose.barcode, aspose.drawing

using System;
using System.Diagnostics;
using System.IO;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates a performance benchmark that records CPU usage while scanning barcodes
/// in each frame of an animated GIF using Aspose.BarCode and Aspose.Drawing.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that creates a sample GIF, iterates through its frames,
    /// reads barcodes, and measures CPU time for each frame.
    /// </summary>
    static void Main()
    {
        // Create a temporary folder for the sample GIF
        string tempFolder = Path.Combine(Path.GetTempPath(), "GifBenchmark_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);
        string gifPath = Path.Combine(tempFolder, "sample.gif");

        // Write a simple (static) GIF file if it does not exist.
        // This satisfies the requirement of having a GIF to process.
        if (!File.Exists(gifPath))
        {
            // 1x1 pixel transparent GIF (base64)
            const string base64Gif = "R0lGODlhAQABAPAAAP///wAAACH5BAAAAAAALAAAAAABAAEAAAICRAEAOw==";
            byte[] gifBytes = Convert.FromBase64String(base64Gif);
            File.WriteAllBytes(gifPath, gifBytes);
        }

        if (!File.Exists(gifPath))
        {
            Console.WriteLine("Failed to create sample GIF.");
            return;
        }

        // Load the GIF using Aspose.Drawing
        using (Image gifImage = Image.FromFile(gifPath))
        {
            // Determine the number of frames (for a static GIF this will be 1)
            int frameCount = gifImage.GetFrameCount(FrameDimension.Time);
            Console.WriteLine($"Total frames in GIF: {frameCount}");

            // Variables to accumulate CPU usage
            TimeSpan totalCpuTime = TimeSpan.Zero;

            // Process each frame
            for (int i = 0; i < frameCount; i++)
            {
                // Select the current frame
                gifImage.SelectActiveFrame(FrameDimension.Time, i);

                // Save the current frame to a memory stream (PNG format)
                using (var frameStream = new MemoryStream())
                {
                    gifImage.Save(frameStream, ImageFormat.Png);
                    frameStream.Position = 0;

                    // Measure CPU time before barcode reading
                    Process currentProcess = Process.GetCurrentProcess();
                    TimeSpan cpuBefore = currentProcess.TotalProcessorTime;

                    // Read barcodes from the frame
                    using (var reader = new BarCodeReader(frameStream, DecodeType.AllSupportedTypes))
                    {
                        // Iterate through all detected barcodes (if any)
                        foreach (var result in reader.ReadBarCodes())
                        {
                            Console.WriteLine($"Frame {i + 1}: Detected barcode - Type: {result.CodeTypeName}, Text: {result.CodeText}");
                        }
                    }

                    // Measure CPU time after barcode reading
                    TimeSpan cpuAfter = currentProcess.TotalProcessorTime;
                    TimeSpan cpuDelta = cpuAfter - cpuBefore;
                    totalCpuTime += cpuDelta;

                    Console.WriteLine($"Frame {i + 1}: CPU time used = {cpuDelta.TotalMilliseconds} ms");
                }
            }

            // Compute average CPU usage per frame
            double averageCpuMs = totalCpuTime.TotalMilliseconds / Math.Max(frameCount, 1);
            Console.WriteLine($"Average CPU time per frame: {averageCpuMs:F2} ms");
        }

        // Clean up temporary files
        try
        {
            File.Delete(gifPath);
            Directory.Delete(tempFolder);
        }
        catch
        {
            // Ignored – cleanup failure should not affect benchmark result
        }
    }
}