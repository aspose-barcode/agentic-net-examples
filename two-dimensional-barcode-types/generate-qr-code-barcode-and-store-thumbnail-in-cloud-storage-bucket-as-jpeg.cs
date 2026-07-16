// Title: Generate QR Code and create JPEG thumbnail
// Description: Demonstrates generating a QR Code barcode, rendering it as a JPEG image, and producing a scaled thumbnail. The thumbnail can then be uploaded to cloud storage.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and image manipulation category. It showcases the BarcodeGenerator class for creating QR codes, Aspose.Drawing for image handling, and typical steps developers follow to produce thumbnails before storing them in cloud buckets such as AWS S3, Azure Blob, or Google Cloud Storage.
// Prompt: Generate QR Code barcode and store thumbnail in cloud storage bucket as JPEG.
// Tags: qr code, barcode generation, thumbnail, jpeg, aspose.barcode, image processing

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Program that generates a QR Code, creates a JPEG thumbnail, and saves it locally.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates QR code, creates thumbnail, and writes output path.
    /// </summary>
    static void Main()
    {
        // Define the QR code content
        const string qrContent = "https://example.com";

        // Initialize the barcode generator for QR code
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, qrContent))
        {
            // Set QR error correction level (optional)
            generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelM;

            // Stream to hold the generated QR code image
            using (var memoryStream = new MemoryStream())
            {
                // Save full-size QR code image to the stream in JPEG format
                generator.Save(memoryStream, BarCodeImageFormat.Jpeg);
                memoryStream.Position = 0; // Reset stream position for reading

                // Load the image from the memory stream
                using (var originalImage = Image.FromStream(memoryStream))
                {
                    // Define thumbnail dimensions
                    const int thumbWidth = 150;
                    const int thumbHeight = 150;

                    // Create a bitmap that will hold the thumbnail
                    using (var thumbnail = new Bitmap(thumbWidth, thumbHeight))
                    {
                        // Draw the original image onto the thumbnail bitmap, scaling it
                        using (var graphics = Graphics.FromImage(thumbnail))
                        {
                            graphics.DrawImage(originalImage, 0, 0, thumbWidth, thumbHeight);
                        }

                        // Save the thumbnail locally as JPEG
                        const string thumbnailPath = "qr_thumbnail.jpg";
                        thumbnail.Save(thumbnailPath, ImageFormat.Jpeg);

                        Console.WriteLine($"QR code thumbnail saved to: {Path.GetFullPath(thumbnailPath)}");
                    }
                }
            }
        }

        // NOTE:
        // In a real scenario, you would upload 'qr_thumbnail.jpg' to a cloud storage bucket
        // using the appropriate SDK (e.g., AWS S3, Azure Blob Storage, Google Cloud Storage).
        // The upload code is omitted here because the required cloud SDK packages are not
        // available in the snippet runner environment.
    }
}