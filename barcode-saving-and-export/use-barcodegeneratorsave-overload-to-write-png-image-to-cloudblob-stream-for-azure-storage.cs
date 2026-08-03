// Title: Generate Code128 barcode PNG and upload to Azure Blob storage (demo)
// Description: Demonstrates creating a Code128 barcode, saving it as PNG, and showing how to upload the image to Azure Blob storage using a stream.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating the use of BarcodeGenerator and its Save overload to produce image streams. Typical scenarios include generating barcodes on the fly for web services, storing them in cloud storage, or embedding them in documents. Developers often need to convert barcodes to common image formats and write them directly to cloud storage streams such as Azure Blob storage.
// Prompt: Use BarcodeGenerator.Save overload to write a PNG image to a CloudBlob stream for Azure storage.
// Tags: code128, barcode generation, png, azure blob, aspnet, aspose.barcode, image stream

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates barcode generation and (commented) Azure Blob upload using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a Code128 barcode, saves it as PNG to a stream,
    /// and illustrates how to upload the stream to Azure Blob storage.
    /// </summary>
    static void Main()
    {
        // Define the data to encode in the barcode.
        const string codeText = "1234567890";

        // Initialize the barcode generator with Code128 symbology.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
        {
            // Optional: adjust barcode appearance (e.g., X-dimension).
            generator.Parameters.Barcode.XDimension.Point = 2f;

            // Create a memory stream to hold the PNG image.
            using (var memoryStream = new MemoryStream())
            {
                // Save the generated barcode image into the memory stream in PNG format.
                generator.Save(memoryStream, BarCodeImageFormat.Png);
                memoryStream.Position = 0; // Reset position for subsequent reads.

                // -----------------------------------------------------------------
                // Azure Blob upload example (requires Azure.Storage.Blobs package)
                // -----------------------------------------------------------------
                // The following code shows how to upload the PNG stream to Azure Blob storage.
                // It is commented out because the Azure SDK is not referenced in this snippet.
                /*
                var blobServiceClient = new Azure.Storage.Blobs.BlobServiceClient("<connection_string>");
                var containerClient = blobServiceClient.GetBlobContainerClient("<container_name>");
                var blobClient = containerClient.GetBlobClient("barcode.png");
                blobClient.Upload(memoryStream, overwrite: true);
                */

                // For demonstration purposes, write the PNG to a local file instead.
                const string localPath = "barcode.png";
                using (var fileStream = new FileStream(localPath, FileMode.Create, FileAccess.Write))
                {
                    memoryStream.CopyTo(fileStream);
                }

                Console.WriteLine($"Barcode image saved to local file: {localPath}");
                // If Azure upload were enabled, the image would be stored in the specified blob container.
            }
        }
    }
}