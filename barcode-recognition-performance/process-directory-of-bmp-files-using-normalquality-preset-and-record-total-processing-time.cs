// Title: Batch BMP Barcode Processing with Normal Quality
// Description: Processes all BMP images in a directory, reads any barcodes using the NormalQuality preset, and reports total processing time.
// Prompt: Process a directory of BMP files using NormalQuality preset and record total processing time.
// Tags: barcode, batch processing, bmp, normalquality, timing, aspose.barcode

using System;
using System.IO;
using System.Diagnostics;
using Aspose.BarCode;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates how to read barcodes from a collection of BMP files using the NormalQuality preset
/// and measures the total time required for processing.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Accepts an optional directory path argument,
    /// processes each BMP file within, and outputs barcode information along with processing duration.
    /// </summary>
    /// <param name="args">Command‑line arguments; the first argument may specify the target directory.</param>
    static void Main(string[] args)
    {
        // Determine the directory to process. Use argument if provided, otherwise default to "BmpFiles".
        string directoryPath = args.Length > 0 ? args[0] : "BmpFiles";

        // Verify that the directory exists before proceeding.
        if (!Directory.Exists(directoryPath))
        {
            Console.WriteLine($"Directory not found: {directoryPath}");
            return;
        }

        // Retrieve all BMP files in the specified directory (non‑recursive).
        string[] bmpFiles = Directory.GetFiles(directoryPath, "*.bmp", SearchOption.TopDirectoryOnly);
        if (bmpFiles.Length == 0)
        {
            Console.WriteLine("No BMP files found to process.");
            return;
        }

        // Start measuring total processing time.
        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();

        // Iterate over each BMP file and attempt to read any barcodes it contains.
        foreach (string filePath in bmpFiles)
        {
            // Defensive check: skip the file if it somehow does not exist.
            if (!File.Exists(filePath))
            {
                Console.WriteLine($"File not found (skipped): {filePath}");
                continue;
            }

            // Initialize a BarCodeReader for the current image, supporting all barcode types.
            using (var reader = new BarCodeReader(filePath, DecodeType.AllSupportedTypes))
            {
                // Apply the NormalQuality preset to balance speed and accuracy.
                reader.QualitySettings = QualitySettings.NormalQuality;

                // Read all barcodes present in the image.
                foreach (var result in reader.ReadBarCodes())
                {
                    // Output the file name, detected barcode type, and decoded text.
                    Console.WriteLine($"File: {Path.GetFileName(filePath)} | Type: {result.CodeTypeName} | Text: {result.CodeText}");
                }
            }
        }

        // Stop the timer after all files have been processed.
        stopwatch.Stop();

        // Report the total number of processed files and the elapsed time in seconds.
        Console.WriteLine($"Processed {bmpFiles.Length} file(s) in {stopwatch.Elapsed.TotalSeconds:F2} seconds.");
    }
}