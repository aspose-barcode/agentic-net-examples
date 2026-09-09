// Title: Extract barcode from Azure Blob image using Aspose.BarCode
// Description: Demonstrates downloading an image from Azure Blob storage (code commented) and using Aspose.BarCode to detect and read any barcode present in the image.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It showcases the use of BarcodeGenerator for creating barcodes and BarCodeReader for decoding them. Typical scenarios include processing images stored in cloud storage, extracting product codes, or validating QR codes in automated workflows. Developers often need to combine Azure Blob SDK with Aspose.BarCode APIs to retrieve images and perform fast, reliable barcode recognition.
// Prompt: Fetch image from Azure Blob storage and extract barcode type and code text.
// Tags: barcode, azure blob, extraction, recognition, aspose.barcode, qrcode, decode, .net

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Sample program that generates a QR code if missing, optionally downloads an image from Azure Blob storage,
/// and reads all supported barcodes using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates a sample barcode image, optionally replaces it with a blob download,
    /// and prints detected barcode type and text to the console.
    /// </summary>
    static void Main()
    {
        // Define a temporary file path for the barcode image
        string imagePath = Path.Combine(Path.GetTempPath(), "sample_barcode.png");

        // If the image does not exist locally, generate a sample QR code image
        if (!File.Exists(imagePath))
        {
            using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.QR, "SampleBarcodeText"))
            {
                // Set the module size (pixel dimension) for better readability
                generator.Parameters.Barcode.XDimension.Pixels = 4;
                // Save the generated QR code as a PNG file
                generator.Save(imagePath, BarCodeImageFormat.Png);
            }
        }

        // -----------------------------------------------------------------
        // Azure Blob Storage download (commented out – SDK not available here)
        // -----------------------------------------------------------------
        // string connectionString = "<your_connection_string>";
        // string containerName = "<your_container>";
        // string blobName = "<your_blob_name>";
        // using (var blobClient = new BlobClient(connectionString, containerName, blobName))
        // {
        //     using (MemoryStream ms = new MemoryStream())
        //     {
        //         blobClient.DownloadTo(ms);
        //         ms.Position = 0;
        //         // Save to local file for processing
        //         using (FileStream fs = new FileStream(imagePath, FileMode.Create, FileAccess.Write))
        //         {
        //             ms.CopyTo(fs);
        //         }
        //     }
        // }

        // Ensure the image file exists before attempting to read it
        if (!File.Exists(imagePath))
        {
            Console.WriteLine($"Image file not found: {imagePath}");
            return;
        }

        // Use Aspose.BarCode to read all supported barcode types from the image
        using (BarCodeReader reader = new BarCodeReader(imagePath, DecodeType.AllSupportedTypes))
        {
            bool anyFound = false;
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                anyFound = true;
                Console.WriteLine($"Barcode type: {result.CodeTypeName}");
                Console.WriteLine($"Barcode data: {result.CodeText}");
            }

            if (!anyFound)
            {
                Console.WriteLine("No barcodes detected in the image.");
            }
        }
    }
}