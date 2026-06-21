using System;
using System.IO;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates barcode recognition on a low‑light image before and after applying
/// histogram equalization to improve contrast.
/// </summary>
class Program
{
    /// <summary>
    /// Applies simple histogram equalization to a grayscale bitmap.
    /// </summary>
    /// <param name="source">The source bitmap to be equalized.</param>
    /// <returns>A new bitmap with equalized intensity values.</returns>
    static Bitmap ApplyHistogramEqualization(Bitmap source)
    {
        int width = source.Width;
        int height = source.Height;
        Bitmap result = new Bitmap(width, height);

        // Build histogram of intensity values (0‑255)
        int[] histogram = new int[256];
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                Color c = source.GetPixel(x, y);
                int intensity = (c.R + c.G + c.B) / 3;
                histogram[intensity]++;
            }
        }

        // Compute cumulative distribution function (CDF)
        int[] cdf = new int[256];
        cdf[0] = histogram[0];
        for (int i = 1; i < 256; i++)
        {
            cdf[i] = cdf[i - 1] + histogram[i];
        }

        int totalPixels = width * height;
        byte[] map = new byte[256];

        // Build mapping from old intensity to new equalized intensity
        for (int i = 0; i < 256; i++)
        {
            map[i] = (byte)Math.Round((double)cdf[i] * 255 / totalPixels);
        }

        // Apply the mapping to each pixel to create the equalized image
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                Color c = source.GetPixel(x, y);
                int intensity = (c.R + c.G + c.B) / 3;
                byte newVal = map[intensity];
                Color newColor = Color.FromArgb(newVal, newVal, newVal);
                result.SetPixel(x, y, newColor);
            }
        }

        return result;
    }

    /// <summary>
    /// Performs barcode recognition on the supplied bitmap and writes results to the console.
    /// </summary>
    /// <param name="bitmap">The bitmap to be scanned for barcodes.</param>
    /// <param name="label">A label used to identify the output (e.g., "Original" or "Equalized").</param>
    static void Recognize(Bitmap bitmap, string label)
    {
        // Initialize the barcode reader for all supported types
        using (var reader = new BarCodeReader(bitmap, DecodeType.AllSupportedTypes))
        {
            var results = reader.ReadBarCodes();

            // Output detection results
            if (results.Length > 0)
            {
                foreach (var result in results)
                {
                    Console.WriteLine($"{label}: Detected {result.CodeTypeName} - {result.CodeText}");
                }
            }
            else
            {
                Console.WriteLine($"{label}: No barcode detected.");
            }
        }
    }

    /// <summary>
    /// Entry point of the program. Loads a low‑light image, runs barcode recognition,
    /// applies histogram equalization, and runs recognition again.
    /// </summary>
    static void Main()
    {
        // Path to a low‑light JPEG image (replace with an actual file if available)
        string imagePath = "lowlight.jpg";

        // Verify that the image file exists before proceeding
        if (!File.Exists(imagePath))
        {
            Console.WriteLine($"Image file not found: {imagePath}");
            return;
        }

        // Load the original image from file
        using (var original = new Bitmap(imagePath))
        {
            // Perform barcode recognition on the original low‑light image
            Recognize(original, "Original");

            // Apply histogram equalization to improve contrast
            using (var equalized = ApplyHistogramEqualization(original))
            {
                // Perform barcode recognition on the enhanced image
                Recognize(equalized, "Equalized");
            }
        }
    }
}