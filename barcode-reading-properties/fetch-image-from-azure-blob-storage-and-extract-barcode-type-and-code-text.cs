// Title: Read barcode from image (local fallback)
// Description: Demonstrates fetching an image (placeholder for Azure Blob) and extracting barcode type and text using Aspose.BarCode.
// Prompt: Fetch image from Azure Blob storage and extract barcode type and code text.
// Tags: barcode symbology, read, console, aspose.barcode, azure blob

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Example program that reads barcodes from an image.
/// In a real scenario the image would be downloaded from Azure Blob storage,
/// but this demo uses a local file as a fallback.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Loads an image, scans for barcodes, and prints their type and text.
    /// </summary>
    static void Main()
    {
        // NOTE: In a real environment you would download the image from Azure Blob Storage
        // using Azure.Storage.Blobs. The required SDK is not available in the snippet runner,
        // so the code is provided as a comment for reference.
        /*
        // Real Azure Blob download (requires Azure.Storage.Blobs NuGet package)
        string connectionString = "<your_connection_string>";
        string containerName = "<your_container_name>";
        string blobName = "<your_blob_name>";
        var blobClient = new BlobClient(connectionString, containerName, blobName);
        using (var downloadStream = new MemoryStream())
        {
            blobClient.DownloadTo(downloadStream);
            downloadStream.Position = 0;
            // Proceed with barcode reading using the stream
        }
        */

        // Fallback: use a local image file for demonstration
        string localImagePath = "sample.png";

        // Verify that the image file exists before proceeding
        if (!File.Exists(localImagePath))
        {
            Console.WriteLine($"Image file not found: {localImagePath}");
            return;
        }

        // Load the image into a Bitmap (Aspose.Drawing)
        using (Bitmap bitmap = new Bitmap(localImagePath))
        {
            // Initialize the barcode reader with the bitmap and enable all supported types
            using (BarCodeReader reader = new BarCodeReader(bitmap, DecodeType.AllSupportedTypes))
            {
                // Iterate through detected barcodes
                foreach (BarCodeResult result in reader.ReadBarCodes())
                {
                    // Output the barcode type (e.g., QR, Code128) and its decoded text
                    Console.WriteLine($"BarCode Type: {result.CodeTypeName}");
                    Console.WriteLine($"BarCode CodeText: {result.CodeText}");
                }
            }
        }
    }
}