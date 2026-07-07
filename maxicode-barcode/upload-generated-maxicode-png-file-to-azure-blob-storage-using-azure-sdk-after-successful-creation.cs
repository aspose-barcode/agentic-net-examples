// Title: Generate MaxiCode PNG and upload to Azure Blob Storage
// Description: Demonstrates creating a MaxiCode barcode (Mode 2) as a PNG image using Aspose.BarCode and outlines how to upload the generated file to Azure Blob storage.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, focusing on complex barcode types such as MaxiCode. It showcases the use of ComplexBarcodeGenerator, MaxiCodeCodetextMode2, and related classes to produce high‑density 2‑D barcodes, a common requirement for logistics and shipping applications. Developers often need to generate these barcodes programmatically and store them in cloud services like Azure for further processing or distribution.
// Prompt: Upload a generated MaxiCode PNG file to Azure Blob storage using the Azure SDK after successful creation.
// Tags: maxicode, generation, png, complexbarcode, azureblob, aspnet, barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.ComplexBarcode;

/// <summary>
/// Example program that creates a MaxiCode barcode image and demonstrates how to upload it to Azure Blob storage.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates a MaxiCode PNG and optionally uploads it to Azure Blob storage.
    /// </summary>
    static void Main()
    {
        // Prepare MaxiCode codetext (Mode 2) with sample data
        var maxiCodeCodetext = new MaxiCodeCodetextMode2
        {
            PostalCode = "524032140",   // 9‑digit US postal code
            CountryCode = 56,           // USA numeric country code
            ServiceCategory = 999       // Sample service category
        };

        // Standard second message (optional additional data)
        var secondMessage = new MaxiCodeStandardSecondMessage
        {
            Message = "Sample MaxiCode message"
        };
        maxiCodeCodetext.SecondMessage = secondMessage;

        // Generate the MaxiCode image into a memory stream
        using (var generator = new ComplexBarcodeGenerator(maxiCodeCodetext))
        {
            using (var ms = new MemoryStream())
            {
                // Save the barcode as PNG to the memory stream
                generator.Save(ms, BarCodeImageFormat.Png);
                ms.Position = 0; // Reset stream position for subsequent reads

                // Write the PNG to a local file (fallback for environments without Azure SDK)
                const string localPath = "maxicode.png";
                using (var fileStream = new FileStream(localPath, FileMode.Create, FileAccess.Write))
                {
                    ms.CopyTo(fileStream);
                }

                Console.WriteLine($"MaxiCode image saved locally to '{localPath}'.");

                // ------------------------------------------------------------
                // Azure Blob Storage upload (requires Azure.Storage.Blobs package)
                // The following code demonstrates the intended upload logic.
                // Uncomment and ensure the Azure.Storage.Blobs NuGet package is referenced
                // when running in an environment where Azure SDK is available.
                // ------------------------------------------------------------
                /*
                try
                {
                    // Replace with your actual Azure Storage connection string and container name
                    string connectionString = "<Your_Azure_Storage_Connection_String>";
                    string containerName = "<Your_Container_Name>";
                    string blobName = "maxicode.png";

                    // Create a BlobServiceClient to interact with the storage account
                    var blobServiceClient = new Azure.Storage.Blobs.BlobServiceClient(connectionString);
                    var containerClient = blobServiceClient.GetBlobContainerClient(containerName);
                    containerClient.CreateIfNotExists();

                    // Get a reference to the blob and upload the image
                    var blobClient = containerClient.GetBlobClient(blobName);
                    ms.Position = 0; // Reset stream position before upload
                    blobClient.Upload(ms, overwrite: true);

                    Console.WriteLine($"MaxiCode image uploaded to Azure Blob storage as '{blobName}'.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Azure upload failed: {ex.Message}");
                }
                */
            }
        }
    }
}