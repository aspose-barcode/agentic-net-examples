using System;
using System.Collections.Generic;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates generation of Swiss Post Parcel barcodes, combines them into a single TIFF,
/// and cleans up temporary resources.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates individual barcode images, stacks them vertically into a TIFF,
    /// and performs cleanup of temporary files.
    /// </summary>
    static void Main()
    {
        // Sample Swiss Post Parcel codes (international format)
        var codes = new List<string>
        {
            "123456789012",
            "987654321098",
            "112233445566",
            "665544332211",
            "000111222333"
        };

        // Create a temporary directory to store individual barcode PNG files
        string tempDir = Path.Combine(Path.GetTempPath(), "SwissPostBarcodes");
        if (!Directory.Exists(tempDir))
        {
            Directory.CreateDirectory(tempDir);
        }

        var imagePaths = new List<string>();

        // --------------------------------------------------------------------
        // Generate individual barcode images and save them as PNG files
        // --------------------------------------------------------------------
        for (int i = 0; i < codes.Count; i++)
        {
            string code = codes[i];
            string imagePath = Path.Combine(tempDir, $"barcode_{i}.png");

            // Use Aspose.BarCode to generate a Swiss Post Parcel barcode
            using (var generator = new BarcodeGenerator(EncodeTypes.SwissPostParcel, code))
            {
                // Set resolution to 300 DPI (optional, can be adjusted)
                generator.Parameters.Resolution = 300f;
                generator.Save(imagePath, BarCodeImageFormat.Png);
            }

            imagePaths.Add(imagePath);
        }

        // --------------------------------------------------------------------
        // Load generated PNGs, determine combined dimensions, and store bitmaps
        // --------------------------------------------------------------------
        var bitmaps = new List<Bitmap>();
        int maxWidth = 0;
        int totalHeight = 0;

        foreach (var path in imagePaths)
        {
            using (var bmp = new Bitmap(path))
            {
                // Clone the bitmap so it remains valid after the using block disposes the original
                var clone = (Bitmap)bmp.Clone();
                bitmaps.Add(clone);

                // Track the widest image and cumulative height for the final canvas
                if (clone.Width > maxWidth) maxWidth = clone.Width;
                totalHeight += clone.Height;
            }
        }

        // --------------------------------------------------------------------
        // Create a combined bitmap and draw each barcode image vertically stacked
        // --------------------------------------------------------------------
        using (var combined = new Bitmap(maxWidth, totalHeight))
        {
            using (var graphics = Graphics.FromImage(combined))
            {
                // Fill background with white
                graphics.Clear(Color.White);

                int offsetY = 0; // Y-coordinate where the next image will be drawn
                foreach (var bmp in bitmaps)
                {
                    graphics.DrawImage(bmp, 0, offsetY, bmp.Width, bmp.Height);
                    offsetY += bmp.Height; // Move down for the next image
                }
            }

            // Save the combined image as a TIFF file in the current working directory
            string outputPath = Path.Combine(Environment.CurrentDirectory, "SwissPostParcel_Barcodes.tiff");
            combined.Save(outputPath, ImageFormat.Tiff);
            Console.WriteLine($"Combined TIFF saved to: {outputPath}");
        }

        // --------------------------------------------------------------------
        // Cleanup: dispose bitmaps and delete temporary files/directories
        // --------------------------------------------------------------------
        foreach (var bmp in bitmaps)
        {
            bmp.Dispose();
        }

        foreach (var path in imagePaths)
        {
            try
            {
                File.Delete(path);
            }
            catch
            {
                // Ignored: if deletion fails, continue cleanup
            }
        }

        try
        {
            Directory.Delete(tempDir, true);
        }
        catch
        {
            // Ignored: if deletion fails, continue
        }
    }
}