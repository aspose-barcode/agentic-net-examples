// Title: Batch decode OneCode barcodes from Azure Blob storage and log results
// Description: Demonstrates how to read OneCode barcodes from image files (simulating Azure Blob storage) and write decoding details to a log file.
// Category-Description: This example belongs to the Aspose.BarCode barcode recognition category, showcasing the BarCodeReader class for OneCode symbology. It illustrates typical batch processing, quality settings, and result logging, which developers often need when integrating barcode scanning into cloud storage workflows.
// Prompt: Perform batch decoding of OneCode barcodes from an Azure Blob storage and write outcomes to a log file.
// Tags: onecode, barcode, decoding, batch, log, aspose.barcode, azure blob, qualitysettings

using System;
using System.IO;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates batch decoding of OneCode barcodes from a local folder (as a stand‑in for Azure Blob storage)
/// and writes detailed results to a log file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Scans up to five image files, decodes OneCode barcodes,
    /// and records outcomes in <c>decode_log.txt</c>.
    /// </summary>
    static void Main()
    {
        // Folder that would contain images downloaded from Azure Blob storage (fallback for demo)
        string imagesFolder = "SampleBarcodes";

        // Path of the log file that will receive decoding results
        string logFilePath = "decode_log.txt";

        // Ensure a fresh log file for each run
        if (File.Exists(logFilePath))
        {
            File.Delete(logFilePath);
        }

        // Open a StreamWriter for the log file (non‑appending mode)
        using (var logWriter = new StreamWriter(logFilePath, append: false))
        {
            // Retrieve up to five image files from the folder
            string[] imageFiles = Array.Empty<string>();
            if (Directory.Exists(imagesFolder))
            {
                // Get all files and filter by common image extensions
                imageFiles = Directory.GetFiles(imagesFolder, "*.*", SearchOption.TopDirectoryOnly);
                imageFiles = Array.FindAll(imageFiles, f =>
                    f.EndsWith(".png", StringComparison.OrdinalIgnoreCase) ||
                    f.EndsWith(".jpg", StringComparison.OrdinalIgnoreCase) ||
                    f.EndsWith(".jpeg", StringComparison.OrdinalIgnoreCase) ||
                    f.EndsWith(".bmp", StringComparison.OrdinalIgnoreCase));

                // Limit the collection to a maximum of five files for the demo
                if (imageFiles.Length > 5)
                {
                    Array.Resize(ref imageFiles, 5);
                }
            }
            else
            {
                // Log missing folder and exit early
                logWriter.WriteLine($"Images folder not found: {imagesFolder}");
                return;
            }

            // Iterate over each image and attempt barcode decoding
            foreach (string imagePath in imageFiles)
            {
                if (!File.Exists(imagePath))
                {
                    logWriter.WriteLine($"File not found: {imagePath}");
                    continue;
                }

                try
                {
                    // Initialize BarCodeReader for OneCode symbology
                    using (var reader = new BarCodeReader(imagePath, DecodeType.OneCode))
                    {
                        // Apply a normal quality preset (optional but recommended)
                        reader.QualitySettings = QualitySettings.NormalQuality;

                        // Perform the decoding operation
                        BarCodeResult[] results = reader.ReadBarCodes();

                        if (results.Length == 0)
                        {
                            // No barcode detected in the current image
                            logWriter.WriteLine($"{Path.GetFileName(imagePath)}: No OneCode barcode detected.");
                        }
                        else
                        {
                            // Log details for each detected barcode
                            foreach (var result in results)
                            {
                                var rect = result.Region.Rectangle;
                                logWriter.WriteLine($"{Path.GetFileName(imagePath)}: Type={result.CodeTypeName}, Text={result.CodeText}, Confidence={result.Confidence}, Quality={result.ReadingQuality}, Region=({rect.X},{rect.Y},{rect.Width},{rect.Height})");
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Record any exception that occurs during processing
                    logWriter.WriteLine($"{Path.GetFileName(imagePath)}: Error - {ex.Message}");
                }
            }
        }

        // Placeholder for Azure Blob Storage download logic (commented out because the SDK is unavailable in this environment)
        /*
        // using Azure.Storage.Blobs;
        // string connectionString = "<your_connection_string>";
        // string containerName = "<your_container_name>";
        // BlobContainerClient container = new BlobContainerClient(connectionString, containerName);
        // foreach (BlobItem blobItem in container.GetBlobs())
        // {
        //     string localPath = Path.Combine(imagesFolder, blobItem.Name);
        //     BlobClient blob = container.GetBlobClient(blobItem.Name);
        //     blob.DownloadTo(localPath);
        //     // Then process localPath as shown above.
        // }
        */

        // Inform the user that processing is complete
        Console.WriteLine("Decoding completed. See log file: " + logFilePath);
    }
}