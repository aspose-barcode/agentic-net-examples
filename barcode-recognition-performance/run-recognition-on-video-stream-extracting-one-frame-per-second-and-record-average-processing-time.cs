// Title: Video frame barcode recognition with average processing time
// Description: Demonstrates extracting one frame per second from a video (simulated by image files), recognizing barcodes, and calculating average processing time per frame.
// Prompt: Run recognition on a video stream extracting one frame per second and record average processing time.
// Tags: barcode, video, frame extraction, performance, Aspose.BarCode, Aspose.Drawing, C#

using System;
using System.IO;
using System.Diagnostics;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Sample program that processes a series of image frames, detects barcodes, and reports average recognition time.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Loads each frame image, runs barcode recognition, and computes average processing time.
    /// </summary>
    static void Main()
    {
        // Simulated video frames (one image per second)
        string[] frameFiles = new string[]
        {
            "frame1.png",
            "frame2.png",
            "frame3.png",
            "frame4.png",
            "frame5.png"
        };

        long totalMilliseconds = 0; // Accumulates total processing time
        int processedFrames = 0;     // Counts successfully processed frames

        // Iterate over each frame file
        foreach (string filePath in frameFiles)
        {
            // Verify that the file exists before attempting to load it
            if (!File.Exists(filePath))
            {
                Console.WriteLine($"File not found: {filePath}");
                continue;
            }

            // Load the image into a bitmap object (required by the barcode reader)
            using (Bitmap bitmap = new Bitmap(filePath))
            {
                // Initialize the barcode reader for all supported symbologies
                using (BarCodeReader reader = new BarCodeReader(bitmap, DecodeType.AllSupportedTypes))
                {
                    // Use a high‑performance quality preset to speed up processing
                    reader.QualitySettings = QualitySettings.HighPerformance;

                    // Start timing the recognition operation
                    Stopwatch sw = Stopwatch.StartNew();
                    BarCodeResult[] results = reader.ReadBarCodes();
                    sw.Stop();

                    // Accumulate elapsed time and increment processed frame count
                    totalMilliseconds += sw.ElapsedMilliseconds;
                    processedFrames++;

                    // Output each detected barcode and its location within the frame
                    foreach (BarCodeResult result in results)
                    {
                        Console.WriteLine($"Frame: {Path.GetFileName(filePath)} | Type: {result.CodeTypeName} | Text: {result.CodeText}");
                        var rect = result.Region.Rectangle;
                        Console.WriteLine($"  Region: X={rect.X}, Y={rect.Y}, Width={rect.Width}, Height={rect.Height}");
                    }
                }
            }
        }

        // After processing all frames, calculate and display the average recognition time
        if (processedFrames > 0)
        {
            double averageMs = (double)totalMilliseconds / processedFrames;
            Console.WriteLine($"Processed {processedFrames} frames. Average recognition time: {averageMs:F2} ms per frame.");
        }
        else
        {
            Console.WriteLine("No frames were processed.");
        }
    }
}