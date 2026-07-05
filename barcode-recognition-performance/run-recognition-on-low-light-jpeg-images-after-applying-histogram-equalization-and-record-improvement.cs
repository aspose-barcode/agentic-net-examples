// Title: Barcode recognition improvement with histogram equalization on low‑light JPEGs
// Description: Demonstrates loading low‑light JPEG images, applying histogram equalization, and comparing barcode detection counts before and after processing.
// Prompt: Run recognition on low‑light JPEG images after applying histogram equalization and record improvement.
// Tags: barcode, recognition, histogram equalization, low-light, jpeg, aspose.barcode, aspose.drawing

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates barcode detection on low‑light JPEG images before and after histogram equalization.
/// </summary>
class Program
{
    /// <summary>
    /// Runs barcode recognition on low‑light JPEG images, applies histogram equalization,
    /// and reports the detection improvement.
    /// </summary>
    static void Main()
    {
        // Directory containing low‑light JPEG images
        string imagesDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Images");
        if (!Directory.Exists(imagesDir))
        {
            Console.WriteLine($"Images directory not found: {imagesDir}");
            return;
        }

        // Retrieve all JPEG files in the directory
        string[] jpegFiles = Directory.GetFiles(imagesDir, "*.jpg");
        if (jpegFiles.Length == 0)
        {
            Console.WriteLine("No JPEG images found.");
            return;
        }

        // Process each image file
        foreach (string filePath in jpegFiles)
        {
            if (!File.Exists(filePath))
            {
                Console.WriteLine($"File does not exist: {filePath}");
                continue;
            }

            // Load original image
            using (Bitmap originalBmp = new Bitmap(filePath))
            {
                // Recognize barcodes in the original image
                int originalCount = RecognizeBarcodes(originalBmp);

                // Apply histogram equalization to improve contrast
                using (Bitmap equalizedBmp = EqualizeHistogram(originalBmp))
                {
                    // Recognize barcodes in the equalized image
                    int equalizedCount = RecognizeBarcodes(equalizedBmp);
                    int improvement = equalizedCount - originalCount;

                    // Output results for the current file
                    Console.WriteLine($"File: {Path.GetFileName(filePath)}");
                    Console.WriteLine($"  Barcodes detected (original): {originalCount}");
                    Console.WriteLine($"  Barcodes detected (equalized): {equalizedCount}");
                    Console.WriteLine($"  Improvement: {improvement}");
                }
            }
        }
    }

    /// <summary>
    /// Recognizes barcodes in the given bitmap using the HighQuality preset.
    /// </summary>
    /// <param name="bmp">Bitmap image to be processed.</param>
    /// <returns>The number of detected barcodes.</returns>
    private static int RecognizeBarcodes(Bitmap bmp)
    {
        using (BarCodeReader reader = new BarCodeReader())
        {
            // Use HighQuality preset for low‑light / damaged images
            reader.QualitySettings = QualitySettings.HighQuality;

            // Set the image for recognition
            reader.SetBarCodeImage(bmp);

            // Perform recognition
            BarCodeResult[] results = reader.ReadBarCodes();
            return results?.Length ?? 0;
        }
    }

    /// <summary>
    /// Performs simple histogram equalization on the input bitmap.
    /// </summary>
    /// <param name="source">Source bitmap to be equalized.</param>
    /// <returns>A new bitmap with equalized luminance (grayscale).</returns>
    private static Bitmap EqualizeHistogram(Bitmap source)
    {
        int width = source.Width;
        int height = source.Height;
        Bitmap result = new Bitmap(width, height);

        // Compute histogram of luminance
        int[] histogram = new int[256];
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                Color pixel = source.GetPixel(x, y);
                // Convert to luminance using Rec. 601 luma formula
                int lum = (int)(0.299 * pixel.R + 0.587 * pixel.G + 0.114 * pixel.B);
                histogram[lum]++;
            }
        }

        // Compute cumulative distribution function (CDF)
        int[] cdf = new int[256];
        cdf[0] = histogram[0];
        for (int i = 1; i < 256; i++)
        {
            cdf[i] = cdf[i - 1] + histogram[i];
        }

        // Normalize CDF to [0,255]
        int totalPixels = width * height;
        byte[] lut = new byte[256];
        for (int i = 0; i < 256; i++)
        {
            // Avoid division by zero
            lut[i] = (byte)Math.Round(((double)cdf[i] - cdf[0]) / (totalPixels - cdf[0]) * 255.0);
        }

        // Apply mapping to create equalized image (grayscale)
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                Color pixel = source.GetPixel(x, y);
                int lum = (int)(0.299 * pixel.R + 0.587 * pixel.G + 0.114 * pixel.B);
                byte newVal = lut[lum];
                Color newColor = Color.FromArgb(newVal, newVal, newVal);
                result.SetPixel(x, y, newColor);
            }
        }

        return result;
    }
}