// Title: Background Worker Barcode Reader from Video Stream
// Description: Demonstrates reading barcodes from simulated video frames using a BackgroundWorker and ProcessorSettings to control core usage.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category, showcasing how to generate barcodes, process them in a background thread, and fine‑tune multi‑core utilization via ProcessorSettings. Developers often need to handle high‑throughput image streams (e.g., video) and require optimal CPU usage while recognizing multiple symbologies using BarCodeReader, BarcodeGenerator, and QualitySettings.
// Prompt: Create a background worker that reads barcodes from a video stream using ProcessorSettings for optimal core usage.
// Tags: code128, read, console, barcodegenerator, barcodereader, processorsettings, qualitysettings

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Threading;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that generates barcode images, simulates video frames,
/// and reads them in a background worker using Aspose.BarCode APIs.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates sample frames, configures processor settings,
    /// and processes frames asynchronously.
    /// </summary>
    static void Main()
    {
        // Generate a few barcode images to simulate video frames
        var frames = new List<byte[]>();
        for (int i = 0; i < 3; i++)
        {
            // Create a barcode generator for Code128 with unique text
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, $"Sample{i + 1}"))
            {
                // Set a simple visual dimension
                generator.Parameters.Barcode.XDimension.Point = 2f;
                // Render the barcode to a bitmap
                using (var bitmap = generator.GenerateBarCodeImage())
                {
                    // Save bitmap to memory stream as PNG
                    using (var ms = new MemoryStream())
                    {
                        bitmap.Save(ms, ImageFormat.Png);
                        frames.Add(ms.ToArray());
                    }
                }
            }
        }

        // Synchronization primitive to wait for background work completion
        var doneEvent = new ManualResetEventSlim(false);

        // BackgroundWorker that processes the simulated video frames
        var worker = new BackgroundWorker();
        worker.DoWork += (sender, args) =>
        {
            // Configure processor settings for optimal core usage
            BarCodeReader.ProcessorSettings.UseAllCores = false;
            BarCodeReader.ProcessorSettings.UseOnlyThisCoresCount = Math.Max(1, Environment.ProcessorCount / 2);

            // Iterate over each simulated frame
            foreach (var frameData in frames)
            {
                // Create a memory stream from the frame bytes
                using (var stream = new MemoryStream(frameData))
                // Initialize the barcode reader for all supported symbologies
                using (var reader = new BarCodeReader(stream, DecodeType.AllSupportedTypes))
                {
                    // Apply a high‑performance quality preset
                    reader.QualitySettings = QualitySettings.HighPerformance;

                    // Read and output all detected barcodes
                    foreach (var result in reader.ReadBarCodes())
                    {
                        Console.WriteLine($"Detected: {result.CodeTypeName} - {result.CodeText}");
                    }
                }
            }
        };
        // Signal completion when background work finishes
        worker.RunWorkerCompleted += (s, e) => doneEvent.Set();

        // Start processing and wait until it finishes
        worker.RunWorkerAsync();
        doneEvent.Wait();
    }
}