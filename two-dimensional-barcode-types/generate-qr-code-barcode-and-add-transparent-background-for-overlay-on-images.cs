using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

class Program
{
    static void Main()
    {
        // QR code content
        const string qrText = "https://example.com";

        // Paths
        const string baseImagePath = "background.jpg";
        const string outputPath = "qr_overlay.png";

        // Create QR code generator with transparent background
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, qrText))
        {
            // Set QR error correction level (optional)
            generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelH;

            // Set image size (pixels)
            generator.Parameters.ImageWidth.Pixels = 300f;
            generator.Parameters.ImageHeight.Pixels = 300f;

            // Transparent background
            generator.Parameters.BackColor = Aspose.Drawing.Color.Transparent;

            // Optional: set bar (foreground) color
            generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Black;

            // Generate QR code bitmap
            using (Bitmap qrBitmap = generator.GenerateBarCodeImage())
            {
                // If a background image exists, overlay the QR code onto it
                if (File.Exists(baseImagePath))
                {
                    using (Bitmap baseImage = new Bitmap(baseImagePath))
                    {
                        // Determine position (bottom‑right corner with 10‑pixel margin)
                        int x = Math.Max(0, baseImage.Width - qrBitmap.Width - 10);
                        int y = Math.Max(0, baseImage.Height - qrBitmap.Height - 10);

                        using (Graphics graphics = Graphics.FromImage(baseImage))
                        {
                            // Draw QR code onto the base image preserving transparency
                            graphics.DrawImage(qrBitmap, x, y, qrBitmap.Width, qrBitmap.Height);
                        }

                        // Save the combined image
                        baseImage.Save(outputPath, ImageFormat.Png);
                    }
                }
                else
                {
                    // No background image – save QR code alone
                    qrBitmap.Save(outputPath, ImageFormat.Png);
                }
            }
        }

        Console.WriteLine($"QR code image saved to '{outputPath}'.");
    }
}