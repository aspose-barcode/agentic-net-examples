// Title: Save Barcode as PNG to Azure Blob Storage Stream
// Description: Demonstrates generating a Code128 barcode and saving it as a PNG image directly to a stream, with an example of uploading to Azure Blob storage.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to use BarcodeGenerator and BarCodeImageFormat to create barcodes, write them to streams, and integrate with Azure Storage SDK for cloud persistence. Developers working with barcode creation, image output formats, and cloud storage will find this pattern useful for automating barcode distribution.
// Prompt: Use BarcodeGenerator.Save overload to write a PNG image to a CloudBlob stream for Azure storage.
// Tags: barcode, symbology, generation, png, azure blob, stream, aspose.barcode, aspose.drawing

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Example program that generates a Code128 barcode and saves it as a PNG image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates a barcode, saves it locally, and includes commented Azure Blob storage code.
    /// </summary>
    static void Main()
    {
        // Define the barcode data to encode.
        string codeText = "12345678";

        // Determine a temporary local file path for fallback storage.
        string outputPath = Path.Combine(Path.GetTempPath(), "barcode.png");

        // -----------------------------------------------------------------
        // Local file generation: create a FileStream and write the PNG image.
        // -----------------------------------------------------------------
        using (FileStream fileStream = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
        {
            // Initialize the barcode generator with Code128 symbology and the data.
            using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
            {
                // Save the generated barcode directly to the provided stream in PNG format.
                generator.Save(fileStream, BarCodeImageFormat.Png);
            }
        }

        Console.WriteLine($"Barcode saved to local file: {outputPath}");

        // -----------------------------------------------------------------
        // Azure Blob storage implementation (requires Azure.Storage.Blobs package).
        // The code is commented out to avoid external dependencies during a simple run.
        // -----------------------------------------------------------------
        // using Azure.Storage.Blobs;
        // string connectionString = "<your_connection_string>";
        // string containerName = "<your_container_name>";
        // string blobName = "barcode.png";
        // BlobContainerClient container = new BlobContainerClient(connectionString, containerName);
        // container.CreateIfNotExists();
        // BlobClient blob = container.GetBlobClient(blobName);
        // using (MemoryStream ms = new MemoryStream())
        // {
        //     using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
        //     {
        //         generator.Save(ms, BarCodeImageFormat.Png);
        //     }
        //     ms.Position = 0; // Reset stream position before upload.
        //     blob.Upload(ms, overwrite: true);
        // }
        // Console.WriteLine($"Barcode uploaded to Azure Blob: {blob.Uri}");
    }
}