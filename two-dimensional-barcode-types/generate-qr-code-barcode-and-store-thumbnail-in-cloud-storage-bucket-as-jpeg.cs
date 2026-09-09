// Title: Generate QR Code and Create JPEG Thumbnail
// Description: This example generates a QR Code barcode, saves it as a JPEG image, and creates a 150x150 pixel thumbnail.
// Category-Description: Demonstrates Aspose.BarCode barcode generation for QR symbology, image rendering, and format conversion. It showcases using BarcodeGenerator, setting QR error correction, and handling bitmap resizing with Aspose.Drawing. Typical use cases include embedding QR codes in documents, generating printable assets, and preparing images for cloud storage uploads. Developers often need to customize barcode parameters, export to common image formats, and create thumbnails for UI previews.
// Prompt: Generate QR Code barcode and store thumbnail in cloud storage bucket as JPEG.
// Tags: qr code, barcode generation, jpeg, thumbnail, aspose.barcode, image processing

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates generating a QR Code barcode, saving it as a JPEG, creating a thumbnail,
/// and (optionally) uploading both images to a cloud storage bucket.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates QR code, saves full-size and thumbnail images,
    /// and provides placeholders for cloud upload.
    /// </summary>
    static void Main()
    {
        // Prepare a temporary output directory for the generated images
        string outputDir = Path.Combine(Path.GetTempPath(), "QrCodeExample_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputDir);

        // QR code content to encode
        string qrText = "https://example.com";

        // Paths for the full-size and thumbnail JPEG images
        string fullImagePath = Path.Combine(outputDir, "qr_full.jpg");
        string thumbImagePath = Path.Combine(outputDir, "qr_thumb.jpg");

        // Generate QR code and save as JPEG
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.QR, qrText))
        {
            // Set QR error correction level (optional)
            generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelM;

            // Save the full-size QR code image
            generator.Save(fullImagePath, BarCodeImageFormat.Jpeg);

            // Create a 150x150 pixel thumbnail from the generated bitmap
            using (Bitmap fullBitmap = generator.GenerateBarCodeImage())
            {
                using (Bitmap thumbBitmap = new Bitmap(fullBitmap, new Size(150, 150)))
                {
                    thumbBitmap.Save(thumbImagePath, ImageFormat.Jpeg);
                }
            }
        }

        // Output the locations of the saved images
        Console.WriteLine($"QR code saved to: {fullImagePath}");
        Console.WriteLine($"Thumbnail saved to: {thumbImagePath}");

        // ------------------------------------------------------------
        // Upload to cloud storage bucket (e.g., Azure Blob Storage)
        // The actual SDK is not available in the snippet runner, so the
        // implementation is provided as commented code.
        // ------------------------------------------------------------
        /*
        // using Azure.Storage.Blobs;
        // string connectionString = "<your-connection-string>";
        // string containerName = "<your-container-name>";
        // BlobContainerClient container = new BlobContainerClient(connectionString, containerName);
        // container.CreateIfNotExists();

        // // Upload full-size image
        // using (FileStream fs = new FileStream(fullImagePath, FileMode.Open, FileAccess.Read))
        // {
        //     BlobClient blob = container.GetBlobClient("qr_full.jpg");
        //     blob.Upload(fs, overwrite: true);
        // }

        // // Upload thumbnail image
        // using (FileStream fs = new FileStream(thumbImagePath, FileMode.Open, FileAccess.Read))
        // {
        //     BlobClient blob = container.GetBlobClient("qr_thumb.jpg");
        //     blob.Upload(fs, overwrite: true);
        // }
        */

        // End of program
    }
}