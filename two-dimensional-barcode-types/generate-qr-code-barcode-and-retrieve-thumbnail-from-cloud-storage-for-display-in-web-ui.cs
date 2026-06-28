using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;
using Aspose.Drawing.Drawing2D;

/// <summary>
/// Demonstrates generating a QR code, creating a thumbnail, and outputting the thumbnail as a Base64 string.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates a QR code image, creates a thumbnail, saves both to disk,
    /// and prints the thumbnail as a Base64 string for UI display.
    /// </summary>
    static void Main()
    {
        // Define file paths for the QR code image and its thumbnail.
        string qrPath = "qr.png";
        string thumbPath = "qr_thumb.png";

        // ------------------------------------------------------------
        // Generate QR Code barcode using Aspose.BarCode
        // ------------------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "https://example.com"))
        {
            // Optional: set QR error correction level to Medium.
            generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelM;

            // Set image resolution to 300 DPI for high quality.
            generator.Parameters.Resolution = 300f;

            // Save the generated QR code to the specified file.
            generator.Save(qrPath);
        }

        // Verify that the QR code image was successfully created.
        if (!File.Exists(qrPath))
        {
            Console.WriteLine("Failed to generate QR code image.");
            return;
        }

        // ------------------------------------------------------------
        // Load the QR code image and create a 100x100 thumbnail.
        // ------------------------------------------------------------
        using (var original = (Bitmap)Image.FromFile(qrPath))
        {
            int thumbWidth = 100;
            int thumbHeight = 100;

            // Create a new bitmap that will hold the thumbnail.
            using (var thumbnail = new Bitmap(thumbWidth, thumbHeight))
            {
                // Draw the original image onto the thumbnail bitmap with high-quality scaling.
                using (var graphics = Graphics.FromImage(thumbnail))
                {
                    graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    graphics.DrawImage(original, new Rectangle(0, 0, thumbWidth, thumbHeight));
                }

                // Save the thumbnail to a file (simulating cloud storage retrieval).
                thumbnail.Save(thumbPath, ImageFormat.Png);

                // --------------------------------------------------------
                // Convert the thumbnail to a Base64 string for web UI display.
                // --------------------------------------------------------
                using (var ms = new MemoryStream())
                {
                    thumbnail.Save(ms, ImageFormat.Png);
                    string base64 = Convert.ToBase64String(ms.ToArray());

                    Console.WriteLine("Thumbnail Base64:");
                    Console.WriteLine(base64);
                }
            }
        }
    }
}