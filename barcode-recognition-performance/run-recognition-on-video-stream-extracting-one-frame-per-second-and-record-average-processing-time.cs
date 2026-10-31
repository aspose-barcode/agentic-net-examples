// Title: Video frame barcode recognition with average processing time measurement
// Description: Demonstrates extracting one frame per second from a simulated video stream, recognizing barcodes in each frame, and calculating the average processing time.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It showcases how to use BarcodeGenerator to create barcode images and BarCodeReader to detect them, a common workflow for processing video streams, batch image sets, or real‑time camera feeds. Developers often need to measure performance metrics such as processing time per frame when integrating barcode scanning into multimedia applications.
// Prompt: Run recognition on a video stream extracting one frame per second and record average processing time.
// Tags: qr, barcode-recognition, png, aspose.barcode, video-processing, performance

using System;
using System.IO;
using System.Diagnostics;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing.Imaging;

/// <summary>
/// Simulates processing a video stream by generating barcode images as frames,
/// recognizing barcodes in each frame, and reporting the average processing time.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates sample frames, reads barcodes, measures performance, and cleans up resources.
    /// </summary>
    static void Main()
    {
        // --------------------------------------------------------------------
        // Create a temporary folder to hold simulated video frames.
        // --------------------------------------------------------------------
        string framesFolder = Path.Combine(Path.GetTempPath(), "VideoFrames_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(framesFolder);

        // --------------------------------------------------------------------
        // Generate sample barcode images (one per second) and store their paths.
        // --------------------------------------------------------------------
        List<string> frameFiles = new List<string>();
        int frameCount = 5; // safe sample size
        for (int i = 0; i < frameCount; i++)
        {
            string filePath = Path.Combine(framesFolder, $"frame_{i + 1}.png");
            using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.QR, $"Frame{i + 1}"))
            {
                generator.Save(filePath, BarCodeImageFormat.Png);
            }
            frameFiles.Add(filePath);
        }

        // --------------------------------------------------------------------
        // Process each frame with barcode recognition and measure elapsed time.
        // --------------------------------------------------------------------
        long totalMilliseconds = 0;
        int processedFrames = 0;

        foreach (string file in frameFiles)
        {
            if (!File.Exists(file))
            {
                Console.WriteLine($"File not found: {file}");
                continue;
            }

            using (BarCodeReader reader = new BarCodeReader(file, DecodeType.AllSupportedTypes))
            {
                Stopwatch sw = Stopwatch.StartNew();               // Start timing
                BarCodeResult[] results = reader.ReadBarCodes();   // Perform recognition
                sw.Stop();                                         // Stop timing

                totalMilliseconds += sw.ElapsedMilliseconds;
                processedFrames++;

                Console.WriteLine($"Frame: {Path.GetFileName(file)} - Time: {sw.ElapsedMilliseconds} ms - Barcodes found: {reader.FoundCount}");
                foreach (BarCodeResult result in results)
                {
                    Console.WriteLine($"  {result.CodeTypeName}: {result.CodeText}");
                }
            }
        }

        // --------------------------------------------------------------------
        // Calculate and display the average processing time per frame.
        // --------------------------------------------------------------------
        if (processedFrames > 0)
        {
            double averageMs = (double)totalMilliseconds / processedFrames;
            Console.WriteLine($"Average processing time per frame: {averageMs:F2} ms");
        }
        else
        {
            Console.WriteLine("No frames were processed.");
        }

        // --------------------------------------------------------------------
        // Clean up temporary files and folder.
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
            if (Directory.Exists(framesFolder))
            {
                Directory.Delete(framesFolder, true);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Cleanup error: {ex.Message}");
        }
    }
}