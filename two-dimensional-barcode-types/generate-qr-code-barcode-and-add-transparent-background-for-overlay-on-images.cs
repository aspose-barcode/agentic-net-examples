using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates generating a QR code with a transparent background and overlaying it onto a base image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        // Define file paths for the generated QR code and the final combined image.
        string qrPath = "qr.png";
        string combinedPath = "combined.png";

        // ------------------------------------------------------------
        // 1. Generate a QR code with a transparent background.
        // ------------------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "https://example.com"))
        {
            // Set the background color of the QR code to transparent.
            generator.Parameters.BackColor = Color.Transparent;

            // Save the QR code as a PNG file (PNG supports transparency).
            generator.Save(qrPath, BarCodeImageFormat.Png);
        }

        // ------------------------------------------------------------
        // 2. Create a base image (white canvas) and overlay the QR code onto it.
        // ------------------------------------------------------------
        using (var baseBitmap = new Bitmap(400, 400, PixelFormat.Format32bppArgb))
        {
            // Obtain a Graphics object to draw on the base bitmap.
            using (var graphics = Graphics.FromImage(baseBitmap))
            {
                // Fill the entire canvas with white color.
                graphics.Clear(Color.White);

                // Load the previously generated QR code image.
                using (var qrImage = Image.FromFile(qrPath))
                {
                    // Define the destination rectangle where the QR code will be drawn.
                    var destRect = new Rectangle(100, 100, 200, 200);

                    // Draw the QR code onto the base canvas within the specified rectangle.
                    graphics.DrawImage(qrImage, destRect);
                }
            }

            // Save the resulting combined image as a PNG file.
            baseBitmap.Save(combinedPath, ImageFormat.Png);
        }

        // Output the full paths of the generated files for user reference.
        Console.WriteLine($"QR code saved to: {Path.GetFullPath(qrPath)}");
        Console.WriteLine($"Combined image saved to: {Path.GetFullPath(combinedPath)}");
    }
}