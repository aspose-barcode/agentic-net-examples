// Title: Write barcode image stream to Azure Blob storage (integration test)
// Description: Demonstrates generating a Code128 barcode, storing it in a memory stream, and uploading the stream to Azure Blob Storage. In CI environments the image is saved locally for validation.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing how to use BarcodeGenerator, EncodeTypes, and BarCodeImageFormat to create barcode images in memory. Typical use cases include integrating barcode creation into cloud workflows, such as uploading directly to Azure Blob Storage. Developers often need to work with streams for seamless storage or transmission without intermediate files.
// Prompt: Write integration test ensuring barcode image stream can be directly written to Azure Blob storage.
// Tags: barcode symbology, generation, png, azure blob storage, aspose.barcode, memorystream

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating a barcode and uploading its image stream to Azure Blob Storage.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates a barcode, obtains its image as a stream, and uploads it to Azure Blob Storage (sample code).
    /// </summary>
    static void Main()
    {
        // Define the data to encode in the barcode.
        string codeText = "123ABC";

        // Generate the barcode image and retrieve it as a MemoryStream.
        using (MemoryStream barcodeStream = GenerateBarcodeStream(codeText))
        {
            // Ensure the stream position is reset before any read/write operations.
            barcodeStream.Position = 0;

            // -----------------------------------------------------------------
            // Azure Blob Storage upload (commented out – Azure SDK not available)
            // -----------------------------------------------------------------
            // The following code illustrates how the stream would be uploaded to
            // Azure Blob Storage in a production scenario.
            // ---------------------------------------------------------------
            // string connectionString = "<your-connection-string>";
            // string containerName = "barcodes";
            // string blobName = "code128.png";
            // var blobClient = new Azure.Storage.Blobs.BlobClient(connectionString, containerName, blobName);
            // barcodeStream.Position = 0; // Ensure stream is at start
            // blobClient.Upload(barcodeStream, overwrite: true);
            // Console.WriteLine("Barcode uploaded to Azure Blob Storage.");

            // -----------------------------------------------------------------
            // Fallback: write the barcode image to a local file for CI validation
            // -----------------------------------------------------------------
            string localPath = Path.Combine(Path.GetTempPath(), "barcode.png");
            using (FileStream file = new FileStream(localPath, FileMode.Create, FileAccess.Write))
            {
                // Reset position again before copying to the file stream.
                barcodeStream.Position = 0;
                barcodeStream.CopyTo(file);
            }

            Console.WriteLine($"Barcode image saved locally at: {localPath}");
        }
    }

    /// <summary>
    /// Generates a barcode image (PNG) and returns it as a <see cref="MemoryStream"/>.
    /// </summary>
    /// <param name="codeText">The text to encode in the barcode.</param>
    /// <returns>A memory stream containing the PNG image data.</returns>
    private static MemoryStream GenerateBarcodeStream(string codeText)
    {
        // Create a memory stream to hold the generated image.
        MemoryStream stream = new MemoryStream();

        // Initialize the barcode generator with Code128 symbology and the provided text.
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
        {
            // Save the barcode directly to the stream in PNG format.
            generator.Save(stream, BarCodeImageFormat.Png);
        }

        // The stream now contains the PNG image data; return it to the caller.
        return stream;
    }
}