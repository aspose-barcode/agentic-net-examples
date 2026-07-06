// Title: Test barcode readability after Gaussian blur
// Description: Generates a Code128 barcode, applies a 3x3 Gaussian blur to simulate printing defects, and attempts to read the blurred image.
// Category-Description: Demonstrates Aspose.BarCode generation, image manipulation with Aspose.Drawing, and barcode recognition using BarCodeReader. This example shows how to create a barcode, modify its image to mimic real‑world damage, and use high‑quality decoding settings—common tasks for developers testing robustness of barcode scanning solutions.
// Prompt: Test barcode readability after applying Gaussian blur to the generated image to simulate printing defects.
// Tags: code128, barcode generation, gaussian blur, image processing, barcode recognition, highquality, aspose.barcode, aspose.drawing

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates generating a barcode, applying Gaussian blur, and reading the blurred barcode.
/// </summary>
class Program
{
    /// <summary>
    /// Applies a simple 3x3 Gaussian blur to the source bitmap.
    /// </summary>
    /// <param name="source">The original bitmap to blur.</param>
    /// <returns>A new bitmap containing the blurred image.</returns>
    static Bitmap ApplyGaussianBlur(Bitmap source)
    {
        int width = source.Width;
        int height = source.Height;
        Bitmap blurred = new Bitmap(width, height);

        // Gaussian kernel 3x3
        int[,] kernel = new int[,] { { 1, 2, 1 }, { 2, 4, 2 }, { 1, 2, 1 } };
        int kernelSum = 16;

        // Convolve kernel over interior pixels
        for (int y = 1; y < height - 1; y++)
        {
            for (int x = 1; x < width - 1; x++)
            {
                int r = 0, g = 0, b = 0;
                for (int ky = -1; ky <= 1; ky++)
                {
                    for (int kx = -1; kx <= 1; kx++)
                    {
                        Color pixel = source.GetPixel(x + kx, y + ky);
                        int weight = kernel[ky + 1, kx + 1];
                        r += pixel.R * weight;
                        g += pixel.G * weight;
                        b += pixel.B * weight;
                    }
                }
                // Normalize and clamp color values
                r = Math.Clamp(r / kernelSum, 0, 255);
                g = Math.Clamp(g / kernelSum, 0, 255);
                b = Math.Clamp(b / kernelSum, 0, 255);
                blurred.SetPixel(x, y, Color.FromArgb(r, g, b));
            }
        }

        // Copy edge pixels unchanged to preserve image dimensions
        for (int x = 0; x < width; x++)
        {
            blurred.SetPixel(x, 0, source.GetPixel(x, 0));
            blurred.SetPixel(x, height - 1, source.GetPixel(x, height - 1));
        }
        for (int y = 0; y < height; y++)
        {
            blurred.SetPixel(0, y, source.GetPixel(0, y));
            blurred.SetPixel(width - 1, y, source.GetPixel(width - 1, y));
        }

        return blurred;
    }

    /// <summary>
    /// Entry point: creates a barcode image, blurs it, and attempts to decode the blurred image.
    /// </summary>
    static void Main()
    {
        const string originalPath = "barcode.png";
        const string blurredPath = "barcode_blurred.png";
        const string barcodeText = "123456789012";

        // Generate a Code128 barcode and save it to disk.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, barcodeText))
        {
            // Optional: set image dimensions for better visibility.
            generator.Parameters.ImageWidth.Point = 300f;
            generator.Parameters.ImageHeight.Point = 150f;
            generator.Save(originalPath, BarCodeImageFormat.Png);
        }

        // Ensure the barcode image was created successfully.
        if (!File.Exists(originalPath))
        {
            Console.WriteLine($"Failed to create barcode image at '{originalPath}'.");
            return;
        }

        // Load the original image, apply Gaussian blur, and save the blurred version.
        using (var originalBitmap = new Bitmap(originalPath))
        using (var blurredBitmap = ApplyGaussianBlur(originalBitmap))
        {
            blurredBitmap.Save(blurredPath, ImageFormat.Png);
        }

        // Verify the blurred image was saved.
        if (!File.Exists(blurredPath))
        {
            Console.WriteLine($"Failed to create blurred image at '{blurredPath}'.");
            return;
        }

        // Read the blurred barcode using high‑quality settings to improve detection.
        using (var reader = new BarCodeReader(blurredPath, DecodeType.Code128))
        {
            // Apply the HighQuality preset for better handling of damaged barcodes.
            reader.QualitySettings = QualitySettings.HighQuality;

            foreach (var result in reader.ReadBarCodes())
            {
                Console.WriteLine($"Detected Type: {result.CodeTypeName}");
                Console.WriteLine($"Code Text   : {result.CodeText}");
                Console.WriteLine($"Confidence  : {result.Confidence}");
                Console.WriteLine($"ReadingQuality: {result.ReadingQuality}");
                var bounds = result.Region.Rectangle;
                Console.WriteLine($"Region      : X={bounds.X}, Y={bounds.Y}, Width={bounds.Width}, Height={bounds.Height}");
            }
        }
    }
}