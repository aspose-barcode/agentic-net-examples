using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates barcode detection using Aspose.BarCode on a set of image files.
/// </summary>
class Program
{
    // Maximum number of files to process in this demo (safe for the runner)
    private const int MaxFilesToProcess = 5;

    /// <summary>
    /// Entry point of the application. Scans a folder for barcode images and prints detection results.
    /// </summary>
    /// <param name="args">Optional command‑line arguments. The first argument can specify the folder path.</param>
    static void Main(string[] args)
    {
        // Determine the folder containing barcode images.
        // If a command‑line argument is provided, use it; otherwise fall back to a sample folder.
        string folderPath = args.Length > 0
            ? args[0]
            : Path.Combine(Directory.GetCurrentDirectory(), "Barcodes");

        // Verify that the folder exists before proceeding.
        if (!Directory.Exists(folderPath))
        {
            Console.WriteLine($"Folder not found: {folderPath}");
            return;
        }

        // Define supported image extensions for barcode detection.
        string[] supportedExtensions = new[] { ".png", ".jpg", ".jpeg", ".bmp", ".tif", ".tiff" };
        List<string> imageFiles = new List<string>();

        // Collect image files up to the defined maximum.
        foreach (string file in Directory.GetFiles(folderPath))
        {
            // Check if the file extension matches one of the supported types.
            if (Array.Exists(supportedExtensions, ext => ext.Equals(Path.GetExtension(file), StringComparison.OrdinalIgnoreCase)))
            {
                imageFiles.Add(file);
                if (imageFiles.Count >= MaxFilesToProcess)
                    break; // limit to a safe sample size
            }
        }

        // If no supported images were found, inform the user and exit.
        if (imageFiles.Count == 0)
        {
            Console.WriteLine("No barcode image files found in the specified folder.");
            return;
        }

        // Process each image file individually.
        foreach (string imagePath in imageFiles)
        {
            // Ensure the file still exists (it could have been removed after enumeration).
            if (!File.Exists(imagePath))
            {
                Console.WriteLine($"File not found: {imagePath}");
                continue;
            }

            try
            {
                // Create a reader for the current image, detecting all supported symbologies.
                using (var reader = new BarCodeReader(imagePath, DecodeType.AllSupportedTypes))
                {
                    // Read all barcodes present in the image.
                    BarCodeResult[] results = reader.ReadBarCodes();

                    // If no barcodes were detected, report and move to the next file.
                    if (results.Length == 0)
                    {
                        Console.WriteLine($"No barcodes detected in file: {Path.GetFileName(imagePath)}");
                        continue;
                    }

                    // Output details for each detected barcode.
                    foreach (BarCodeResult result in results)
                    {
                        Console.WriteLine($"File: {Path.GetFileName(imagePath)}");
                        Console.WriteLine($"  Type            : {result.CodeTypeName}");
                        Console.WriteLine($"  CodeText        : {result.CodeText}");
                        Console.WriteLine($"  Confidence      : {result.Confidence}");
                        Console.WriteLine($"  ReadingQuality  : {result.ReadingQuality}");
                        Console.WriteLine();
                    }
                }
            }
            catch (Exception ex)
            {
                // Report any errors that occur while processing the current file.
                Console.WriteLine($"Error processing file '{Path.GetFileName(imagePath)}': {ex.Message}");
            }
        }
    }
}