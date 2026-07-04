// Title: Batch Barcode Reader with Confidence and ReadingQuality
// Description: Demonstrates reading multiple barcode images from a network share and retrieving confidence and reading quality metrics for each detected barcode.
// Prompt: Batch read multiple barcode images from a network share, capturing Confidence and ReadingQuality for each file.
// Tags: barcode, batch, confidence, readingquality, aspose, csharp, network-share

using System;
using System.IO;
using System.Linq;
using Aspose.BarCode.BarCodeRecognition;

namespace BarcodeBatchReader
{
    /// <summary>
    /// Entry point for the batch barcode reading example.
    /// </summary>
    class Program
    {
        /// <summary>
        /// Scans a network share for up to five barcode images, reads each barcode,
        /// and prints its type, text, confidence, and reading quality.
        /// </summary>
        static void Main()
        {
            // Path to the network share containing barcode images.
            // Adjust the UNC path as needed for your environment.
            string folderPath = @"\\networkshare\barcodes";

            // Verify that the folder exists before proceeding.
            if (!Directory.Exists(folderPath))
            {
                Console.WriteLine($"Folder not found: {folderPath}");
                return;
            }

            // Define the set of image file extensions that will be processed.
            string[] supportedExtensions = { ".png", ".jpg", ".jpeg", ".bmp", ".gif" };

            // Retrieve up to 5 image files that match the supported extensions.
            string[] files = Directory.GetFiles(folderPath)
                .Where(f => supportedExtensions.Contains(Path.GetExtension(f), StringComparer.OrdinalIgnoreCase))
                .Take(5)
                .ToArray();

            // If no matching files are found, inform the user and exit.
            if (files.Length == 0)
            {
                Console.WriteLine("No barcode image files found in the specified folder.");
                return;
            }

            // Process each discovered image file.
            foreach (string filePath in files)
            {
                // Double‑check that the file still exists (it could have been removed).
                if (!File.Exists(filePath))
                {
                    Console.WriteLine($"File not found: {filePath}");
                    continue;
                }

                // Create a BarCodeReader for the current file, using all supported symbologies.
                using (var reader = new BarCodeReader(filePath, DecodeType.AllSupportedTypes))
                {
                    // Perform the barcode recognition operation.
                    BarCodeResult[] results = reader.ReadBarCodes();

                    // If no barcodes were detected, report and move to the next file.
                    if (results.Length == 0)
                    {
                        Console.WriteLine($"No barcode detected in file: {Path.GetFileName(filePath)}");
                        continue;
                    }

                    // Output details for each detected barcode.
                    foreach (var result in results)
                    {
                        Console.WriteLine($"File: {Path.GetFileName(filePath)}");
                        Console.WriteLine($"  Type: {result.CodeTypeName}");
                        Console.WriteLine($"  Text: {result.CodeText}");
                        Console.WriteLine($"  Confidence: {result.Confidence}");
                        Console.WriteLine($"  ReadingQuality: {result.ReadingQuality}");
                    }
                }
            }
        }
    }
}