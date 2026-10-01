// Title: Save barcode as PNG to Azure Blob storage stream using Aspose.BarCode
// Description: Demonstrates generating a Code128 barcode with Aspose.BarCode, saving it as a PNG image to a MemoryStream, and uploading the stream to Azure Blob storage.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to use BarcodeGenerator and BarCodeImageFormat to create barcode images. Typical use cases include exporting barcodes to cloud storage, such as Azure Blob, for later retrieval in web or mobile applications. Developers often need to stream barcode images directly to storage services without intermediate files.
// Prompt: Use BarcodeGenerator.Save overload to write a PNG image to a CloudBlob stream for Azure storage.
// Tags: barcode symbology, generation, png, azure blob, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates generating a barcode and uploading it to Azure Blob storage as a PNG image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates a Code128 barcode, saves it to a memory stream, writes a local fallback file, and contains commented code for Azure Blob upload.
    /// </summary>
    static void Main()
    {
        // Define the barcode data to encode.
        const string codeText = "1234567890";

        // Select the barcode symbology (Code128 in this case).
        BaseEncodeType encodeType = EncodeTypes.Code128;

        // Create a BarcodeGenerator instance with the chosen symbology and data.
        using (var generator = new BarcodeGenerator(encodeType, codeText))
        {
            // Use a MemoryStream to hold the generated PNG image.
            using (var ms = new MemoryStream())
            {
                // Save the barcode directly to the memory stream in PNG format.
                generator.Save(ms, BarCodeImageFormat.Png);
                ms.Position = 0; // Reset stream position for subsequent reads.

                // ----- Local file fallback (runs in the snippet runner) -----
                const string localPath = "barcode.png";
                using (var fileStream = new FileStream(localPath, FileMode.Create, FileAccess.Write))
                {
                    // Copy the PNG data from the memory stream to a file on disk.
                    ms.CopyTo(fileStream);
                }
                Console.WriteLine($"Barcode saved locally to '{localPath}'.");

                // ----- Real Azure Blob upload (commented out - requires Azure.Storage.Blobs package) -----
                /*
                // Install-Package Azure.Storage.Blobs
                using Azure.Storage.Blobs;
                using Azure.Storage.Blobs.Specialized;

                string connectionString = "<Your Azure Storage connection string>";
                string containerName = "mycontainer";
                string blobName = "barcode.png";

                // Create a client for the target container and ensure it exists.
                BlobContainerClient containerClient = new BlobContainerClient(connectionString, containerName);
                containerClient.CreateIfNotExists();

                // Get a reference to the blob where the image will be stored.
                BlobClient blobClient = containerClient.GetBlobClient(blobName);

                // Reset stream position before uploading.
                ms.Position = 0;

                // Upload the PNG image stream to Azure Blob storage, overwriting if it already exists.
                blobClient.Upload(ms, overwrite: true);
                Console.WriteLine($"Barcode uploaded to Azure Blob storage as '{blobName}'.");
                */
            }
        }
    }
}