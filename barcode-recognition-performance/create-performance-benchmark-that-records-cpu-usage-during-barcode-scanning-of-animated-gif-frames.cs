// Title: CPU Usage Benchmark for Barcode Scanning in Animated GIF Frames
// Description: Demonstrates how to measure CPU time while recognizing barcodes in each frame of an animated GIF using Aspose.BarCode.
// Prompt: Create a performance benchmark that records CPU usage during barcode scanning of animated GIF frames.
// Tags: barcode, scanning, animated gif, cpu benchmark, aspose.barcode, c#

using System;
using System.Diagnostics;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Provides a simple console application that benchmarks CPU usage while scanning barcodes
/// from each frame of an animated GIF. The example shows how to extract frames, feed them
/// to Aspose.BarCode, and report per‑frame processing time and detection results.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the benchmark application.
    /// </summary>
    static void Main()
    {
        // Path to the animated GIF containing barcodes.
        const string gifPath = "sample.gif";

        // Verify that the GIF file exists before proceeding.
        if (!File.Exists(gifPath))
        {
            Console.WriteLine($"File not found: {gifPath}");
            Console.WriteLine("Please provide an animated GIF with barcode frames and place it beside the executable.");
            return;
        }

        // Load the animated GIF into an Aspose.Drawing.Image object.
        using (Image gifImage = Image.FromFile(gifPath))
        {
            // GIF frames are accessed via the Time dimension.
            var timeDimension = FrameDimension.Time;
            int frameCount = gifImage.GetFrameCount(timeDimension);
            Console.WriteLine($"Animated GIF contains {frameCount} frame(s).");

            // Iterate over each frame in the animated GIF.
            for (int i = 0; i < frameCount; i++)
            {
                // Select the current frame for processing.
                gifImage.SelectActiveFrame(timeDimension, i);

                // Save the selected frame to a memory stream in PNG format.
                // PNG is used because BarCodeReader expects a raster image format.
                using (var frameStream = new MemoryStream())
                {
                    gifImage.Save(frameStream, ImageFormat.Png);
                    frameStream.Position = 0; // Reset stream position for reading.

                    // Initialize the barcode reader for the current frame.
                    using (var reader = new BarCodeReader(frameStream, DecodeType.AllSupportedTypes))
                    {
                        // Record CPU time before barcode recognition starts.
                        TimeSpan cpuStart = Process.GetCurrentProcess().TotalProcessorTime;

                        // Perform barcode detection on the current frame.
                        var results = reader.ReadBarCodes();

                        // Record CPU time after barcode recognition completes.
                        TimeSpan cpuEnd = Process.GetCurrentProcess().TotalProcessorTime;
                        double cpuMilliseconds = (cpuEnd - cpuStart).TotalMilliseconds;

                        // Output per‑frame CPU usage and number of barcodes found.
                        Console.WriteLine($"Frame {i + 1}/{frameCount}: CPU time = {cpuMilliseconds:F2} ms, barcodes detected = {reader.FoundCount}");

                        // List details of each detected barcode.
                        foreach (var result in results)
                        {
                            Console.WriteLine($"  Type: {result.CodeTypeName}, Text: {result.CodeText}");
                        }
                    }
                }
            }
        }

        // Indicate that the benchmark has finished.
        Console.WriteLine("Benchmark completed.");
    }
}