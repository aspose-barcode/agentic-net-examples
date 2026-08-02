// Title: High‑Resolution PNG Barcode Recognition Timing
// Description: Demonstrates loading a high‑resolution PNG image and measuring the time required to recognize barcodes using Aspose.BarCode default settings.
// Category-Description: This example belongs to the Aspose.BarCode barcode recognition category, showcasing the use of BarCodeReader with DecodeType.AllSupportedTypes. It illustrates typical scenarios such as performance benchmarking and bulk image processing where developers need to assess recognition speed across various symbologies.
// Prompt: Load a high‑resolution PNG image and measure barcode recognition time using default settings.
// Tags: barcode, png, recognition, performance, timing, aspose.barcode, decodeall

using System;
using System.Diagnostics;
using System.IO;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates loading a high‑resolution PNG image and measuring barcode recognition time using default settings.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Accepts optional image path argument, validates file, reads barcodes, and reports timing.
    /// </summary>
    /// <param name="args">Command‑line arguments; first argument may specify the image file path.</param>
    static void Main(string[] args)
    {
        // Determine the image file path (use argument if provided, otherwise default to "barcode.png")
        string imagePath = args.Length > 0 ? args[0] : "barcode.png";

        // Verify that the specified file exists before attempting to read
        if (!File.Exists(imagePath))
        {
            Console.WriteLine($"Image file not found: {imagePath}");
            return;
        }

        // Initialize BarCodeReader to detect all supported symbologies in the image
        using (var reader = new BarCodeReader(imagePath, DecodeType.AllSupportedTypes))
        {
            // Start timing the recognition process
            var stopwatch = Stopwatch.StartNew();

            // Perform barcode detection
            var results = reader.ReadBarCodes();

            // Stop timing after detection completes
            stopwatch.Stop();

            // Output the elapsed time in milliseconds
            Console.WriteLine($"Recognition time: {stopwatch.ElapsedMilliseconds} ms");

            // Iterate through detected barcodes and display their type and decoded text
            foreach (var result in results)
            {
                Console.WriteLine($"Type: {result.CodeTypeName}, Text: {result.CodeText}");
            }
        }
    }
}