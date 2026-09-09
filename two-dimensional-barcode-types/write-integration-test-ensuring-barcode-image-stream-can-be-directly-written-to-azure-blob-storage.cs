// Title: Write barcode image stream directly to Azure Blob storage (simulated)
// Description: Demonstrates generating a barcode with Aspose.BarCode, storing it in a memory stream, and uploading the stream to Azure Blob storage. In this example the upload is simulated by writing to a local file.
// Category-Description: This example belongs to the Aspose.BarCode generation and integration category. It showcases the use of BarcodeGenerator, EncodeTypes, and BarCodeImageFormat to create barcode images, then streams the result for direct upload to cloud storage such as Azure Blob. Developers often need to generate barcodes on the fly and store them without intermediate files, making memory streams and direct uploads essential for integration tests and production pipelines.
// Prompt: Write integration test ensuring barcode image stream can be directly written to Azure Blob storage.
// Tags: barcode, code128, generation, png, memorystream, azure blob storage, aspose.barcode, integration test

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Example program that generates a Code128 barcode, writes it to a memory stream,
/// and demonstrates how the stream could be uploaded to Azure Blob storage.
/// The actual upload is simulated by saving the stream to a temporary local file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates the barcode, resets the stream,
    /// and copies the image data to a destination (simulated Azure Blob storage).
    /// </summary>
    static void Main()
    {
        // Create a memory stream to hold the generated barcode image.
        using (MemoryStream barcodeStream = new MemoryStream())
        {
            // Initialize the barcode generator with Code128 symbology and the desired text.
            using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128, "Test123"))
            {
                // Save the barcode image directly into the memory stream in PNG format.
                generator.Save(barcodeStream, BarCodeImageFormat.Png);
            }

            // Reset the stream position to the beginning so it can be read from the start.
            barcodeStream.Position = 0;

            // -----------------------------------------------------------------
            // Simulate uploading the stream to Azure Blob Storage.
            // In a real scenario you would use Azure.Storage.Blobs:
            //   var blobClient = new BlobClient(connectionString, containerName, blobName);
            //   blobClient.Upload(barcodeStream, overwrite: true);
            // Since the Azure SDK is not referenced, write the stream to a local file instead.
            // -----------------------------------------------------------------

            // Determine a temporary file path for the simulated upload.
            string localPath = Path.Combine(Path.GetTempPath(), "uploaded_barcode.png");

            // Write the contents of the memory stream to the temporary file.
            using (FileStream file = new FileStream(localPath, FileMode.Create, FileAccess.Write))
            {
                barcodeStream.CopyTo(file);
            }

            // Inform the user where the simulated upload file is located.
            Console.WriteLine($"Barcode image written to stream and saved locally at: {localPath}");
        }
    }
}