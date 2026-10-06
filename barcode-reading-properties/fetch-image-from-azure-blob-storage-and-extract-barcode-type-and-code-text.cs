// Title: Extract barcode type and data from an image (Azure Blob example)
// Description: Demonstrates reading an image—optionally from Azure Blob storage—and using Aspose.BarCode to detect all supported barcode symbologies, outputting each barcode's type and decoded text.
// Category-Description: This example belongs to the Aspose.BarCode barcode recognition category, showcasing the BarCodeReader class with DecodeType.AllSupportedTypes. Typical scenarios include scanning documents, inventory labels, or any image containing barcodes to retrieve their data programmatically. Developers often need to load images from various sources (file system, streams, cloud storage) and extract barcode information for further processing.
// Prompt: Fetch image from Azure Blob storage and extract barcode type and code text.
// Tags: barcode recognition, azure blob, decode all types, aspose.barcode, csharp

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates extracting barcode information from an image, with optional Azure Blob storage retrieval.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Loads a local image (or Azure Blob) and processes it for barcodes.
    /// </summary>
    /// <param name="args">Command‑line arguments (not used).</param>
    static void Main(string[] args)
    {
        // Path to a local sample image used when Azure Blob access is not configured.
        string localImagePath = "sample.png";

        // Uncomment and adjust the following block to download the image from Azure Blob storage.
        /*
        // using Azure.Storage.Blobs;
        // string connectionString = "<your_connection_string>";
        // string containerName = "<your_container_name>";
        // string blobName = "<your_blob_name>";
        // BlobClient blobClient = new BlobClient(connectionString, containerName, blobName);
        // using (MemoryStream ms = new MemoryStream())
        // {
        //     blobClient.DownloadTo(ms);
        //     ms.Position = 0;
        //     ProcessStream(ms);
        // }
        */

        // Verify that the local image file exists before attempting processing.
        if (!File.Exists(localImagePath))
        {
            Console.WriteLine($"Image file not found: {localImagePath}");
            return;
        }

        // Process the image file to read any barcodes it contains.
        ProcessFile(localImagePath);
    }

    /// <summary>
    /// Reads all supported barcodes from the specified image file and writes their type and data to the console.
    /// </summary>
    /// <param name="imagePath">Full path to the image file.</param>
    static void ProcessFile(string imagePath)
    {
        // Initialize BarCodeReader to scan the image for every supported barcode symbology.
        using (BarCodeReader reader = new BarCodeReader(imagePath, DecodeType.AllSupportedTypes))
        {
            // Perform the recognition and retrieve all results.
            BarCodeResult[] results = reader.ReadBarCodes();

            // If no barcodes were found, inform the user.
            if (results.Length == 0)
            {
                Console.WriteLine("No barcodes detected.");
                return;
            }

            // Output each detected barcode's type and decoded text.
            foreach (BarCodeResult result in results)
            {
                Console.WriteLine($"Barcode type: {result.CodeTypeName}, Barcode Data: {result.CodeText}");
            }
        }
    }
}