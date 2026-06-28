using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating barcode images, treating them as video frames,
/// and processing each frame to recognize barcodes using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Configures processor settings, generates sample frames, and processes them asynchronously.
    /// </summary>
    static void Main()
    {
        // Use all available CPU cores for barcode processing (optimal performance)
        BarCodeReader.ProcessorSettings.UseOnlyThisCoresCount = Environment.ProcessorCount;

        // Generate a small collection of barcode images that simulate video frames
        List<MemoryStream> frameStreams = GenerateSampleFrames(5);

        // Run the frame processing on a background task (simulating a background worker)
        Task processingTask = Task.Run(() => ProcessFrames(frameStreams));

        // Wait for the background task to complete before exiting the program
        processingTask.Wait();
    }

    /// <summary>
    /// Generates the specified number of barcode images and returns them as memory streams.
    /// </summary>
    /// <param name="count">Number of barcode frames to generate.</param>
    /// <returns>List of memory streams containing PNG barcode images.</returns>
    private static List<MemoryStream> GenerateSampleFrames(int count)
    {
        var frames = new List<MemoryStream>();

        for (int i = 0; i < count; i++)
        {
            // Create a unique code text for each frame (e.g., FRAME01, FRAME02, ...)
            string codeText = $"FRAME{i + 1:D2}";

            // Initialize a barcode generator for Code128 with the specified text
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
            {
                // Set a consistent image size for all generated barcodes
                generator.Parameters.ImageWidth.Point = 300f;
                generator.Parameters.ImageHeight.Point = 150f;

                // Save the generated barcode image to a memory stream in PNG format
                var ms = new MemoryStream();
                generator.Save(ms, BarCodeImageFormat.Png);
                ms.Position = 0; // Reset stream position for subsequent reading

                // Add the memory stream to the collection of frames
                frames.Add(ms);
            }
        }

        return frames;
    }

    /// <summary>
    /// Processes each barcode frame, reads any barcodes present, and writes the results to the console.
    /// </summary>
    /// <param name="frameStreams">List of memory streams representing barcode frames.</param>
    private static void ProcessFrames(List<MemoryStream> frameStreams)
    {
        int frameIndex = 0;

        // Iterate over each frame stream
        foreach (var stream in frameStreams)
        {
            frameIndex++;

            // Load the image from the memory stream
            using (var bitmap = new Bitmap(stream))
            {
                // Initialize a barcode reader that supports all barcode types
                using (var reader = new BarCodeReader(bitmap, DecodeType.AllSupportedTypes))
                {
                    // Apply a high-performance quality preset for faster processing
                    reader.QualitySettings = QualitySettings.HighPerformance;

                    // Read all barcodes found in the current frame
                    foreach (var result in reader.ReadBarCodes())
                    {
                        // Output the detected barcode type and its text value
                        Console.WriteLine($"Frame {frameIndex}: Detected {result.CodeTypeName} - Text: {result.CodeText}");
                    }
                }
            }

            // Release the memory stream resources after processing the frame
            stream.Dispose();
        }
    }
}