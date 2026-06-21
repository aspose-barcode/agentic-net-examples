using System;
using System.Diagnostics;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates generating a barcode, applying a sharpening filter,
/// and recognizing the barcode using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Applies a simple sharpening filter (approximation of Gaussian blur removal) to the given bitmap.
    /// </summary>
    /// <param name="source">The source bitmap to be processed.</param>
    /// <returns>A new bitmap with the sharpening filter applied.</returns>
    static Bitmap ApplySharpenFilter(Bitmap source)
    {
        int width = source.Width;
        int height = source.Height;
        Bitmap result = new Bitmap(width, height);

        // Sharpen kernel matrix
        int[,] kernel = new int[,] { { 0, -1, 0 }, { -1, 5, -1 }, { 0, -1, 0 } };
        int kernelSize = 3;
        int offset = kernelSize / 2;

        // Iterate over each pixel in the source image
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                int r = 0, g = 0, b = 0;

                // Apply kernel to the neighbourhood of the current pixel
                for (int ky = -offset; ky <= offset; ky++)
                {
                    int py = y + ky;
                    if (py < 0 || py >= height) continue; // Skip out‑of‑bounds rows

                    for (int kx = -offset; kx <= offset; kx++)
                    {
                        int px = x + kx;
                        if (px < 0 || px >= width) continue; // Skip out‑of‑bounds columns

                        Color pixel = source.GetPixel(px, py);
                        int weight = kernel[ky + offset, kx + offset];
                        r += pixel.R * weight;
                        g += pixel.G * weight;
                        b += pixel.B * weight;
                    }
                }

                // Clamp color values to the valid byte range
                r = Math.Max(0, Math.Min(255, r));
                g = Math.Max(0, Math.Min(255, g));
                b = Math.Max(0, Math.Min(255, b));

                result.SetPixel(x, y, Color.FromArgb(r, g, b));
            }
        }

        return result;
    }

    /// <summary>
    /// Entry point of the program. Generates a barcode, sharpens the image,
    /// and attempts to read the barcode while measuring recognition time.
    /// </summary>
    static void Main()
    {
        // Generate a sample barcode image (Code128) with checksum enabled
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            generator.Parameters.Barcode.IsChecksumEnabled = EnableChecksum.Yes;

            // Save the generated barcode to a memory stream in PNG format
            using (var ms = new MemoryStream())
            {
                generator.Save(ms, BarCodeImageFormat.Png);
                ms.Position = 0; // Reset stream position for reading

                // Load the generated image into a bitmap
                using (var originalBitmap = new Bitmap(ms))
                {
                    // Apply the sharpening filter to reduce blur
                    using (var processedBitmap = ApplySharpenFilter(originalBitmap))
                    {
                        // Save processed image for visual verification (optional)
                        processedBitmap.Save("processed.png", ImageFormat.Png);

                        // Start timing the barcode recognition process
                        var stopwatch = Stopwatch.StartNew();

                        // Initialize the barcode reader for all supported types
                        using (var reader = new BarCodeReader(processedBitmap, DecodeType.AllSupportedTypes))
                        {
                            // Optional: improve detection on degraded images
                            reader.QualitySettings.Deconvolution = DeconvolutionMode.Fast;

                            // Read and output all detected barcodes
                            foreach (var result in reader.ReadBarCodes())
                            {
                                Console.WriteLine($"Detected Type: {result.CodeTypeName}");
                                Console.WriteLine($"Code Text: {result.CodeText}");
                            }
                        }

                        // Stop timing and output the elapsed time
                        stopwatch.Stop();
                        Console.WriteLine($"Recognition time (ms): {stopwatch.ElapsedMilliseconds}");
                    }
                }
            }
        }
    }
}