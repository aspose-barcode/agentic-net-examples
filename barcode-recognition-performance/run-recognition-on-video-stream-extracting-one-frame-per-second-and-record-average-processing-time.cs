using System;
using System.IO;
using System.Diagnostics;
using System.Collections.Generic;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating barcode images, treating them as video frames,
/// and measuring the time required to recognize barcodes in each frame.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates sample barcode frames, reads them, reports detection results,
    /// calculates average processing time, and cleans up temporary files.
    /// </summary>
    static void Main()
    {
        // Number of frames to simulate (one per second)
        const int frameCount = 5;

        // Temporary directory for generated barcode images
        string tempDir = Path.Combine(Path.GetTempPath(), "AsposeBarcodeVideoDemo");
        if (!Directory.Exists(tempDir))
        {
            Directory.CreateDirectory(tempDir);
        }

        // Generate sample barcode images to act as video frames
        List<string> framePaths = new List<string>();
        for (int i = 0; i < frameCount; i++)
        {
            // Build file path for the current frame
            string filePath = Path.Combine(tempDir, $"frame_{i}.png");

            // Create a barcode image with unique text for each frame
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, $"Sample{i}"))
            {
                generator.Save(filePath);
            }

            // Store the path for later processing
            framePaths.Add(filePath);
        }

        // Variables for timing statistics
        double totalMilliseconds = 0;
        int processedFrames = 0;

        // Process each generated frame
        foreach (string framePath in framePaths)
        {
            // Verify the frame file exists before attempting to read it
            if (!File.Exists(framePath))
            {
                Console.WriteLine($"Frame file not found: {framePath}");
                continue;
            }

            // Start timing the recognition operation
            var stopwatch = Stopwatch.StartNew();

            // Read and decode all supported barcode types from the image
            using (var reader = new BarCodeReader(framePath, DecodeType.AllSupportedTypes))
            {
                foreach (var result in reader.ReadBarCodes())
                {
                    // Output detected barcode type and its text value
                    Console.WriteLine($"Detected [{result.CodeTypeName}]: {result.CodeText}");
                }
            }

            // Stop timing and accumulate elapsed time
            stopwatch.Stop();
            totalMilliseconds += stopwatch.Elapsed.TotalMilliseconds;
            processedFrames++;
        }

        // Report average recognition time if any frames were processed
        if (processedFrames > 0)
        {
            double averageMs = totalMilliseconds / processedFrames;
            Console.WriteLine($"Processed {processedFrames} frames. Average recognition time: {averageMs:F2} ms");
        }
        else
        {
            Console.WriteLine("No frames were processed.");
        }

        // Clean up temporary barcode image files
        foreach (string path in framePaths)
        {
            try
            {
                File.Delete(path);
            }
            catch
            {
                // Ignore any deletion errors
            }
        }

        // Attempt to delete the temporary directory
        try
        {
            Directory.Delete(tempDir);
        }
        catch
        {
            // Ignore any deletion errors
        }
    }
}