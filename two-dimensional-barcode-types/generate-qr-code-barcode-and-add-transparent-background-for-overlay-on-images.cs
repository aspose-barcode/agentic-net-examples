// Title: Generate QR Code with Transparent Background and Overlay on Image
// Description: Demonstrates how to create a QR Code barcode with a transparent background using Aspose.BarCode, then overlay it onto another image.
// Category-Description: This example belongs to the Aspose.BarCode image generation and manipulation category. It showcases the BarcodeGenerator class for QR code creation, setting background transparency, and using Aspose.Drawing to compose images. Developers often need to generate barcodes that can be placed over UI elements or photos without obscuring the background, making transparent barcodes essential for overlay scenarios.
// Prompt: Generate QR Code barcode and add a transparent background for overlay on images.
// Tags: qr code, transparent background, image overlay, aspose.barcode, aspose.drawing, png, barcode generation

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that generates a QR Code with a transparent background
/// and overlays it onto a base image using Aspose.BarCode and Aspose.Drawing.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates the QR Code, creates a composite image, and saves both files.
    /// </summary>
    static void Main()
    {
        // Define output directory in the system temporary folder
        string outputDir = Path.Combine(Path.GetTempPath(), "AsposeBarcodeDemo");
        if (!Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        // File paths for the generated QR code and the final composite image
        string qrPath = Path.Combine(outputDir, "qr_transparent.png");
        string finalPath = Path.Combine(outputDir, "qr_overlay.png");

        // Generate QR Code with transparent background
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "https://example.com"))
        {
            // Set background to transparent and barcode color to black
            generator.Parameters.BackColor = Aspose.Drawing.Color.Transparent;
            generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Black;

            // Optional: adjust module size (pixel dimension)
            generator.Parameters.Barcode.XDimension.Pixels = 4f;

            // Save QR code to a memory stream in PNG format
            using (var qrStream = new MemoryStream())
            {
                generator.Save(qrStream, BarCodeImageFormat.Png);
                qrStream.Position = 0; // Reset stream position for reading

                // Load QR code image from the stream
                using (var qrBitmap = new Bitmap(qrStream))
                {
                    // Create a base bitmap (400x400) with 32bpp ARGB pixel format
                    using (var baseBitmap = new Bitmap(400, 400, PixelFormat.Format32bppArgb))
                    {
                        // Obtain graphics object to draw on the base bitmap
                        using (var graphics = Graphics.FromImage(baseBitmap))
                        {
                            // Fill the base image with a solid light gray color
                            graphics.Clear(Aspose.Drawing.Color.LightGray);

                            // Draw the QR code onto the base image at position (100,100)
                            graphics.DrawImage(qrBitmap, new Rectangle(100, 100, qrBitmap.Width, qrBitmap.Height));
                        }

                        // Save the QR code image with transparent background (optional)
                        qrBitmap.Save(qrPath, ImageFormat.Png);

                        // Save the final composite image (base + QR overlay)
                        baseBitmap.Save(finalPath, ImageFormat.Png);
                    }
                }
            }
        }

        // Output the locations of the saved images
        Console.WriteLine("QR code with transparent background saved to:");
        Console.WriteLine(qrPath);
        Console.WriteLine("Composite image saved to:");
        Console.WriteLine(finalPath);
    }
}