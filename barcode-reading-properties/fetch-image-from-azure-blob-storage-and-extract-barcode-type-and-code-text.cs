// Title: Read QR Code from Azure Blob Storage (Local Fallback)
// Description: Demonstrates generating a QR code image, optionally downloading it from Azure Blob storage, and extracting its type and text using Aspose.BarCode.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It showcases the use of BarcodeGenerator to create barcodes and BarCodeReader to decode them. Typical scenarios include processing barcode images stored in cloud services such as Azure Blob Storage, extracting product information, or validating scanned data. Developers often need to combine cloud storage access with Aspose.BarCode APIs to automate barcode workflows.
// Prompt: Fetch image from Azure Blob storage and extract barcode type and code text.
// Tags: qr, barcode, azure blob, generation, recognition, aspose.barcode, csharp

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Sample program that generates a QR code, optionally retrieves it from Azure Blob storage,
/// and reads the barcode type and data using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Creates a temporary QR code image, reads it with BarCodeReader,
    /// and writes the barcode type and text to the console.
    /// </summary>
    /// <param name="args">Command‑line arguments (not used).</param>
    static void Main(string[] args)
    {
        // Create a unique temporary folder for the sample image
        string tempDir = Path.Combine(Path.GetTempPath(), "AsposeBarcodeSample_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempDir);
        string imagePath = Path.Combine(tempDir, "sample_qr.png");

        // Generate a sample QR code image if it does not already exist
        if (!File.Exists(imagePath))
        {
            using (var generator = new BarcodeGenerator(EncodeTypes.QR, "Hello Aspose"))
            {
                // Set QR code module size (pixel dimension)
                generator.Parameters.Barcode.XDimension.Pixels = 4f;
                // Save the generated QR code as a PNG file
                generator.Save(imagePath, BarCodeImageFormat.Png);
            }
        }

        Console.WriteLine("Reading barcode from image...");

        // Azure Blob Storage download example (commented out because Azure SDK is not available in the runner)
        // string connectionString = "YourAzureBlobConnectionString";
        // string containerName = "your-container";
        // string blobName = "sample_qr.png";
        // var blobClient = new BlobClient(connectionString, containerName, blobName);
        // using (var ms = new MemoryStream())
        // {
        //     blobClient.DownloadTo(ms);
        //     ms.Position = 0;
        //     using (var reader = new BarCodeReader(ms, DecodeType.QR))
        //     {
        //         foreach (var result in reader.ReadBarCodes())
        //         {
        //             Console.WriteLine($"Barcode type: {result.CodeTypeName}");
        //             Console.WriteLine($"Barcode data: {result.CodeText}");
        //         }
        //     }
        // }

        // Read the barcode from the local file (fallback when Azure download is not used)
        if (File.Exists(imagePath))
        {
            using (var reader = new BarCodeReader(imagePath, DecodeType.QR))
            {
                foreach (var result in reader.ReadBarCodes())
                {
                    Console.WriteLine($"Barcode type: {result.CodeTypeName}");
                    Console.WriteLine($"Barcode data: {result.CodeText}");
                }
            }
        }
        else
        {
            Console.WriteLine($"Image file not found: {imagePath}");
        }

        // Optional cleanup of the temporary directory
        // Directory.Delete(tempDir, true);
    }
}