// Title: Read barcodes from video frames using a background worker and processor settings
// Description: Demonstrates how to generate sample barcode images, configure Aspose.BarCode processor settings for multi‑core usage, and read the barcodes asynchronously with a BackgroundWorker.
// Category-Description: This example belongs to the Aspose.BarCode barcode recognition category, showcasing the use of BarCodeReader with ProcessorSettings for optimal core utilization. It illustrates typical scenarios such as processing video streams or large image batches where developers need high‑performance, multi‑threaded barcode decoding using classes like BarCodeReader, BarcodeGenerator, and BackgroundWorker.
// Prompt: Create a background worker that reads barcodes from a video stream using ProcessorSettings for optimal core usage.
// Tags: barcode, qr, recognition, backgroundworker, multithreading, processorsettings, aspnet, aspose.barcode

using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates reading barcodes from a simulated video stream using a background worker and processor settings for optimal core usage.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates sample QR code frames, configures processor settings, and processes the frames asynchronously.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for sample barcode images
        string tempFolder = Path.Combine(Path.GetTempPath(), "BarcodeVideo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Generate sample barcode images (simulating video frames)
        List<string> frameFiles = new List<string>();
        for (int i = 1; i <= 5; i++)
        {
            string codeText = "Frame" + i;
            string filePath = Path.Combine(tempFolder, $"frame_{i}.png");
            using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.QR, codeText))
            {
                generator.Save(filePath, BarCodeImageFormat.Png);
            }
            frameFiles.Add(filePath);
        }

        // Configure processor settings for optimal core usage
        BarCodeReader.ProcessorSettings.UseAllCores = true;
        BarCodeReader.ProcessorSettings.MaxAdditionalAllowedThreads = Environment.ProcessorCount * 2;

        // Set up a background worker to read barcodes from the generated frames
        using (BackgroundWorker worker = new BackgroundWorker())
        {
            // Signal when processing is complete
            ManualResetEventSlim completedEvent = new ManualResetEventSlim(false);

            // Define the work to be performed on a background thread
            worker.DoWork += (sender, e) =>
            {
                List<string> files = (List<string>)e.Argument;
                foreach (string file in files)
                {
                    try
                    {
                        // Read all supported barcodes from the current frame
                        using (BarCodeReader reader = new BarCodeReader(file, DecodeType.AllSupportedTypes))
                        {
                            BarCodeResult[] results = reader.ReadBarCodes();
                            foreach (BarCodeResult result in results)
                            {
                                Console.WriteLine($"File {Path.GetFileName(file)}: {result.CodeTypeName} - {result.CodeText}");
                            }
                        }
                    }
                    catch (ArgumentException ex)
                    {
                        // Handle cases where the file cannot be processed
                        Console.WriteLine($"Failed to read {Path.GetFileName(file)}: {ex.Message}");
                    }
                }
            };

            // Notify when the background work has finished
            worker.RunWorkerCompleted += (sender, e) =>
            {
                completedEvent.Set();
            };

            // Start processing the list of frame files
            worker.RunWorkerAsync(frameFiles);
            // Wait for the background worker to signal completion
            completedEvent.Wait();
        }

        // Clean up temporary files
        try
        {
            Directory.Delete(tempFolder, true);
        }
        catch
        {
            // Ignored - cleanup failure should not affect program exit
        }
    }
}