// Title: Video Stream Barcode Recognition with Per-Second Frame Extraction
// Description: Demonstrates extracting one frame per second from a video-like sequence, recognizing barcodes in each frame, and calculating the average processing time.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category, showcasing how to create barcode images (using BarcodeGenerator) and read them (using BarCodeReader). Typical scenarios include processing video streams, live camera feeds, or batch image sets where each frame must be analyzed for barcodes. Developers often need to configure multithreading, handle multiple symbologies, and measure performance, which this sample illustrates.
// Prompt: Run recognition on a video stream extracting one frame per second and record average processing time.
// Tags: barcode, recognition, video, performance, multithreading, aspose.barcode, generation, decoding

using System;
using System.IO;
using System.Diagnostics;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.BarCode;

/// <summary>
/// Sample program that generates barcode images, simulates video frames, recognizes them, and reports average processing time.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates sample barcodes, processes each as a video frame, and outputs timing statistics.
    /// </summary>
    static void Main()
    {
        // Create a temporary folder to store generated barcode images (simulated video frames)
        string tempFolder = Path.Combine(Path.GetTempPath(), "BarcodeVideoSample_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Define sample data: each tuple represents a frame with a specific barcode symbology and text
        var samples = new (BaseEncodeType Encode, string Text)[]
        {
            (EncodeTypes.QR, "Frame1"),
            (EncodeTypes.Code128, "Frame2"),
            (EncodeTypes.DataMatrix, "Frame3"),
            (EncodeTypes.Pdf417, "Frame4"),
            (EncodeTypes.Aztec, "Frame5")
        };

        // Generate barcode images (one per "frame") and store their file paths
        var imageFiles = new string[samples.Length];
        for (int i = 0; i < samples.Length; i++)
        {
            string filePath = Path.Combine(tempFolder, $"frame_{i + 1}.png");
            using (var generator = new BarcodeGenerator(samples[i].Encode, samples[i].Text))
            {
                generator.Save(filePath, BarCodeImageFormat.Png);
            }
            imageFiles[i] = filePath;
        }

        // Variables to accumulate total processing time and count processed frames
        double totalMilliseconds = 0;
        int processedFrames = 0;

        // Optional: enable multithreading to use all CPU cores for faster recognition
        BarCodeReader.ProcessorSettings.UseAllCores = true;

        // Iterate over each generated image, recognize barcodes, and measure elapsed time
        foreach (string imagePath in imageFiles)
        {
            if (!File.Exists(imagePath))
                continue;

            using (var reader = new BarCodeReader(imagePath, DecodeType.AllSupportedTypes))
            {
                Stopwatch sw = Stopwatch.StartNew();
                BarCodeResult[] results = reader.ReadBarCodes();
                sw.Stop();

                totalMilliseconds += sw.Elapsed.TotalMilliseconds;
                processedFrames++;

                Console.WriteLine($"Processed {Path.GetFileName(imagePath)} - Detected {results.Length} barcode(s)");
                foreach (var result in results)
                {
                    Console.WriteLine($"  Type: {result.CodeTypeName}, Text: {result.CodeText}");
                }
            }
        }

        // Calculate and display the average processing time per frame
        if (processedFrames > 0)
        {
            double averageMs = totalMilliseconds / processedFrames;
            Console.WriteLine($"Average processing time per frame: {averageMs:F2} ms");
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
            // Ignore cleanup errors
        }
    }
}