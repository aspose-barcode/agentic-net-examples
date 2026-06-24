using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates generating an EAN13 barcode, applying a Gaussian blur,
/// and then recognizing the barcode from the blurred image using Aspose.BarCode.
/// </summary>
class Program
{
    // Simple 5x5 Gaussian blur kernel (sigma ≈ 1.0)
    static readonly double[,] GaussianKernel = new double[5, 5]
    {
        { 1,  4,  7,  4, 1 },
        { 4, 16, 26, 16, 4 },
        { 7, 26, 41, 26, 7 },
        { 4, 16, 26, 16, 4 },
        { 1,  4,  7,  4, 1 }
    };
    // Sum of all kernel values (used for normalization)
    static readonly double KernelSum = 273.0;

    /// <summary>
    /// Entry point of the program. Generates a barcode, blurs it, saves the result,
    /// and attempts to read the barcode from the blurred image.
    /// </summary>
    static void Main()
    {
        const string codeText = "123456789012";
        const string tempPath = "barcode.png";

        // 1. Generate barcode image and save to a memory stream
        using (var generator = new BarcodeGenerator(EncodeTypes.EAN13, codeText))
        {
            generator.Parameters.Resolution = 300f; // high resolution for better quality

            using (var ms = new MemoryStream())
            {
                generator.Save(ms, BarCodeImageFormat.Png);
                ms.Position = 0; // reset stream position for reading

                // 2. Load the image into Aspose.Drawing.Bitmap
                using (var originalBitmap = new Bitmap(ms))
                {
                    // 3. Apply Gaussian blur to the bitmap
                    using (var blurredBitmap = ApplyGaussianBlur(originalBitmap))
                    {
                        // 4. Save blurred image (optional, for visual inspection)
                        blurredBitmap.Save(tempPath, Aspose.Drawing.Imaging.ImageFormat.Png);

                        // 5. Recognize barcode from blurred image
                        using (var reader = new BarCodeReader(blurredBitmap, DecodeType.EAN13))
                        {
                            // Use a high-quality preset to improve detection on blurred image
                            reader.QualitySettings = QualitySettings.HighQuality;

                            // Iterate over all detected barcodes and output details
                            foreach (var result in reader.ReadBarCodes())
                            {
                                Console.WriteLine($"Detected Type: {result.CodeTypeName}");
                                Console.WriteLine($"Detected Text: {result.CodeText}");
                                Console.WriteLine($"Confidence   : {result.Confidence}");
                                Console.WriteLine($"ReadingQuality: {result.ReadingQuality}");
                            }
                        }
                    }
                }
            }
        }
    }

    /// <summary>
    /// Applies a simple Gaussian blur to the provided bitmap and returns a new blurred bitmap.
    /// </summary>
    /// <param name="source">The source bitmap to blur.</param>
    /// <returns>A new bitmap containing the blurred image.</returns>
    static Bitmap ApplyGaussianBlur(Bitmap source)
    {
        int width = source.Width;
        int height = source.Height;
        var blurred = new Bitmap(width, height);

        // Process each pixel (skip the border to avoid out-of-range checks)
        for (int y = 2; y < height - 2; y++)
        {
            for (int x = 2; x < width - 2; x++)
            {
                double a = 0, r = 0, g = 0, b = 0;

                // Convolve the kernel over the neighborhood
                for (int ky = -2; ky <= 2; ky++)
                {
                    for (int kx = -2; kx <= 2; kx++)
                    {
                        Color pixel = source.GetPixel(x + kx, y + ky);
                        double weight = GaussianKernel[ky + 2, kx + 2];

                        a += pixel.A * weight;
                        r += pixel.R * weight;
                        g += pixel.G * weight;
                        b += pixel.B * weight;
                    }
                }

                // Normalize the accumulated values and convert to byte
                byte aByte = (byte)Math.Round(a / KernelSum);
                byte rByte = (byte)Math.Round(r / KernelSum);
                byte gByte = (byte)Math.Round(g / KernelSum);
                byte bByte = (byte)Math.Round(b / KernelSum);

                blurred.SetPixel(x, y, Color.FromArgb(aByte, rByte, gByte, bByte));
            }
        }

        // Copy border pixels unchanged to preserve original edges
        for (int y = 0; y < height; y++)
        {
            blurred.SetPixel(0, y, source.GetPixel(0, y));
            blurred.SetPixel(width - 1, y, source.GetPixel(width - 1, y));
        }
        for (int x = 0; x < width; x++)
        {
            blurred.SetPixel(x, 0, source.GetPixel(x, 0));
            blurred.SetPixel(x, height - 1, source.GetPixel(x, height - 1));
        }

        return blurred;
    }
}