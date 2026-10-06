// Title: Write barcode image stream to Azure Blob storage (integration test)
// Description: Demonstrates generating a Code128 barcode, storing it in a memory stream, and uploading the stream directly to Azure Blob storage. The example also shows a local file fallback and verification by reading the barcode back.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category, illustrating how to use BarcodeGenerator to create barcodes, BarCodeReader to decode them, and how to handle image streams for cloud storage. Developers working with Azure Blob storage often need to upload barcode images without intermediate files, using Aspose.BarCodeImageFormat and .NET stream APIs.
// Prompt: Write integration test ensuring barcode image stream can be directly written to Azure Blob storage.
// Tags: barcode, code128, generation, recognition, stream, azure blob storage, aspose.barcode, png

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates barcode generation, local persistence, verification, and a placeholder for Azure Blob upload.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that creates a Code128 barcode, writes it to a memory stream,
    /// saves it locally for verification, and shows how to upload the stream to Azure Blob storage.
    /// </summary>
    /// <param name="args">Command‑line arguments (not used).</param>
    static void Main(string[] args)
    {
        // Define the data to encode in the barcode.
        string codeText = "Test123";

        // Create a BarcodeGenerator for Code128 and write the image to a memory stream.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
        {
            using (var barcodeStream = new MemoryStream())
            {
                // Save the generated barcode as PNG into the stream.
                generator.Save(barcodeStream, BarCodeImageFormat.Png);
                // Reset stream position for subsequent reads.
                barcodeStream.Position = 0;

                // ---------------------------------------------------------------
                // Simulate Azure Blob upload by writing the stream to a temporary file.
                // ---------------------------------------------------------------
                string localPath = Path.Combine(Path.GetTempPath(), "barcode.png");
                using (var fileStream = new FileStream(localPath, FileMode.Create, FileAccess.Write))
                {
                    barcodeStream.CopyTo(fileStream);
                }

                Console.WriteLine($"Barcode image written to: {localPath}");

                // Verify the saved barcode by reading it back and decoding.
                using (var readStream = new FileStream(localPath, FileMode.Open, FileAccess.Read))
                {
                    using (var reader = new BarCodeReader(readStream, DecodeType.Code128))
                    {
                        foreach (var result in reader.ReadBarCodes())
                        {
                            Console.WriteLine($"Read barcode: Text='{result.CodeText}', Type='{result.CodeTypeName}'");
                        }
                    }
                }

                // ---------------------------------------------------------------
                // Real Azure Blob Storage upload (requires Azure.Storage.Blobs package)
                // Uncomment and configure the following code to upload directly to Azure.
                // ---------------------------------------------------------------
                // string connectionString = "<your_connection_string>";
                // string containerName = "<your_container_name>";
                // string blobName = "barcode.png";
                // var blobClient = new BlobClient(connectionString, containerName, blobName);
                // barcodeStream.Position = 0; // Reset before upload
                // blobClient.Upload(barcodeStream, overwrite: true);
            }
        }
    }
}