// Title: Apply custom colors to QR barcode and compare images
// Description: Demonstrates generating QR barcodes with different foreground and background colors, saving them as PNG files, and programmatically comparing the visual differences.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and image processing category. It shows how to use BarcodeGenerator, set BarColor and BackColor, render the barcode to a bitmap, and perform pixel‑by‑pixel comparison using Aspose.Drawing. Developers often need to customize barcode appearance for branding and verify visual output in automated tests.
// Prompt: Apply different custom colors to the same barcode type and compare visual differences programmatically.
// Tags: barcode, qr, custom colors, image comparison, aspose.barcode, aspose.drawing, generation, png

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Generates QR barcodes with custom colors, saves them as PNG files,
/// and compares the resulting images pixel by pixel.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates two barcodes with different color schemes,
    /// saves them, and reports the number of differing pixels.
    /// </summary>
    static void Main()
    {
        // Define the data to encode in the barcode
        string codeText = "Sample123";

        // First barcode color scheme: blue bars on white background
        Color barColor1 = Color.Blue;
        Color backColor1 = Color.White;

        // Second barcode color scheme: red bars on yellow background
        Color barColor2 = Color.Red;
        Color backColor2 = Color.Yellow;

        // Generate the two barcode images using the specified colors
        using (Bitmap bmp1 = GenerateBarcode(codeText, barColor1, backColor1))
        using (Bitmap bmp2 = GenerateBarcode(codeText, barColor2, backColor2))
        {
            // Persist the generated images for visual inspection
            SaveBitmap(bmp1, "barcode1.png");
            SaveBitmap(bmp2, "barcode2.png");

            // Compare the two images pixel by pixel
            int diffPixels = CompareBitmaps(bmp1, bmp2);
            if (diffPixels == -1)
            {
                Console.WriteLine("Images have different dimensions and cannot be compared.");
            }
            else
            {
                Console.WriteLine($"Number of differing pixels: {diffPixels}");
            }
        }
    }

    /// <summary>
    /// Generates a QR barcode bitmap with the specified foreground (bar) and background colors.
    /// </summary>
    /// <param name="codeText">The text to encode in the barcode.</param>
    /// <param name="barColor">The color of the barcode bars.</param>
    /// <param name="backColor">The background color of the barcode image.</param>
    /// <returns>A <see cref="Bitmap"/> containing the rendered barcode.</returns>
    static Bitmap GenerateBarcode(string codeText, Color barColor, Color backColor)
    {
        // Initialize the barcode generator for QR code type
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, codeText))
        {
            // Apply custom colors
            generator.Parameters.Barcode.BarColor = barColor;
            generator.Parameters.BackColor = backColor;

            // Render the barcode to a memory stream in PNG format
            using (var ms = new MemoryStream())
            {
                generator.Save(ms, BarCodeImageFormat.Png);
                ms.Position = 0;

                // Load the bitmap from the memory stream
                return new Bitmap(ms);
            }
        }
    }

    /// <summary>
    /// Saves a bitmap to a file in PNG format.
    /// </summary>
    /// <param name="bitmap">The bitmap to save.</param>
    /// <param name="filePath">The destination file path.</param>
    static void SaveBitmap(Bitmap bitmap, string filePath)
    {
        using (var fs = new FileStream(filePath, FileMode.Create, FileAccess.Write))
        {
            bitmap.Save(fs, ImageFormat.Png);
        }
    }

    /// <summary>
    /// Compares two bitmaps pixel by pixel.
    /// Returns -1 if the images have different dimensions; otherwise returns the count of differing pixels.
    /// </summary>
    /// <param name="bmp1">First bitmap to compare.</param>
    /// <param name="bmp2">Second bitmap to compare.</param>
    /// <returns>Number of differing pixels, or -1 if dimensions differ.</returns>
    static int CompareBitmaps(Bitmap bmp1, Bitmap bmp2)
    {
        // Ensure both images have the same size before comparison
        if (bmp1.Width != bmp2.Width || bmp1.Height != bmp2.Height)
            return -1;

        int diffCount = 0;
        for (int y = 0; y < bmp1.Height; y++)
        {
            for (int x = 0; x < bmp1.Width; x++)
            {
                // Increment count when pixel colors differ
                if (bmp1.GetPixel(x, y) != bmp2.GetPixel(x, y))
                    diffCount++;
            }
        }
        return diffCount;
    }
}