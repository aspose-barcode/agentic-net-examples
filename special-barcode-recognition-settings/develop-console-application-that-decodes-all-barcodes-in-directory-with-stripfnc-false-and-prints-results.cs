// Title: Decode all barcodes in a directory with StripFNC disabled
// Description: This console app scans a specified folder, decodes every supported barcode in image files while preserving FNC characters, and prints detailed results.
// Category-Description: Demonstrates Aspose.BarCode barcode recognition across multiple image formats. It uses BarCodeReader and DecodeType.AllSupportedTypes, showing how to configure BarcodeSettings (StripFNC) and iterate over BarCodeResult objects. Ideal for developers needing batch processing of barcodes in files, such as inventory audits or document digitization.
// Prompt: Develop a console application that decodes all barcodes in a directory with StripFNC false and prints results.
// Tags: barcode, decoding, batch, stripfnc, console, aspose.barcode, recognition

using System;
using System.IO;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Entry point for the barcode batch decoding console application.
/// </summary>
class Program
{
    /// <summary>
    /// Scans a directory for image files, decodes all supported barcodes with StripFNC disabled, and writes results to the console.
    /// </summary>
    /// <param name="args">Optional first argument specifying the directory path; if omitted, the current directory is used.</param>
    static void Main(string[] args)
    {
        // Determine the directory to scan. Use the first argument if provided; otherwise, use the current directory.
        string directoryPath = args.Length > 0 ? args[0] : Directory.GetCurrentDirectory();

        // Verify that the directory exists before proceeding.
        if (!Directory.Exists(directoryPath))
        {
            Console.WriteLine($"Directory does not exist: {directoryPath}");
            return;
        }

        // Retrieve all files in the directory (non‑recursive). Adjust the filter if you want to limit to specific image extensions.
        string[] files = Directory.GetFiles(directoryPath);
        if (files.Length == 0)
        {
            Console.WriteLine($"No files found in directory: {directoryPath}");
            return;
        }

        // Process each file individually.
        foreach (string filePath in files)
        {
            // Skip non‑existing files (should not happen) and filter out unsupported extensions.
            if (!File.Exists(filePath))
                continue;

            string extension = Path.GetExtension(filePath).ToLowerInvariant();
            if (extension != ".png" && extension != ".jpg" && extension != ".jpeg" && extension != ".bmp" && extension != ".tif" && extension != ".tiff")
                continue;

            // Use BarCodeReader to decode barcodes in the current image file.
            using (BarCodeReader reader = new BarCodeReader(filePath, DecodeType.AllSupportedTypes))
            {
                // Ensure FNC characters are not stripped (set to false as required).
                reader.BarcodeSettings.StripFNC = false;

                // Perform the recognition and obtain all results.
                BarCodeResult[] results = reader.ReadBarCodes();

                // If no barcodes were detected, report and continue to the next file.
                if (results.Length == 0)
                {
                    Console.WriteLine($"[File: {Path.GetFileName(filePath)}] No barcodes detected.");
                    continue;
                }

                // Output summary information for the current file.
                Console.WriteLine($"[File: {Path.GetFileName(filePath)}] Detected {results.Length} barcode(s):");

                // Iterate through each detected barcode and display detailed information.
                foreach (BarCodeResult result in results)
                {
                    Console.WriteLine($"  Type: {result.CodeTypeName}");
                    Console.WriteLine($"  CodeText: {result.CodeText}");
                    Console.WriteLine($"  Confidence: {result.Confidence}");
                    Console.WriteLine($"  ReadingQuality: {result.ReadingQuality}");
                }
            }
        }
    }
}