// Title: Generate QR Code and thumbnail preview
// Description: Demonstrates creating a QR Code barcode, saving the full-size image, and producing a smaller thumbnail for preview purposes.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, illustrating how to use BarcodeGenerator, set QR error correction, and manipulate images with Aspose.Drawing to create thumbnails. Typical use cases include generating QR codes for URLs and providing lightweight preview images in web or mobile applications. Developers often need to export barcodes to PNG and create reduced‑size versions for UI thumbnails.
// Prompt: Generate QR Code barcode and create a thumbnail version with reduced dimensions for preview.
// Tags: qr code, barcode generation, thumbnail, image processing, aspose.barcode, aspose.drawing, png

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates QR Code generation and thumbnail creation using Aspose.BarCode and Aspose.Drawing.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that creates a QR Code, saves it, and generates a 100x100 thumbnail.
    /// </summary>
    static void Main()
    {
        // Define a temporary output directory and ensure it exists
        string outputDir = Path.Combine(Path.GetTempPath(), "AsposeBarcodeDemo");
        Directory.CreateDirectory(outputDir);

        // Build file paths for the full-size QR code and its thumbnail
        string fullPath = Path.Combine(outputDir, "qr.png");
        string thumbPath = Path.Combine(outputDir, "qr_thumb.png");

        // Initialize QR Code generator with sample text (a URL)
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "https://example.com"))
        {
            // Optional: set QR error correction level to Medium
            generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelM;

            // Save the full-size QR code image as PNG
            generator.Save(fullPath, BarCodeImageFormat.Png);
            Console.WriteLine($"Full QR code saved to: {fullPath}");

            // Generate a bitmap for further image processing
            using (Bitmap bitmap = generator.GenerateBarCodeImage())
            {
                // Write bitmap to a memory stream in PNG format
                using (var ms = new MemoryStream())
                {
                    bitmap.Save(ms, ImageFormat.Png);
                    ms.Position = 0; // Reset stream position for reading

                    // Load the image from the memory stream to use GetThumbnailImage
                    using (Image img = Image.FromStream(ms))
                    {
                        // Abort delegate required by GetThumbnailImage (always continue)
                        Image.GetThumbnailImageAbort abort = delegate { return false; };

                        // Create a 100x100 pixel thumbnail
                        using (Image thumb = img.GetThumbnailImage(100, 100, abort, IntPtr.Zero))
                        {
                            // Save the thumbnail as PNG
                            thumb.Save(thumbPath, ImageFormat.Png);
                            Console.WriteLine($"Thumbnail saved to: {thumbPath}");
                        }
                    }
                }
            }
        }

        Console.WriteLine("Barcode generation completed.");
    }
}