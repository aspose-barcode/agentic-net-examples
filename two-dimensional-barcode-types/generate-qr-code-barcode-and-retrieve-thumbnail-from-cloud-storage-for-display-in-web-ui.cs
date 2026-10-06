// Title: Generate QR Code and Create Thumbnail for Web UI
// Description: Demonstrates generating a QR Code barcode, saving it as PNG, creating a 100x100 thumbnail, and preparing the images for display in a web UI.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and image processing category. It showcases the use of BarcodeGenerator for QR Code creation, the Parameters API for customizing size and error correction, and Aspose.Drawing for resizing images. Developers working on web applications often need to generate barcodes on the fly and provide thumbnail previews, making this pattern a common requirement.
// Prompt: Generate QR Code barcode and retrieve thumbnail from cloud storage for display in web UI.
// Tags: qr code,barcode generation,thumbnail,aspose.barcode,aspose.drawing,image processing,web ui

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that generates a QR Code, creates a thumbnail, and demonstrates how the images could be used in a web UI.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates a QR Code, creates a thumbnail, and writes the file locations to the console.
    /// </summary>
    static void Main()
    {
        // -----------------------------------------------------------------
        // Prepare a temporary output directory for the generated images.
        // -----------------------------------------------------------------
        string outputDir = Path.Combine(Path.GetTempPath(), "AsposeBarcodeDemo");
        Directory.CreateDirectory(outputDir);

        // Define file paths for the full-size QR code and its thumbnail.
        string qrPath = Path.Combine(outputDir, "qr.png");
        string thumbPath = Path.Combine(outputDir, "qr_thumb.png");

        // -----------------------------------------------------------------
        // Generate QR Code barcode using Aspose.BarCode.
        // -----------------------------------------------------------------
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.QR, "https://example.com"))
        {
            // Set the module (pixel) size of the QR code.
            generator.Parameters.Barcode.XDimension.Pixels = 4f;

            // Set the error correction level (Level M provides a good balance of data capacity and resilience).
            generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelM;

            // Save the generated QR code as a PNG image.
            generator.Save(qrPath, BarCodeImageFormat.Png);
        }

        // -----------------------------------------------------------------
        // Load the generated QR code image and create a 100x100 thumbnail.
        // -----------------------------------------------------------------
        using (Bitmap original = new Bitmap(qrPath))
        {
            // Desired thumbnail dimensions.
            int thumbWidth = 100;
            int thumbHeight = 100;

            // Create a resized bitmap based on the original image.
            using (Bitmap thumbnail = new Bitmap(original, new Size(thumbWidth, thumbHeight)))
            {
                // Save the thumbnail as a PNG image.
                thumbnail.Save(thumbPath, ImageFormat.Png);
            }
        }

        // -----------------------------------------------------------------
        // Placeholder for retrieving the thumbnail from cloud storage.
        // Replace the commented code with actual cloud SDK calls as needed.
        // -----------------------------------------------------------------
        // using (var blobClient = new BlobClient(connectionString, containerName, "qr_thumb.png"))
        // {
        //     blobClient.DownloadTo(thumbPath);
        // }

        // Output the locations of the generated files for verification.
        Console.WriteLine($"QR code saved to: {qrPath}");
        Console.WriteLine($"Thumbnail saved to: {thumbPath}");
    }
}