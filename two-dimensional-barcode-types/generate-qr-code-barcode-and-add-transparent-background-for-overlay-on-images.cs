// Title: Generate QR Code with Transparent Background for Image Overlay
// Description: Demonstrates creating a QR Code barcode with a transparent background and compositing it onto a simple image.
// Category-Description: This example belongs to the Aspose.BarCode image generation and manipulation category. It showcases the use of BarcodeGenerator, EncodeTypes, and drawing classes to produce QR Code barcodes, adjust visual properties like background transparency, and overlay them onto other graphics. Developers often need to embed barcodes into UI elements, marketing materials, or composite images while preserving transparency for seamless integration.
// Prompt: Generate QR Code barcode and add a transparent background for overlay on images.
// Tags: qr code, transparent background, image overlay, aspose.barcode, aspose.drawing, png

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates generating a QR Code with a transparent background and overlaying it onto a background image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates a background bitmap, generates a QR Code with transparency,
    /// draws it onto the background, and saves the result as a PNG file.
    /// </summary>
    static void Main()
    {
        // Path where the final composite image will be saved
        const string outputPath = "overlay.png";

        // Create a 400x400 pixel background image filled with light gray
        using (var background = new Bitmap(400, 400))
        {
            using (var graphics = Graphics.FromImage(background))
            {
                graphics.Clear(Aspose.Drawing.Color.LightGray);
            }

            // Initialize a QR Code generator with the desired data
            using (var generator = new BarcodeGenerator(EncodeTypes.QR, "https://example.com"))
            {
                // Set the QR Code background to transparent
                generator.Parameters.BackColor = Aspose.Drawing.Color.Transparent;

                // Optional: use the highest error correction level for better resilience
                generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelH;

                // Generate the QR Code as a bitmap image
                using (var qrBitmap = generator.GenerateBarCodeImage())
                {
                    // Draw the QR Code onto the background at coordinates (50, 50)
                    using (var graphics = Graphics.FromImage(background))
                    {
                        graphics.DrawImage(qrBitmap, 50, 50, qrBitmap.Width, qrBitmap.Height);
                    }
                }
            }

            // Save the combined image as a PNG to preserve transparency
            background.Save(outputPath, ImageFormat.Png);
        }

        // Inform the user where the file was saved
        Console.WriteLine($"QR code overlay image saved to: {outputPath}");
    }
}