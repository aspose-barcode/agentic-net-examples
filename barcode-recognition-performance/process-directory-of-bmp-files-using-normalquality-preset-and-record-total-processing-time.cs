// Title: Process BMP barcode images with NormalQuality preset and measure execution time
// Description: Demonstrates loading BMP files containing barcodes, reading them using Aspose.BarCode, and timing the overall processing.
// Category-Description: This example belongs to the Aspose.BarCode image processing and recognition category. It showcases the BarCodeReader class for detecting barcodes in bitmap images, typical for batch processing scenarios where developers need to read multiple files efficiently. Common use cases include inventory scanning, document automation, and bulk image analysis, where performance measurement is essential.
// Prompt: Process a directory of BMP files using NormalQuality preset and record total processing time.
// Tags: bmp, barcode, recognition, performance, batch-processing, aspose.barcode, normalquality

using System;
using System.IO;
using System.Diagnostics;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates generating sample QR code BMP files, reading them with <see cref="BarCodeReader"/>, and measuring total processing time.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates sample BMP files, reads barcodes, and reports processing duration.
    /// </summary>
    /// <param name="args">Command‑line arguments (not used).</param>
    static void Main(string[] args)
    {
        // Create a unique temporary folder for sample BMP files
        string tempFolder = Path.Combine(Path.GetTempPath(), "BmpProcess_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Generate a few sample barcode BMP images
        List<string> bmpFiles = new List<string>();
        for (int i = 1; i <= 5; i++)
        {
            string filePath = Path.Combine(tempFolder, $"Sample{i}.bmp");
            GenerateSampleBarcode(filePath, $"Sample{i}");
            bmpFiles.Add(filePath);
        }

        // Start timing the batch processing of BMP files
        Stopwatch sw = Stopwatch.StartNew();

        // Iterate through each generated BMP file and attempt to read barcodes
        foreach (string file in bmpFiles)
        {
            if (!File.Exists(file))
            {
                Console.WriteLine($"File not found: {file}");
                continue;
            }

            try
            {
                // Use BarCodeReader to detect all barcodes in the current image
                using (var reader = new BarCodeReader(file))
                {
                    var barcodes = reader.ReadBarCodes();
                    Console.WriteLine($"File: {Path.GetFileName(file)} - Detected {barcodes.Length} barcode(s).");

                    // Output details of each detected barcode
                    foreach (var result in barcodes)
                    {
                        Console.WriteLine($"  Type: {result.CodeTypeName}, Text: {result.CodeText}");
                    }
                }
            }
            catch (ArgumentException ex) when (ex.Message.Contains("Image loading failed"))
            {
                // Handle cases where the file format is unsupported or corrupted
                Console.WriteLine($"Skipping unsupported file: {file} ({ex.Message})");
            }
            catch (Exception ex)
            {
                // Log any unexpected errors during processing
                Console.WriteLine($"Error processing file {file}: {ex.Message}");
            }
        }

        // Stop the timer and report total elapsed time
        sw.Stop();
        Console.WriteLine($"Total processing time: {sw.Elapsed.TotalSeconds:F3} seconds");

        // Clean up temporary files (optional)
        try
        {
            Directory.Delete(tempFolder, true);
        }
        catch
        {
            // Ignore cleanup errors
        }
    }

    // Generates a simple QR barcode and saves it as a BMP file
    private static void GenerateSampleBarcode(string filePath, string codeText)
    {
        BaseEncodeType encodeType = EncodeTypes.QR;

        using (var generator = new BarcodeGenerator(encodeType, codeText))
        {
            generator.Save(filePath, BarCodeImageFormat.Bmp);
        }
    }
}