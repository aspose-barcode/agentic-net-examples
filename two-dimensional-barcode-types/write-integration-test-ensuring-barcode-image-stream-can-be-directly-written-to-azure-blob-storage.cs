using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates generating a Code128 barcode, saving it to a memory stream,
/// and then persisting the image either to Azure Blob Storage (commented example)
/// or to a local temporary file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates a barcode, writes it to a stream, and saves the image locally.
    /// </summary>
    static void Main()
    {
        // Define barcode type and content.
        BaseEncodeType encodeType = EncodeTypes.Code128;
        string codeText = "Test123";

        // Create a memory stream to hold the generated barcode image.
        using (var memoryStream = new MemoryStream())
        {
            // Initialize the barcode generator with the specified type and text.
            using (var generator = new BarcodeGenerator(encodeType, codeText))
            {
                // Set optional parameters, e.g., image resolution.
                generator.Parameters.Resolution = 300f;

                // Save the barcode directly into the memory stream in PNG format.
                generator.Save(memoryStream, BarCodeImageFormat.Png);
            }

            // Reset the stream position to the beginning before any read operations.
            memoryStream.Position = 0;

            // -----------------------------------------------------------------
            // Azure Blob Storage upload (example – requires Azure.Storage.Blobs SDK)
            // -----------------------------------------------------------------
            /*
            string connectionString = "<Your Azure Blob Storage connection string>";
            string containerName = "barcodes";
            string blobName = "code128.png";

            var blobServiceClient = new Azure.Storage.Blobs.BlobServiceClient(connectionString);
            var containerClient = blobServiceClient.GetBlobContainerClient(containerName);
            containerClient.CreateIfNotExists();

            var blobClient = containerClient.GetBlobClient(blobName);
            memoryStream.Position = 0; // Ensure stream is at the beginning
            blobClient.Upload(memoryStream, overwrite: true);
            Console.WriteLine($"Barcode uploaded to Azure Blob: {blobClient.Uri}");
            */

            // -----------------------------------------------------------------
            // Fallback: write the barcode image to a local temporary file.
            // -----------------------------------------------------------------
            string localPath = Path.Combine(Path.GetTempPath(), "code128.png");

            // Create a file stream for writing the image to disk.
            using (var fileStream = new FileStream(localPath, FileMode.Create, FileAccess.Write))
            {
                // Ensure the memory stream is positioned at the start before copying.
                memoryStream.Position = 0;
                memoryStream.CopyTo(fileStream);
            }

            // Inform the user where the image was saved.
            Console.WriteLine($"Barcode image saved locally at: {localPath}");
        }
    }
}