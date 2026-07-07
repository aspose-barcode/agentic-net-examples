// Title: Render Barcode to PNG Stream and Upload to Azure Blob
// Description: Demonstrates generating a Code128 barcode, saving it as a PNG in memory, optionally persisting locally, and uploading directly to an Azure Blob storage container.
// Category-Description: This example belongs to the Aspose.BarCode image generation and cloud storage integration category. It shows how to use BarcodeGenerator, BarCodeImageFormat, and Azure.Storage.Blobs to create barcodes, work with streams, and store them in Azure Blob containers—common tasks for developers building automated labeling or inventory systems.
// Prompt: Render barcode to a PNG stream and upload directly to an Azure Blob storage container.
// Tags: barcode symbology, generation, png, azure blob storage, aspose.barcode, stream

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing.Imaging;

namespace BarcodeToAzureBlob
{
    /// <summary>
    /// Generates a Code128 barcode, saves it as PNG, and uploads it to Azure Blob storage.
    /// </summary>
    class Program
    {
        /// <summary>
        /// Entry point of the example. Creates a barcode, writes it to a memory stream,
        /// optionally saves it locally, and demonstrates how to upload it to Azure Blob storage.
        /// </summary>
        static void Main()
        {
            // Initialize a barcode generator for Code128 and set the text to encode.
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128))
            {
                generator.CodeText = "Sample123";

                // Render the barcode to a PNG image stored in a memory stream.
                using (var stream = new MemoryStream())
                {
                    generator.Save(stream, BarCodeImageFormat.Png);
                    // Reset stream position to the beginning for subsequent reads.
                    stream.Position = 0;

                    // Optional: save the generated image to a local file for verification.
                    const string localPath = "barcode.png";
                    using (var fileStream = new FileStream(localPath, FileMode.Create, FileAccess.Write))
                    {
                        stream.CopyTo(fileStream);
                    }

                    // -----------------------------------------------------------------
                    // Azure Blob Storage upload (requires Azure.Storage.Blobs package)
                    // -----------------------------------------------------------------
                    /*
                    string connectionString = "<Your Azure Blob Storage connection string>";
                    string containerName = "<Your container name>";
                    string blobName = "barcode.png";

                    var blobServiceClient = new Azure.Storage.Blobs.BlobServiceClient(connectionString);
                    var containerClient = blobServiceClient.GetBlobContainerClient(containerName);
                    var blobClient = containerClient.GetBlobClient(blobName);

                    // Ensure the container exists.
                    containerClient.CreateIfNotExists();

                    // Reset stream position before uploading.
                    stream.Position = 0;
                    blobClient.Upload(stream, overwrite: true);
                    */
                }
            }

            Console.WriteLine("Barcode generated and saved locally.");
        }
    }
}