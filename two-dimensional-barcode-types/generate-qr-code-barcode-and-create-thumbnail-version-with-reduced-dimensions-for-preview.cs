using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates generating a QR code image and creating a thumbnail using Aspose.BarCode and Aspose.Drawing.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates a QR code, saves it, creates a thumbnail, and saves the thumbnail.
    /// </summary>
    static void Main()
    {
        // Define file paths for the full-size QR code and its thumbnail.
        string fullPath = "qr.png";
        string thumbPath = "qr_thumb.png";

        // --------------------------------------------------------------------
        // Generate QR code and save the full-size image.
        // --------------------------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "https://example.com"))
        {
            // Optional: set error correction level if desired.
            // generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelM;

            // Save the generated QR code to the specified path.
            generator.Save(fullPath);
        }

        // --------------------------------------------------------------------
        // Load the saved QR code image, create a thumbnail, and save it.
        // --------------------------------------------------------------------
        using (var original = Image.FromFile(fullPath))
        {
            // Calculate thumbnail dimensions (e.g., 25% of the original size).
            int thumbWidth = original.Width / 4;
            int thumbHeight = original.Height / 4;

            // Create a thumbnail image with the calculated dimensions.
            using (var thumbnail = original.GetThumbnailImage(thumbWidth, thumbHeight, null, IntPtr.Zero))
            {
                // Save the thumbnail as a PNG file.
                thumbnail.Save(thumbPath, ImageFormat.Png);
            }
        }

        // Output the locations of the saved files to the console.
        Console.WriteLine($"QR code saved to: {fullPath}");
        Console.WriteLine($"Thumbnail saved to: {thumbPath}");
    }
}