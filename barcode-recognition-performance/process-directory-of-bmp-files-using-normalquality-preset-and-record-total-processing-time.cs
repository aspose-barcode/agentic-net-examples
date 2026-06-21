using System;
using System.Diagnostics;
using System.IO;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Entry point for the barcode processing console application.
/// </summary>
class Program
{
    /// <summary>
    /// Main method that processes BMP images in a specified directory,
    /// reads all supported barcodes using Aspose.BarCode, and outputs results.
    /// </summary>
    /// <param name="args">Optional command‑line argument specifying the directory to scan.</param>
    static void Main(string[] args)
    {
        // Determine the directory to process: use the first argument if provided,
        // otherwise fall back to the current working directory.
        string directoryPath = args.Length > 0 ? args[0] : Directory.GetCurrentDirectory();

        // Verify that the target directory exists before proceeding.
        if (!Directory.Exists(directoryPath))
        {
            Console.WriteLine($"Directory does not exist: {directoryPath}");
            return;
        }

        // Retrieve all BMP files in the directory (non‑recursive).
        string[] bmpFiles = Directory.GetFiles(directoryPath, "*.bmp", SearchOption.TopDirectoryOnly);
        if (bmpFiles.Length == 0)
        {
            Console.WriteLine("No BMP files found to process.");
            return;
        }

        // Start a stopwatch to measure total processing time.
        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();

        // Process each BMP file individually.
        foreach (string filePath in bmpFiles)
        {
            // Guard against a file being removed between enumeration and processing.
            if (!File.Exists(filePath))
            {
                Console.WriteLine($"File not found (skipped): {filePath}");
                continue;
            }

            // Create a BarCodeReader with default (NormalQuality) settings.
            // The DecodeType.AllSupportedTypes flag tells the reader to attempt all known barcode formats.
            using (var reader = new BarCodeReader(filePath, DecodeType.AllSupportedTypes))
            {
                // Iterate through all detected barcodes in the current image.
                foreach (var result in reader.ReadBarCodes())
                {
                    // Output the file name, barcode type, and decoded text.
                    Console.WriteLine($"File: {Path.GetFileName(filePath)} | Type: {result.CodeTypeName} | Text: {result.CodeText}");
                }
            }
        }

        // Stop the timer and report the elapsed time.
        stopwatch.Stop();
        Console.WriteLine($"Processed {bmpFiles.Length} BMP file(s) in {stopwatch.Elapsed.TotalSeconds:F2} seconds.");
    }
}