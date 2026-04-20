using System;
using System.IO;
using Aspose.BarCode.BarCodeRecognition;

class Program
{
    static void Main()
    {
        // Azure Blob parameters (placeholders for real implementation)
        string connectionString = "YourAzureBlobConnectionString";
        string containerName = "your-container";
        string blobName = "sample.png";

        // Local fallback file path
        string localFilePath = "sample.png";

        // Azure Blob download code (commented out – Azure.Storage.Blobs not available in this environment)
        /*
        // Requires Azure.Storage.Blobs NuGet package
        var blobClient = new BlobClient(connectionString, containerName, blobName);
        using (var downloadStream = new MemoryStream())
        {
            blobClient.DownloadTo(downloadStream);
            downloadStream.Position = 0;
            using (var fileStream = new FileStream(localFilePath, FileMode.Create, FileAccess.Write))
            {
                downloadStream.CopyTo(fileStream);
            }
        }
        */

        // Verify that the image file exists before processing
        if (!File.Exists(localFilePath))
        {
            Console.WriteLine($"Image file '{localFilePath}' not found.");
            return;
        }

        // Read all supported barcodes from the image
        using (var reader = new BarCodeReader(localFilePath, DecodeType.AllSupportedTypes))
        {
            foreach (var result in reader.ReadBarCodes())
            {
                Console.WriteLine($"BarCode Type: {result.CodeTypeName}");
                Console.WriteLine($"BarCode CodeText: {result.CodeText}");
            }
        }
    }
}