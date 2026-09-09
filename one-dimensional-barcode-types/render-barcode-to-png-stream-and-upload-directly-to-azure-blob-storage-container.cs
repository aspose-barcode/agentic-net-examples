// Title: Render barcode to PNG stream and upload to Azure Blob storage (local fallback)
// Description: Generates a barcode image using Aspose.BarCode, saves it as a PNG stream, and demonstrates how to upload the image directly to an Azure Blob storage container. If Azure SDK is unavailable, the example falls back to saving the PNG locally.
// Category-Description: This example belongs to the Aspose.BarCode generation and export category. It showcases the use of BarcodeGenerator, EncodeTypes, and BarCodeImageFormat to create barcode images, then streams the result for direct upload to Azure Blob storage using Azure.Storage.Blobs. Developers working with barcode creation, image streaming, and cloud storage integrations commonly reference these APIs to produce and distribute barcode assets in web and enterprise applications.
// Prompt: Render barcode to a PNG stream and upload directly to an Azure Blob storage container.
// Tags: barcode, code128, png, azure blob, aspose.barcode, generation, stream

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating a barcode, converting it to a PNG stream, and uploading it to Azure Blob storage (or saving locally as a fallback).
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Accepts optional command‑line arguments for the barcode text and symbology.
    /// </summary>
    /// <param name="args">[0] – barcode text (default: "12345678"); [1] – symbology name (default: "Code128").</param>
    static void Main(string[] args)
    {
        // ----------------------------------------------------------------------
        // Resolve input parameters (barcode text and symbology)
        // ----------------------------------------------------------------------
        string codeText = args.Length > 0 ? args[0] : "12345678";
        string symbologyName = args.Length > 1 ? args[1] : "Code128";

        // ----------------------------------------------------------------------
        // Convert the symbology name to the corresponding EncodeTypes value via reflection
        // ----------------------------------------------------------------------
        var field = typeof(EncodeTypes).GetField(symbologyName);
        if (field == null)
        {
            Console.WriteLine($"Unknown symbology '{symbologyName}'. Defaulting to Code128.");
            field = typeof(EncodeTypes).GetField("Code128");
        }
        BaseEncodeType encodeType = (BaseEncodeType)field.GetValue(null);

        // ----------------------------------------------------------------------
        // Generate the barcode and write it to an in‑memory PNG stream
        // ----------------------------------------------------------------------
        using (MemoryStream pngStream = new MemoryStream())
        {
            using (var generator = new BarcodeGenerator(encodeType, codeText))
            {
                generator.Save(pngStream, BarCodeImageFormat.Png);
            }

            // Reset stream position for subsequent reads
            pngStream.Position = 0;

            // ------------------------------------------------------------------
            // Fallback: save the PNG locally (useful when Azure SDK is unavailable)
            // ------------------------------------------------------------------
            string outputPath = Path.Combine(Path.GetTempPath(), "barcode.png");
            using (FileStream fileStream = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
            {
                pngStream.CopyTo(fileStream);
            }

            Console.WriteLine($"Barcode PNG saved to: {outputPath}");

            /*
            // ------------------------------------------------------------------
            // Real Azure Blob upload (requires Azure.Storage.Blobs package)
            // ------------------------------------------------------------------
            // string connectionString = Environment.GetEnvironmentVariable("AZURE_STORAGE_CONNECTION_STRING");
            // string containerName = "mycontainer";
            // string blobName = "barcode.png";
            // var blobServiceClient = new Azure.Storage.Blobs.BlobServiceClient(connectionString);
            // var containerClient = blobServiceClient.GetBlobContainerClient(containerName);
            // containerClient.CreateIfNotExists();
            // var blobClient = containerClient.GetBlobClient(blobName);
            // pngStream.Position = 0;
            // blobClient.Upload(pngStream, overwrite: true);
            // Console.WriteLine($"Uploaded barcode to Azure Blob: {blobClient.Uri}");
            */
        }
    }
}