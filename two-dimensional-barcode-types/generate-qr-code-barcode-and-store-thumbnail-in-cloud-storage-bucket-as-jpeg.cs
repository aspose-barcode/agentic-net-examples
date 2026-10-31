// Title: Generate QR Code and Create JPEG Thumbnail in Simulated Cloud Bucket
// Description: This example generates a QR Code barcode, saves it as a JPEG image, and creates a smaller thumbnail version, storing both in a temporary folder that simulates a cloud storage bucket.
// Category-Description: Demonstrates Aspose.BarCode barcode generation (BarcodeGenerator, EncodeTypes) combined with Aspose.Drawing image manipulation (Bitmap, Graphics) to produce QR Code images and thumbnails. Typical for scenarios where barcodes need to be stored or displayed in cloud storage, requiring both full‑size and preview images. Developers often use these APIs to automate barcode creation, resize images, and integrate with cloud SDKs for upload.
// Prompt: Generate QR Code barcode and store thumbnail in cloud storage bucket as JPEG.
// Tags: qr code, barcode generation, thumbnail, jpeg, aspose.barcode, aspose.drawing

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates generating a QR Code barcode, saving it as JPEG, and creating a thumbnail image stored in a simulated cloud bucket.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that creates the QR Code, generates a thumbnail, and writes the file locations to the console.
    /// </summary>
    static void Main()
    {
        // Define a temporary folder to simulate a cloud storage bucket.
        string bucketPath = Path.Combine(Path.GetTempPath(), "cloud_bucket");
        Directory.CreateDirectory(bucketPath);

        // Paths for the full‑size QR code image and its thumbnail.
        string qrPath = Path.Combine(bucketPath, "qr.jpg");
        string thumbPath = Path.Combine(bucketPath, "qr_thumb.jpg");

        // Generate QR Code barcode and save it as a JPEG file.
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "https://example.com"))
        {
            // Optional: set QR error correction level to improve readability.
            generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelM;

            // Save the full‑size QR code image.
            generator.Save(qrPath, BarCodeImageFormat.Jpeg);
        }

        // Create a thumbnail from the generated QR code image.
        using (var original = new Bitmap(qrPath))
        {
            // Define thumbnail width and calculate height to preserve aspect ratio.
            int thumbWidth = 150;
            int thumbHeight = (int)(original.Height * (thumbWidth / (float)original.Width));

            using (var thumbnail = new Bitmap(thumbWidth, thumbHeight))
            {
                // Draw the scaled image onto the thumbnail bitmap.
                using (var graphics = Graphics.FromImage(thumbnail))
                {
                    graphics.DrawImage(original, 0, 0, thumbWidth, thumbHeight);
                }

                // Save the thumbnail as a JPEG file.
                thumbnail.Save(thumbPath, ImageFormat.Jpeg);
            }
        }

        // Output the locations of the saved images.
        Console.WriteLine($"QR code saved to: {qrPath}");
        Console.WriteLine($"Thumbnail saved to: {thumbPath}");

        // In a real implementation, the thumbnail would be uploaded to a cloud storage bucket
        // using the appropriate SDK (e.g., Azure Blob Storage, AWS S3, Google Cloud Storage).
        // The upload code is omitted here because the required SDKs are not available in the snippet runner.
    }
}