// Title: Generate QR Code and Create Thumbnail for Web UI
// Description: Demonstrates generating a QR Code barcode, saving it as a PNG file, and creating a 100x100 thumbnail image for display in a web interface.
// Category-Description: This example belongs to the Aspose.BarCode generation and image manipulation category. It showcases the use of BarcodeGenerator (Aspose.BarCode.Generation) to create QR Code barcodes, and Aspose.Drawing to resize images. Typical scenarios include preparing barcode images for web pages, mobile apps, or any UI where a smaller preview is needed. Developers often need to generate barcodes on the fly and provide thumbnail previews without additional third‑party tools.
// Prompt: Generate QR Code barcode and retrieve thumbnail from cloud storage for display in web UI.
// Tags: qr code, barcode generation, thumbnail, image processing, aspose.barcode, aspose.drawing, png

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that creates a QR Code barcode, saves it, and generates a thumbnail image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a QR Code, creates a thumbnail, and writes file paths to the console.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder to store generated files
        string tempFolder = Path.Combine(Path.GetTempPath(), "AsposeBarcodeDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Define file paths for the QR code image and its thumbnail
        string qrPath = Path.Combine(tempFolder, "qr.png");
        string thumbPath = Path.Combine(tempFolder, "qr_thumb.png");

        // Generate QR Code barcode and save it as PNG
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.QR, "Hello Aspose Barcode"))
        {
            // Set module size (pixel dimension) for the QR code
            generator.Parameters.Barcode.XDimension.Pixels = 4f;
            // Set error correction level to Medium (Level M)
            generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelM;
            // Save the generated QR code image
            generator.Save(qrPath, BarCodeImageFormat.Png);
        }

        // Load the generated QR code image and create a 100x100 thumbnail
        using (Bitmap original = new Bitmap(qrPath))
        {
            int thumbWidth = 100;
            int thumbHeight = 100;

            // Create a new bitmap for the thumbnail
            using (Bitmap thumbnail = new Bitmap(thumbWidth, thumbHeight))
            {
                // Draw the original image onto the thumbnail bitmap, scaling it down
                using (Graphics graphics = Graphics.FromImage(thumbnail))
                {
                    graphics.DrawImage(original, 0, 0, thumbWidth, thumbHeight);
                }

                // Save the thumbnail as PNG
                thumbnail.Save(thumbPath, ImageFormat.Png);
            }
        }

        // Output the locations of the generated files
        Console.WriteLine("QR code saved to: " + qrPath);
        Console.WriteLine("Thumbnail saved to: " + thumbPath);
    }
}