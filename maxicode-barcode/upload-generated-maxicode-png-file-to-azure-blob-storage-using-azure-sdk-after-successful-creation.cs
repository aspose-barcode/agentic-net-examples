// Title: Generate MaxiCode barcode and upload to Azure Blob storage
// Description: This example creates a MaxiCode barcode, saves it as a PNG file, and shows how to upload the generated image to Azure Blob storage using the Azure SDK.
// Category-Description: Demonstrates Aspose.BarCode barcode generation (EncodeTypes.MaxiCode) and image export (BarCodeImageFormat.Png). Typical scenarios include creating shipping labels or inventory tags where MaxiCode is required, then storing the resulting images in cloud storage for distribution. Developers often use BarcodeGenerator, its Parameters, and Azure.Storage.Blobs to automate barcode creation and cloud upload.
// Prompt: Upload a generated MaxiCode PNG file to Azure Blob storage using the Azure SDK after successful creation.
// Tags: maxicode, barcode generation, png, aspose.barcode, azure.blob.storage

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates generating a MaxiCode barcode and uploading it to Azure Blob storage.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates the barcode, saves it locally, and contains sample code for Azure upload.
    /// </summary>
    static void Main()
    {
        // Define a temporary file path for the generated PNG image
        string outputPath = Path.Combine(Path.GetTempPath(), "maxicode.png");

        // Create a MaxiCode barcode generator with sample text
        using (var generator = new BarcodeGenerator(EncodeTypes.MaxiCode, "Sample MaxiCode Text"))
        {
            // Configure the MaxiCode mode to Mode4 (arbitrary text encoding)
            generator.Parameters.Barcode.MaxiCode.Mode = MaxiCodeMode.Mode4;

            // Set the size of each module (pixel dimension) for better readability
            generator.Parameters.Barcode.XDimension.Pixels = 15f;

            // Save the generated barcode as a PNG file to the specified path
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the barcode image has been saved
        Console.WriteLine($"MaxiCode barcode saved to: {outputPath}");

        // ----------------------------------------------------------------------
        // Azure Blob Storage upload (commented out because the Azure SDK is not
        // available in the snippet runner). In a full development environment,
        // uncomment and provide the appropriate connection string.
        // ----------------------------------------------------------------------
        /*
        using Azure.Storage.Blobs;

        // Azure storage connection details
        string connectionString = "<Your Azure Blob Storage connection string>";
        string containerName = "barcodes";
        string blobName = "maxicode.png";

        // Initialize the Blob service client
        BlobServiceClient serviceClient = new BlobServiceClient(connectionString);

        // Get a reference to the container and create it if it doesn't exist
        BlobContainerClient containerClient = serviceClient.GetBlobContainerClient(containerName);
        containerClient.CreateIfNotExists();

        // Get a reference to the blob (file) within the container
        BlobClient blobClient = containerClient.GetBlobClient(blobName);

        // Open the local PNG file and upload it to Azure Blob storage
        using (FileStream fileStream = File.OpenRead(outputPath))
        {
            blobClient.Upload(fileStream, overwrite: true);
        }

        // Confirm successful upload
        Console.WriteLine($"Uploaded barcode to Azure Blob storage as {blobName}");
        */
    }
}