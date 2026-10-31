// Title: Generate Code128 barcode and write to stream for Azure Blob storage
// Description: This example creates a Code128 barcode, saves it into a MemoryStream, and writes the image to a temporary file. The same stream can be used to upload the barcode directly to Azure Blob storage.
// Category-Description: Demonstrates Aspose.BarCode barcode generation and stream handling, a common scenario for cloud‑based storage or transmission. It showcases the BarcodeGenerator class, EncodeTypes enumeration, and saving to BarCodeImageFormat. Developers often need to generate barcodes on‑the‑fly and store them in Azure Blob containers without intermediate files.
// Prompt: Write integration test ensuring barcode image stream can be directly written to Azure Blob storage.
// Tags: barcode, code128, generation, stream, azure, blob, png, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates barcode generation and saving to a stream for potential Azure Blob upload.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that creates a Code128 barcode, writes it to a MemoryStream, and saves it to a temporary file.
    /// </summary>
    static void Main()
    {
        // Define the text to encode in the barcode
        string codeText = "Test12345";

        // Create a memory stream to hold the generated barcode image
        using (var memoryStream = new MemoryStream())
        {
            // Generate the barcode using Aspose.BarCode
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
            {
                // Save the barcode image as PNG into the memory stream
                generator.Save(memoryStream, BarCodeImageFormat.Png);
            }

            // Reset stream position to the beginning before reading
            memoryStream.Position = 0;

            // Determine a temporary file path for demonstration purposes
            string localPath = Path.Combine(Path.GetTempPath(), "barcode.png");

            // Write the memory stream contents to the temporary file
            using (var fileStream = new FileStream(localPath, FileMode.Create, FileAccess.Write))
            {
                memoryStream.CopyTo(fileStream);
            }

            // Output information about the saved file
            Console.WriteLine($"Barcode image saved to temporary file: {localPath}");
            Console.WriteLine($"File exists: {File.Exists(localPath)}");
            Console.WriteLine($"File size: {new FileInfo(localPath).Length} bytes");
        }

        // Real Azure Blob upload (requires Azure.Storage.Blobs package)
        /*
        // using Azure.Storage.Blobs;
        // string connectionString = "<your_connection_string>";
        // string containerName = "barcodes";
        // string blobName = "barcode.png";
        // var container = new BlobContainerClient(connectionString, containerName);
        // container.CreateIfNotExists();
        // var blob = container.GetBlobClient(blobName);
        // memoryStream.Position = 0;
        // blob.Upload(memoryStream, overwrite: true);
        // Console.WriteLine($"Uploaded barcode to Azure Blob: {blob.Uri}");
        */
    }
}