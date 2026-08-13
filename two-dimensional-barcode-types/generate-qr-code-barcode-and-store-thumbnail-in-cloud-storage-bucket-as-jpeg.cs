// Title: Generate QR Code and Save Thumbnail as JPEG
// Description: Creates a QR Code barcode, generates a 100x100 pixel thumbnail, and saves it as a JPEG file (simulating cloud storage).
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing how to use BarcodeGenerator to create QR Code barcodes, manipulate the resulting bitmap, and export images in common formats. Typical use cases include creating printable QR codes, generating thumbnails for web previews, and preparing images for upload to cloud storage. Developers often work with BarcodeGenerator, Bitmap, and ImageFormat classes to customize barcode appearance and handle image output.
// Prompt: Generate QR Code barcode and store thumbnail in cloud storage bucket as JPEG.
// Tags: qr code, barcode generation, thumbnail, jpeg, aspose.barcode, image processing, cloud storage

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates generating a QR Code barcode, creating a thumbnail, and saving it as a JPEG.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that performs the barcode generation and thumbnail saving.
    /// </summary>
    static void Main()
    {
        // Text to encode in the QR code.
        const string qrText = "https://example.com";

        // Temporary file path for the thumbnail image (acts as a placeholder for cloud storage).
        string thumbnailPath = Path.Combine(Path.GetTempPath(), "qr_thumbnail.jpg");

        // Initialize the QR code generator with the desired text.
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, qrText))
        {
            // Optional: set the QR error correction level to improve readability.
            generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelM;

            // Generate the full‑size barcode image as a bitmap.
            using (Bitmap fullSizeBitmap = generator.GenerateBarCodeImage())
            {
                // Desired thumbnail dimensions.
                const int thumbWidth = 100;
                const int thumbHeight = 100;

                // Create a thumbnail from the full‑size bitmap.
                using (Bitmap thumbnail = fullSizeBitmap.GetThumbnailImage(thumbWidth, thumbHeight, null, IntPtr.Zero) as Bitmap)
                {
                    if (thumbnail == null)
                    {
                        Console.WriteLine("Failed to create thumbnail.");
                        return;
                    }

                    // Save the thumbnail as a JPEG file.
                    using (var stream = new FileStream(thumbnailPath, FileMode.Create, FileAccess.Write))
                    {
                        thumbnail.Save(stream, ImageFormat.Jpeg);
                    }

                    Console.WriteLine($"Thumbnail saved to: {thumbnailPath}");
                }
            }
        }

        // NOTE: In a real scenario, upload 'thumbnailPath' to a cloud storage bucket using the appropriate SDK.
    }
}