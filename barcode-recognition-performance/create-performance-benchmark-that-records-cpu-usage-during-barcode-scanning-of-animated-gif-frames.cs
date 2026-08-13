// Title: Barcode scanning performance benchmark for animated GIF frames
// Description: Demonstrates measuring CPU usage while scanning each frame of an animated GIF for barcodes using Aspose.BarCode.
// Category-Description: This example belongs to the Aspose.BarCode recognition category, illustrating how to process animated images frame‑by‑frame. It showcases the BarCodeReader class with DecodeType.AllSupportedTypes, typical for developers who need to benchmark or optimize barcode detection in multi‑frame media such as GIFs, videos, or image sequences. The snippet is useful for performance testing, CI pipelines, and real‑time scanning scenarios.
// Prompt: Create a performance benchmark that records CPU usage during barcode scanning of animated GIF frames.
// Tags: barcode, scanning, performance, cpu, animated gif, aspose.barcode, barcodereader, decode type, benchmark

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Provides a console application that benchmarks CPU usage while scanning each frame of an animated GIF for barcodes.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the benchmark application.
    /// </summary>
    static void Main()
    {
        // Path to the animated GIF file. Replace with an existing file or ensure it exists.
        const string gifPath = "sample.gif";

        // Verify that the GIF file exists before proceeding.
        if (!File.Exists(gifPath))
        {
            Console.WriteLine($"Animated GIF not found at '{Path.GetFullPath(gifPath)}'. Benchmark will not run.");
            return;
        }

        // Load the animated GIF using Aspose.Drawing.
        using (Image gifImage = Image.FromFile(gifPath))
        {
            // Determine the number of frames in the GIF (time dimension).
            int frameCount = gifImage.GetFrameCount(FrameDimension.Time);
            Console.WriteLine($"Animated GIF contains {frameCount} frame(s).");

            var cpuUsages = new List<TimeSpan>();

            // Iterate through each frame and measure CPU time for barcode scanning.
            for (int i = 0; i < frameCount; i++)
            {
                // Select the current frame.
                gifImage.SelectActiveFrame(FrameDimension.Time, i);

                // Save the current frame to a memory stream in PNG format.
                using (var frameStream = new MemoryStream())
                {
                    gifImage.Save(frameStream, ImageFormat.Png);
                    frameStream.Position = 0;

                    // Capture CPU time before barcode scanning.
                    Process proc = Process.GetCurrentProcess();
                    TimeSpan cpuStart = proc.TotalProcessorTime;

                    // Scan the frame for all supported barcode types.
                    using (var reader = new BarCodeReader(frameStream, DecodeType.AllSupportedTypes))
                    {
                        foreach (var result in reader.ReadBarCodes())
                        {
                            // The read operation is performed for benchmarking; result handling is optional.
                        }
                    }

                    // Capture CPU time after scanning and calculate usage for this frame.
                    TimeSpan cpuEnd = proc.TotalProcessorTime;
                    cpuUsages.Add(cpuEnd - cpuStart);
                }
            }

            // Compute total and average CPU usage across all frames.
            TimeSpan totalCpu = new TimeSpan();
            foreach (var usage in cpuUsages)
                totalCpu += usage;

            double averageMs = cpuUsages.Count > 0 ? totalCpu.TotalMilliseconds / cpuUsages.Count : 0;

            Console.WriteLine($"Total CPU time for scanning {cpuUsages.Count} frame(s): {totalCpu.TotalMilliseconds:F2} ms");
            Console.WriteLine($"Average CPU time per frame: {averageMs:F2} ms");
        }
    }
}