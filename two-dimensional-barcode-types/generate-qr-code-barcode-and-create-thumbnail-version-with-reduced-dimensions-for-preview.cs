// Title: Generate QR Code and Thumbnail Preview
// Description: Demonstrates creating a QR Code barcode image and producing a smaller thumbnail for quick preview purposes.
// Category-Description: This example belongs to the Aspose.BarCode generation and image manipulation category. It showcases the use of BarcodeGenerator for QR code creation, and Aspose.Drawing classes (Image, Bitmap, Graphics) to resize and save a thumbnail. Developers often need to generate barcodes and then provide lightweight preview images for web or mobile interfaces.
// Prompt: Generate QR Code barcode and create a thumbnail version with reduced dimensions for preview.
// Tags: qr code, barcode generation, thumbnail, image resizing, aspose.barcode, aspose.drawing, png

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that generates a QR Code barcode, saves it as a PNG,
/// and creates a reduced‑size thumbnail for preview purposes.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates the QR code, verifies its creation,
    /// creates a thumbnail, and writes the output file locations to the console.
    /// </summary>
    static void Main()
    {
        // Define file paths for the full‑size QR code and its thumbnail.
        string fullPath = "qr_full.png";
        string thumbPath = "qr_thumbnail.png";

        // --------------------------------------------------------------------
        // Generate a QR Code barcode and save it as a PNG image.
        // --------------------------------------------------------------------
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.QR))
        {
            // Text to encode in the QR code.
            generator.CodeText = "https://example.com";

            // Set a moderate error correction level (Level M).
            generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelM;

            // Save the generated barcode image.
            generator.Save(fullPath, BarCodeImageFormat.Png);
        }

        // Verify that the QR code image was successfully created.
        if (!File.Exists(fullPath))
        {
            Console.WriteLine("Failed to generate the QR code image.");
            return;
        }

        // --------------------------------------------------------------------
        // Load the full‑size image, create a thumbnail (20% of original size),
        // and save the thumbnail as a PNG.
        // --------------------------------------------------------------------
        using (Image fullImage = Image.FromFile(fullPath))
        {
            // Calculate thumbnail dimensions (20% of the original width/height).
            int thumbWidth = (int)(fullImage.Width * 0.2f);
            int thumbHeight = (int)(fullImage.Height * 0.2f);

            // Ensure dimensions are at least 1 pixel.
            if (thumbWidth <= 0) thumbWidth = 1;
            if (thumbHeight <= 0) thumbHeight = 1;

            // Create a bitmap to hold the thumbnail.
            using (Bitmap thumbnail = new Bitmap(thumbWidth, thumbHeight))
            {
                // Draw the scaled image onto the thumbnail bitmap.
                using (Graphics graphics = Graphics.FromImage(thumbnail))
                {
                    graphics.DrawImage(fullImage, 0, 0, thumbWidth, thumbHeight);
                }

                // Save the thumbnail image.
                thumbnail.Save(thumbPath, ImageFormat.Png);
            }
        }

        // Output the locations of the generated files.
        Console.WriteLine($"QR code saved to '{fullPath}'.");
        Console.WriteLine($"Thumbnail saved to '{thumbPath}'.");
    }
}