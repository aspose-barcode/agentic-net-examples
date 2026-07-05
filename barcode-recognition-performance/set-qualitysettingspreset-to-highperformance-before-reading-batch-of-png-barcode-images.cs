// Title: High-Performance PNG Barcode Batch Reader
// Description: Demonstrates setting QualitySettings.Preset to HighPerformance before reading a batch of PNG barcode images, improving processing speed.
// Prompt: Set QualitySettings.Preset to HighPerformance before reading a batch of PNG barcode images.
// Tags: barcode, png, batch, highperformance, qualitysettings, aspose.barcode, read

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Program that reads up to five PNG barcode images using Aspose.BarCode with high‑performance quality settings.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Scans a folder for PNG files, configures the reader for high performance, and outputs detected barcodes.
    /// </summary>
    static void Main()
    {
        // Define the folder that contains PNG barcode images
        string folderPath = "Barcodes";

        // Verify that the folder exists before proceeding
        if (!Directory.Exists(folderPath))
        {
            Console.WriteLine($"Folder not found: {folderPath}");
            return;
        }

        // Retrieve all PNG files in the folder (limit processing to a maximum of 5 files for safety)
        string[] pngFiles = Directory.GetFiles(folderPath, "*.png");
        if (pngFiles.Length == 0)
        {
            Console.WriteLine("No PNG files found in the folder.");
            return;
        }

        // Determine how many files to process (up to 5)
        int maxFiles = Math.Min(pngFiles.Length, 5);

        // Process each selected PNG file
        for (int i = 0; i < maxFiles; i++)
        {
            string filePath = pngFiles[i];

            // Ensure the file still exists before attempting to read it
            if (!File.Exists(filePath))
            {
                Console.WriteLine($"File not found: {filePath}");
                continue;
            }

            // Create a BarCodeReader for the image and set the high‑performance quality preset
            using (var reader = new BarCodeReader(filePath, DecodeType.AllSupportedTypes))
            {
                reader.QualitySettings = QualitySettings.HighPerformance;

                // Read all barcodes present in the image and output their details
                foreach (var result in reader.ReadBarCodes())
                {
                    Console.WriteLine($"File: {Path.GetFileName(filePath)} | Type: {result.CodeTypeName} | Text: {result.CodeText}");
                }
            }
        }
    }
}