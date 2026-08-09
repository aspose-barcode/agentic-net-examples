// Title: Generate MaxiCode barcode and upload to Azure Blob storage
// Description: Demonstrates creating a MaxiCode barcode image in PNG format using Aspose.BarCode and outlines how to upload the file to Azure Blob storage with the Azure SDK.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing the use of BarcodeGenerator, EncodeTypes, and BarCodeImageFormat to produce barcode images. Typical use cases include creating shipping labels or inventory tags, after which developers often need to store the generated images in cloud storage such as Azure Blob. The snippet illustrates the workflow from barcode creation to cloud upload, useful for developers integrating barcode generation into cloud‑based applications.
// Prompt: Upload a generated MaxiCode PNG file to Azure Blob storage using the Azure SDK after successful creation.
// Tags: maxicode, barcode generation, png, azure blob storage, aspose.barcode, upload

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Generates a MaxiCode barcode image and demonstrates how to upload it to Azure Blob storage.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates a MaxiCode PNG file and (optionally) uploads it to Azure Blob storage.
    /// </summary>
    static void Main()
    {
        // Define the local file path where the generated MaxiCode image will be saved.
        string localPath = Path.Combine(Directory.GetCurrentDirectory(), "maxicode.png");

        // Create a BarcodeGenerator for the MaxiCode symbology with the desired message.
        using (var generator = new BarcodeGenerator(EncodeTypes.MaxiCode, "Test message"))
        {
            // Save the generated barcode directly to a PNG file.
            generator.Save(localPath, BarCodeImageFormat.Png);
        }

        // ----------------------------------------------------------------------
        // Azure Blob Storage upload (commented out because the Azure SDK is not
        // available in the current execution environment). Uncomment and adjust
        // the code below when running in an environment with Azure.Storage.Blobs
        // installed and a valid connection string.
        // ----------------------------------------------------------------------
        /*
        // Install-Package Azure.Storage.Blobs
        using Azure.Storage.Blobs;
        using Azure.Storage.Blobs.Specialized;

        string connectionString = "<Your Azure Blob Storage connection string>";
        string containerName = "<Your container name>";
        string blobName = "maxicode.png";

        // Create a client to interact with the Azure storage account.
        BlobServiceClient serviceClient = new BlobServiceClient(connectionString);

        // Get a reference to the container (creates it if it does not exist).
        BlobContainerClient containerClient = serviceClient.GetBlobContainerClient(containerName);
        containerClient.CreateIfNotExists();

        // Get a reference to the target blob within the container.
        BlobClient blobClient = containerClient.GetBlobClient(blobName);

        // Open the local PNG file and upload its contents to the blob.
        using (FileStream fileStream = File.OpenRead(localPath))
        {
            blobClient.Upload(fileStream, overwrite: true);
        }

        Console.WriteLine($"Uploaded '{localPath}' to blob '{blobName}' in container '{containerName}'.");
        */

        // Indicate that the barcode image has been generated (and would be uploaded if enabled).
        Console.WriteLine($"Generated MaxiCode image saved to: {localPath}");
    }
}