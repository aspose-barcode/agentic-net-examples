using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates barcode recognition from an image using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Loads an image (local fallback) and reads all supported barcodes,
    /// printing each barcode's type and decoded text to the console.
    /// </summary>
    static void Main()
    {
        // NOTE: In a real environment you would fetch the image from Azure Blob Storage using Azure.Storage.Blobs.
        // The following commented code demonstrates how it could be done when the required package is available.
        /*
        string connectionString = "<your_connection_string>";
        string containerName = "<your_container_name>";
        string blobName = "<your_blob_name>";

        var blobClient = new BlobClient(connectionString, containerName, blobName);
        using var memoryStream = new MemoryStream();
        blobClient.DownloadTo(memoryStream);
        memoryStream.Position = 0;
        // Use memoryStream as the image source for barcode recognition.
        */

        // For the runnable example, use a local image file as a fallback.
        string imagePath = "sample.png";

        // Verify that the image file exists before attempting to process it.
        if (!File.Exists(imagePath))
        {
            Console.WriteLine($"Image file not found: {imagePath}");
            return;
        }

        // Load the image into a Bitmap object (Aspose.Drawing) for processing.
        using (var bitmap = new Bitmap(imagePath))
        {
            // Initialize the barcode reader to decode all supported barcode types.
            using (var reader = new BarCodeReader(bitmap, DecodeType.AllSupportedTypes))
            {
                // Iterate through each detected barcode and output its details.
                foreach (var result in reader.ReadBarCodes())
                {
                    Console.WriteLine($"BarCode Type: {result.CodeTypeName}");
                    Console.WriteLine($"BarCode CodeText: {result.CodeText}");
                }
            }
        }
    }
}