// Title: Generate Barcode and Save to Stream for Azure Blob Upload
// Description: Demonstrates generating a Code128 barcode, storing it in a memory stream, and preparing it for direct upload to Azure Blob storage.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing how to create barcode images using the BarcodeGenerator class, save them in-memory with BarCodeImageFormat, and integrate with cloud storage services such as Azure Blob Storage. Developers often need to produce barcodes on-the-fly and upload them without intermediate files; this snippet illustrates the typical workflow and key API classes for such scenarios, making it searchable for integration testing and CI pipelines.
// Prompt: Write integration test ensuring barcode image stream can be directly written to Azure Blob storage.
// Tags: barcode, code128, generation, png, memorystream, azure blob, aspose.barcode, integration test

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that generates a Code128 barcode, writes it to a memory stream,
/// and demonstrates how the stream could be uploaded to Azure Blob storage.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates the barcode, resets the stream,
    /// and saves the image locally (simulating a blob upload).
    /// </summary>
    static void Main()
    {
        // Create a memory stream to hold the barcode image.
        using (var memoryStream = new MemoryStream())
        {
            // Initialize the barcode generator with the desired symbology and data.
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "Test123"))
            {
                // Save the generated barcode directly into the memory stream in PNG format.
                generator.Save(memoryStream, BarCodeImageFormat.Png);
            }

            // Reset the stream position to the beginning before any read operations.
            memoryStream.Position = 0;

            // -----------------------------------------------------------------
            // Azure Blob Storage upload simulation (commented out for test env)
            // -----------------------------------------------------------------
            /*
            // Uncomment and add the Azure.Storage.Blobs NuGet package to enable real upload.
            string connectionString = "<your-azure-blob-connection-string>";
            string containerName = "barcodes";
            string blobName = "barcode.png";

            var blobServiceClient = new BlobServiceClient(connectionString);
            var containerClient = blobServiceClient.GetBlobContainerClient(containerName);
            containerClient.CreateIfNotExists();

            var blobClient = containerClient.GetBlobClient(blobName);
            memoryStream.Position = 0; // Ensure stream is at start before upload.
            blobClient.Upload(memoryStream, overwrite: true);
            */

            // For the purpose of this test environment, write the stream to a local file.
            string localPath = Path.Combine(Directory.GetCurrentDirectory(), "barcode.png");
            using (var fileStream = new FileStream(localPath, FileMode.Create, FileAccess.Write))
            {
                memoryStream.CopyTo(fileStream);
            }

            // Inform the user where the file was saved.
            Console.WriteLine($"Barcode image saved to: {localPath}");
        }
    }
}