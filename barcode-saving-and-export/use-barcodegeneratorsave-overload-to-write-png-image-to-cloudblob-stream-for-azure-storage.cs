using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating a Code128 barcode, saving it to a memory stream,
/// and writing the image to a local file. Includes commented example code
/// for uploading the stream to Azure Blob Storage.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates a barcode, saves it to a
    /// memory stream, writes the image to a file, and optionally shows how
    /// to upload the stream to Azure Blob Storage.
    /// </summary>
    /// <param name="args">Command‑line arguments (not used).</param>
    static void Main(string[] args)
    {
        // Initialize a barcode generator for Code128 with the sample text "1234567890".
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            // Set the barcode's foreground (bar) color to black.
            generator.Parameters.Barcode.BarColor = Color.Black;

            // Set the background color of the image to white.
            generator.Parameters.BackColor = Color.White;

            // Create a memory stream to hold the generated PNG image.
            using (var memoryStream = new MemoryStream())
            {
                // Save the barcode image into the memory stream in PNG format.
                generator.Save(memoryStream, BarCodeImageFormat.Png);

                // Reset the stream position to the beginning for subsequent reads.
                memoryStream.Position = 0;

                // Write the image bytes from the memory stream to a local file named "barcode.png".
                File.WriteAllBytes("barcode.png", memoryStream.ToArray());

                // -----------------------------------------------------------------
                // Example code for uploading the stream to Azure Blob Storage.
                // This requires the Azure.Storage.Blobs NuGet package, which is
                // not available in the snippet runner environment.
                // -----------------------------------------------------------------
                /*
                // using Azure.Storage.Blobs;
                // string connectionString = "<your-azure-connection-string>";
                // var blobServiceClient = new BlobServiceClient(connectionString);
                // var containerClient = blobServiceClient.GetBlobContainerClient("mycontainer");
                // var blobClient = containerClient.GetBlobClient("barcode.png");
                // memoryStream.Position = 0;
                // await blobClient.UploadAsync(memoryStream, overwrite: true);
                */
            }
        }

        // Inform the user that the barcode has been generated and saved.
        Console.WriteLine("Barcode generated and saved to 'barcode.png'.");
    }
}