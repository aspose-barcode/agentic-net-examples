// Title: Save Barcode as PNG to Azure Blob Storage Stream
// Description: Demonstrates generating a Code128 barcode with Aspose.BarCode and saving it as a PNG image directly to a stream, which can be uploaded to Azure Blob Storage.
// Category-Description: This example belongs to the Aspose.BarCode generation and output category, illustrating how to use BarcodeGenerator together with BarCodeImageFormat to create barcode images in memory. Developers often need to store generated barcodes in cloud services such as Azure Blob Storage, requiring stream-based Save overloads. The snippet shows typical usage of the generator, file fallback, and a commented Azure upload pattern, useful for web and cloud applications.
// Prompt: Use BarcodeGenerator.Save overload to write a PNG image to a CloudBlob stream for Azure storage.
// Tags: barcode, code128, png, azure, blob, storage, aspose.barcode, generation, stream

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that generates a Code128 barcode and saves it as a PNG image.
/// The image is first written to a local temporary file (fallback) and the code
/// includes a commented pattern for uploading the image to Azure Blob Storage.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates the barcode, saves it locally,
    /// and provides guidance for uploading to Azure Blob Storage.
    /// </summary>
    static void Main()
    {
        // Define the barcode data to encode.
        string codeText = "12345678";

        // Determine a temporary file path for a local fallback save.
        string localPath = Path.Combine(Path.GetTempPath(), "barcode.png");

        // Initialize the barcode generator with Code128 symbology.
        BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128, codeText);

        // -----------------------------------------------------------------
        // Save the generated barcode to a local file stream.
        // This is useful when Azure SDK is not available or for debugging.
        // -----------------------------------------------------------------
        using (FileStream fileStream = new FileStream(localPath, FileMode.Create, FileAccess.Write))
        {
            // Use the Save overload that accepts a stream and PNG format.
            generator.Save(fileStream, BarCodeImageFormat.Png);
        }

        Console.WriteLine($"Barcode saved to local file: {localPath}");

        // -----------------------------------------------------------------
        // Azure Blob Storage upload example (requires Azure.Storage.Blobs package)
        // The code is commented out to avoid mandatory Azure dependencies.
        // Uncomment and provide valid connection details to use.
        // -----------------------------------------------------------------
        // string connectionString = "<your_connection_string>";
        // string containerName = "<your_container_name>";
        // string blobName = "barcode.png";
        // var blobClient = new BlobClient(connectionString, containerName, blobName);
        // using (MemoryStream ms = new MemoryStream())
        // {
        //     // Save the barcode image to a memory stream.
        //     generator.Save(ms, BarCodeImageFormat.Png);
        //     ms.Position = 0; // Reset stream position before upload.
        //     blobClient.Upload(ms);
        // }
        // Console.WriteLine("Barcode uploaded to Azure Blob Storage.");
    }
}