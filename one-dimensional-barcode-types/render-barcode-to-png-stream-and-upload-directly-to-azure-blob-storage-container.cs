using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating a Code128 barcode, saving it locally,
/// and (optionally) uploading it to Azure Blob Storage.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates a barcode image, writes it to a PNG file,
    /// and shows where Azure Blob upload code would be placed.
    /// </summary>
    /// <param name="args">Command‑line arguments (not used).</param>
    static void Main(string[] args)
    {
        // Sample barcode data to encode.
        string codeText = "123ABC456";

        // Choose a barcode symbology (Code128) for encoding.
        BaseEncodeType encodeType = EncodeTypes.Code128;

        // Create a BarcodeGenerator instance with the selected type and data.
        using (var generator = new BarcodeGenerator(encodeType, codeText))
        {
            // Prepare a memory stream to hold the generated PNG image.
            using (var pngStream = new MemoryStream())
            {
                // Save the barcode image into the memory stream in PNG format.
                generator.Save(pngStream, BarCodeImageFormat.Png);

                // Reset the stream position to the beginning for subsequent reads.
                pngStream.Position = 0;

                // Define the local file path where the PNG will be saved.
                string localPath = "barcode.png";

                // Write the PNG data from the memory stream to a physical file.
                using (var fileStream = new FileStream(localPath, FileMode.Create, FileAccess.Write))
                {
                    pngStream.CopyTo(fileStream);
                }

                // -----------------------------------------------------------------
                // Azure Blob Storage upload (commented out because the required
                // Azure.Storage.Blobs package is not available in this environment).
                // -----------------------------------------------------------------
                // string connectionString = "<your Azure Storage connection string>";
                // string containerName = "<your container name>";
                // string blobName = "barcode.png";
                //
                // var blobServiceClient = new Azure.Storage.Blobs.BlobServiceClient(connectionString);
                // var containerClient = blobServiceClient.GetBlobContainerClient(containerName);
                // containerClient.CreateIfNotExists();
                // var blobClient = containerClient.GetBlobClient(blobName);
                // pngStream.Position = 0; // Ensure stream is at the beginning before upload.
                // blobClient.Upload(pngStream, overwrite: true);
                // -----------------------------------------------------------------

                // Inform the user that the barcode image has been saved locally.
                Console.WriteLine($"Barcode image saved locally as '{localPath}'.");
                // Note: Azure Blob upload code remains commented out.
            }
        }
    }
}