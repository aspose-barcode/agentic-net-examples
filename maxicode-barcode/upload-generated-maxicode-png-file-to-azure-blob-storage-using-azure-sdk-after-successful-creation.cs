// Title: Generate MaxiCode barcode and (optionally) upload to Azure Blob storage
// Description: Demonstrates creating a MaxiCode barcode as a PNG image using Aspose.BarCode and shows how to upload the file to Azure Blob storage.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, illustrating the use of BarcodeGenerator with EncodeTypes.MaxiCode. It covers setting barcode dimensions, saving to PNG, and integrating with Azure.Storage.Blobs for cloud storage—a common requirement for applications that need to distribute or archive generated barcodes.
// Prompt: Upload a generated MaxiCode PNG file to Azure Blob storage using the Azure SDK after successful creation.
// Tags: maxicode, barcode generation, png, aspose.barcode, azure blob storage

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates generating a MaxiCode barcode image and optionally uploading it to Azure Blob storage.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a MaxiCode PNG file and contains commented code for Azure Blob upload.
    /// </summary>
    static void Main()
    {
        // --------------------------------------------------------------------
        // Prepare a unique temporary output folder for the generated barcode.
        // --------------------------------------------------------------------
        string outputFolder = Path.Combine(Path.GetTempPath(), "MaxiCodeDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputFolder);
        string barcodePath = Path.Combine(outputFolder, "maxicode.png");

        // ---------------------------------------------------------------
        // Generate the MaxiCode barcode using Aspose.BarCode.
        // ---------------------------------------------------------------
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.MaxiCode, "Åspóse.Barcóde©"))
        {
            // Set the X-dimension (module size) in pixels.
            generator.Parameters.Barcode.XDimension.Pixels = 15f;

            // Save the barcode as a PNG image to the specified path.
            generator.Save(barcodePath, BarCodeImageFormat.Png);
        }

        Console.WriteLine($"MaxiCode barcode saved to: {barcodePath}");

        // --------------------------------------------------------------------
        // Azure Blob upload (requires Azure.Storage.Blobs NuGet package).
        // The following code is provided as a template; uncomment and configure
        // the connection details to enable uploading.
        // --------------------------------------------------------------------
        /*
        // using Azure.Storage.Blobs;
        // string connectionString = "<Your Azure Blob Storage connection string>";
        // string containerName = "<Your container name>";
        // string blobName = "maxicode.png";

        // BlobServiceClient serviceClient = new BlobServiceClient(connectionString);
        // BlobContainerClient containerClient = serviceClient.GetBlobContainerClient(containerName);
        // containerClient.CreateIfNotExists();

        // BlobClient blobClient = containerClient.GetBlobClient(blobName);
        // using (FileStream uploadFileStream = File.OpenRead(barcodePath))
        // {
        //     blobClient.Upload(uploadFileStream, overwrite: true);
        // }
        // Console.WriteLine($"Uploaded barcode to Azure Blob storage as: {blobName}");
        */

        // End of program
    }
}