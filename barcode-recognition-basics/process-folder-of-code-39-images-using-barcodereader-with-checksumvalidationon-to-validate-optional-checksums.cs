// Title: Process Code 39 images with checksum validation
// Description: Demonstrates reading Code 39 barcodes from a folder of images, enabling optional checksum validation to ensure data integrity.
// Prompt: Process a folder of Code 39 images using BarCodeReader with ChecksumValidation.On to validate optional checksums.
// Tags: code39, barcode, checksumvalidation, barcodereader, imageprocessing

using System;
using System.IO;
using System.Linq;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates processing a folder of Code 39 barcode images with checksum validation.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Reads barcode images from a specified folder (or default) and prints decoded values.
    /// </summary>
    /// <param name="args">Optional command‑line argument specifying the folder path.</param>
    static void Main(string[] args)
    {
        // Determine the folder containing Code 39 barcode images.
        // Use the first command‑line argument if provided; otherwise default to "Code39Images".
        string folderPath = args.Length > 0 ? args[0] : "Code39Images";

        // Verify that the folder exists before proceeding.
        if (!Directory.Exists(folderPath))
        {
            Console.WriteLine($"Folder not found: {folderPath}");
            return;
        }

        // Retrieve image files (common bitmap extensions) from the folder.
        string[] imageFiles = Directory.GetFiles(folderPath, "*.*")
            .Where(f => f.EndsWith(".png", StringComparison.OrdinalIgnoreCase) ||
                        f.EndsWith(".jpg", StringComparison.OrdinalIgnoreCase) ||
                        f.EndsWith(".jpeg", StringComparison.OrdinalIgnoreCase) ||
                        f.EndsWith(".bmp", StringComparison.OrdinalIgnoreCase))
            .ToArray();

        // If no image files are found, inform the user and exit.
        if (imageFiles.Length == 0)
        {
            Console.WriteLine($"No image files found in folder: {folderPath}");
            return;
        }

        // Process each image file individually.
        foreach (string imagePath in imageFiles)
        {
            // Initialize BarCodeReader for Code 39 decoding.
            using (var reader = new BarCodeReader(imagePath, DecodeType.Code39))
            {
                // Enable checksum validation for optional Code 39 checksums.
                reader.BarcodeSettings.ChecksumValidation = ChecksumValidation.On;

                // Read all barcodes present in the current image.
                foreach (var result in reader.ReadBarCodes())
                {
                    // Output the file name, barcode type, and decoded text to the console.
                    Console.WriteLine($"File: {Path.GetFileName(imagePath)} | Type: {result.CodeTypeName} | Text: {result.CodeText}");
                }
            }
        }
    }
}