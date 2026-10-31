// Title: Barcode generation and recognition with timing and CPU usage logging
// Description: Demonstrates creating QR, Code128, and PDF417 barcodes, then recognizing them while logging wall‑clock time and CPU time for each image.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It showcases the use of BarcodeGenerator for encoding, BarCodeReader for decoding, and System.Diagnostics for performance measurement. Developers often need to benchmark barcode processing, track CPU consumption, and log detailed results for diagnostics or optimization.
// Prompt: Write a wrapper that logs both recognition time and CPU usage for each processed image.
// Tags: barcode, generation, recognition, performance, timing, cpu, aspose.barcode, qr, code128, pdf417

using System;
using System.IO;
using System.Diagnostics;
using System.Collections.Generic;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Sample program that generates several barcode images, reads them back,
/// and logs both elapsed wall‑clock time and CPU time for each recognition operation.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates barcodes, processes each image,
    /// and outputs timing and recognition details to the console.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for sample barcodes
        string tempFolder = Path.Combine(Path.GetTempPath(), "Barcodes_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // List to hold generated file paths (populated via BarcodeFileTracker)
        List<string> barcodeFiles = new List<string>();

        // Generate sample barcode images
        GenerateBarcode(Path.Combine(tempFolder, "qr.png"), EncodeTypes.QR, "Sample QR");
        GenerateBarcode(Path.Combine(tempFolder, "code128.png"), EncodeTypes.Code128, "1234567890");
        GenerateBarcode(Path.Combine(tempFolder, "pdf417.png"), EncodeTypes.Pdf417, "PDF417 Sample");

        // Retrieve tracked file paths
        barcodeFiles.AddRange(BarcodeFileTracker.Files);

        // Process each image: measure wall time and CPU time
        foreach (string filePath in barcodeFiles)
        {
            if (!File.Exists(filePath))
            {
                Console.WriteLine($"File not found: {filePath}");
                continue;
            }

            // Capture CPU time before recognition
            Process currentProcess = Process.GetCurrentProcess();
            TimeSpan cpuStart = currentProcess.TotalProcessorTime;

            // Start wall‑clock timer
            Stopwatch sw = Stopwatch.StartNew();

            try
            {
                // Initialize barcode reader for all supported types
                using (BarCodeReader reader = new BarCodeReader(filePath, DecodeType.AllSupportedTypes))
                {
                    // Perform recognition
                    reader.ReadBarCodes();

                    // Stop timers
                    sw.Stop();
                    TimeSpan cpuEnd = currentProcess.TotalProcessorTime;

                    // Output results
                    Console.WriteLine($"File: {Path.GetFileName(filePath)}");
                    Console.WriteLine($"Recognition time: {sw.ElapsedMilliseconds} ms");
                    Console.WriteLine($"CPU time used: {(cpuEnd - cpuStart).TotalMilliseconds} ms");
                    Console.WriteLine($"Barcodes found: {reader.FoundCount}");

                    // List each detected barcode
                    foreach (BarCodeResult result in reader.FoundBarCodes)
                    {
                        Console.WriteLine($"  Type: {result.CodeTypeName}, Text: {result.CodeText}, Quality: {result.ReadingQuality}");
                    }
                }
            }
            catch (ArgumentException ex) when (ex.Message.Contains("Image loading failed"))
            {
                Console.WriteLine($"Failed to load image {filePath}: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error processing {filePath}: {ex.Message}");
            }
        }

        // Clean up temporary files
        try
        {
            Directory.Delete(tempFolder, true);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to delete temporary folder: {ex.Message}");
        }
    }

    /// <summary>
    /// Generates a barcode image using the specified encoding type and text,
    /// saves it to the given file path, and tracks the file for later processing.
    /// </summary>
    /// <param name="filePath">Full path where the barcode image will be saved.</param>
    /// <param name="encodeType">The barcode symbology to encode.</param>
    /// <param name="text">The text or data to encode in the barcode.</param>
    static void GenerateBarcode(string filePath, BaseEncodeType encodeType, string text)
    {
        // Create and configure the barcode generator
        using (BarcodeGenerator generator = new BarcodeGenerator(encodeType, text))
        {
            // Save the generated barcode as a PNG image
            generator.Save(filePath, BarCodeImageFormat.Png);
        }

        // Track the generated file path for later processing
        BarcodeFileTracker.Add(filePath);
    }
}

// Helper static class to track generated file paths
static class BarcodeFileTracker
{
    private static readonly List<string> _files = new List<string>();
    public static void Add(string path) => _files.Add(path);
    public static IReadOnlyList<string> Files => _files;
}

// Extension to expose the tracked files to Main (placeholder for future use)
static class ProgramExtensions
{
    public static List<string> GetBarcodeFiles(this Program p)
    {
        // Not used; placeholder to satisfy compiler if needed
        return new List<string>();
    }
}

// Helper class for potential future initialization (currently empty)
static class ProgramHelper
{
    static ProgramHelper()
    {
        // No initialization needed
    }
}