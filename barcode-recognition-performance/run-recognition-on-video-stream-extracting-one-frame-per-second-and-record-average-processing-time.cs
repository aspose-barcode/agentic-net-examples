// Title: Video stream barcode recognition with per-frame timing
// Description: Demonstrates extracting frames from a video (simulated by generated images), recognizing barcodes in each frame, and measuring average processing time.
// Category-Description: This example belongs to the Aspose.BarCode barcode recognition category, showing how to use BarCodeReader with DecodeType.AllSupportedTypes to process image sequences such as video frames. Typical use cases include real‑time video analysis, batch processing of captured frames, and performance benchmarking. Developers often need to generate or load frames, invoke the reader, and aggregate timing metrics.
// Prompt: Run recognition on a video stream extracting one frame per second and record average processing time.
// Tags: barcode, recognition, video, frame extraction, performance, aspose.barcode, barcodegenerator, barcodereader, decode

using System;
using System.IO;
using System.Diagnostics;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that simulates video frame extraction, reads barcodes from each frame,
/// and calculates the average recognition time per frame.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates sample barcode images, processes them with BarCodeReader,
    /// and reports timing statistics.
    /// </summary>
    static void Main()
    {
        // Create a temporary folder to store simulated video frame images
        string tempFolder = Path.Combine(Path.GetTempPath(), "VideoFrames_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Generate sample barcode images (each image represents a video frame)
        int frameCount = 5;
        for (int i = 0; i < frameCount; i++)
        {
            string text = $"Frame{i + 1}";
            string filePath = Path.Combine(tempFolder, $"frame_{i + 1}.png");
            using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.QR, text))
            {
                generator.Save(filePath, BarCodeImageFormat.Png);
            }
        }

        // Variables to accumulate total processing time and count of processed frames
        long totalMilliseconds = 0;
        int processedFrames = 0;

        // Build an array of file paths for the generated frames
        string[] frameFiles = new string[frameCount];
        for (int i = 0; i < frameCount; i++)
        {
            frameFiles[i] = Path.Combine(tempFolder, $"frame_{i + 1}.png");
        }

        // Iterate over each frame, recognize barcodes, and measure the time taken
        foreach (string framePath in frameFiles)
        {
            if (!File.Exists(framePath))
            {
                Console.WriteLine($"File not found: {framePath}");
                continue;
            }

            using (BarCodeReader reader = new BarCodeReader(framePath, DecodeType.AllSupportedTypes))
            {
                // Start timing before reading barcodes
                Stopwatch sw = Stopwatch.StartNew();
                BarCodeResult[] results = reader.ReadBarCodes();
                sw.Stop();

                // Accumulate timing data
                totalMilliseconds += sw.ElapsedMilliseconds;
                processedFrames++;

                Console.WriteLine($"Processed {Path.GetFileName(framePath)} - Time: {sw.ElapsedMilliseconds} ms - Detected: {results.Length}");
                foreach (BarCodeResult result in results)
                {
                    Console.WriteLine($"  {result.CodeTypeName}: {result.CodeText}");
                }
            }
        }

        // Calculate and display the average recognition time per frame
        if (processedFrames > 0)
        {
            double averageMs = (double)totalMilliseconds / processedFrames;
            Console.WriteLine($"Average recognition time per frame: {averageMs:F2} ms");
        }
        else
        {
            Console.WriteLine("No frames were processed.");
        }

        // Clean up temporary files and folder
        try
        {
            Directory.Delete(tempFolder, true);
        }
        catch
        {
            // Ignore any errors during cleanup
        }
    }
}