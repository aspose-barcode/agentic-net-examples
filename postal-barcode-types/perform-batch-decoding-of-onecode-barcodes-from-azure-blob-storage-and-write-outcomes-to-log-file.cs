using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates decoding OneCode barcodes from image files and logging the results.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Scans a folder for image files, attempts to decode OneCode barcodes,
    /// and writes detailed results to a log file.
    /// </summary>
    /// <param name="args">Optional command‑line arguments; the first argument can specify the images folder.</param>
    static void Main(string[] args)
    {
        // NOTE: In a real scenario the images would be downloaded from Azure Blob Storage.
        // The Azure SDK code is omitted because the required package is not available in the snippet runner.
        // Example Azure Blob download (commented out):
        // using Azure.Storage.Blobs;
        // var blobServiceClient = new BlobServiceClient(connectionString);
        // var containerClient = blobServiceClient.GetBlobContainerClient(containerName);
        // foreach (var blobItem in containerClient.GetBlobs())
        // {
        //     var blobClient = containerClient.GetBlobClient(blobItem.Name);
        //     using var downloadStream = new MemoryStream();
        //     blobClient.DownloadTo(downloadStream);
        //     // Process downloadStream...
        // }

        // Determine the folder that contains sample barcode images.
        string imagesFolder = args.Length > 0 ? args[0] : "SampleImages";

        // Verify that the folder exists before proceeding.
        if (!Directory.Exists(imagesFolder))
        {
            Console.WriteLine($"Images folder not found: {imagesFolder}");
            return;
        }

        // Prepare a log file to capture decoding details.
        string logPath = "decode_log.txt";
        using (var logWriter = new StreamWriter(logPath, append: true))
        {
            logWriter.WriteLine($"--- Decoding session started at {DateTime.Now} ---");

            // Retrieve all files in the folder and filter for supported image extensions.
            string[] allFiles = Directory.GetFiles(imagesFolder);
            var imageFiles = new List<string>();
            foreach (var file in allFiles)
            {
                string ext = Path.GetExtension(file).ToLowerInvariant();
                if (ext == ".png" || ext == ".jpg" || ext == ".jpeg" || ext == ".bmp")
                {
                    imageFiles.Add(file);
                    // Limit processing to a maximum of 5 images for this sample.
                    if (imageFiles.Count >= 5) break;
                }
            }

            // If no supported images were found, log and exit.
            if (imageFiles.Count == 0)
            {
                Console.WriteLine("No image files found in the folder.");
                logWriter.WriteLine("No image files found.");
                return;
            }

            // Process each selected image file.
            foreach (var imagePath in imageFiles)
            {
                // Ensure the file still exists before attempting to read it.
                if (!File.Exists(imagePath))
                {
                    Console.WriteLine($"File not found: {imagePath}");
                    logWriter.WriteLine($"{imagePath}: File not found.");
                    continue;
                }

                try
                {
                    // Initialize the barcode reader for OneCode type using the image file.
                    using (var reader = new BarCodeReader(imagePath, DecodeType.OneCode))
                    {
                        // Attempt to read all barcodes present in the image.
                        BarCodeResult[] results = reader.ReadBarCodes();

                        // If no barcodes were detected, report and continue to the next image.
                        if (results.Length == 0)
                        {
                            Console.WriteLine($"{Path.GetFileName(imagePath)}: No OneCode barcode detected.");
                            logWriter.WriteLine($"{Path.GetFileName(imagePath)}: No OneCode barcode detected.");
                            continue;
                        }

                        // Log details for each detected barcode.
                        foreach (var result in results)
                        {
                            string logEntry = $"{Path.GetFileName(imagePath)} | Type: {result.CodeTypeName} | Text: {result.CodeText} | Confidence: {result.Confidence} | Quality: {result.ReadingQuality} | Angle: {result.Region.Angle}";
                            Console.WriteLine(logEntry);
                            logWriter.WriteLine(logEntry);
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Capture any unexpected errors and log them.
                    Console.WriteLine($"{Path.GetFileName(imagePath)}: Error - {ex.Message}");
                    logWriter.WriteLine($"{Path.GetFileName(imagePath)}: Error - {ex.Message}");
                }
            }

            logWriter.WriteLine($"--- Decoding session ended at {DateTime.Now} ---");
        }

        // Inform the user that processing is complete and where to find the log.
        Console.WriteLine($"Decoding completed. Log written to {Path.GetFullPath(logPath)}");
    }
}