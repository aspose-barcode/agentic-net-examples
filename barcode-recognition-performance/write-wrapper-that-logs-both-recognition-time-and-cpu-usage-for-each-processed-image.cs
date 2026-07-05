// Title: Barcode generation and recognition with timing and CPU usage logging
// Description: Demonstrates generating sample barcodes, then recognizing them while logging the elapsed time and CPU consumption for each image.
// Prompt: Write a wrapper that logs both recognition time and CPU usage for each processed image.
// Tags: barcode, generation, recognition, timing, cpu usage, aspose.barcode

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Example program that creates barcode images, reads them back, and logs
/// both the recognition duration and the CPU time consumed for each image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates sample barcodes, then
    /// processes each image while measuring performance metrics.
    /// </summary>
    static void Main()
    {
        // Define sample barcodes to generate and process
        var samples = new List<(BaseEncodeType EncodeType, string CodeText, string FileName)>
        {
            (EncodeTypes.Code128, "Sample123", "barcode1.png"),
            (EncodeTypes.QR, "https://example.com", "barcode2.png"),
            (EncodeTypes.DataMatrix, "DataMatrixTest", "barcode3.png")
        };

        // ------------------------------------------------------------
        // Generate barcode images
        // ------------------------------------------------------------
        foreach (var sample in samples)
        {
            // Ensure any existing file is overwritten
            if (File.Exists(sample.FileName))
            {
                File.Delete(sample.FileName);
            }

            // Create a generator for the specified type and text
            using (var generator = new BarcodeGenerator(sample.EncodeType, sample.CodeText))
            {
                // Save the generated barcode as a PNG image
                generator.Save(sample.FileName, BarCodeImageFormat.Png);
            }
        }

        // ------------------------------------------------------------
        // Process each image: measure recognition time and CPU usage
        // ------------------------------------------------------------
        foreach (var sample in samples)
        {
            // Verify that the image file exists before attempting to read it
            if (!File.Exists(sample.FileName))
            {
                Console.WriteLine($"File not found: {sample.FileName}");
                continue;
            }

            // Create a BarCodeReader that attempts to decode all supported types
            using (var reader = new BarCodeReader(sample.FileName, DecodeType.AllSupportedTypes))
            {
                // Start timing and capture initial CPU usage
                var stopwatch = Stopwatch.StartNew();
                var process = Process.GetCurrentProcess();
                var cpuStart = process.TotalProcessorTime;

                // Perform barcode recognition
                var results = reader.ReadBarCodes();

                // Stop timing and capture final CPU usage
                stopwatch.Stop();
                var cpuEnd = process.TotalProcessorTime;
                var cpuUsed = cpuEnd - cpuStart;

                // Log the performance metrics and recognition results
                Console.WriteLine($"Processing file: {sample.FileName}");
                Console.WriteLine($"Recognition time: {stopwatch.Elapsed.TotalMilliseconds} ms");
                Console.WriteLine($"CPU time used: {cpuUsed.TotalMilliseconds} ms");
                foreach (var result in results)
                {
                    Console.WriteLine($"  Detected Type: {result.CodeTypeName}");
                    Console.WriteLine($"  CodeText: {result.CodeText}");
                }
                Console.WriteLine(); // Blank line for readability
            }
        }
    }
}