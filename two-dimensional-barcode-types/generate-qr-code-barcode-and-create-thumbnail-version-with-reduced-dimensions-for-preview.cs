// Title: Generate QR Code and Thumbnail Preview
// Description: Demonstrates creating a QR Code barcode image and a reduced-size thumbnail for preview purposes.
// Category-Description: This example belongs to the Aspose.BarCode image generation category. It shows how to use BarcodeGenerator to produce a QR Code, then uses Aspose.Drawing to manipulate the bitmap and create a smaller preview image. Developers working with barcode rendering, image scaling, or preview generation commonly use the BarcodeGenerator, BarCodeImageFormat, Bitmap, and Graphics classes.
// Prompt: Generate QR Code barcode and create a thumbnail version with reduced dimensions for preview.
// Tags: qr code, barcode generation, thumbnail, png, aspose.barcode, aspose.drawing

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that generates a QR Code barcode, saves the full‑size image,
/// and creates a smaller thumbnail version for quick preview.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates the QR Code, creates a thumbnail, and writes the output file paths to the console.
    /// </summary>
    static void Main()
    {
        // Determine the output directory (current working directory)
        string outputDir = Directory.GetCurrentDirectory();

        // Build full file paths for the original QR image and its thumbnail
        string fullPath = Path.Combine(outputDir, "qr.png");
        string thumbPath = Path.Combine(outputDir, "qr_thumbnail.png");

        // Generate QR Code barcode and save the full‑size PNG image
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.QR, "Aspose.BarCode QR Example"))
        {
            // Set the module size (pixel dimension) for the QR Code
            generator.Parameters.Barcode.XDimension.Pixels = 4;

            // Save the generated QR Code directly to a file
            generator.Save(fullPath, BarCodeImageFormat.Png);

            // Create a thumbnail by scaling the generated bitmap
            using (Bitmap fullBitmap = generator.GenerateBarCodeImage())
            {
                // Calculate thumbnail dimensions (50% of original size)
                int thumbWidth = fullBitmap.Width / 2;
                int thumbHeight = fullBitmap.Height / 2;

                // Create a new bitmap with the thumbnail dimensions
                using (Bitmap thumbBitmap = new Bitmap(thumbWidth, thumbHeight))
                {
                    // Draw the scaled image onto the thumbnail bitmap
                    using (Graphics graphics = Graphics.FromImage(thumbBitmap))
                    {
                        graphics.DrawImage(fullBitmap, new Rectangle(0, 0, thumbWidth, thumbHeight));
                    }

                    // Save the thumbnail as a PNG file
                    thumbBitmap.Save(thumbPath, ImageFormat.Png);
                }
            }
        }

        // Output the locations of the saved images
        Console.WriteLine($"Full QR Code saved to: {fullPath}");
        Console.WriteLine($"Thumbnail saved to: {thumbPath}");
    }
}