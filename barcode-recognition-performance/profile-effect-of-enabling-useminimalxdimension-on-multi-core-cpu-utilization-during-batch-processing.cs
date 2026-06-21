using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates batch generation and recognition of barcodes using Aspose.BarCode.
/// </summary>
class Program
{
    // Number of barcodes to process in the batch (small size for the runner)
    const int BatchSize = 5;

    /// <summary>
    /// Entry point of the application.
    /// Generates a batch of barcode images, then processes them twice:
    /// once with the default XDimension mode and once with the minimal XDimension mode.
    /// </summary>
    static void Main()
    {
        // Generate a list of barcode images stored in memory streams.
        List<MemoryStream> barcodeImages = GenerateBarcodes();

        // Configure the reader to use all available processor cores.
        BarCodeReader.ProcessorSettings.UseOnlyThisCoresCount = Environment.ProcessorCount;

        // Process the batch using the default XDimension mode and measure elapsed time.
        TimeSpan defaultTime = ProcessBatch(barcodeImages, useMinimalXDimension: false);
        Console.WriteLine($"Default XDimension mode elapsed: {defaultTime.TotalMilliseconds} ms");

        // Process the batch using the minimal XDimension mode and measure elapsed time.
        TimeSpan minimalTime = ProcessBatch(barcodeImages, useMinimalXDimension: true);
        Console.WriteLine($"UseMinimalXDimension mode elapsed: {minimalTime.TotalMilliseconds} ms");
    }

    /// <summary>
    /// Generates a list of <see cref="MemoryStream"/> objects, each containing a PNG image of a Code128 barcode.
    /// </summary>
    /// <returns>A list of memory streams with generated barcode images.</returns>
    static List<MemoryStream> GenerateBarcodes()
    {
        var streams = new List<MemoryStream>();

        // Create the specified number of barcode images.
        for (int i = 0; i < BatchSize; i++)
        {
            // Initialize a barcode generator with unique text for each barcode.
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, $"CODE{i:D4}"))
            {
                // Save the generated barcode to a memory stream in PNG format.
                var ms = new MemoryStream();
                generator.Save(ms, BarCodeImageFormat.Png);
                ms.Position = 0; // Reset stream position for later reading.
                streams.Add(ms);
            }
        }

        return streams;
    }

    /// <summary>
    /// Reads all barcodes from the provided image streams and returns the total processing time.
    /// </summary>
    /// <param name="images">The list of memory streams containing barcode images.</param>
    /// <param name="useMinimalXDimension">
    /// If true, configures the reader to use <see cref="XDimensionMode.UseMinimalXDimension"/> with a minimal value;
    /// otherwise uses the default <see cref="XDimensionMode.Auto"/>.
    /// </param>
    /// <returns>The <see cref="TimeSpan"/> representing the elapsed processing time.</returns>
    static TimeSpan ProcessBatch(List<MemoryStream> images, bool useMinimalXDimension)
    {
        // Start measuring elapsed time.
        var stopwatch = Stopwatch.StartNew();

        // Iterate over each image stream in the batch.
        foreach (var imageStream in images)
        {
            // Ensure the stream is positioned at the beginning before each read operation.
            imageStream.Position = 0;

            // Create a barcode reader for the current image stream.
            using (var reader = new BarCodeReader(imageStream, DecodeType.AllSupportedTypes))
            {
                // Configure quality settings based on the requested XDimension mode.
                if (useMinimalXDimension)
                {
                    reader.QualitySettings.XDimension = XDimensionMode.UseMinimalXDimension;
                    // Example minimal X dimension (in pixels).
                    reader.QualitySettings.MinimalXDimension = 2f;
                }
                else
                {
                    // Default mode (Auto) lets the library determine the optimal X dimension.
                    reader.QualitySettings.XDimension = XDimensionMode.Auto;
                }

                // Read all barcodes from the image.
                foreach (var result in reader.ReadBarCodes())
                {
                    // Output the decoded text to demonstrate successful recognition.
                    Console.WriteLine($"Decoded: {result.CodeText}");
                }
            }
        }

        // Stop the timer and return the elapsed time.
        stopwatch.Stop();
        return stopwatch.Elapsed;
    }
}