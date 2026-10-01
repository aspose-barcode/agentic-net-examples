// Title: Extract barcode type and text from an image (simulated Azure Blob download)
// Description: Demonstrates loading an image—originally intended to be fetched from Azure Blob storage—and using Aspose.BarCode to detect and read any barcode present, outputting its type and decoded text.
// Category-Description: This example belongs to the Aspose.BarCode barcode recognition category, showcasing the BarCodeReader class with DecodeType.AllSupportedTypes to automatically identify any supported symbology. Typical use cases include processing scanned documents, inventory images, or any media where barcode data must be extracted programmatically. Developers often need to integrate such recognition into workflows that retrieve images from cloud storage, then parse barcode information for downstream processing.
// Prompt: Fetch image from Azure Blob storage and extract barcode type and code text.
// Tags: barcode,recognition,azure blob storage,aspose.barcode,c#,image processing

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates fetching an image (simulated) and extracting barcode information using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Reads a barcode from an image file (or Azure Blob storage) and prints its type and text.
    /// </summary>
    static void Main()
    {
        // In a real environment you would download the image from Azure Blob Storage.
        // The Azure SDK is not available in the snippet runner, so the code is shown as a comment.
        /*
        // Azure Blob Storage download (requires Azure.Storage.Blobs package)
        // string connectionString = "<your_connection_string>";
        // string containerName = "<your_container_name>";
        // string blobName = "<your_blob_name>";
        // var blobClient = new BlobClient(connectionString, containerName, blobName);
        // using var downloadStream = new MemoryStream();
        // blobClient.DownloadTo(downloadStream);
        // downloadStream.Position = 0;
        // // Save to a temporary file for Aspose.BarCode processing
        // string tempPath = Path.Combine(Path.GetTempPath(), "downloaded_image.png");
        // using (var fileStream = new FileStream(tempPath, FileMode.Create, FileAccess.Write))
        // {
        //     downloadStream.CopyTo(fileStream);
        // }
        // string imagePath = tempPath;
        */

        // Fallback to a local sample image for the runnable example.
        string imagePath = "sample.png";

        // Verify that the image file exists before attempting to read it.
        if (!File.Exists(imagePath))
        {
            Console.WriteLine($"Image file not found: {imagePath}");
            return;
        }

        // Use DecodeType.AllSupportedTypes to detect any barcode symbology.
        BaseDecodeType decodeType = DecodeType.AllSupportedTypes;

        // Initialize the barcode reader with the image path and the chosen decode type.
        using (var reader = new BarCodeReader(imagePath, decodeType))
        {
            // Read all barcodes found in the image.
            BarCodeResult[] results = reader.ReadBarCodes();

            // Handle the case where no barcodes are detected.
            if (results == null || results.Length == 0)
            {
                Console.WriteLine("No barcode detected in the image.");
                return;
            }

            // Iterate through each detected barcode and output its details.
            foreach (BarCodeResult result in results)
            {
                Console.WriteLine($"Detected Barcode Type: {result.CodeTypeName}");
                Console.WriteLine($"Code Text: {result.CodeText}");
                Console.WriteLine($"Reading Quality: {result.ReadingQuality}");
                Console.WriteLine();
            }
        }
    }
}