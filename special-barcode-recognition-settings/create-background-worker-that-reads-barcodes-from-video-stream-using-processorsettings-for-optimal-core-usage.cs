// Title: Read barcodes from video frames using a background worker
// Description: Demonstrates how to generate sample barcode images, configure processor settings for multi‑core usage, and read them asynchronously with BackgroundWorker.
// Category-Description: This example belongs to the Aspose.BarCode barcode recognition category, showcasing the use of BarCodeReader with ProcessorSettings for optimal core utilization. It illustrates typical scenarios such as processing video streams or large image batches, where developers need high‑performance, multi‑threaded barcode decoding using classes like BarCodeReader, BarcodeGenerator, and BackgroundWorker.
// Prompt: Create a background worker that reads barcodes from a video stream using ProcessorSettings for optimal core usage.
// Tags: barcode, code128, backgroundworker, multithreading, processorsettings, videostream, aspose.barcode, recognition

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Threading;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Sample program that generates barcode images, configures multi‑core processing,
/// and reads the barcodes asynchronously using a BackgroundWorker.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates temporary barcode frames, sets processor settings,
    /// runs a background worker to decode each frame, and cleans up afterwards.
    /// </summary>
    static void Main()
    {
        // --------------------------------------------------------------------
        // 1. Create a temporary folder to hold simulated video frame images.
        // --------------------------------------------------------------------
        string tempFolder = Path.Combine(Path.GetTempPath(), "BarcodeVideoFrames_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // --------------------------------------------------------------------
        // 2. Generate sample barcode images (each image represents a video frame).
        // --------------------------------------------------------------------
        List<string> frameFiles = new List<string>();
        int frameCount = 5;
        for (int i = 0; i < frameCount; i++)
        {
            string codeText = $"FRAME{i + 1}";
            string filePath = Path.Combine(tempFolder, $"frame_{i + 1}.png");
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
            {
                generator.Save(filePath, BarCodeImageFormat.Png);
            }
            frameFiles.Add(filePath);
        }

        // --------------------------------------------------------------------
        // 3. Configure ProcessorSettings for optimal core usage.
        //    - UseAllCores enables utilization of all logical processors.
        //    - MaxAdditionalAllowedThreads defines extra threads beyond the default pool.
        // --------------------------------------------------------------------
        BarCodeReader.ProcessorSettings.UseAllCores = true;
        BarCodeReader.ProcessorSettings.MaxAdditionalAllowedThreads = Environment.ProcessorCount * 2;
        // Optional: limit to a specific number of cores
        // BarCodeReader.ProcessorSettings.UseOnlyThisCoresCount = Environment.ProcessorCount;

        // --------------------------------------------------------------------
        // 4. Set up a BackgroundWorker to read barcodes from the simulated video stream.
        // --------------------------------------------------------------------
        using (BackgroundWorker worker = new BackgroundWorker())
        {
            ManualResetEvent doneEvent = new ManualResetEvent(false);

            // DoWork: iterate over each frame file and decode barcodes.
            worker.DoWork += (sender, e) =>
            {
                foreach (string file in frameFiles)
                {
                    if (!File.Exists(file))
                    {
                        Console.WriteLine($"File not found: {file}");
                        continue;
                    }

                    using (var reader = new BarCodeReader(file, DecodeType.AllSupportedTypes))
                    {
                        BarCodeResult[] results = reader.ReadBarCodes();
                        foreach (BarCodeResult result in results)
                        {
                            Console.WriteLine($"{Path.GetFileName(file)}: {result.CodeTypeName} - {result.CodeText}");
                        }
                    }
                }
            };

            // RunWorkerCompleted: signal that processing is finished.
            worker.RunWorkerCompleted += (sender, e) =>
            {
                Console.WriteLine("Barcode processing completed.");
                doneEvent.Set();
            };

            // Start the background operation.
            worker.RunWorkerAsync();

            // Wait for the background worker to finish before proceeding.
            doneEvent.WaitOne();
        }

        // --------------------------------------------------------------------
        // 5. Clean up temporary files and folder.
        // --------------------------------------------------------------------
        try
        {
            foreach (string file in frameFiles)
            {
                if (File.Exists(file))
                {
                    File.Delete(file);
                }
            }

            if (Directory.Exists(tempFolder))
            {
                Directory.Delete(tempFolder, true);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Cleanup error: {ex.Message}");
        }
    }
}