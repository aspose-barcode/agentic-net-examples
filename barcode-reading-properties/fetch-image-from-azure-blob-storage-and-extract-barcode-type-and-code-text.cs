// Title: Extract barcode information from an image stored in Azure Blob Storage
// Description: Demonstrates how to download an image from Azure Blob storage (illustrated as a placeholder) and use Aspose.BarCode to recognize barcode type and text.
// Category-Description: This example belongs to the Aspose.BarCode recognition category, showcasing the BarCodeReader class for decoding various symbologies from image streams. Typical use cases include processing scanned documents, inventory images, or any media retrieved from cloud storage. Developers often need to integrate Azure Blob retrieval with barcode extraction for automated workflows.
// Prompt: Fetch image from Azure Blob storage and extract barcode type and code text.
// Tags: barcode recognition, azure blob storage, decode type, image processing, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Sample program that demonstrates how to obtain an image from Azure Blob storage
/// (shown as a commented placeholder) and extract barcode information using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Reads an image, creates a BarCodeReader,
    /// and prints detected barcode types and their corresponding text values.
    /// </summary>
    static void Main()
    {
        // NOTE: In a real environment you would download the image from Azure Blob Storage.
        // The Azure SDK is not available in the snippet runner, so the code is shown as a comment.
        /*
        // Azure Blob Storage example (requires Azure.Storage.Blobs NuGet package)
        // string connectionString = "<your_connection_string>";
        // string containerName = "<your_container_name>";
        // string blobName = "<your_blob_name>";
        // var blobClient = new BlobClient(connectionString, containerName, blobName);
        // using var memoryStream = new MemoryStream();
        // blobClient.DownloadTo(memoryStream);
        // memoryStream.Position = 0;
        // ProcessImageStream(memoryStream);
        */

        // Local fallback image for the runnable example
        string imagePath = "sample.png";

        // Verify that the image file exists before attempting to read it
        if (!File.Exists(imagePath))
        {
            Console.WriteLine($"Image file not found: {imagePath}");
            return;
        }

        // Initialize BarCodeReader to scan all supported barcode types in the image file
        using (BarCodeReader reader = new BarCodeReader(imagePath, DecodeType.AllSupportedTypes))
        {
            // Iterate through all detected barcodes and output their type and text
            foreach (var result in reader.ReadBarCodes())
            {
                Console.WriteLine($"BarCode Type: {result.CodeTypeName}");
                Console.WriteLine($"BarCode CodeText: {result.CodeText}");
            }
        }
    }
}