// Title: Folder Monitoring Barcode Reader
// Description: Demonstrates reading barcode images from a folder and logging quality metrics for each detected barcode.
// Prompt: Develop a Windows service that monitors a folder, reads incoming barcode images, and logs quality metrics.
// Tags: barcode symbology, reading, console, aspose.barcode, folder monitoring

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Simple console application that scans a folder for barcode images,
/// reads any barcodes using Aspose.BarCode, and logs quality metrics.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Accepts an optional folder path argument, processes up to five image files,
    /// and writes barcode type, text, reading quality, and confidence to the console.
    /// </summary>
    /// <param name="args">Command‑line arguments; first argument may specify the folder to monitor.</param>
    static void Main(string[] args)
    {
        // Determine the folder to monitor. Use a default if not provided.
        string folderPath = args.Length > 0 ? args[0] : "Barcodes";

        // Verify that the folder exists before proceeding.
        if (!Directory.Exists(folderPath))
        {
            Console.WriteLine($"Folder not found: {folderPath}");
            return;
        }

        // Define supported image extensions and collect up to five matching files.
        string[] supportedExtensions = new[] { ".png", ".jpg", ".jpeg", ".bmp", ".tif", ".tiff", ".gif" };
        string[] allFiles = Directory.GetFiles(folderPath);
        var imageFiles = new System.Collections.Generic.List<string>();
        foreach (var file in allFiles)
        {
            if (imageFiles.Count >= 5) break; // Limit to five files.
            if (Array.Exists(supportedExtensions, ext => ext.Equals(Path.GetExtension(file), StringComparison.OrdinalIgnoreCase)))
            {
                imageFiles.Add(file);
            }
        }

        // If no supported images were found, inform the user and exit.
        if (imageFiles.Count == 0)
        {
            Console.WriteLine("No barcode image files found in the folder.");
            return;
        }

        // Process each image file individually.
        foreach (var imagePath in imageFiles)
        {
            // Ensure the file still exists before attempting to read it.
            if (!File.Exists(imagePath))
            {
                Console.WriteLine($"File not found: {imagePath}");
                continue;
            }

            Console.WriteLine($"Processing file: {Path.GetFileName(imagePath)}");

            // Create a barcode reader for the image, enabling all supported decode types.
            using (var reader = new BarCodeReader(imagePath, DecodeType.AllSupportedTypes))
            {
                // Apply normal quality settings for reading.
                reader.QualitySettings = QualitySettings.NormalQuality;

                // Attempt to read all barcodes present in the image.
                var results = reader.ReadBarCodes();

                // If no barcodes were detected, report and continue to the next file.
                if (results == null || results.Length == 0)
                {
                    Console.WriteLine("  No barcodes detected.");
                    continue;
                }

                // Log quality metrics for each detected barcode.
                foreach (var result in results)
                {
                    Console.WriteLine($"  Type: {result.CodeTypeName}");
                    Console.WriteLine($"  CodeText: {result.CodeText}");
                    Console.WriteLine($"  ReadingQuality: {result.ReadingQuality}%");
                    Console.WriteLine($"  Confidence: {result.Confidence}");
                }
            }
        }

        // Indicate that processing of all files has completed.
        Console.WriteLine("Processing completed.");
    }
}