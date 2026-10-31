// Title: Barcode recognition improvement using histogram equalization on low‑light images
// Description: Demonstrates generating a QR code, darkening it to simulate low‑light conditions, applying histogram equalization, and comparing barcode detection results.
// Category-Description: This example belongs to the Aspose.BarCode image preprocessing and recognition category. It showcases the BarcodeGenerator, BarCodeReader, and Aspose.Drawing classes for creating barcodes, manipulating image brightness, and enhancing detection in challenging lighting. Developers often use these APIs to improve read rates for scanned or photographed barcodes in real‑world applications.
// Prompt: Run recognition on low‑light JPEG images after applying histogram equalization and record improvement.
// Tags: qr, barcode generation, barcode recognition, low-light, histogram equalization, jpeg, aspose.barcode, aspose.drawing

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates barcode generation, low‑light simulation, histogram equalization, and recognition comparison using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that creates a QR barcode, darkens it, enhances it, and prints detection results.
    /// </summary>
    static void Main()
    {
        // Generate a sample QR barcode
        var generator = new BarcodeGenerator(EncodeTypes.QR, "Test123");
        using (var genStream = new MemoryStream())
        {
            // Save barcode as JPEG into memory stream
            generator.Save(genStream, BarCodeImageFormat.Jpeg);
            genStream.Position = 0;

            using (var originalBitmap = new Bitmap(genStream))
            {
                // Create a low‑light (dark) version of the image by scaling pixel brightness
                using (var darkBitmap = new Bitmap(originalBitmap.Width, originalBitmap.Height))
                {
                    for (int y = 0; y < originalBitmap.Height; y++)
                    {
                        for (int x = 0; x < originalBitmap.Width; x++)
                        {
                            var color = originalBitmap.GetPixel(x, y);
                            int r = (int)(color.R * 0.3);
                            int g = (int)(color.G * 0.3);
                            int b = (int)(color.B * 0.3);
                            var darkColor = Color.FromArgb(r, g, b);
                            darkBitmap.SetPixel(x, y, darkColor);
                        }
                    }

                    // Save the low‑light image to a temporary file
                    string darkPath = Path.Combine(Path.GetTempPath(), "dark.jpg");
                    using (var darkFile = new FileStream(darkPath, FileMode.Create, FileAccess.Write))
                    {
                        darkBitmap.Save(darkFile, ImageFormat.Jpeg);
                    }

                    // Recognize barcode in low‑light image
                    BaseDecodeType decodeAll = DecodeType.AllSupportedTypes;
                    using (var readerDark = new BarCodeReader(darkPath, decodeAll))
                    {
                        readerDark.QualitySettings.BarcodeQuality = BarcodeQualityMode.Low;
                        readerDark.QualitySettings.Deconvolution = DeconvolutionMode.Fast;
                        var resultsDark = readerDark.ReadBarCodes();
                        bool successDark = resultsDark.Length > 0;
                        Console.WriteLine($"Low‑light detection: {(successDark ? "Success" : "Failure")}");
                        if (successDark)
                            Console.WriteLine($"Reading quality: {resultsDark[0].ReadingQuality}");
                    }

                    // Apply histogram equalization to improve contrast
                    using (var equalizedBitmap = HistogramEqualize(darkBitmap))
                    {
                        // Save the equalized image to a temporary file
                        string equalizedPath = Path.Combine(Path.GetTempPath(), "equalized.jpg");
                        using (var eqFile = new FileStream(equalizedPath, FileMode.Create, FileAccess.Write))
                        {
                            equalizedBitmap.Save(eqFile, ImageFormat.Jpeg);
                        }

                        // Recognize barcode in equalized image
                        using (var readerEq = new BarCodeReader(equalizedPath, decodeAll))
                        {
                            readerEq.QualitySettings.BarcodeQuality = BarcodeQualityMode.Low;
                            readerEq.QualitySettings.Deconvolution = DeconvolutionMode.Fast;
                            var resultsEq = readerEq.ReadBarCodes();
                            bool successEq = resultsEq.Length > 0;
                            Console.WriteLine($"Equalized detection: {(successEq ? "Success" : "Failure")}");
                            if (successEq)
                                Console.WriteLine($"Reading quality: {resultsEq[0].ReadingQuality}");
                        }

                        // Cleanup equalized file
                        if (File.Exists(equalizedPath))
                            File.Delete(equalizedPath);
                    }

                    // Cleanup low‑light file
                    if (File.Exists(darkPath))
                        File.Delete(darkPath);
                }
            }
        }
    }

    /// <summary>
    /// Performs histogram equalization on the provided bitmap to enhance contrast.
    /// </summary>
    /// <param name="source">Source bitmap to be equalized.</param>
    /// <returns>A new bitmap with equalized histogram.</returns>
    static Bitmap HistogramEqualize(Bitmap source)
    {
        int width = source.Width;
        int height = source.Height;
        int[] histogram = new int[256];

        // Build histogram of luminance values
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                var c = source.GetPixel(x, y);
                int lum = (int)(0.299 * c.R + 0.587 * c.G + 0.114 * c.B);
                histogram[lum]++;
            }
        }

        // Compute cumulative distribution function (CDF)
        int[] cdf = new int[256];
        cdf[0] = histogram[0];
        for (int i = 1; i < 256; i++)
            cdf[i] = cdf[i - 1] + histogram[i];

        int total = width * height;
        byte[] map = new byte[256];

        // Create mapping from old to new intensity values
        for (int i = 0; i < 256; i++)
        {
            double value = (double)(cdf[i] - cdf[0]) / (total - 1) * 255.0;
            map[i] = (byte)Math.Round(value);
        }

        // Apply mapping to generate equalized image
        var result = new Bitmap(width, height);
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                var c = source.GetPixel(x, y);
                int lum = (int)(0.299 * c.R + 0.587 * c.G + 0.114 * c.B);
                byte newVal = map[lum];
                var newColor = Color.FromArgb(newVal, newVal, newVal);
                result.SetPixel(x, y, newColor);
            }
        }
        return result;
    }
}