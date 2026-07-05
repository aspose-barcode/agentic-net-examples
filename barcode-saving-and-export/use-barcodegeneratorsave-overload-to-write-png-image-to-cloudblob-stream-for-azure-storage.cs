// Title: Generate Code128 barcode PNG and upload to Azure Blob storage (demo)
// Description: Demonstrates creating a Code128 barcode, saving it as PNG to a stream, and showing how to upload the stream to Azure Blob storage.
// Prompt: Use BarcodeGenerator.Save overload to write a PNG image to a CloudBlob stream for Azure storage.
// Tags: barcode, code128, png, azure blob, aspose.barcode, stream

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing.Imaging;

namespace BarcodeToAzureBlobDemo
{
    /// <summary>
    /// Demonstrates barcode generation and (simulated) upload to Azure Blob storage.
    /// </summary>
    class Program
    {
        /// <summary>
        /// Entry point. Generates a Code128 barcode, saves it as PNG to a stream,
        /// and writes the image to a local file (placeholder for Azure Blob upload).
        /// </summary>
        static void Main()
        {
            // Initialize a barcode generator for Code128 with sample text.
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
            {
                // Optional: customize barcode appearance (blue bars on white background).
                generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Blue;
                generator.Parameters.BackColor = Aspose.Drawing.Color.White;

                // Save the barcode image to a memory stream in PNG format.
                using (var memoryStream = new MemoryStream())
                {
                    generator.Save(memoryStream, BarCodeImageFormat.Png);
                    memoryStream.Position = 0; // Reset stream position for subsequent reading.

                    // -----------------------------------------------------------------
                    // Real Azure Blob storage implementation (requires Azure.Storage.Blobs NuGet package):
                    // -----------------------------------------------------------------
                    // using Azure.Storage.Blobs;
                    // var blobServiceClient = new BlobServiceClient("<connection-string>");
                    // var containerClient = blobServiceClient.GetBlobContainerClient("mycontainer");
                    // var blobClient = containerClient.GetBlobClient("barcode.png");
                    // await blobClient.UploadAsync(memoryStream, overwrite: true);
                    // -----------------------------------------------------------------
                    // Since Azure SDK is not available in the snippet runner, write to a local file instead.

                    const string localFilePath = "barcode.png";

                    // Write the PNG data from the memory stream to a local file.
                    using (var fileStream = new FileStream(localFilePath, FileMode.Create, FileAccess.Write))
                    {
                        memoryStream.CopyTo(fileStream);
                    }

                    Console.WriteLine($"Barcode image saved to '{localFilePath}'.");
                }
            }
        }
    }
}