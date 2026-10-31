// Title: Render Code128 barcode to PNG stream and upload to Azure Blob storage
// Description: Demonstrates generating a Code128 barcode, saving it to a PNG memory stream, and uploading the stream directly to an Azure Blob storage container.
// Category-Description: This example belongs to the Aspose.BarCode generation and output category, showcasing how to use BarcodeGenerator with EncodeTypes and BarCodeImageFormat to create barcodes, and how to integrate the resulting image stream with Azure.Storage.Blobs for cloud storage. Developers often need to generate barcodes on the fly and store them in cloud services without intermediate files.
// Prompt: Render barcode to a PNG stream and upload directly to an Azure Blob storage container.
// Tags: code128, barcode generation, png, aspose.barcode, aspose.drawing, azure.blob.storage

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that generates a Code128 barcode, writes it to a PNG memory stream,
/// and demonstrates how to upload that stream to Azure Blob storage (code commented out).
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates the barcode, prepares the PNG stream, and either uploads it to Azure Blob storage
    /// or saves it locally as a fallback.
    /// </summary>
    static void Main()
    {
        // Create a memory stream to hold the generated PNG image
        using (var barcodeStream = new MemoryStream())
        {
            // Initialize the barcode generator with Code128 symbology and the desired data
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "12345678"))
            {
                // Save the barcode directly into the memory stream in PNG format
                generator.Save(barcodeStream, BarCodeImageFormat.Png);
            }

            // Reset stream position to the beginning before reading or uploading
            barcodeStream.Position = 0;

            // ----- Azure Blob Storage upload (requires Azure.Storage.Blobs package) -----
            // The following code demonstrates how to upload the stream to Azure Blob storage.
            // It is commented out because the Azure SDK is not available in the snippet runner.
            /*
            string connectionString = "<Your Azure Blob Storage connection string>";
            string containerName = "mycontainer";
            string blobName = "barcode.png";

            var blobServiceClient = new Azure.Storage.Blobs.BlobServiceClient(connectionString);
            var containerClient = blobServiceClient.GetBlobContainerClient(containerName);
            containerClient.CreateIfNotExists();
            var blobClient = containerClient.GetBlobClient(blobName);
            blobClient.Upload(barcodeStream, overwrite: true);
            Console.WriteLine($"Uploaded barcode to Azure Blob storage: {blobClient.Uri}");
            */

            // Fallback: save the PNG stream to a local file if Azure upload is not performed
            string localPath = Path.Combine(Directory.GetCurrentDirectory(), "barcode.png");
            using (var fileStream = new FileStream(localPath, FileMode.Create, FileAccess.Write))
            {
                barcodeStream.CopyTo(fileStream);
            }

            Console.WriteLine($"Barcode saved locally to {localPath}");
        }
    }
}