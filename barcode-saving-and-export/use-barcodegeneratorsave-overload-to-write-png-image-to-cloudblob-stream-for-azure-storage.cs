// Title: Save Barcode as PNG to Azure Blob Stream using Aspose.BarCode
// Description: Demonstrates generating a Code128 barcode with Aspose.BarCode, saving it as a PNG image to a stream, and showing how the stream could be uploaded to Azure Blob Storage.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and export category. It illustrates the use of BarcodeGenerator, BarCodeImageFormat, and stream handling to create barcode images in memory. Typical scenarios include saving barcodes to cloud storage services such as Azure Blob Storage, where developers need to work with streams rather than file paths. The sample also shows a fallback to local file storage, covering common use cases for both cloud and on‑premises deployments.
// Prompt: Use BarcodeGenerator.Save overload to write a PNG image to a CloudBlob stream for Azure storage.
// Tags: barcode, code128, png, azure blob, stream, aspose.barcode, save overload

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating a Code128 barcode, saving it as PNG to a stream,
/// and uploading it to Azure Blob Storage (sample code).
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates the barcode, writes it to a memory stream,
    /// and saves the image locally. The Azure upload code is provided as comments.
    /// </summary>
    static void Main()
    {
        // Define the data to encode in the barcode.
        string codeText = "Sample123";

        // Create a BarcodeGenerator for Code128 symbology.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
        {
            // Prepare a memory stream to hold the PNG image.
            using (var memoryStream = new MemoryStream())
            {
                // Save the generated barcode to the memory stream in PNG format.
                generator.Save(memoryStream, BarCodeImageFormat.Png);
                // Reset the stream position to the beginning for subsequent reads.
                memoryStream.Position = 0;

                // ----- Azure Blob Storage upload (commented out - SDK not available in runner) -----
                // Example using Azure.Storage.Blobs (requires Azure.Storage.Blobs NuGet package)
                // var blobServiceClient = new BlobServiceClient("<connection_string>");
                // var containerClient = blobServiceClient.GetBlobContainerClient("<container_name>");
                // var blobClient = containerClient.GetBlobClient("barcode.png");
                // using (var uploadStream = memoryStream)
                // {
                //     blobClient.Upload(uploadStream, overwrite: true);
                // }
                // ------------------------------------------------------------------------------

                // Fallback: write the PNG image to a local file.
                string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "barcode.png");
                using (var fileStream = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
                {
                    memoryStream.CopyTo(fileStream);
                }

                Console.WriteLine($"Barcode saved to {outputPath}");
            }
        }
    }
}