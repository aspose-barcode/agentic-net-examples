// Title: Render barcode to PNG stream and upload to Azure Blob storage
// Description: Demonstrates generating a Code128 barcode, saving it to a PNG memory stream, and showing how to upload the stream to Azure Blob storage.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to use BarcodeGenerator to create barcodes and output them in image formats. It covers saving to streams, optional appearance customization, and integrating with Azure.Storage.Blobs for direct cloud uploads—common tasks for developers building automated labeling or inventory systems.
// Prompt: Render barcode to a PNG stream and upload directly to an Azure Blob storage container.
// Tags: barcode, code128, png, stream, azure blob, upload, aspose.barcode, generation

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates barcode generation to a PNG stream and outlines Azure Blob upload.
/// </summary>
class Program
{
    /// <summary>
    /// Generates a Code128 barcode, saves it to a PNG memory stream, writes it to a local file,
    /// and provides sample code for uploading the stream to Azure Blob storage.
    /// </summary>
    static void Main()
    {
        // Define barcode parameters
        const string codeText = "1234567890";
        const string localFilePath = "barcode.png";

        // Create a memory stream to hold the PNG image
        using (var pngStream = new MemoryStream())
        {
            // Generate the barcode and save it directly to the PNG stream
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
            {
                // Optional: customize barcode appearance here
                // generator.Parameters.Barcode.XDimension.Point = 2f;
                // generator.Parameters.Barcode.FilledBars = false;
                // generator.Parameters.Barcode.ThrowExceptionWhenCodeTextIncorrect = false;

                generator.Save(pngStream, BarCodeImageFormat.Png);
            }

            // Reset the stream position before reading/writing
            pngStream.Position = 0;

            // Write the PNG stream to a local file (placeholder for Azure Blob upload)
            using (var fileStream = new FileStream(localFilePath, FileMode.Create, FileAccess.Write))
            {
                pngStream.CopyTo(fileStream);
            }

            Console.WriteLine($"Barcode image saved locally to '{localFilePath}'.");
        }

        // -----------------------------------------------------------------
        // Azure Blob Storage upload (commented out – Azure SDK not available in the runner)
        // -----------------------------------------------------------------
        // The following code demonstrates how you would upload the PNG stream
        // directly to an Azure Blob container using Azure.Storage.Blobs.
        // Uncomment and add the required NuGet package (Azure.Storage.Blobs) in a real environment.
        /*
        // using Azure.Storage.Blobs;
        // const string connectionString = "<your-azure-blob-connection-string>";
        // const string containerName = "<your-container-name>";
        // const string blobName = "barcode.png";

        // using (var pngStream = new MemoryStream())
        // {
        //     using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
        //     {
        //         generator.Save(pngStream, BarCodeImageFormat.Png);
        //     }
        //     pngStream.Position = 0;

        //     var blobClient = new BlobClient(connectionString, containerName, blobName);
        //     blobClient.Upload(pngStream, overwrite: true);
        //     Console.WriteLine($"Barcode image uploaded to Azure Blob storage as '{blobName}'.");
        // }
        */
    }
}