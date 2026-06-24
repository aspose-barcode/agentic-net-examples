using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.ComplexBarcode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generation of a MaxiCode barcode and saving it locally,
/// with optional Azure Blob Storage upload (commented out).
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates a MaxiCode barcode, writes it to a PNG file,
    /// and contains placeholder code for uploading to Azure Blob Storage.
    /// </summary>
    static void Main()
    {
        // --------------------------------------------------------------------
        // Prepare MaxiCode codetext (Mode 4 with a simple message)
        // --------------------------------------------------------------------
        var maxiCodeCodetext = new MaxiCodeStandardCodetext
        {
            Mode = MaxiCodeMode.Mode4,
            Message = "Hello from Aspose"
        };

        // --------------------------------------------------------------------
        // Generate the barcode image into a memory stream
        // --------------------------------------------------------------------
        using (var memoryStream = new MemoryStream())
        {
            // Create a generator for the complex barcode using the prepared codetext
            using (var generator = new ComplexBarcodeGenerator(maxiCodeCodetext))
            {
                // Save the generated barcode as PNG into the memory stream
                generator.Save(memoryStream, BarCodeImageFormat.Png);
            }

            // Reset stream position to the beginning for subsequent reads
            memoryStream.Position = 0;

            // ----------------------------------------------------------------
            // Save the barcode image locally (fallback when Azure SDK is unavailable)
            // ----------------------------------------------------------------
            const string localFilePath = "maxicode.png";

            // Create a file stream and copy the memory stream contents into it
            using (var fileStream = File.Create(localFilePath))
            {
                memoryStream.CopyTo(fileStream);
            }

            Console.WriteLine($"Barcode image saved locally to '{localFilePath}'.");

            // ----------------------------------------------------------------
            // Azure Blob Storage upload (requires Azure.Storage.Blobs package)
            // ----------------------------------------------------------------
            // Replace the placeholders below with your actual Azure Storage connection details.
            // string connectionString = "<Your_Azure_Storage_Connection_String>";
            // string containerName   = "barcode-container";
            // string blobName       = "maxicode.png";

            // try
            // {
            //     // Uncomment the following lines after adding the Azure.Storage.Blobs NuGet package.
            //     /*
            //     var blobServiceClient = new Azure.Storage.Blobs.BlobServiceClient(connectionString);
            //     var containerClient   = blobServiceClient.GetBlobContainerClient(containerName);
            //     containerClient.CreateIfNotExists();
            //     var blobClient        = containerClient.GetBlobClient(blobName);
            //     memoryStream.Position = 0; // Ensure stream is at the beginning
            //     blobClient.Upload(memoryStream, overwrite: true);
            //     Console.WriteLine($"Uploaded to Azure Blob Storage: {blobClient.Uri}");
            //     */
            // }
            // catch (Exception ex)
            // {
            //     Console.WriteLine($"Azure upload failed: {ex.Message}");
            // }
        }
    }
}