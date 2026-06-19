using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating Code39 barcodes in memory and measuring
/// the performance of reading them with checksum validation enabled and disabled.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates sample barcodes, runs performance measurements, and cleans up resources.
    /// </summary>
    static void Main()
    {
        const int sampleCount = 10; // Number of barcode samples to generate

        // Store generated barcode images in memory streams
        var barcodeImages = new List<MemoryStream>();

        // ------------------------------------------------------------
        // Generate sample Code39 barcodes in memory
        // ------------------------------------------------------------
        for (int i = 0; i < sampleCount; i++)
        {
            // Create a barcode generator for Code39FullASCII with a unique text value
            using (var generator = new BarcodeGenerator(EncodeTypes.Code39FullASCII, $"TEST{i:D2}"))
            {
                // Save the generated barcode to a memory stream in PNG format
                var ms = new MemoryStream();
                generator.Save(ms, BarCodeImageFormat.Png);
                ms.Position = 0; // Reset stream position for later reading
                barcodeImages.Add(ms);
            }
        }

        // ------------------------------------------------------------
        // Measure performance with checksum validation enabled
        // ------------------------------------------------------------
        MeasurePerformance("Checksum Validation: On", ChecksumValidation.On, barcodeImages);

        // ------------------------------------------------------------
        // Measure performance with checksum validation disabled
        // ------------------------------------------------------------
        MeasurePerformance("Checksum Validation: Off", ChecksumValidation.Off, barcodeImages);

        // ------------------------------------------------------------
        // Clean up memory streams
        // ------------------------------------------------------------
        foreach (var ms in barcodeImages)
        {
            ms.Dispose();
        }
    }

    /// <summary>
    /// Measures and reports the time taken to read a collection of barcode images
    /// using the specified checksum validation setting.
    /// </summary>
    /// <param name="description">Label describing the measurement scenario.</param>
    /// <param name="validation">Checksum validation mode to apply.</param>
    /// <param name="images">List of memory streams containing barcode images.</param>
    static void MeasurePerformance(string description, ChecksumValidation validation, List<MemoryStream> images)
    {
        var stopwatch = new Stopwatch(); // Timer for performance measurement
        stopwatch.Start();

        // Iterate over each barcode image stream
        foreach (var imageStream in images)
        {
            // Ensure the stream is positioned at the beginning before each read
            imageStream.Position = 0;

            // Create a barcode reader for Code39 format
            using (var reader = new BarCodeReader(imageStream, DecodeType.Code39))
            {
                // Apply the requested checksum validation setting
                reader.BarcodeSettings.ChecksumValidation = validation;

                // Read all barcodes found in the image
                foreach (var result in reader.ReadBarCodes())
                {
                    // Output the detected barcode text to verify processing
                    Console.WriteLine($"{description} - Detected: {result.CodeText}");
                }
            }
        }

        stopwatch.Stop();
        // Report total elapsed time for the measurement scenario
        Console.WriteLine($"{description} - Total Time: {stopwatch.ElapsedMilliseconds} ms");
    }
}