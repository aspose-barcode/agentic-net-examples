using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates reading barcodes from image files in a specified directory using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Scans a directory for image files and extracts any barcodes found.
    /// </summary>
    static void Main()
    {
        // Directory containing barcode images (adjust as needed)
        string imagesDirectory = "Barcodes";

        // Verify that the directory exists before proceeding
        if (!Directory.Exists(imagesDirectory))
        {
            Console.WriteLine($"Directory not found: {imagesDirectory}");
            return;
        }

        // Retrieve all files in the directory (any extension) – will filter later
        string[] imageFiles = Directory.GetFiles(imagesDirectory, "*.*", SearchOption.TopDirectoryOnly);
        int processedCount = 0;
        const int maxFiles = 10; // safety cap for the snippet runner

        // Iterate over each file path found
        foreach (string filePath in imageFiles)
        {
            // Stop processing once the safety limit is reached
            if (processedCount >= maxFiles)
                break;

            // Determine the file's extension and ensure it is a supported image format
            string extension = Path.GetExtension(filePath).ToLowerInvariant();
            if (extension != ".png" && extension != ".jpg" && extension != ".jpeg" &&
                extension != ".bmp" && extension != ".tif" && extension != ".tiff")
                continue; // Skip unsupported file types

            // Double‑check that the file still exists (it could have been removed)
            if (!File.Exists(filePath))
            {
                Console.WriteLine($"File not found: {filePath}");
                continue;
            }

            // Initialize a BarCodeReader for the current image, allowing all supported barcode types
            using (var reader = new BarCodeReader(filePath, DecodeType.AllSupportedTypes))
            {
                // Read and output each barcode detected in the image
                foreach (var result in reader.ReadBarCodes())
                {
                    Console.WriteLine($"File: {Path.GetFileName(filePath)} | Type: {result.CodeTypeName} | Text: {result.CodeText}");
                }
            }

            // Increment the count of processed files
            processedCount++;
        }

        // Indicate that processing of all eligible files is finished
        Console.WriteLine("Processing completed.");
    }
}