// Title: Generate QR Code with Centered Logo Overlay
// Description: Demonstrates creating a QR Code barcode and overlaying a logo image at its center, then saving the result as a PNG file.
// Category-Description: This example belongs to the Aspose.BarCode image generation category, showcasing how to use BarcodeGenerator, QR encoding, and Aspose.Drawing to customize barcode appearance. Typical use cases include branding QR codes with company logos or adding visual identifiers to barcodes. Developers often need to combine barcode creation with graphic manipulation to produce marketing‑ready images.
// Prompt: Generate QR Code barcode and overlay a logo image at center of barcode.
// Tags: qr code, logo overlay, barcode generation, png output, aspose.barcode, aspose.drawing

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates generating a QR Code barcode and overlaying a logo image at its center.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Creates output folder, ensures a placeholder logo, generates QR code, draws logo, and saves the image.
    /// </summary>
    static void Main()
    {
        // Prepare a temporary output directory for generated files
        string outputDir = Path.Combine(Path.GetTempPath(), "AsposeBarcodeDemo");
        Directory.CreateDirectory(outputDir);

        // Define file paths for the logo image and the final barcode image
        string logoPath = Path.Combine(outputDir, "logo.png");
        string barcodePath = Path.Combine(outputDir, "qr_with_logo.png");

        // Ensure a logo image exists; create a simple red square placeholder if missing
        if (!File.Exists(logoPath))
        {
            using (var placeholder = new Bitmap(100, 100))
            {
                using (var g = Graphics.FromImage(placeholder))
                {
                    g.Clear(Color.Red);
                }
                placeholder.Save(logoPath, ImageFormat.Png);
            }
        }

        // Generate a QR Code barcode with high error correction level
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "https://example.com"))
        {
            generator.Parameters.Barcode.XDimension.Pixels = 4f;
            generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelH;

            // Render the barcode to a bitmap
            using (var barcodeBmp = generator.GenerateBarCodeImage())
            {
                // Load the logo image to be overlaid
                using (var logoBmp = new Bitmap(logoPath))
                {
                    // Calculate coordinates to center the logo on the barcode
                    int x = (barcodeBmp.Width - logoBmp.Width) / 2;
                    int y = (barcodeBmp.Height - logoBmp.Height) / 2;

                    // Draw the logo onto the barcode bitmap
                    using (var graphics = Graphics.FromImage(barcodeBmp))
                    {
                        graphics.DrawImage(logoBmp, x, y, logoBmp.Width, logoBmp.Height);
                    }

                    // Save the combined image as PNG
                    barcodeBmp.Save(barcodePath, ImageFormat.Png);
                }
            }
        }

        Console.WriteLine($"QR code with logo saved to: {barcodePath}");
    }
}