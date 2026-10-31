// Title: Generate QR Code and Thumbnail Preview
// Description: Demonstrates creating a QR Code barcode, saving the full-size image, and producing a reduced‑size thumbnail for preview purposes.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to use BarcodeGenerator with EncodeTypes.QR, configure barcode parameters, and render images using Aspose.Drawing. Typical use cases include generating QR codes for URLs or product information and creating smaller preview thumbnails for UI displays. Developers often need to save the full‑resolution barcode and a scaled‑down version for quick loading in web or mobile applications.
// Prompt: Generate QR Code barcode and create a thumbnail version with reduced dimensions for preview.
// Tags: qr code, barcode generation, thumbnail, image scaling, aspose.barcode, aspose.drawing, png

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that generates a QR Code barcode, saves the full‑size image,
/// and creates a thumbnail version for quick preview.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        // Define a temporary output directory and ensure it exists.
        string outputDir = Path.Combine(Path.GetTempPath(), "AsposeBarcodeDemo");
        Directory.CreateDirectory(outputDir);

        // Paths for the full‑size QR code image and its thumbnail.
        string fullPath = Path.Combine(outputDir, "qr_full.png");
        string thumbPath = Path.Combine(outputDir, "qr_thumb.png");

        // Create a QR Code generator with the desired text.
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.QR, "Aspose QR Code"))
        {
            // Set the module size (pixel dimension) of the QR code.
            generator.Parameters.Barcode.XDimension.Pixels = 4;

            // Save the full‑size QR code directly to a PNG file.
            generator.Save(fullPath, BarCodeImageFormat.Png);

            // Generate a Bitmap of the full QR code for further processing.
            using (Bitmap fullBitmap = generator.GenerateBarCodeImage())
            {
                // Desired thumbnail dimensions.
                int thumbWidth = 100;
                int thumbHeight = 100;

                // Create a new Bitmap that will hold the scaled thumbnail.
                using (Bitmap thumbBitmap = new Bitmap(thumbWidth, thumbHeight))
                {
                    // Obtain a Graphics object to draw the scaled image.
                    using (Graphics graphics = Graphics.FromImage(thumbBitmap))
                    {
                        // Draw the full‑size bitmap onto the thumbnail bitmap, scaling it to fit.
                        graphics.DrawImage(fullBitmap, new Rectangle(0, 0, thumbWidth, thumbHeight));
                    }

                    // Save the thumbnail image as PNG.
                    thumbBitmap.Save(thumbPath, ImageFormat.Png);
                }
            }
        }

        // Output the locations of the generated files.
        Console.WriteLine($"Full QR code saved to: {fullPath}");
        Console.WriteLine($"Thumbnail QR code saved to: {thumbPath}");
    }
}