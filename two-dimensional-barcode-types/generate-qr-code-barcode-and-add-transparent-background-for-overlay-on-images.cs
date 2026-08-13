// Title: Generate QR Code with Transparent Background and Overlay on Image
// Description: Demonstrates how to create a QR Code barcode with a transparent background, save it as PNG, and overlay it onto another image using Aspose.BarCode.
// Category-Description: This example belongs to the Aspose.BarCode generation and image manipulation category. It showcases the BarcodeGenerator class for QR Code creation, the use of Aspose.Drawing for bitmap handling, and typical scenarios such as creating transparent barcodes for UI overlays or composite graphics. Developers often need to generate barcodes with custom backgrounds and combine them with other images for marketing, packaging, or augmented reality applications.
/// Prompt: Generate QR Code barcode and add a transparent background for overlay on images.
/// Tags: qr code, barcode generation, transparent background, overlay, png, aspose.barcode, aspose.drawing

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that creates a QR Code with a transparent background
/// and composites it onto a sample background image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates the QR Code, saves it,
    /// creates a background bitmap, draws the QR Code onto it, and saves the result.
    /// </summary>
    static void Main()
    {
        // --------------------------------------------------------------------
        // Prepare output directory in the system temporary folder
        // --------------------------------------------------------------------
        string outputDir = Path.Combine(Path.GetTempPath(), "AsposeBarcodeDemo");
        Directory.CreateDirectory(outputDir);

        // Define file paths for the generated images
        string qrPath = Path.Combine(outputDir, "qr_transparent.png");
        string overlayPath = Path.Combine(outputDir, "qr_overlay.png");

        // --------------------------------------------------------------------
        // Create QR code generator with a transparent background
        // --------------------------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.QR))
        {
            // Set the data to encode
            generator.CodeText = "https://example.com";

            // Configure a fully transparent background
            generator.Parameters.BackColor = Aspose.Drawing.Color.Transparent;

            // Optional visual tweaks: black foreground and high error correction level
            generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Black;
            generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelH;

            // Save the QR code directly as a transparent PNG file
            generator.Save(qrPath, BarCodeImageFormat.Png);

            // ----------------------------------------------------------------
            // Generate a bitmap of the QR code for further composition
            // ----------------------------------------------------------------
            using (Bitmap qrBitmap = generator.GenerateBarCodeImage())
            {
                // Create a sample background image (300x300) with ARGB pixel format
                using (Bitmap background = new Bitmap(300, 300, PixelFormat.Format32bppArgb))
                {
                    // Obtain a graphics object to draw on the background bitmap
                    using (Graphics g = Graphics.FromImage(background))
                    {
                        // Fill the background with a semi‑transparent light gray color
                        g.Clear(Aspose.Drawing.Color.FromArgb(200, 200, 200, 200));

                        // Draw the QR code bitmap onto the background at position (50, 50)
                        g.DrawImage(qrBitmap, new Point(50, 50));
                    }

                    // Save the combined image (background + QR code) as PNG
                    background.Save(overlayPath, ImageFormat.Png);
                }
            }
        }

        // Output the locations of the generated files
        Console.WriteLine($"QR code with transparent background saved to: {qrPath}");
        Console.WriteLine($"Overlay image saved to: {overlayPath}");
    }
}