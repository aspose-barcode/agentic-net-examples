// Title: Render Barcode to PNG Stream and Upload to Azure Blob Storage
// Description: Demonstrates generating a Code128 barcode, saving it as a PNG in a memory stream, and uploading the image directly to an Azure Blob storage container (simulated by a local file write).
// Category-Description: This example belongs to the Aspose.BarCode generation and image export category. It shows how to use the BarcodeGenerator class together with BarCodeImageFormat to create barcode images in memory, a common requirement for web services that need to store or transmit barcodes without intermediate files. Developers often combine this with cloud storage SDKs such as Azure.Storage.Blobs to persist the generated images directly to cloud containers.
// Prompt: Render barcode to a PNG stream and upload directly to an Azure Blob storage container.
// Tags: barcode, code128, png, azure blob, aspose.barcode, image generation, cloud storage

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing.Imaging;

namespace BarcodeToAzureBlobDemo
{
    /// <summary>
    /// Demonstrates rendering a barcode to a PNG stream and uploading it to Azure Blob storage.
    /// </summary>
    class Program
    {
        /// <summary>
        /// Entry point that generates a barcode, writes it to a PNG stream, and uploads it.
        /// </summary>
        static void Main()
        {
            // Create an in‑memory stream to hold the generated PNG barcode image
            using (var barcodeStream = new MemoryStream())
            {
                // Initialize the barcode generator with Code128 symbology and the desired data
                var generator = new BarcodeGenerator(EncodeTypes.Code128, "12345678");

                // Save the barcode directly into the memory stream in PNG format
                generator.Save(barcodeStream, BarCodeImageFormat.Png);

                // Reset stream position to the beginning before reading or uploading
                barcodeStream.Position = 0;

                // Simulate an Azure Blob upload by writing the stream to a temporary local file
                string localPath = Path.Combine(Path.GetTempPath(), "uploaded_barcode.png");
                using (var fileStream = new FileStream(localPath, FileMode.Create, FileAccess.Write))
                {
                    barcodeStream.CopyTo(fileStream);
                }

                Console.WriteLine($"Barcode PNG saved to local path: {localPath}");

                // Real Azure Blob upload (commented out – Azure SDK not available in this environment)
                /*
                // Install-Package Azure.Storage.Blobs
                // using Azure.Storage.Blobs;
                // string connectionString = "<your_connection_string>";
                // string containerName = "<your_container_name>";
                // string blobName = "barcode.png";
                // BlobContainerClient container = new BlobContainerClient(connectionString, containerName);
                // container.CreateIfNotExists();
                // BlobClient blob = container.GetBlobClient(blobName);
                // barcodeStream.Position = 0;
                // blob.Upload(barcodeStream, overwrite:true);
                // Console.WriteLine($"Uploaded barcode to Azure Blob: {blob.Uri}");
                */
            }
        }
    }
}