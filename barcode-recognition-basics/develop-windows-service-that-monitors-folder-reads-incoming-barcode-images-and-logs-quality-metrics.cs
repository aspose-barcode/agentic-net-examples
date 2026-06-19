using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates how to read barcodes from image files in a specified folder using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Scans a folder for supported image files,
    /// reads any barcodes found, and outputs details to the console.
    /// </summary>
    static void Main()
    {
        // Define the folder that contains barcode images (modify as needed).
        string inputFolder = "Barcodes";

        // Verify that the input folder exists before proceeding.
        if (!Directory.Exists(inputFolder))
        {
            Console.WriteLine($"Folder not found: {inputFolder}");
            return;
        }

        // List of supported image file extensions.
        string[] extensions = new[] { ".png", ".jpg", ".jpeg", ".bmp", ".gif" };
        List<string> imageFiles = new List<string>();

        // Collect all files in the folder that match the supported extensions.
        foreach (string file in Directory.GetFiles(inputFolder))
        {
            if (Array.Exists(extensions, e => e.Equals(Path.GetExtension(file), StringComparison.OrdinalIgnoreCase)))
            {
                imageFiles.Add(file);
            }
        }

        // If no supported image files were found, inform the user and exit.
        if (imageFiles.Count == 0)
        {
            Console.WriteLine("No barcode image files found.");
            return;
        }

        // Process each image file individually.
        foreach (string imagePath in imageFiles)
        {
            // Ensure the file still exists (it could have been removed after the initial scan).
            if (!File.Exists(imagePath))
            {
                Console.WriteLine($"File not found: {imagePath}");
                continue;
            }

            Console.WriteLine($"Processing: {Path.GetFileName(imagePath)}");

            // Load the image into a bitmap object.
            using (var bitmap = new Bitmap(imagePath))
            {
                // Initialize the barcode reader for all supported barcode types.
                using (var reader = new BarCodeReader(bitmap, DecodeType.AllSupportedTypes))
                {
                    // Optional: set the quality preset to normal.
                    reader.QualitySettings = QualitySettings.NormalQuality;

                    // Read all barcodes present in the image.
                    BarCodeResult[] results = reader.ReadBarCodes();

                    // If no barcodes were detected, report and continue to the next image.
                    if (results.Length == 0)
                    {
                        Console.WriteLine("  No barcodes detected.");
                        continue;
                    }

                    // Output details for each detected barcode.
                    foreach (var result in results)
                    {
                        // Basic barcode information.
                        Console.WriteLine($"  Type: {result.CodeTypeName}");
                        Console.WriteLine($"  CodeText: {result.CodeText}");

                        // Quality metrics provided by the reader.
                        Console.WriteLine($"  Confidence: {result.Confidence}");
                        Console.WriteLine($"  ReadingQuality: {result.ReadingQuality}");

                        // Region of the barcode within the image.
                        var rect = result.Region.Rectangle;
                        int x = (int)Math.Round((double)rect.X);
                        int y = (int)Math.Round((double)rect.Y);
                        int width = (int)Math.Round((double)rect.Width);
                        int height = (int)Math.Round((double)rect.Height);
                        Console.WriteLine($"  Region: X={x}, Y={y}, Width={width}, Height={height}");
                    }
                }
            }
        }

        // Indicate that processing of all images is complete.
        Console.WriteLine("Processing completed.");
    }
}