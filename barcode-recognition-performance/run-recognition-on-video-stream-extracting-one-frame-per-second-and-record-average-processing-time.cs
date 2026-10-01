// Title: Video Stream Barcode Recognition with Per-Second Frame Extraction
// Description: Demonstrates extracting one frame per second from a simulated video stream, recognizing barcodes in each frame, and calculating the average processing time.
// Category-Description: This example belongs to the Aspose.BarCode video processing category. It showcases how to generate barcode images, read them using the BarCodeReader, and tune performance with processor and quality settings. Typical use cases include real‑time video analysis, batch processing of surveillance footage, and any scenario where barcodes must be detected quickly from a sequence of images. Developers often need to combine barcode generation, high‑performance recognition, and timing metrics, which this sample illustrates.
// Prompt: Run recognition on a video stream extracting one frame per second and record average processing time.
// Tags: barcode, recognition, video, performance, aspose.barcode, generation, processing, qualitysettings, decodetype

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Simulates a video stream by generating barcode images as frames,
/// reads each frame to detect barcodes, and reports average processing time.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates sample frames, recognises barcodes,
    /// measures processing time per frame, and outputs the average duration.
    /// </summary>
    /// <param name="args">Command‑line arguments (not used).</param>
    static void Main(string[] args)
    {
        // Create a temporary folder to store simulated video frames (one per second)
        string tempFolder = Path.Combine(Path.GetTempPath(), "VideoFrames_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Number of frames to simulate – each frame represents one second of video
        const int frameCount = 5;

        // Generate sample barcode images and save them as PNG files
        GenerateSampleBarcodes(tempFolder, frameCount);

        // Collect the full paths of the generated frame files
        List<string> frameFiles = new List<string>();
        for (int i = 0; i < frameCount; i++)
        {
            string filePath = Path.Combine(tempFolder, $"frame{i + 1}.png");
            if (File.Exists(filePath))
                frameFiles.Add(filePath);
        }

        // Exit early if no frames were created
        if (frameFiles.Count == 0)
        {
            Console.WriteLine("No frames were generated. Exiting.");
            return;
        }

        // List to store processing time (in milliseconds) for each frame
        List<double> processingTimes = new List<double>();

        // Optional: enable multi‑core processing for higher throughput
        BarCodeReader.ProcessorSettings.UseAllCores = true;

        // Iterate over each frame, recognise barcodes, and measure elapsed time
        foreach (string framePath in frameFiles)
        {
            // Start timing for the current frame
            Stopwatch sw = Stopwatch.StartNew();

            using (BarCodeReader reader = new BarCodeReader(framePath, DecodeType.AllSupportedTypes))
            {
                // Apply a high‑performance quality preset to speed up recognition
                reader.QualitySettings = QualitySettings.HighPerformance;

                // Perform barcode detection
                BarCodeResult[] results = reader.ReadBarCodes();

                // Output detected barcodes for demonstration purposes
                foreach (BarCodeResult result in results)
                {
                    Console.WriteLine($"File: {Path.GetFileName(framePath)} | CodeText: {result.CodeText} | Type: {result.CodeTypeName}");
                }
            }

            // Stop timing and record the elapsed milliseconds
            sw.Stop();
            processingTimes.Add(sw.Elapsed.TotalMilliseconds);
        }

        // Calculate the average processing time across all frames
        double averageTime = 0;
        if (processingTimes.Count > 0)
        {
            double sum = 0;
            foreach (double t in processingTimes) sum += t;
            averageTime = sum / processingTimes.Count;
        }

        // Report the results
        Console.WriteLine($"Processed {processingTimes.Count} frames.");
        Console.WriteLine($"Average processing time per frame: {averageTime:F2} ms");

        // Clean up temporary files and folder
        try
        {
            Directory.Delete(tempFolder, true);
        }
        catch
        {
            // If cleanup fails, ignore – the OS will eventually remove the temporary files.
        }
    }

    /// <summary>
    /// Generates a specified number of QR code images and saves them as PNG files.
    /// Each image represents a video frame.
    /// </summary>
    /// <param name="folderPath">Destination folder for the generated frames.</param>
    /// <param name="count">Number of frames (images) to create.</param>
    private static void GenerateSampleBarcodes(string folderPath, int count)
    {
        for (int i = 1; i <= count; i++)
        {
            // Create a QR code containing sample text for the current frame
            using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.QR, $"SampleFrame{i}"))
            {
                // Optional: adjust the X‑dimension (module size) of the barcode
                generator.Parameters.Barcode.XDimension.Point = 2.5f;

                string fileName = Path.Combine(folderPath, $"frame{i}.png");
                generator.Save(fileName, BarCodeImageFormat.Png);
            }
        }
    }
}