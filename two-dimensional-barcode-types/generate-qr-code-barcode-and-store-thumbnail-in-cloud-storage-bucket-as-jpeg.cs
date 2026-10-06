// Title: Generate QR Code and Create JPEG Thumbnail
// Description: This example generates a QR Code barcode, saves it as a JPEG image, and creates a 150x150 thumbnail version.
// Category-Description: Demonstrates Aspose.BarCode barcode generation and image processing using Aspose.Drawing. It covers creating QR Code symbology, configuring barcode parameters, saving to JPEG, and resizing images to produce thumbnails. Developers working with barcode creation, image manipulation, or preparing assets for cloud storage will find this pattern useful.
// Prompt: Generate QR Code barcode and store thumbnail in cloud storage bucket as JPEG.
// Tags: qr code, barcode generation, jpeg, thumbnail, aspose.barcode, aspose.drawing, image processing

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that generates a QR Code barcode, saves it as a JPEG,
/// creates a thumbnail image, and outlines where to upload the thumbnail to cloud storage.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        // --------------------------------------------------------------------
        // Set up a temporary output directory for the generated images.
        // --------------------------------------------------------------------
        string outputDir = Path.Combine(Path.GetTempPath(), "AsposeBarcodeDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputDir);

        // Define file paths for the full-size QR image and its thumbnail.
        string fullImagePath = Path.Combine(outputDir, "qr_full.jpg");
        string thumbImagePath = Path.Combine(outputDir, "qr_thumbnail.jpg");

        // Text to encode in the QR Code.
        string qrText = "https://example.com";

        // --------------------------------------------------------------------
        // Generate the QR Code barcode and save it as a JPEG image.
        // --------------------------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, qrText))
        {
            // Optional: set the size of each QR module (pixel dimension).
            generator.Parameters.Barcode.XDimension.Pixels = 4f;

            // Optional: set the error correction level (LevelM provides a good balance).
            generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelM;

            // Save the generated barcode to the specified JPEG file.
            generator.Save(fullImagePath, BarCodeImageFormat.Jpeg);
        }

        // --------------------------------------------------------------------
        // Create a 150x150 pixel thumbnail from the generated QR image.
        // --------------------------------------------------------------------
        using (var original = new Bitmap(fullImagePath))
        {
            int thumbWidth = 150;
            int thumbHeight = 150;

            using (var thumbnail = new Bitmap(thumbWidth, thumbHeight))
            {
                using (var graphics = Graphics.FromImage(thumbnail))
                {
                    // Draw the original image scaled down to the thumbnail dimensions.
                    graphics.DrawImage(original, 0, 0, thumbWidth, thumbHeight);
                }

                // Save the thumbnail as a JPEG file.
                thumbnail.Save(thumbImagePath, ImageFormat.Jpeg);
            }
        }

        // Output the locations of the generated files.
        Console.WriteLine($"QR code saved to: {fullImagePath}");
        Console.WriteLine($"Thumbnail saved to: {thumbImagePath}");

        // NOTE: In a real scenario, upload 'thumbImagePath' to a cloud storage bucket using the appropriate SDK.
        // The cloud SDK is not available in this runner, so the upload step is omitted.
    }
}