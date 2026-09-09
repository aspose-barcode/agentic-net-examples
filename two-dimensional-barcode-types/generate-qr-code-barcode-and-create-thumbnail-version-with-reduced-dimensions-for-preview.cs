// Title: Generate QR Code and Create Thumbnail Preview
// Description: Demonstrates generating a QR Code barcode with Aspose.BarCode, saving the full‑size PNG image, and producing a smaller thumbnail for quick preview.
// Category-Description: This example belongs to the Aspose.BarCode image generation category. It showcases the use of BarcodeGenerator to create a QR Code, then leverages Aspose.Drawing's Bitmap and Graphics classes to resize the image. Developers commonly need to generate barcodes for documents and provide thumbnail previews in UI components or reports, making this pattern a frequent requirement in barcode‑related applications.
/// Prompt: Generate QR Code barcode and create a thumbnail version with reduced dimensions for preview.
/// Tags: qr code, barcode generation, thumbnail, image processing, aspose.barcode, aspose.drawing, png

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that creates a QR Code barcode, saves it as a full‑size PNG,
/// and generates a smaller thumbnail image for preview purposes.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        // Define a temporary output folder for the generated images.
        string outputFolder = Path.Combine(Path.GetTempPath(), "AsposeBarcodeDemo");
        Directory.CreateDirectory(outputFolder);

        // Build full paths for the high‑resolution and thumbnail PNG files.
        string fullImagePath = Path.Combine(outputFolder, "qr_full.png");
        string thumbImagePath = Path.Combine(outputFolder, "qr_thumb.png");

        // Create a QR Code barcode generator with sample data.
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.QR, "Sample QR Code"))
        {
            // Optional: increase the module (pixel) size for better readability.
            generator.Parameters.Barcode.XDimension.Pixels = 4f;

            // Save the full‑size QR Code directly to a PNG file.
            generator.Save(fullImagePath, BarCodeImageFormat.Png);

            // Generate an in‑memory bitmap of the QR Code for further processing.
            using (Bitmap fullBitmap = generator.GenerateBarCodeImage())
            {
                // Calculate thumbnail dimensions (25 % of the original size, minimum 1 pixel).
                int thumbWidth = Math.Max(1, fullBitmap.Width / 4);
                int thumbHeight = Math.Max(1, fullBitmap.Height / 4);

                // Create a new bitmap that will hold the scaled‑down image.
                using (Bitmap thumbBitmap = new Bitmap(thumbWidth, thumbHeight))
                {
                    // Obtain a graphics object to draw the scaled image.
                    using (Graphics graphics = Graphics.FromImage(thumbBitmap))
                    {
                        // Render the full‑size bitmap onto the thumbnail bitmap with scaling.
                        graphics.DrawImage(
                            fullBitmap,
                            new Rectangle(0, 0, thumbWidth, thumbHeight));
                    }

                    // Persist the thumbnail bitmap as a PNG file.
                    thumbBitmap.Save(thumbImagePath, ImageFormat.Png);
                }
            }
        }

        // Output the locations of the generated files for user reference.
        Console.WriteLine($"QR code saved to: {fullImagePath}");
        Console.WriteLine($"Thumbnail saved to: {thumbImagePath}");
    }
}